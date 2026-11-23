/* Kiểm thử Phiên 8: schema mã khuyến mãi và tách nguồn bán hàng legacy. */
USE QL_PhongGym;
GO
SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

DECLARE @SoMa INT, @SoMaKhongNull INT, @SoMaTrung INT, @LegacyQty INT, @CanonicalUnpaidQty INT;
SELECT @SoMa = COUNT(*), @SoMaKhongNull = COUNT(MaCode) FROM dbo.KhuyenMai;
SELECT @SoMaTrung = COUNT(*) - COUNT(DISTINCT MaCode) FROM dbo.KhuyenMai;
IF @SoMa = 0 OR @SoMaKhongNull <> @SoMa OR @SoMaTrung <> 0
    THROW 53001, N'MaCode khuyến mãi chưa đầy đủ hoặc bị trùng.', 1;
PRINT N'PASS_PROMO_CODE_SCHEMA';

SELECT @LegacyQty = ISNULL(SUM(SoLuong), 0) FROM dbo.ChiTietPhieuBanHang;
SELECT @CanonicalUnpaidQty = ISNULL(SUM(c.SoLuong), 0)
FROM dbo.ChiTietHoaDon c JOIN dbo.HoaDon h ON h.MaHD = c.MaHD
WHERE c.LoaiDichVu = N'Sản phẩm' AND h.TrangThai <> N'Đã thanh toán';
IF @LegacyQty <= 0 OR @CanonicalUnpaidQty <= 0
    THROW 53002, N'Không còn dữ liệu đối soát legacy/unpaid để xác minh tách nguồn.', 1;
PRINT N'PASS_LEGACY_RECONCILIATION_BASELINE';

/* Hóa đơn sản phẩm chưa thanh toán không được coi là bán trong báo cáo chuẩn. */
DECLARE @CanonicalPaidQty INT;
SELECT @CanonicalPaidQty = ISNULL(SUM(c.SoLuong), 0)
FROM dbo.ChiTietHoaDon c
JOIN dbo.HoaDon h ON h.MaHD = c.MaHD
WHERE c.LoaiDichVu = N'Sản phẩm'
  AND c.MaThamChieu = 15
  AND h.TrangThai = N'Đã thanh toán';
IF @CanonicalPaidQty <> 0
    THROW 53003, N'Báo cáo nguồn hóa đơn chuẩn vẫn tính dòng sản phẩm chưa thanh toán.', 1;
PRINT N'PASS_PRODUCT_REPORT_PAID_SOURCE';

/* Biên dịch/chạy đúng hình dạng truy vấn mà ThongKeDAL sử dụng. */
SELECT TOP (1) sp.MaSanPham, sp.TenSP, sp.DonGia,
       ISNULL(cthd.DaBan, 0) AS DaBan, sp.SoLuongTon AS TonKho,
       ISNULL(cthd.DoanhThu, 0) AS DoanhThu
FROM dbo.SanPham sp
OUTER APPLY (
    SELECT SUM(ct.SoLuong) AS DaBan, SUM(ct.SoLuong * ct.DonGia) AS DoanhThu
    FROM dbo.ChiTietHoaDon ct
    JOIN dbo.HoaDon h ON h.MaHD = ct.MaHD
    WHERE ct.LoaiDichVu = N'Sản phẩm'
      AND ct.MaThamChieu = sp.MaSP
      AND h.TrangThai = N'Đã thanh toán'
) cthd
ORDER BY DaBan DESC, sp.MaSP DESC;
PRINT N'PASS_PRODUCT_REPORT_QUERY';

PRINT N'PASS SESSION_8_CODES_INVENTORY';
