-- 22_khach_vang_lai_va_lop_tap.sql
-- Migration Phiên 14: Thêm bản ghi hệ thống Khách vãng lai cho bán hàng POS
USE QL_PhongGym;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

-- 1. Bổ sung bản ghi Khách vãng lai cho nghiệp vụ bán hàng POS
IF NOT EXISTS (SELECT 1 FROM dbo.ThanhVien WHERE SDT = '0900000000' OR HoTen = N'Khách vãng lai')
BEGIN
    INSERT INTO dbo.ThanhVien (HoTen, NgaySinh, GioiTinh, SDT, Email, DiaChi, MaLoaiTV, TrangThai)
    VALUES (N'Khách vãng lai', '2000-01-01', N'Khác', '0900000000', NULL, N'Bán lẻ tại quầy', 1, N'Hoạt động');
END;
GO
