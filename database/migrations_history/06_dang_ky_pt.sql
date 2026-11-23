/* Chay sau 01_upgrade_ql_phonggym_v3.sql. */
USE QL_PhongGym;
GO
CREATE OR ALTER PROCEDURE dbo.sp_DangKyPT
    @MaTV INT, @MaHLV INT, @SoBuoi INT, @MaKM INT = NULL,
    @NgayBatDau DATE = NULL, @NgayKetThuc DATE = NULL, @MaNV INT,
    @MaDKPT INT OUTPUT, @MaHD INT OUTPUT
AS
BEGIN TRY
    SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
    IF @SoBuoi <= 0 THROW 51030,N'Số buổi PT phải lớn hơn 0.',1;
    IF NOT EXISTS(SELECT 1 FROM dbo.ThanhVien WHERE MaTV=@MaTV AND TrangThai=N'Hoạt động') THROW 51031,N'Thành viên không hoạt động.',1;
    DECLARE @Phi DECIMAL(18,2), @Giam DECIMAL(5,2)=0;
    SELECT @Phi=PhiThue FROM dbo.HuanLuyenVien WHERE MaHLV=@MaHLV;
    IF @Phi IS NULL THROW 51032,N'Huấn luyện viên không hợp lệ.',1;
    IF @MaKM IS NOT NULL SELECT @Giam=PhanTramGiam FROM dbo.KhuyenMai WHERE MaKM=@MaKM AND TrangThai=N'Hoạt động' AND CAST(GETDATE() AS DATE) BETWEEN NgayBatDau AND NgayKetThuc;
    IF @MaKM IS NOT NULL AND @Giam IS NULL THROW 51033,N'Khuyến mãi không còn hiệu lực.',1;
    SET @NgayBatDau=ISNULL(@NgayBatDau,CAST(GETDATE() AS DATE));
    SET @NgayKetThuc=ISNULL(@NgayKetThuc,DATEADD(DAY,@SoBuoi*7,@NgayBatDau));
    INSERT dbo.DangKyPT(MaTV,MaHLV,MaKM,SoBuoi,NgayBatDau,NgayKetThuc,TongTien,TrangThai)
    VALUES(@MaTV,@MaHLV,@MaKM,@SoBuoi,@NgayBatDau,@NgayKetThuc,ROUND(@Phi*@SoBuoi*(1-@Giam/100),2),N'Đang học');
    SET @MaDKPT=SCOPE_IDENTITY();
    INSERT dbo.HoaDon(MaTV,MaNV,TongTien,TrangThai) VALUES(@MaTV,@MaNV,0,N'Chưa thanh toán'); SET @MaHD=SCOPE_IDENTITY();
    INSERT dbo.ChiTietHoaDon(MaHD,LoaiDichVu,MaThamChieu,SoLuong,DonGia)
    SELECT @MaHD,N'PT',@MaDKPT,SoBuoi,ROUND(TongTien/SoBuoi,2) FROM dbo.DangKyPT WHERE MaDKPT=@MaDKPT;
    INSERT dbo.HoaDonPT(MaCTHD,MaDKPT) SELECT MaCTHD,@MaDKPT FROM dbo.ChiTietHoaDon WHERE MaHD=@MaHD;
    COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
