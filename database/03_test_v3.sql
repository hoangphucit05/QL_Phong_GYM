/* Smoke test co rollback: khong lam thay doi du lieu sau khi kiem thu. */
USE QL_PhongGym;
GO
SET XACT_ABORT ON;
BEGIN TRAN;
BEGIN TRY
 DECLARE @MaTV INT,@MaDKG INT,@MaHD INT,@MaTT INT,@MaCheckIn BIGINT,@MaDKL INT,@MaLichPT INT,@MaPNK INT,@MaBT INT,
         @HomNay DATE=CONVERT(DATE,SYSDATETIME()),@LichBD DATETIME2(0)=DATEADD(DAY,30,SYSDATETIME()),@LichKT DATETIME2(0)=DATEADD(MINUTE,60,DATEADD(DAY,30,SYSDATETIME())),@NgayBaoTri DATE=CONVERT(DATE,DATEADD(DAY,7,SYSDATETIME()));
 EXEC dbo.sp_ThemThanhVien @HoTen=N'Test Migration',@NgaySinh='2000-01-01',@GioiTinh=N'Khác',@SDT='0999000000',@Email='migration.test@example.invalid',@DiaChi=N'Hà Nội',@MaLoaiTV=1,@MaTV=@MaTV OUTPUT;
 EXEC dbo.sp_DangKyGoiTap @MaTV=@MaTV,@MaGoi=1,@NgayBatDau=@HomNay,@MaNV=1,@MaDKG=@MaDKG OUTPUT,@MaHD=@MaHD OUTPUT;
 EXEC dbo.sp_ThanhToanHoaDon @MaHD=@MaHD,@SoTien=300000,@PhuongThuc=N'Tiền mặt',@MaNV=1,@MaTT=@MaTT OUTPUT;
 IF NOT EXISTS(SELECT 1 FROM dbo.HoaDon WHERE MaHD=@MaHD AND TrangThai=N'Đã thanh toán') THROW 51001,N'Test thanh toán thất bại.',1;
 EXEC dbo.sp_CheckIn @MaTV=@MaTV,@MaPhong=1,@Kenh=N'Quầy',@MaCheckIn=@MaCheckIn OUTPUT,@MaNV=1; EXEC dbo.sp_CheckOut @MaTV=@MaTV,@MaNV=1;
 EXEC dbo.sp_DangKyLop @MaTV=@MaTV,@MaLop=1,@VaoDanhSachCho=1,@MaDKL=@MaDKL OUTPUT,@MaNV=1;
 EXEC dbo.sp_DatLichPT @MaDKPT=1,@ThoiGianBatDau=@LichBD,@ThoiGianKetThuc=@LichKT,@MaLichPT=@MaLichPT OUTPUT;
 EXEC dbo.sp_HoanThanhBuoiPT @MaLichPT=@MaLichPT,@NhanXet=N'Đạt mục tiêu buổi test';
 EXEC dbo.sp_NhapKho @MaNCC=1,@MaNV=1,@MaSP=1,@SoLuong=2,@DonGiaNhap=500000,@MaPNK=@MaPNK OUTPUT;
 EXEC dbo.sp_XuatKho @MaSP=1,@SoLuong=1,@MaNV=1,@Nguon=N'Test',@MaThamChieu=@MaPNK;
 EXEC dbo.sp_BaoTriThietBi @MaTB=1,@MaNV=13,@NoiDung=N'Kiểm thử lịch bảo trì',@ChiPhi=0,@NgayDuKien=@NgayBaoTri,@MaBaoTri=@MaBT OUTPUT;
 SELECT N'PASS' AS KetQua,@MaTV MaThanhVien,@MaHD HoaDon,@MaCheckIn CheckIn,@MaPNK PhieuNhap;
 ROLLBACK;
END TRY
BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
