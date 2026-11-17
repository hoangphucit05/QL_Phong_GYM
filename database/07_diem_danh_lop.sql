/* Chay sau 01_upgrade_ql_phonggym_v3.sql. */
USE QL_PhongGym;
GO
CREATE OR ALTER PROCEDURE dbo.sp_DiemDanhLop
 @MaDKL INT,@NgayDiemDanh DATE,@TrangThai NVARCHAR(30)
AS
BEGIN TRY
 SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
 IF @TrangThai NOT IN(N'Có mặt',N'Vắng mặt') THROW 51050,N'Trạng thái điểm danh không hợp lệ.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.DangKyLop WHERE MaDKL=@MaDKL AND TrangThai=N'Đang tham gia') THROW 51051,N'Đăng ký lớp không hợp lệ.',1;
 UPDATE dbo.DiemDanhLop SET TrangThai=@TrangThai WHERE MaDKL=@MaDKL AND NgayDiemDanh=@NgayDiemDanh;
 IF @@ROWCOUNT=0 INSERT dbo.DiemDanhLop(MaDKL,NgayDiemDanh,TrangThai) VALUES(@MaDKL,@NgayDiemDanh,@TrangThai);
 COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
