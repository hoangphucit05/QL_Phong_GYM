-- 21_sua_giahan_checkin_baotri.sql
-- Migration Phiên 13: Sửa tính tiền gia hạn gói, thứ tự check-in FIFO và hoàn tất bảo trì thiết bị
USE QL_PhongGym;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

-- 0. Bỏ ràng buộc UNIQUE 1:1 trên HoaDonGoiTap để 1 gói tập có thể có nhiều hóa đơn (đăng ký gốc + các lần gia hạn)
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UQ_HDGoiTap_DKG' AND object_id = OBJECT_ID('dbo.HoaDonGoiTap'))
    ALTER TABLE dbo.HoaDonGoiTap DROP CONSTRAINT UQ_HDGoiTap_DKG;
GO

-- 1. BUG-021: Cập nhật sp_CheckIn để ưu tiên gói hết hạn sớm nhất (FIFO)
IF OBJECT_ID('dbo.sp_CheckIn', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_CheckIn;
GO

CREATE PROCEDURE dbo.sp_CheckIn
    @MaTV INT, @MaPhong INT=NULL, @Kenh NVARCHAR(20)=N'Quầy', @MaCheckIn BIGINT OUTPUT, @MaNV INT=NULL
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|Lễ tân|Nhân viên')=0
            THROW 52106,N'Tài khoản thực hiện check-in không có quyền hoặc không còn hoạt động.',1;
        DECLARE @HienTai DATETIME2(0)=SYSDATETIME(), @GioHienTai TIME(0)=CAST(SYSDATETIME() AS TIME(0)), @MaDKG INT;
        IF @MaPhong IS NOT NULL AND NOT EXISTS(SELECT 1 FROM dbo.PhongTap WHERE MaPhong=@MaPhong AND TrangThai=N'Hoạt động')
            THROW 50038,N'Phòng không tồn tại hoặc đang bảo trì/đóng.',1;
        IF EXISTS(SELECT 1 FROM dbo.LichSuCheckIn WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND ThoiGianRa IS NULL)
            THROW 50010,N'Thành viên đang có lượt check-in mở.',1;
        -- Ưu tiên gói hết hạn sớm nhất (FIFO: NgayKetThuc ASC, MaDKG ASC)
        SELECT TOP(1) @MaDKG=d.MaDKG
        FROM dbo.DangKyGoi d WITH(UPDLOCK,HOLDLOCK)
        JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi
        JOIN dbo.ThanhVien t ON t.MaTV=d.MaTV
        WHERE d.MaTV=@MaTV AND d.TrangThai=N'Còn hạn' AND d.NgayBatDau<=CAST(@HienTai AS DATE) AND d.NgayKetThuc>=CAST(@HienTai AS DATE)
          AND t.TrangThai=N'Hoạt động' AND t.DaXoa=0 AND (g.GioiHanLuot IS NULL OR ISNULL(d.SoLuotDaDung,0)<g.GioiHanLuot)
          AND (g.GioBatDau IS NULL OR g.GioKetThuc IS NULL OR (g.GioBatDau<=g.GioKetThuc AND @GioHienTai BETWEEN g.GioBatDau AND g.GioKetThuc) OR (g.GioBatDau>g.GioKetThuc AND (@GioHienTai>=g.GioBatDau OR @GioHienTai<=g.GioKetThuc)))
          AND EXISTS(SELECT 1 FROM dbo.HoaDonGoiTap x JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD JOIN dbo.HoaDon h ON h.MaHD=c.MaHD WHERE x.MaDKG=d.MaDKG AND h.TrangThai=N'Đã thanh toán')
        ORDER BY d.NgayKetThuc ASC, d.MaDKG ASC;

        IF @MaDKG IS NULL THROW 50011,N'Không có gói hợp lệ đã thanh toán, còn lượt và đúng khung giờ để check-in.',1;
        INSERT dbo.LichSuCheckIn(MaTV,MaDKG,MaPhong,Kenh) VALUES(@MaTV,@MaDKG,@MaPhong,@Kenh);
        SET @MaCheckIn=CONVERT(BIGINT,SCOPE_IDENTITY());
        UPDATE dbo.DangKyGoi SET SoLuotDaDung=ISNULL(SoLuotDaDung,0)+1,NgayCapNhat=@HienTai WHERE MaDKG=@MaDKG;
        INSERT dbo.NhatKyHeThong(MaNV,HanhDong,DoiTuong,KhoaDoiTuong,NoiDung) VALUES(@MaNV,N'Check-in',N'LichSuCheckIn',CONVERT(NVARCHAR(30),@MaCheckIn),@Kenh);
        COMMIT;
    END TRY
    BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
END;
GO

-- 2. BUG-020: Cập nhật sp_GiaHanGoiTap để tính phí gia hạn và sinh hóa đơn thu tiền
IF OBJECT_ID('dbo.sp_GiaHanGoiTap', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GiaHanGoiTap;
GO

CREATE PROCEDURE dbo.sp_GiaHanGoiTap
    @MaDKG INT,
    @SoNgay INT,
    @MaNV INT = NULL,
    @MaHD INT = NULL OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV, N'Admin|Quản lý|Lễ tân|Nhân viên') = 0
            THROW 52109, N'Tài khoản thực hiện gia hạn không có quyền hoặc không còn hoạt động.', 1;

        IF @SoNgay <= 0
            THROW 50006, N'Số ngày gia hạn phải lớn hơn 0.', 1;

        DECLARE @MaTV INT, @MaGoi INT, @Cu DATE, @GiaGoi DECIMAL(18,2), @ThoiHanGoi INT;
        SELECT @MaTV = d.MaTV, @MaGoi = d.MaGoi, @Cu = d.NgayKetThuc, @GiaGoi = g.Gia, @ThoiHanGoi = g.ThoiHanNgay
        FROM dbo.DangKyGoi d WITH (UPDLOCK, HOLDLOCK)
        JOIN dbo.GoiTap g ON g.MaGoi = d.MaGoi
        WHERE d.MaDKG = @MaDKG AND d.TrangThai IN (N'Còn hạn', N'Hết hạn');

        IF @Cu IS NULL
            THROW 50007, N'Gói tập không tồn tại hoặc không ở trạng thái có thể gia hạn.', 1;

        -- Tính phí gia hạn theo đơn giá ngày
        DECLARE @DonGiaNgay DECIMAL(18,2) = ROUND(@GiaGoi / CASE WHEN ISNULL(@ThoiHanGoi, 0) > 0 THEN @ThoiHanGoi ELSE 30 END, 0);
        DECLARE @PhiGiaHan DECIMAL(18,2) = @DonGiaNgay * @SoNgay;

        -- Sinh hóa đơn gia hạn
        INSERT dbo.HoaDon(MaTV, MaNV, TongTien, TrangThai)
        VALUES (@MaTV, @MaNV, 0, N'Chưa thanh toán');
        SET @MaHD = SCOPE_IDENTITY();

        INSERT dbo.ChiTietHoaDon(MaHD, LoaiDichVu, MaThamChieu, SoLuong, DonGia)
        VALUES (@MaHD, N'Gói tập', @MaDKG, @SoNgay, @PhiGiaHan);

        INSERT dbo.HoaDonGoiTap(MaCTHD, MaDKG)
        SELECT MaCTHD, @MaDKG FROM dbo.ChiTietHoaDon WHERE MaHD = @MaHD;

        -- Cập nhật ngày kết thúc
        DECLARE @Moc DATE = CASE WHEN @Cu < CAST(SYSDATETIME() AS DATE) THEN CAST(SYSDATETIME() AS DATE) ELSE @Cu END;
        DECLARE @Moi DATE = DATEADD(DAY, @SoNgay, @Moc);

        UPDATE dbo.DangKyGoi
        SET NgayKetThuc = @Moi,
            TrangThai = N'Còn hạn',
            NgayCapNhat = SYSDATETIME()
        WHERE MaDKG = @MaDKG;

        INSERT dbo.LichSuGoiTap(MaDKG, HanhDong, TuNgay, DenNgay, MaNV)
        VALUES (@MaDKG, N'Gia hạn', @Cu, @Moi, @MaNV);

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

-- 3. BUG-024: Cập nhật sp_BaoTriThietBi và tạo sp_HoanTatBaoTri
IF OBJECT_ID('dbo.sp_BaoTriThietBi', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_BaoTriThietBi;
GO

CREATE PROCEDURE dbo.sp_BaoTriThietBi
    @MaTB INT,
    @MaNV INT,
    @NoiDung NVARCHAR(255),
    @ChiPhi DECIMAL(18,2),
    @NgayDuKien DATE = NULL,
    @MaBaoTri INT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV, N'Admin|Quản lý|Nhân viên') = 0
            THROW 52110, N'Tài khoản thực hiện lập phiếu bảo trì không có quyền hoặc không còn hoạt động.', 1;

        IF NOT EXISTS (SELECT 1 FROM dbo.ThietBi WHERE MaTB = @MaTB)
            THROW 50027, N'Thiết bị không tồn tại.', 1;

        IF @ChiPhi < 0
            THROW 50039, N'Chi phí bảo trì không thể âm.', 1;

        -- Khi lập phiếu bảo trì mới, thiết bị luôn chuyển sang 'Bảo trì' và phiếu ở trạng thái 'Đang bảo trì'
        INSERT dbo.BaoTriThietBi(MaTB, MaNV, NoiDung, ChiPhi, NgayDuKien, TrangThai)
        VALUES (@MaTB, @MaNV, @NoiDung, @ChiPhi, @NgayDuKien, N'Đang bảo trì');
        SET @MaBaoTri = SCOPE_IDENTITY();

        UPDATE dbo.ThietBi
        SET TinhTrang = N'Bảo trì'
        WHERE MaTB = @MaTB;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

IF OBJECT_ID('dbo.sp_HoanTatBaoTri', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_HoanTatBaoTri;
GO

CREATE PROCEDURE dbo.sp_HoanTatBaoTri
    @MaBaoTri INT,
    @MaNV INT,
    @GhiChu NVARCHAR(255) = NULL
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV, N'Admin|Quản lý|Nhân viên') = 0
            THROW 52111, N'Tài khoản thực hiện hoàn tất bảo trì không có quyền hoặc không còn hoạt động.', 1;

        DECLARE @MaTB INT, @TrangThaiHienTai NVARCHAR(50);
        SELECT @MaTB = MaTB, @TrangThaiHienTai = TrangThai
        FROM dbo.BaoTriThietBi WITH (UPDLOCK, HOLDLOCK)
        WHERE MaBaoTri = @MaBaoTri;

        IF @MaTB IS NULL
            THROW 50040, N'Phiếu bảo trì không tồn tại.', 1;

        IF @TrangThaiHienTai = N'Hoàn thành'
            THROW 50041, N'Phiếu bảo trì này đã hoàn thành trước đó.', 1;

        UPDATE dbo.BaoTriThietBi
        SET TrangThai = N'Hoàn thành',
            NgayHoanThanh = CAST(SYSDATETIME() AS DATE),
            GhiChu = ISNULL(@GhiChu, GhiChu)
        WHERE MaBaoTri = @MaBaoTri;

        -- Nếu không còn phiếu bảo trì nào khác đang mở cho thiết bị này, trả thiết bị về 'Hoạt động'
        IF NOT EXISTS (
            SELECT 1 FROM dbo.BaoTriThietBi
            WHERE MaTB = @MaTB AND MaBaoTri <> @MaBaoTri AND TrangThai IN (N'Đang bảo trì', N'Chờ bảo trì')
        )
        BEGIN
            UPDATE dbo.ThietBi
            SET TinhTrang = N'Hoạt động'
            WHERE MaTB = @MaTB;
        END;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO
