/* Phiên 2 - Tự động chuyển gói đã quá ngày kết thúc sang Hết hạn.
   Chạy sau 01_upgrade_ql_phonggym_v3.sql. */
USE QL_PhongGym;
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.sp_CapNhatGoiHetHan
    @NgayThamChieu DATE = NULL,
    @MaNV INT = NULL,
    @SoLuongCapNhat INT OUTPUT
AS
BEGIN TRY
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRAN;

    SET @NgayThamChieu = ISNULL(@NgayThamChieu, CONVERT(DATE, SYSDATETIME()));
    DECLARE @DaHetHan TABLE
    (
        MaDKG INT NOT NULL,
        TuNgay DATE NOT NULL,
        DenNgay DATE NOT NULL
    );

    UPDATE d WITH (UPDLOCK, ROWLOCK)
    SET d.TrangThai=N'Hết hạn', d.NgayCapNhat=SYSDATETIME()
    OUTPUT inserted.MaDKG, inserted.NgayBatDau, inserted.NgayKetThuc
        INTO @DaHetHan(MaDKG,TuNgay,DenNgay)
    FROM dbo.DangKyGoi d
    WHERE d.TrangThai=N'Còn hạn' AND d.NgayKetThuc<@NgayThamChieu;

    SET @SoLuongCapNhat = @@ROWCOUNT;

    INSERT dbo.LichSuGoiTap(MaDKG,HanhDong,TuNgay,DenNgay,GhiChu,MaNV)
    SELECT MaDKG,N'Hết hạn',TuNgay,DenNgay,
           CONCAT(N'Tự động cập nhật theo ngày ',CONVERT(NVARCHAR(10),@NgayThamChieu,103)),@MaNV
    FROM @DaHetHan;

    IF @SoLuongCapNhat>0
    BEGIN
        INSERT dbo.NhatKyHeThong(MaNV,HanhDong,DoiTuong,KhoaDoiTuong,NoiDung)
        VALUES(@MaNV,N'Tự động hết hạn gói',N'DangKyGoi',NULL,
               CONCAT(N'Đã cập nhật ',@SoLuongCapNhat,N' gói đến ngày ',CONVERT(NVARCHAR(10),@NgayThamChieu,103)));
    END;

    COMMIT;
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK;
    THROW;
END CATCH;
GO

/* Tạo lịch SQL Server Agent hằng ngày lúc 00:05 nếu máy chủ hỗ trợ Agent.
   Nếu là SQL Express hoặc tài khoản không có quyền Agent, phần này chỉ cảnh báo;
   ứng dụng vẫn gọi thủ tục khi mở Dashboard. */
USE msdb;
GO
BEGIN TRY
    IF CONVERT(INT,SERVERPROPERTY('EngineEdition'))<>4
       AND NOT EXISTS (SELECT 1 FROM dbo.sysjobs WHERE name=N'QL_PhongGym - Cập nhật gói hết hạn')
    BEGIN
        EXEC dbo.sp_add_job
            @job_name=N'QL_PhongGym - Cập nhật gói hết hạn',
            @enabled=1,
            @description=N'Cập nhật DangKyGoi từ Còn hạn sang Hết hạn mỗi ngày.';
        EXEC dbo.sp_add_jobstep
            @job_name=N'QL_PhongGym - Cập nhật gói hết hạn',
            @step_name=N'Cập nhật trạng thái gói',
            @subsystem=N'TSQL',
            @database_name=N'QL_PhongGym',
            @command=N'DECLARE @SoLuong INT; EXEC dbo.sp_CapNhatGoiHetHan @SoLuongCapNhat=@SoLuong OUTPUT;';
        EXEC dbo.sp_add_schedule
            @schedule_name=N'QL_PhongGym - Hằng ngày 00:05',
            @freq_type=4,
            @freq_interval=1,
            @active_start_time=500;
        EXEC dbo.sp_attach_schedule
            @job_name=N'QL_PhongGym - Cập nhật gói hết hạn',
            @schedule_name=N'QL_PhongGym - Hằng ngày 00:05';
        EXEC dbo.sp_add_jobserver
            @job_name=N'QL_PhongGym - Cập nhật gói hết hạn';
    END;
END TRY
BEGIN CATCH
    PRINT N'Không tạo được SQL Agent Job; ứng dụng vẫn tự cập nhật khi mở Dashboard: '+ERROR_MESSAGE();
END CATCH;
GO
