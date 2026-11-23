-- Bổ sung quyền mặc định cho các gói đã được tạo trước khi luồng tạo gói
-- tự động cấp quyền được cập nhật.
USE QL_PhongGym;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
GO

IF OBJECT_ID(N'dbo.GoiTapDichVu', N'U') IS NULL
   OR OBJECT_ID(N'dbo.GoiTapPhong', N'U') IS NULL
    THROW 53001, N'Cần chạy migration 27_phan_quyen_dich_vu_theo_goi.sql trước.', 1;
GO

BEGIN TRANSACTION;

-- Mọi gói đều cần quyền dịch vụ phòng tập để có thể check-in.
INSERT dbo.GoiTapDichVu(MaGoi,MaDichVu,TenDichVu)
SELECT g.MaGoi,'PHONG_TAP',N'Phòng tập theo quyền gói'
FROM dbo.GoiTap g
WHERE NOT EXISTS
(
    SELECT 1
    FROM dbo.GoiTapDichVu q
    WHERE q.MaGoi=g.MaGoi AND q.MaDichVu='PHONG_TAP'
);

-- Gói cũ chưa được gán phòng: cấp các phòng hiện đang hoạt động.
INSERT dbo.GoiTapPhong(MaGoi,MaPhong)
SELECT g.MaGoi,p.MaPhong
FROM dbo.GoiTap g
CROSS JOIN dbo.PhongTap p
WHERE p.TrangThai=N'Hoạt động'
  AND NOT EXISTS
(
    SELECT 1
    FROM dbo.GoiTapPhong q
    WHERE q.MaGoi=g.MaGoi AND q.MaPhong=p.MaPhong
);

COMMIT TRANSACTION;
GO
