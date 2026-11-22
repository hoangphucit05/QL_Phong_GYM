/* Kiểm thử Phiên 12. Các mutation đều được rollback; không giữ lại dữ liệu test. */
USE QL_PhongGym;
GO
SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

/* BUG-010: các procedure nghiệp vụ có actor bắt buộc ở tầng database. */
DECLARE @MaCheckIn BIGINT, @MaDKL INT, @MaLichHoc INT;
BEGIN TRY
    EXEC dbo.sp_CheckIn @MaTV=1,@MaPhong=NULL,@Kenh=N'Quầy',@MaCheckIn=@MaCheckIn OUTPUT,@MaNV=NULL;
    THROW 59101,N'FAIL BUG-010 sp_CheckIn vẫn nhận actor NULL.',1;
END TRY
BEGIN CATCH
    IF ERROR_NUMBER()=52106 PRINT N'PASS BUG-010 sp_CheckIn'; ELSE THROW;
END CATCH;
BEGIN TRY
    EXEC dbo.sp_CheckOut @MaTV=1,@MaNV=NULL;
    THROW 59102,N'FAIL BUG-010 sp_CheckOut vẫn nhận actor NULL.',1;
END TRY
BEGIN CATCH
    IF ERROR_NUMBER()=52107 PRINT N'PASS BUG-010 sp_CheckOut'; ELSE THROW;
END CATCH;
BEGIN TRY
    EXEC dbo.sp_DangKyLop @MaTV=1,@MaLop=1,@VaoDanhSachCho=1,@MaDKL=@MaDKL OUTPUT,@MaNV=NULL;
    THROW 59103,N'FAIL BUG-010 sp_DangKyLop vẫn nhận actor NULL.',1;
END TRY
BEGIN CATCH
    IF ERROR_NUMBER()=52108 PRINT N'PASS BUG-010 sp_DangKyLop'; ELSE THROW;
END CATCH;
BEGIN TRY
    EXEC dbo.sp_TaoLichHoc @MaLop=1,@ThoiGianBatDau='2099-01-01 08:00',@ThoiGianKetThuc='2099-01-01 09:00',@MaLichHoc=@MaLichHoc OUTPUT,@MaNV=NULL;
    THROW 59104,N'FAIL BUG-010 sp_TaoLichHoc vẫn nhận actor NULL.',1;
END TRY
BEGIN CATCH
    IF ERROR_NUMBER()=52109 PRINT N'PASS BUG-010 sp_TaoLichHoc'; ELSE THROW;
END CATCH;
BEGIN TRY
    EXEC dbo.sp_DiemDanhLop @MaDKL=1,@MaLichHoc=1,@TrangThai=N'Có mặt',@MaNV=NULL;
    THROW 59105,N'FAIL BUG-010 sp_DiemDanhLop vẫn nhận actor NULL.',1;
END TRY
BEGIN CATCH
    IF ERROR_NUMBER()=52110 PRINT N'PASS BUG-010 sp_DiemDanhLop'; ELSE THROW;
END CATCH;

/* BUG-016: CHECK constraint chặn số điện thoại/email sai ở database. */
BEGIN TRY
    BEGIN TRAN;
    UPDATE dbo.ThanhVien SET SDT=N'123' WHERE MaTV=(SELECT TOP(1) MaTV FROM dbo.ThanhVien);
    ROLLBACK;
    THROW 59106,N'FAIL BUG-016 số điện thoại hội viên không bị chặn.',1;
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK;
    IF ERROR_NUMBER()=547 PRINT N'PASS BUG-016 ThanhVien.SDT'; ELSE THROW;
END CATCH;
BEGIN TRY
    BEGIN TRAN;
    UPDATE dbo.ThanhVien SET Email=N'not-an-email' WHERE MaTV=(SELECT TOP(1) MaTV FROM dbo.ThanhVien);
    ROLLBACK;
    THROW 59107,N'FAIL BUG-016 email hội viên không bị chặn.',1;
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK;
    IF ERROR_NUMBER()=547 PRINT N'PASS BUG-016 ThanhVien.Email'; ELSE THROW;
END CATCH;
BEGIN TRY
    BEGIN TRAN;
    UPDATE dbo.NhanVien SET SDT=N'123' WHERE MaNV=(SELECT TOP(1) MaNV FROM dbo.NhanVien);
    ROLLBACK;
    THROW 59108,N'FAIL BUG-016 số điện thoại nhân viên không bị chặn.',1;
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK;
    IF ERROR_NUMBER()=547 PRINT N'PASS BUG-016 NhanVien.SDT'; ELSE THROW;
END CATCH;
BEGIN TRY
    BEGIN TRAN;
    UPDATE dbo.NhanVien SET Email=N'not-an-email' WHERE MaNV=(SELECT TOP(1) MaNV FROM dbo.NhanVien);
    ROLLBACK;
    THROW 59109,N'FAIL BUG-016 email nhân viên không bị chặn.',1;
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK;
    IF ERROR_NUMBER()=547 PRINT N'PASS BUG-016 NhanVien.Email'; ELSE THROW;
END CATCH;

/* BUG-017: năm lần sai khóa tạm thời, đăng nhập thành công reset và ghi audit. */
DECLARE @MaTK INT, @TenDangNhap VARCHAR(50), @SoLanSai INT, @KhoaDen DATETIME2(0), @AuditTruoc INT, @AuditSau INT;
SELECT TOP(1) @MaTK=tk.MaTK,@TenDangNhap=tk.TenDangNhap
FROM dbo.TaiKhoan tk
WHERE tk.TrangThai=N'Hoạt động'
ORDER BY tk.MaTK;
IF @MaTK IS NULL THROW 59110,N'Không có tài khoản hoạt động để test BUG-017.',1;
SELECT @AuditTruoc=COUNT(*) FROM dbo.LichSuDangNhap WHERE MaTK=@MaTK;
BEGIN TRAN;
EXEC dbo.sp_GhiKetQuaDangNhap @MaTK=@MaTK,@TenDangNhap=@TenDangNhap,@ThanhCong=0,@ThietBi=N'QA';
EXEC dbo.sp_GhiKetQuaDangNhap @MaTK=@MaTK,@TenDangNhap=@TenDangNhap,@ThanhCong=0,@ThietBi=N'QA';
EXEC dbo.sp_GhiKetQuaDangNhap @MaTK=@MaTK,@TenDangNhap=@TenDangNhap,@ThanhCong=0,@ThietBi=N'QA';
EXEC dbo.sp_GhiKetQuaDangNhap @MaTK=@MaTK,@TenDangNhap=@TenDangNhap,@ThanhCong=0,@ThietBi=N'QA';
EXEC dbo.sp_GhiKetQuaDangNhap @MaTK=@MaTK,@TenDangNhap=@TenDangNhap,@ThanhCong=0,@ThietBi=N'QA';
SELECT @SoLanSai=SoLanDangNhapSai,@KhoaDen=KhoaDen FROM dbo.TaiKhoan WHERE MaTK=@MaTK;
SELECT @AuditSau=COUNT(*) FROM dbo.LichSuDangNhap WHERE MaTK=@MaTK;
IF @SoLanSai<5 OR @KhoaDen IS NULL OR @KhoaDen<=SYSDATETIME() OR @AuditSau<>@AuditTruoc+5
    THROW 59111,N'FAIL BUG-017 khóa tạm thời/audit sau năm lần sai.',1;
EXEC dbo.sp_GhiKetQuaDangNhap @MaTK=@MaTK,@TenDangNhap=@TenDangNhap,@ThanhCong=1,@ThietBi=N'QA';
SELECT @SoLanSai=SoLanDangNhapSai,@KhoaDen=KhoaDen FROM dbo.TaiKhoan WHERE MaTK=@MaTK;
SELECT @AuditSau=COUNT(*) FROM dbo.LichSuDangNhap WHERE MaTK=@MaTK;
IF @SoLanSai<>0 OR @KhoaDen IS NOT NULL OR @AuditSau<>@AuditTruoc+6
    THROW 59112,N'FAIL BUG-017 đăng nhập thành công không reset.',1;
PRINT N'PASS BUG-017 lockout và audit';
ROLLBACK;

PRINT N'PHIEN_12_PASS';
GO
