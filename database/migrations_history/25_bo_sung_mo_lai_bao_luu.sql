USE QL_PhongGym;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.sp_MoLaiBaoLuuGoiTap
    @MaDKG INT,
    @MaNV INT = NULL
AS
BEGIN TRY
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRAN;

    DECLARE @HomNay DATE = CAST(SYSDATETIME() AS DATE),
            @MaBaoLuu INT,
            @TuNgay DATE,
            @DenNgay DATE,
            @NgayKetThucCu DATE,
            @NgayKetThucMoi DATE,
            @SoNgayDuKien INT,
            @SoNgayBaoLuu INT,
            @TrangThaiMoi NVARCHAR(20);

    SELECT TOP (1)
        @MaBaoLuu = b.MaBaoLuu,
        @TuNgay = b.TuNgay,
        @DenNgay = b.DenNgay
    FROM dbo.BaoLuuGoiTap b WITH (UPDLOCK, HOLDLOCK)
    WHERE b.MaDKG = @MaDKG AND b.TrangThai = N'Đang bảo lưu'
    ORDER BY b.MaBaoLuu DESC;

    IF @MaBaoLuu IS NULL
        THROW 50044, N'Không có lần bảo lưu đang hoạt động.', 1;

    SELECT @NgayKetThucCu = NgayKetThuc
    FROM dbo.DangKyGoi WITH (UPDLOCK, HOLDLOCK)
    WHERE MaDKG = @MaDKG AND TrangThai = N'Tạm dừng';

    IF @NgayKetThucCu IS NULL
        THROW 50046, N'Gói không ở trạng thái tạm dừng.', 1;

    SET @SoNgayDuKien = DATEDIFF(DAY, @TuNgay, @DenNgay) + 1;
    SET @SoNgayBaoLuu = CASE
        WHEN @HomNay < @TuNgay THEN 0
        WHEN @HomNay < @DenNgay THEN DATEDIFF(DAY, @TuNgay, @HomNay) + 1
        ELSE @SoNgayDuKien
    END;
    SET @NgayKetThucMoi = DATEADD(DAY, @SoNgayBaoLuu, @NgayKetThucCu);
    SET @TrangThaiMoi = CASE WHEN @NgayKetThucMoi >= @HomNay THEN N'Còn hạn' ELSE N'Hết hạn' END;

    UPDATE dbo.BaoLuuGoiTap
    SET TrangThai = N'Đã kết thúc'
    WHERE MaBaoLuu = @MaBaoLuu;

    UPDATE dbo.DangKyGoi
    SET TrangThai = @TrangThaiMoi,
        NgayKetThuc = @NgayKetThucMoi,
        TongSoNgayBaoLuu = CASE
            WHEN TongSoNgayBaoLuu - @SoNgayDuKien + @SoNgayBaoLuu < 0 THEN 0
            ELSE TongSoNgayBaoLuu - @SoNgayDuKien + @SoNgayBaoLuu
        END,
        NgayCapNhat = SYSDATETIME()
    WHERE MaDKG = @MaDKG;

    INSERT dbo.LichSuGoiTap(MaDKG, HanhDong, TuNgay, DenNgay, GhiChu, MaNV)
    VALUES(@MaDKG, N'Mở lại bảo lưu', @TuNgay, @DenNgay,
           CONCAT(N'Mở lại bảo lưu; số ngày tạm dừng thực tế: ', @SoNgayBaoLuu, N'.'), @MaNV);

    COMMIT;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK;
    THROW;
END CATCH;
GO
