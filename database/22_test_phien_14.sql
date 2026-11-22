SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET NOCOUNT ON;

BEGIN TRANSACTION;

PRINT N'====================================================================';
PRINT N'   BẮT ĐẦU KIỂM THỬ TÍCH HỢP PHIÊN 14: KHÁCH VÃNG LAI, LỚP TẬP, HỦY HÓA ĐƠN';
PRINT N'====================================================================';

-- 1. Kiểm tra bản ghi Khách vãng lai
PRINT N'--> Bước 1: Kiểm tra bản ghi hệ thống Khách vãng lai';
DECLARE @MaTV_VL INT;
SELECT @MaTV_VL = MaTV FROM dbo.ThanhVien WHERE MaThanhVien = 'TV001035';

IF @MaTV_VL IS NULL
    THROW 50001, N'Không tìm thấy hội viên TV001035 (Khách vãng lai).', 1;

PRINT N'    [PASS] Tìm thấy Khách vãng lai MaTV = ' + CAST(@MaTV_VL AS NVARCHAR(10));

-- 2. Kiểm tra bán sản phẩm cho khách vãng lai và trừ tồn kho
PRINT N'--> Bước 2: Bán sản phẩm cho Khách vãng lai qua sp_BanSanPham';
DECLARE @MaSP INT, @TonKhoTruoc INT;
SELECT TOP 1 @MaSP = MaSP, @TonKhoTruoc = SoLuongTon
FROM dbo.SanPham
WHERE TrangThai = N'Còn bán' AND SoLuongTon >= 5;

IF @MaSP IS NULL
    THROW 50002, N'Không có sản phẩm nào đủ tồn kho để test.', 1;

DECLARE @MaHD INT;
EXEC dbo.sp_BanSanPham
    @MaTV = @MaTV_VL,
    @MaNV = 1,
    @MaSP = @MaSP,
    @SoLuong = 2,
    @MaHD = @MaHD OUTPUT;

IF @MaHD IS NULL
    THROW 50003, N'sp_BanSanPham không trả về MaHD.', 1;

DECLARE @TonKhoSauBan INT;
SELECT @TonKhoSauBan = SoLuongTon FROM dbo.SanPham WHERE MaSP = @MaSP;

IF @TonKhoSauBan <> @TonKhoTruoc - 2
    THROW 50004, N'Tồn kho sản phẩm không trừ đúng sau khi bán.', 1;

PRINT N'    [PASS] Tạo HĐ bán lẻ #' + CAST(@MaHD AS NVARCHAR(10)) + N' cho Khách vãng lai thành công. Tồn kho giảm từ ' 
      + CAST(@TonKhoTruoc AS NVARCHAR(10)) + N' -> ' + CAST(@TonKhoSauBan AS NVARCHAR(10));

-- 3. Kiểm tra hủy hóa đơn sp_HuyHoaDon và kiểm tra tự động hoàn kho
PRINT N'--> Bước 3: Hủy hóa đơn sp_HuyHoaDon và kiểm tra tự động hoàn kho';
EXEC dbo.sp_HuyHoaDon
    @MaHD = @MaHD,
    @LyDo = N'Khách vãng lai hủy đơn tại quầy test phiên 14',
    @MaNV = 1;

DECLARE @TrangThaiHD NVARCHAR(30), @TonKhoSauHuy INT;
SELECT @TrangThaiHD = TrangThai FROM dbo.HoaDon WHERE MaHD = @MaHD;
SELECT @TonKhoSauHuy = SoLuongTon FROM dbo.SanPham WHERE MaSP = @MaSP;

IF @TrangThaiHD <> N'Hủy'
    THROW 50005, N'Hóa đơn chưa chuyển trạng thái Hủy.', 1;

IF @TonKhoSauHuy <> @TonKhoTruoc
    THROW 50006, N'Tồn kho không được hoàn trả nguyên vẹn sau khi hủy đơn.', 1;

PRINT N'    [PASS] Hủy HĐ #' + CAST(@MaHD AS NVARCHAR(10)) + N' thành công. Tồn kho đã được hoàn trả về ' + CAST(@TonKhoSauHuy AS NVARCHAR(10));

-- 4. Kiểm tra cập nhật và đóng lớp tập
PRINT N'--> Bước 4: Kiểm tra cập nhật lớp tập và đóng lớp tập';
DECLARE @MaLop INT;
SELECT TOP 1 @MaLop = MaLop FROM dbo.LopTap WHERE TrangThai = N'Đang mở';

IF @MaLop IS NOT NULL
BEGIN
    UPDATE dbo.LopTap
    SET TenLop = N'Lớp Yoga Test Phiên 14',
        SucChua = 25
    WHERE MaLop = @MaLop;

    DECLARE @TenLopMoi NVARCHAR(100), @SucChuaMoi INT;
    SELECT @TenLopMoi = TenLop, @SucChuaMoi = SucChua FROM dbo.LopTap WHERE MaLop = @MaLop;

    IF @TenLopMoi <> N'Lớp Yoga Test Phiên 14' OR @SucChuaMoi <> 25
        THROW 50007, N'Cập nhật lớp tập thất bại.', 1;

    PRINT N'    [PASS] Cập nhật lớp tập MaLop = ' + CAST(@MaLop AS NVARCHAR(10)) + N' thành công.';

    UPDATE dbo.LopTap SET TrangThai = N'Đã đóng' WHERE MaLop = @MaLop;
    DECLARE @TrangThaiLop NVARCHAR(30);
    SELECT @TrangThaiLop = TrangThai FROM dbo.LopTap WHERE MaLop = @MaLop;

    IF @TrangThaiLop <> N'Đã đóng'
        THROW 50008, N'Đóng lớp tập thất bại.', 1;

    PRINT N'    [PASS] Đóng lớp tập MaLop = ' + CAST(@MaLop AS NVARCHAR(10)) + N' thành công (Trạng thái: Đã đóng).';
END
ELSE
BEGIN
    PRINT N'    [INFO] Không có lớp tập trạng thái Đang mở để test.';
END

-- 5. Kiểm tra chặn hủy hóa đơn đã thanh toán
PRINT N'--> Bước 5: Kiểm tra chặn hủy hóa đơn đã thanh toán';
DECLARE @MaHD_DaTT INT;
SELECT TOP 1 @MaHD_DaTT = h.MaHD
FROM dbo.HoaDon h
JOIN dbo.ThanhToan t ON t.MaHD = h.MaHD
WHERE t.TrangThai = N'Thành công' AND t.LoaiGiaoDich = N'Thanh toán' AND h.TrangThai = N'Đã thanh toán';

IF @MaHD_DaTT IS NOT NULL
BEGIN
    DECLARE @BiChan BIT = 0;
    BEGIN TRY
        EXEC dbo.sp_HuyHoaDon @MaHD = @MaHD_DaTT, @LyDo = N'Cố tình hủy đơn đã thanh toán', @MaNV = 1;
    END TRY
    BEGIN CATCH
        IF ERROR_NUMBER() = 50034
            SET @BiChan = 1;
        ELSE
            PRINT N'    Lỗi khác: ' + ERROR_MESSAGE();
    END CATCH

    IF @BiChan = 1
        PRINT N'    [PASS] Đã chặn đúng nghiệp vụ khi cố tình hủy HĐ đã thanh toán #' + CAST(@MaHD_DaTT AS NVARCHAR(10)) + N' (Mã lỗi 50034).';
    ELSE
        THROW 50009, N'Hệ thống không chặn việc hủy hóa đơn đã thanh toán.', 1;
END

PRINT N'====================================================================';
PRINT N'   TẤT CẢ TEST PHIÊN 14 THÀNH CÔNG VƯỢT TRỘI!';
PRINT N'====================================================================';

IF @@TRANCOUNT > 0
    ROLLBACK TRANSACTION;
PRINT N'--> Đã ROLLBACK dữ liệu thử nghiệm an toàn.';
