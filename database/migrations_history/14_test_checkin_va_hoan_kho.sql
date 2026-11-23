/* Kiểm thử hồi quy Phiên 6. Mọi thay đổi đều rollback. */
USE QL_PhongGym;
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.DangKyGoi WHERE MaDKG = 1 AND MaTV = 1 AND TrangThai = N'Còn hạn')
    THROW 51000, N'Không có dữ liệu mẫu DKG=1 để chạy kiểm thử check-in.', 1;

DECLARE @MaCheckIn BIGINT, @SoLuotSau INT, @MaHD INT;
DECLARE @TonTruoc INT, @TonSauKhiGiu INT, @TonSauKhiHuy INT, @SoDongHoan INT;
DECLARE @Loi INT;
DECLARE @BatDau TIME(0), @KetThuc TIME(0);

/* 1. Check-in đúng điều kiện tăng lượt đúng một lần, rồi rollback. */
BEGIN TRAN;
UPDATE dbo.GoiTap SET GioiHanLuot = 1, GioBatDau = '00:00:00', GioKetThuc = '23:59:59' WHERE MaGoi = 1;
UPDATE dbo.DangKyGoi SET SoLuotDaDung = 0 WHERE MaDKG = 1;
EXEC dbo.sp_CheckIn @MaTV = 1, @MaPhong = NULL, @Kenh = N'Quầy', @MaCheckIn = @MaCheckIn OUTPUT, @MaNV = 1;
SELECT @SoLuotSau = SoLuotDaDung FROM dbo.DangKyGoi WHERE MaDKG = 1;
IF @MaCheckIn IS NULL OR @SoLuotSau <> 1
    THROW 51001, N'Check-in không tăng SoLuotDaDung đúng một lần.', 1;
EXEC dbo.sp_CheckOut @MaTV = 1, @MaNV = 1;
ROLLBACK;
PRINT N'PASS CHECKIN_INCREMENT';

/* 2. Vượt giới hạn lượt phải bị chặn. Procedure tự rollback transaction lỗi. */
SET @Loi = NULL;
BEGIN TRAN;
UPDATE dbo.GoiTap SET GioiHanLuot = 1, GioBatDau = '00:00:00', GioKetThuc = '23:59:59' WHERE MaGoi = 1;
UPDATE dbo.DangKyGoi SET SoLuotDaDung = 1 WHERE MaDKG = 1;
BEGIN TRY
    EXEC dbo.sp_CheckIn @MaTV = 1, @MaPhong = NULL, @Kenh = N'Quầy', @MaCheckIn = @MaCheckIn OUTPUT, @MaNV = 1;
END TRY
BEGIN CATCH
    SET @Loi = ERROR_NUMBER();
END CATCH;
IF @@TRANCOUNT > 0 ROLLBACK;
IF @Loi <> 50011
    THROW 51002, N'Check-in vượt lượt không bị chặn đúng mã lỗi.', 1;
PRINT N'PASS CHECKIN_LIMIT';

/* 3. Ngoài khung giờ phải bị chặn, kể cả gói còn lượt. */
SET @Loi = NULL;
SET @BatDau = CONVERT(TIME(0), DATEADD(MINUTE, 1, SYSDATETIME()));
SET @KetThuc = CONVERT(TIME(0), DATEADD(MINUTE, 2, SYSDATETIME()));
BEGIN TRAN;
UPDATE dbo.GoiTap SET GioiHanLuot = NULL, GioBatDau = @BatDau, GioKetThuc = @KetThuc WHERE MaGoi = 1;
UPDATE dbo.DangKyGoi SET SoLuotDaDung = 0 WHERE MaDKG = 1;
BEGIN TRY
    EXEC dbo.sp_CheckIn @MaTV = 1, @MaPhong = NULL, @Kenh = N'Quầy', @MaCheckIn = @MaCheckIn OUTPUT, @MaNV = 1;
END TRY
BEGIN CATCH
    SET @Loi = ERROR_NUMBER();
END CATCH;
IF @@TRANCOUNT > 0 ROLLBACK;
IF @Loi <> 50011
    THROW 51003, N'Check-in ngoài khung giờ không bị chặn đúng mã lỗi.', 1;
PRINT N'PASS CHECKIN_TIME_WINDOW';

/* 4. Hủy hóa đơn sản phẩm phải hoàn lại tồn và ghi lịch sử Hoàn trả. */
BEGIN TRAN;
SELECT @TonTruoc = SoLuongTon FROM dbo.SanPham WHERE MaSP = 15;
EXEC dbo.sp_BanSanPham @MaTV = 1, @MaNV = 1, @MaSP = 15, @SoLuong = 1, @MaHD = @MaHD OUTPUT;
SELECT @TonSauKhiGiu = SoLuongTon FROM dbo.SanPham WHERE MaSP = 15;
EXEC dbo.sp_HuyHoaDon @MaHD = @MaHD, @LyDo = N'Kiểm thử hoàn kho Phiên 6', @MaNV = 1;
SELECT @TonSauKhiHuy = SoLuongTon FROM dbo.SanPham WHERE MaSP = 15;
SELECT @SoDongHoan = COUNT(*) FROM dbo.LichSuTonKho
WHERE MaSP = 15 AND MaThamChieu = @MaHD AND LoaiBienDong = N'Hoàn trả';
IF @TonSauKhiGiu <> @TonTruoc - 1 OR @TonSauKhiHuy <> @TonTruoc OR @SoDongHoan <> 1
    THROW 51004, N'Hủy hóa đơn không hoàn kho hoặc ghi lịch sử đúng.', 1;
ROLLBACK;
PRINT N'PASS CANCEL_RESTORE_STOCK';

PRINT N'PASS SESSION_6_CHECKIN_INVENTORY';
