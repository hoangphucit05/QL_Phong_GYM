-- 21_test_phien_13.sql
USE QL_PhongGym;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRANSACTION;
BEGIN TRY
    -- 1. Test BUG-020: Gia hạn gói tập phải sinh hóa đơn và tính phí
    DECLARE @MaHDGiaHan INT;
    EXEC dbo.sp_GiaHanGoiTap @MaDKG = 1, @SoNgay = 10, @MaNV = 1, @MaHD = @MaHDGiaHan OUTPUT;
    IF @MaHDGiaHan IS NULL OR NOT EXISTS (SELECT 1 FROM dbo.HoaDon WHERE MaHD = @MaHDGiaHan AND TongTien > 0 AND TrangThai = N'Chưa thanh toán')
        THROW 59001, N'TEST FAIL: Gia hạn không sinh hóa đơn hợp lệ hoặc tiền = 0.', 1;
    PRINT 'PASS BUG-020: Gia hạn sinh hóa đơn có phí thành công.';

    -- 2. Test BUG-021: Check-in ưu tiên gói hết hạn sớm nhất (FIFO)
    -- Tạo gói B có thời hạn xa hơn gói 1
    DECLARE @MaDKG_Xa INT, @MaHD_Tam INT, @MaCheckIn BIGINT;
    -- Cô lập test với dữ liệu demo: nếu hội viên mặc định đang có phiên mở,
    -- chỉ đóng phiên đó trong transaction test; cuối script luôn rollback.
    UPDATE dbo.LichSuCheckIn
    SET ThoiGianRa = SYSDATETIME()
    WHERE MaTV = 1 AND ThoiGianRa IS NULL;
    EXEC dbo.sp_DangKyGoiTap @MaTV = 1, @MaGoi = 1, @MaKM = NULL, @NgayBatDau = '2026-11-20', @MaNV = 1, @MaDKG = @MaDKG_Xa OUTPUT, @MaHD = @MaHD_Tam OUTPUT;
    -- Kích hoạt cả hai gói
    UPDATE dbo.DangKyGoi SET TrangThai = N'Còn hạn', NgayKetThuc = '2026-11-25' WHERE MaDKG = 1;
    UPDATE dbo.DangKyGoi SET TrangThai = N'Còn hạn', NgayKetThuc = '2026-12-31' WHERE MaDKG = @MaDKG_Xa;
    UPDATE dbo.HoaDon SET TrangThai = N'Đã thanh toán' WHERE MaHD IN (1, @MaHD_Tam);
    -- Check-in
    EXEC dbo.sp_CheckIn @MaTV = 1, @MaPhong = 1, @Kenh = N'Quầy', @MaCheckIn = @MaCheckIn OUTPUT, @MaNV = 1;
    -- Kiểm tra xem lượt check-in thuộc MaDKG = 1 (hết hạn 25/11) chứ không phải MaDKG_Xa (hết hạn 31/12)
    IF NOT EXISTS (SELECT 1 FROM dbo.LichSuCheckIn WHERE MaCheckIn = @MaCheckIn AND MaDKG = 1)
        THROW 59002, N'TEST FAIL: Check-in không ưu tiên gói hết hạn sớm nhất.', 1;
    PRINT 'PASS BUG-021: Check-in FIFO ưu tiên gói hết hạn sớm nhất thành công.';

    -- 3. Test BUG-024: Lập phiếu bảo trì và hoàn tất bảo trì
    DECLARE @MaBT_Test INT;
    EXEC dbo.sp_BaoTriThietBi @MaTB = 1, @MaNV = 1, @NoiDung = N'Sửa thảm chạy', @ChiPhi = 500000, @NgayDuKien = NULL, @MaBaoTri = @MaBT_Test OUTPUT;
    IF NOT EXISTS (SELECT 1 FROM dbo.ThietBi WHERE MaTB = 1 AND TinhTrang = N'Bảo trì')
        THROW 59003, N'TEST FAIL: Lập phiếu bảo trì nhưng thiết bị không chuyển sang Bảo trì.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.BaoTriThietBi WHERE MaBaoTri = @MaBT_Test AND TrangThai = N'Đang bảo trì')
        THROW 59004, N'TEST FAIL: Phiếu bảo trì không ở trạng thái Đang bảo trì.', 1;

    -- Hoàn tất bảo trì
    EXEC dbo.sp_HoanTatBaoTri @MaBaoTri = @MaBT_Test, @MaNV = 1, @GhiChu = N'Đã thay thảm mới';
    IF NOT EXISTS (SELECT 1 FROM dbo.ThietBi WHERE MaTB = 1 AND TinhTrang = N'Hoạt động')
        THROW 59005, N'TEST FAIL: Hoàn tất bảo trì nhưng thiết bị không khôi phục về Hoạt động.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.BaoTriThietBi WHERE MaBaoTri = @MaBT_Test AND TrangThai = N'Hoàn thành')
        THROW 59006, N'TEST FAIL: Phiếu bảo trì không cập nhật Hoàn thành.', 1;
    PRINT 'PASS BUG-024: Lập phiếu bảo trì và hoàn tất bảo trì khôi phục thiết bị thành công.';

    ROLLBACK TRANSACTION;
    PRINT 'PHIEN_13_TEST_PASS_ROLLBACK_CLEAN';
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    PRINT 'TEST FAIL: ' + ERROR_MESSAGE();
    THROW;
END CATCH;
GO
