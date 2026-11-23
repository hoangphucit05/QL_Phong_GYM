/*
    Đánh dấu minh bạch dữ liệu điểm danh cũ không có MaLichHoc.
    Không tự gán dữ liệu cũ vào buổi học mới vì không đủ bằng chứng xác định.
    Chạy sau database/14_sua_checkin_va_hoan_kho.sql.
*/
USE QL_PhongGym;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
GO

IF COL_LENGTH(N'dbo.DiemDanhLop', N'LaDuLieuCu') IS NULL
BEGIN
    ALTER TABLE dbo.DiemDanhLop
        ADD LaDuLieuCu BIT NOT NULL
            CONSTRAINT DF_DiemDanhLop_LaDuLieuCu DEFAULT (0) WITH VALUES;
END;
GO

UPDATE dbo.DiemDanhLop
SET LaDuLieuCu = 1
WHERE MaLichHoc IS NULL AND LaDuLieuCu = 0;
GO

IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = N'CK_DiemDanhLop_BuoiHoacLegacy')
    ALTER TABLE dbo.DiemDanhLop ADD CONSTRAINT CK_DiemDanhLop_BuoiHoacLegacy
        CHECK (MaLichHoc IS NOT NULL OR LaDuLieuCu = 1);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_DiemDanhLop_Legacy')
    CREATE INDEX IX_DiemDanhLop_Legacy ON dbo.DiemDanhLop(LaDuLieuCu, NgayDiemDanh)
        INCLUDE (MaDKL, MaLichHoc, TrangThai);
GO

CREATE OR ALTER VIEW dbo.vw_DiemDanhLegacy AS
SELECT dd.MaDD, dd.MaDKL, dd.NgayDiemDanh, dd.TrangThai, dd.LaDuLieuCu,
       d.MaLop, l.MaLopTap, l.TenLop, d.MaTV, tv.MaThanhVien, tv.HoTen
FROM dbo.DiemDanhLop dd
JOIN dbo.DangKyLop d ON d.MaDKL = dd.MaDKL
JOIN dbo.LopTap l ON l.MaLop = d.MaLop
JOIN dbo.ThanhVien tv ON tv.MaTV = d.MaTV
WHERE dd.MaLichHoc IS NULL AND dd.LaDuLieuCu = 1;
GO
