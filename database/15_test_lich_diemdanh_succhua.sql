/* Kiểm thử mục tiêu Phiên 7. Mọi dữ liệu tạo tạm đều rollback. */
USE QL_PhongGym;
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

DECLARE @LegacyCount INT, @LinkedCount INT, @Lich1 INT, @Lich2 INT;
DECLARE @NgayMai DATETIME2(0) = DATEADD(DAY, 1, CAST(CAST(SYSDATETIME() AS DATE) AS DATETIME2));
DECLARE @NgaySau DATETIME2(0) = DATEADD(DAY, 2, CAST(CAST(SYSDATETIME() AS DATE) AS DATETIME2));
DECLARE @Buoi1BatDau DATETIME2(0) = DATEADD(MINUTE, 30, @NgayMai);
DECLARE @Buoi1KetThuc DATETIME2(0) = DATEADD(MINUTE, 90, @NgayMai);
DECLARE @Buoi2BatDau DATETIME2(0) = DATEADD(MINUTE, 120, @NgayMai);
DECLARE @Buoi2KetThuc DATETIME2(0) = DATEADD(MINUTE, 180, @NgayMai);
DECLARE @BuoiSucChuaKetThuc DATETIME2(0) = DATEADD(MINUTE, 60, @NgaySau);
SELECT @LegacyCount = COUNT(*) FROM dbo.DiemDanhLop WHERE MaLichHoc IS NULL AND LaDuLieuCu = 1;
SELECT @LinkedCount = COUNT(*) FROM dbo.DiemDanhLop WHERE MaLichHoc IS NOT NULL AND LaDuLieuCu = 0;
IF @LegacyCount NOT IN (0, 17) OR @LinkedCount < 1
    THROW 52000, N'Đánh dấu điểm danh legacy hoặc dữ liệu điểm danh mới không đúng.', 1;
PRINT N'PASS LEGACY_ATTENDANCE_MARKED';

/* Không cho tạo điểm danh mới thiếu lịch học và không gắn cờ legacy. */
DECLARE @MaDKL INT = (SELECT TOP (1) MaDKL FROM dbo.DangKyLop WHERE TrangThai = N'Đang tham gia');
BEGIN TRY
    BEGIN TRAN;
    INSERT dbo.DiemDanhLop(MaDKL, NgayDiemDanh, TrangThai)
    VALUES (@MaDKL, CAST(SYSDATETIME() AS DATE), N'Có mặt');
    ROLLBACK;
    THROW 52001, N'Ràng buộc legacy không chặn được điểm danh mới thiếu MaLichHoc.', 1;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK;
    IF ERROR_NUMBER() <> 547
        THROW;
END CATCH;
PRINT N'PASS LEGACY_GUARD';

/* Một lớp có thể có nhiều buổi trong cùng ngày; procedure phải tạo được cả hai. */
BEGIN TRAN;
EXEC dbo.sp_TaoLichHoc
    @MaNV = 1,
    @MaLop = 11,
    @ThoiGianBatDau = @Buoi1BatDau,
    @ThoiGianKetThuc = @Buoi1KetThuc,
    @MaLichHoc = @Lich1 OUTPUT;
EXEC dbo.sp_TaoLichHoc
    @MaNV = 1,
    @MaLop = 11,
    @ThoiGianBatDau = @Buoi2BatDau,
    @ThoiGianKetThuc = @Buoi2KetThuc,
    @MaLichHoc = @Lich2 OUTPUT;
IF (SELECT COUNT(*) FROM dbo.LichHoc WHERE MaLop = 11 AND CAST(ThoiGianBatDau AS DATE) = CAST(@NgayMai AS DATE)) <> 2
    THROW 52002, N'Không tạo được hai buổi học trong cùng ngày.', 1;
ROLLBACK;
PRINT N'PASS_MULTI_SESSION_SAME_DAY';

/* Phòng Yoga A 25 chỗ phải chặn lớp 15 có sức chứa 30. */
BEGIN TRY
    BEGIN TRAN;
    UPDATE dbo.LopTap SET SucChua = 30 WHERE MaLop = 15;
    EXEC dbo.sp_TaoLichHoc
        @MaNV = 1,
        @MaLop = 15,
        @ThoiGianBatDau = @NgaySau,
        @ThoiGianKetThuc = @BuoiSucChuaKetThuc,
        @MaLichHoc = @Lich1 OUTPUT;
    ROLLBACK;
    THROW 52003, N'Procedure không chặn lớp vượt sức chứa phòng.', 1;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK;
    IF ERROR_NUMBER() <> 51073
        THROW;
END CATCH;
PRINT N'PASS_ROOM_CAPACITY_GUARD';

PRINT N'PASS SESSION_7_SCHEDULE_ATTENDANCE';
