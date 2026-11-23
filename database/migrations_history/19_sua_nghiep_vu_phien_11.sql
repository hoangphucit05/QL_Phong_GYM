/*
   Migration 19 - Phiên 11: hoàn tiền, check-in phòng, bảo lưu và trạng thái PT.
   Chạy sau database/18_phan_quyen_nghiep_vu.sql trên database QL_PhongGym.
   Các procedure dùng transaction; có thể chạy lại an toàn.
*/
USE QL_PhongGym;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
GO

/* PT chưa thanh toán không được sử dụng/sắp lịch. */
DECLARE @dropStatus NVARCHAR(MAX) = N'';
SELECT @dropStatus = @dropStatus + N'ALTER TABLE dbo.DangKyPT DROP CONSTRAINT ' + QUOTENAME(cc.name) + N';'
FROM sys.check_constraints cc
WHERE cc.parent_object_id = OBJECT_ID(N'dbo.DangKyPT')
  AND cc.definition LIKE N'%TrangThai%';
IF @dropStatus <> N'' EXEC sys.sp_executesql @dropStatus;
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = N'CK_DangKyPT_TrangThai_v3')
    ALTER TABLE dbo.DangKyPT ADD CONSTRAINT CK_DangKyPT_TrangThai_v3
        CHECK (TrangThai IN (N'Chờ thanh toán', N'Đang học', N'Hoàn thành', N'Hủy'));
GO

CREATE OR ALTER PROCEDURE dbo.sp_DangKyPT
    @MaTV INT, @MaHLV INT, @SoBuoi INT, @MaKM INT = NULL,
    @NgayBatDau DATE = NULL, @NgayKetThuc DATE = NULL, @MaNV INT,
    @MaDKPT INT OUTPUT, @MaHD INT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        IF @SoBuoi <= 0 THROW 51030, N'Số buổi PT phải lớn hơn 0.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.ThanhVien WHERE MaTV=@MaTV AND TrangThai=N'Hoạt động' AND DaXoa=0)
            THROW 51031, N'Thành viên không hoạt động.', 1;
        DECLARE @Phi DECIMAL(18,2), @Giam DECIMAL(5,2)=0;
        SELECT @Phi=PhiThue FROM dbo.HuanLuyenVien h JOIN dbo.NhanVien n ON n.MaNV=h.MaNV
        WHERE h.MaHLV=@MaHLV AND n.TrangThai=N'Đang làm';
        IF @Phi IS NULL THROW 51032, N'Huấn luyện viên không hợp lệ.', 1;
        IF @MaKM IS NOT NULL
            SELECT @Giam=PhanTramGiam FROM dbo.KhuyenMai
            WHERE MaKM=@MaKM AND TrangThai=N'Hoạt động' AND CAST(GETDATE() AS DATE) BETWEEN NgayBatDau AND NgayKetThuc;
        IF @MaKM IS NOT NULL AND @Giam IS NULL THROW 51033, N'Khuyến mãi không còn hiệu lực.', 1;
        SET @NgayBatDau=ISNULL(@NgayBatDau, CAST(GETDATE() AS DATE));
        SET @NgayKetThuc=ISNULL(@NgayKetThuc, DATEADD(DAY, @SoBuoi*7, @NgayBatDau));
        IF @NgayKetThuc < @NgayBatDau THROW 51034, N'Khoảng thời gian PT không hợp lệ.', 1;

        INSERT dbo.DangKyPT(MaTV,MaHLV,MaKM,SoBuoi,NgayBatDau,NgayKetThuc,TongTien,TrangThai)
        VALUES(@MaTV,@MaHLV,@MaKM,@SoBuoi,@NgayBatDau,@NgayKetThuc,
               ROUND(@Phi*@SoBuoi*(1-ISNULL(@Giam,0)/100),2),N'Chờ thanh toán');
        SET @MaDKPT=SCOPE_IDENTITY();
        INSERT dbo.HoaDon(MaTV,MaNV,TongTien,TrangThai) VALUES(@MaTV,@MaNV,0,N'Chưa thanh toán');
        SET @MaHD=SCOPE_IDENTITY();
        INSERT dbo.ChiTietHoaDon(MaHD,LoaiDichVu,MaThamChieu,SoLuong,DonGia)
        SELECT @MaHD,N'PT',@MaDKPT,SoBuoi,ROUND(TongTien/SoBuoi,2)
        FROM dbo.DangKyPT WHERE MaDKPT=@MaDKPT;
        INSERT dbo.HoaDonPT(MaCTHD,MaDKPT)
        SELECT MaCTHD,@MaDKPT FROM dbo.ChiTietHoaDon WHERE MaHD=@MaHD;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK; THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_ThanhToanHoaDon
    @MaHD INT, @SoTien DECIMAL(18,2), @PhuongThuc NVARCHAR(50), @MaNV INT=NULL, @MaTT INT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        IF @SoTien<=0 THROW 50019, N'Số tiền thanh toán phải dương.', 1;
        DECLARE @Tong DECIMAL(18,2), @DaTra DECIMAL(18,2);
        SELECT @Tong=TongTien FROM dbo.HoaDon WITH (UPDLOCK,HOLDLOCK)
        WHERE MaHD=@MaHD AND TrangThai NOT IN(N'Hủy',N'Đã hoàn tiền');
        IF @Tong IS NULL THROW 50020, N'Hóa đơn không thể thanh toán.', 1;
        SELECT @DaTra=ISNULL(SUM(CASE WHEN LoaiGiaoDich=N'Thanh toán' THEN SoTien ELSE -SoTien END),0)
        FROM dbo.ThanhToan WHERE MaHD=@MaHD AND TrangThai=N'Thành công';
        IF @DaTra+@SoTien>@Tong THROW 50021, N'Thanh toán vượt quá số tiền phải thu.', 1;
        INSERT dbo.ThanhToan(MaHD,SoTien,PhuongThuc,TrangThai,LoaiGiaoDich,MaNV)
        VALUES(@MaHD,@SoTien,@PhuongThuc,N'Thành công',N'Thanh toán',@MaNV);
        SET @MaTT=SCOPE_IDENTITY();
        UPDATE dbo.HoaDon SET TrangThai=CASE WHEN @DaTra+@SoTien=@Tong THEN N'Đã thanh toán' ELSE N'Đã thanh toán một phần' END,
            NgayCapNhat=SYSDATETIME() WHERE MaHD=@MaHD;
        IF @DaTra+@SoTien=@Tong
        BEGIN
            UPDATE d SET TrangThai=N'Còn hạn',NgayCapNhat=SYSDATETIME()
            FROM dbo.DangKyGoi d JOIN dbo.HoaDonGoiTap x ON x.MaDKG=d.MaDKG
            JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD
            WHERE c.MaHD=@MaHD AND d.TrangThai=N'Chờ thanh toán';
            UPDATE d SET TrangThai=N'Đang học'
            FROM dbo.DangKyPT d JOIN dbo.HoaDonPT x ON x.MaDKPT=d.MaDKPT
            JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD
            WHERE c.MaHD=@MaHD AND d.TrangThai=N'Chờ thanh toán';
        END;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK; THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_HoanTien
    @MaHD INT, @SoTien DECIMAL(18,2), @PhuongThuc NVARCHAR(50), @MaNV INT=NULL, @MaTT INT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        DECLARE @DaThu DECIMAL(18,2), @Tong DECIMAL(18,2);
        SELECT @Tong=TongTien FROM dbo.HoaDon WITH (UPDLOCK,HOLDLOCK) WHERE MaHD=@MaHD;
        IF @Tong IS NULL THROW 50020, N'Hóa đơn không tồn tại.', 1;
        SELECT @DaThu=ISNULL(SUM(CASE WHEN LoaiGiaoDich=N'Thanh toán' THEN SoTien ELSE -SoTien END),0)
        FROM dbo.ThanhToan WITH (UPDLOCK,HOLDLOCK) WHERE MaHD=@MaHD AND TrangThai=N'Thành công';
        IF @SoTien<=0 OR @SoTien>@DaThu THROW 50022, N'Số tiền hoàn không hợp lệ.', 1;
        INSERT dbo.ThanhToan(MaHD,SoTien,PhuongThuc,TrangThai,LoaiGiaoDich,MaNV)
        VALUES(@MaHD,@SoTien,@PhuongThuc,N'Thành công',N'Hoàn tiền',@MaNV);
        SET @MaTT=SCOPE_IDENTITY();
        UPDATE dbo.HoaDon SET TrangThai=CASE WHEN @SoTien=@DaThu THEN N'Đã hoàn tiền' ELSE N'Đã thanh toán một phần' END,
            NgayCapNhat=SYSDATETIME() WHERE MaHD=@MaHD;

        IF @SoTien=@DaThu
        BEGIN
            UPDATE d SET TrangThai=N'Hủy',NgayCapNhat=SYSDATETIME()
            FROM dbo.DangKyGoi d JOIN dbo.HoaDonGoiTap x ON x.MaDKG=d.MaDKG
            JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD WHERE c.MaHD=@MaHD AND d.TrangThai<>N'Hủy';
            INSERT dbo.LichSuGoiTap(MaDKG,HanhDong,GhiChu,MaNV)
            SELECT d.MaDKG,N'Hủy do hoàn tiền toàn bộ',N'Hóa đơn '+CONVERT(NVARCHAR(30),@MaHD),@MaNV
            FROM dbo.DangKyGoi d JOIN dbo.HoaDonGoiTap x ON x.MaDKG=d.MaDKG
            JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD WHERE c.MaHD=@MaHD;
            UPDATE d SET TrangThai=N'Hủy'
            FROM dbo.DangKyPT d JOIN dbo.HoaDonPT x ON x.MaDKPT=d.MaDKPT
            JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD WHERE c.MaHD=@MaHD AND d.TrangThai<>N'Hủy';
        END;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK; THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_BaoLuuGoiTap
    @MaDKG INT, @TuNgay DATE, @DenNgay DATE, @LyDo NVARCHAR(500), @MaNV INT=NULL
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        DECLARE @HomNay DATE=CAST(SYSDATETIME() AS DATE), @BatDau DATE, @KetThuc DATE;
        IF @DenNgay<@TuNgay THROW 50008, N'Khoảng bảo lưu không hợp lệ.', 1;
        IF @TuNgay<@HomNay THROW 50040, N'Ngày bắt đầu bảo lưu không được ở quá khứ.', 1;
        IF NULLIF(LTRIM(RTRIM(@LyDo)),N'') IS NULL THROW 50041, N'Lý do bảo lưu không được để trống.', 1;
        SELECT @BatDau=NgayBatDau,@KetThuc=NgayKetThuc FROM dbo.DangKyGoi WITH(UPDLOCK,HOLDLOCK)
        WHERE MaDKG=@MaDKG AND TrangThai=N'Còn hạn';
        IF @BatDau IS NULL THROW 50009, N'Chỉ bảo lưu gói còn hạn.', 1;
        IF @TuNgay<@BatDau OR @DenNgay>@KetThuc
            THROW 50042, N'Khoảng bảo lưu phải nằm trong thời hạn gói tập.', 1;
        IF EXISTS
        (
            SELECT 1 FROM dbo.BaoLuuGoiTap WITH(UPDLOCK,HOLDLOCK)
            WHERE MaDKG=@MaDKG AND TrangThai=N'Đang bảo lưu'
              AND @TuNgay<=DenNgay AND @DenNgay>=TuNgay
        ) THROW 50043, N'Khoảng bảo lưu bị trùng với lần bảo lưu đang có.', 1;
        INSERT dbo.BaoLuuGoiTap(MaDKG,TuNgay,DenNgay,LyDo,MaNV)
        VALUES(@MaDKG,@TuNgay,@DenNgay,LTRIM(RTRIM(@LyDo)),@MaNV);
        UPDATE dbo.DangKyGoi SET TrangThai=N'Tạm dừng',NgayTamDung=SYSDATETIME(),
            TongSoNgayBaoLuu=TongSoNgayBaoLuu+DATEDIFF(DAY,@TuNgay,@DenNgay)+1,NgayCapNhat=SYSDATETIME()
        WHERE MaDKG=@MaDKG;
        INSERT dbo.LichSuGoiTap(MaDKG,HanhDong,TuNgay,DenNgay,GhiChu,MaNV)
        VALUES(@MaDKG,N'Bảo lưu',@TuNgay,@DenNgay,LTRIM(RTRIM(@LyDo)),@MaNV);
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK; THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_CheckIn
    @MaTV INT, @MaPhong INT=NULL, @Kenh NVARCHAR(20)=N'Quầy', @MaCheckIn BIGINT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        DECLARE @HienTai DATETIME2(0)=SYSDATETIME(), @GioHienTai TIME(0)=CAST(SYSDATETIME() AS TIME(0)), @MaDKG INT;
        IF @MaPhong IS NOT NULL AND NOT EXISTS
            (SELECT 1 FROM dbo.PhongTap WHERE MaPhong=@MaPhong AND TrangThai=N'Hoạt động')
            THROW 50038, N'Phòng không tồn tại hoặc đang bảo trì/đóng.', 1;
        IF EXISTS(SELECT 1 FROM dbo.LichSuCheckIn WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND ThoiGianRa IS NULL)
            THROW 50010, N'Thành viên đang có lượt check-in mở.', 1;
        SELECT TOP(1) @MaDKG=d.MaDKG
        FROM dbo.DangKyGoi d WITH(UPDLOCK,HOLDLOCK) JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi JOIN dbo.ThanhVien t ON t.MaTV=d.MaTV
        WHERE d.MaTV=@MaTV AND d.TrangThai=N'Còn hạn' AND d.NgayBatDau<=CAST(@HienTai AS DATE) AND d.NgayKetThuc>=CAST(@HienTai AS DATE)
          AND t.TrangThai=N'Hoạt động' AND t.DaXoa=0 AND (g.GioiHanLuot IS NULL OR ISNULL(d.SoLuotDaDung,0)<g.GioiHanLuot)
          AND (g.GioBatDau IS NULL OR g.GioKetThuc IS NULL OR
              (g.GioBatDau<=g.GioKetThuc AND @GioHienTai BETWEEN g.GioBatDau AND g.GioKetThuc) OR
              (g.GioBatDau>g.GioKetThuc AND (@GioHienTai>=g.GioBatDau OR @GioHienTai<=g.GioKetThuc)))
          AND EXISTS(SELECT 1 FROM dbo.HoaDonGoiTap x JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD JOIN dbo.HoaDon h ON h.MaHD=c.MaHD
                     WHERE x.MaDKG=d.MaDKG AND h.TrangThai=N'Đã thanh toán')
        ORDER BY d.NgayKetThuc DESC,d.MaDKG DESC;
        IF @MaDKG IS NULL THROW 50011, N'Không có gói hợp lệ đã thanh toán, còn lượt và đúng khung giờ để check-in.', 1;
        INSERT dbo.LichSuCheckIn(MaTV,MaDKG,MaPhong,Kenh) VALUES(@MaTV,@MaDKG,@MaPhong,@Kenh);
        SET @MaCheckIn=CONVERT(BIGINT,SCOPE_IDENTITY());
        UPDATE dbo.DangKyGoi SET SoLuotDaDung=ISNULL(SoLuotDaDung,0)+1,NgayCapNhat=@HienTai WHERE MaDKG=@MaDKG;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK; THROW;
    END CATCH;
END;
GO
