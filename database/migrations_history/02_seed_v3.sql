/* Du lieu mau bo sung. Chay sau 01_upgrade_ql_phonggym_v3.sql. */
USE QL_PhongGym;
GO
IF NOT EXISTS(SELECT 1 FROM dbo.NhaCungCap WHERE MaNhaCungCap='NCC0001')
INSERT dbo.NhaCungCap(MaNhaCungCap,TenNCC,SDT,Email,DiaChi) VALUES('NCC0001',N'Fitness Supply Việt Nam','0281234567','sales@fitness.vn',N'TP. Hồ Chí Minh');
GO
DECLARE @NCC INT=(SELECT MaNCC FROM dbo.NhaCungCap WHERE MaNhaCungCap='NCC0001');
UPDATE dbo.SanPham SET MaNCC=@NCC,GiaNhap=CASE WHEN GiaNhap=0 THEN DonGia*0.65 ELSE GiaNhap END;
UPDATE dbo.ThietBi SET MaNCC=@NCC,ChuKyBaoTriNgay=ISNULL(ChuKyBaoTriNgay,90),NgayBaoTriTiepTheo=ISNULL(NgayBaoTriTiepTheo,DATEADD(DAY,90,NgayMua));
GO
/* Mat khau 123456 trong script goc la khong an toan. Ung dung phai ghi PBKDF2/bcrypt/Argon2 da salt vao MatKhauHash/MatKhauSalt va bat buoc doi mat khau truoc production. */
