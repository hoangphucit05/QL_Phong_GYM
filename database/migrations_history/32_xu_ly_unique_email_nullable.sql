-- Migration 32: Cho phép nhiều hội viên / nhân viên không có email (Email IS NULL)
-- Đảm bảo khi có email thì vẫn duy nhất (Unique Filtered Index: WHERE Email IS NOT NULL).
USE QL_PhongGym;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- 1. Xử lý ThanhVien.Email
DECLARE @ConstraintName SYSNAME;
SELECT TOP (1) @ConstraintName = kc.name
FROM sys.key_constraints kc
JOIN sys.index_columns ic ON kc.parent_object_id = ic.object_id AND kc.unique_index_id = ic.index_id
JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
WHERE kc.parent_object_id = OBJECT_ID(N'dbo.ThanhVien') AND c.name = N'Email';

IF @ConstraintName IS NOT NULL
BEGIN
    EXEC(N'ALTER TABLE dbo.ThanhVien DROP CONSTRAINT [' + @ConstraintName + N']');
    PRINT N'Đã gỡ bỏ ràng buộc UNIQUE cũ trên ThanhVien.Email: ' + @ConstraintName;
END;

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_ThanhVien_Email' AND object_id = OBJECT_ID(N'dbo.ThanhVien'))
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UX_ThanhVien_Email
    ON dbo.ThanhVien(Email)
    WHERE Email IS NOT NULL;
    PRINT N'Đã tạo UNIQUE FILTERED INDEX UX_ThanhVien_Email (WHERE Email IS NOT NULL).';
END;
GO

-- 2. Xử lý NhanVien.Email
DECLARE @ConstraintNV SYSNAME;
SELECT TOP (1) @ConstraintNV = kc.name
FROM sys.key_constraints kc
JOIN sys.index_columns ic ON kc.parent_object_id = ic.object_id AND kc.unique_index_id = ic.index_id
JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
WHERE kc.parent_object_id = OBJECT_ID(N'dbo.NhanVien') AND c.name = N'Email';

IF @ConstraintNV IS NOT NULL
BEGIN
    EXEC(N'ALTER TABLE dbo.NhanVien DROP CONSTRAINT [' + @ConstraintNV + N']');
    PRINT N'Đã gỡ bỏ ràng buộc UNIQUE cũ trên NhanVien.Email: ' + @ConstraintNV;
END;

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_NhanVien_Email' AND object_id = OBJECT_ID(N'dbo.NhanVien'))
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UX_NhanVien_Email
    ON dbo.NhanVien(Email)
    WHERE Email IS NOT NULL;
    PRINT N'Đã tạo UNIQUE FILTERED INDEX UX_NhanVien_Email (WHERE Email IS NOT NULL).';
END;
GO
