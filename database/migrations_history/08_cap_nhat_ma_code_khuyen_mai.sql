-- =========================================================================
-- PHIÊN 008: CẬP NHẬT MÃ KHUYẾN MÃI DẠNG CHỮ (PROMO CODE) THỰC TẾ
-- Thay thế mã khuyến mãi dạng số thứ tự đơn điệu bằng mã code thực tế
-- Ví dụ: CHAOHE10, SINHVIEN15, BLACKFRIDAY30, TET2026, NEWGYM5,...
-- =========================================================================

USE QL_PhongGym;
GO

-- 1. Bổ sung cột MaCode vào bảng KhuyenMai nếu chưa có
IF NOT EXISTS (
    SELECT 1 FROM sys.columns 
    WHERE object_id = OBJECT_ID(N'dbo.KhuyenMai') AND name = N'MaCode'
)
BEGIN
    ALTER TABLE dbo.KhuyenMai ADD MaCode NVARCHAR(30) NULL;
    PRINT N'Đã thêm cột MaCode vào bảng dbo.KhuyenMai.';
END
GO

-- 2. Cập nhật mã code thực tế cho các chương trình khuyến mãi hiện có
UPDATE dbo.KhuyenMai SET MaCode = N'CHAOHE10'       WHERE TenKM LIKE N'%Chào hè%';
UPDATE dbo.KhuyenMai SET MaCode = N'SINHVIEN15'     WHERE TenKM LIKE N'%Sinh viên%';
UPDATE dbo.KhuyenMai SET MaCode = N'NEWGYM5'        WHERE TenKM LIKE N'%Khách hàng mới%';
UPDATE dbo.KhuyenMai SET MaCode = N'BLACKFRIDAY30'  WHERE TenKM LIKE N'%Black Friday%';
UPDATE dbo.KhuyenMai SET MaCode = N'TET2026'        WHERE TenKM LIKE N'%Tết%';
UPDATE dbo.KhuyenMai SET MaCode = N'FAMILY12'       WHERE TenKM LIKE N'%gia đình%';
UPDATE dbo.KhuyenMai SET MaCode = N'PROPT10'        WHERE TenKM LIKE N'%PT%';
UPDATE dbo.KhuyenMai SET MaCode = N'YOGAMAY8'       WHERE TenKM LIKE N'%Yoga%';
UPDATE dbo.KhuyenMai SET MaCode = N'CARDIO7'        WHERE TenKM LIKE N'%Cardio%';
UPDATE dbo.KhuyenMai SET MaCode = N'VIPPRO18'       WHERE TenKM LIKE N'%VIP%';
UPDATE dbo.KhuyenMai SET MaCode = N'NOEL25'         WHERE TenKM LIKE N'%Noel%';
UPDATE dbo.KhuyenMai SET MaCode = N'TRIAN15'        WHERE TenKM LIKE N'%Tri ân%';
UPDATE dbo.KhuyenMai SET MaCode = N'FREETRIAL'      WHERE TenKM LIKE N'%Tập thử%';
UPDATE dbo.KhuyenMai SET MaCode = N'BACK2GYM10'     WHERE TenKM LIKE N'%tựu trường%';
UPDATE dbo.KhuyenMai SET MaCode = N'GOLDHEALTH9'    WHERE TenKM LIKE N'%Sức khỏe vàng%';

-- Tự động sinh mã cho bất kỳ bản ghi nào còn lại nếu chưa có mã
UPDATE dbo.KhuyenMai 
SET MaCode = N'KM' + RIGHT(N'000' + CAST(MaKM AS NVARCHAR(10)), 3)
WHERE MaCode IS NULL OR LTRIM(RTRIM(MaCode)) = N'';

PRINT N'Đã cập nhật mã khuyến mãi thực tế cho tất cả các chương trình.';
GO

-- 3. Đảm bảo ràng buộc UNIQUE cho MaCode nếu cần
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes 
    WHERE object_id = OBJECT_ID(N'dbo.KhuyenMai') AND name = N'UQ_KhuyenMai_MaCode'
)
BEGIN
    ALTER TABLE dbo.KhuyenMai ADD CONSTRAINT UQ_KhuyenMai_MaCode UNIQUE (MaCode);
    PRINT N'Đã tạo ràng buộc UNIQUE cho cột MaCode.';
END
GO
