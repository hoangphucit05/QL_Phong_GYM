-- Migration 33: Bổ sung thủ tục xác nhận hoàn thành buổi tập PT và xem lịch sử buổi tập
USE QL_PhongGym;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- 1. Thủ tục xác nhận hoàn thành 1 buổi tập PT (trừ buổi và đổi trạng thái nếu đủ)
CREATE OR ALTER PROCEDURE dbo.sp_XacNhanBuoiTapPT
    @MaDKPT INT,
    @MaNV INT,
    @NhanXet NVARCHAR(500) = NULL,
    @SoBuoiDaTap INT OUTPUT,
    @SoBuoiConLai INT OUTPUT,
    @TrangThaiMoi NVARCHAR(30) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRAN;

        -- 1. Kiểm tra quyền nhân viên thực hiện (HLV, Lễ tân, Quản lý, Admin)
        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV, N'Admin|Quản lý|Lễ tân|HLV|Huấn luyện viên|Nhân viên') = 0
            THROW 52120, N'Tài khoản thực hiện xác nhận buổi tập không có quyền hoặc không còn hoạt động.', 1;

        -- 2. Kiểm tra hợp đồng PT
        DECLARE @MaTV INT, @MaHLV INT, @SoBuoiTong INT, @TrangThaiHT NVARCHAR(30), @NgayKetThuc DATE;
        SELECT @MaTV = d.MaTV,
               @MaHLV = d.MaHLV,
               @SoBuoiTong = d.SoBuoi,
               @TrangThaiHT = d.TrangThai,
               @NgayKetThuc = d.NgayKetThuc
        FROM dbo.DangKyPT d WITH (UPDLOCK, HOLDLOCK)
        WHERE d.MaDKPT = @MaDKPT;

        IF @MaTV IS NULL
            THROW 50050, N'Hợp đồng đăng ký PT không tồn tại.', 1;

        IF @TrangThaiHT = N'Chờ thanh toán'
            THROW 50051, N'Hợp đồng PT chưa được thanh toán. Vui lòng thanh toán trước khi tập.', 1;

        IF @TrangThaiHT = N'Hủy'
            THROW 50052, N'Hợp đồng PT đã bị hủy.', 1;

        IF @TrangThaiHT = N'Hoàn thành'
            THROW 50053, N'Hợp đồng PT này đã hoàn thành tất cả số buổi tập.', 1;

        IF @TrangThaiHT <> N'Đang học'
            THROW 50054, N'Hợp đồng PT không ở trạng thái hợp lệ để tập.', 1;

        -- 3. Đếm số buổi đã hoàn thành
        SELECT @SoBuoiDaTap = COUNT(*)
        FROM dbo.LichTapPT WITH (UPDLOCK, HOLDLOCK)
        WHERE MaDKPT = @MaDKPT AND TrangThai = N'Hoàn thành';

        IF @SoBuoiDaTap >= @SoBuoiTong
        BEGIN
            UPDATE dbo.DangKyPT SET TrangThai = N'Hoàn thành' WHERE MaDKPT = @MaDKPT;
            THROW 50055, N'Hợp đồng đã hoàn thành đủ số buổi đăng ký.', 1;
        END;

        -- 4. Ghi nhận buổi tập hoàn thành vào LichTapPT
        DECLARE @MaLichPT INT;
        INSERT INTO dbo.LichTapPT(MaDKPT, MaHLV, MaTV, ThoiGianBatDau, ThoiGianKetThuc, TrangThai, NhanXet, NgayTao)
        VALUES(@MaDKPT, @MaHLV, @MaTV, DATEADD(HOUR, -1, SYSDATETIME()), SYSDATETIME(), N'Hoàn thành', @NhanXet, SYSDATETIME());
        SET @MaLichPT = SCOPE_IDENTITY();

        SET @SoBuoiDaTap = @SoBuoiDaTap + 1;
        SET @SoBuoiConLai = @SoBuoiTong - @SoBuoiDaTap;

        -- 5. Nếu đã đủ số buổi, chuyển trạng thái DangKyPT thành 'Hoàn thành'
        IF @SoBuoiConLai = 0
        BEGIN
            UPDATE dbo.DangKyPT SET TrangThai = N'Hoàn thành' WHERE MaDKPT = @MaDKPT;
            SET @TrangThaiMoi = N'Hoàn thành';
        END
        ELSE
        BEGIN
            SET @TrangThaiMoi = N'Đang học';
        END;

        -- 6. Ghi nhật ký hệ thống
        INSERT INTO dbo.NhatKyHeThong(MaNV, HanhDong, DoiTuong, KhoaDoiTuong, NoiDung)
        VALUES(@MaNV, N'Hoàn thành buổi PT', N'DangKyPT', CAST(@MaDKPT AS NVARCHAR(30)),
               CONCAT(N'Buổi ', @SoBuoiDaTap, N'/', @SoBuoiTong, N'; Ghi chú: ', ISNULL(@NhanXet, N'--')));

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

-- 2. Thủ tục lấy lịch sử các buổi tập của 1 hợp đồng PT
CREATE OR ALTER PROCEDURE dbo.sp_LayLichSuBuoiTapPT
    @MaDKPT INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT lt.MaLichPT,
           lt.ThoiGianBatDau,
           lt.ThoiGianKetThuc,
           lt.TrangThai,
           lt.NhanXet,
           lt.NgayTao
    FROM dbo.LichTapPT lt
    WHERE lt.MaDKPT = @MaDKPT
    ORDER BY lt.MaLichPT DESC;
END;
GO
