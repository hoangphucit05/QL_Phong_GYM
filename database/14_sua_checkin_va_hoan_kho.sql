/*
    QL_PhongGym v3 - sửa check-in theo lượt/khung giờ và hoàn kho khi hủy hóa đơn.
    Chạy sau database/13_chuan_hoa_ma_nghiep_vu.sql trên database đã backup.
    Không sửa dữ liệu lịch sử; chỉ thay đổi các procedure nghiệp vụ.
*/
USE QL_PhongGym;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
GO

CREATE OR ALTER PROCEDURE dbo.sp_CheckIn
    @MaTV INT,
    @MaPhong INT = NULL,
    @Kenh NVARCHAR(20) = N'Quầy',
    @MaCheckIn BIGINT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        DECLARE @HienTai DATETIME2(0) = SYSDATETIME();
        DECLARE @GioHienTai TIME(0) = CAST(@HienTai AS TIME(0));
        DECLARE @MaDKG INT;

        IF EXISTS (
            SELECT 1
            FROM dbo.LichSuCheckIn WITH (UPDLOCK, HOLDLOCK)
            WHERE MaTV = @MaTV AND ThoiGianRa IS NULL
        )
            THROW 50010, N'Thành viên đang có lượt check-in mở.', 1;

        SELECT TOP (1) @MaDKG = d.MaDKG
        FROM dbo.DangKyGoi d WITH (UPDLOCK, HOLDLOCK)
        JOIN dbo.GoiTap g ON g.MaGoi = d.MaGoi
        JOIN dbo.ThanhVien t ON t.MaTV = d.MaTV
        WHERE d.MaTV = @MaTV
          AND d.TrangThai = N'Còn hạn'
          AND d.NgayBatDau <= CAST(@HienTai AS DATE)
          AND d.NgayKetThuc >= CAST(@HienTai AS DATE)
          AND t.TrangThai = N'Hoạt động'
          AND t.DaXoa = 0
          AND (g.GioiHanLuot IS NULL OR ISNULL(d.SoLuotDaDung, 0) < g.GioiHanLuot)
          AND (
              g.GioBatDau IS NULL OR g.GioKetThuc IS NULL
              OR (g.GioBatDau <= g.GioKetThuc AND @GioHienTai BETWEEN g.GioBatDau AND g.GioKetThuc)
              OR (g.GioBatDau > g.GioKetThuc AND (@GioHienTai >= g.GioBatDau OR @GioHienTai <= g.GioKetThuc))
          )
          AND EXISTS (
              SELECT 1
              FROM dbo.HoaDonGoiTap x
              JOIN dbo.ChiTietHoaDon c ON c.MaCTHD = x.MaCTHD
              JOIN dbo.HoaDon h ON h.MaHD = c.MaHD
              WHERE x.MaDKG = d.MaDKG
                AND h.TrangThai = N'Đã thanh toán'
          )
        ORDER BY d.NgayKetThuc DESC, d.MaDKG DESC;

        IF @MaDKG IS NULL
            THROW 50011, N'Không có gói hợp lệ đã thanh toán, còn lượt và đúng khung giờ để check-in.', 1;

        INSERT dbo.LichSuCheckIn(MaTV, MaDKG, MaPhong, Kenh)
        VALUES (@MaTV, @MaDKG, @MaPhong, @Kenh);
        SET @MaCheckIn = CONVERT(BIGINT, SCOPE_IDENTITY());

        UPDATE dbo.DangKyGoi
        SET SoLuotDaDung = ISNULL(SoLuotDaDung, 0) + 1,
            NgayCapNhat = @HienTai
        WHERE MaDKG = @MaDKG;

        IF @@ROWCOUNT = 0
            THROW 50037, N'Không thể cập nhật số lượt sử dụng của gói.', 1;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_HuyHoaDon
    @MaHD INT,
    @LyDo NVARCHAR(255),
    @MaNV INT = NULL
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        IF EXISTS (
            SELECT 1
            FROM dbo.ThanhToan WITH (UPDLOCK, HOLDLOCK)
            WHERE MaHD = @MaHD
              AND TrangThai = N'Thành công'
              AND LoaiGiaoDich = N'Thanh toán'
        )
            THROW 50034, N'Hóa đơn đã có thanh toán; hãy hoàn tiền trước khi hủy.', 1;

        DECLARE @HoanKho TABLE
        (
            MaSP INT NOT NULL PRIMARY KEY,
            SoLuong INT NOT NULL,
            TonTruoc INT NOT NULL,
            TonSau INT NOT NULL
        );

        INSERT @HoanKho(MaSP, SoLuong, TonTruoc, TonSau)
        SELECT hs.MaSP,
               SUM(ct.SoLuong),
               sp.SoLuongTon,
               sp.SoLuongTon + SUM(ct.SoLuong)
        FROM dbo.HoaDonSanPham hs
        JOIN dbo.ChiTietHoaDon ct ON ct.MaCTHD = hs.MaCTHD AND ct.MaHD = @MaHD
        JOIN dbo.SanPham sp WITH (UPDLOCK, HOLDLOCK) ON sp.MaSP = hs.MaSP
        GROUP BY hs.MaSP, sp.SoLuongTon;

        UPDATE dbo.HoaDon
        SET TrangThai = N'Hủy',
            LyDoHuy = @LyDo,
            NgayCapNhat = SYSDATETIME()
        WHERE MaHD = @MaHD
          AND TrangThai IN (N'Chưa thanh toán', N'Đã thanh toán một phần');

        IF @@ROWCOUNT = 0
            THROW 50035, N'Hóa đơn không thể hủy.', 1;

        UPDATE sp
        SET sp.SoLuongTon = hk.TonSau,
            sp.NgayCapNhat = SYSDATETIME()
        FROM dbo.SanPham sp
        JOIN @HoanKho hk ON hk.MaSP = sp.MaSP;

        INSERT dbo.LichSuTonKho
            (MaSP, LoaiBienDong, SoLuong, TonTruoc, TonSau, NguonThamChieu, MaThamChieu, MaNV)
        SELECT MaSP, N'Hoàn trả', SoLuong, TonTruoc, TonSau, N'Hóa đơn', @MaHD, @MaNV
        FROM @HoanKho;

        INSERT dbo.NhatKyHeThong(MaNV, HanhDong, DoiTuong, KhoaDoiTuong, NoiDung)
        VALUES (@MaNV, N'Hủy hóa đơn', N'HoaDon', CONVERT(NVARCHAR(30), @MaHD), @LyDo);

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO
