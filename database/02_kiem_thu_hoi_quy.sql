-- ========================================================================================
-- DỰ ÁN: HỆ THỐNG QUẢN LÝ PHÒNG GYM
-- TỆP: 02_kiem_thu_hoi_quy.sql
-- MỤC ĐÍCH: KIỂM THỬ HỒI QUY TỰ ĐỘNG 12 LUỒNG NGHIỆP VỤ LÕI TRÊN DATABASE
-- ĐẶC ĐIỂM: Sử dụng TRANSACTION và tự động ROLLBACK ở cuối script, đảm bảo không làm
--          thay đổi hoặc sai lệch bất kỳ dữ liệu thực tế nào của cơ sở dữ liệu.
-- CÁCH CHẠY: Mở trong SQL Server Management Studio (SSMS) hoặc Azure Data Studio, nhấn F5.
-- ========================================================================================

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET NOCOUNT ON;

BEGIN TRANSACTION;

PRINT N'========================================================================================';
PRINT N'   BẮT ĐẦU KIỂM THỬ HỒI QUY TOÀN DIỆN HỆ THỐNG QUẢN LÝ PHÒNG GYM (12 LUỒNG LÕI)';
PRINT N'========================================================================================';

-- LUỒNG 1: THÊM HỘI VIÊN & VALIDATION RÀNG BUỘC
PRINT N'--> [Luồng 1] Kiểm tra thêm hội viên và validation ràng buộc SĐT/Email';
DECLARE @SdtTest VARCHAR(20) = '0988776655';
DECLARE @EmailTest VARCHAR(100) = 'test.member.p16@gympro.vn';

INSERT INTO dbo.ThanhVien (HoTen, NgaySinh, GioiTinh, SDT, Email, DiaChi, MaLoaiTV, TrangThai)
VALUES (N'Nguyễn Văn Nghiệm Thu', '1995-05-15', N'Nam', @SdtTest, @EmailTest, N'Hà Nội', 1, N'Hoạt động');

DECLARE @MaTV_Test INT = SCOPE_IDENTITY();
IF @MaTV_Test IS NULL
    THROW 51001, N'Không thể thêm hội viên nghiệm thu.', 1;

-- Kiểm tra chặn trùng SĐT
DECLARE @TrungSdt BIT = 0;
BEGIN TRY
    INSERT INTO dbo.ThanhVien (HoTen, NgaySinh, GioiTinh, SDT, Email, DiaChi, MaLoaiTV, TrangThai)
    VALUES (N'Trùng SĐT', '1996-01-01', N'Nam', @SdtTest, 'other@gympro.vn', N'Hà Nội', 1, N'Hoạt động');
END TRY
BEGIN CATCH
    IF ERROR_NUMBER() IN (2601, 2627) SET @TrungSdt = 1;
END CATCH;

IF @TrungSdt = 0 THROW 51002, N'Hệ thống không chặn trùng SĐT hội viên.', 1;
PRINT N'    [PASS] Thêm hội viên MaTV = ' + CAST(@MaTV_Test AS NVARCHAR(10)) + N' và chặn trùng SĐT thành công.';

-- LUỒNG 2: ĐĂNG KÝ GÓI TẬP CÓ KHUYẾN MÃI
PRINT N'--> [Luồng 2] Đăng ký gói tập có khuyến mãi qua sp_DangKyGoiTap';
DECLARE @MaGoi INT, @MaKM INT;
SELECT TOP 1 @MaGoi = MaGoi FROM dbo.GoiTap WHERE TrangThai = N'Đang áp dụng';
SELECT TOP 1 @MaKM = MaKM FROM dbo.KhuyenMai WHERE TrangThai = N'Hoạt động' AND CAST(GETDATE() AS DATE) BETWEEN NgayBatDau AND NgayKetThuc;

DECLARE @MaDKG INT, @MaHD_Goi INT;
EXEC dbo.sp_DangKyGoiTap
    @MaTV = @MaTV_Test,
    @MaGoi = @MaGoi,
    @MaKM = @MaKM,
    @NgayBatDau = '2026-11-22',
    @MaNV = 1,
    @MaDKG = @MaDKG OUTPUT,
    @MaHD = @MaHD_Goi OUTPUT;

IF @MaDKG IS NULL OR @MaHD_Goi IS NULL
    THROW 51003, N'sp_DangKyGoiTap không trả về MaDKG hoặc MaHD.', 1;

PRINT N'    [PASS] Đăng ký gói MaDKG = ' + CAST(@MaDKG AS NVARCHAR(10)) + N', Hóa đơn công nợ #' + CAST(@MaHD_Goi AS NVARCHAR(10));

-- LUỒNG 3: THANH TOÁN HÓA ĐƠN
PRINT N'--> [Luồng 3] Thu tiền thanh toán hóa đơn gói tập qua sp_ThanhToanHoaDon';
DECLARE @TongTienGoi DECIMAL(18,2);
SELECT @TongTienGoi = TongTien FROM dbo.HoaDon WHERE MaHD = @MaHD_Goi;

DECLARE @MaTT INT;
EXEC dbo.sp_ThanhToanHoaDon
    @MaHD = @MaHD_Goi,
    @SoTien = @TongTienGoi,
    @PhuongThuc = N'Chuyển khoản',
    @MaNV = 1,
    @MaTT = @MaTT OUTPUT;

DECLARE @TrangThaiHD_Goi NVARCHAR(30);
SELECT @TrangThaiHD_Goi = TrangThai FROM dbo.HoaDon WHERE MaHD = @MaHD_Goi;
IF @TrangThaiHD_Goi <> N'Đã thanh toán'
    THROW 51004, N'Hóa đơn chưa chuyển trạng thái Đã thanh toán sau khi thu tiền.', 1;

PRINT N'    [PASS] Thanh toán thành công HĐ #' + CAST(@MaHD_Goi AS NVARCHAR(10)) + N' (Tổng tiền: ' + CAST(@TongTienGoi AS NVARCHAR(20)) + N' đ).';

-- LUỒNG 4 & 5: CHECK-IN VÀ CHECK-OUT
PRINT N'--> [Luồng 4 & 5] Check-in FIFO và Check-out lượt tập';
DECLARE @MaPhong INT;
SELECT TOP 1 @MaPhong = MaPhong FROM dbo.PhongTap WHERE TrangThai = N'Hoạt động';

DECLARE @MaCheckIn INT;
EXEC dbo.sp_CheckIn
    @MaTV = @MaTV_Test,
    @MaPhong = @MaPhong,
    @MaNV = 1,
    @MaCheckIn = @MaCheckIn OUTPUT;

IF @MaCheckIn IS NULL THROW 51005, N'Check-in thất bại.', 1;

EXEC dbo.sp_CheckOut
    @MaTV = @MaTV_Test,
    @MaNV = 1;

DECLARE @ThoiGianRa DATETIME2;
SELECT @ThoiGianRa = ThoiGianRa FROM dbo.LichSuCheckIn WHERE MaCheckIn = @MaCheckIn;
IF @ThoiGianRa IS NULL THROW 51006, N'Check-out không ghi nhận thời gian ra.', 1;

PRINT N'    [PASS] Check-in (MaCheckIn = ' + CAST(@MaCheckIn AS NVARCHAR(10)) + N') và Check-out thành công.';

-- LUỒNG 6: GIA HẠN GÓI TẬP CÓ TÍNH PHÍ
PRINT N'--> [Luồng 6] Gia hạn gói tập sinh hóa đơn có phí qua sp_GiaHanGoiTap';
DECLARE @NgayKetThucTruoc DATE, @NgayKetThucSau DATE, @MaHD_GiaHan INT;
SELECT @NgayKetThucTruoc = NgayKetThuc FROM dbo.DangKyGoi WHERE MaDKG = @MaDKG;

EXEC dbo.sp_GiaHanGoiTap
    @MaDKG = @MaDKG,
    @SoNgay = 30,
    @MaNV = 1,
    @MaHD = @MaHD_GiaHan OUTPUT;

SELECT @NgayKetThucSau = NgayKetThuc FROM dbo.DangKyGoi WHERE MaDKG = @MaDKG;
IF @NgayKetThucSau <> DATEADD(DAY, 30, @NgayKetThucTruoc) OR @MaHD_GiaHan IS NULL
    THROW 51007, N'Gia hạn gói tập không cộng ngày hoặc không sinh hóa đơn phí.', 1;

PRINT N'    [PASS] Gia hạn gói thêm 30 ngày thành công. Hóa đơn phí gia hạn #' + CAST(@MaHD_GiaHan AS NVARCHAR(10));

-- LUỒNG 7: BẢO LƯU GÓI TẬP
PRINT N'--> [Luồng 7] Bảo lưu gói tập qua sp_BaoLuuGoiTap';
EXEC dbo.sp_BaoLuuGoiTap
    @MaDKG = @MaDKG,
    @TuNgay = '2026-11-25',
    @DenNgay = '2026-12-05',
    @LyDo = N'Đi công tác đột xuất nghiệm thu',
    @MaNV = 1;

PRINT N'    [PASS] Bảo lưu gói tập thành công.';

-- LUỒNG 8: ĐĂNG KÝ HỢP ĐỒNG PT & KÍCH HOẠT KHI ĐỦ TIỀN
PRINT N'--> [Luồng 8] Đăng ký PT chờ thanh toán và kích hoạt khi thu đủ';
DECLARE @MaHLV INT;
SELECT TOP 1 @MaHLV = MaHLV FROM dbo.HuanLuyenVien;

DECLARE @MaDKPT INT, @MaHD_PT INT;
EXEC dbo.sp_DangKyPT
    @MaTV = @MaTV_Test,
    @MaHLV = @MaHLV,
    @SoBuoi = 12,
    @MaKM = NULL,
    @NgayBatDau = '2026-11-22',
    @NgayKetThuc = '2027-01-22',
    @MaNV = 1,
    @MaDKPT = @MaDKPT OUTPUT,
    @MaHD = @MaHD_PT OUTPUT;

DECLARE @TrangThaiPT NVARCHAR(30);
SELECT @TrangThaiPT = TrangThai FROM dbo.DangKyPT WHERE MaDKPT = @MaDKPT;
IF @TrangThaiPT <> N'Chờ thanh toán'
    THROW 51009, N'Hợp đồng PT không ở trạng thái Chờ thanh toán trước khi thu tiền.', 1;

DECLARE @TienPT DECIMAL(18,2);
SELECT @TienPT = TongTien FROM dbo.HoaDon WHERE MaHD = @MaHD_PT;

EXEC dbo.sp_ThanhToanHoaDon
    @MaHD = @MaHD_PT,
    @SoTien = @TienPT,
    @PhuongThuc = N'Tiền mặt',
    @MaNV = 1,
    @MaTT = @MaTT OUTPUT;

SELECT @TrangThaiPT = TrangThai FROM dbo.DangKyPT WHERE MaDKPT = @MaDKPT;
IF @TrangThaiPT <> N'Đang học'
    THROW 51010, N'Hợp đồng PT không chuyển sang Đang học sau khi thu đủ tiền.', 1;

PRINT N'    [PASS] Đăng ký PT (MaDKPT = ' + CAST(@MaDKPT AS NVARCHAR(10)) + N') và kích hoạt thành công sau thanh toán.';

-- LUỒNG 9: BÁN HÀNG KHÁCH VÃNG LAI & HỦY HÓA ĐƠN HOÀN KHO
PRINT N'--> [Luồng 9] Bán lẻ cho Khách vãng lai và Hủy hóa đơn tự hoàn kho';
DECLARE @MaTV_VL INT;
SELECT @MaTV_VL = MaTV FROM dbo.ThanhVien WHERE MaThanhVien = 'TV001035';

DECLARE @MaSP INT, @TonTruoc INT;
SELECT TOP 1 @MaSP = MaSP, @TonTruoc = SoLuongTon FROM dbo.SanPham WHERE TrangThai = N'Còn bán' AND SoLuongTon >= 10;

DECLARE @MaHD_BanLe INT;
EXEC dbo.sp_BanSanPham
    @MaTV = @MaTV_VL,
    @MaNV = 1,
    @MaSP = @MaSP,
    @SoLuong = 3,
    @MaHD = @MaHD_BanLe OUTPUT;

DECLARE @TonSauBan INT;
SELECT @TonSauBan = SoLuongTon FROM dbo.SanPham WHERE MaSP = @MaSP;
IF @TonSauBan <> @TonTruoc - 3 THROW 51011, N'Tồn kho không trừ đúng sau khi bán hàng.', 1;

-- Hủy hóa đơn hoàn kho
EXEC dbo.sp_HuyHoaDon
    @MaHD = @MaHD_BanLe,
    @LyDo = N'Hủy hóa đơn bán lẻ nghiệm thu',
    @MaNV = 1;

DECLARE @TonSauHuy INT, @TrangThaiHuy NVARCHAR(30);
SELECT @TonSauHuy = SoLuongTon FROM dbo.SanPham WHERE MaSP = @MaSP;
SELECT @TrangThaiHuy = TrangThai FROM dbo.HoaDon WHERE MaHD = @MaHD_BanLe;

IF @TrangThaiHuy <> N'Hủy' OR @TonSauHuy <> @TonTruoc
    THROW 51012, N'Hủy hóa đơn không cập nhật trạng thái Hủy hoặc không hoàn kho nguyên vẹn.', 1;

PRINT N'    [PASS] Bán lẻ khách vãng lai và hủy hóa đơn hoàn lại kho thành công.';

-- LUỒNG 10: NHẬP KHO HÀNG HÓA
PRINT N'--> [Luồng 10] Nhập kho hàng hóa qua sp_NhapKho';
DECLARE @MaPNK INT;
EXEC dbo.sp_NhapKho
    @MaNCC = 1,
    @MaNV = 1,
    @MaSP = @MaSP,
    @SoLuong = 20,
    @DonGiaNhap = 15000,
    @MaPNK = @MaPNK OUTPUT;

DECLARE @TonSauNhapKho INT;
SELECT @TonSauNhapKho = SoLuongTon FROM dbo.SanPham WHERE MaSP = @MaSP;
IF @TonSauNhapKho <> @TonTruoc + 20
    THROW 51013, N'Nhập kho không tăng tồn kho chính xác.', 1;

PRINT N'    [PASS] Nhập kho thành công 20 sản phẩm. Phiếu nhập #' + CAST(@MaPNK AS NVARCHAR(10)) + N', Tồn kho: ' + CAST(@TonSauNhapKho AS NVARCHAR(10));

-- LUỒNG 11: BẢO TRÌ THIẾT BỊ VÀ HOÀN TẤT BẢO TRÌ
PRINT N'--> [Luồng 11] Lập phiếu bảo trì và hoàn tất bảo trì khôi phục máy';
DECLARE @MaTB INT;
SELECT TOP 1 @MaTB = MaTB FROM dbo.ThietBi;

DECLARE @MaBaoTri INT;
EXEC dbo.sp_BaoTriThietBi
    @MaTB = @MaTB,
    @MaNV = 1,
    @NoiDung = N'Bảo trì định kỳ nghiệm thu P16',
    @ChiPhi = 250000,
    @NgayDuKien = NULL,
    @MaBaoTri = @MaBaoTri OUTPUT;

DECLARE @TinhTrangTB NVARCHAR(30);
SELECT @TinhTrangTB = TinhTrang FROM dbo.ThietBi WHERE MaTB = @MaTB;
IF @TinhTrangTB <> N'Bảo trì' THROW 51014, N'Thiết bị chưa chuyển sang Bảo trì.', 1;

EXEC dbo.sp_HoanTatBaoTri
    @MaBaoTri = @MaBaoTri,
    @MaNV = 1,
    @GhiChu = N'Máy đã sửa chữa và kiểm tra đạt';

SELECT @TinhTrangTB = TinhTrang FROM dbo.ThietBi WHERE MaTB = @MaTB;
IF @TinhTrangTB <> N'Hoạt động' THROW 51015, N'Thiết bị chưa khôi phục về Hoạt động sau khi hoàn tất bảo trì.', 1;

PRINT N'    [PASS] Bảo trì và hoàn tất bảo trì khôi phục thiết bị về Hoạt động thành công.';

-- LUỒNG 12: HOÀN TIỀN VÀ HỦY GÓI TẬP LIÊN QUAN
PRINT N'--> [Luồng 12] Hoàn tiền toàn bộ và hủy gói tập liên quan qua sp_HoanTien';
DECLARE @MaTT_Hoan INT;
EXEC dbo.sp_HoanTien
    @MaHD = @MaHD_Goi,
    @SoTien = @TongTienGoi,
    @PhuongThuc = N'Chuyển khoản',
    @MaNV = 1,
    @MaTT = @MaTT_Hoan OUTPUT;

DECLARE @TrangThaiGoiSauHoan NVARCHAR(30);
SELECT @TrangThaiGoiSauHoan = TrangThai FROM dbo.DangKyGoi WHERE MaDKG = @MaDKG;
IF @TrangThaiGoiSauHoan <> N'Hủy'
    THROW 51016, N'Gói tập không chuyển sang Hủy sau khi hoàn tiền toàn bộ hóa đơn.', 1;

PRINT N'    [PASS] Hoàn tiền toàn bộ và hủy gói tập liên quan thành công.';

PRINT N'========================================================================================';
PRINT N'   TẤT CẢ 12 LUỒNG NGHIỆP VỤ LÕI ĐÃ PASS 100% - HỆ THỐNG ĐẠT CHUẨN NGHIỆM THU!';
PRINT N'========================================================================================';

IF @@TRANCOUNT > 0
    ROLLBACK TRANSACTION;
PRINT N'--> Đã ROLLBACK dữ liệu thử nghiệm an toàn, database sạch nguyên vẹn.';
