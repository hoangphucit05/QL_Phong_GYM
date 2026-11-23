/*
   Migration 20 - Phiên 12: actor bắt buộc ở database, audit/lockout đăng nhập
   và ràng buộc định dạng dữ liệu liên hệ.
   Chạy sau migration 19.
*/
USE QL_PhongGym;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
GO

IF COL_LENGTH(N'dbo.TaiKhoan', N'SoLanDangNhapSai') IS NULL
    ALTER TABLE dbo.TaiKhoan ADD SoLanDangNhapSai INT NOT NULL CONSTRAINT DF_TaiKhoan_SoLanDangNhapSai DEFAULT 0;
IF COL_LENGTH(N'dbo.TaiKhoan', N'KhoaDen') IS NULL
    ALTER TABLE dbo.TaiKhoan ADD KhoaDen DATETIME2(0) NULL;
IF COL_LENGTH(N'dbo.LichSuDangNhap', N'TenDangNhap') IS NULL
    ALTER TABLE dbo.LichSuDangNhap ADD TenDangNhap VARCHAR(50) NULL;
GO

DECLARE @fkLogin NVARCHAR(300) = N'';
SELECT @fkLogin = @fkLogin + N'ALTER TABLE dbo.LichSuDangNhap DROP CONSTRAINT ' + QUOTENAME(fk.name) + N';'
FROM sys.foreign_keys fk
WHERE fk.parent_object_id = OBJECT_ID(N'dbo.LichSuDangNhap')
  AND EXISTS (SELECT 1 FROM sys.foreign_key_columns fkc WHERE fkc.constraint_object_id=fk.object_id
              AND fkc.parent_column_id= (SELECT column_id FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.LichSuDangNhap') AND name=N'MaTK'));
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.LichSuDangNhap') AND name=N'MaTK' AND is_nullable=0)
BEGIN
    IF @fkLogin <> N'' EXEC sys.sp_executesql @fkLogin;
    ALTER TABLE dbo.LichSuDangNhap ALTER COLUMN MaTK INT NULL;
END;
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name=N'FK_LichSuDangNhap_TaiKhoan_v3')
    ALTER TABLE dbo.LichSuDangNhap ADD CONSTRAINT FK_LichSuDangNhap_TaiKhoan_v3 FOREIGN KEY(MaTK) REFERENCES dbo.TaiKhoan(MaTK);
GO

IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_ThanhVien_SDT_v3')
    ALTER TABLE dbo.ThanhVien ADD CONSTRAINT CK_ThanhVien_SDT_v3
        CHECK (SDT NOT LIKE '%[^0-9]%' AND SDT LIKE '0%' AND LEN(SDT) BETWEEN 10 AND 11);
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_ThanhVien_Email_v3')
    ALTER TABLE dbo.ThanhVien ADD CONSTRAINT CK_ThanhVien_Email_v3
        CHECK (Email IS NULL OR (Email NOT LIKE '% %' AND Email LIKE '%_@_%._%'));
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_NhanVien_SDT_v3')
    ALTER TABLE dbo.NhanVien ADD CONSTRAINT CK_NhanVien_SDT_v3
        CHECK (SDT NOT LIKE '%[^0-9]%' AND SDT LIKE '0%' AND LEN(SDT) BETWEEN 10 AND 11);
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_NhanVien_Email_v3')
    ALTER TABLE dbo.NhanVien ADD CONSTRAINT CK_NhanVien_Email_v3
        CHECK (Email IS NULL OR (Email NOT LIKE '% %' AND Email LIKE '%_@_%._%'));
GO

CREATE OR ALTER PROCEDURE dbo.sp_GhiKetQuaDangNhap
    @MaTK INT=NULL,
    @TenDangNhap VARCHAR(50),
    @ThanhCong BIT,
    @ThietBi NVARCHAR(255)=NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRAN;
    IF @MaTK IS NOT NULL
    BEGIN
        IF @ThanhCong=1
            UPDATE dbo.TaiKhoan SET SoLanDangNhapSai=0,KhoaDen=NULL,LanDangNhapCuoi=SYSDATETIME() WHERE MaTK=@MaTK;
        ELSE
            UPDATE dbo.TaiKhoan
            SET SoLanDangNhapSai=ISNULL(SoLanDangNhapSai,0)+1,
                KhoaDen=CASE WHEN ISNULL(SoLanDangNhapSai,0)+1>=5 THEN DATEADD(MINUTE,15,SYSDATETIME()) ELSE KhoaDen END
            WHERE MaTK=@MaTK;
    END;
    INSERT dbo.LichSuDangNhap(MaTK,TenDangNhap,ThanhCong,ThietBi)
    VALUES(@MaTK,@TenDangNhap,@ThanhCong,@ThietBi);
    COMMIT;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_CheckIn
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
        SELECT TOP(1) @MaDKG=d.MaDKG
        FROM dbo.DangKyGoi d WITH(UPDLOCK,HOLDLOCK) JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi JOIN dbo.ThanhVien t ON t.MaTV=d.MaTV
        WHERE d.MaTV=@MaTV AND d.TrangThai=N'Còn hạn' AND d.NgayBatDau<=CAST(@HienTai AS DATE) AND d.NgayKetThuc>=CAST(@HienTai AS DATE)
          AND t.TrangThai=N'Hoạt động' AND t.DaXoa=0 AND (g.GioiHanLuot IS NULL OR ISNULL(d.SoLuotDaDung,0)<g.GioiHanLuot)
          AND (g.GioBatDau IS NULL OR g.GioKetThuc IS NULL OR (g.GioBatDau<=g.GioKetThuc AND @GioHienTai BETWEEN g.GioBatDau AND g.GioKetThuc) OR (g.GioBatDau>g.GioKetThuc AND (@GioHienTai>=g.GioBatDau OR @GioHienTai<=g.GioKetThuc)))
          AND EXISTS(SELECT 1 FROM dbo.HoaDonGoiTap x JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD JOIN dbo.HoaDon h ON h.MaHD=c.MaHD WHERE x.MaDKG=d.MaDKG AND h.TrangThai=N'Đã thanh toán')
        ORDER BY d.NgayKetThuc DESC,d.MaDKG DESC;
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

CREATE OR ALTER PROCEDURE dbo.sp_CheckOut @MaTV INT, @MaNV INT=NULL AS
BEGIN TRY
    IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|Lễ tân|Nhân viên')=0
        THROW 52107,N'Tài khoản thực hiện check-out không có quyền hoặc không còn hoạt động.',1;
    UPDATE dbo.LichSuCheckIn SET ThoiGianRa=SYSDATETIME() WHERE MaTV=@MaTV AND ThoiGianRa IS NULL;
    IF @@ROWCOUNT=0 THROW 50012,N'Không tìm thấy lượt check-in mở.',1;
END TRY BEGIN CATCH THROW; END CATCH;
GO

CREATE OR ALTER PROCEDURE dbo.sp_DangKyLop
    @MaTV INT, @MaLop INT, @VaoDanhSachCho BIT=1, @MaDKL INT OUTPUT, @MaNV INT=NULL
AS
BEGIN TRY
    SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
    IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|Lễ tân|Nhân viên')=0
        THROW 52108,N'Tài khoản thực hiện đăng ký lớp không có quyền hoặc không còn hoạt động.',1;
    IF NOT EXISTS(SELECT 1 FROM dbo.ThanhVien WHERE MaTV=@MaTV AND TrangThai=N'Hoạt động' AND DaXoa=0) THROW 50028,N'Thành viên không hoạt động.',1;
    DECLARE @SucChua INT,@DangKy INT;
    SELECT @SucChua=SucChua FROM dbo.LopTap WITH(UPDLOCK,HOLDLOCK) WHERE MaLop=@MaLop AND TrangThai=N'Đang mở';
    IF @SucChua IS NULL THROW 50013,N'Lớp không mở hoặc không tồn tại.',1;
    IF EXISTS(SELECT 1 FROM dbo.DangKyLop WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND MaLop=@MaLop AND TrangThai=N'Đang tham gia') THROW 50029,N'Thành viên đã đăng ký lớp này.',1;
    IF EXISTS(SELECT 1 FROM dbo.DanhSachCho WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND MaLop=@MaLop AND TrangThai=N'Đang chờ') THROW 50030,N'Thành viên đang ở danh sách chờ của lớp này.',1;
    SELECT @DangKy=COUNT(*) FROM dbo.DangKyLop WHERE MaLop=@MaLop AND TrangThai=N'Đang tham gia';
    IF @DangKy>=@SucChua
    BEGIN
        IF @VaoDanhSachCho=1 BEGIN INSERT dbo.DanhSachCho(MaLop,MaTV) VALUES(@MaLop,@MaTV); SET @MaDKL=NULL; COMMIT; RETURN; END;
        THROW 50014,N'Lớp đã đủ sức chứa.',1;
    END;
    INSERT dbo.DangKyLop(MaTV,MaLop) VALUES(@MaTV,@MaLop); SET @MaDKL=SCOPE_IDENTITY(); COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO

CREATE OR ALTER PROCEDURE dbo.sp_TaoLichHoc
    @MaLop INT, @ThoiGianBatDau DATETIME2(0), @ThoiGianKetThuc DATETIME2(0), @MaLichHoc INT OUTPUT, @MaNV INT=NULL
AS
BEGIN TRY
    SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
    IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|HLV|Huấn luyện viên')=0 THROW 52109,N'Tài khoản tạo lịch không có quyền hoặc không còn hoạt động.',1;
    IF @ThoiGianKetThuc<=@ThoiGianBatDau THROW 51070,N'Khung giờ lớp không hợp lệ.',1;
    IF CAST(@ThoiGianBatDau AS DATE)<CAST(SYSDATETIME() AS DATE) THROW 51071,N'Không thể tạo buổi học trong quá khứ.',1;
    DECLARE @HLV INT,@Phong INT,@SucChuaLop INT,@SucChuaPhong INT,@ThuTrongTuan TINYINT;
    SELECT @HLV=MaHLV,@Phong=MaPhong,@SucChuaLop=SucChua,@ThuTrongTuan=ThuTrongTuan FROM dbo.LopTap WITH(UPDLOCK,HOLDLOCK) WHERE MaLop=@MaLop AND TrangThai=N'Đang mở';
    IF @HLV IS NULL OR @Phong IS NULL THROW 51072,N'Lớp chưa mở hoặc chưa được gán phòng.',1;
    SELECT @SucChuaPhong=SucChua FROM dbo.PhongTap WITH(UPDLOCK,HOLDLOCK) WHERE MaPhong=@Phong AND TrangThai=N'Hoạt động';
    IF @SucChuaPhong IS NULL OR @SucChuaPhong<@SucChuaLop THROW 51073,N'Phòng không hoạt động hoặc không đủ sức chứa cho lớp.',1;
    IF @ThuTrongTuan IS NOT NULL AND @ThuTrongTuan<>CONVERT(TINYINT,(DATEDIFF(DAY,'19000101',CAST(@ThoiGianBatDau AS DATE))%7)+2) THROW 51074,N'Ngày tạo buổi học không đúng lịch cố định của lớp.',1;
    IF EXISTS(SELECT 1 FROM dbo.LichHoc WITH(UPDLOCK,HOLDLOCK) WHERE MaLop=@MaLop AND ThoiGianBatDau=@ThoiGianBatDau) THROW 51075,N'Buổi học này đã được tạo.',1;
    IF EXISTS(SELECT 1 FROM dbo.LichHoc WITH(UPDLOCK,HOLDLOCK) WHERE TrangThai=N'Đã lên lịch' AND (MaPhong=@Phong OR MaHLV=@HLV) AND @ThoiGianBatDau<ThoiGianKetThuc AND @ThoiGianKetThuc>ThoiGianBatDau) THROW 51076,N'Phòng hoặc HLV bị trùng lịch.',1;
    INSERT dbo.LichHoc(MaLop,MaHLV,MaPhong,ThoiGianBatDau,ThoiGianKetThuc) VALUES(@MaLop,@HLV,@Phong,@ThoiGianBatDau,@ThoiGianKetThuc); SET @MaLichHoc=SCOPE_IDENTITY(); COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO

CREATE OR ALTER PROCEDURE dbo.sp_DiemDanhLop
    @MaDKL INT, @MaLichHoc INT, @TrangThai NVARCHAR(30), @MaNV INT=NULL
AS
BEGIN TRY
    SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
    IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|Lễ tân|Nhân viên|HLV|Huấn luyện viên')=0 THROW 52110,N'Tài khoản điểm danh không có quyền hoặc không còn hoạt động.',1;
    IF @TrangThai NOT IN(N'Có mặt',N'Vắng mặt') THROW 51050,N'Trạng thái điểm danh không hợp lệ.',1;
    DECLARE @NgayDiemDanh DATE;
    SELECT @NgayDiemDanh=CAST(ThoiGianBatDau AS DATE) FROM dbo.LichHoc WITH(UPDLOCK,HOLDLOCK) WHERE MaLichHoc=@MaLichHoc AND TrangThai IN(N'Đã lên lịch',N'Đã diễn ra');
    IF @NgayDiemDanh IS NULL THROW 51077,N'Buổi học không tồn tại hoặc đã bị hủy.',1;
    IF @NgayDiemDanh>CAST(SYSDATETIME() AS DATE) THROW 51078,N'Không thể điểm danh cho buổi học trong tương lai.',1;
    IF NOT EXISTS(SELECT 1 FROM dbo.DangKyLop d JOIN dbo.LichHoc l ON l.MaLop=d.MaLop WHERE d.MaDKL=@MaDKL AND l.MaLichHoc=@MaLichHoc AND d.TrangThai=N'Đang tham gia') THROW 51079,N'Học viên không thuộc buổi học này hoặc đăng ký không còn hiệu lực.',1;
    UPDATE dbo.DiemDanhLop SET TrangThai=@TrangThai,NgayDiemDanh=@NgayDiemDanh WHERE MaDKL=@MaDKL AND MaLichHoc=@MaLichHoc;
    IF @@ROWCOUNT=0 INSERT dbo.DiemDanhLop(MaDKL,MaLichHoc,NgayDiemDanh,TrangThai) VALUES(@MaDKL,@MaLichHoc,@NgayDiemDanh,@TrangThai);
    UPDATE dbo.LichHoc SET TrangThai=N'Đã diễn ra' WHERE MaLichHoc=@MaLichHoc AND TrangThai=N'Đã lên lịch';
    INSERT dbo.NhatKyHeThong(MaNV,HanhDong,DoiTuong,KhoaDoiTuong,NoiDung) VALUES(@MaNV,N'Điểm danh',N'DiemDanhLop',CONVERT(NVARCHAR(30),@MaDKL),@TrangThai);
    COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
