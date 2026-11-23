/* Chay sau 01_upgrade_ql_phonggym_v3.sql. Mo rong vai tro ma khong doi ten bang/cu. */
USE QL_PhongGym;
GO
DECLARE @sql NVARCHAR(MAX)=N'';
SELECT @sql=@sql+N'ALTER TABLE dbo.TaiKhoan DROP CONSTRAINT '+QUOTENAME(name)+N';'
FROM sys.check_constraints
WHERE parent_object_id=OBJECT_ID(N'dbo.TaiKhoan') AND definition LIKE N'%VaiTro%';
IF @sql<>N'' EXEC sys.sp_executesql @sql;
GO
ALTER TABLE dbo.TaiKhoan ADD CONSTRAINT CK_TaiKhoan_VaiTro_v3 CHECK
    (VaiTro IN(N'Admin',N'Quản lý',N'Lễ tân',N'Kế toán',N'Huấn luyện viên',N'Nhân viên bán hàng',N'Nhân viên',N'HLV'));
GO
INSERT dbo.VaiTro(TenVaiTro,MoTa)
SELECT v.TenVaiTro,v.MoTa FROM (VALUES
 (N'Admin',N'Quản trị toàn hệ thống'),(N'Quản lý',N'Quản lý vận hành'),(N'Lễ tân',N'Tiếp nhận thành viên'),
 (N'Kế toán',N'Quản lý tài chính'),(N'Huấn luyện viên',N'Đào tạo PT'),(N'Nhân viên bán hàng',N'Bán sản phẩm'),
 (N'Nhân viên',N'Nhân viên vận hành'),(N'HLV',N'Huấn luyện viên legacy'))v(TenVaiTro,MoTa)
WHERE NOT EXISTS(SELECT 1 FROM dbo.VaiTro r WHERE r.TenVaiTro=v.TenVaiTro);
GO
INSERT dbo.TaiKhoanVaiTro(MaTK,MaVaiTro)
SELECT tk.MaTK,vt.MaVaiTro FROM dbo.TaiKhoan tk JOIN dbo.VaiTro vt ON vt.TenVaiTro=tk.VaiTro
WHERE NOT EXISTS(SELECT 1 FROM dbo.TaiKhoanVaiTro x WHERE x.MaTK=tk.MaTK AND x.MaVaiTro=vt.MaVaiTro);
GO
