-- Migration 26: Chuyển phòng trong cùng phiên Check-in, không tính thêm lượt.
USE QL_PhongGym;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
SET ANSI_PADDING ON;
GO
SET ANSI_WARNINGS ON;
GO
SET CONCAT_NULL_YIELDS_NULL ON;
GO
SET ANSI_NULL_DFLT_ON ON;
GO
SET ANSI_NULL_DFLT_OFF OFF;
GO
SET ARITHABORT ON;
GO

CREATE OR ALTER PROCEDURE dbo.sp_ChuyenPhongCheckIn
    @MaTV INT,
    @MaPhongMoi INT,
    @MaNV INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV, N'Admin|Quản lý|Lễ tân|Nhân viên') = 0
            THROW 52110, N'Tài khoản thực hiện chuyển phòng không có quyền hoặc không còn hoạt động.', 1;

        DECLARE @MaPhongCu INT, @SucChua INT, @SoNguoiDangTap INT;

        SELECT TOP (1) @MaPhongCu = MaPhong
        FROM dbo.LichSuCheckIn WITH (UPDLOCK, HOLDLOCK)
        WHERE MaTV = @MaTV AND ThoiGianRa IS NULL
        ORDER BY ThoiGianVao DESC;

        IF @@ROWCOUNT = 0
            THROW 50012, N'Không tìm thấy lượt check-in mở.', 1;

        IF @MaPhongCu = @MaPhongMoi
            THROW 50047, N'Phòng mới phải khác phòng hiện tại.', 1;

        SELECT @SucChua = SucChua
        FROM dbo.PhongTap WITH (UPDLOCK, HOLDLOCK)
        WHERE MaPhong = @MaPhongMoi AND TrangThai = N'Hoạt động';

        IF @SucChua IS NULL
            THROW 50038, N'Phòng không tồn tại hoặc đang bảo trì/đóng.', 1;

        SELECT @SoNguoiDangTap = COUNT(*)
        FROM dbo.LichSuCheckIn WITH (UPDLOCK, HOLDLOCK)
        WHERE MaPhong = @MaPhongMoi AND ThoiGianRa IS NULL;

        IF @SoNguoiDangTap >= @SucChua
            THROW 50048, N'Phòng mới đã đủ sức chứa.', 1;

        UPDATE dbo.LichSuCheckIn
        SET MaPhong = @MaPhongMoi
        WHERE MaTV = @MaTV AND ThoiGianRa IS NULL;

        INSERT dbo.NhatKyHeThong(MaNV, HanhDong, DoiTuong, KhoaDoiTuong, NoiDung)
        VALUES
        (
            @MaNV,
            N'Chuyển phòng',
            N'LichSuCheckIn',
            CONVERT(NVARCHAR(30), @MaTV),
            CONCAT(N'Chuyển từ phòng #', COALESCE(CONVERT(NVARCHAR(20), @MaPhongCu), N'Chưa gán'), N' sang phòng #', @MaPhongMoi)
        );

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO
