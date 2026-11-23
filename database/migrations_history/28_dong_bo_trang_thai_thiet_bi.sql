/*
   Migration 28 - đồng bộ dữ liệu thiết bị sau các phiếu bảo trì lịch sử.
   Chỉ khôi phục thiết bị về Hoạt động khi không còn phiếu bảo trì mở.
   Thiết bị còn phiếu Đang bảo trì/Chờ bảo trì sẽ không bị thay đổi.
*/
USE QL_PhongGym;
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
GO

BEGIN TRANSACTION;
BEGIN TRY
    UPDATE tb
    SET tb.TinhTrang = N'Hoạt động'
    FROM dbo.ThietBi tb
    WHERE tb.TinhTrang = N'Bảo trì'
      AND NOT EXISTS
      (
          SELECT 1
          FROM dbo.BaoTriThietBi bt
          WHERE bt.MaTB = tb.MaTB
            AND bt.TrangThai IN (N'Đang bảo trì', N'Chờ bảo trì')
      );

    PRINT N'Đã đồng bộ ' + CONVERT(NVARCHAR(20), @@ROWCOUNT) + N' thiết bị không còn phiếu bảo trì mở về Hoạt động.';
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
