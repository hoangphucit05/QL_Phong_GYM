/* Phiên 1 - Chỉ đọc dữ liệu. Chạy trước/sau mỗi migration để đối soát vận hành. */
USE QL_PhongGym;
GO
SET NOCOUNT ON;

SELECT N'Gói còn hạn nhưng quá ngày kết thúc' AS HangMuc,
       COUNT(*) AS SoLuong
FROM dbo.DangKyGoi
WHERE TrangThai=N'Còn hạn' AND NgayKetThuc<CAST(SYSDATETIME() AS DATE)

UNION ALL
SELECT N'Lớp đang mở chưa có buổi học tương lai', COUNT(*)
FROM dbo.LopTap l
WHERE l.TrangThai=N'Đang mở'
  AND NOT EXISTS (
      SELECT 1 FROM dbo.LichHoc lh
      WHERE lh.MaLop=l.MaLop AND lh.ThoiGianBatDau>=SYSDATETIME() AND lh.TrangThai=N'Đã lên lịch'
  )

UNION ALL
SELECT N'Điểm danh lịch sử chưa gắn buổi học', COUNT(*)
FROM dbo.DiemDanhLop
WHERE MaLichHoc IS NULL

UNION ALL
SELECT N'Lớp có sức chứa vượt phòng được gán', COUNT(*)
FROM dbo.LopTap l
JOIN dbo.PhongTap p ON p.MaPhong=l.MaPhong
WHERE l.TrangThai=N'Đang mở' AND l.SucChua>p.SucChua

UNION ALL
SELECT N'Hóa đơn lệch tổng tiền chi tiết', COUNT(*)
FROM dbo.HoaDon h
OUTER APPLY (SELECT SUM(SoLuong*DonGia) AS TongChiTiet FROM dbo.ChiTietHoaDon WHERE MaHD=h.MaHD) ct
WHERE ABS(h.TongTien-ISNULL(ct.TongChiTiet,0))>0.01;
GO

SELECT l.MaLop,l.TenLop,l.SucChua,p.MaPhong,p.TenPhong,p.SucChua AS SucChuaPhong
FROM dbo.LopTap l
JOIN dbo.PhongTap p ON p.MaPhong=l.MaPhong
WHERE l.TrangThai=N'Đang mở' AND l.SucChua>p.SucChua;
GO
