/*
  QL_PhongGym v3 - migration an toan, chay SAU htql_gym_v2.sql.
  Khong DROP bang/du lieu. Chay mot lan tren ban sao da backup truoc khi len production.
  Yeu cau SQL Server 2017+ (CREATE OR ALTER, STRING_AGG khong duoc su dung).
*/
USE QL_PhongGym;
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* 1. Mo rong cac bang dang co, giu nguyen ten va khoa cu. */
IF COL_LENGTH(N'dbo.ThanhVien', N'MaThanhVien') IS NULL
    ALTER TABLE dbo.ThanhVien ADD MaThanhVien VARCHAR(16) NULL, AnhDaiDien NVARCHAR(500) NULL,
        NgayCapNhat DATETIME2(0) NOT NULL CONSTRAINT DF_ThanhVien_NgayCapNhat DEFAULT SYSDATETIME(),
        DaXoa BIT NOT NULL CONSTRAINT DF_ThanhVien_DaXoa DEFAULT 0,
        NgayXoa DATETIME2(0) NULL, LyDoKhoa NVARCHAR(255) NULL;
GO
UPDATE dbo.ThanhVien SET MaThanhVien = CONCAT('TV', RIGHT('000000' + CONVERT(VARCHAR(6), MaTV), 6))
WHERE MaThanhVien IS NULL;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_ThanhVien_MaThanhVien')
    CREATE UNIQUE INDEX UX_ThanhVien_MaThanhVien ON dbo.ThanhVien(MaThanhVien) WHERE MaThanhVien IS NOT NULL;
GO

IF COL_LENGTH(N'dbo.GoiTap', N'LoaiChuKy') IS NULL
    ALTER TABLE dbo.GoiTap ADD LoaiChuKy NVARCHAR(20) NOT NULL CONSTRAINT DF_GoiTap_LoaiChuKy DEFAULT N'Ngày',
        GioiHanLuot INT NULL, GioBatDau TIME NULL, GioKetThuc TIME NULL,
        NgayCapNhat DATETIME2(0) NOT NULL CONSTRAINT DF_GoiTap_NgayCapNhat DEFAULT SYSDATETIME();
GO
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_GoiTap_GioiHanLuot')
    ALTER TABLE dbo.GoiTap ADD CONSTRAINT CK_GoiTap_GioiHanLuot CHECK (GioiHanLuot IS NULL OR GioiHanLuot > 0);
GO

IF COL_LENGTH(N'dbo.DangKyGoi', N'SoLuotDaDung') IS NULL
    ALTER TABLE dbo.DangKyGoi ADD SoLuotDaDung INT NOT NULL CONSTRAINT DF_DKG_SoLuotDaDung DEFAULT 0,
        NgayTamDung DATETIME2(0) NULL, TongSoNgayBaoLuu INT NOT NULL CONSTRAINT DF_DKG_BaoLuu DEFAULT 0,
        NgayCapNhat DATETIME2(0) NOT NULL CONSTRAINT DF_DKG_NgayCapNhat DEFAULT SYSDATETIME();
GO
DECLARE @DropCkDKG NVARCHAR(MAX) = N'';
SELECT @DropCkDKG = @DropCkDKG + N'ALTER TABLE dbo.DangKyGoi DROP CONSTRAINT ' + QUOTENAME(cc.name) + N';'
FROM sys.check_constraints cc JOIN sys.columns c ON c.object_id=cc.parent_object_id AND c.column_id=cc.parent_column_id
WHERE cc.parent_object_id=OBJECT_ID(N'dbo.DangKyGoi') AND c.name=N'TrangThai';
IF @DropCkDKG <> N'' EXEC sys.sp_executesql @DropCkDKG;
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_DangKyGoi_TrangThai_v3')
    ALTER TABLE dbo.DangKyGoi ADD CONSTRAINT CK_DangKyGoi_TrangThai_v3
        CHECK (TrangThai IN (N'Còn hạn',N'Hết hạn',N'Hủy',N'Tạm dừng',N'Chờ thanh toán'));
GO

IF COL_LENGTH(N'dbo.HoaDon', N'NgayCapNhat') IS NULL
    ALTER TABLE dbo.HoaDon ADD NgayCapNhat DATETIME2(0) NOT NULL CONSTRAINT DF_HoaDon_NgayCapNhat DEFAULT SYSDATETIME(),
        LyDoHuy NVARCHAR(255) NULL;
GO
DECLARE @DropCkHD NVARCHAR(MAX) = N'';
SELECT @DropCkHD = @DropCkHD + N'ALTER TABLE dbo.HoaDon DROP CONSTRAINT ' + QUOTENAME(cc.name) + N';'
FROM sys.check_constraints cc JOIN sys.columns c ON c.object_id=cc.parent_object_id AND c.column_id=cc.parent_column_id
WHERE cc.parent_object_id=OBJECT_ID(N'dbo.HoaDon') AND c.name=N'TrangThai';
IF @DropCkHD <> N'' EXEC sys.sp_executesql @DropCkHD;
ALTER TABLE dbo.HoaDon ADD CONSTRAINT CK_HoaDon_TrangThai_v3 CHECK
 (TrangThai IN (N'Chưa thanh toán',N'Đã thanh toán một phần',N'Đã thanh toán',N'Đã hoàn tiền',N'Hủy'));
GO

IF COL_LENGTH(N'dbo.ThanhToan', N'MaGiaoDich') IS NULL
    ALTER TABLE dbo.ThanhToan ADD MaGiaoDich UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_ThanhToan_MaGiaoDich DEFAULT NEWSEQUENTIALID(),
        LoaiGiaoDich NVARCHAR(20) NOT NULL CONSTRAINT DF_ThanhToan_Loai DEFAULT N'Thanh toán',
        MaTTGoc INT NULL, MaNV INT NULL, GhiChu NVARCHAR(255) NULL;
GO
UPDATE dbo.ThanhToan SET LoaiGiaoDich=N'Hoàn tiền' WHERE TrangThai=N'Hoàn tiền' AND LoaiGiaoDich=N'Thanh toán';
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_ThanhToan_LoaiGiaoDich')
    ALTER TABLE dbo.ThanhToan ADD CONSTRAINT CK_ThanhToan_LoaiGiaoDich CHECK(LoaiGiaoDich IN(N'Thanh toán',N'Hoàn tiền'));
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'UX_ThanhToan_MaGiaoDich')
    CREATE UNIQUE INDEX UX_ThanhToan_MaGiaoDich ON dbo.ThanhToan(MaGiaoDich);
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name=N'FK_ThanhToan_ThanhToanGoc')
    ALTER TABLE dbo.ThanhToan ADD CONSTRAINT FK_ThanhToan_ThanhToanGoc FOREIGN KEY(MaTTGoc) REFERENCES dbo.ThanhToan(MaTT);
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name=N'FK_ThanhToan_NhanVien')
    ALTER TABLE dbo.ThanhToan ADD CONSTRAINT FK_ThanhToan_NhanVien FOREIGN KEY(MaNV) REFERENCES dbo.NhanVien(MaNV);
GO

IF COL_LENGTH(N'dbo.SanPham', N'MaSanPham') IS NULL
    ALTER TABLE dbo.SanPham ADD MaSanPham VARCHAR(24) NULL, GiaNhap DECIMAL(18,2) NOT NULL CONSTRAINT DF_SanPham_GiaNhap DEFAULT 0,
        TonToiThieu INT NOT NULL CONSTRAINT DF_SanPham_TonToiThieu DEFAULT 0, MaNCC INT NULL,
        NgayCapNhat DATETIME2(0) NOT NULL CONSTRAINT DF_SanPham_NgayCapNhat DEFAULT SYSDATETIME();
GO
UPDATE dbo.SanPham SET MaSanPham=CONCAT('SP',RIGHT('000000'+CONVERT(VARCHAR(6),MaSP),6)) WHERE MaSanPham IS NULL;
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'UX_SanPham_MaSanPham')
    CREATE UNIQUE INDEX UX_SanPham_MaSanPham ON dbo.SanPham(MaSanPham) WHERE MaSanPham IS NOT NULL;
GO

IF COL_LENGTH(N'dbo.ThietBi', N'MaThietBi') IS NULL
    ALTER TABLE dbo.ThietBi ADD MaThietBi VARCHAR(24) NULL, GiaMua DECIMAL(18,2) NULL,
        MaNCC INT NULL, HanBaoHanh DATE NULL, ChuKyBaoTriNgay INT NULL,
        NgayBaoTriTiepTheo DATE NULL;
GO
UPDATE dbo.ThietBi SET MaThietBi=CONCAT('TB',RIGHT('000000'+CONVERT(VARCHAR(6),MaTB),6)) WHERE MaThietBi IS NULL;
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'UX_ThietBi_MaThietBi')
    CREATE UNIQUE INDEX UX_ThietBi_MaThietBi ON dbo.ThietBi(MaThietBi) WHERE MaThietBi IS NOT NULL;
GO
IF COL_LENGTH(N'dbo.BaoTriThietBi', N'TrangThai') IS NULL
    ALTER TABLE dbo.BaoTriThietBi ADD TrangThai NVARCHAR(30) NOT NULL CONSTRAINT DF_BaoTri_TrangThai DEFAULT N'Hoàn thành',
        NgayDuKien DATE NULL, NgayHoanThanh DATE NULL, GhiChu NVARCHAR(500) NULL;
GO

IF COL_LENGTH(N'dbo.LopTap', N'NgayBatDau') IS NULL
    ALTER TABLE dbo.LopTap ADD NgayBatDau DATE NULL, NgayKetThuc DATE NULL, ThuTrongTuan TINYINT NULL;
GO
IF COL_LENGTH(N'dbo.DiemDanhLop', N'MaLichHoc') IS NULL ALTER TABLE dbo.DiemDanhLop ADD MaLichHoc INT NULL;
GO

/* 2. Cac bang bo sung: khong trung lap voi bang goc. */
CREATE TABLE dbo.NhaCungCap (
    MaNCC INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_NhaCungCap PRIMARY KEY,
    MaNhaCungCap VARCHAR(24) NOT NULL CONSTRAINT UQ_NhaCungCap_Ma UNIQUE,
    TenNCC NVARCHAR(150) NOT NULL, SDT VARCHAR(15) NULL, Email VARCHAR(100) NULL,
    DiaChi NVARCHAR(255) NULL, TrangThai NVARCHAR(20) NOT NULL CONSTRAINT DF_NCC_TrangThai DEFAULT N'Hoạt động',
    CONSTRAINT CK_NCC_TrangThai CHECK(TrangThai IN(N'Hoạt động',N'Ngừng hợp tác'))
);
GO
ALTER TABLE dbo.SanPham ADD CONSTRAINT FK_SanPham_NhaCungCap FOREIGN KEY(MaNCC) REFERENCES dbo.NhaCungCap(MaNCC);
ALTER TABLE dbo.ThietBi ADD CONSTRAINT FK_ThietBi_NhaCungCap FOREIGN KEY(MaNCC) REFERENCES dbo.NhaCungCap(MaNCC);
GO
CREATE TABLE dbo.LichSuThanhVien (
    MaLichSuTV BIGINT IDENTITY(1,1) PRIMARY KEY, MaTV INT NOT NULL, HanhDong NVARCHAR(30) NOT NULL,
    DuLieuCu NVARCHAR(MAX) NULL, DuLieuMoi NVARCHAR(MAX) NULL, ThoiGian DATETIME2(0) NOT NULL DEFAULT SYSDATETIME(), MaNV INT NULL,
    FOREIGN KEY(MaTV) REFERENCES dbo.ThanhVien(MaTV), FOREIGN KEY(MaNV) REFERENCES dbo.NhanVien(MaNV)
);
CREATE TABLE dbo.LichSuGoiTap (
    MaLichSuGoi BIGINT IDENTITY(1,1) PRIMARY KEY, MaDKG INT NOT NULL, HanhDong NVARCHAR(30) NOT NULL,
    TuNgay DATE NULL, DenNgay DATE NULL, GhiChu NVARCHAR(500) NULL, ThoiGian DATETIME2(0) NOT NULL DEFAULT SYSDATETIME(), MaNV INT NULL,
    FOREIGN KEY(MaDKG) REFERENCES dbo.DangKyGoi(MaDKG), FOREIGN KEY(MaNV) REFERENCES dbo.NhanVien(MaNV)
);
CREATE TABLE dbo.BaoLuuGoiTap (
    MaBaoLuu INT IDENTITY(1,1) PRIMARY KEY, MaDKG INT NOT NULL, TuNgay DATE NOT NULL, DenNgay DATE NOT NULL,
    LyDo NVARCHAR(500) NOT NULL, TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Đang bảo lưu',
    NgayTao DATETIME2(0) NOT NULL DEFAULT SYSDATETIME(), MaNV INT NULL,
    CONSTRAINT CK_BaoLuu_Ngay CHECK(DenNgay>=TuNgay), CONSTRAINT CK_BaoLuu_TrangThai CHECK(TrangThai IN(N'Đang bảo lưu',N'Đã kết thúc',N'Hủy')),
    FOREIGN KEY(MaDKG) REFERENCES dbo.DangKyGoi(MaDKG), FOREIGN KEY(MaNV) REFERENCES dbo.NhanVien(MaNV)
);
CREATE TABLE dbo.LichSuCheckIn (
    MaCheckIn BIGINT IDENTITY(1,1) PRIMARY KEY, MaTV INT NOT NULL, MaDKG INT NOT NULL, MaPhong INT NULL,
    ThoiGianVao DATETIME2(0) NOT NULL DEFAULT SYSDATETIME(), ThoiGianRa DATETIME2(0) NULL,
    Kenh NVARCHAR(20) NOT NULL DEFAULT N'Quầy', MaQR VARCHAR(100) NULL, GhiChu NVARCHAR(255) NULL,
    CONSTRAINT CK_CheckIn_Kenh CHECK(Kenh IN(N'Quầy',N'QR',N'Ứng dụng')),
    CONSTRAINT CK_CheckIn_ThoiGian CHECK(ThoiGianRa IS NULL OR ThoiGianRa>=ThoiGianVao),
    FOREIGN KEY(MaTV) REFERENCES dbo.ThanhVien(MaTV), FOREIGN KEY(MaDKG) REFERENCES dbo.DangKyGoi(MaDKG), FOREIGN KEY(MaPhong) REFERENCES dbo.PhongTap(MaPhong)
);
CREATE UNIQUE INDEX UX_CheckIn_Mo ON dbo.LichSuCheckIn(MaTV) WHERE ThoiGianRa IS NULL;
GO
CREATE TABLE dbo.LichLamViecHLV (
    MaLichLamViec INT IDENTITY(1,1) PRIMARY KEY, MaHLV INT NOT NULL, ThuTrongTuan TINYINT NOT NULL,
    GioBatDau TIME NOT NULL, GioKetThuc TIME NOT NULL, TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Hoạt động',
    CONSTRAINT CK_LLV_Thu CHECK(ThuTrongTuan BETWEEN 2 AND 8), CONSTRAINT CK_LLV_Gio CHECK(GioKetThuc>GioBatDau),
    FOREIGN KEY(MaHLV) REFERENCES dbo.HuanLuyenVien(MaHLV)
);
CREATE TABLE dbo.LichTapPT (
    MaLichPT INT IDENTITY(1,1) PRIMARY KEY, MaDKPT INT NOT NULL, MaHLV INT NOT NULL, MaTV INT NOT NULL,
    ThoiGianBatDau DATETIME2(0) NOT NULL, ThoiGianKetThuc DATETIME2(0) NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Đã đặt', NhanXet NVARCHAR(1000) NULL, NgayTao DATETIME2(0) NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT CK_LichPT_ThoiGian CHECK(ThoiGianKetThuc>ThoiGianBatDau),
    CONSTRAINT CK_LichPT_TrangThai CHECK(TrangThai IN(N'Đã đặt',N'Hoàn thành',N'Đã hủy',N'Vắng mặt')),
    FOREIGN KEY(MaDKPT) REFERENCES dbo.DangKyPT(MaDKPT), FOREIGN KEY(MaHLV) REFERENCES dbo.HuanLuyenVien(MaHLV), FOREIGN KEY(MaTV) REFERENCES dbo.ThanhVien(MaTV)
);
CREATE INDEX IX_LichTapPT_HLV_ThoiGian ON dbo.LichTapPT(MaHLV,ThoiGianBatDau,ThoiGianKetThuc) INCLUDE(TrangThai);
GO
CREATE TABLE dbo.LichHoc (
    MaLichHoc INT IDENTITY(1,1) PRIMARY KEY, MaLop INT NOT NULL, MaHLV INT NOT NULL, MaPhong INT NOT NULL,
    ThoiGianBatDau DATETIME2(0) NOT NULL, ThoiGianKetThuc DATETIME2(0) NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Đã lên lịch', GhiChu NVARCHAR(500) NULL,
    CONSTRAINT CK_LichHoc_ThoiGian CHECK(ThoiGianKetThuc>ThoiGianBatDau),
    CONSTRAINT CK_LichHoc_TrangThai CHECK(TrangThai IN(N'Đã lên lịch',N'Đã diễn ra',N'Đã hủy')),
    FOREIGN KEY(MaLop) REFERENCES dbo.LopTap(MaLop), FOREIGN KEY(MaHLV) REFERENCES dbo.HuanLuyenVien(MaHLV), FOREIGN KEY(MaPhong) REFERENCES dbo.PhongTap(MaPhong)
);
CREATE INDEX IX_LichHoc_Phong_ThoiGian ON dbo.LichHoc(MaPhong,ThoiGianBatDau,ThoiGianKetThuc) INCLUDE(TrangThai);
CREATE TABLE dbo.DanhSachCho (
    MaCho INT IDENTITY(1,1) PRIMARY KEY, MaLop INT NOT NULL, MaTV INT NOT NULL, ThoiGianCho DATETIME2(0) NOT NULL DEFAULT SYSDATETIME(),
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Đang chờ', CONSTRAINT UQ_DanhSachCho UNIQUE(MaLop,MaTV),
    CONSTRAINT CK_DSC_TrangThai CHECK(TrangThai IN(N'Đang chờ',N'Đã mời',N'Đã hủy')), FOREIGN KEY(MaLop) REFERENCES dbo.LopTap(MaLop), FOREIGN KEY(MaTV) REFERENCES dbo.ThanhVien(MaTV)
);
ALTER TABLE dbo.DiemDanhLop ADD CONSTRAINT FK_DiemDanh_LichHoc FOREIGN KEY(MaLichHoc) REFERENCES dbo.LichHoc(MaLichHoc);
CREATE UNIQUE INDEX UX_DiemDanh_LichHoc_DKL ON dbo.DiemDanhLop(MaLichHoc,MaDKL) WHERE MaLichHoc IS NOT NULL;
GO
CREATE TABLE dbo.PhieuNhapKho (
    MaPNK INT IDENTITY(1,1) PRIMARY KEY, MaNCC INT NOT NULL, MaNV INT NOT NULL, NgayNhap DATETIME2(0) NOT NULL DEFAULT SYSDATETIME(),
    TongTien DECIMAL(18,2) NOT NULL DEFAULT 0, TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Hoàn thành', GhiChu NVARCHAR(500) NULL,
    CONSTRAINT CK_PNK_TrangThai CHECK(TrangThai IN(N'Hoàn thành',N'Hủy')), FOREIGN KEY(MaNCC) REFERENCES dbo.NhaCungCap(MaNCC), FOREIGN KEY(MaNV) REFERENCES dbo.NhanVien(MaNV)
);
CREATE TABLE dbo.ChiTietPhieuNhap (
    MaCTPNK INT IDENTITY(1,1) PRIMARY KEY, MaPNK INT NOT NULL, MaSP INT NOT NULL, SoLuong INT NOT NULL, DonGiaNhap DECIMAL(18,2) NOT NULL,
    ThanhTien AS (CONVERT(DECIMAL(18,2),SoLuong)*DonGiaNhap) PERSISTED, CONSTRAINT CK_CTPNK_SL CHECK(SoLuong>0), CONSTRAINT CK_CTPNK_Gia CHECK(DonGiaNhap>=0),
    CONSTRAINT UQ_CTPNK UNIQUE(MaPNK,MaSP), FOREIGN KEY(MaPNK) REFERENCES dbo.PhieuNhapKho(MaPNK), FOREIGN KEY(MaSP) REFERENCES dbo.SanPham(MaSP)
);
CREATE TABLE dbo.LichSuTonKho (
    MaLichSuTon BIGINT IDENTITY(1,1) PRIMARY KEY, MaSP INT NOT NULL, LoaiBienDong NVARCHAR(20) NOT NULL, SoLuong INT NOT NULL,
    TonTruoc INT NOT NULL, TonSau INT NOT NULL, NguonThamChieu NVARCHAR(30) NOT NULL, MaThamChieu INT NULL, ThoiGian DATETIME2(0) NOT NULL DEFAULT SYSDATETIME(), MaNV INT NULL,
    CONSTRAINT CK_LSTK_Loai CHECK(LoaiBienDong IN(N'Nhập',N'Xuất',N'Điều chỉnh',N'Hoàn trả')),
    CONSTRAINT CK_LSTK_Ton CHECK(TonTruoc>=0 AND TonSau>=0), FOREIGN KEY(MaSP) REFERENCES dbo.SanPham(MaSP), FOREIGN KEY(MaNV) REFERENCES dbo.NhanVien(MaNV)
);
GO
CREATE TABLE dbo.NhatKyHeThong (
    MaNhatKy BIGINT IDENTITY(1,1) PRIMARY KEY, MaTK INT NULL, MaNV INT NULL, HanhDong NVARCHAR(100) NOT NULL,
    DoiTuong NVARCHAR(100) NULL, KhoaDoiTuong NVARCHAR(100) NULL, NoiDung NVARCHAR(MAX) NULL, ThoiGian DATETIME2(0) NOT NULL DEFAULT SYSDATETIME(), IP VARCHAR(45) NULL,
    FOREIGN KEY(MaTK) REFERENCES dbo.TaiKhoan(MaTK), FOREIGN KEY(MaNV) REFERENCES dbo.NhanVien(MaNV)
);
CREATE TABLE dbo.LichSuDangNhap (
    MaLichSuDN BIGINT IDENTITY(1,1) PRIMARY KEY, MaTK INT NOT NULL, ThoiGian DATETIME2(0) NOT NULL DEFAULT SYSDATETIME(),
    ThanhCong BIT NOT NULL, IP VARCHAR(45) NULL, ThietBi NVARCHAR(255) NULL, FOREIGN KEY(MaTK) REFERENCES dbo.TaiKhoan(MaTK)
);
CREATE TABLE dbo.VaiTro (
    MaVaiTro INT IDENTITY(1,1) PRIMARY KEY, TenVaiTro NVARCHAR(50) NOT NULL CONSTRAINT UQ_VaiTro UNIQUE, MoTa NVARCHAR(255) NULL
);
CREATE TABLE dbo.TaiKhoanVaiTro (
    MaTK INT NOT NULL, MaVaiTro INT NOT NULL, CONSTRAINT PK_TaiKhoanVaiTro PRIMARY KEY(MaTK,MaVaiTro),
    FOREIGN KEY(MaTK) REFERENCES dbo.TaiKhoan(MaTK), FOREIGN KEY(MaVaiTro) REFERENCES dbo.VaiTro(MaVaiTro)
);
GO
INSERT dbo.VaiTro(TenVaiTro,MoTa) SELECT v.TenVaiTro,v.MoTa FROM (VALUES
 (N'Admin',N'Quản trị toàn hệ thống'),(N'Quản lý',N'Quản lý vận hành'),(N'Lễ tân',N'Tiếp nhận'),(N'Kế toán',N'Tài chính'),(N'Huấn luyện viên',N'PT'),(N'Nhân viên bán hàng',N'Bán sản phẩm'))v(TenVaiTro,MoTa)
WHERE NOT EXISTS(SELECT 1 FROM dbo.VaiTro r WHERE r.TenVaiTro=v.TenVaiTro);
IF COL_LENGTH(N'dbo.TaiKhoan',N'MatKhauHash') IS NULL ALTER TABLE dbo.TaiKhoan ADD MatKhauHash VARBINARY(64) NULL, MatKhauSalt VARBINARY(32) NULL, LanDangNhapCuoi DATETIME2(0) NULL;
GO

/* 3. Thay the tham chieu da hinh cua ChiTietHoaDon bang FK theo tung loai. */
CREATE TABLE dbo.HoaDonGoiTap (MaCTHD INT NOT NULL PRIMARY KEY, MaDKG INT NOT NULL CONSTRAINT UQ_HDGoiTap_DKG UNIQUE,
 FOREIGN KEY(MaCTHD) REFERENCES dbo.ChiTietHoaDon(MaCTHD), FOREIGN KEY(MaDKG) REFERENCES dbo.DangKyGoi(MaDKG));
CREATE TABLE dbo.HoaDonPT (MaCTHD INT NOT NULL PRIMARY KEY, MaDKPT INT NOT NULL CONSTRAINT UQ_HDPT_DKPT UNIQUE,
 FOREIGN KEY(MaCTHD) REFERENCES dbo.ChiTietHoaDon(MaCTHD), FOREIGN KEY(MaDKPT) REFERENCES dbo.DangKyPT(MaDKPT));
CREATE TABLE dbo.HoaDonLop (MaCTHD INT NOT NULL PRIMARY KEY, MaDKL INT NOT NULL CONSTRAINT UQ_HDLop_DKL UNIQUE,
 FOREIGN KEY(MaCTHD) REFERENCES dbo.ChiTietHoaDon(MaCTHD), FOREIGN KEY(MaDKL) REFERENCES dbo.DangKyLop(MaDKL));
CREATE TABLE dbo.HoaDonSanPham (MaCTHD INT NOT NULL PRIMARY KEY, MaSP INT NOT NULL,
 FOREIGN KEY(MaCTHD) REFERENCES dbo.ChiTietHoaDon(MaCTHD), FOREIGN KEY(MaSP) REFERENCES dbo.SanPham(MaSP));
GO
INSERT dbo.HoaDonGoiTap(MaCTHD,MaDKG) SELECT c.MaCTHD,c.MaThamChieu FROM dbo.ChiTietHoaDon c WHERE c.LoaiDichVu=N'Gói tập' AND c.MaThamChieu IS NOT NULL AND EXISTS(SELECT 1 FROM dbo.DangKyGoi d WHERE d.MaDKG=c.MaThamChieu) AND NOT EXISTS(SELECT 1 FROM dbo.HoaDonGoiTap x WHERE x.MaCTHD=c.MaCTHD);
INSERT dbo.HoaDonPT(MaCTHD,MaDKPT) SELECT c.MaCTHD,c.MaThamChieu FROM dbo.ChiTietHoaDon c WHERE c.LoaiDichVu=N'PT' AND c.MaThamChieu IS NOT NULL AND EXISTS(SELECT 1 FROM dbo.DangKyPT d WHERE d.MaDKPT=c.MaThamChieu) AND NOT EXISTS(SELECT 1 FROM dbo.HoaDonPT x WHERE x.MaCTHD=c.MaCTHD);
GO

/* 4. Index cho cac truy van nghiep vu chinh. */
CREATE INDEX IX_ThanhVien_TimKiem ON dbo.ThanhVien(HoTen,SDT,Email) INCLUDE(TrangThai,DaXoa,MaLoaiTV);
CREATE INDEX IX_DangKyGoi_ThanhVien_TrangThai ON dbo.DangKyGoi(MaTV,TrangThai,NgayKetThuc) INCLUDE(MaGoi,GiaSauGiam);
CREATE INDEX IX_HoaDon_TrangThai_NgayLap ON dbo.HoaDon(TrangThai,NgayLap) INCLUDE(MaTV,TongTien);
CREATE INDEX IX_ThanhToan_HoaDon ON dbo.ThanhToan(MaHD,TrangThai,LoaiGiaoDich) INCLUDE(SoTien,NgayThanhToan);
CREATE INDEX IX_DangKyLop_Lop_TrangThai ON dbo.DangKyLop(MaLop,TrangThai) INCLUDE(MaTV);
CREATE INDEX IX_SanPham_Ton ON dbo.SanPham(SoLuongTon,TonToiThieu) INCLUDE(TenSP,DonGia,GiaNhap);
CREATE INDEX IX_BaoTri_ThietBi_Ngay ON dbo.BaoTriThietBi(MaTB,NgayBaoTri) INCLUDE(ChiPhi,TrangThai);
GO

/* 5. Trigger nhe: tong hoa don la tong chi tiet, ma TV tu dong, audit thay doi thanh vien. */
CREATE OR ALTER TRIGGER dbo.TR_ThanhVien_MaTuDong ON dbo.ThanhVien AFTER INSERT AS
BEGIN
 SET NOCOUNT ON;
 UPDATE t SET MaThanhVien=CONCAT('TV',RIGHT('000000'+CONVERT(VARCHAR(6),t.MaTV),6))
 FROM dbo.ThanhVien t JOIN inserted i ON i.MaTV=t.MaTV WHERE t.MaThanhVien IS NULL;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_ChiTietHoaDon_CapNhatTong ON dbo.ChiTietHoaDon AFTER INSERT,UPDATE,DELETE AS
BEGIN
 SET NOCOUNT ON;
 ;WITH h AS (SELECT MaHD FROM inserted UNION SELECT MaHD FROM deleted)
 UPDATE hd SET TongTien=ISNULL(x.TongTien,0),NgayCapNhat=SYSDATETIME()
 FROM dbo.HoaDon hd JOIN h ON h.MaHD=hd.MaHD
 OUTER APPLY(SELECT SUM(ThanhTien) TongTien FROM dbo.ChiTietHoaDon c WHERE c.MaHD=hd.MaHD)x;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_ThanhVien_LichSu ON dbo.ThanhVien AFTER UPDATE AS
BEGIN
 SET NOCOUNT ON;
 INSERT dbo.LichSuThanhVien(MaTV,HanhDong,DuLieuCu,DuLieuMoi)
 SELECT i.MaTV,N'Cập nhật',CONCAT(N'Họ tên=',d.HoTen,N'; Trạng thái=',d.TrangThai),CONCAT(N'Họ tên=',i.HoTen,N'; Trạng thái=',i.TrangThai)
 FROM inserted i JOIN deleted d ON d.MaTV=i.MaTV
 WHERE ISNULL(i.HoTen,N'')<>ISNULL(d.HoTen,N'') OR ISNULL(i.TrangThai,N'')<>ISNULL(d.TrangThai,N'') OR ISNULL(i.SDT,N'')<>ISNULL(d.SDT,N'');
END;
GO

/* 6. Stored procedures: tat ca ghi nhieu bang deu dung transaction va THROW de caller xu ly. */
CREATE OR ALTER PROCEDURE dbo.sp_ThemThanhVien @HoTen NVARCHAR(100),@NgaySinh DATE,@GioiTinh NVARCHAR(10),@SDT VARCHAR(15),@Email VARCHAR(100)=NULL,@DiaChi NVARCHAR(255)=NULL,@MaLoaiTV INT,@MaTV INT OUTPUT AS
BEGIN TRY
 INSERT dbo.ThanhVien(HoTen,NgaySinh,GioiTinh,SDT,Email,DiaChi,MaLoaiTV) VALUES(@HoTen,@NgaySinh,@GioiTinh,@SDT,@Email,@DiaChi,@MaLoaiTV); SET @MaTV=SCOPE_IDENTITY();
END TRY BEGIN CATCH THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_SuaThanhVien @MaTV INT,@HoTen NVARCHAR(100),@SDT VARCHAR(15),@Email VARCHAR(100)=NULL,@DiaChi NVARCHAR(255)=NULL,@MaLoaiTV INT AS
BEGIN TRY
 UPDATE dbo.ThanhVien SET HoTen=@HoTen,SDT=@SDT,Email=@Email,DiaChi=@DiaChi,MaLoaiTV=@MaLoaiTV,NgayCapNhat=SYSDATETIME() WHERE MaTV=@MaTV AND DaXoa=0;
 IF @@ROWCOUNT=0 THROW 50001,N'Thành viên không tồn tại hoặc đã xóa mềm.',1;
END TRY BEGIN CATCH THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_KhoaThanhVien @MaTV INT,@LyDo NVARCHAR(255),@MaNV INT=NULL AS
BEGIN TRY
 UPDATE dbo.ThanhVien SET TrangThai=N'Tạm khóa',LyDoKhoa=@LyDo,NgayCapNhat=SYSDATETIME() WHERE MaTV=@MaTV AND DaXoa=0;
 IF @@ROWCOUNT=0 THROW 50002,N'Không tìm thấy thành viên.',1;
 INSERT dbo.NhatKyHeThong(MaNV,HanhDong,DoiTuong,KhoaDoiTuong,NoiDung) VALUES(@MaNV,N'Khóa thành viên',N'ThanhVien',CONVERT(NVARCHAR(30),@MaTV),@LyDo);
END TRY BEGIN CATCH THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_DangKyGoiTap @MaTV INT,@MaGoi INT,@MaKM INT=NULL,@NgayBatDau DATE=NULL,@MaNV INT,@MaDKG INT OUTPUT,@MaHD INT OUTPUT AS
BEGIN TRY
 SET XACT_ABORT ON; BEGIN TRAN;
 IF NOT EXISTS(SELECT 1 FROM dbo.ThanhVien WHERE MaTV=@MaTV AND TrangThai=N'Hoạt động' AND DaXoa=0) THROW 50003,N'Thành viên không hoạt động.',1;
 DECLARE @Gia DECIMAL(18,2),@Han INT,@Giam DECIMAL(5,2); SELECT @Gia=Gia,@Han=ThoiHanNgay FROM dbo.GoiTap WHERE MaGoi=@MaGoi AND TrangThai=N'Đang áp dụng';
 IF @Gia IS NULL THROW 50004,N'Gói tập không hợp lệ.',1;
 IF @MaKM IS NOT NULL SELECT @Giam=PhanTramGiam FROM dbo.KhuyenMai WHERE MaKM=@MaKM AND TrangThai=N'Hoạt động' AND CAST(SYSDATETIME() AS DATE) BETWEEN NgayBatDau AND NgayKetThuc;
 IF @MaKM IS NOT NULL AND @Giam IS NULL THROW 50005,N'Khuyến mãi không còn hiệu lực.',1;
 SET @Giam=ISNULL(@Giam,0);
 SET @NgayBatDau=ISNULL(@NgayBatDau,CAST(SYSDATETIME() AS DATE));
 INSERT dbo.DangKyGoi(MaTV,MaGoi,MaKM,NgayBatDau,NgayKetThuc,GiaSauGiam,TrangThai) VALUES(@MaTV,@MaGoi,@MaKM,@NgayBatDau,DATEADD(DAY,@Han-1,@NgayBatDau),ROUND(@Gia*(1-@Giam/100),2),N'Chờ thanh toán'); SET @MaDKG=SCOPE_IDENTITY();
 INSERT dbo.HoaDon(MaTV,MaNV,TongTien,TrangThai) VALUES(@MaTV,@MaNV,0,N'Chưa thanh toán'); SET @MaHD=SCOPE_IDENTITY();
 INSERT dbo.ChiTietHoaDon(MaHD,LoaiDichVu,MaThamChieu,SoLuong,DonGia) SELECT @MaHD,N'Gói tập',@MaDKG,1,GiaSauGiam FROM dbo.DangKyGoi WHERE MaDKG=@MaDKG;
 INSERT dbo.HoaDonGoiTap(MaCTHD,MaDKG) SELECT MaCTHD,@MaDKG FROM dbo.ChiTietHoaDon WHERE MaHD=@MaHD;
 INSERT dbo.LichSuGoiTap(MaDKG,HanhDong,TuNgay,DenNgay,MaNV) SELECT @MaDKG,N'Đăng ký',NgayBatDau,NgayKetThuc,@MaNV FROM dbo.DangKyGoi WHERE MaDKG=@MaDKG;
 COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_GiaHanGoiTap @MaDKG INT,@SoNgay INT,@MaNV INT=NULL AS
BEGIN TRY
 SET XACT_ABORT ON; BEGIN TRAN; IF @SoNgay<=0 THROW 50006,N'Số ngày gia hạn phải lớn hơn 0.',1;
 DECLARE @Cu DATE; SELECT @Cu=NgayKetThuc FROM dbo.DangKyGoi WITH(UPDLOCK) WHERE MaDKG=@MaDKG AND TrangThai IN(N'Còn hạn',N'Hết hạn');
 IF @Cu IS NULL THROW 50007,N'Gói không thể gia hạn.',1;
 UPDATE dbo.DangKyGoi SET NgayKetThuc=DATEADD(DAY,@SoNgay,CASE WHEN @Cu<CAST(SYSDATETIME() AS DATE) THEN CAST(SYSDATETIME() AS DATE) ELSE @Cu END),TrangThai=N'Còn hạn',NgayCapNhat=SYSDATETIME() WHERE MaDKG=@MaDKG;
 INSERT dbo.LichSuGoiTap(MaDKG,HanhDong,TuNgay,DenNgay,MaNV) SELECT @MaDKG,N'Gia hạn',@Cu,NgayKetThuc,@MaNV FROM dbo.DangKyGoi WHERE MaDKG=@MaDKG; COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_BaoLuuGoiTap @MaDKG INT,@TuNgay DATE,@DenNgay DATE,@LyDo NVARCHAR(500),@MaNV INT=NULL AS
BEGIN TRY
 SET XACT_ABORT ON; BEGIN TRAN;
 IF @DenNgay<@TuNgay THROW 50008,N'Khoảng bảo lưu không hợp lệ.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.DangKyGoi WITH(UPDLOCK) WHERE MaDKG=@MaDKG AND TrangThai=N'Còn hạn') THROW 50009,N'Chỉ bảo lưu gói còn hạn.',1;
 INSERT dbo.BaoLuuGoiTap(MaDKG,TuNgay,DenNgay,LyDo,MaNV) VALUES(@MaDKG,@TuNgay,@DenNgay,@LyDo,@MaNV);
 UPDATE dbo.DangKyGoi SET TrangThai=N'Tạm dừng',NgayTamDung=SYSDATETIME(),TongSoNgayBaoLuu=TongSoNgayBaoLuu+DATEDIFF(DAY,@TuNgay,@DenNgay)+1 WHERE MaDKG=@MaDKG;
 INSERT dbo.LichSuGoiTap(MaDKG,HanhDong,TuNgay,DenNgay,GhiChu,MaNV) VALUES(@MaDKG,N'Bảo lưu',@TuNgay,@DenNgay,@LyDo,@MaNV); COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_CheckIn @MaTV INT,@MaPhong INT=NULL,@Kenh NVARCHAR(20)=N'Quầy',@MaCheckIn BIGINT OUTPUT AS
BEGIN TRY
 SET XACT_ABORT ON; BEGIN TRAN;
 IF EXISTS(SELECT 1 FROM dbo.LichSuCheckIn WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND ThoiGianRa IS NULL) THROW 50010,N'Thành viên đang có lượt check-in mở.',1;
 DECLARE @MaDKG INT; SELECT TOP(1) @MaDKG=d.MaDKG FROM dbo.DangKyGoi d JOIN dbo.ThanhVien t ON t.MaTV=d.MaTV
 WHERE d.MaTV=@MaTV AND d.TrangThai=N'Còn hạn' AND d.NgayBatDau<=CAST(SYSDATETIME() AS DATE) AND d.NgayKetThuc>=CAST(SYSDATETIME() AS DATE) AND t.TrangThai=N'Hoạt động' AND t.DaXoa=0
 AND EXISTS(SELECT 1 FROM dbo.HoaDonGoiTap x JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD JOIN dbo.HoaDon h ON h.MaHD=c.MaHD WHERE x.MaDKG=d.MaDKG AND h.TrangThai=N'Đã thanh toán') ORDER BY d.NgayKetThuc DESC;
 IF @MaDKG IS NULL THROW 50011,N'Không có gói hợp lệ đã thanh toán để check-in.',1;
 INSERT dbo.LichSuCheckIn(MaTV,MaDKG,MaPhong,Kenh) VALUES(@MaTV,@MaDKG,@MaPhong,@Kenh); SET @MaCheckIn=SCOPE_IDENTITY(); COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_CheckOut @MaTV INT AS
BEGIN TRY
 UPDATE dbo.LichSuCheckIn SET ThoiGianRa=SYSDATETIME() WHERE MaTV=@MaTV AND ThoiGianRa IS NULL;
 IF @@ROWCOUNT=0 THROW 50012,N'Không tìm thấy lượt check-in mở.',1;
END TRY BEGIN CATCH THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_DangKyLop @MaTV INT,@MaLop INT,@VaoDanhSachCho BIT=1,@MaDKL INT OUTPUT AS
BEGIN TRY
 SET XACT_ABORT ON; BEGIN TRAN;
 IF NOT EXISTS(SELECT 1 FROM dbo.ThanhVien WHERE MaTV=@MaTV AND TrangThai=N'Hoạt động' AND DaXoa=0) THROW 50028,N'Thành viên không hoạt động.',1;
 DECLARE @SucChua INT,@DangKy INT; SELECT @SucChua=SucChua FROM dbo.LopTap WITH(UPDLOCK,HOLDLOCK) WHERE MaLop=@MaLop AND TrangThai=N'Đang mở';
 IF @SucChua IS NULL THROW 50013,N'Lớp không mở hoặc không tồn tại.',1;
 SELECT @DangKy=COUNT(*) FROM dbo.DangKyLop WHERE MaLop=@MaLop AND TrangThai=N'Đang tham gia';
 IF @DangKy>=@SucChua BEGIN IF @VaoDanhSachCho=1 BEGIN INSERT dbo.DanhSachCho(MaLop,MaTV) VALUES(@MaLop,@MaTV); SET @MaDKL=NULL; COMMIT; RETURN; END; THROW 50014,N'Lớp đã đủ sức chứa.',1; END;
 INSERT dbo.DangKyLop(MaTV,MaLop) VALUES(@MaTV,@MaLop); SET @MaDKL=SCOPE_IDENTITY(); COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_HuyDangKyLop @MaDKL INT AS
BEGIN TRY
 UPDATE dbo.DangKyLop SET TrangThai=N'Đã hủy' WHERE MaDKL=@MaDKL AND TrangThai=N'Đang tham gia'; IF @@ROWCOUNT=0 THROW 50015,N'Không thể hủy đăng ký lớp.',1;
END TRY BEGIN CATCH THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_DatLichPT @MaDKPT INT,@ThoiGianBatDau DATETIME2(0),@ThoiGianKetThuc DATETIME2(0),@MaLichPT INT OUTPUT AS
BEGIN TRY
 SET XACT_ABORT ON; BEGIN TRAN; DECLARE @HLV INT,@TV INT;
 SELECT @HLV=MaHLV,@TV=MaTV FROM dbo.DangKyPT WITH(UPDLOCK,HOLDLOCK) WHERE MaDKPT=@MaDKPT AND TrangThai=N'Đang học';
 IF @HLV IS NULL THROW 50016,N'Đăng ký PT không hợp lệ.',1;
 IF EXISTS(SELECT 1 FROM dbo.LichTapPT WITH(UPDLOCK,HOLDLOCK) WHERE MaHLV=@HLV AND TrangThai=N'Đã đặt' AND @ThoiGianBatDau<ThoiGianKetThuc AND @ThoiGianKetThuc>ThoiGianBatDau) THROW 50017,N'HLV bị trùng lịch.',1;
 INSERT dbo.LichTapPT(MaDKPT,MaHLV,MaTV,ThoiGianBatDau,ThoiGianKetThuc) VALUES(@MaDKPT,@HLV,@TV,@ThoiGianBatDau,@ThoiGianKetThuc); SET @MaLichPT=SCOPE_IDENTITY(); COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_HoanThanhBuoiPT @MaLichPT INT,@NhanXet NVARCHAR(1000)=NULL AS
BEGIN TRY
 UPDATE dbo.LichTapPT SET TrangThai=N'Hoàn thành',NhanXet=@NhanXet WHERE MaLichPT=@MaLichPT AND TrangThai=N'Đã đặt'; IF @@ROWCOUNT=0 THROW 50018,N'Buổi PT không thể hoàn thành.',1;
END TRY BEGIN CATCH THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_TaoLichHoc @MaLop INT,@ThoiGianBatDau DATETIME2(0),@ThoiGianKetThuc DATETIME2(0),@MaLichHoc INT OUTPUT AS
BEGIN TRY
 SET XACT_ABORT ON; BEGIN TRAN; DECLARE @HLV INT,@Phong INT;
 SELECT @HLV=MaHLV,@Phong=MaPhong FROM dbo.LopTap WITH(UPDLOCK,HOLDLOCK) WHERE MaLop=@MaLop AND TrangThai=N'Đang mở';
 IF @HLV IS NULL OR @Phong IS NULL THROW 50029,N'Lớp chưa mở hoặc chưa được gán phòng.',1;
 IF @ThoiGianKetThuc<=@ThoiGianBatDau THROW 50030,N'Khung giờ lớp không hợp lệ.',1;
 IF EXISTS(SELECT 1 FROM dbo.LichHoc WITH(UPDLOCK,HOLDLOCK) WHERE TrangThai=N'Đã lên lịch' AND (MaPhong=@Phong OR MaHLV=@HLV) AND @ThoiGianBatDau<ThoiGianKetThuc AND @ThoiGianKetThuc>ThoiGianBatDau) THROW 50031,N'Phòng hoặc HLV bị trùng lịch.',1;
 INSERT dbo.LichHoc(MaLop,MaHLV,MaPhong,ThoiGianBatDau,ThoiGianKetThuc) VALUES(@MaLop,@HLV,@Phong,@ThoiGianBatDau,@ThoiGianKetThuc); SET @MaLichHoc=SCOPE_IDENTITY(); COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_TaoHoaDon @MaTV INT,@MaNV INT,@MaHD INT OUTPUT AS
BEGIN TRY
 IF NOT EXISTS(SELECT 1 FROM dbo.ThanhVien WHERE MaTV=@MaTV AND DaXoa=0) THROW 50032,N'Thành viên không tồn tại.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.NhanVien WHERE MaNV=@MaNV AND TrangThai=N'Đang làm') THROW 50033,N'Nhân viên không hoạt động.',1;
 INSERT dbo.HoaDon(MaTV,MaNV,TongTien,TrangThai) VALUES(@MaTV,@MaNV,0,N'Chưa thanh toán'); SET @MaHD=SCOPE_IDENTITY();
END TRY BEGIN CATCH THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_HuyHoaDon @MaHD INT,@LyDo NVARCHAR(255),@MaNV INT=NULL AS
BEGIN TRY
 SET XACT_ABORT ON; BEGIN TRAN;
 IF EXISTS(SELECT 1 FROM dbo.ThanhToan WITH(UPDLOCK,HOLDLOCK) WHERE MaHD=@MaHD AND TrangThai=N'Thành công' AND LoaiGiaoDich=N'Thanh toán') THROW 50034,N'Hóa đơn đã có thanh toán; hãy hoàn tiền trước khi hủy.',1;
 UPDATE dbo.HoaDon SET TrangThai=N'Hủy',LyDoHuy=@LyDo,NgayCapNhat=SYSDATETIME() WHERE MaHD=@MaHD AND TrangThai IN(N'Chưa thanh toán',N'Đã thanh toán một phần');
 IF @@ROWCOUNT=0 THROW 50035,N'Hóa đơn không thể hủy.',1;
 INSERT dbo.NhatKyHeThong(MaNV,HanhDong,DoiTuong,KhoaDoiTuong,NoiDung) VALUES(@MaNV,N'Hủy hóa đơn',N'HoaDon',CONVERT(NVARCHAR(30),@MaHD),@LyDo); COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_ThanhToanHoaDon @MaHD INT,@SoTien DECIMAL(18,2),@PhuongThuc NVARCHAR(50),@MaNV INT=NULL,@MaTT INT OUTPUT AS
BEGIN TRY
 SET XACT_ABORT ON; BEGIN TRAN; IF @SoTien<=0 THROW 50019,N'Số tiền thanh toán phải dương.',1;
 DECLARE @Tong DECIMAL(18,2),@DaTra DECIMAL(18,2); SELECT @Tong=TongTien FROM dbo.HoaDon WITH(UPDLOCK,HOLDLOCK) WHERE MaHD=@MaHD AND TrangThai NOT IN(N'Hủy',N'Đã hoàn tiền');
 IF @Tong IS NULL THROW 50020,N'Hóa đơn không thể thanh toán.',1;
 SELECT @DaTra=ISNULL(SUM(CASE WHEN LoaiGiaoDich=N'Thanh toán' THEN SoTien ELSE -SoTien END),0) FROM dbo.ThanhToan WHERE MaHD=@MaHD AND TrangThai=N'Thành công';
 IF @DaTra+@SoTien>@Tong THROW 50021,N'Thanh toán vượt quá số tiền phải thu.',1;
 INSERT dbo.ThanhToan(MaHD,SoTien,PhuongThuc,TrangThai,LoaiGiaoDich,MaNV) VALUES(@MaHD,@SoTien,@PhuongThuc,N'Thành công',N'Thanh toán',@MaNV); SET @MaTT=SCOPE_IDENTITY();
 UPDATE dbo.HoaDon SET TrangThai=CASE WHEN @DaTra+@SoTien=@Tong THEN N'Đã thanh toán' ELSE N'Đã thanh toán một phần' END,NgayCapNhat=SYSDATETIME() WHERE MaHD=@MaHD;
 IF @DaTra+@SoTien=@Tong UPDATE d SET TrangThai=N'Còn hạn',NgayCapNhat=SYSDATETIME() FROM dbo.DangKyGoi d JOIN dbo.HoaDonGoiTap x ON x.MaDKG=d.MaDKG JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD WHERE c.MaHD=@MaHD AND d.TrangThai=N'Chờ thanh toán'; COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_HoanTien @MaHD INT,@SoTien DECIMAL(18,2),@PhuongThuc NVARCHAR(50),@MaNV INT=NULL,@MaTT INT OUTPUT AS
BEGIN TRY
 SET XACT_ABORT ON; BEGIN TRAN; DECLARE @Rong DECIMAL(18,2);
 SELECT @Rong=ISNULL(SUM(CASE WHEN LoaiGiaoDich=N'Thanh toán' THEN SoTien ELSE -SoTien END),0) FROM dbo.ThanhToan WITH(UPDLOCK,HOLDLOCK) WHERE MaHD=@MaHD AND TrangThai=N'Thành công';
 IF @SoTien<=0 OR @SoTien>@Rong THROW 50022,N'Số tiền hoàn không hợp lệ.',1;
 INSERT dbo.ThanhToan(MaHD,SoTien,PhuongThuc,TrangThai,LoaiGiaoDich,MaNV) VALUES(@MaHD,@SoTien,@PhuongThuc,N'Thành công',N'Hoàn tiền',@MaNV); SET @MaTT=SCOPE_IDENTITY();
 UPDATE dbo.HoaDon SET TrangThai=CASE WHEN @SoTien=@Rong THEN N'Đã hoàn tiền' ELSE N'Đã thanh toán một phần' END,NgayCapNhat=SYSDATETIME() WHERE MaHD=@MaHD; COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_NhapKho @MaNCC INT,@MaNV INT,@MaSP INT,@SoLuong INT,@DonGiaNhap DECIMAL(18,2),@MaPNK INT OUTPUT AS
BEGIN TRY
 SET XACT_ABORT ON; BEGIN TRAN; IF @SoLuong<=0 OR @DonGiaNhap<0 THROW 50023,N'Dữ liệu nhập kho không hợp lệ.',1;
 DECLARE @Ton INT; SELECT @Ton=SoLuongTon FROM dbo.SanPham WITH(UPDLOCK,HOLDLOCK) WHERE MaSP=@MaSP; IF @Ton IS NULL THROW 50024,N'Sản phẩm không tồn tại.',1;
 INSERT dbo.PhieuNhapKho(MaNCC,MaNV,TongTien) VALUES(@MaNCC,@MaNV,0); SET @MaPNK=SCOPE_IDENTITY();
 INSERT dbo.ChiTietPhieuNhap(MaPNK,MaSP,SoLuong,DonGiaNhap) VALUES(@MaPNK,@MaSP,@SoLuong,@DonGiaNhap);
 UPDATE dbo.PhieuNhapKho SET TongTien=(SELECT SUM(ThanhTien) FROM dbo.ChiTietPhieuNhap WHERE MaPNK=@MaPNK) WHERE MaPNK=@MaPNK;
 UPDATE dbo.SanPham SET SoLuongTon=SoLuongTon+@SoLuong,GiaNhap=@DonGiaNhap,MaNCC=@MaNCC,NgayCapNhat=SYSDATETIME() WHERE MaSP=@MaSP;
 INSERT dbo.LichSuTonKho(MaSP,LoaiBienDong,SoLuong,TonTruoc,TonSau,NguonThamChieu,MaThamChieu,MaNV) VALUES(@MaSP,N'Nhập',@SoLuong,@Ton,@Ton+@SoLuong,N'Phiếu nhập',@MaPNK,@MaNV); COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_XuatKho @MaSP INT,@SoLuong INT,@MaNV INT,@Nguon NVARCHAR(30)=N'Xuất khác',@MaThamChieu INT=NULL AS
BEGIN TRY
 SET XACT_ABORT ON; BEGIN TRAN; DECLARE @Ton INT; SELECT @Ton=SoLuongTon FROM dbo.SanPham WITH(UPDLOCK,HOLDLOCK) WHERE MaSP=@MaSP; IF @SoLuong<=0 OR @Ton IS NULL OR @Ton<@SoLuong THROW 50025,N'Không đủ tồn kho.',1;
 UPDATE dbo.SanPham SET SoLuongTon=SoLuongTon-@SoLuong,NgayCapNhat=SYSDATETIME() WHERE MaSP=@MaSP;
 INSERT dbo.LichSuTonKho(MaSP,LoaiBienDong,SoLuong,TonTruoc,TonSau,NguonThamChieu,MaThamChieu,MaNV) VALUES(@MaSP,N'Xuất',@SoLuong,@Ton,@Ton-@SoLuong,@Nguon,@MaThamChieu,@MaNV); COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_BanSanPham @MaTV INT,@MaNV INT,@MaSP INT,@SoLuong INT,@MaHD INT OUTPUT AS
BEGIN TRY
 SET XACT_ABORT ON; BEGIN TRAN; DECLARE @Gia DECIMAL(18,2),@Ton INT,@MaCT INT;
 SELECT @Gia=DonGia,@Ton=SoLuongTon FROM dbo.SanPham WITH(UPDLOCK,HOLDLOCK) WHERE MaSP=@MaSP AND TrangThai=N'Còn bán'; IF @Gia IS NULL OR @Ton<@SoLuong OR @SoLuong<=0 THROW 50026,N'Sản phẩm không hợp lệ hoặc không đủ tồn.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.ThanhVien WHERE MaTV=@MaTV AND DaXoa=0) THROW 50036,N'Thành viên không tồn tại.',1;
 INSERT dbo.HoaDon(MaTV,MaNV,TongTien,TrangThai) VALUES(@MaTV,@MaNV,0,N'Chưa thanh toán'); SET @MaHD=SCOPE_IDENTITY();
 INSERT dbo.ChiTietHoaDon(MaHD,LoaiDichVu,MaThamChieu,SoLuong,DonGia) VALUES(@MaHD,N'Sản phẩm',@MaSP,@SoLuong,@Gia); SET @MaCT=SCOPE_IDENTITY(); INSERT dbo.HoaDonSanPham(MaCTHD,MaSP) VALUES(@MaCT,@MaSP);
 UPDATE dbo.SanPham SET SoLuongTon=SoLuongTon-@SoLuong,NgayCapNhat=SYSDATETIME() WHERE MaSP=@MaSP;
 INSERT dbo.LichSuTonKho(MaSP,LoaiBienDong,SoLuong,TonTruoc,TonSau,NguonThamChieu,MaThamChieu,MaNV) VALUES(@MaSP,N'Xuất',@SoLuong,@Ton,@Ton-@SoLuong,N'Hóa đơn',@MaHD,@MaNV); COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO
CREATE OR ALTER PROCEDURE dbo.sp_BaoTriThietBi @MaTB INT,@MaNV INT,@NoiDung NVARCHAR(255),@ChiPhi DECIMAL(18,2),@NgayDuKien DATE=NULL,@MaBaoTri INT OUTPUT AS
BEGIN TRY
 SET XACT_ABORT ON; BEGIN TRAN; IF NOT EXISTS(SELECT 1 FROM dbo.ThietBi WHERE MaTB=@MaTB) THROW 50027,N'Thiết bị không tồn tại.',1;
 INSERT dbo.BaoTriThietBi(MaTB,MaNV,NoiDung,ChiPhi,NgayDuKien,TrangThai) VALUES(@MaTB,@MaNV,@NoiDung,@ChiPhi,@NgayDuKien,CASE WHEN @NgayDuKien IS NULL THEN N'Hoàn thành' ELSE N'Chờ bảo trì' END); SET @MaBaoTri=SCOPE_IDENTITY();
 UPDATE dbo.ThietBi SET TinhTrang=CASE WHEN @NgayDuKien IS NULL THEN TinhTrang ELSE N'Bảo trì' END WHERE MaTB=@MaTB; COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO

/* 7. Views bao cao; doanh thu tinh tren giao dich thanh cong, khong JOIN nhan ban. */
CREATE OR ALTER VIEW dbo.vw_DanhSachThanhVien AS SELECT MaTV,MaThanhVien,HoTen,SDT,Email,NgayDangKy,TrangThai,MaLoaiTV FROM dbo.ThanhVien WHERE DaXoa=0;
GO
CREATE OR ALTER VIEW dbo.vw_ThanhVienGoiHieuLuc AS SELECT t.MaTV,t.HoTen,d.MaDKG,g.TenGoi,d.NgayBatDau,d.NgayKetThuc,d.SoLuotDaDung FROM dbo.ThanhVien t JOIN dbo.DangKyGoi d ON d.MaTV=t.MaTV JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi WHERE d.TrangThai=N'Còn hạn' AND CAST(SYSDATETIME() AS DATE) BETWEEN d.NgayBatDau AND d.NgayKetThuc;
GO
CREATE OR ALTER VIEW dbo.vw_ThanhVienSapHetHan AS SELECT t.MaTV,t.HoTen,d.MaDKG,g.TenGoi,d.NgayKetThuc FROM dbo.ThanhVien t JOIN dbo.DangKyGoi d ON d.MaTV=t.MaTV JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi WHERE d.TrangThai=N'Còn hạn' AND d.NgayKetThuc BETWEEN CAST(SYSDATETIME() AS DATE) AND DATEADD(DAY,7,CAST(SYSDATETIME() AS DATE));
GO
CREATE OR ALTER VIEW dbo.vw_DanhSachPT AS SELECT h.MaHLV,n.MaNV,n.HoTen,h.ChuyenMon,h.KinhNghiem,h.PhiThue,n.TrangThai FROM dbo.HuanLuyenVien h JOIN dbo.NhanVien n ON n.MaNV=h.MaNV;
GO
CREATE OR ALTER VIEW dbo.vw_LichHoc AS SELECT lh.MaLichHoc,l.TenLop,n.HoTen AS HLV,p.TenPhong,lh.ThoiGianBatDau,lh.ThoiGianKetThuc,lh.TrangThai FROM dbo.LichHoc lh JOIN dbo.LopTap l ON l.MaLop=lh.MaLop JOIN dbo.HuanLuyenVien h ON h.MaHLV=lh.MaHLV JOIN dbo.NhanVien n ON n.MaNV=h.MaNV JOIN dbo.PhongTap p ON p.MaPhong=lh.MaPhong;
GO
CREATE OR ALTER VIEW dbo.vw_LichSuCheckIn AS SELECT c.MaCheckIn,t.MaTV,t.HoTen,p.TenPhong,c.ThoiGianVao,c.ThoiGianRa,DATEDIFF(MINUTE,c.ThoiGianVao,COALESCE(c.ThoiGianRa,SYSDATETIME())) AS SoPhutTap,c.Kenh FROM dbo.LichSuCheckIn c JOIN dbo.ThanhVien t ON t.MaTV=c.MaTV LEFT JOIN dbo.PhongTap p ON p.MaPhong=c.MaPhong;
GO
CREATE OR ALTER VIEW dbo.vw_DoanhThuTheoNgay AS SELECT CAST(NgayThanhToan AS DATE) Ngay, SUM(CASE WHEN LoaiGiaoDich=N'Thanh toán' THEN SoTien ELSE -SoTien END) DoanhThuThuc FROM dbo.ThanhToan WHERE TrangThai=N'Thành công' GROUP BY CAST(NgayThanhToan AS DATE);
GO
CREATE OR ALTER VIEW dbo.vw_CongNoHoaDon AS SELECT h.MaHD,h.MaTV,h.TongTien,ISNULL(p.DaThanhToan,0) DaThanhToan,h.TongTien-ISNULL(p.DaThanhToan,0) ConNo,h.TrangThai FROM dbo.HoaDon h OUTER APPLY(SELECT SUM(CASE WHEN LoaiGiaoDich=N'Thanh toán' THEN SoTien ELSE -SoTien END) DaThanhToan FROM dbo.ThanhToan WHERE MaHD=h.MaHD AND TrangThai=N'Thành công')p WHERE h.TrangThai<>N'Hủy';
GO
CREATE OR ALTER VIEW dbo.vw_TonKho AS SELECT MaSP,MaSanPham,TenSP,SoLuongTon,TonToiThieu,GiaNhap,DonGia,CONVERT(DECIMAL(18,2),SoLuongTon)*GiaNhap GiaTriTon,CASE WHEN SoLuongTon<=TonToiThieu THEN 1 ELSE 0 END SapHetHang FROM dbo.SanPham;
GO
CREATE OR ALTER VIEW dbo.vw_ThietBiCanBaoTri AS SELECT t.MaTB,t.MaThietBi,t.TenTB,p.TenPhong,t.NgayBaoTriTiepTheo,t.TinhTrang FROM dbo.ThietBi t LEFT JOIN dbo.PhongTap p ON p.MaPhong=t.MaPhong WHERE t.TinhTrang IN(N'Bảo trì',N'Hư hỏng') OR t.NgayBaoTriTiepTheo<=CAST(SYSDATETIME() AS DATE);
GO
