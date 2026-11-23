/* Chạy sau 01_upgrade_ql_phonggym_v3.sql.
   Đồng bộ kết quả đăng ký lớp với giao diện: hoặc có mã đăng ký, hoặc vào danh sách chờ. */
USE QL_PhongGym;
GO

CREATE OR ALTER PROCEDURE dbo.sp_DangKyLop
    @MaTV INT,
    @MaLop INT,
    @VaoDanhSachCho BIT = 1,
    @MaDKL INT OUTPUT
AS
BEGIN TRY
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRAN;

    SET @MaDKL = NULL;

    IF NOT EXISTS (
        SELECT 1 FROM dbo.ThanhVien
        WHERE MaTV = @MaTV AND TrangThai = N'Hoạt động' AND DaXoa = 0
    ) THROW 51060, N'Thành viên không hoạt động.', 1;

    DECLARE @SucChua INT, @SoDangKy INT, @TrangThaiCu NVARCHAR(30);
    SELECT @SucChua = SucChua
    FROM dbo.LopTap WITH (UPDLOCK, HOLDLOCK)
    WHERE MaLop = @MaLop AND TrangThai = N'Đang mở';

    IF @SucChua IS NULL THROW 51061, N'Lớp không mở hoặc không tồn tại.', 1;

    SELECT @TrangThaiCu = TrangThai
    FROM dbo.DangKyLop WITH (UPDLOCK, HOLDLOCK)
    WHERE MaTV = @MaTV AND MaLop = @MaLop;

    IF @TrangThaiCu = N'Đang tham gia'
        THROW 51062, N'Thành viên đã đăng ký lớp này.', 1;

    IF EXISTS (
        SELECT 1 FROM dbo.DanhSachCho
        WHERE MaLop = @MaLop AND MaTV = @MaTV AND TrangThai = N'Đang chờ'
    ) THROW 51063, N'Thành viên đang ở danh sách chờ của lớp này.', 1;

    SELECT @SoDangKy = COUNT(*)
    FROM dbo.DangKyLop WITH (UPDLOCK, HOLDLOCK)
    WHERE MaLop = @MaLop AND TrangThai = N'Đang tham gia';

    IF @SoDangKy >= @SucChua
    BEGIN
        IF @VaoDanhSachCho = 0
            THROW 51064, N'Lớp đã đủ sức chứa.', 1;

        IF EXISTS (SELECT 1 FROM dbo.DanhSachCho WHERE MaLop = @MaLop AND MaTV = @MaTV)
            UPDATE dbo.DanhSachCho
            SET TrangThai = N'Đang chờ', ThoiGianCho = SYSDATETIME()
            WHERE MaLop = @MaLop AND MaTV = @MaTV;
        ELSE
            INSERT dbo.DanhSachCho(MaLop, MaTV) VALUES(@MaLop, @MaTV);
        COMMIT;
        RETURN;
    END;

    IF @TrangThaiCu = N'Đã hủy'
    BEGIN
        UPDATE dbo.DangKyLop
        SET TrangThai = N'Đang tham gia', NgayDangKy = CAST(SYSDATETIME() AS DATE)
        WHERE MaTV = @MaTV AND MaLop = @MaLop;

        SELECT @MaDKL = MaDKL FROM dbo.DangKyLop WHERE MaTV = @MaTV AND MaLop = @MaLop;
    END
    ELSE IF @TrangThaiCu IS NOT NULL
        THROW 51065, N'Đăng ký lớp trước đó không thể dùng lại.', 1;
    ELSE
    BEGIN
        INSERT dbo.DangKyLop(MaTV, MaLop) VALUES(@MaTV, @MaLop);
        SET @MaDKL = SCOPE_IDENTITY();
    END;

    UPDATE dbo.DanhSachCho
    SET TrangThai = N'Đã mời'
    WHERE MaLop = @MaLop AND MaTV = @MaTV AND TrangThai = N'Đang chờ';

    COMMIT;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK;
    THROW;
END CATCH;
GO
