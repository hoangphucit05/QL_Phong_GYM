/*
   Migration 29 - Bổ sung ràng buộc dữ liệu & validation trong stored procedure
   Khắc phục các lỗ hổng từ báo cáo phân tích ràng buộc dữ liệu.
   Tất cả thay đổi đều idempotent (IF NOT EXISTS / IF COL_LENGTH).
   Yêu cầu: SQL Server 2017+
*/
USE QL_PhongGym;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
SET XACT_ABORT ON;
GO

PRINT N'=== Migration 29: Bổ sung ràng buộc dữ liệu & validation ===';
GO

-- ========================================================================================
-- PHẦN 1: BỔ SUNG NOT NULL VỚI DEFAULT (Ưu tiên 🔴 Cao)
-- ========================================================================================

-- 1.1 ThanhVien.GioiTinh: NOT NULL (default N'Khác' cho dữ liệu cũ)
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID('dbo.ThanhVien') AND name=N'GioiTinh' AND is_nullable=1)
BEGIN
    UPDATE dbo.ThanhVien SET GioiTinh=N'Khác' WHERE GioiTinh IS NULL;
    ALTER TABLE dbo.ThanhVien ALTER COLUMN GioiTinh NVARCHAR(10) NOT NULL;
    PRINT N'  [OK] ThanhVien.GioiTinh -> NOT NULL';
END;
GO

-- 1.2 NhanVien.GioiTinh: NOT NULL
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID('dbo.NhanVien') AND name=N'GioiTinh' AND is_nullable=1)
BEGIN
    UPDATE dbo.NhanVien SET GioiTinh=N'Khác' WHERE GioiTinh IS NULL;
    ALTER TABLE dbo.NhanVien ALTER COLUMN GioiTinh NVARCHAR(10) NOT NULL;
    PRINT N'  [OK] NhanVien.GioiTinh -> NOT NULL';
END;
GO

-- 1.3 NhanVien.Luong: NOT NULL DEFAULT 0
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID('dbo.NhanVien') AND name=N'Luong' AND is_nullable=1)
BEGIN
    UPDATE dbo.NhanVien SET Luong=0 WHERE Luong IS NULL;
    ALTER TABLE dbo.NhanVien ALTER COLUMN Luong DECIMAL(18,2) NOT NULL;
    PRINT N'  [OK] NhanVien.Luong -> NOT NULL';
END;
IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE parent_object_id=OBJECT_ID('dbo.NhanVien') AND name='DF_NhanVien_Luong')
    ALTER TABLE dbo.NhanVien ADD CONSTRAINT DF_NhanVien_Luong DEFAULT 0 FOR Luong;
GO

-- 1.4 HuanLuyenVien.KinhNghiem: NOT NULL DEFAULT 0
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID('dbo.HuanLuyenVien') AND name=N'KinhNghiem' AND is_nullable=1)
BEGIN
    UPDATE dbo.HuanLuyenVien SET KinhNghiem=0 WHERE KinhNghiem IS NULL;
    ALTER TABLE dbo.HuanLuyenVien ALTER COLUMN KinhNghiem INT NOT NULL;
    PRINT N'  [OK] HuanLuyenVien.KinhNghiem -> NOT NULL';
END;
IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE parent_object_id=OBJECT_ID('dbo.HuanLuyenVien') AND name='DF_HuanLuyenVien_KinhNghiem')
    ALTER TABLE dbo.HuanLuyenVien ADD CONSTRAINT DF_HuanLuyenVien_KinhNghiem DEFAULT 0 FOR KinhNghiem;
GO

-- 1.5 HuanLuyenVien.PhiThue: NOT NULL DEFAULT 0
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID('dbo.HuanLuyenVien') AND name=N'PhiThue' AND is_nullable=1)
BEGIN
    UPDATE dbo.HuanLuyenVien SET PhiThue=0 WHERE PhiThue IS NULL;
    ALTER TABLE dbo.HuanLuyenVien ALTER COLUMN PhiThue DECIMAL(18,2) NOT NULL;
    PRINT N'  [OK] HuanLuyenVien.PhiThue -> NOT NULL';
END;
IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE parent_object_id=OBJECT_ID('dbo.HuanLuyenVien') AND name='DF_HuanLuyenVien_PhiThue')
    ALTER TABLE dbo.HuanLuyenVien ADD CONSTRAINT DF_HuanLuyenVien_PhiThue DEFAULT 0 FOR PhiThue;
GO

-- ========================================================================================
-- PHẦN 2: BỔ SUNG CHECK CONSTRAINTS (Ưu tiên 🔴 Cao + 🟡 Trung bình)
-- ========================================================================================

-- 2.1 ThanhVien.HoTen: Ít nhất 2 ký tự (không tính khoảng trắng)
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_ThanhVien_HoTen_v4')
    ALTER TABLE dbo.ThanhVien ADD CONSTRAINT CK_ThanhVien_HoTen_v4
        CHECK (LEN(LTRIM(RTRIM(HoTen))) >= 2);
GO

-- 2.2 NhanVien.HoTen: Ít nhất 2 ký tự
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_NhanVien_HoTen_v4')
    ALTER TABLE dbo.NhanVien ADD CONSTRAINT CK_NhanVien_HoTen_v4
        CHECK (LEN(LTRIM(RTRIM(HoTen))) >= 2);
GO

-- 2.3 BaoTriThietBi.TrangThai: Chỉ cho phép giá trị v3
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_BaoTriThietBi_TrangThai_v4')
    ALTER TABLE dbo.BaoTriThietBi ADD CONSTRAINT CK_BaoTriThietBi_TrangThai_v4
        CHECK (TrangThai IN (N'Đang bảo trì', N'Chờ bảo trì', N'Hoàn thành'));
GO

-- 2.4 GoiTap.TenGoi: Không rỗng
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_GoiTap_TenGoi_v4')
    ALTER TABLE dbo.GoiTap ADD CONSTRAINT CK_GoiTap_TenGoi_v4
        CHECK (LEN(LTRIM(RTRIM(TenGoi))) > 0);
GO

-- 2.5 SanPham.TenSP: Không rỗng
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_SanPham_TenSP_v4')
    ALTER TABLE dbo.SanPham ADD CONSTRAINT CK_SanPham_TenSP_v4
        CHECK (LEN(LTRIM(RTRIM(TenSP))) > 0);
GO

-- 2.6 PhongTap.Tang >= 1
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_PhongTap_Tang_v4')
    ALTER TABLE dbo.PhongTap ADD CONSTRAINT CK_PhongTap_Tang_v4
        CHECK (Tang >= 1);
GO

-- 2.7 TaiKhoan.TenDangNhap >= 3 ký tự
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_TaiKhoan_TenDangNhap_v4')
    ALTER TABLE dbo.TaiKhoan ADD CONSTRAINT CK_TaiKhoan_TenDangNhap_v4
        CHECK (LEN(TenDangNhap) >= 3);
GO

-- 2.8 ThanhVien.GioiTinh CHECK (đảm bảo tồn tại sau khi ALTER COLUMN)
-- Xóa CHECK cũ nếu có (vì ALTER COLUMN có thể drop nó)
DECLARE @ck_gt NVARCHAR(200) = N'';
SELECT @ck_gt = @ck_gt + N'ALTER TABLE dbo.ThanhVien DROP CONSTRAINT ' + QUOTENAME(name) + N'; '
FROM sys.check_constraints
WHERE parent_object_id = OBJECT_ID('dbo.ThanhVien')
  AND OBJECT_DEFINITION(object_id) LIKE '%GioiTinh%'
  AND name <> N'CK_ThanhVien_GioiTinh_v4';
IF @ck_gt <> N'' EXEC sp_executesql @ck_gt;
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_ThanhVien_GioiTinh_v4')
    ALTER TABLE dbo.ThanhVien ADD CONSTRAINT CK_ThanhVien_GioiTinh_v4
        CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác'));
GO

-- 2.9 NhanVien.GioiTinh CHECK
DECLARE @ck_gt_nv NVARCHAR(200) = N'';
SELECT @ck_gt_nv = @ck_gt_nv + N'ALTER TABLE dbo.NhanVien DROP CONSTRAINT ' + QUOTENAME(name) + N'; '
FROM sys.check_constraints
WHERE parent_object_id = OBJECT_ID('dbo.NhanVien')
  AND OBJECT_DEFINITION(object_id) LIKE '%GioiTinh%'
  AND name <> N'CK_NhanVien_GioiTinh_v4';
IF @ck_gt_nv <> N'' EXEC sp_executesql @ck_gt_nv;
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_NhanVien_GioiTinh_v4')
    ALTER TABLE dbo.NhanVien ADD CONSTRAINT CK_NhanVien_GioiTinh_v4
        CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác'));
GO

PRINT N'  [OK] Tất cả CHECK constraints đã được thêm.';
GO

-- ========================================================================================
-- PHẦN 3: NÂNG CẤP STORED PROCEDURES VỚI VALIDATION & TRANSACTION
-- ========================================================================================

-- 3.1 sp_ThemThanhVien: Thêm validation rõ ràng + TRANSACTION
CREATE OR ALTER PROCEDURE dbo.sp_ThemThanhVien
    @HoTen     NVARCHAR(100),
    @NgaySinh  DATE,
    @GioiTinh  NVARCHAR(10),
    @SDT       VARCHAR(15),
    @Email     VARCHAR(100) = NULL,
    @DiaChi    NVARCHAR(255) = NULL,
    @MaLoaiTV  INT,
    @MaTV      INT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;

        -- Validation đầu vào với thông báo tiếng Việt thân thiện
        IF NULLIF(LTRIM(RTRIM(@HoTen)), N'') IS NULL OR LEN(LTRIM(RTRIM(@HoTen))) < 2
            THROW 51101, N'Họ tên phải có ít nhất 2 ký tự.', 1;

        IF @NgaySinh >= CAST(SYSDATETIME() AS DATE)
            THROW 51102, N'Ngày sinh phải nhỏ hơn ngày hiện tại.', 1;

        IF @GioiTinh NOT IN (N'Nam', N'Nữ', N'Khác')
            THROW 51103, N'Giới tính phải là Nam, Nữ hoặc Khác.', 1;

        IF @SDT IS NULL OR @SDT NOT LIKE '0%' OR @SDT LIKE '%[^0-9]%' OR LEN(@SDT) NOT BETWEEN 10 AND 11
            THROW 51104, N'Số điện thoại phải gồm 10-11 chữ số và bắt đầu bằng 0.', 1;

        IF @Email IS NOT NULL AND (@Email LIKE '% %' OR @Email NOT LIKE '%_@_%._%')
            THROW 51105, N'Email không đúng định dạng (ví dụ: ten@domain.com).', 1;

        IF NOT EXISTS(SELECT 1 FROM dbo.LoaiThanhVien WHERE MaLoaiTV=@MaLoaiTV)
            THROW 51106, N'Loại thành viên không tồn tại.', 1;

        BEGIN TRAN;

        IF EXISTS(SELECT 1 FROM dbo.ThanhVien WITH(UPDLOCK,HOLDLOCK) WHERE SDT=@SDT AND DaXoa=0)
            THROW 51107, N'Số điện thoại này đã được đăng ký cho thành viên khác.', 1;

        INSERT dbo.ThanhVien(HoTen, NgaySinh, GioiTinh, SDT, Email, DiaChi, MaLoaiTV)
        VALUES(LTRIM(RTRIM(@HoTen)), @NgaySinh, @GioiTinh, @SDT, @Email, @DiaChi, @MaLoaiTV);
        SET @MaTV = SCOPE_IDENTITY();

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

-- 3.2 sp_SuaThanhVien: Thêm validation + TRANSACTION
CREATE OR ALTER PROCEDURE dbo.sp_SuaThanhVien
    @MaTV     INT,
    @HoTen    NVARCHAR(100),
    @SDT      VARCHAR(15),
    @Email    VARCHAR(100) = NULL,
    @DiaChi   NVARCHAR(255) = NULL,
    @MaLoaiTV INT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;

        -- Validation đầu vào
        IF NULLIF(LTRIM(RTRIM(@HoTen)), N'') IS NULL OR LEN(LTRIM(RTRIM(@HoTen))) < 2
            THROW 51101, N'Họ tên phải có ít nhất 2 ký tự.', 1;

        IF @SDT IS NULL OR @SDT NOT LIKE '0%' OR @SDT LIKE '%[^0-9]%' OR LEN(@SDT) NOT BETWEEN 10 AND 11
            THROW 51104, N'Số điện thoại phải gồm 10-11 chữ số và bắt đầu bằng 0.', 1;

        IF @Email IS NOT NULL AND (@Email LIKE '% %' OR @Email NOT LIKE '%_@_%._%')
            THROW 51105, N'Email không đúng định dạng (ví dụ: ten@domain.com).', 1;

        IF NOT EXISTS(SELECT 1 FROM dbo.LoaiThanhVien WHERE MaLoaiTV=@MaLoaiTV)
            THROW 51106, N'Loại thành viên không tồn tại.', 1;

        BEGIN TRAN;

        IF EXISTS(SELECT 1 FROM dbo.ThanhVien WITH(UPDLOCK,HOLDLOCK) WHERE SDT=@SDT AND MaTV<>@MaTV AND DaXoa=0)
            THROW 51107, N'Số điện thoại này đã được đăng ký cho thành viên khác.', 1;

        UPDATE dbo.ThanhVien
        SET HoTen=LTRIM(RTRIM(@HoTen)), SDT=@SDT, Email=@Email, DiaChi=@DiaChi, MaLoaiTV=@MaLoaiTV, NgayCapNhat=SYSDATETIME()
        WHERE MaTV=@MaTV AND DaXoa=0;

        IF @@ROWCOUNT = 0
            THROW 50001, N'Thành viên không tồn tại hoặc đã xóa mềm.', 1;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

-- 3.3 sp_KhoaThanhVien: Bọc TRANSACTION
CREATE OR ALTER PROCEDURE dbo.sp_KhoaThanhVien
    @MaTV  INT,
    @LyDo  NVARCHAR(255),
    @MaNV  INT = NULL
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;

        IF NULLIF(LTRIM(RTRIM(@LyDo)), N'') IS NULL
            THROW 51108, N'Lý do khóa thành viên không được để trống.', 1;

        BEGIN TRAN;

        UPDATE dbo.ThanhVien
        SET TrangThai=N'Tạm khóa', LyDoKhoa=LTRIM(RTRIM(@LyDo)), NgayCapNhat=SYSDATETIME()
        WHERE MaTV=@MaTV AND DaXoa=0;

        IF @@ROWCOUNT = 0
            THROW 50002, N'Không tìm thấy thành viên.', 1;

        INSERT dbo.NhatKyHeThong(MaNV, HanhDong, DoiTuong, KhoaDoiTuong, NoiDung)
        VALUES(@MaNV, N'Khóa thành viên', N'ThanhVien', CONVERT(NVARCHAR(30), @MaTV), LTRIM(RTRIM(@LyDo)));

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

-- 3.4 sp_DangKyGoiTap: Kiểm tra trùng gói cùng loại đang hoạt động
CREATE OR ALTER PROCEDURE dbo.sp_DangKyGoiTap
    @MaTV INT, @MaGoi INT, @MaKM INT = NULL, @NgayBatDau DATE = NULL,
    @MaNV INT, @MaDKG INT OUTPUT, @MaHD INT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        IF NOT EXISTS(SELECT 1 FROM dbo.ThanhVien WHERE MaTV=@MaTV AND TrangThai=N'Hoạt động' AND DaXoa=0)
            THROW 50003, N'Thành viên không hoạt động.', 1;

        DECLARE @Gia DECIMAL(18,2), @Han INT, @Giam DECIMAL(5,2);
        SELECT @Gia=Gia, @Han=ThoiHanNgay FROM dbo.GoiTap WHERE MaGoi=@MaGoi AND TrangThai=N'Đang áp dụng';
        IF @Gia IS NULL THROW 50004, N'Gói tập không hợp lệ hoặc đã ngừng áp dụng.', 1;

        -- [MỚI] Kiểm tra trùng gói cùng loại đang hoạt động
        IF EXISTS(
            SELECT 1 FROM dbo.DangKyGoi WITH(UPDLOCK,HOLDLOCK)
            WHERE MaTV=@MaTV AND MaGoi=@MaGoi AND TrangThai IN(N'Còn hạn', N'Chờ thanh toán')
        )
            THROW 51109, N'Thành viên đã có gói tập cùng loại đang hoạt động. Vui lòng gia hạn thay vì đăng ký mới.', 1;

        IF @MaKM IS NOT NULL
            SELECT @Giam=PhanTramGiam FROM dbo.KhuyenMai
            WHERE MaKM=@MaKM AND TrangThai=N'Hoạt động' AND CAST(SYSDATETIME() AS DATE) BETWEEN NgayBatDau AND NgayKetThuc;
        IF @MaKM IS NOT NULL AND @Giam IS NULL THROW 50005, N'Khuyến mãi không còn hiệu lực.', 1;
        SET @Giam = ISNULL(@Giam, 0);
        SET @NgayBatDau = ISNULL(@NgayBatDau, CAST(SYSDATETIME() AS DATE));

        INSERT dbo.DangKyGoi(MaTV, MaGoi, MaKM, NgayBatDau, NgayKetThuc, GiaSauGiam, TrangThai)
        VALUES(@MaTV, @MaGoi, @MaKM, @NgayBatDau, DATEADD(DAY, @Han-1, @NgayBatDau),
               ROUND(@Gia*(1-@Giam/100), 2), N'Chờ thanh toán');
        SET @MaDKG = SCOPE_IDENTITY();

        INSERT dbo.HoaDon(MaTV, MaNV, TongTien, TrangThai) VALUES(@MaTV, @MaNV, 0, N'Chưa thanh toán');
        SET @MaHD = SCOPE_IDENTITY();

        INSERT dbo.ChiTietHoaDon(MaHD, LoaiDichVu, MaThamChieu, SoLuong, DonGia)
        SELECT @MaHD, N'Gói tập', @MaDKG, 1, GiaSauGiam FROM dbo.DangKyGoi WHERE MaDKG=@MaDKG;

        INSERT dbo.HoaDonGoiTap(MaCTHD, MaDKG)
        SELECT MaCTHD, @MaDKG FROM dbo.ChiTietHoaDon WHERE MaHD=@MaHD;

        INSERT dbo.LichSuGoiTap(MaDKG, HanhDong, TuNgay, DenNgay, MaNV)
        SELECT @MaDKG, N'Đăng ký', NgayBatDau, NgayKetThuc, @MaNV FROM dbo.DangKyGoi WHERE MaDKG=@MaDKG;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

-- 3.5 sp_GiaHanGoiTap: Giới hạn @SoNgay tối đa 365 ngày
CREATE OR ALTER PROCEDURE dbo.sp_GiaHanGoiTap
    @MaDKG INT,
    @SoNgay INT,
    @MaNV INT = NULL,
    @MaHD INT = NULL OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV, N'Admin|Quản lý|Lễ tân|Nhân viên') = 0
            THROW 52109, N'Tài khoản thực hiện gia hạn không có quyền hoặc không còn hoạt động.', 1;

        IF @SoNgay <= 0
            THROW 50006, N'Số ngày gia hạn phải lớn hơn 0.', 1;

        -- [MỚI] Giới hạn số ngày gia hạn tối đa
        IF @SoNgay > 365
            THROW 51110, N'Số ngày gia hạn không được vượt quá 365 ngày. Vui lòng đăng ký gói mới nếu cần thời gian dài hơn.', 1;

        DECLARE @MaTV INT, @MaGoi INT, @Cu DATE, @GiaGoi DECIMAL(18,2), @ThoiHanGoi INT;
        SELECT @MaTV=d.MaTV, @MaGoi=d.MaGoi, @Cu=d.NgayKetThuc, @GiaGoi=g.Gia, @ThoiHanGoi=g.ThoiHanNgay
        FROM dbo.DangKyGoi d WITH(UPDLOCK,HOLDLOCK)
        JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi
        WHERE d.MaDKG=@MaDKG AND d.TrangThai IN(N'Còn hạn', N'Hết hạn');

        IF @Cu IS NULL
            THROW 50007, N'Gói tập không tồn tại hoặc không ở trạng thái có thể gia hạn.', 1;

        DECLARE @DonGiaNgay DECIMAL(18,2) = ROUND(@GiaGoi / CASE WHEN ISNULL(@ThoiHanGoi,0)>0 THEN @ThoiHanGoi ELSE 30 END, 0);
        DECLARE @PhiGiaHan DECIMAL(18,2) = @DonGiaNgay * @SoNgay;

        INSERT dbo.HoaDon(MaTV, MaNV, TongTien, TrangThai) VALUES(@MaTV, @MaNV, 0, N'Chưa thanh toán');
        SET @MaHD = SCOPE_IDENTITY();

        INSERT dbo.ChiTietHoaDon(MaHD, LoaiDichVu, MaThamChieu, SoLuong, DonGia)
        VALUES(@MaHD, N'Gói tập', @MaDKG, @SoNgay, @PhiGiaHan);

        INSERT dbo.HoaDonGoiTap(MaCTHD, MaDKG)
        SELECT MaCTHD, @MaDKG FROM dbo.ChiTietHoaDon WHERE MaHD=@MaHD;

        DECLARE @Moc DATE = CASE WHEN @Cu < CAST(SYSDATETIME() AS DATE) THEN CAST(SYSDATETIME() AS DATE) ELSE @Cu END;
        DECLARE @Moi DATE = DATEADD(DAY, @SoNgay, @Moc);

        UPDATE dbo.DangKyGoi SET NgayKetThuc=@Moi, TrangThai=N'Còn hạn', NgayCapNhat=SYSDATETIME()
        WHERE MaDKG=@MaDKG;

        INSERT dbo.LichSuGoiTap(MaDKG, HanhDong, TuNgay, DenNgay, MaNV)
        VALUES(@MaDKG, N'Gia hạn', @Cu, @Moi, @MaNV);

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

-- 3.6 sp_BanSanPham: Kiểm tra @MaNV hoạt động
CREATE OR ALTER PROCEDURE dbo.sp_BanSanPham
    @MaTV INT, @MaNV INT, @MaSP INT, @SoLuong INT, @MaHD INT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        DECLARE @Gia DECIMAL(18,2), @Ton INT, @MaCT INT;

        SELECT @Gia=DonGia, @Ton=SoLuongTon FROM dbo.SanPham WITH(UPDLOCK,HOLDLOCK)
        WHERE MaSP=@MaSP AND TrangThai=N'Còn bán';
        IF @Gia IS NULL OR @Ton<@SoLuong OR @SoLuong<=0
            THROW 50026, N'Sản phẩm không hợp lệ hoặc không đủ tồn kho.', 1;

        IF NOT EXISTS(SELECT 1 FROM dbo.ThanhVien WHERE MaTV=@MaTV AND DaXoa=0)
            THROW 50036, N'Thành viên không tồn tại.', 1;

        -- [MỚI] Kiểm tra nhân viên đang hoạt động
        IF NOT EXISTS(SELECT 1 FROM dbo.NhanVien WHERE MaNV=@MaNV AND TrangThai=N'Đang làm')
            THROW 51111, N'Nhân viên không hoạt động hoặc không tồn tại.', 1;

        INSERT dbo.HoaDon(MaTV, MaNV, TongTien, TrangThai) VALUES(@MaTV, @MaNV, 0, N'Chưa thanh toán');
        SET @MaHD = SCOPE_IDENTITY();

        INSERT dbo.ChiTietHoaDon(MaHD, LoaiDichVu, MaThamChieu, SoLuong, DonGia)
        VALUES(@MaHD, N'Sản phẩm', @MaSP, @SoLuong, @Gia);
        SET @MaCT = SCOPE_IDENTITY();

        INSERT dbo.HoaDonSanPham(MaCTHD, MaSP) VALUES(@MaCT, @MaSP);

        UPDATE dbo.SanPham SET SoLuongTon=SoLuongTon-@SoLuong, NgayCapNhat=SYSDATETIME() WHERE MaSP=@MaSP;

        INSERT dbo.LichSuTonKho(MaSP, LoaiBienDong, SoLuong, TonTruoc, TonSau, NguonThamChieu, MaThamChieu, MaNV)
        VALUES(@MaSP, N'Xuất', @SoLuong, @Ton, @Ton-@SoLuong, N'Hóa đơn', @MaHD, @MaNV);

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

-- 3.7 sp_HuyDangKyLop: Kiểm tra đã có điểm danh chưa
CREATE OR ALTER PROCEDURE dbo.sp_HuyDangKyLop @MaDKL INT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        -- [MỚI] Kiểm tra đã có điểm danh chưa
        IF EXISTS(SELECT 1 FROM dbo.DiemDanhLop WHERE MaDKL=@MaDKL AND TrangThai=N'Có mặt')
            THROW 51112, N'Không thể hủy đăng ký lớp vì học viên đã có buổi điểm danh. Vui lòng liên hệ quản lý.', 1;

        UPDATE dbo.DangKyLop SET TrangThai=N'Đã hủy' WHERE MaDKL=@MaDKL AND TrangThai=N'Đang tham gia';
        IF @@ROWCOUNT = 0 THROW 50015, N'Không thể hủy đăng ký lớp.', 1;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

-- 3.8 sp_HoanTatBaoTri: Chỉ cho phép từ trạng thái 'Đang bảo trì'
CREATE OR ALTER PROCEDURE dbo.sp_HoanTatBaoTri
    @MaBaoTri INT,
    @MaNV INT,
    @GhiChu NVARCHAR(255) = NULL
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV, N'Admin|Quản lý|Nhân viên') = 0
            THROW 52111, N'Tài khoản thực hiện hoàn tất bảo trì không có quyền hoặc không còn hoạt động.', 1;

        DECLARE @MaTB INT, @TrangThaiHienTai NVARCHAR(50);
        SELECT @MaTB=MaTB, @TrangThaiHienTai=TrangThai
        FROM dbo.BaoTriThietBi WITH(UPDLOCK,HOLDLOCK) WHERE MaBaoTri=@MaBaoTri;

        IF @MaTB IS NULL
            THROW 50040, N'Phiếu bảo trì không tồn tại.', 1;

        IF @TrangThaiHienTai = N'Hoàn thành'
            THROW 50041, N'Phiếu bảo trì này đã hoàn thành trước đó.', 1;

        -- [MỚI] Chỉ cho phép hoàn tất từ trạng thái 'Đang bảo trì' (không phải 'Chờ bảo trì')
        IF @TrangThaiHienTai <> N'Đang bảo trì'
            THROW 51113, N'Chỉ có thể hoàn tất phiếu bảo trì đang ở trạng thái "Đang bảo trì". Phiếu hiện đang "' + @TrangThaiHienTai + N'".', 1;

        UPDATE dbo.BaoTriThietBi
        SET TrangThai=N'Hoàn thành', NgayHoanThanh=CAST(SYSDATETIME() AS DATE), GhiChu=ISNULL(@GhiChu, GhiChu)
        WHERE MaBaoTri=@MaBaoTri;

        IF NOT EXISTS (
            SELECT 1 FROM dbo.BaoTriThietBi
            WHERE MaTB=@MaTB AND MaBaoTri<>@MaBaoTri AND TrangThai IN(N'Đang bảo trì', N'Chờ bảo trì')
        )
        BEGIN
            UPDATE dbo.ThietBi SET TinhTrang=N'Hoạt động' WHERE MaTB=@MaTB;
        END;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

-- 3.9 sp_NhapKho: Validate @MaNCC tồn tại
CREATE OR ALTER PROCEDURE dbo.sp_NhapKho
    @MaNCC INT, @MaNV INT, @MaSP INT, @SoLuong INT, @DonGiaNhap DECIMAL(18,2), @MaPNK INT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        IF @SoLuong<=0 OR @DonGiaNhap<0
            THROW 50023, N'Dữ liệu nhập kho không hợp lệ.', 1;

        -- [MỚI] Validate NCC tồn tại
        IF NOT EXISTS(SELECT 1 FROM dbo.NhaCungCap WHERE MaNCC=@MaNCC)
            THROW 51114, N'Nhà cung cấp không tồn tại.', 1;

        DECLARE @Ton INT;
        SELECT @Ton=SoLuongTon FROM dbo.SanPham WITH(UPDLOCK,HOLDLOCK) WHERE MaSP=@MaSP;
        IF @Ton IS NULL THROW 50024, N'Sản phẩm không tồn tại.', 1;

        INSERT dbo.PhieuNhapKho(MaNCC, MaNV, TongTien) VALUES(@MaNCC, @MaNV, 0);
        SET @MaPNK = SCOPE_IDENTITY();

        INSERT dbo.ChiTietPhieuNhap(MaPNK, MaSP, SoLuong, DonGiaNhap) VALUES(@MaPNK, @MaSP, @SoLuong, @DonGiaNhap);

        UPDATE dbo.PhieuNhapKho SET TongTien=(SELECT SUM(ThanhTien) FROM dbo.ChiTietPhieuNhap WHERE MaPNK=@MaPNK) WHERE MaPNK=@MaPNK;

        UPDATE dbo.SanPham SET SoLuongTon=SoLuongTon+@SoLuong, GiaNhap=@DonGiaNhap, MaNCC=@MaNCC, NgayCapNhat=SYSDATETIME() WHERE MaSP=@MaSP;

        INSERT dbo.LichSuTonKho(MaSP, LoaiBienDong, SoLuong, TonTruoc, TonSau, NguonThamChieu, MaThamChieu, MaNV)
        VALUES(@MaSP, N'Nhập', @SoLuong, @Ton, @Ton+@SoLuong, N'Phiếu nhập', @MaPNK, @MaNV);

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

PRINT N'=== Migration 29: Hoàn tất! ===';
GO
