/*
    Dữ liệu mẫu demo: hoàn tất lịch lớp/điểm danh và cấu hình sức chứa.
    Chạy sau migration 16 trên database demo. Có thể chạy lặp an toàn.
*/
USE QL_PhongGym;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    /* Đồng bộ sức chứa lớp với phòng hiện tại; dữ liệu demo không để lớp vượt phòng. */
    UPDATE l
    SET l.SucChua = p.SucChua
    FROM dbo.LopTap l
    JOIN dbo.PhongTap p ON p.MaPhong = l.MaPhong
    WHERE l.SucChua > p.SucChua;

    /* Tạo một buổi lịch sử cho mỗi lớp có điểm danh cũ chưa gắn buổi. */
    DECLARE @NgayLegacy DATE = '2026-05-20';
    INSERT dbo.LichHoc(MaLop, MaHLV, MaPhong, ThoiGianBatDau, ThoiGianKetThuc, TrangThai)
    SELECT l.MaLop, l.MaHLV, l.MaPhong,
           DATEADD(MINUTE, DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), l.GioBatDau), CAST(@NgayLegacy AS DATETIME2)),
           DATEADD(MINUTE, DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), l.GioKetThuc), CAST(@NgayLegacy AS DATETIME2)),
           N'Đã diễn ra'
    FROM dbo.LopTap l
    WHERE EXISTS (
              SELECT 1
              FROM dbo.DiemDanhLop dd
              JOIN dbo.DangKyLop d ON d.MaDKL = dd.MaDKL
              WHERE dd.MaLichHoc IS NULL AND d.MaLop = l.MaLop AND dd.NgayDiemDanh = @NgayLegacy
          )
      AND NOT EXISTS (
              SELECT 1 FROM dbo.LichHoc lh
              WHERE lh.MaLop = l.MaLop
                AND CAST(lh.ThoiGianBatDau AS DATE) = @NgayLegacy
          );

    UPDATE dd
    SET dd.MaLichHoc = lh.MaLichHoc,
        dd.LaDuLieuCu = 0
    FROM dbo.DiemDanhLop dd
    JOIN dbo.DangKyLop d ON d.MaDKL = dd.MaDKL
    JOIN dbo.LichHoc lh ON lh.MaLop = d.MaLop
                         AND CAST(lh.ThoiGianBatDau AS DATE) = dd.NgayDiemDanh
                         AND lh.TrangThai = N'Đã diễn ra'
    WHERE dd.MaLichHoc IS NULL;

    /* Bao phủ các ngày legacy khác nếu dữ liệu demo có nhiều mốc lịch sử. */
    INSERT dbo.LichHoc(MaLop, MaHLV, MaPhong, ThoiGianBatDau, ThoiGianKetThuc, TrangThai)
    SELECT DISTINCT l.MaLop, l.MaHLV, l.MaPhong,
           DATEADD(MINUTE, DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), l.GioBatDau), CAST(dd.NgayDiemDanh AS DATETIME2)),
           DATEADD(MINUTE, DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), l.GioKetThuc), CAST(dd.NgayDiemDanh AS DATETIME2)),
           N'Đã diễn ra'
    FROM dbo.DiemDanhLop dd
    JOIN dbo.DangKyLop d ON d.MaDKL = dd.MaDKL
    JOIN dbo.LopTap l ON l.MaLop = d.MaLop
    WHERE dd.MaLichHoc IS NULL
      AND NOT EXISTS (
              SELECT 1 FROM dbo.LichHoc lh
              WHERE lh.MaLop = l.MaLop
                AND CAST(lh.ThoiGianBatDau AS DATE) = dd.NgayDiemDanh
          );

    UPDATE dd
    SET dd.MaLichHoc = lh.MaLichHoc,
        dd.LaDuLieuCu = 0
    FROM dbo.DiemDanhLop dd
    JOIN dbo.DangKyLop d ON d.MaDKL = dd.MaDKL
    JOIN dbo.LichHoc lh ON lh.MaLop = d.MaLop
                         AND CAST(lh.ThoiGianBatDau AS DATE) = dd.NgayDiemDanh
                         AND lh.TrangThai = N'Đã diễn ra'
    WHERE dd.MaLichHoc IS NULL;

    /* Tạo tối thiểu một buổi tương lai cho mọi lớp đang mở chưa có lịch tương lai. */
    DECLARE @NgayDemo DATE = DATEADD(DAY, 1, CAST(SYSDATETIME() AS DATE));
    INSERT dbo.LichHoc(MaLop, MaHLV, MaPhong, ThoiGianBatDau, ThoiGianKetThuc, TrangThai)
    SELECT l.MaLop, l.MaHLV, l.MaPhong,
           DATEADD(MINUTE, DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), l.GioBatDau),
                   CAST(DATEADD(DAY, l.MaLop - 1, @NgayDemo) AS DATETIME2)),
           DATEADD(MINUTE, DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), l.GioKetThuc),
                   CAST(DATEADD(DAY, l.MaLop - 1, @NgayDemo) AS DATETIME2)),
           N'Đã lên lịch'
    FROM dbo.LopTap l
    WHERE l.TrangThai = N'Đang mở'
      AND NOT EXISTS (
              SELECT 1 FROM dbo.LichHoc lh
              WHERE lh.MaLop = l.MaLop
                AND lh.ThoiGianBatDau >= CAST(@NgayDemo AS DATETIME2)
                AND lh.TrangThai = N'Đã lên lịch'
          );

    /* Nguồn bán legacy được giữ riêng; view chỉ phục vụ trình diễn/đối soát. */
    COMMIT;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK;
    THROW;
END CATCH;
GO

CREATE OR ALTER VIEW dbo.vw_BanHangLegacyDoiSoat AS
SELECT sp.MaSP, sp.MaSanPham, sp.TenSP,
       COUNT_BIG(*) AS SoDongLegacy,
       SUM(pb.SoLuong) AS SoLuongLegacy,
       SUM(CONVERT(DECIMAL(18,2), pb.SoLuong) * pb.DonGia) AS DoanhThuLegacy
FROM dbo.ChiTietPhieuBanHang pb
JOIN dbo.SanPham sp ON sp.MaSP = pb.MaSP
GROUP BY sp.MaSP, sp.MaSanPham, sp.TenSP;
GO
