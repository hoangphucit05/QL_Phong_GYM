/* Kiểm thử dữ liệu demo sau migration 17, chỉ đọc. */
USE QL_PhongGym;
GO
SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

DECLARE @ThieuLich INT, @LegacyAttendance INT, @VuotSucChua INT, @LegacyLines INT, @LegacyQty INT;
SELECT @ThieuLich = COUNT(*)
FROM dbo.LopTap l
WHERE l.TrangThai = N'Đang mở'
  AND NOT EXISTS (
      SELECT 1 FROM dbo.LichHoc lh
      WHERE lh.MaLop = l.MaLop
        AND lh.ThoiGianBatDau >= CAST(SYSDATETIME() AS DATE)
        AND lh.TrangThai = N'Đã lên lịch'
  );
SELECT @LegacyAttendance = COUNT(*) FROM dbo.DiemDanhLop WHERE MaLichHoc IS NULL;
SELECT @VuotSucChua = COUNT(*) FROM dbo.LopTap l JOIN dbo.PhongTap p ON p.MaPhong = l.MaPhong WHERE l.SucChua > p.SucChua;
SELECT @LegacyLines = COUNT(*), @LegacyQty = ISNULL(SUM(SoLuong), 0) FROM dbo.ChiTietPhieuBanHang;

IF @ThieuLich <> 0 OR @LegacyAttendance <> 0 OR @VuotSucChua <> 0
    THROW 54000, N'Dữ liệu demo lịch/điểm danh/sức chứa chưa nhất quán.', 1;
IF @LegacyLines <> 15 OR @LegacyQty <> 16
    THROW 54001, N'Dữ liệu bán hàng legacy demo không còn đúng baseline.', 1;

IF NOT EXISTS (SELECT 1 FROM sys.views WHERE name = N'vw_BanHangLegacyDoiSoat')
    THROW 54002, N'Chưa có view đối soát bán hàng legacy.', 1;

SELECT N'PASS' AS KetQua,
       @ThieuLich AS LopThieuLich,
       @LegacyAttendance AS DiemDanhChuaGanLich,
       @VuotSucChua AS LopVuotSucChua,
       @LegacyLines AS DongBanLegacy,
       @LegacyQty AS SoLuongBanLegacy;
