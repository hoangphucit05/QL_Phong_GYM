SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET NOCOUNT ON;

BEGIN TRANSACTION;

PRINT N'====================================================================';
PRINT N'   BẮT ĐẦU KIỂM THỬ TÍCH HỢP PHIÊN 15: CRUD SẢN PHẨM, NHẬP KHO, THIẾT BỊ, PHÒNG TẬP, KHUYẾN MÃI';
PRINT N'====================================================================';

-- 1. Kiểm thử CRUD Sản phẩm & Trigger tự sinh mã
PRINT N'--> Bước 1: Thêm sản phẩm mới và kiểm tra trigger tự sinh mã';
INSERT INTO dbo.SanPham (TenSP, DonGia, SoLuongTon, TrangThai, GiaNhap, TonToiThieu, MaNCC)
VALUES (N'Thanh Năng Lượng Test Phiên 15', 35000, 0, N'Còn bán', 20000, 5, 1);

DECLARE @MaSP_Moi INT = SCOPE_IDENTITY();
DECLARE @MaSPCode VARCHAR(20), @DonGiaHienTai DECIMAL(18,2);

SELECT @MaSPCode = MaSanPham, @DonGiaHienTai = DonGia FROM dbo.SanPham WHERE MaSP = @MaSP_Moi;

IF @MaSPCode IS NULL OR @MaSPCode NOT LIKE 'SP%'
    THROW 50010, N'Trigger TR_SanPham_MaSanPham không tự sinh mã hợp lệ.', 1;

PRINT N'    [PASS] Tạo sản phẩm mới MaSP = ' + CAST(@MaSP_Moi AS NVARCHAR(10)) + N', Mã SP = ' + @MaSPCode;

-- Cập nhật sản phẩm
UPDATE dbo.SanPham
SET DonGia = 40000, TrangThai = N'Còn bán'
WHERE MaSP = @MaSP_Moi;

SELECT @DonGiaHienTai = DonGia FROM dbo.SanPham WHERE MaSP = @MaSP_Moi;
IF @DonGiaHienTai <> 40000
    THROW 50011, N'Cập nhật đơn giá sản phẩm thất bại.', 1;

PRINT N'    [PASS] Cập nhật đơn giá sản phẩm lên 40,000 đ thành công.';

-- 2. Kiểm thử Nhập kho qua sp_NhapKho
PRINT N'--> Bước 2: Kiểm thử nhập kho hàng hóa qua sp_NhapKho';
DECLARE @MaPNK INT;
EXEC dbo.sp_NhapKho
    @MaNCC = 1,
    @MaNV = 1,
    @MaSP = @MaSP_Moi,
    @SoLuong = 50,
    @DonGiaNhap = 22000,
    @MaPNK = @MaPNK OUTPUT;

IF @MaPNK IS NULL
    THROW 50012, N'sp_NhapKho không trả về MaPNK.', 1;

DECLARE @TonKhoSauNhap INT, @LichSuTonCount INT;
SELECT @TonKhoSauNhap = SoLuongTon FROM dbo.SanPham WHERE MaSP = @MaSP_Moi;
SELECT @LichSuTonCount = COUNT(*) FROM dbo.LichSuTonKho WHERE MaSP = @MaSP_Moi AND NguonThamChieu = N'Phiếu nhập';

IF @TonKhoSauNhap <> 50
    THROW 50013, N'Tồn kho sản phẩm không tăng lên 50 sau khi nhập kho.', 1;

IF @LichSuTonCount = 0
    THROW 50014, N'Lịch sử tồn kho không được ghi nhận sau khi nhập kho.', 1;

PRINT N'    [PASS] Nhập kho thành công 50 sản phẩm. Phiếu nhập #' + CAST(@MaPNK AS NVARCHAR(10)) + N', Tồn kho hiện tại: ' + CAST(@TonKhoSauNhap AS NVARCHAR(10));

-- 3. Kiểm thử Cập nhật Thiết bị & Phòng tập
PRINT N'--> Bước 3: Cập nhật thiết bị và phòng tập';
DECLARE @MaTB INT;
SELECT TOP 1 @MaTB = MaTB FROM dbo.ThietBi;

IF @MaTB IS NOT NULL
BEGIN
    UPDATE dbo.ThietBi
    SET TinhTrang = N'Bảo trì'
    WHERE MaTB = @MaTB;

    DECLARE @TinhTrangTB NVARCHAR(30);
    SELECT @TinhTrangTB = TinhTrang FROM dbo.ThietBi WHERE MaTB = @MaTB;
    IF @TinhTrangTB <> N'Bảo trì'
        THROW 50015, N'Cập nhật tình trạng thiết bị thất bại.', 1;

    PRINT N'    [PASS] Cập nhật thiết bị MaTB = ' + CAST(@MaTB AS NVARCHAR(10)) + N' thành công.';
END

DECLARE @MaPhong INT;
SELECT TOP 1 @MaPhong = MaPhong FROM dbo.PhongTap;

IF @MaPhong IS NOT NULL
BEGIN
    UPDATE dbo.PhongTap
    SET SucChua = 45, TrangThai = N'Hoạt động'
    WHERE MaPhong = @MaPhong;

    DECLARE @SucChuaPhong INT;
    SELECT @SucChuaPhong = SucChua FROM dbo.PhongTap WHERE MaPhong = @MaPhong;
    IF @SucChuaPhong <> 45
        THROW 50016, N'Cập nhật phòng tập thất bại.', 1;

    PRINT N'    [PASS] Cập nhật phòng tập MaPhong = ' + CAST(@MaPhong AS NVARCHAR(10)) + N' thành công.';
END

-- 4. Kiểm thử CRUD Khuyến mãi
PRINT N'--> Bước 4: Kiểm thử thêm và cập nhật chương trình khuyến mãi';
INSERT INTO dbo.KhuyenMai (TenKM, PhanTramGiam, NgayBatDau, NgayKetThuc, TrangThai, MaCode)
VALUES (N'Siêu Sale Hè 2026 Test', 25, CAST(GETDATE() AS DATE), CAST(DATEADD(MONTH, 1, GETDATE()) AS DATE), N'Hoạt động', 'SUMMER25TEST');

DECLARE @MaKM_Moi INT = SCOPE_IDENTITY();
IF @MaKM_Moi IS NULL
    THROW 50017, N'Thêm khuyến mãi thất bại.', 1;

UPDATE dbo.KhuyenMai
SET PhanTramGiam = 30
WHERE MaKM = @MaKM_Moi;

DECLARE @GiamMoi DECIMAL(18,2);
SELECT @GiamMoi = PhanTramGiam FROM dbo.KhuyenMai WHERE MaKM = @MaKM_Moi;
IF @GiamMoi <> 30
    THROW 50018, N'Cập nhật khuyến mãi thất bại.', 1;

PRINT N'    [PASS] Thêm và cập nhật khuyến mãi MaKM = ' + CAST(@MaKM_Moi AS NVARCHAR(10)) + N' (Mức giảm: 30%) thành công.';

PRINT N'====================================================================';
PRINT N'   TẤT CẢ TEST PHIÊN 15 THÀNH CÔNG VƯỢT TRỘI!';
PRINT N'====================================================================';

IF @@TRANCOUNT > 0
    ROLLBACK TRANSACTION;
PRINT N'--> Đã ROLLBACK dữ liệu thử nghiệm an toàn.';
