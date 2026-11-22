/* Kiểm thử Phiên 11. Mọi mutation đều rollback, không đổi dữ liệu demo. */
USE QL_PhongGym;
GO
SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

DECLARE @MaDKG INT=1, @MaHD INT, @MaNV INT, @SoTien DECIMAL(18,2), @MaTT INT, @MaCheckIn BIGINT;
SELECT @MaHD=h.MaHD,@SoTien=h.TongTien
FROM dbo.DangKyGoi d JOIN dbo.HoaDonGoiTap x ON x.MaDKG=d.MaDKG
JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD JOIN dbo.HoaDon h ON h.MaHD=c.MaHD
WHERE d.MaDKG=@MaDKG AND h.TrangThai=N'Đã thanh toán';
SET @MaNV=(SELECT TOP(1) MaNV FROM dbo.TaiKhoan WHERE VaiTro=N'Admin' AND TrangThai=N'Hoạt động');
IF @MaHD IS NULL THROW 59001,N'Không có dữ liệu gói đã thanh toán để test hoàn tiền.',1;

BEGIN TRAN;
EXEC dbo.sp_HoanTien @MaHD=@MaHD,@SoTien=@SoTien,@PhuongThuc=N'Tiền mặt',@MaNV=@MaNV,@MaTT=@MaTT OUTPUT;
IF EXISTS (SELECT 1 FROM dbo.HoaDon WHERE MaHD=@MaHD AND TrangThai=N'Đã hoàn tiền')
   AND EXISTS (SELECT 1 FROM dbo.DangKyGoi WHERE MaDKG=@MaDKG AND TrangThai=N'Hủy')
    PRINT N'PASS BUG-011: hoàn tiền toàn bộ vô hiệu hóa gói.';
ELSE THROW 59002,N'FAIL BUG-011.',1;
ROLLBACK;
GO

DECLARE @MaTV INT=(SELECT MaTV FROM dbo.DangKyGoi WHERE MaDKG=1), @MaCheckIn BIGINT;
BEGIN TRY
    BEGIN TRAN;
    EXEC dbo.sp_CheckIn @MaTV=@MaTV,@MaPhong=6,@MaCheckIn=@MaCheckIn OUTPUT,@MaNV=1;
    ROLLBACK;
    THROW 59003,N'FAIL BUG-012: check-in được vào phòng bảo trì.',1;
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK;
    IF ERROR_NUMBER()=50038 PRINT N'PASS BUG-012: chặn check-in phòng không hoạt động.';
    ELSE THROW;
END CATCH;
GO

DECLARE @MaDKG2 INT=1, @MaNV2 INT=(SELECT TOP(1) MaNV FROM dbo.TaiKhoan WHERE VaiTro=N'Admin' AND TrangThai=N'Hoạt động'), @MaCheckIn2 BIGINT;
DECLARE @NgayQuaKhu DATE=DATEADD(DAY,-1,CAST(GETDATE() AS DATE)), @HomNay DATE=CAST(GETDATE() AS DATE);
BEGIN TRY
    BEGIN TRAN;
    EXEC dbo.sp_BaoLuuGoiTap @MaDKG=@MaDKG2,@TuNgay=@NgayQuaKhu,
         @DenNgay=@HomNay,@LyDo=N'Test ngày quá khứ',@MaNV=@MaNV2;
    ROLLBACK;
    THROW 59004,N'FAIL BUG-013: chấp nhận ngày bảo lưu quá khứ.',1;
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK;
    IF ERROR_NUMBER()=50040 PRINT N'PASS BUG-013: chặn ngày bảo lưu quá khứ.';
    ELSE THROW;
END CATCH;
GO

DECLARE @MaDKPT INT, @MaHDPT INT, @MaTVPT INT=1, @MaHLVPT INT=1, @MaNVPT INT=(SELECT TOP(1) MaNV FROM dbo.TaiKhoan WHERE VaiTro=N'Admin' AND TrangThai=N'Hoạt động');
DECLARE @NgayBatDauPT DATE=CAST(GETDATE() AS DATE), @NgayKetThucPT DATE=DATEADD(DAY,7,CAST(GETDATE() AS DATE));
BEGIN TRAN;
EXEC dbo.sp_DangKyPT @MaTV=@MaTVPT,@MaHLV=@MaHLVPT,@SoBuoi=1,@MaNV=@MaNVPT,
     @NgayBatDau=@NgayBatDauPT,@NgayKetThuc=@NgayKetThucPT,
     @MaDKPT=@MaDKPT OUTPUT,@MaHD=@MaHDPT OUTPUT;
IF EXISTS (SELECT 1 FROM dbo.DangKyPT WHERE MaDKPT=@MaDKPT AND TrangThai=N'Chờ thanh toán')
   AND EXISTS (SELECT 1 FROM dbo.HoaDon WHERE MaHD=@MaHDPT AND TrangThai=N'Chưa thanh toán')
    PRINT N'PASS BUG-014a: đăng ký PT chờ thanh toán.';
ELSE THROW 59005,N'FAIL BUG-014a.',1;
ROLLBACK;
GO

DECLARE @MaDKPT2 INT, @MaHDPT2 INT, @MaTTPT INT, @TongPT DECIMAL(18,2), @MaNVPT2 INT=(SELECT TOP(1) MaNV FROM dbo.TaiKhoan WHERE VaiTro=N'Admin' AND TrangThai=N'Hoạt động');
DECLARE @NgayBatDauPT2 DATE=CAST(GETDATE() AS DATE), @NgayKetThucPT2 DATE=DATEADD(DAY,7,CAST(GETDATE() AS DATE));
BEGIN TRAN;
EXEC dbo.sp_DangKyPT @MaTV=1,@MaHLV=1,@SoBuoi=1,@MaNV=@MaNVPT2,
     @NgayBatDau=@NgayBatDauPT2,@NgayKetThuc=@NgayKetThucPT2,
     @MaDKPT=@MaDKPT2 OUTPUT,@MaHD=@MaHDPT2 OUTPUT;
SELECT @TongPT=TongTien FROM dbo.HoaDon WHERE MaHD=@MaHDPT2;
EXEC dbo.sp_ThanhToanHoaDon @MaHD=@MaHDPT2,@SoTien=@TongPT,@PhuongThuc=N'Tiền mặt',@MaNV=@MaNVPT2,@MaTT=@MaTTPT OUTPUT;
IF EXISTS (SELECT 1 FROM dbo.DangKyPT WHERE MaDKPT=@MaDKPT2 AND TrangThai=N'Đang học')
   AND EXISTS (SELECT 1 FROM dbo.HoaDon WHERE MaHD=@MaHDPT2 AND TrangThai=N'Đã thanh toán')
    PRINT N'PASS BUG-014b: thanh toán đủ mới chuyển PT sang đang học.';
ELSE THROW 59006,N'FAIL BUG-014b.',1;
ROLLBACK;
GO

PRINT N'PHIEN_11_PASS';
