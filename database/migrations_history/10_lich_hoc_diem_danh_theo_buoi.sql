/* Chạy sau 01_upgrade_ql_phonggym_v3.sql.
   Chuẩn hóa điểm danh theo buổi học cụ thể thay vì chỉ theo ngày. */
USE QL_PhongGym;
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'UX_LichHoc_Lop_ThoiGianBatDau')
    CREATE UNIQUE INDEX UX_LichHoc_Lop_ThoiGianBatDau ON dbo.LichHoc(MaLop, ThoiGianBatDau);
GO

CREATE OR ALTER PROCEDURE dbo.sp_TaoLichHoc
    @MaLop INT,
    @ThoiGianBatDau DATETIME2(0),
    @ThoiGianKetThuc DATETIME2(0),
    @MaLichHoc INT OUTPUT
AS
BEGIN TRY
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRAN;

    IF @ThoiGianKetThuc <= @ThoiGianBatDau
        THROW 51070, N'Khung giờ lớp không hợp lệ.', 1;
    IF CAST(@ThoiGianBatDau AS DATE) < CAST(SYSDATETIME() AS DATE)
        THROW 51071, N'Không thể tạo buổi học trong quá khứ.', 1;

    DECLARE @HLV INT, @Phong INT, @SucChuaLop INT, @SucChuaPhong INT, @ThuTrongTuan TINYINT;
    SELECT @HLV=MaHLV, @Phong=MaPhong, @SucChuaLop=SucChua, @ThuTrongTuan=ThuTrongTuan
    FROM dbo.LopTap WITH (UPDLOCK, HOLDLOCK)
    WHERE MaLop=@MaLop AND TrangThai=N'Đang mở';

    IF @HLV IS NULL OR @Phong IS NULL
        THROW 51072, N'Lớp chưa mở hoặc chưa được gán phòng.', 1;

    SELECT @SucChuaPhong=SucChua FROM dbo.PhongTap WITH (UPDLOCK, HOLDLOCK)
    WHERE MaPhong=@Phong AND TrangThai=N'Hoạt động';
    IF @SucChuaPhong IS NULL OR @SucChuaPhong < @SucChuaLop
        THROW 51073, N'Phòng không hoạt động hoặc không đủ sức chứa cho lớp.', 1;

    IF @ThuTrongTuan IS NOT NULL
       AND @ThuTrongTuan <> CONVERT(TINYINT, (DATEDIFF(DAY, '19000101', CAST(@ThoiGianBatDau AS DATE)) % 7) + 2)
        THROW 51074, N'Ngày tạo buổi học không đúng lịch cố định của lớp.', 1;

    IF EXISTS (SELECT 1 FROM dbo.LichHoc WITH (UPDLOCK, HOLDLOCK)
               WHERE MaLop=@MaLop AND ThoiGianBatDau=@ThoiGianBatDau)
        THROW 51075, N'Buổi học này đã được tạo.', 1;

    IF EXISTS (SELECT 1 FROM dbo.LichHoc WITH (UPDLOCK, HOLDLOCK)
               WHERE TrangThai=N'Đã lên lịch' AND (MaPhong=@Phong OR MaHLV=@HLV)
                 AND @ThoiGianBatDau<ThoiGianKetThuc AND @ThoiGianKetThuc>ThoiGianBatDau)
        THROW 51076, N'Phòng hoặc HLV bị trùng lịch.', 1;

    INSERT dbo.LichHoc(MaLop,MaHLV,MaPhong,ThoiGianBatDau,ThoiGianKetThuc)
    VALUES(@MaLop,@HLV,@Phong,@ThoiGianBatDau,@ThoiGianKetThuc);
    SET @MaLichHoc=SCOPE_IDENTITY();
    COMMIT;
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK;
    THROW;
END CATCH;
GO

CREATE OR ALTER PROCEDURE dbo.sp_DiemDanhLop
    @MaDKL INT,
    @MaLichHoc INT,
    @TrangThai NVARCHAR(30)
AS
BEGIN TRY
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRAN;

    IF @TrangThai NOT IN(N'Có mặt',N'Vắng mặt')
        THROW 51050, N'Trạng thái điểm danh không hợp lệ.', 1;

    DECLARE @NgayDiemDanh DATE;
    SELECT @NgayDiemDanh=CAST(ThoiGianBatDau AS DATE)
    FROM dbo.LichHoc WITH (UPDLOCK, HOLDLOCK)
    WHERE MaLichHoc=@MaLichHoc AND TrangThai IN(N'Đã lên lịch',N'Đã diễn ra');
    IF @NgayDiemDanh IS NULL
        THROW 51077, N'Buổi học không tồn tại hoặc đã bị hủy.', 1;
    IF @NgayDiemDanh > CAST(SYSDATETIME() AS DATE)
        THROW 51078, N'Không thể điểm danh cho buổi học trong tương lai.', 1;

    IF NOT EXISTS (
        SELECT 1
        FROM dbo.DangKyLop d
        JOIN dbo.LichHoc l ON l.MaLop=d.MaLop
        WHERE d.MaDKL=@MaDKL AND l.MaLichHoc=@MaLichHoc AND d.TrangThai=N'Đang tham gia'
    ) THROW 51079, N'Học viên không thuộc buổi học này hoặc đăng ký không còn hiệu lực.', 1;

    UPDATE dbo.DiemDanhLop
    SET TrangThai=@TrangThai, NgayDiemDanh=@NgayDiemDanh
    WHERE MaDKL=@MaDKL AND MaLichHoc=@MaLichHoc;

    IF @@ROWCOUNT=0
        INSERT dbo.DiemDanhLop(MaDKL,MaLichHoc,NgayDiemDanh,TrangThai)
        VALUES(@MaDKL,@MaLichHoc,@NgayDiemDanh,@TrangThai);

    UPDATE dbo.LichHoc
    SET TrangThai=N'Đã diễn ra'
    WHERE MaLichHoc=@MaLichHoc AND TrangThai=N'Đã lên lịch';

    COMMIT;
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK;
    THROW;
END CATCH;
GO
