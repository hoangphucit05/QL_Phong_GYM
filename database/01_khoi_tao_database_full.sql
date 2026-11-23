/*
========================================================================================
   HỆ THỐNG QUẢN LÝ PHÒNG GYM (GYM PRO) - MASTER DATABASE SCRIPT
   Tạo toàn bộ CSDL QL_PhongGym, Bảng, Ràng buộc, Triggers, Procedures, Views & Dữ liệu mẫu.
   Yêu cầu: Microsoft SQL Server 2017+ 
   Cách dùng: Mở trong SQL Server Management Studio (SSMS) hoặc Azure Data Studio -> Nhấn F5 (Execute).
========================================================================================
*/
SET NOCOUNT ON;
GO

/* ========================================================================================
   PHẦN: htql_gym_v2.sql
   ======================================================================================== */
GO
CREATE DATABASE QL_PhongGym;
GO
USE QL_PhongGym;
GO

-- 1. LOẠI THÀNH VIÊN
CREATE TABLE LoaiThanhVien (
    MaLoaiTV INT IDENTITY(1,1) PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL UNIQUE,
    MoTa NVARCHAR(255)
);

-- 2. THÀNH VIÊN
CREATE TABLE ThanhVien (
    MaTV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh NVARCHAR(10) CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác')),
    SDT VARCHAR(15) NOT NULL UNIQUE,
    Email VARCHAR(100) UNIQUE,
    DiaChi NVARCHAR(255),
    NgayDangKy DATE DEFAULT GETDATE(),
    MaLoaiTV INT NOT NULL,
    TrangThai NVARCHAR(30) DEFAULT N'Hoạt động'
        CHECK (TrangThai IN (N'Hoạt động', N'Tạm khóa', N'Ngừng sử dụng')),

    CONSTRAINT FK_ThanhVien_LoaiThanhVien
        FOREIGN KEY (MaLoaiTV) REFERENCES LoaiThanhVien(MaLoaiTV),

    CONSTRAINT CK_ThanhVien_NgaySinh
        CHECK (NgaySinh < GETDATE())
);

-- 3. HỒ SƠ SỨC KHỎE
CREATE TABLE HoSoSucKhoe (
    MaHoSo INT IDENTITY(1,1) PRIMARY KEY,
    MaTV INT NOT NULL UNIQUE,
    TinhTrangSucKhoe NVARCHAR(255),
    BenhLyNen NVARCHAR(255),
    GhiChu NVARCHAR(255),
    NgayCapNhat DATE DEFAULT GETDATE(),

    CONSTRAINT FK_HoSoSucKhoe_ThanhVien
        FOREIGN KEY (MaTV) REFERENCES ThanhVien(MaTV)
);

-- 4. CHỈ SỐ CƠ THỂ
CREATE TABLE ChiSoCoThe (
    MaChiSo INT IDENTITY(1,1) PRIMARY KEY,
    MaTV INT NOT NULL,
    ChieuCao DECIMAL(5,2) CHECK (ChieuCao > 0),
    CanNang DECIMAL(5,2) CHECK (CanNang > 0),
    BMI DECIMAL(5,2),
    NgayDo DATE DEFAULT GETDATE(),

    CONSTRAINT FK_ChiSoCoThe_ThanhVien
        FOREIGN KEY (MaTV) REFERENCES ThanhVien(MaTV)
);

-- 5. GÓI TẬP
CREATE TABLE GoiTap (
    MaGoi INT IDENTITY(1,1) PRIMARY KEY,
    TenGoi NVARCHAR(100) NOT NULL UNIQUE,
    ThoiHanNgay INT NOT NULL CHECK (ThoiHanNgay > 0),
    Gia DECIMAL(18,2) NOT NULL CHECK (Gia >= 0),
    MoTa NVARCHAR(255),
    TrangThai NVARCHAR(30) DEFAULT N'Đang áp dụng'
        CHECK (TrangThai IN (N'Đang áp dụng', N'Ngừng áp dụng'))
);

-- 6. KHUYẾN MÃI
CREATE TABLE KhuyenMai (
    MaKM INT IDENTITY(1,1) PRIMARY KEY,
    MaCode NVARCHAR(30) NULL,
    TenKM NVARCHAR(100) NOT NULL,
    PhanTramGiam DECIMAL(5,2) NOT NULL CHECK (PhanTramGiam BETWEEN 0 AND 100),
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    TrangThai NVARCHAR(30) DEFAULT N'Hoạt động'
        CHECK (TrangThai IN (N'Hoạt động', N'Hết hạn')),

    CONSTRAINT CK_KhuyenMai_Ngay
        CHECK (NgayKetThuc >= NgayBatDau)
);

-- 7. ĐĂNG KÝ GÓI TẬP
CREATE TABLE DangKyGoi (
    MaDKG INT IDENTITY(1,1) PRIMARY KEY,
    MaTV INT NOT NULL,
    MaGoi INT NOT NULL,
    MaKM INT NULL,
    NgayBatDau DATE NOT NULL DEFAULT GETDATE(),
    NgayKetThuc DATE NOT NULL,
    GiaSauGiam DECIMAL(18,2) NOT NULL CHECK (GiaSauGiam >= 0),
    TrangThai NVARCHAR(30) DEFAULT N'Còn hạn'
        CHECK (TrangThai IN (N'Còn hạn', N'Hết hạn', N'Hủy')),

    CONSTRAINT FK_DangKyGoi_ThanhVien
        FOREIGN KEY (MaTV) REFERENCES ThanhVien(MaTV),

    CONSTRAINT FK_DangKyGoi_GoiTap
        FOREIGN KEY (MaGoi) REFERENCES GoiTap(MaGoi),

    CONSTRAINT FK_DangKyGoi_KhuyenMai
        FOREIGN KEY (MaKM) REFERENCES KhuyenMai(MaKM),

    CONSTRAINT CK_DangKyGoi_Ngay
        CHECK (NgayKetThuc >= NgayBatDau)
);

-- 8. NHÂN VIÊN
CREATE TABLE NhanVien (
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh NVARCHAR(10) CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác')),
    SDT VARCHAR(15) NOT NULL UNIQUE,
    Email VARCHAR(100) UNIQUE,
    DiaChi NVARCHAR(255),
    ChucVu NVARCHAR(100) NOT NULL,
    Luong DECIMAL(18,2) CHECK (Luong >= 0),
    TrangThai NVARCHAR(30) DEFAULT N'Đang làm'
        CHECK (TrangThai IN (N'Đang làm', N'Nghỉ việc')),

    CONSTRAINT CK_NhanVien_NgaySinh
        CHECK (NgaySinh < GETDATE())
);

-- 9. TÀI KHOẢN
CREATE TABLE TaiKhoan (
    MaTK INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap VARCHAR(50) NOT NULL UNIQUE,
    MatKhau VARCHAR(255) NOT NULL,
    VaiTro NVARCHAR(50) NOT NULL
        CHECK (VaiTro IN (N'Admin', N'Nhân viên', N'HLV')),
    MaNV INT NOT NULL UNIQUE,
    TrangThai NVARCHAR(30) DEFAULT N'Hoạt động'
        CHECK (TrangThai IN (N'Hoạt động', N'Khóa')),

    CONSTRAINT FK_TaiKhoan_NhanVien
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);

-- 10. HUẤN LUYỆN VIÊN
CREATE TABLE HuanLuyenVien (
    MaHLV INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT NOT NULL UNIQUE,
    ChuyenMon NVARCHAR(255),
    KinhNghiem INT CHECK (KinhNghiem >= 0),
    PhiThue DECIMAL(18,2) CHECK (PhiThue >= 0),

    CONSTRAINT FK_HLV_NhanVien
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);

-- 11. ĐĂNG KÝ PT
CREATE TABLE DangKyPT (
    MaDKPT INT IDENTITY(1,1) PRIMARY KEY,
    MaTV INT NOT NULL,
    MaHLV INT NOT NULL,
    MaKM INT NULL,
    SoBuoi INT NOT NULL CHECK (SoBuoi > 0),
    NgayBatDau DATE NOT NULL DEFAULT GETDATE(),
    NgayKetThuc DATE NOT NULL,
    TongTien DECIMAL(18,2) NOT NULL CHECK (TongTien >= 0),
    TrangThai NVARCHAR(30) DEFAULT N'Đang học'
        CHECK (TrangThai IN (N'Đang học', N'Hoàn thành', N'Hủy')),

    CONSTRAINT FK_DangKyPT_ThanhVien
        FOREIGN KEY (MaTV) REFERENCES ThanhVien(MaTV),

    CONSTRAINT FK_DangKyPT_HLV
        FOREIGN KEY (MaHLV) REFERENCES HuanLuyenVien(MaHLV),

    CONSTRAINT FK_DangKyPT_KhuyenMai
        FOREIGN KEY (MaKM) REFERENCES KhuyenMai(MaKM),

    CONSTRAINT CK_DangKyPT_Ngay
        CHECK (NgayKetThuc >= NgayBatDau)
);

-- 12. LOẠI LỚP TẬP
CREATE TABLE LoaiLopTap (
    MaLoaiLop INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiLop NVARCHAR(100) NOT NULL UNIQUE,
    MoTa NVARCHAR(255)
);

-- 13. LỚP TẬP
CREATE TABLE LopTap (
    MaLop INT IDENTITY(1,1) PRIMARY KEY,
    TenLop NVARCHAR(100) NOT NULL,
    MaLoaiLop INT NOT NULL,
    MaHLV INT NOT NULL,
    SucChua INT NOT NULL CHECK (SucChua > 0),
    GioBatDau TIME NOT NULL,
    GioKetThuc TIME NOT NULL,
    TrangThai NVARCHAR(30) DEFAULT N'Đang mở'
        CHECK (TrangThai IN (N'Đang mở', N'Đã đóng')),

    CONSTRAINT FK_LopTap_LoaiLopTap
        FOREIGN KEY (MaLoaiLop) REFERENCES LoaiLopTap(MaLoaiLop),

    CONSTRAINT FK_LopTap_HLV
        FOREIGN KEY (MaHLV) REFERENCES HuanLuyenVien(MaHLV),

    CONSTRAINT CK_LopTap_Gio
        CHECK (GioKetThuc > GioBatDau)
);

-- 14. ĐĂNG KÝ LỚP
CREATE TABLE DangKyLop (
    MaDKL INT IDENTITY(1,1) PRIMARY KEY,
    MaTV INT NOT NULL,
    MaLop INT NOT NULL,
    NgayDangKy DATE DEFAULT GETDATE(),
    TrangThai NVARCHAR(30) DEFAULT N'Đang tham gia'
        CHECK (TrangThai IN (N'Đang tham gia', N'Đã hủy', N'Hoàn thành')),

    CONSTRAINT FK_DangKyLop_ThanhVien
        FOREIGN KEY (MaTV) REFERENCES ThanhVien(MaTV),

    CONSTRAINT FK_DangKyLop_LopTap
        FOREIGN KEY (MaLop) REFERENCES LopTap(MaLop),

    CONSTRAINT UQ_DangKyLop UNIQUE (MaTV, MaLop)
);

-- 15. ĐIỂM DANH LỚP
CREATE TABLE DiemDanhLop (
    MaDD INT IDENTITY(1,1) PRIMARY KEY,
    MaDKL INT NOT NULL,
    NgayDiemDanh DATE NOT NULL DEFAULT GETDATE(),
    TrangThai NVARCHAR(30) NOT NULL
        CHECK (TrangThai IN (N'Có mặt', N'Vắng mặt')),

    CONSTRAINT FK_DiemDanh_DangKyLop
        FOREIGN KEY (MaDKL) REFERENCES DangKyLop(MaDKL),

    CONSTRAINT UQ_DiemDanh UNIQUE (MaDKL, NgayDiemDanh)
);

-- 16. HÓA ĐƠN
CREATE TABLE HoaDon (
    MaHD INT IDENTITY(1,1) PRIMARY KEY,
    MaTV INT NOT NULL,
    MaNV INT NOT NULL,
    NgayLap DATETIME DEFAULT GETDATE(),
    TongTien DECIMAL(18,2) NOT NULL DEFAULT 0 CHECK (TongTien >= 0),
    TrangThai NVARCHAR(30) DEFAULT N'Chưa thanh toán'
        CHECK (TrangThai IN (N'Chưa thanh toán', N'Đã thanh toán', N'Hủy')),

    CONSTRAINT FK_HoaDon_ThanhVien
        FOREIGN KEY (MaTV) REFERENCES ThanhVien(MaTV),

    CONSTRAINT FK_HoaDon_NhanVien
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);

-- 17. CHI TIẾT HÓA ĐƠN
CREATE TABLE ChiTietHoaDon (
    MaCTHD INT IDENTITY(1,1) PRIMARY KEY,
    MaHD INT NOT NULL,
    LoaiDichVu NVARCHAR(50) NOT NULL
        CHECK (LoaiDichVu IN (N'Gói tập', N'PT', N'Lớp tập', N'Sản phẩm')),
    MaThamChieu INT NULL,
    SoLuong INT NOT NULL DEFAULT 1 CHECK (SoLuong > 0),
    DonGia DECIMAL(18,2) NOT NULL CHECK (DonGia >= 0),
    ThanhTien AS (SoLuong * DonGia) PERSISTED,

    CONSTRAINT FK_ChiTietHoaDon_HoaDon
        FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD)
);

-- 18. THANH TOÁN
CREATE TABLE ThanhToan (
    MaTT INT IDENTITY(1,1) PRIMARY KEY,
    MaHD INT NOT NULL,
    SoTien DECIMAL(18,2) NOT NULL CHECK (SoTien > 0),
    PhuongThuc NVARCHAR(50) NOT NULL
        CHECK (PhuongThuc IN (N'Tiền mặt', N'Chuyển khoản', N'Thẻ')),
    NgayThanhToan DATETIME DEFAULT GETDATE(),
    TrangThai NVARCHAR(30) DEFAULT N'Thành công'
        CHECK (TrangThai IN (N'Thành công', N'Thất bại', N'Hoàn tiền')),

    CONSTRAINT FK_ThanhToan_HoaDon
        FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD)
);

-- 19. SẢN PHẨM
CREATE TABLE SanPham (
    MaSP INT IDENTITY(1,1) PRIMARY KEY,
    TenSP NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL CHECK (DonGia >= 0),
    SoLuongTon INT NOT NULL DEFAULT 0 CHECK (SoLuongTon >= 0),
    TrangThai NVARCHAR(30) DEFAULT N'Còn bán'
        CHECK (TrangThai IN (N'Còn bán', N'Ngừng bán'))
);

-- 20. PHIẾU BÁN HÀNG
CREATE TABLE PhieuBanHang (
    MaPBH INT IDENTITY(1,1) PRIMARY KEY,
    MaTV INT NULL,
    MaNV INT NOT NULL,
    NgayBan DATETIME DEFAULT GETDATE(),
    TongTien DECIMAL(18,2) DEFAULT 0 CHECK (TongTien >= 0),

    CONSTRAINT FK_PhieuBanHang_ThanhVien
        FOREIGN KEY (MaTV) REFERENCES ThanhVien(MaTV),

    CONSTRAINT FK_PhieuBanHang_NhanVien
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);

-- 21. CHI TIẾT PHIẾU BÁN HÀNG
CREATE TABLE ChiTietPhieuBanHang (
    MaCTPBH INT IDENTITY(1,1) PRIMARY KEY,
    MaPBH INT NOT NULL,
    MaSP INT NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    DonGia DECIMAL(18,2) NOT NULL CHECK (DonGia >= 0),
    ThanhTien AS (SoLuong * DonGia) PERSISTED,

    CONSTRAINT FK_CTPBH_PhieuBanHang
        FOREIGN KEY (MaPBH) REFERENCES PhieuBanHang(MaPBH),

    CONSTRAINT FK_CTPBH_SanPham
        FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

-- 22. LOẠI THIẾT BỊ
CREATE TABLE LoaiThietBi (
    MaLoaiTB INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiTB NVARCHAR(100) NOT NULL UNIQUE,
    MoTa NVARCHAR(255)
);

-- 23. THIẾT BỊ
CREATE TABLE ThietBi (
    MaTB INT IDENTITY(1,1) PRIMARY KEY,
    TenTB NVARCHAR(100) NOT NULL,
    MaLoaiTB INT NOT NULL,
    NgayMua DATE,
    TinhTrang NVARCHAR(50) DEFAULT N'Hoạt động'
        CHECK (TinhTrang IN (N'Hoạt động', N'Hư hỏng', N'Bảo trì', N'Thanh lý')),

    CONSTRAINT FK_ThietBi_LoaiThietBi
        FOREIGN KEY (MaLoaiTB) REFERENCES LoaiThietBi(MaLoaiTB)
);

-- 24. BẢO TRÌ THIẾT BỊ
CREATE TABLE BaoTriThietBi (
    MaBaoTri INT IDENTITY(1,1) PRIMARY KEY,
    MaTB INT NOT NULL,
    MaNV INT NOT NULL,
    NgayBaoTri DATE NOT NULL DEFAULT GETDATE(),
    NoiDung NVARCHAR(255) NOT NULL,
    ChiPhi DECIMAL(18,2) DEFAULT 0 CHECK (ChiPhi >= 0),

    CONSTRAINT FK_BaoTri_ThietBi
        FOREIGN KEY (MaTB) REFERENCES ThietBi(MaTB),

    CONSTRAINT FK_BaoTri_NhanVien
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);

-- 25. PHÒNG TẬP
CREATE TABLE PhongTap (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong NVARCHAR(100) NOT NULL UNIQUE,
    LoaiPhong NVARCHAR(50) NOT NULL
        CHECK (LoaiPhong IN (N'Phòng gym', N'Phòng yoga', N'Phòng cardio', N'Phòng boxing', N'Phòng đa năng')),
    SucChua INT NOT NULL CHECK (SucChua > 0),
    DienTich DECIMAL(8,2) CHECK (DienTich > 0),
    Tang INT DEFAULT 1,
    TrangThai NVARCHAR(30) DEFAULT N'Hoạt động'
        CHECK (TrangThai IN (N'Hoạt động', N'Đang bảo trì', N'Tạm đóng'))
);

-- Liên kết ThietBi -> PhongTap
ALTER TABLE ThietBi
    ADD MaPhong INT NULL
        CONSTRAINT FK_ThietBi_PhongTap FOREIGN KEY REFERENCES PhongTap(MaPhong);

-- Liên kết LopTap -> PhongTap
ALTER TABLE LopTap
    ADD MaPhong INT NULL
        CONSTRAINT FK_LopTap_PhongTap FOREIGN KEY REFERENCES PhongTap(MaPhong);

-- NHẬP DỮ LIỆU 
USE QL_PhongGym;
GO

-- 1. LoaiThanhVien
INSERT INTO LoaiThanhVien (TenLoai, MoTa) VALUES
(N'Thường', N'Thành viên cơ bản'),
(N'Bạc', N'Thành viên mức bạc'),
(N'Vàng', N'Thành viên mức vàng'),
(N'Kim cương', N'Thành viên cao cấp'),
(N'Sinh viên', N'Ưu đãi cho sinh viên'),
(N'Gia đình', N'Gói dành cho gia đình'),
(N'Doanh nghiệp', N'Khách hàng công ty'),
(N'VIP', N'Dịch vụ ưu tiên'),
(N'Tập thử', N'Thành viên dùng thử'),
(N'Thân thiết', N'Thành viên lâu năm'),
(N'Online', N'Tập luyện có hướng dẫn online'),
(N'PT Plus', N'Ưu tiên PT'),
(N'Fitness Pro', N'Tập luyện chuyên sâu'),
(N'Yoga Member', N'Thành viên yoga'),
(N'Cardio Member', N'Thành viên cardio');

-- 2. ThanhVien
INSERT INTO ThanhVien (HoTen, NgaySinh, GioiTinh, SDT, Email, DiaChi, MaLoaiTV, TrangThai) VALUES
(N'Nguyễn Minh Anh', '2001-03-12', N'Nữ', '0901000001', 'minhanh@gmail.com', N'Hà Nội', 1, N'Hoạt động'),
(N'Trần Hoàng Nam', '1999-07-21', N'Nam', '0901000002', 'hoangnam@gmail.com', N'Hải Phòng', 2, N'Hoạt động'),
(N'Lê Thu Hà', '2002-11-05', N'Nữ', '0901000003', 'thuha@gmail.com', N'Đà Nẵng', 3, N'Hoạt động'),
(N'Phạm Gia Huy', '1998-01-19', N'Nam', '0901000004', 'giahuy@gmail.com', N'TP.HCM', 4, N'Hoạt động'),
(N'Vũ Thanh Tùng', '2000-09-09', N'Nam', '0901000005', 'thanhtung@gmail.com', N'Cần Thơ', 5, N'Hoạt động'),
(N'Đỗ Ngọc Mai', '2003-02-22', N'Nữ', '0901000006', 'ngocmai@gmail.com', N'Hà Nam', 6, N'Hoạt động'),
(N'Bùi Quang Huy', '1997-06-15', N'Nam', '0901000007', 'quanghuy@gmail.com', N'Bắc Ninh', 7, N'Tạm khóa'),
(N'Ngô Lan Hương', '2001-12-30', N'Nữ', '0901000008', 'lanhuong@gmail.com', N'Huế', 8, N'Hoạt động'),
(N'Hoàng Đức Anh', '1996-04-10', N'Nam', '0901000009', 'ducanh@gmail.com', N'Nghệ An', 9, N'Hoạt động'),
(N'Phan Thị Hồng', '2004-08-18', N'Nữ', '0901000010', 'hongphan@gmail.com', N'Thanh Hóa', 10, N'Hoạt động'),
(N'Đặng Quốc Việt', '1995-05-25', N'Nam', '0901000011', 'quocviet@gmail.com', N'Quảng Ninh', 11, N'Ngừng sử dụng'),
(N'Tạ Minh Châu', '2002-10-02', N'Nữ', '0901000012', 'minhchau@gmail.com', N'Hà Nội', 12, N'Hoạt động'),
(N'Cao Tuấn Kiệt', '1999-03-28', N'Nam', '0901000013', 'tuankiet@gmail.com', N'Bình Dương', 13, N'Hoạt động'),
(N'Nguyễn Bảo Ngọc', '2000-01-01', N'Nữ', '0901000014', 'baongoc@gmail.com', N'Đồng Nai', 14, N'Hoạt động'),
(N'Trịnh Văn Long', '1998-12-12', N'Nam', '0901000015', 'vanlong@gmail.com', N'TP.HCM', 15, N'Hoạt động');

-- 3. HoSoSucKhoe
INSERT INTO HoSoSucKhoe (MaTV, TinhTrangSucKhoe, BenhLyNen, GhiChu) VALUES
(1, N'Tốt', N'Không', N'Có thể tập bình thường'),
(2, N'Tốt', N'Không', N'Tập tăng cơ'),
(3, N'Trung bình', N'Huyết áp thấp', N'Tránh tập quá sức'),
(4, N'Tốt', N'Không', N'Tập cường độ cao'),
(5, N'Tốt', N'Không', N'Tập giảm mỡ'),
(6, N'Trung bình', N'Đau lưng nhẹ', N'Cần theo dõi'),
(7, N'Tốt', N'Không', N'Tập duy trì'),
(8, N'Tốt', N'Không', N'Tập yoga'),
(9, N'Trung bình', N'Đau khớp gối', N'Hạn chế chạy bộ'),
(10, N'Tốt', N'Không', N'Tập cardio'),
(11, N'Tốt', N'Không', N'Tập nhẹ'),
(12, N'Tốt', N'Không', N'Tập PT'),
(13, N'Trung bình', N'Thừa cân', N'Tập giảm cân'),
(14, N'Tốt', N'Không', N'Tập yoga'),
(15, N'Tốt', N'Không', N'Tập sức bền');

-- 4. ChiSoCoThe
INSERT INTO ChiSoCoThe (MaTV, ChieuCao, CanNang, BMI) VALUES
(1, 160, 50, 19.53),
(2, 172, 68, 22.99),
(3, 158, 47, 18.83),
(4, 175, 75, 24.49),
(5, 170, 80, 27.68),
(6, 162, 55, 20.96),
(7, 168, 70, 24.80),
(8, 159, 49, 19.38),
(9, 173, 82, 27.40),
(10, 155, 46, 19.15),
(11, 176, 72, 23.24),
(12, 161, 52, 20.06),
(13, 169, 85, 29.76),
(14, 157, 48, 19.47),
(15, 174, 69, 22.79);

-- 5. GoiTap
INSERT INTO GoiTap (TenGoi, ThoiHanNgay, Gia, MoTa) VALUES
(N'Basic 1 tháng', 30, 300000, N'Tập tự do 1 tháng'),
(N'Basic 3 tháng', 90, 800000, N'Tập tự do 3 tháng'),
(N'Basic 6 tháng', 180, 1500000, N'Tập tự do 6 tháng'),
(N'Premium 1 tháng', 30, 500000, N'Có thêm lớp nhóm'),
(N'Premium 3 tháng', 90, 1350000, N'Gói phổ biến'),
(N'Premium 6 tháng', 180, 2500000, N'Tiết kiệm chi phí'),
(N'VIP 1 tháng', 30, 900000, N'Dịch vụ ưu tiên'),
(N'VIP 3 tháng', 90, 2500000, N'Phù hợp tập chuyên sâu'),
(N'VIP 6 tháng', 180, 4800000, N'Tập không giới hạn'),
(N'Sinh viên 1 tháng', 30, 200000, N'Ưu đãi sinh viên'),
(N'Sinh viên 3 tháng', 90, 550000, N'Ưu đãi sinh viên'),
(N'Yoga 1 tháng', 30, 400000, N'Chuyên yoga'),
(N'Cardio 1 tháng', 30, 350000, N'Chuyên cardio'),
(N'PT Combo', 60, 2000000, N'Kèm buổi PT'),
(N'Family 3 tháng', 90, 2200000, N'Dành cho gia đình');

-- 6. KhuyenMai
INSERT INTO KhuyenMai (MaCode, TenKM, PhanTramGiam, NgayBatDau, NgayKetThuc, TrangThai) VALUES
(N'CHAOHE10', N'Chào hè', 10, '2026-05-01', '2026-06-30', N'Hoạt động'),
(N'SINHVIEN15', N'Sinh viên năng động', 15, '2026-01-01', '2026-12-31', N'Hoạt động'),
(N'NEWGYM5', N'Khách hàng mới', 5, '2026-03-01', '2026-08-31', N'Hoạt động'),
(N'BLACKFRIDAY30', N'Black Friday', 30, '2026-11-20', '2026-11-30', N'Hoạt động'),
(N'TET2026', N'Tết khỏe mạnh', 20, '2026-01-01', '2026-02-28', N'Hết hạn'),
(N'FAMILY12', N'Combo gia đình', 12, '2026-04-01', '2026-09-30', N'Hoạt động'),
(N'PROPT10', N'Giảm giá PT', 10, '2026-05-01', '2026-10-31', N'Hoạt động'),
(N'YOGAMAY8', N'Yoga tháng 5', 8, '2026-05-01', '2026-05-31', N'Hoạt động'),
(N'CARDIO7', N'Cardio Day', 7, '2026-06-01', '2026-06-30', N'Hoạt động'),
(N'VIPPRO18', N'Ưu đãi VIP', 18, '2026-05-01', '2026-12-31', N'Hoạt động'),
(N'NOEL25', N'Noel Fit', 25, '2026-12-01', '2026-12-31', N'Hoạt động'),
(N'TRIAN15', N'Tri ân khách hàng', 15, '2026-07-01', '2026-07-31', N'Hoạt động'),
(N'FREETRIAL', N'Tập thử miễn phí', 100, '2026-05-01', '2026-05-20', N'Hết hạn'),
(N'BACK2GYM10', N'Mùa tựu trường', 10, '2026-08-01', '2026-09-15', N'Hoạt động'),
(N'GOLDHEALTH9', N'Sức khỏe vàng', 9, '2026-05-10', '2026-08-10', N'Hoạt động');

-- 7. DangKyGoi
INSERT INTO DangKyGoi (MaTV, MaGoi, MaKM, NgayBatDau, NgayKetThuc, GiaSauGiam, TrangThai) VALUES
(1, 1, 1, '2026-05-01', '2026-05-31', 270000, N'Còn hạn'),
(2, 2, 2, '2026-04-01', '2026-06-30', 680000, N'Còn hạn'),
(3, 3, NULL, '2026-01-01', '2026-06-30', 1500000, N'Còn hạn'),
(4, 4, 3, '2026-05-10', '2026-06-09', 475000, N'Còn hạn'),
(5, 5, 1, '2026-04-15', '2026-07-14', 1215000, N'Còn hạn'),
(6, 6, 6, '2026-03-01', '2026-08-28', 2200000, N'Còn hạn'),
(7, 7, NULL, '2026-02-01', '2026-03-02', 900000, N'Hết hạn'),
(8, 8, 10, '2026-05-01', '2026-07-30', 2050000, N'Còn hạn'),
(9, 9, NULL, '2026-01-01', '2026-06-30', 4800000, N'Còn hạn'),
(10, 10, 2, '2026-05-01', '2026-05-31', 170000, N'Còn hạn'),
(11, 11, NULL, '2026-01-01', '2026-03-31', 550000, N'Hết hạn'),
(12, 12, 8, '2026-05-01', '2026-05-31', 368000, N'Còn hạn'),
(13, 13, 9, '2026-06-01', '2026-06-30', 325500, N'Còn hạn'),
(14, 14, 7, '2026-05-15', '2026-07-14', 1800000, N'Còn hạn'),
(15, 15, 6, '2026-04-01', '2026-06-30', 1936000, N'Còn hạn');

-- 8. NhanVien
INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, SDT, Email, DiaChi, ChucVu, Luong, TrangThai) VALUES
(N'Nguyễn Văn An', '1990-02-10', N'Nam', '0912000001', 'an.nv@gym.com', N'Hà Nội', N'Quản lý', 15000000, N'Đang làm'),
(N'Trần Thị Bình', '1992-06-15', N'Nữ', '0912000002', 'binh.tt@gym.com', N'Hà Nội', N'Lễ tân', 8000000, N'Đang làm'),
(N'Lê Quốc Cường', '1988-03-20', N'Nam', '0912000003', 'cuong.lq@gym.com', N'Hà Nội', N'HLV', 12000000, N'Đang làm'),
(N'Phạm Thị Dung', '1995-09-01', N'Nữ', '0912000004', 'dung.pt@gym.com', N'Hà Nội', N'HLV', 11000000, N'Đang làm'),
(N'Hoàng Minh Đức', '1991-11-11', N'Nam', '0912000005', 'duc.hm@gym.com', N'Đà Nẵng', N'HLV', 13000000, N'Đang làm'),
(N'Vũ Ngọc Hà', '1994-04-22', N'Nữ', '0912000006', 'ha.vn@gym.com', N'Hà Nội', N'HLV', 11500000, N'Đang làm'),
(N'Đặng Tuấn Hải', '1989-08-30', N'Nam', '0912000007', 'hai.dt@gym.com', N'TP.HCM', N'HLV', 12500000, N'Đang làm'),
(N'Bùi Lan Anh', '1996-12-05', N'Nữ', '0912000008', 'lananh.bl@gym.com', N'Hà Nội', N'HLV', 10000000, N'Đang làm'),
(N'Ngô Đức Huy', '1993-05-17', N'Nam', '0912000009', 'huy.nd@gym.com', N'Hà Nội', N'HLV', 11800000, N'Đang làm'),
(N'Cao Thanh Mai', '1997-07-07', N'Nữ', '0912000010', 'mai.ct@gym.com', N'Hà Nội', N'HLV', 10500000, N'Đang làm'),
(N'Tạ Văn Nam', '1990-01-19', N'Nam', '0912000011', 'nam.tv@gym.com', N'Hà Nội', N'Kế toán', 9000000, N'Đang làm'),
(N'Đỗ Hồng Nhung', '1998-10-25', N'Nữ', '0912000012', 'nhung.dh@gym.com', N'Hà Nội', N'Lễ tân', 7500000, N'Đang làm'),
(N'Phan Quốc Phong', '1987-09-09', N'Nam', '0912000013', 'phong.pq@gym.com', N'Hà Nội', N'Bảo trì', 8500000, N'Đang làm'),
(N'Trịnh Minh Quân', '1991-02-28', N'Nam', '0912000014', 'quan.tm@gym.com', N'Hà Nội', N'Nhân viên bán hàng', 7800000, N'Đang làm'),
(N'Lý Thu Trang', '1999-06-18', N'Nữ', '0912000015', 'trang.lt@gym.com', N'Hà Nội', N'Tư vấn viên', 8200000, N'Đang làm');

-- 9. TaiKhoan
INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, MaNV, TrangThai) VALUES
('admin01', '123456', N'Admin', 1, N'Hoạt động'),
('letan01', '123456', N'Nhân viên', 2, N'Hoạt động'),
('hlv01', '123456', N'HLV', 3, N'Hoạt động'),
('hlv02', '123456', N'HLV', 4, N'Hoạt động'),
('hlv03', '123456', N'HLV', 5, N'Hoạt động'),
('hlv04', '123456', N'HLV', 6, N'Hoạt động'),
('hlv05', '123456', N'HLV', 7, N'Hoạt động'),
('hlv06', '123456', N'HLV', 8, N'Hoạt động'),
('hlv07', '123456', N'HLV', 9, N'Hoạt động'),
('hlv08', '123456', N'HLV', 10, N'Hoạt động'),
('ketoan01', '123456', N'Nhân viên', 11, N'Hoạt động'),
('letan02', '123456', N'Nhân viên', 12, N'Hoạt động'),
('baotri01', '123456', N'Nhân viên', 13, N'Hoạt động'),
('banhang01', '123456', N'Nhân viên', 14, N'Hoạt động'),
('tuvan01', '123456', N'Nhân viên', 15, N'Hoạt động');

-- 10. HuanLuyenVien
INSERT INTO HuanLuyenVien (MaNV, ChuyenMon, KinhNghiem, PhiThue) VALUES
(3, N'Tăng cơ, thể hình', 6, 300000),
(4, N'Yoga, Pilates', 5, 250000),
(5, N'Giảm mỡ, cardio', 7, 280000),
(6, N'Yoga trị liệu', 4, 220000),
(7, N'Fitness nâng cao', 8, 350000),
(8, N'Aerobic, Zumba', 3, 200000),
(9, N'Powerlifting', 6, 320000),
(10, N'Cardio cơ bản', 2, 180000);

-- 11. DangKyPT
INSERT INTO DangKyPT (MaTV, MaHLV, MaKM, SoBuoi, NgayBatDau, NgayKetThuc, TongTien, TrangThai) VALUES
(1, 1, 7, 10, '2026-05-01', '2026-06-01', 2700000, N'Đang học'),
(2, 2, NULL, 8, '2026-05-02', '2026-06-02', 2000000, N'Đang học'),
(3, 3, 7, 12, '2026-04-15', '2026-06-15', 3024000, N'Đang học'),
(4, 4, NULL, 6, '2026-05-10', '2026-06-10', 1320000, N'Đang học'),
(5, 5, 10, 15, '2026-05-01', '2026-07-01', 4305000, N'Đang học'),
(6, 6, NULL, 5, '2026-03-01', '2026-04-01', 1000000, N'Hoàn thành'),
(7, 7, NULL, 10, '2026-02-01', '2026-03-01', 3200000, N'Hoàn thành'),
(8, 8, 8, 8, '2026-05-03', '2026-06-03', 1324800, N'Đang học'),
(9, 1, NULL, 6, '2026-05-11', '2026-06-11', 1800000, N'Đang học'),
(10, 2, 2, 10, '2026-05-01', '2026-06-01', 2125000, N'Đang học'),
(11, 3, NULL, 5, '2026-01-01', '2026-02-01', 1400000, N'Hủy'),
(12, 4, 7, 10, '2026-05-05', '2026-06-05', 1980000, N'Đang học'),
(13, 5, NULL, 12, '2026-05-01', '2026-07-01', 4200000, N'Đang học'),
(14, 6, 8, 6, '2026-05-12', '2026-06-12', 1104000, N'Đang học'),
(15, 7, NULL, 8, '2026-05-01', '2026-06-01', 2560000, N'Đang học');

-- 12. LoaiLopTap
INSERT INTO LoaiLopTap (TenLoaiLop, MoTa) VALUES
(N'Yoga', N'Lớp yoga'),
(N'Zumba', N'Lớp nhảy giảm mỡ'),
(N'Cardio', N'Tập tim mạch'),
(N'HIIT', N'Cường độ cao'),
(N'Pilates', N'Tập kiểm soát cơ thể'),
(N'Boxing', N'Tập boxing'),
(N'Aerobic', N'Tập nhịp điệu'),
(N'Gym cơ bản', N'Hướng dẫn gym cơ bản'),
(N'Powerlifting', N'Tập sức mạnh'),
(N'Stretching', N'Giãn cơ'),
(N'Crossfit', N'Tập tổng hợp'),
(N'Body Pump', N'Tập với tạ nhẹ'),
(N'Core Training', N'Tập cơ bụng'),
(N'Cycling', N'Đạp xe trong nhà'),
(N'Meditation', N'Thiền và thư giãn');

-- 13. LopTap
INSERT INTO LopTap (TenLop, MaLoaiLop, MaHLV, SucChua, GioBatDau, GioKetThuc) VALUES
(N'Yoga sáng', 1, 2, 25, '06:00', '07:00'),
(N'Zumba tối', 2, 6, 30, '18:00', '19:00'),
(N'Cardio cơ bản', 3, 8, 20, '07:00', '08:00'),
(N'HIIT nâng cao', 4, 3, 18, '19:00', '20:00'),
(N'Pilates nữ', 5, 4, 20, '17:00', '18:00'),
(N'Boxing cơ bản', 6, 5, 15, '18:30', '19:30'),
(N'Aerobic vui khỏe', 7, 6, 30, '16:00', '17:00'),
(N'Gym nhập môn', 8, 1, 20, '08:00', '09:00'),
(N'Powerlifting A', 9, 7, 12, '20:00', '21:00'),
(N'Stretching tối', 10, 2, 25, '20:00', '20:45'),
(N'Crossfit sáng', 11, 5, 15, '06:30', '07:30'),
(N'Body Pump', 12, 1, 20, '17:30', '18:30'),
(N'Core Training', 13, 3, 20, '19:30', '20:30'),
(N'Cycling giảm mỡ', 14, 8, 18, '18:00', '19:00'),
(N'Meditation thư giãn', 15, 4, 30, '21:00', '21:45');

-- 14. DangKyLop
INSERT INTO DangKyLop (MaTV, MaLop, TrangThai) VALUES
(1, 1, N'Đang tham gia'),
(2, 2, N'Đang tham gia'),
(3, 3, N'Đang tham gia'),
(4, 4, N'Đang tham gia'),
(5, 5, N'Đang tham gia'),
(6, 6, N'Đang tham gia'),
(7, 7, N'Đã hủy'),
(8, 8, N'Đang tham gia'),
(9, 9, N'Đang tham gia'),
(10, 10, N'Đang tham gia'),
(11, 11, N'Hoàn thành'),
(12, 12, N'Đang tham gia'),
(13, 13, N'Đang tham gia'),
(14, 14, N'Đang tham gia'),
(15, 15, N'Đang tham gia');

-- 15. DiemDanhLop
INSERT INTO DiemDanhLop (MaDKL, NgayDiemDanh, TrangThai) VALUES
(1, '2026-05-20', N'Có mặt'),
(2, '2026-05-20', N'Có mặt'),
(3, '2026-05-20', N'Vắng mặt'),
(4, '2026-05-20', N'Có mặt'),
(5, '2026-05-20', N'Có mặt'),
(6, '2026-05-20', N'Có mặt'),
(7, '2026-05-20', N'Vắng mặt'),
(8, '2026-05-20', N'Có mặt'),
(9, '2026-05-20', N'Có mặt'),
(10, '2026-05-20', N'Có mặt'),
(11, '2026-05-20', N'Vắng mặt'),
(12, '2026-05-20', N'Có mặt'),
(13, '2026-05-20', N'Có mặt'),
(14, '2026-05-20', N'Có mặt'),
(15, '2026-05-20', N'Có mặt');

-- 16. HoaDon
INSERT INTO HoaDon (MaTV, MaNV, TongTien, TrangThai) VALUES
(1, 2, 270000, N'Đã thanh toán'),
(2, 2, 680000, N'Đã thanh toán'),
(3, 11, 1500000, N'Đã thanh toán'),
(4, 2, 475000, N'Chưa thanh toán'),
(5, 11, 1215000, N'Đã thanh toán'),
(6, 2, 2200000, N'Đã thanh toán'),
(7, 11, 900000, N'Đã thanh toán'),
(8, 2, 2050000, N'Đã thanh toán'),
(9, 11, 4800000, N'Chưa thanh toán'),
(10, 2, 170000, N'Đã thanh toán'),
(11, 11, 550000, N'Hủy'),
(12, 2, 368000, N'Đã thanh toán'),
(13, 11, 325500, N'Đã thanh toán'),
(14, 2, 1800000, N'Đã thanh toán'),
(15, 11, 1936000, N'Chưa thanh toán');

-- 17. ChiTietHoaDon
INSERT INTO ChiTietHoaDon (MaHD, LoaiDichVu, MaThamChieu, SoLuong, DonGia) VALUES
(1, N'Gói tập', 1, 1, 270000),
(2, N'Gói tập', 2, 1, 680000),
(3, N'Gói tập', 3, 1, 1500000),
(4, N'Gói tập', 4, 1, 475000),
(5, N'Gói tập', 5, 1, 1215000),
(6, N'Gói tập', 6, 1, 2200000),
(7, N'Gói tập', 7, 1, 900000),
(8, N'Gói tập', 8, 1, 2050000),
(9, N'Gói tập', 9, 1, 4800000),
(10, N'Gói tập', 10, 1, 170000),
(11, N'Gói tập', 11, 1, 550000),
(12, N'Gói tập', 12, 1, 368000),
(13, N'Gói tập', 13, 1, 325500),
(14, N'PT', 14, 1, 1800000),
(15, N'Gói tập', 15, 1, 1936000);

-- 18. ThanhToan
INSERT INTO ThanhToan (MaHD, SoTien, PhuongThuc, TrangThai) VALUES
(1, 270000, N'Tiền mặt', N'Thành công'),
(2, 680000, N'Chuyển khoản', N'Thành công'),
(3, 1500000, N'Thẻ', N'Thành công'),
(5, 1215000, N'Chuyển khoản', N'Thành công'),
(6, 2200000, N'Tiền mặt', N'Thành công'),
(7, 900000, N'Thẻ', N'Thành công'),
(8, 2050000, N'Chuyển khoản', N'Thành công'),
(10, 170000, N'Tiền mặt', N'Thành công'),
(12, 368000, N'Thẻ', N'Thành công'),
(13, 325500, N'Chuyển khoản', N'Thành công'),
(14, 1800000, N'Tiền mặt', N'Thành công'),
(4, 100000, N'Tiền mặt', N'Thất bại'),
(9, 500000, N'Chuyển khoản', N'Thất bại'),
(11, 550000, N'Tiền mặt', N'Hoàn tiền'),
(15, 1000000, N'Thẻ', N'Thất bại');

-- Nếu dòng đầu bị lỗi do gõ sai trạng thái, dùng dòng này thay thế:
-- UPDATE ThanhToan SET TrangThai = N'Thành công' WHERE MaTT = 1;

-- 19. SanPham
INSERT INTO SanPham (TenSP, DonGia, SoLuongTon) VALUES
(N'Whey Protein 1kg', 850000, 30),
(N'Bình nước thể thao', 120000, 100),
(N'Găng tay tập gym', 150000, 60),
(N'Dây kháng lực', 90000, 80),
(N'Áo tập nam', 180000, 50),
(N'Áo tập nữ', 180000, 50),
(N'Khăn tập gym', 70000, 120),
(N'BCAA', 500000, 25),
(N'Pre-workout', 650000, 20),
(N'Thanh protein', 35000, 200),
(N'Đai lưng tập gym', 300000, 35),
(N'Dây nhảy', 80000, 90),
(N'Túi thể thao', 250000, 40),
(N'Băng cổ tay', 100000, 70),
(N'Sữa tăng cân', 950000, 15);

-- 20. PhieuBanHang
INSERT INTO PhieuBanHang (MaTV, MaNV, TongTien) VALUES
(1, 14, 120000),
(2, 14, 150000),
(3, 14, 850000),
(4, 14, 90000),
(5, 14, 180000),
(6, 14, 70000),
(7, 14, 500000),
(8, 14, 650000),
(9, 14, 35000),
(10, 14, 300000),
(11, 14, 80000),
(12, 14, 250000),
(13, 14, 100000),
(14, 14, 950000),
(15, 14, 240000);

-- 21. ChiTietPhieuBanHang
INSERT INTO ChiTietPhieuBanHang (MaPBH, MaSP, SoLuong, DonGia) VALUES
(1, 2, 1, 120000),
(2, 3, 1, 150000),
(3, 1, 1, 850000),
(4, 4, 1, 90000),
(5, 5, 1, 180000),
(6, 7, 1, 70000),
(7, 8, 1, 500000),
(8, 9, 1, 650000),
(9, 10, 1, 35000),
(10, 11, 1, 300000),
(11, 12, 1, 80000),
(12, 13, 1, 250000),
(13, 14, 1, 100000),
(14, 15, 1, 950000),
(15, 2, 2, 120000);

-- 22. LoaiThietBi
INSERT INTO LoaiThietBi (TenLoaiTB, MoTa) VALUES
(N'Máy chạy bộ', N'Thiết bị chạy bộ'),
(N'Xe đạp tập', N'Tập cardio'),
(N'Máy kéo xô', N'Tập lưng'),
(N'Máy ép ngực', N'Tập ngực'),
(N'Tạ đơn', N'Tập tay'),
(N'Tạ đòn', N'Tập sức mạnh'),
(N'Ghế tập', N'Hỗ trợ tập tạ'),
(N'Máy squat', N'Tập chân'),
(N'Máy gập bụng', N'Tập cơ bụng'),
(N'Dây cáp', N'Tập đa năng'),
(N'Thảm yoga', N'Tập yoga'),
(N'Bóng tập', N'Tập core'),
(N'Máy chèo thuyền', N'Tập toàn thân'),
(N'Máy rung', N'Thư giãn cơ'),
(N'Khung power rack', N'Tập sức mạnh');

-- 23. ThietBi
INSERT INTO ThietBi (TenTB, MaLoaiTB, NgayMua, TinhTrang) VALUES
(N'Máy chạy bộ A1', 1, '2024-01-10', N'Hoạt động'),
(N'Xe đạp tập B1', 2, '2024-02-15', N'Hoạt động'),
(N'Máy kéo xô C1', 3, '2023-12-01', N'Hoạt động'),
(N'Máy ép ngực D1', 4, '2024-03-20', N'Bảo trì'),
(N'Tạ đơn 10kg', 5, '2023-11-11', N'Hoạt động'),
(N'Tạ đòn Olympic', 6, '2024-04-01', N'Hoạt động'),
(N'Ghế tập Bench', 7, '2024-01-25', N'Hoạt động'),
(N'Máy squat S1', 8, '2023-10-10', N'Hư hỏng'),
(N'Máy gập bụng G1', 9, '2024-02-02', N'Hoạt động'),
(N'Dây cáp đa năng', 10, '2024-03-03', N'Bảo trì'),
(N'Thảm yoga xanh', 11, '2025-01-01', N'Hoạt động'),
(N'Bóng tập core', 12, '2025-02-01', N'Hoạt động'),
(N'Máy chèo thuyền R1', 13, '2024-06-01', N'Hoạt động'),
(N'Máy rung thư giãn', 14, '2024-07-01', N'Thanh lý'),
(N'Power Rack PR1', 15, '2024-08-01', N'Hoạt động');

-- 24. BaoTriThietBi
INSERT INTO BaoTriThietBi (MaTB, MaNV, NgayBaoTri, NoiDung, ChiPhi) VALUES
(1, 13, '2026-05-01', N'Kiểm tra băng tải', 200000),
(2, 13, '2026-05-02', N'Vệ sinh động cơ', 150000),
(3, 13, '2026-05-03', N'Tra dầu cáp kéo', 100000),
(4, 13, '2026-05-04', N'Thay ốc cố định', 250000),
(5, 13, '2026-05-05', N'Kiểm tra tay cầm', 50000),
(6, 13, '2026-05-06', N'Vệ sinh thanh đòn', 70000),
(7, 13, '2026-05-07', N'Siết lại khớp ghế', 120000),
(8, 13, '2026-05-08', N'Sửa bộ phận khóa an toàn', 400000),
(9, 13, '2026-05-09', N'Kiểm tra đệm lưng', 90000),
(10, 13, '2026-05-10', N'Thay dây cáp', 350000),
(11, 13, '2026-05-11', N'Vệ sinh thảm', 30000),
(12, 13, '2026-05-12', N'Bơm bóng tập', 20000),
(13, 13, '2026-05-13', N'Kiểm tra tay kéo', 180000),
(14, 13, '2026-05-14', N'Đánh giá thanh lý', 0),
(15, 13, '2026-05-15', N'Kiểm tra khung chịu lực', 220000);

-- 25. PhongTap
INSERT INTO PhongTap (TenPhong, LoaiPhong, SucChua, DienTich, Tang, TrangThai) VALUES
(N'Phòng Gym Tổng Hợp',   N'Phòng gym',      50, 200.00, 1, N'Hoạt động'),
(N'Phòng Yoga A',          N'Phòng yoga',     25,  80.00, 2, N'Hoạt động'),
(N'Phòng Cardio',          N'Phòng cardio',   30, 120.00, 1, N'Hoạt động'),
(N'Phòng Boxing',          N'Phòng boxing',   20,  90.00, 2, N'Hoạt động'),
(N'Phòng Đa Năng B',       N'Phòng đa năng',  40, 150.00, 3, N'Hoạt động'),
(N'Phòng Yoga B',          N'Phòng yoga',     25,  75.00, 3, N'Đang bảo trì'),
(N'Phòng Gym Nâng Cao',    N'Phòng gym',      30, 100.00, 1, N'Hoạt động');

-- Gán phòng cho ThietBi (15 thiết bị -> 7 phòng)
UPDATE ThietBi SET MaPhong = 1 WHERE MaTB IN (1, 5, 6, 7, 15);  -- Gym tổng hợp
UPDATE ThietBi SET MaPhong = 2 WHERE MaTB IN (11, 12);           -- Yoga A
UPDATE ThietBi SET MaPhong = 3 WHERE MaTB IN (2, 9, 10);         -- Cardio
UPDATE ThietBi SET MaPhong = 4 WHERE MaTB IN (8);                -- Boxing
UPDATE ThietBi SET MaPhong = 5 WHERE MaTB IN (3, 4);             -- Đa năng B
UPDATE ThietBi SET MaPhong = 7 WHERE MaTB IN (13, 14);           -- Gym nâng cao

-- Gán phòng cho LopTap (15 lớp -> 7 phòng)
UPDATE LopTap SET MaPhong = 2 WHERE MaLop IN (1, 5, 10, 15);    -- Yoga A
UPDATE LopTap SET MaPhong = 3 WHERE MaLop IN (3, 14);            -- Cardio
UPDATE LopTap SET MaPhong = 4 WHERE MaLop IN (6);                -- Boxing
UPDATE LopTap SET MaPhong = 1 WHERE MaLop IN (8, 12);            -- Gym tổng hợp
UPDATE LopTap SET MaPhong = 5 WHERE MaLop IN (2, 7);             -- Đa năng B
UPDATE LopTap SET MaPhong = 7 WHERE MaLop IN (4, 9, 11, 13);    -- Gym nâng cao

--THỰC HIỆN TRUY VẤN

-- 1. Lấy danh sách thành viên
SELECT MaTV, HoTen, SDT, Email, TrangThai
FROM ThanhVien;

-- 2. Lấy danh sách thành viên đang hoạt động
SELECT MaTV, HoTen, SDT, NgayDangKy
FROM ThanhVien
WHERE TrangThai = N'Hoạt động';

-- 3. Tìm kiếm thành viên theo họ tên
SELECT MaTV, HoTen, SDT
FROM ThanhVien
WHERE HoTen LIKE N'%Nguyễn%';

-- 4. Sắp xếp thành viên theo ngày đăng ký mới nhất
SELECT MaTV, HoTen, NgayDangKy
FROM ThanhVien
ORDER BY NgayDangKy DESC;

-- 5. Lấy danh sách gói tập đang áp dụng
SELECT MaGoi, TenGoi, ThoiHanNgay, Gia
FROM GoiTap
WHERE TrangThai = N'Đang áp dụng';

-- 6. Lấy danh sách gói tập có giá từ 500000 trở lên
SELECT MaGoi, TenGoi, Gia
FROM GoiTap
WHERE Gia >= 500000
ORDER BY Gia DESC;

-- 7. Hiển thị thông tin đăng ký gói tập của thành viên
SELECT 
    tv.HoTen AS ThanhVien,
    gt.TenGoi,
    dkg.NgayBatDau,
    dkg.NgayKetThuc,
    dkg.TrangThai
FROM DangKyGoi dkg
JOIN ThanhVien tv ON dkg.MaTV = tv.MaTV
JOIN GoiTap gt ON dkg.MaGoi = gt.MaGoi;

-- 8. Hiển thị thành viên sắp hết hạn gói tập trong 7 ngày tới
SELECT 
    tv.HoTen,
    gt.TenGoi,
    dkg.NgayKetThuc
FROM DangKyGoi dkg
JOIN ThanhVien tv ON dkg.MaTV = tv.MaTV
JOIN GoiTap gt ON dkg.MaGoi = gt.MaGoi
WHERE dkg.NgayKetThuc BETWEEN GETDATE() AND DATEADD(DAY, 7, GETDATE());

-- 9. Hiển thị danh sách lớp tập và huấn luyện viên phụ trách
SELECT 
    lt.TenLop,
    llt.TenLoaiLop,
    nv.HoTen AS HuanLuyenVien,
    lt.SucChua,
    lt.GioBatDau,
    lt.GioKetThuc
FROM LopTap lt
JOIN LoaiLopTap llt ON lt.MaLoaiLop = llt.MaLoaiLop
JOIN HuanLuyenVien hlv ON lt.MaHLV = hlv.MaHLV
JOIN NhanVien nv ON hlv.MaNV = nv.MaNV;

-- 10. Hiển thị danh sách thành viên đăng ký lớp tập
SELECT 
    tv.HoTen AS ThanhVien,
    lt.TenLop,
    dkl.NgayDangKy,
    dkl.TrangThai
FROM DangKyLop dkl
JOIN ThanhVien tv ON dkl.MaTV = tv.MaTV
JOIN LopTap lt ON dkl.MaLop = lt.MaLop;

-- 11. Thống kê số lượng thành viên theo loại thành viên
SELECT 
    ltv.TenLoai,
    COUNT(tv.MaTV) AS SoLuongThanhVien
FROM LoaiThanhVien ltv
LEFT JOIN ThanhVien tv ON ltv.MaLoaiTV = tv.MaLoaiTV
GROUP BY ltv.TenLoai;

-- 12. Thống kê số lượng đăng ký của từng gói tập
SELECT 
    gt.TenGoi,
    COUNT(dkg.MaDKG) AS SoLanDangKy
FROM GoiTap gt
LEFT JOIN DangKyGoi dkg ON gt.MaGoi = dkg.MaGoi
GROUP BY gt.TenGoi
ORDER BY SoLanDangKy DESC;

-- 13. Thống kê doanh thu từ hóa đơn đã thanh toán
SELECT 
    SUM(TongTien) AS TongDoanhThu
FROM HoaDon
WHERE TrangThai = N'Đã thanh toán';

-- 14. Thống kê doanh thu hóa đơn theo tháng
SELECT 
    YEAR(NgayLap) AS Nam,
    MONTH(NgayLap) AS Thang,
    SUM(TongTien) AS DoanhThu
FROM HoaDon
WHERE TrangThai = N'Đã thanh toán'
GROUP BY YEAR(NgayLap), MONTH(NgayLap)
ORDER BY Nam, Thang;

-- 15. Thống kê các gói tập có từ 2 lượt đăng ký trở lên
SELECT 
    gt.TenGoi,
    COUNT(dkg.MaDKG) AS SoLanDangKy
FROM GoiTap gt
JOIN DangKyGoi dkg ON gt.MaGoi = dkg.MaGoi
GROUP BY gt.TenGoi
HAVING COUNT(dkg.MaDKG) >= 2;

-- 16. Hiển thị thành viên có hóa đơn lớn hơn mức trung bình
SELECT 
    tv.HoTen,
    hd.MaHD,
    hd.TongTien
FROM HoaDon hd
JOIN ThanhVien tv ON hd.MaTV = tv.MaTV
WHERE hd.TongTien > (
    SELECT AVG(TongTien)
    FROM HoaDon
);

-- 17. Hiển thị thành viên đã đăng ký huấn luyện viên cá nhân
SELECT 
    tv.MaTV,
    tv.HoTen,
    tv.SDT
FROM ThanhVien tv
WHERE tv.MaTV IN (
    SELECT MaTV
    FROM DangKyPT
);

-- 18. Hiển thị thành viên chưa đăng ký lớp tập
SELECT 
    tv.MaTV,
    tv.HoTen,
    tv.SDT
FROM ThanhVien tv
WHERE NOT EXISTS (
    SELECT 1
    FROM DangKyLop dkl
    WHERE dkl.MaTV = tv.MaTV
);

-- 19. Phân loại hóa đơn theo giá trị thanh toán
SELECT 
    MaHD,
    TongTien,
    CASE
        WHEN TongTien >= 3000000 THEN N'Cao'
        WHEN TongTien >= 1000000 THEN N'Trung bình'
        ELSE N'Thấp'
    END AS MucHoaDon
FROM HoaDon;

-- 20. Tổng hợp thông tin đăng ký và thanh toán của từng thành viên
SELECT 
    tv.MaTV,
    tv.HoTen,
    COUNT(DISTINCT dkg.MaDKG) AS SoGoiDaDangKy,
    COUNT(DISTINCT dkpt.MaDKPT) AS SoLanDangKyPT,
    COUNT(DISTINCT dkl.MaDKL) AS SoLopDaDangKy,
    SUM(CASE 
            WHEN hd.TrangThai = N'Đã thanh toán' THEN hd.TongTien 
            ELSE 0 
        END) AS TongTienDaThanhToan
FROM ThanhVien tv
LEFT JOIN DangKyGoi dkg ON tv.MaTV = dkg.MaTV
LEFT JOIN DangKyPT dkpt ON tv.MaTV = dkpt.MaTV
LEFT JOIN DangKyLop dkl ON tv.MaTV = dkl.MaTV
LEFT JOIN HoaDon hd ON tv.MaTV = hd.MaTV
GROUP BY tv.MaTV, tv.HoTen
ORDER BY TongTienDaThanhToan DESC;

-- 21. Danh sách phòng tập và trạng thái
SELECT MaPhong, TenPhong, LoaiPhong, SucChua, DienTich, Tang, TrangThai
FROM PhongTap
ORDER BY Tang, TenPhong;

-- 22. Danh sách thiết bị theo từng phòng
SELECT 
    pt.TenPhong,
    pt.LoaiPhong,
    tb.TenTB,
    ltb.TenLoaiTB,
    tb.TinhTrang
FROM ThietBi tb
JOIN PhongTap pt ON tb.MaPhong = pt.MaPhong
JOIN LoaiThietBi ltb ON tb.MaLoaiTB = ltb.MaLoaiTB
ORDER BY pt.TenPhong, ltb.TenLoaiTB;

-- 23. Thống kê số lượng thiết bị theo phòng
SELECT 
    pt.TenPhong,
    COUNT(tb.MaTB) AS TongThietBi,
    SUM(CASE WHEN tb.TinhTrang = N'Hoạt động' THEN 1 ELSE 0 END) AS DangHoatDong,
    SUM(CASE WHEN tb.TinhTrang = N'Bảo trì'   THEN 1 ELSE 0 END) AS DangBaoTri,
    SUM(CASE WHEN tb.TinhTrang = N'Hư hỏng'   THEN 1 ELSE 0 END) AS HuHong
FROM PhongTap pt
LEFT JOIN ThietBi tb ON pt.MaPhong = tb.MaPhong
GROUP BY pt.TenPhong
ORDER BY TongThietBi DESC;

-- 24. Danh sách lớp tập theo phòng và huấn luyện viên
SELECT 
    pt.TenPhong,
    lt.TenLop,
    llt.TenLoaiLop,
    nv.HoTen AS HuanLuyenVien,
    lt.GioBatDau,
    lt.GioKetThuc,
    lt.SucChua,
    lt.TrangThai
FROM LopTap lt
JOIN PhongTap pt  ON lt.MaPhong = pt.MaPhong
JOIN LoaiLopTap llt ON lt.MaLoaiLop = llt.MaLoaiLop
JOIN HuanLuyenVien hlv ON lt.MaHLV = hlv.MaHLV
JOIN NhanVien nv ON hlv.MaNV = nv.MaNV
ORDER BY pt.TenPhong, lt.GioBatDau;

-- 25. Phòng tập đang có lớp hoạt động và tỉ lệ lấp đầy
SELECT 
    pt.TenPhong,
    pt.SucChua AS SucChuaPhong,
    COUNT(dkl.MaDKL) AS SoThanhVienDangHoc,
    CAST(COUNT(dkl.MaDKL) * 100.0 / pt.SucChua AS DECIMAL(5,1)) AS TiLeLapDay
FROM PhongTap pt
JOIN LopTap lt ON pt.MaPhong = lt.MaPhong
JOIN DangKyLop dkl ON lt.MaLop = dkl.MaLop
WHERE lt.TrangThai = N'Đang mở'
  AND dkl.TrangThai = N'Đang tham gia'
GROUP BY pt.TenPhong, pt.SucChua
ORDER BY TiLeLapDay DESC;

-- 26. Phòng tập chưa có thiết bị nào (kiểm tra dữ liệu)
SELECT pt.MaPhong, pt.TenPhong, pt.LoaiPhong
FROM PhongTap pt
WHERE NOT EXISTS (
    SELECT 1 FROM ThietBi tb WHERE tb.MaPhong = pt.MaPhong
);

-- 27. Chi phí bảo trì thiết bị theo phòng
SELECT 
    pt.TenPhong,
    COUNT(btt.MaBaoTri) AS SoLanBaoTri,
    SUM(btt.ChiPhi) AS TongChiPhiBaoTri
FROM PhongTap pt
JOIN ThietBi tb ON pt.MaPhong = tb.MaPhong
JOIN BaoTriThietBi btt ON tb.MaTB = btt.MaTB
GROUP BY pt.TenPhong
ORDER BY TongChiPhiBaoTri DESC;

-- Câu 28: Danh sách thành viên đang hoạt động
SELECT MaTV, HoTen, SDT
FROM ThanhVien
WHERE TrangThai = N'Hoạt động';

-- Câu 29: Danh sách gói tập đang áp dụng
SELECT *
FROM GoiTap
WHERE TrangThai = N'Đang áp dụng';

-- Câu 30: Danh sách thiết bị đang hoạt động
SELECT *
FROM ThietBi
WHERE TinhTrang = N'Hoạt động';

-- Câu 31: Huấn luyện viên có trên 3 năm kinh nghiệm
SELECT *
FROM HuanLuyenVien
WHERE KinhNghiem > 3;

-- Câu 32: Đếm số lượng thành viên theo loại thành viên
SELECT LTV.TenLoai,
       COUNT(TV.MaTV) AS SoLuongThanhVien
FROM LoaiThanhVien LTV
LEFT JOIN ThanhVien TV ON LTV.MaLoaiTV = TV.MaLoaiTV
GROUP BY LTV.TenLoai;

-- Câu 33: Tổng doanh thu từ đăng ký gói tập
SELECT SUM(GiaSauGiam) AS TongDoanhThu
FROM DangKyGoi;

-- Câu 34: Tổng doanh thu bán sản phẩm
SELECT SUM(TongTien) AS TongDoanhThuBanHang
FROM PhieuBanHang;

-- Câu 35: Số lượng hóa đơn theo từng tháng
SELECT MONTH(NgayLap) AS Thang,
       COUNT(*) AS SoHoaDon
FROM HoaDon
GROUP BY MONTH(NgayLap)
ORDER BY Thang;

-- Câu 36: Thành viên sắp hết hạn gói tập trong 7 ngày tới
SELECT TV.HoTen,
       DKG.NgayKetThuc
FROM DangKyGoi DKG
JOIN ThanhVien TV ON DKG.MaTV = TV.MaTV
WHERE DKG.NgayKetThuc BETWEEN GETDATE() AND DATEADD(DAY, 7, GETDATE());

-- Câu 37: Thành viên chưa từng đăng ký PT
SELECT *
FROM ThanhVien
WHERE MaTV NOT IN (
    SELECT MaTV
    FROM DangKyPT
);

-- Câu 38: Thiết bị đang cần bảo trì
SELECT TB.TenTB,
       BT.NgayBaoTri
FROM ThietBi TB
JOIN BaoTriThietBi BT ON TB.MaTB = BT.MaTB
WHERE TB.TinhTrang IN (N'Bảo trì', N'Hư hỏng');

-- Câu 39: Huấn luyện viên có nhiều học viên nhất
SELECT TOP 1
       HLV.MaHLV,
       NV.HoTen,
       COUNT(*) AS SoHocVien
FROM DangKyPT DK
JOIN HuanLuyenVien HLV ON DK.MaHLV = HLV.MaHLV
JOIN NhanVien NV ON HLV.MaNV = NV.MaNV
GROUP BY HLV.MaHLV, NV.HoTen
ORDER BY SoHocVien DESC;

-- Câu 40: Top 5 thành viên chi tiêu nhiều nhất
SELECT TOP 5
       TV.MaTV,
       TV.HoTen,
       SUM(HD.TongTien) AS TongChiTieu
FROM ThanhVien TV
JOIN HoaDon HD ON TV.MaTV = HD.MaTV
GROUP BY TV.MaTV, TV.HoTen
ORDER BY TongChiTieu DESC;

-- Câu 41: Gói tập được đăng ký nhiều nhất
SELECT TOP 1
       GT.TenGoi,
       COUNT(*) AS SoLanDangKy
FROM DangKyGoi DKG
JOIN GoiTap GT ON DKG.MaGoi = GT.MaGoi
GROUP BY GT.TenGoi
ORDER BY SoLanDangKy DESC;

-- Câu 42: Doanh thu từng tháng trong năm hiện tại
SELECT MONTH(NgayLap) AS Thang,
       SUM(TongTien) AS DoanhThu
FROM HoaDon
WHERE YEAR(NgayLap) = YEAR(GETDATE())
GROUP BY MONTH(NgayLap)
ORDER BY Thang;

-- Câu 43: Thành viên có BMI cao nhất
SELECT TOP 1
       TV.HoTen,
       CS.BMI
FROM ChiSoCoThe CS
JOIN ThanhVien TV ON CS.MaTV = TV.MaTV
ORDER BY CS.BMI DESC;

-- Câu 44: Huấn luyện viên mang lại doanh thu cao nhất
SELECT TOP 1
       NV.HoTen,
       SUM(DK.TongTien) AS DoanhThu
FROM DangKyPT DK
JOIN HuanLuyenVien HLV ON DK.MaHLV = HLV.MaHLV
JOIN NhanVien NV ON HLV.MaNV = NV.MaNV
GROUP BY NV.HoTen
ORDER BY DoanhThu DESC;

-- Câu 45: Thành viên có tổng chi tiêu cao hơn mức trung bình hóa đơn
SELECT TV.MaTV,
       TV.HoTen,
       SUM(HD.TongTien) AS TongChiTieu
FROM ThanhVien TV
JOIN HoaDon HD ON TV.MaTV = HD.MaTV
GROUP BY TV.MaTV, TV.HoTen
HAVING SUM(HD.TongTien) > (
    SELECT AVG(TongTien)
    FROM HoaDon
);

-- Câu 46: Thành viên đăng ký nhiều loại dịch vụ nhất
SELECT TOP 1
       TV.HoTen,
       COUNT(DISTINCT DKG.MaGoi) + COUNT(DISTINCT DKPT.MaDKPT) AS TongDichVu
FROM ThanhVien TV
LEFT JOIN DangKyGoi DKG ON TV.MaTV = DKG.MaTV
LEFT JOIN DangKyPT DKPT ON TV.MaTV = DKPT.MaTV
GROUP BY TV.HoTen
ORDER BY TongDichVu DESC;

-- Câu 47: Tìm tháng có doanh thu cao nhất
SELECT TOP 1
       MONTH(NgayLap) AS Thang,
       SUM(TongTien) AS DoanhThu
FROM HoaDon
GROUP BY MONTH(NgayLap)
ORDER BY DoanhThu DESC;

-- Câu 48: Xếp hạng thành viên theo tổng chi tiêu
SELECT TV.MaTV,
       TV.HoTen,
       SUM(HD.TongTien) AS TongChiTieu,
       RANK() OVER (
           ORDER BY SUM(HD.TongTien) DESC
       ) AS XepHang
FROM ThanhVien TV
JOIN HoaDon HD ON TV.MaTV = HD.MaTV
GROUP BY TV.MaTV, TV.HoTen;

GO

/* ========================================================================================
   PHẦN: 01_upgrade_ql_phonggym_v3.sql
   ======================================================================================== */
GO
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
 IF EXISTS(SELECT 1 FROM dbo.DangKyLop WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND MaLop=@MaLop AND TrangThai=N'Đang tham gia') THROW 50029,N'Thành viên đã đăng ký lớp này.',1;
 IF EXISTS(SELECT 1 FROM dbo.DanhSachCho WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND MaLop=@MaLop AND TrangThai=N'Đang chờ') THROW 50030,N'Thành viên đang ở danh sách chờ của lớp này.',1;
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

GO

/* ========================================================================================
   PHẦN: 02_seed_v3.sql
   ======================================================================================== */
GO
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

GO

/* ========================================================================================
   PHẦN: 04_khoi_tao_mat_khau_mau.sql
   ======================================================================================== */
GO
/*
  CHI DUNG CHO MOI TRUONG PHAT TRIEN / KHOI TAO LAN DAU.
  Chay sau 01_upgrade_ql_phonggym_v3.sql.
  Tai khoan co hash NULL se duoc chuyen sang PBKDF2-HMAC-SHA512 (210,000 vong),
  mat khau mau hien tai la 123456. Dang nhap va doi ngay sang mat khau rieng >= 8 ky tu.
*/
USE QL_PhongGym;
GO
UPDATE dbo.TaiKhoan
SET MatKhauHash = 0x5E167A74676D86DD31A79850DCC007C73628A5C6EF16B30B629EEFB7F4431107DD305F6FB26B9DB4489420AF851BB72C647B5C8FC2D554F4E093058034A06FB7,
    MatKhauSalt = 0x6D88C9B74F31BCF3DE96C250B922654348B0285275744FC7A7BA1FBE102FED80,
    MatKhau = ''
WHERE MatKhauHash IS NULL OR MatKhauSalt IS NULL;
GO
IF EXISTS (SELECT 1 FROM dbo.TaiKhoan WHERE MatKhauHash IS NULL OR MatKhauSalt IS NULL)
    THROW 51010, N'Không thể hoàn tất khởi tạo mật khẩu hash.', 1;
GO

GO

/* ========================================================================================
   PHẦN: 05_phan_quyen_tai_khoan.sql
   ======================================================================================== */
GO
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

GO

/* ========================================================================================
   PHẦN: 06_dang_ky_pt.sql
   ======================================================================================== */
GO
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

GO

/* ========================================================================================
   PHẦN: 07_diem_danh_lop.sql
   ======================================================================================== */
GO
/* Chay sau 01_upgrade_ql_phonggym_v3.sql. */
USE QL_PhongGym;
GO
CREATE OR ALTER PROCEDURE dbo.sp_DiemDanhLop
 @MaDKL INT,@NgayDiemDanh DATE,@TrangThai NVARCHAR(30)
AS
BEGIN TRY
 SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
 IF @TrangThai NOT IN(N'Có mặt',N'Vắng mặt') THROW 51050,N'Trạng thái điểm danh không hợp lệ.',1;
 IF @NgayDiemDanh>CAST(SYSDATETIME() AS DATE) THROW 51052,N'Không thể điểm danh cho ngày trong tương lai.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.DangKyLop WHERE MaDKL=@MaDKL AND TrangThai=N'Đang tham gia') THROW 51051,N'Đăng ký lớp không hợp lệ.',1;
 UPDATE dbo.DiemDanhLop SET TrangThai=@TrangThai WHERE MaDKL=@MaDKL AND NgayDiemDanh=@NgayDiemDanh;
 IF @@ROWCOUNT=0 INSERT dbo.DiemDanhLop(MaDKL,NgayDiemDanh,TrangThai) VALUES(@MaDKL,@NgayDiemDanh,@TrangThai);
 COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO

GO

/* ========================================================================================
   PHẦN: 08_cap_nhat_ma_code_khuyen_mai.sql
   ======================================================================================== */
GO
-- =========================================================================
-- PHIÊN 008: CẬP NHẬT MÃ KHUYẾN MÃI DẠNG CHỮ (PROMO CODE) THỰC TẾ
-- Thay thế mã khuyến mãi dạng số thứ tự đơn điệu bằng mã code thực tế
-- Ví dụ: CHAOHE10, SINHVIEN15, BLACKFRIDAY30, TET2026, NEWGYM5,...
-- =========================================================================

USE QL_PhongGym;
GO

-- 1. Bổ sung cột MaCode vào bảng KhuyenMai nếu chưa có
IF NOT EXISTS (
    SELECT 1 FROM sys.columns 
    WHERE object_id = OBJECT_ID(N'dbo.KhuyenMai') AND name = N'MaCode'
)
BEGIN
    ALTER TABLE dbo.KhuyenMai ADD MaCode NVARCHAR(30) NULL;
    PRINT N'Đã thêm cột MaCode vào bảng dbo.KhuyenMai.';
END
GO

-- 2. Cập nhật mã code thực tế cho các chương trình khuyến mãi hiện có
UPDATE dbo.KhuyenMai SET MaCode = N'CHAOHE10'       WHERE TenKM LIKE N'%Chào hè%';
UPDATE dbo.KhuyenMai SET MaCode = N'SINHVIEN15'     WHERE TenKM LIKE N'%Sinh viên%';
UPDATE dbo.KhuyenMai SET MaCode = N'NEWGYM5'        WHERE TenKM LIKE N'%Khách hàng mới%';
UPDATE dbo.KhuyenMai SET MaCode = N'BLACKFRIDAY30'  WHERE TenKM LIKE N'%Black Friday%';
UPDATE dbo.KhuyenMai SET MaCode = N'TET2026'        WHERE TenKM LIKE N'%Tết%';
UPDATE dbo.KhuyenMai SET MaCode = N'FAMILY12'       WHERE TenKM LIKE N'%gia đình%';
UPDATE dbo.KhuyenMai SET MaCode = N'PROPT10'        WHERE TenKM LIKE N'%PT%';
UPDATE dbo.KhuyenMai SET MaCode = N'YOGAMAY8'       WHERE TenKM LIKE N'%Yoga%';
UPDATE dbo.KhuyenMai SET MaCode = N'CARDIO7'        WHERE TenKM LIKE N'%Cardio%';
UPDATE dbo.KhuyenMai SET MaCode = N'VIPPRO18'       WHERE TenKM LIKE N'%VIP%';
UPDATE dbo.KhuyenMai SET MaCode = N'NOEL25'         WHERE TenKM LIKE N'%Noel%';
UPDATE dbo.KhuyenMai SET MaCode = N'TRIAN15'        WHERE TenKM LIKE N'%Tri ân%';
UPDATE dbo.KhuyenMai SET MaCode = N'FREETRIAL'      WHERE TenKM LIKE N'%Tập thử%';
UPDATE dbo.KhuyenMai SET MaCode = N'BACK2GYM10'     WHERE TenKM LIKE N'%tựu trường%';
UPDATE dbo.KhuyenMai SET MaCode = N'GOLDHEALTH9'    WHERE TenKM LIKE N'%Sức khỏe vàng%';

-- Tự động sinh mã cho bất kỳ bản ghi nào còn lại nếu chưa có mã
UPDATE dbo.KhuyenMai 
SET MaCode = N'KM' + RIGHT(N'000' + CAST(MaKM AS NVARCHAR(10)), 3)
WHERE MaCode IS NULL OR LTRIM(RTRIM(MaCode)) = N'';

PRINT N'Đã cập nhật mã khuyến mãi thực tế cho tất cả các chương trình.';
GO

-- 3. Đảm bảo ràng buộc UNIQUE cho MaCode nếu cần
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes 
    WHERE object_id = OBJECT_ID(N'dbo.KhuyenMai') AND name = N'UQ_KhuyenMai_MaCode'
)
BEGIN
    ALTER TABLE dbo.KhuyenMai ADD CONSTRAINT UQ_KhuyenMai_MaCode UNIQUE (MaCode);
    PRINT N'Đã tạo ràng buộc UNIQUE cho cột MaCode.';
END
GO

GO

/* ========================================================================================
   PHẦN: 09_sua_dang_ky_lop.sql
   ======================================================================================== */
GO
/* Chạy sau 01_upgrade_ql_phonggym_v3.sql.
   Đồng bộ kết quả đăng ký lớp với giao diện: hoặc có mã đăng ký, hoặc vào danh sách chờ. */
USE QL_PhongGym;
GO

CREATE OR ALTER PROCEDURE dbo.sp_DangKyLop
    @MaTV INT,
    @MaLop INT,
    @VaoDanhSachCho BIT = 1,
    @MaDKL INT OUTPUT
AS
BEGIN TRY
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRAN;

    SET @MaDKL = NULL;

    IF NOT EXISTS (
        SELECT 1 FROM dbo.ThanhVien
        WHERE MaTV = @MaTV AND TrangThai = N'Hoạt động' AND DaXoa = 0
    ) THROW 51060, N'Thành viên không hoạt động.', 1;

    DECLARE @SucChua INT, @SoDangKy INT, @TrangThaiCu NVARCHAR(30);
    SELECT @SucChua = SucChua
    FROM dbo.LopTap WITH (UPDLOCK, HOLDLOCK)
    WHERE MaLop = @MaLop AND TrangThai = N'Đang mở';

    IF @SucChua IS NULL THROW 51061, N'Lớp không mở hoặc không tồn tại.', 1;

    SELECT @TrangThaiCu = TrangThai
    FROM dbo.DangKyLop WITH (UPDLOCK, HOLDLOCK)
    WHERE MaTV = @MaTV AND MaLop = @MaLop;

    IF @TrangThaiCu = N'Đang tham gia'
        THROW 51062, N'Thành viên đã đăng ký lớp này.', 1;

    IF EXISTS (
        SELECT 1 FROM dbo.DanhSachCho
        WHERE MaLop = @MaLop AND MaTV = @MaTV AND TrangThai = N'Đang chờ'
    ) THROW 51063, N'Thành viên đang ở danh sách chờ của lớp này.', 1;

    SELECT @SoDangKy = COUNT(*)
    FROM dbo.DangKyLop WITH (UPDLOCK, HOLDLOCK)
    WHERE MaLop = @MaLop AND TrangThai = N'Đang tham gia';

    IF @SoDangKy >= @SucChua
    BEGIN
        IF @VaoDanhSachCho = 0
            THROW 51064, N'Lớp đã đủ sức chứa.', 1;

        IF EXISTS (SELECT 1 FROM dbo.DanhSachCho WHERE MaLop = @MaLop AND MaTV = @MaTV)
            UPDATE dbo.DanhSachCho
            SET TrangThai = N'Đang chờ', ThoiGianCho = SYSDATETIME()
            WHERE MaLop = @MaLop AND MaTV = @MaTV;
        ELSE
            INSERT dbo.DanhSachCho(MaLop, MaTV) VALUES(@MaLop, @MaTV);
        COMMIT;
        RETURN;
    END;

    IF @TrangThaiCu = N'Đã hủy'
    BEGIN
        UPDATE dbo.DangKyLop
        SET TrangThai = N'Đang tham gia', NgayDangKy = CAST(SYSDATETIME() AS DATE)
        WHERE MaTV = @MaTV AND MaLop = @MaLop;

        SELECT @MaDKL = MaDKL FROM dbo.DangKyLop WHERE MaTV = @MaTV AND MaLop = @MaLop;
    END
    ELSE IF @TrangThaiCu IS NOT NULL
        THROW 51065, N'Đăng ký lớp trước đó không thể dùng lại.', 1;
    ELSE
    BEGIN
        INSERT dbo.DangKyLop(MaTV, MaLop) VALUES(@MaTV, @MaLop);
        SET @MaDKL = SCOPE_IDENTITY();
    END;

    UPDATE dbo.DanhSachCho
    SET TrangThai = N'Đã mời'
    WHERE MaLop = @MaLop AND MaTV = @MaTV AND TrangThai = N'Đang chờ';

    COMMIT;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK;
    THROW;
END CATCH;
GO

GO

/* ========================================================================================
   PHẦN: 10_lich_hoc_diem_danh_theo_buoi.sql
   ======================================================================================== */
GO
/* Chạy sau 01_upgrade_ql_phonggym_v3.sql.
   Chuẩn hóa điểm danh theo buổi học cụ thể thay vì chỉ theo ngày. */
USE QL_PhongGym;
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'UX_LichHoc_Lop_ThoiGianBatDau')
    CREATE UNIQUE INDEX UX_LichHoc_Lop_ThoiGianBatDau ON dbo.LichHoc(MaLop, ThoiGianBatDau);
GO

CREATE OR ALTER PROCEDURE dbo.sp_TaoLichHoc
    @MaLop INT,
    @ThoiGianBatDau DATETIME2(0),
    @ThoiGianKetThuc DATETIME2(0),
    @MaLichHoc INT OUTPUT
AS
BEGIN TRY
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRAN;

    IF @ThoiGianKetThuc <= @ThoiGianBatDau
        THROW 51070, N'Khung giờ lớp không hợp lệ.', 1;
    IF CAST(@ThoiGianBatDau AS DATE) < CAST(SYSDATETIME() AS DATE)
        THROW 51071, N'Không thể tạo buổi học trong quá khứ.', 1;

    DECLARE @HLV INT, @Phong INT, @SucChuaLop INT, @SucChuaPhong INT, @ThuTrongTuan TINYINT;
    SELECT @HLV=MaHLV, @Phong=MaPhong, @SucChuaLop=SucChua, @ThuTrongTuan=ThuTrongTuan
    FROM dbo.LopTap WITH (UPDLOCK, HOLDLOCK)
    WHERE MaLop=@MaLop AND TrangThai=N'Đang mở';

    IF @HLV IS NULL OR @Phong IS NULL
        THROW 51072, N'Lớp chưa mở hoặc chưa được gán phòng.', 1;

    SELECT @SucChuaPhong=SucChua FROM dbo.PhongTap WITH (UPDLOCK, HOLDLOCK)
    WHERE MaPhong=@Phong AND TrangThai=N'Hoạt động';
    IF @SucChuaPhong IS NULL OR @SucChuaPhong < @SucChuaLop
        THROW 51073, N'Phòng không hoạt động hoặc không đủ sức chứa cho lớp.', 1;

    IF @ThuTrongTuan IS NOT NULL
       AND @ThuTrongTuan <> CONVERT(TINYINT, (DATEDIFF(DAY, '19000101', CAST(@ThoiGianBatDau AS DATE)) % 7) + 2)
        THROW 51074, N'Ngày tạo buổi học không đúng lịch cố định của lớp.', 1;

    IF EXISTS (SELECT 1 FROM dbo.LichHoc WITH (UPDLOCK, HOLDLOCK)
               WHERE MaLop=@MaLop AND ThoiGianBatDau=@ThoiGianBatDau)
        THROW 51075, N'Buổi học này đã được tạo.', 1;

    IF EXISTS (SELECT 1 FROM dbo.LichHoc WITH (UPDLOCK, HOLDLOCK)
               WHERE TrangThai=N'Đã lên lịch' AND (MaPhong=@Phong OR MaHLV=@HLV)
                 AND @ThoiGianBatDau<ThoiGianKetThuc AND @ThoiGianKetThuc>ThoiGianBatDau)
        THROW 51076, N'Phòng hoặc HLV bị trùng lịch.', 1;

    INSERT dbo.LichHoc(MaLop,MaHLV,MaPhong,ThoiGianBatDau,ThoiGianKetThuc)
    VALUES(@MaLop,@HLV,@Phong,@ThoiGianBatDau,@ThoiGianKetThuc);
    SET @MaLichHoc=SCOPE_IDENTITY();
    COMMIT;
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK;
    THROW;
END CATCH;
GO

CREATE OR ALTER PROCEDURE dbo.sp_DiemDanhLop
    @MaDKL INT,
    @MaLichHoc INT,
    @TrangThai NVARCHAR(30)
AS
BEGIN TRY
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRAN;

    IF @TrangThai NOT IN(N'Có mặt',N'Vắng mặt')
        THROW 51050, N'Trạng thái điểm danh không hợp lệ.', 1;

    DECLARE @NgayDiemDanh DATE;
    SELECT @NgayDiemDanh=CAST(ThoiGianBatDau AS DATE)
    FROM dbo.LichHoc WITH (UPDLOCK, HOLDLOCK)
    WHERE MaLichHoc=@MaLichHoc AND TrangThai IN(N'Đã lên lịch',N'Đã diễn ra');
    IF @NgayDiemDanh IS NULL
        THROW 51077, N'Buổi học không tồn tại hoặc đã bị hủy.', 1;
    IF @NgayDiemDanh > CAST(SYSDATETIME() AS DATE)
        THROW 51078, N'Không thể điểm danh cho buổi học trong tương lai.', 1;

    IF NOT EXISTS (
        SELECT 1
        FROM dbo.DangKyLop d
        JOIN dbo.LichHoc l ON l.MaLop=d.MaLop
        WHERE d.MaDKL=@MaDKL AND l.MaLichHoc=@MaLichHoc AND d.TrangThai=N'Đang tham gia'
    ) THROW 51079, N'Học viên không thuộc buổi học này hoặc đăng ký không còn hiệu lực.', 1;

    UPDATE dbo.DiemDanhLop
    SET TrangThai=@TrangThai, NgayDiemDanh=@NgayDiemDanh
    WHERE MaDKL=@MaDKL AND MaLichHoc=@MaLichHoc;

    IF @@ROWCOUNT=0
        INSERT dbo.DiemDanhLop(MaDKL,MaLichHoc,NgayDiemDanh,TrangThai)
        VALUES(@MaDKL,@MaLichHoc,@NgayDiemDanh,@TrangThai);

    UPDATE dbo.LichHoc
    SET TrangThai=N'Đã diễn ra'
    WHERE MaLichHoc=@MaLichHoc AND TrangThai=N'Đã lên lịch';

    COMMIT;
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK;
    THROW;
END CATCH;
GO

GO

/* ========================================================================================
   PHẦN: 12_tu_dong_het_han_goi.sql
   ======================================================================================== */
GO
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

GO

/* ========================================================================================
   PHẦN: 13_chuan_hoa_ma_nghiep_vu.sql
   ======================================================================================== */
GO
/* Phiên 2 - Chuẩn hóa mã nghiệp vụ.
   Giữ nguyên toàn bộ IDENTITY PK/FK; chỉ thêm mã hiển thị có tiền tố.
   Script có thể chạy lặp lại, không xóa dữ liệu. Chạy sau migration 12. */
USE QL_PhongGym;
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* 1. Bổ sung cột mã nghiệp vụ nếu database đích chưa có.
   Tách batch để SQL Server compile được các UPDATE ở batch kế tiếp. */
    IF COL_LENGTH(N'dbo.NhanVien', N'MaNhanVien') IS NULL
        ALTER TABLE dbo.NhanVien ADD MaNhanVien VARCHAR(16) NULL;
    IF COL_LENGTH(N'dbo.GoiTap', N'MaGoiTap') IS NULL
        ALTER TABLE dbo.GoiTap ADD MaGoiTap VARCHAR(16) NULL;
    IF COL_LENGTH(N'dbo.DangKyGoi', N'MaDangKyGoi') IS NULL
        ALTER TABLE dbo.DangKyGoi ADD MaDangKyGoi VARCHAR(32) NULL;
    IF COL_LENGTH(N'dbo.DangKyPT', N'MaDangKyPT') IS NULL
        ALTER TABLE dbo.DangKyPT ADD MaDangKyPT VARCHAR(32) NULL;
    IF COL_LENGTH(N'dbo.LopTap', N'MaLopTap') IS NULL
        ALTER TABLE dbo.LopTap ADD MaLopTap VARCHAR(16) NULL;
    IF COL_LENGTH(N'dbo.DangKyLop', N'MaDangKyLop') IS NULL
        ALTER TABLE dbo.DangKyLop ADD MaDangKyLop VARCHAR(32) NULL;
    IF COL_LENGTH(N'dbo.LichHoc', N'MaBuoiHoc') IS NULL
        ALTER TABLE dbo.LichHoc ADD MaBuoiHoc VARCHAR(32) NULL;
    IF COL_LENGTH(N'dbo.HoaDon', N'SoHoaDon') IS NULL
        ALTER TABLE dbo.HoaDon ADD SoHoaDon VARCHAR(32) NULL;
    IF COL_LENGTH(N'dbo.ThanhToan', N'SoGiaoDich') IS NULL
        ALTER TABLE dbo.ThanhToan ADD SoGiaoDich VARCHAR(32) NULL;
    IF COL_LENGTH(N'dbo.LichSuCheckIn', N'MaPhienCheckIn') IS NULL
        ALTER TABLE dbo.LichSuCheckIn ADD MaPhienCheckIn VARCHAR(32) NULL;
    IF COL_LENGTH(N'dbo.PhongTap', N'MaPhongTap') IS NULL
        ALTER TABLE dbo.PhongTap ADD MaPhongTap VARCHAR(16) NULL;
    IF COL_LENGTH(N'dbo.PhieuNhapKho', N'SoPhieuNhap') IS NULL
        ALTER TABLE dbo.PhieuNhapKho ADD SoPhieuNhap VARCHAR(32) NULL;
    IF COL_LENGTH(N'dbo.KhuyenMai', N'MaCode') IS NULL
        ALTER TABLE dbo.KhuyenMai ADD MaCode NVARCHAR(30) NULL;
GO

BEGIN TRY
    BEGIN TRAN;

    /* 2. Backfill deterministic từ ID hiện tại; không đổi mã đã có. */
    UPDATE dbo.ThanhVien
    SET MaThanhVien=CONCAT('TV',CASE WHEN MaTV<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),MaTV),6) ELSE CONVERT(VARCHAR(20),MaTV) END)
    WHERE NULLIF(LTRIM(RTRIM(MaThanhVien)),'') IS NULL;

    UPDATE dbo.SanPham
    SET MaSanPham=CONCAT('SP',CASE WHEN MaSP<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),MaSP),6) ELSE CONVERT(VARCHAR(20),MaSP) END)
    WHERE NULLIF(LTRIM(RTRIM(MaSanPham)),'') IS NULL;

    UPDATE dbo.ThietBi
    SET MaThietBi=CONCAT('TB',CASE WHEN MaTB<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),MaTB),6) ELSE CONVERT(VARCHAR(20),MaTB) END)
    WHERE NULLIF(LTRIM(RTRIM(MaThietBi)),'') IS NULL;

    UPDATE dbo.NhanVien
    SET MaNhanVien=CONCAT('NV',CASE WHEN MaNV<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),MaNV),6) ELSE CONVERT(VARCHAR(20),MaNV) END)
    WHERE NULLIF(LTRIM(RTRIM(MaNhanVien)),'') IS NULL;

    UPDATE dbo.GoiTap
    SET MaGoiTap=CONCAT('GOI',CASE WHEN MaGoi<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),MaGoi),6) ELSE CONVERT(VARCHAR(20),MaGoi) END)
    WHERE NULLIF(LTRIM(RTRIM(MaGoiTap)),'') IS NULL;

    UPDATE dbo.DangKyGoi
    SET MaDangKyGoi=CONCAT('DKG-',CONVERT(CHAR(8),ISNULL(NgayBatDau,'20000101'),112),'-',CASE WHEN MaDKG<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),MaDKG),6) ELSE CONVERT(VARCHAR(20),MaDKG) END)
    WHERE NULLIF(LTRIM(RTRIM(MaDangKyGoi)),'') IS NULL;

    UPDATE dbo.DangKyPT
    SET MaDangKyPT=CONCAT('DPT-',CONVERT(CHAR(8),ISNULL(NgayBatDau,'20000101'),112),'-',CASE WHEN MaDKPT<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),MaDKPT),6) ELSE CONVERT(VARCHAR(20),MaDKPT) END)
    WHERE NULLIF(LTRIM(RTRIM(MaDangKyPT)),'') IS NULL;

    UPDATE dbo.LopTap
    SET MaLopTap=CONCAT('LOP',CASE WHEN MaLop<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),MaLop),6) ELSE CONVERT(VARCHAR(20),MaLop) END)
    WHERE NULLIF(LTRIM(RTRIM(MaLopTap)),'') IS NULL;

    UPDATE dbo.DangKyLop
    SET MaDangKyLop=CONCAT('DKL-',CONVERT(CHAR(8),ISNULL(NgayDangKy,'20000101'),112),'-',CASE WHEN MaDKL<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),MaDKL),6) ELSE CONVERT(VARCHAR(20),MaDKL) END)
    WHERE NULLIF(LTRIM(RTRIM(MaDangKyLop)),'') IS NULL;

    UPDATE dbo.LichHoc
    SET MaBuoiHoc=CONCAT('LH-',CONVERT(CHAR(8),ISNULL(ThoiGianBatDau,'20000101'),112),'-',CASE WHEN MaLichHoc<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),MaLichHoc),6) ELSE CONVERT(VARCHAR(20),MaLichHoc) END)
    WHERE NULLIF(LTRIM(RTRIM(MaBuoiHoc)),'') IS NULL;

    UPDATE dbo.HoaDon
    SET SoHoaDon=CONCAT('HD-',CONVERT(CHAR(8),ISNULL(NgayLap,'20000101'),112),'-',CASE WHEN MaHD<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),MaHD),6) ELSE CONVERT(VARCHAR(20),MaHD) END)
    WHERE NULLIF(LTRIM(RTRIM(SoHoaDon)),'') IS NULL;

    UPDATE dbo.ThanhToan
    SET SoGiaoDich=CONCAT('TT-',CONVERT(CHAR(8),ISNULL(NgayThanhToan,'20000101'),112),'-',CASE WHEN MaTT<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),MaTT),6) ELSE CONVERT(VARCHAR(20),MaTT) END)
    WHERE NULLIF(LTRIM(RTRIM(SoGiaoDich)),'') IS NULL;

    UPDATE dbo.LichSuCheckIn
    SET MaPhienCheckIn=CONCAT('CI-',CONVERT(CHAR(8),ISNULL(ThoiGianVao,'20000101'),112),'-',CASE WHEN MaCheckIn<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),MaCheckIn),6) ELSE CONVERT(VARCHAR(20),MaCheckIn) END)
    WHERE NULLIF(LTRIM(RTRIM(MaPhienCheckIn)),'') IS NULL;

    UPDATE dbo.PhongTap
    SET MaPhongTap=CONCAT('PHG',CASE WHEN MaPhong<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),MaPhong),6) ELSE CONVERT(VARCHAR(20),MaPhong) END)
    WHERE NULLIF(LTRIM(RTRIM(MaPhongTap)),'') IS NULL;

    UPDATE dbo.PhieuNhapKho
    SET SoPhieuNhap=CONCAT('PNK-',CONVERT(CHAR(8),ISNULL(NgayNhap,'20000101'),112),'-',CASE WHEN MaPNK<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),MaPNK),6) ELSE CONVERT(VARCHAR(20),MaPNK) END)
    WHERE NULLIF(LTRIM(RTRIM(SoPhieuNhap)),'') IS NULL;

    UPDATE dbo.KhuyenMai
    SET MaCode=CONCAT(N'KM',CASE WHEN MaKM<1000000 THEN RIGHT(N'000000'+CONVERT(NVARCHAR(20),MaKM),6) ELSE CONVERT(NVARCHAR(20),MaKM) END)
    WHERE NULLIF(LTRIM(RTRIM(MaCode)),N'') IS NULL;

    /* 3. Chặn mã trùng sau khi backfill. */
    IF EXISTS(SELECT MaThanhVien FROM dbo.ThanhVien WHERE MaThanhVien IS NOT NULL GROUP BY MaThanhVien HAVING COUNT(*)>1)
        THROW 51301,N'Trùng MaThanhVien sau backfill.',1;
    IF EXISTS(SELECT MaSanPham FROM dbo.SanPham WHERE MaSanPham IS NOT NULL GROUP BY MaSanPham HAVING COUNT(*)>1)
        THROW 51302,N'Trùng MaSanPham sau backfill.',1;
    IF EXISTS(SELECT MaThietBi FROM dbo.ThietBi WHERE MaThietBi IS NOT NULL GROUP BY MaThietBi HAVING COUNT(*)>1)
        THROW 51303,N'Trùng MaThietBi sau backfill.',1;
    IF EXISTS(SELECT MaCode FROM dbo.KhuyenMai WHERE MaCode IS NOT NULL GROUP BY MaCode HAVING COUNT(*)>1)
        THROW 51304,N'Trùng MaCode sau backfill.',1;

    /* 4. Unique index/constraint, chỉ lọc NULL để trigger có thể cấp mã sau INSERT. */
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.NhanVien') AND name=N'UX_NhanVien_MaNhanVien')
        CREATE UNIQUE INDEX UX_NhanVien_MaNhanVien ON dbo.NhanVien(MaNhanVien) WHERE MaNhanVien IS NOT NULL;
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.GoiTap') AND name=N'UX_GoiTap_MaGoiTap')
        CREATE UNIQUE INDEX UX_GoiTap_MaGoiTap ON dbo.GoiTap(MaGoiTap) WHERE MaGoiTap IS NOT NULL;
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.DangKyGoi') AND name=N'UX_DangKyGoi_MaDangKyGoi')
        CREATE UNIQUE INDEX UX_DangKyGoi_MaDangKyGoi ON dbo.DangKyGoi(MaDangKyGoi) WHERE MaDangKyGoi IS NOT NULL;
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.DangKyPT') AND name=N'UX_DangKyPT_MaDangKyPT')
        CREATE UNIQUE INDEX UX_DangKyPT_MaDangKyPT ON dbo.DangKyPT(MaDangKyPT) WHERE MaDangKyPT IS NOT NULL;
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.LopTap') AND name=N'UX_LopTap_MaLopTap')
        CREATE UNIQUE INDEX UX_LopTap_MaLopTap ON dbo.LopTap(MaLopTap) WHERE MaLopTap IS NOT NULL;
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.DangKyLop') AND name=N'UX_DangKyLop_MaDangKyLop')
        CREATE UNIQUE INDEX UX_DangKyLop_MaDangKyLop ON dbo.DangKyLop(MaDangKyLop) WHERE MaDangKyLop IS NOT NULL;
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.LichHoc') AND name=N'UX_LichHoc_MaBuoiHoc')
        CREATE UNIQUE INDEX UX_LichHoc_MaBuoiHoc ON dbo.LichHoc(MaBuoiHoc) WHERE MaBuoiHoc IS NOT NULL;
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.HoaDon') AND name=N'UX_HoaDon_SoHoaDon')
        CREATE UNIQUE INDEX UX_HoaDon_SoHoaDon ON dbo.HoaDon(SoHoaDon) WHERE SoHoaDon IS NOT NULL;
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.ThanhToan') AND name=N'UX_ThanhToan_SoGiaoDich')
        CREATE UNIQUE INDEX UX_ThanhToan_SoGiaoDich ON dbo.ThanhToan(SoGiaoDich) WHERE SoGiaoDich IS NOT NULL;
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.LichSuCheckIn') AND name=N'UX_LichSuCheckIn_MaPhien')
        CREATE UNIQUE INDEX UX_LichSuCheckIn_MaPhien ON dbo.LichSuCheckIn(MaPhienCheckIn) WHERE MaPhienCheckIn IS NOT NULL;
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.PhongTap') AND name=N'UX_PhongTap_MaPhongTap')
        CREATE UNIQUE INDEX UX_PhongTap_MaPhongTap ON dbo.PhongTap(MaPhongTap) WHERE MaPhongTap IS NOT NULL;
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.PhieuNhapKho') AND name=N'UX_PhieuNhapKho_SoPhieuNhap')
        CREATE UNIQUE INDEX UX_PhieuNhapKho_SoPhieuNhap ON dbo.PhieuNhapKho(SoPhieuNhap) WHERE SoPhieuNhap IS NOT NULL;
    IF NOT EXISTS(SELECT 1 FROM sys.key_constraints WHERE parent_object_id=OBJECT_ID(N'dbo.KhuyenMai') AND name=N'UQ_KhuyenMai_MaCode')
        ALTER TABLE dbo.KhuyenMai ADD CONSTRAINT UQ_KhuyenMai_MaCode UNIQUE(MaCode);

    COMMIT;
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK;
    THROW;
END CATCH;
GO

/* 5. Cấp mã tự động cho bản ghi mới nếu caller không truyền mã. */
CREATE OR ALTER TRIGGER dbo.TR_NhanVien_MaNhanVien ON dbo.NhanVien AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    UPDATE n SET MaNhanVien=CONCAT('NV',CASE WHEN n.MaNV<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),n.MaNV),6) ELSE CONVERT(VARCHAR(20),n.MaNV) END)
    FROM dbo.NhanVien n JOIN inserted i ON i.MaNV=n.MaNV
    WHERE NULLIF(LTRIM(RTRIM(n.MaNhanVien)),'') IS NULL;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_GoiTap_MaGoiTap ON dbo.GoiTap AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    UPDATE g SET MaGoiTap=CONCAT('GOI',CASE WHEN g.MaGoi<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),g.MaGoi),6) ELSE CONVERT(VARCHAR(20),g.MaGoi) END)
    FROM dbo.GoiTap g JOIN inserted i ON i.MaGoi=g.MaGoi
    WHERE NULLIF(LTRIM(RTRIM(g.MaGoiTap)),'') IS NULL;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_DangKyGoi_MaDangKyGoi ON dbo.DangKyGoi AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    UPDATE d SET MaDangKyGoi=CONCAT('DKG-',CONVERT(CHAR(8),ISNULL(d.NgayBatDau,'20000101'),112),'-',CASE WHEN d.MaDKG<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),d.MaDKG),6) ELSE CONVERT(VARCHAR(20),d.MaDKG) END)
    FROM dbo.DangKyGoi d JOIN inserted i ON i.MaDKG=d.MaDKG
    WHERE NULLIF(LTRIM(RTRIM(d.MaDangKyGoi)),'') IS NULL;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_DangKyPT_MaDangKyPT ON dbo.DangKyPT AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    UPDATE d SET MaDangKyPT=CONCAT('DPT-',CONVERT(CHAR(8),ISNULL(d.NgayBatDau,'20000101'),112),'-',CASE WHEN d.MaDKPT<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),d.MaDKPT),6) ELSE CONVERT(VARCHAR(20),d.MaDKPT) END)
    FROM dbo.DangKyPT d JOIN inserted i ON i.MaDKPT=d.MaDKPT
    WHERE NULLIF(LTRIM(RTRIM(d.MaDangKyPT)),'') IS NULL;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_LopTap_MaLopTap ON dbo.LopTap AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    UPDATE l SET MaLopTap=CONCAT('LOP',CASE WHEN l.MaLop<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),l.MaLop),6) ELSE CONVERT(VARCHAR(20),l.MaLop) END)
    FROM dbo.LopTap l JOIN inserted i ON i.MaLop=l.MaLop
    WHERE NULLIF(LTRIM(RTRIM(l.MaLopTap)),'') IS NULL;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_DangKyLop_MaDangKyLop ON dbo.DangKyLop AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    UPDATE d SET MaDangKyLop=CONCAT('DKL-',CONVERT(CHAR(8),ISNULL(d.NgayDangKy,'20000101'),112),'-',CASE WHEN d.MaDKL<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),d.MaDKL),6) ELSE CONVERT(VARCHAR(20),d.MaDKL) END)
    FROM dbo.DangKyLop d JOIN inserted i ON i.MaDKL=d.MaDKL
    WHERE NULLIF(LTRIM(RTRIM(d.MaDangKyLop)),'') IS NULL;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_LichHoc_MaBuoiHoc ON dbo.LichHoc AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    UPDATE l SET MaBuoiHoc=CONCAT('LH-',CONVERT(CHAR(8),ISNULL(l.ThoiGianBatDau,'20000101'),112),'-',CASE WHEN l.MaLichHoc<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),l.MaLichHoc),6) ELSE CONVERT(VARCHAR(20),l.MaLichHoc) END)
    FROM dbo.LichHoc l JOIN inserted i ON i.MaLichHoc=l.MaLichHoc
    WHERE NULLIF(LTRIM(RTRIM(l.MaBuoiHoc)),'') IS NULL;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_HoaDon_SoHoaDon ON dbo.HoaDon AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    UPDATE h SET SoHoaDon=CONCAT('HD-',CONVERT(CHAR(8),ISNULL(h.NgayLap,'20000101'),112),'-',CASE WHEN h.MaHD<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),h.MaHD),6) ELSE CONVERT(VARCHAR(20),h.MaHD) END)
    FROM dbo.HoaDon h JOIN inserted i ON i.MaHD=h.MaHD
    WHERE NULLIF(LTRIM(RTRIM(h.SoHoaDon)),'') IS NULL;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_ThanhToan_SoGiaoDich ON dbo.ThanhToan AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    UPDATE t SET SoGiaoDich=CONCAT('TT-',CONVERT(CHAR(8),ISNULL(t.NgayThanhToan,'20000101'),112),'-',CASE WHEN t.MaTT<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),t.MaTT),6) ELSE CONVERT(VARCHAR(20),t.MaTT) END)
    FROM dbo.ThanhToan t JOIN inserted i ON i.MaTT=t.MaTT
    WHERE NULLIF(LTRIM(RTRIM(t.SoGiaoDich)),'') IS NULL;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_LichSuCheckIn_MaPhien ON dbo.LichSuCheckIn AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    UPDATE c SET MaPhienCheckIn=CONCAT('CI-',CONVERT(CHAR(8),ISNULL(c.ThoiGianVao,'20000101'),112),'-',CASE WHEN c.MaCheckIn<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),c.MaCheckIn),6) ELSE CONVERT(VARCHAR(20),c.MaCheckIn) END)
    FROM dbo.LichSuCheckIn c JOIN inserted i ON i.MaCheckIn=c.MaCheckIn
    WHERE NULLIF(LTRIM(RTRIM(c.MaPhienCheckIn)),'') IS NULL;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_PhongTap_MaPhongTap ON dbo.PhongTap AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    UPDATE p SET MaPhongTap=CONCAT('PHG',CASE WHEN p.MaPhong<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),p.MaPhong),6) ELSE CONVERT(VARCHAR(20),p.MaPhong) END)
    FROM dbo.PhongTap p JOIN inserted i ON i.MaPhong=p.MaPhong
    WHERE NULLIF(LTRIM(RTRIM(p.MaPhongTap)),'') IS NULL;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_PhieuNhapKho_SoPhieuNhap ON dbo.PhieuNhapKho AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    UPDATE p SET SoPhieuNhap=CONCAT('PNK-',CONVERT(CHAR(8),ISNULL(p.NgayNhap,'20000101'),112),'-',CASE WHEN p.MaPNK<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),p.MaPNK),6) ELSE CONVERT(VARCHAR(20),p.MaPNK) END)
    FROM dbo.PhieuNhapKho p JOIN inserted i ON i.MaPNK=p.MaPNK
    WHERE NULLIF(LTRIM(RTRIM(p.SoPhieuNhap)),'') IS NULL;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_KhuyenMai_MaCode ON dbo.KhuyenMai AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    UPDATE k SET MaCode=CONCAT(N'KM',CASE WHEN k.MaKM<1000000 THEN RIGHT(N'000000'+CONVERT(NVARCHAR(20),k.MaKM),6) ELSE CONVERT(NVARCHAR(20),k.MaKM) END)
    FROM dbo.KhuyenMai k JOIN inserted i ON i.MaKM=k.MaKM
    WHERE NULLIF(LTRIM(RTRIM(k.MaCode)),N'') IS NULL;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_SanPham_MaSanPham ON dbo.SanPham AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    UPDATE s SET MaSanPham=CONCAT('SP',CASE WHEN s.MaSP<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),s.MaSP),6) ELSE CONVERT(VARCHAR(20),s.MaSP) END)
    FROM dbo.SanPham s JOIN inserted i ON i.MaSP=s.MaSP
    WHERE NULLIF(LTRIM(RTRIM(s.MaSanPham)),'') IS NULL;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_ThietBi_MaThietBi ON dbo.ThietBi AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    UPDATE t SET MaThietBi=CONCAT('TB',CASE WHEN t.MaTB<1000000 THEN RIGHT('000000'+CONVERT(VARCHAR(20),t.MaTB),6) ELSE CONVERT(VARCHAR(20),t.MaTB) END)
    FROM dbo.ThietBi t JOIN inserted i ON i.MaTB=t.MaTB
    WHERE NULLIF(LTRIM(RTRIM(t.MaThietBi)),'') IS NULL;
END;
GO

/* 6. Chuẩn hóa SET options cho procedure ghi DangKyLop.
   Các filtered index yêu cầu QUOTED_IDENTIFIER/ANSI_NULLS đúng khi DML chạy. */
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.sp_DangKyLop
    @MaTV INT,
    @MaLop INT,
    @VaoDanhSachCho BIT=1,
    @MaDKL INT OUTPUT
AS
BEGIN TRY
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRAN;
    IF NOT EXISTS(SELECT 1 FROM dbo.ThanhVien WHERE MaTV=@MaTV AND TrangThai=N'Hoạt động' AND DaXoa=0)
        THROW 50028,N'Thành viên không hoạt động.',1;
    DECLARE @SucChua INT,@DangKy INT;
    SELECT @SucChua=SucChua FROM dbo.LopTap WITH(UPDLOCK,HOLDLOCK) WHERE MaLop=@MaLop AND TrangThai=N'Đang mở';
    IF @SucChua IS NULL THROW 50013,N'Lớp không mở hoặc không tồn tại.',1;
    IF EXISTS(SELECT 1 FROM dbo.DangKyLop WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND MaLop=@MaLop AND TrangThai=N'Đang tham gia')
        THROW 50029,N'Thành viên đã đăng ký lớp này.',1;
    IF EXISTS(SELECT 1 FROM dbo.DanhSachCho WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND MaLop=@MaLop AND TrangThai=N'Đang chờ')
        THROW 50030,N'Thành viên đang ở danh sách chờ của lớp này.',1;
    SELECT @DangKy=COUNT(*) FROM dbo.DangKyLop WHERE MaLop=@MaLop AND TrangThai=N'Đang tham gia';
    IF @DangKy>=@SucChua
    BEGIN
        IF @VaoDanhSachCho=1
        BEGIN
            INSERT dbo.DanhSachCho(MaLop,MaTV) VALUES(@MaLop,@MaTV);
            SET @MaDKL=NULL;
            COMMIT;
            RETURN;
        END;
        THROW 50014,N'Lớp đã đủ sức chứa.',1;
    END;
    INSERT dbo.DangKyLop(MaTV,MaLop) VALUES(@MaTV,@MaLop);
    SET @MaDKL=SCOPE_IDENTITY();
    COMMIT;
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK;
    THROW;
END CATCH;
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.sp_HuyDangKyLop @MaDKL INT AS
BEGIN TRY
    SET NOCOUNT ON;
    UPDATE dbo.DangKyLop SET TrangThai=N'Đã hủy'
    WHERE MaDKL=@MaDKL AND TrangThai=N'Đang tham gia';
    IF @@ROWCOUNT=0 THROW 50015,N'Không thể hủy đăng ký lớp.',1;
END TRY
BEGIN CATCH
    THROW;
END CATCH;
GO

GO

/* ========================================================================================
   PHẦN: 14_sua_checkin_va_hoan_kho.sql
   ======================================================================================== */
GO
/*
    QL_PhongGym v3 - sửa check-in theo lượt/khung giờ và hoàn kho khi hủy hóa đơn.
    Chạy sau database/13_chuan_hoa_ma_nghiep_vu.sql trên database đã backup.
    Không sửa dữ liệu lịch sử; chỉ thay đổi các procedure nghiệp vụ.
*/
USE QL_PhongGym;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
GO

CREATE OR ALTER PROCEDURE dbo.sp_CheckIn
    @MaTV INT,
    @MaPhong INT = NULL,
    @Kenh NVARCHAR(20) = N'Quầy',
    @MaCheckIn BIGINT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        DECLARE @HienTai DATETIME2(0) = SYSDATETIME();
        DECLARE @GioHienTai TIME(0) = CAST(@HienTai AS TIME(0));
        DECLARE @MaDKG INT;

        IF EXISTS (
            SELECT 1
            FROM dbo.LichSuCheckIn WITH (UPDLOCK, HOLDLOCK)
            WHERE MaTV = @MaTV AND ThoiGianRa IS NULL
        )
            THROW 50010, N'Thành viên đang có lượt check-in mở.', 1;

        SELECT TOP (1) @MaDKG = d.MaDKG
        FROM dbo.DangKyGoi d WITH (UPDLOCK, HOLDLOCK)
        JOIN dbo.GoiTap g ON g.MaGoi = d.MaGoi
        JOIN dbo.ThanhVien t ON t.MaTV = d.MaTV
        WHERE d.MaTV = @MaTV
          AND d.TrangThai = N'Còn hạn'
          AND d.NgayBatDau <= CAST(@HienTai AS DATE)
          AND d.NgayKetThuc >= CAST(@HienTai AS DATE)
          AND t.TrangThai = N'Hoạt động'
          AND t.DaXoa = 0
          AND (g.GioiHanLuot IS NULL OR ISNULL(d.SoLuotDaDung, 0) < g.GioiHanLuot)
          AND (
              g.GioBatDau IS NULL OR g.GioKetThuc IS NULL
              OR (g.GioBatDau <= g.GioKetThuc AND @GioHienTai BETWEEN g.GioBatDau AND g.GioKetThuc)
              OR (g.GioBatDau > g.GioKetThuc AND (@GioHienTai >= g.GioBatDau OR @GioHienTai <= g.GioKetThuc))
          )
          AND EXISTS (
              SELECT 1
              FROM dbo.HoaDonGoiTap x
              JOIN dbo.ChiTietHoaDon c ON c.MaCTHD = x.MaCTHD
              JOIN dbo.HoaDon h ON h.MaHD = c.MaHD
              WHERE x.MaDKG = d.MaDKG
                AND h.TrangThai = N'Đã thanh toán'
          )
        ORDER BY d.NgayKetThuc DESC, d.MaDKG DESC;

        IF @MaDKG IS NULL
            THROW 50011, N'Không có gói hợp lệ đã thanh toán, còn lượt và đúng khung giờ để check-in.', 1;

        INSERT dbo.LichSuCheckIn(MaTV, MaDKG, MaPhong, Kenh)
        VALUES (@MaTV, @MaDKG, @MaPhong, @Kenh);
        SET @MaCheckIn = CONVERT(BIGINT, SCOPE_IDENTITY());

        UPDATE dbo.DangKyGoi
        SET SoLuotDaDung = ISNULL(SoLuotDaDung, 0) + 1,
            NgayCapNhat = @HienTai
        WHERE MaDKG = @MaDKG;

        IF @@ROWCOUNT = 0
            THROW 50037, N'Không thể cập nhật số lượt sử dụng của gói.', 1;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_HuyHoaDon
    @MaHD INT,
    @LyDo NVARCHAR(255),
    @MaNV INT = NULL
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        IF EXISTS (
            SELECT 1
            FROM dbo.ThanhToan WITH (UPDLOCK, HOLDLOCK)
            WHERE MaHD = @MaHD
              AND TrangThai = N'Thành công'
              AND LoaiGiaoDich = N'Thanh toán'
        )
            THROW 50034, N'Hóa đơn đã có thanh toán; hãy hoàn tiền trước khi hủy.', 1;

        DECLARE @HoanKho TABLE
        (
            MaSP INT NOT NULL PRIMARY KEY,
            SoLuong INT NOT NULL,
            TonTruoc INT NOT NULL,
            TonSau INT NOT NULL
        );

        INSERT @HoanKho(MaSP, SoLuong, TonTruoc, TonSau)
        SELECT hs.MaSP,
               SUM(ct.SoLuong),
               sp.SoLuongTon,
               sp.SoLuongTon + SUM(ct.SoLuong)
        FROM dbo.HoaDonSanPham hs
        JOIN dbo.ChiTietHoaDon ct ON ct.MaCTHD = hs.MaCTHD AND ct.MaHD = @MaHD
        JOIN dbo.SanPham sp WITH (UPDLOCK, HOLDLOCK) ON sp.MaSP = hs.MaSP
        GROUP BY hs.MaSP, sp.SoLuongTon;

        UPDATE dbo.HoaDon
        SET TrangThai = N'Hủy',
            LyDoHuy = @LyDo,
            NgayCapNhat = SYSDATETIME()
        WHERE MaHD = @MaHD
          AND TrangThai IN (N'Chưa thanh toán', N'Đã thanh toán một phần');

        IF @@ROWCOUNT = 0
            THROW 50035, N'Hóa đơn không thể hủy.', 1;

        UPDATE sp
        SET sp.SoLuongTon = hk.TonSau,
            sp.NgayCapNhat = SYSDATETIME()
        FROM dbo.SanPham sp
        JOIN @HoanKho hk ON hk.MaSP = sp.MaSP;

        INSERT dbo.LichSuTonKho
            (MaSP, LoaiBienDong, SoLuong, TonTruoc, TonSau, NguonThamChieu, MaThamChieu, MaNV)
        SELECT MaSP, N'Hoàn trả', SoLuong, TonTruoc, TonSau, N'Hóa đơn', @MaHD, @MaNV
        FROM @HoanKho;

        INSERT dbo.NhatKyHeThong(MaNV, HanhDong, DoiTuong, KhoaDoiTuong, NoiDung)
        VALUES (@MaNV, N'Hủy hóa đơn', N'HoaDon', CONVERT(NVARCHAR(30), @MaHD), @LyDo);

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

GO

/* ========================================================================================
   PHẦN: 15_danh_dau_diem_danh_legacy.sql
   ======================================================================================== */
GO
/*
    Đánh dấu minh bạch dữ liệu điểm danh cũ không có MaLichHoc.
    Không tự gán dữ liệu cũ vào buổi học mới vì không đủ bằng chứng xác định.
    Chạy sau database/14_sua_checkin_va_hoan_kho.sql.
*/
USE QL_PhongGym;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
GO

IF COL_LENGTH(N'dbo.DiemDanhLop', N'LaDuLieuCu') IS NULL
BEGIN
    ALTER TABLE dbo.DiemDanhLop
        ADD LaDuLieuCu BIT NOT NULL
            CONSTRAINT DF_DiemDanhLop_LaDuLieuCu DEFAULT (0) WITH VALUES;
END;
GO

UPDATE dbo.DiemDanhLop
SET LaDuLieuCu = 1
WHERE MaLichHoc IS NULL AND LaDuLieuCu = 0;
GO

IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = N'CK_DiemDanhLop_BuoiHoacLegacy')
    ALTER TABLE dbo.DiemDanhLop ADD CONSTRAINT CK_DiemDanhLop_BuoiHoacLegacy
        CHECK (MaLichHoc IS NOT NULL OR LaDuLieuCu = 1);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_DiemDanhLop_Legacy')
    CREATE INDEX IX_DiemDanhLop_Legacy ON dbo.DiemDanhLop(LaDuLieuCu, NgayDiemDanh)
        INCLUDE (MaDKL, MaLichHoc, TrangThai);
GO

CREATE OR ALTER VIEW dbo.vw_DiemDanhLegacy AS
SELECT dd.MaDD, dd.MaDKL, dd.NgayDiemDanh, dd.TrangThai, dd.LaDuLieuCu,
       d.MaLop, l.MaLopTap, l.TenLop, d.MaTV, tv.MaThanhVien, tv.HoTen
FROM dbo.DiemDanhLop dd
JOIN dbo.DangKyLop d ON d.MaDKL = dd.MaDKL
JOIN dbo.LopTap l ON l.MaLop = d.MaLop
JOIN dbo.ThanhVien tv ON tv.MaTV = d.MaTV
WHERE dd.MaLichHoc IS NULL AND dd.LaDuLieuCu = 1;
GO

GO

/* ========================================================================================
   PHẦN: 16_dong_bo_ma_code_khuyen_mai.sql
   ======================================================================================== */
GO
/* Đồng bộ schema MaCode khuyến mãi với source và giao diện. */
USE QL_PhongGym;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
GO

IF COL_LENGTH(N'dbo.KhuyenMai', N'MaCode') IS NULL
    ALTER TABLE dbo.KhuyenMai ADD MaCode NVARCHAR(30) NULL;
GO

UPDATE k
SET MaCode = CASE k.MaKM
    WHEN 1 THEN N'CHAOHE10'
    WHEN 2 THEN N'SINHVIEN15'
    WHEN 3 THEN N'NEWGYM5'
    WHEN 4 THEN N'BLACKFRIDAY30'
    WHEN 5 THEN N'TET2026'
    WHEN 6 THEN N'FAMILY12'
    WHEN 7 THEN N'PROPT10'
    WHEN 8 THEN N'YOGAMAY8'
    WHEN 9 THEN N'CARDIO7'
    WHEN 10 THEN N'VIPPRO18'
    WHEN 11 THEN N'NOEL25'
    WHEN 12 THEN N'TRIAN15'
    WHEN 13 THEN N'FREETRIAL'
    WHEN 14 THEN N'BACK2GYM10'
    WHEN 15 THEN N'GOLDHEALTH9'
    ELSE N'KM' + RIGHT(N'000000' + CONVERT(NVARCHAR(10), k.MaKM), 6)
END
FROM dbo.KhuyenMai k
WHERE k.MaCode IS NULL OR LTRIM(RTRIM(k.MaCode)) = N'';
GO

IF EXISTS (SELECT 1 FROM dbo.KhuyenMai WHERE MaCode IS NULL OR LTRIM(RTRIM(MaCode)) = N'')
    THROW 53000, N'Còn khuyến mãi chưa có MaCode.', 1;

IF COL_LENGTH(N'dbo.KhuyenMai', N'MaCode') IS NOT NULL
BEGIN
    IF EXISTS (SELECT 1 FROM sys.key_constraints WHERE name = N'UQ_KhuyenMai_MaCode')
        ALTER TABLE dbo.KhuyenMai DROP CONSTRAINT UQ_KhuyenMai_MaCode;
    ALTER TABLE dbo.KhuyenMai ALTER COLUMN MaCode NVARCHAR(30) NOT NULL;
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE name = N'UQ_KhuyenMai_MaCode')
    ALTER TABLE dbo.KhuyenMai ADD CONSTRAINT UQ_KhuyenMai_MaCode UNIQUE (MaCode);
GO

GO

/* ========================================================================================
   PHẦN: 17_bo_sung_du_lieu_demo_lich_diemdanh.sql
   ======================================================================================== */
GO
/*
    Dữ liệu mẫu demo: hoàn tất lịch lớp/điểm danh và cấu hình sức chứa.
    Chạy sau migration 16 trên database demo. Có thể chạy lặp an toàn.
*/
USE QL_PhongGym;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    /* Đồng bộ sức chứa lớp với phòng hiện tại; dữ liệu demo không để lớp vượt phòng. */
    UPDATE l
    SET l.SucChua = p.SucChua
    FROM dbo.LopTap l
    JOIN dbo.PhongTap p ON p.MaPhong = l.MaPhong
    WHERE l.SucChua > p.SucChua;

    /* Tạo một buổi lịch sử cho mỗi lớp có điểm danh cũ chưa gắn buổi. */
    DECLARE @NgayLegacy DATE = '2026-05-20';
    INSERT dbo.LichHoc(MaLop, MaHLV, MaPhong, ThoiGianBatDau, ThoiGianKetThuc, TrangThai)
    SELECT l.MaLop, l.MaHLV, l.MaPhong,
           DATEADD(MINUTE, DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), l.GioBatDau), CAST(@NgayLegacy AS DATETIME2)),
           DATEADD(MINUTE, DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), l.GioKetThuc), CAST(@NgayLegacy AS DATETIME2)),
           N'Đã diễn ra'
    FROM dbo.LopTap l
    WHERE EXISTS (
              SELECT 1
              FROM dbo.DiemDanhLop dd
              JOIN dbo.DangKyLop d ON d.MaDKL = dd.MaDKL
              WHERE dd.MaLichHoc IS NULL AND d.MaLop = l.MaLop AND dd.NgayDiemDanh = @NgayLegacy
          )
      AND NOT EXISTS (
              SELECT 1 FROM dbo.LichHoc lh
              WHERE lh.MaLop = l.MaLop
                AND CAST(lh.ThoiGianBatDau AS DATE) = @NgayLegacy
          );

    UPDATE dd
    SET dd.MaLichHoc = lh.MaLichHoc,
        dd.LaDuLieuCu = 0
    FROM dbo.DiemDanhLop dd
    JOIN dbo.DangKyLop d ON d.MaDKL = dd.MaDKL
    JOIN dbo.LichHoc lh ON lh.MaLop = d.MaLop
                         AND CAST(lh.ThoiGianBatDau AS DATE) = dd.NgayDiemDanh
                         AND lh.TrangThai = N'Đã diễn ra'
    WHERE dd.MaLichHoc IS NULL;

    /* Bao phủ các ngày legacy khác nếu dữ liệu demo có nhiều mốc lịch sử. */
    INSERT dbo.LichHoc(MaLop, MaHLV, MaPhong, ThoiGianBatDau, ThoiGianKetThuc, TrangThai)
    SELECT DISTINCT l.MaLop, l.MaHLV, l.MaPhong,
           DATEADD(MINUTE, DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), l.GioBatDau), CAST(dd.NgayDiemDanh AS DATETIME2)),
           DATEADD(MINUTE, DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), l.GioKetThuc), CAST(dd.NgayDiemDanh AS DATETIME2)),
           N'Đã diễn ra'
    FROM dbo.DiemDanhLop dd
    JOIN dbo.DangKyLop d ON d.MaDKL = dd.MaDKL
    JOIN dbo.LopTap l ON l.MaLop = d.MaLop
    WHERE dd.MaLichHoc IS NULL
      AND NOT EXISTS (
              SELECT 1 FROM dbo.LichHoc lh
              WHERE lh.MaLop = l.MaLop
                AND CAST(lh.ThoiGianBatDau AS DATE) = dd.NgayDiemDanh
          );

    UPDATE dd
    SET dd.MaLichHoc = lh.MaLichHoc,
        dd.LaDuLieuCu = 0
    FROM dbo.DiemDanhLop dd
    JOIN dbo.DangKyLop d ON d.MaDKL = dd.MaDKL
    JOIN dbo.LichHoc lh ON lh.MaLop = d.MaLop
                         AND CAST(lh.ThoiGianBatDau AS DATE) = dd.NgayDiemDanh
                         AND lh.TrangThai = N'Đã diễn ra'
    WHERE dd.MaLichHoc IS NULL;

    /* Tạo tối thiểu một buổi tương lai cho mọi lớp đang mở chưa có lịch tương lai. */
    DECLARE @NgayDemo DATE = DATEADD(DAY, 1, CAST(SYSDATETIME() AS DATE));
    INSERT dbo.LichHoc(MaLop, MaHLV, MaPhong, ThoiGianBatDau, ThoiGianKetThuc, TrangThai)
    SELECT l.MaLop, l.MaHLV, l.MaPhong,
           DATEADD(MINUTE, DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), l.GioBatDau),
                   CAST(DATEADD(DAY, l.MaLop - 1, @NgayDemo) AS DATETIME2)),
           DATEADD(MINUTE, DATEDIFF(MINUTE, CAST('00:00:00' AS TIME), l.GioKetThuc),
                   CAST(DATEADD(DAY, l.MaLop - 1, @NgayDemo) AS DATETIME2)),
           N'Đã lên lịch'
    FROM dbo.LopTap l
    WHERE l.TrangThai = N'Đang mở'
      AND NOT EXISTS (
              SELECT 1 FROM dbo.LichHoc lh
              WHERE lh.MaLop = l.MaLop
                AND lh.ThoiGianBatDau >= CAST(@NgayDemo AS DATETIME2)
                AND lh.TrangThai = N'Đã lên lịch'
          );

    /* Nguồn bán legacy được giữ riêng; view chỉ phục vụ trình diễn/đối soát. */
    COMMIT;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK;
    THROW;
END CATCH;
GO

CREATE OR ALTER VIEW dbo.vw_BanHangLegacyDoiSoat AS
SELECT sp.MaSP, sp.MaSanPham, sp.TenSP,
       COUNT_BIG(*) AS SoDongLegacy,
       SUM(pb.SoLuong) AS SoLuongLegacy,
       SUM(CONVERT(DECIMAL(18,2), pb.SoLuong) * pb.DonGia) AS DoanhThuLegacy
FROM dbo.ChiTietPhieuBanHang pb
JOIN dbo.SanPham sp ON sp.MaSP = pb.MaSP
GROUP BY sp.MaSP, sp.MaSanPham, sp.TenSP;
GO

GO

/* ========================================================================================
   PHẦN: 18_phan_quyen_nghiep_vu.sql
   ======================================================================================== */
GO
/*
   Migration 18 - lớp bảo vệ quyền ở database cho các giao dịch có MaNV.
   Lớp UI/BLL vẫn là nơi quyết định quyền menu; các trigger này là guard cuối
   cho những bảng nhạy cảm có actor rõ ràng, tránh insert trực tiếp bằng MaNV
   không thuộc tài khoản hoạt động/hợp lệ.
*/
USE QL_PhongGym;
GO

CREATE OR ALTER FUNCTION dbo.fn_NhanVienCoVaiTro
(
    @MaNV INT,
    @VaiTroChoPhep NVARCHAR(400)
)
RETURNS BIT
AS
BEGIN
    DECLARE @KetQua BIT = 0;
    IF EXISTS
    (
        SELECT 1
        FROM dbo.TaiKhoan tk
        WHERE tk.MaNV = @MaNV
          AND tk.TrangThai = N'Hoạt động'
          AND CHARINDEX(N'|' + tk.VaiTro + N'|', N'|' + @VaiTroChoPhep + N'|') > 0
    ) SET @KetQua = 1;
    RETURN @KetQua;
END;
GO

CREATE OR ALTER TRIGGER dbo.TR_Auth_HoaDon
ON dbo.HoaDon
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS
    (
        SELECT 1
        FROM inserted i
        WHERE i.MaNV IS NULL
           OR dbo.fn_NhanVienCoVaiTro(i.MaNV, N'Admin|Quản lý|Lễ tân|Kế toán|Nhân viên bán hàng|Nhân viên|HLV|Huấn luyện viên') = 0
    )
        THROW 52101, N'Tài khoản thực hiện hóa đơn không có quyền hoặc không còn hoạt động.', 1;
END;
GO

CREATE OR ALTER TRIGGER dbo.TR_Auth_ThanhToan
ON dbo.ThanhToan
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS
    (
        SELECT 1
        FROM inserted i
        WHERE i.MaNV IS NULL
           OR dbo.fn_NhanVienCoVaiTro(i.MaNV, N'Admin|Quản lý|Kế toán|Lễ tân') = 0
    )
        THROW 52102, N'Tài khoản thực hiện thanh toán không có quyền.', 1;
END;
GO

CREATE OR ALTER TRIGGER dbo.TR_Auth_LichSuTonKho
ON dbo.LichSuTonKho
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS
    (
        SELECT 1
        FROM inserted i
        WHERE i.MaNV IS NULL
           OR dbo.fn_NhanVienCoVaiTro(i.MaNV, N'Admin|Quản lý|Nhân viên bán hàng|Nhân viên') = 0
    )
        THROW 52103, N'Tài khoản thực hiện biến động kho không có quyền.', 1;
END;
GO

CREATE OR ALTER TRIGGER dbo.TR_Auth_BaoTri
ON dbo.BaoTriThietBi
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS
    (
        SELECT 1
        FROM inserted i
        WHERE i.MaNV IS NULL
           OR dbo.fn_NhanVienCoVaiTro(i.MaNV, N'Admin|Quản lý|Nhân viên') = 0
    )
        THROW 52104, N'Tài khoản thực hiện bảo trì không có quyền.', 1;
END;
GO

CREATE OR ALTER TRIGGER dbo.TR_Auth_LichSuGoi
ON dbo.LichSuGoiTap
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS
    (
        SELECT 1
        FROM inserted i
        WHERE i.MaNV IS NOT NULL
          AND dbo.fn_NhanVienCoVaiTro(i.MaNV, N'Admin|Quản lý|Lễ tân|Nhân viên') = 0
    )
        THROW 52105, N'Tài khoản cập nhật vòng đời gói không có quyền.', 1;
END;
GO

GO

/* ========================================================================================
   PHẦN: 19_sua_nghiep_vu_phien_11.sql
   ======================================================================================== */
GO
/*
   Migration 19 - Phiên 11: hoàn tiền, check-in phòng, bảo lưu và trạng thái PT.
   Chạy sau database/18_phan_quyen_nghiep_vu.sql trên database QL_PhongGym.
   Các procedure dùng transaction; có thể chạy lại an toàn.
*/
USE QL_PhongGym;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
GO

/* PT chưa thanh toán không được sử dụng/sắp lịch. */
DECLARE @dropStatus NVARCHAR(MAX) = N'';
SELECT @dropStatus = @dropStatus + N'ALTER TABLE dbo.DangKyPT DROP CONSTRAINT ' + QUOTENAME(cc.name) + N';'
FROM sys.check_constraints cc
WHERE cc.parent_object_id = OBJECT_ID(N'dbo.DangKyPT')
  AND cc.definition LIKE N'%TrangThai%';
IF @dropStatus <> N'' EXEC sys.sp_executesql @dropStatus;
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = N'CK_DangKyPT_TrangThai_v3')
    ALTER TABLE dbo.DangKyPT ADD CONSTRAINT CK_DangKyPT_TrangThai_v3
        CHECK (TrangThai IN (N'Chờ thanh toán', N'Đang học', N'Hoàn thành', N'Hủy'));
GO

CREATE OR ALTER PROCEDURE dbo.sp_DangKyPT
    @MaTV INT, @MaHLV INT, @SoBuoi INT, @MaKM INT = NULL,
    @NgayBatDau DATE = NULL, @NgayKetThuc DATE = NULL, @MaNV INT,
    @MaDKPT INT OUTPUT, @MaHD INT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        IF @SoBuoi <= 0 THROW 51030, N'Số buổi PT phải lớn hơn 0.', 1;
        IF NOT EXISTS (SELECT 1 FROM dbo.ThanhVien WHERE MaTV=@MaTV AND TrangThai=N'Hoạt động' AND DaXoa=0)
            THROW 51031, N'Thành viên không hoạt động.', 1;
        DECLARE @Phi DECIMAL(18,2), @Giam DECIMAL(5,2)=0;
        SELECT @Phi=PhiThue FROM dbo.HuanLuyenVien h JOIN dbo.NhanVien n ON n.MaNV=h.MaNV
        WHERE h.MaHLV=@MaHLV AND n.TrangThai=N'Đang làm';
        IF @Phi IS NULL THROW 51032, N'Huấn luyện viên không hợp lệ.', 1;
        IF @MaKM IS NOT NULL
            SELECT @Giam=PhanTramGiam FROM dbo.KhuyenMai
            WHERE MaKM=@MaKM AND TrangThai=N'Hoạt động' AND CAST(GETDATE() AS DATE) BETWEEN NgayBatDau AND NgayKetThuc;
        IF @MaKM IS NOT NULL AND @Giam IS NULL THROW 51033, N'Khuyến mãi không còn hiệu lực.', 1;
        SET @NgayBatDau=ISNULL(@NgayBatDau, CAST(GETDATE() AS DATE));
        SET @NgayKetThuc=ISNULL(@NgayKetThuc, DATEADD(DAY, @SoBuoi*7, @NgayBatDau));
        IF @NgayKetThuc < @NgayBatDau THROW 51034, N'Khoảng thời gian PT không hợp lệ.', 1;

        INSERT dbo.DangKyPT(MaTV,MaHLV,MaKM,SoBuoi,NgayBatDau,NgayKetThuc,TongTien,TrangThai)
        VALUES(@MaTV,@MaHLV,@MaKM,@SoBuoi,@NgayBatDau,@NgayKetThuc,
               ROUND(@Phi*@SoBuoi*(1-ISNULL(@Giam,0)/100),2),N'Chờ thanh toán');
        SET @MaDKPT=SCOPE_IDENTITY();
        INSERT dbo.HoaDon(MaTV,MaNV,TongTien,TrangThai) VALUES(@MaTV,@MaNV,0,N'Chưa thanh toán');
        SET @MaHD=SCOPE_IDENTITY();
        INSERT dbo.ChiTietHoaDon(MaHD,LoaiDichVu,MaThamChieu,SoLuong,DonGia)
        SELECT @MaHD,N'PT',@MaDKPT,SoBuoi,ROUND(TongTien/SoBuoi,2)
        FROM dbo.DangKyPT WHERE MaDKPT=@MaDKPT;
        INSERT dbo.HoaDonPT(MaCTHD,MaDKPT)
        SELECT MaCTHD,@MaDKPT FROM dbo.ChiTietHoaDon WHERE MaHD=@MaHD;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK; THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_ThanhToanHoaDon
    @MaHD INT, @SoTien DECIMAL(18,2), @PhuongThuc NVARCHAR(50), @MaNV INT=NULL, @MaTT INT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        IF @SoTien<=0 THROW 50019, N'Số tiền thanh toán phải dương.', 1;
        DECLARE @Tong DECIMAL(18,2), @DaTra DECIMAL(18,2);
        SELECT @Tong=TongTien FROM dbo.HoaDon WITH (UPDLOCK,HOLDLOCK)
        WHERE MaHD=@MaHD AND TrangThai NOT IN(N'Hủy',N'Đã hoàn tiền');
        IF @Tong IS NULL THROW 50020, N'Hóa đơn không thể thanh toán.', 1;
        SELECT @DaTra=ISNULL(SUM(CASE WHEN LoaiGiaoDich=N'Thanh toán' THEN SoTien ELSE -SoTien END),0)
        FROM dbo.ThanhToan WHERE MaHD=@MaHD AND TrangThai=N'Thành công';
        IF @DaTra+@SoTien>@Tong THROW 50021, N'Thanh toán vượt quá số tiền phải thu.', 1;
        INSERT dbo.ThanhToan(MaHD,SoTien,PhuongThuc,TrangThai,LoaiGiaoDich,MaNV)
        VALUES(@MaHD,@SoTien,@PhuongThuc,N'Thành công',N'Thanh toán',@MaNV);
        SET @MaTT=SCOPE_IDENTITY();
        UPDATE dbo.HoaDon SET TrangThai=CASE WHEN @DaTra+@SoTien=@Tong THEN N'Đã thanh toán' ELSE N'Đã thanh toán một phần' END,
            NgayCapNhat=SYSDATETIME() WHERE MaHD=@MaHD;
        IF @DaTra+@SoTien=@Tong
        BEGIN
            UPDATE d SET TrangThai=N'Còn hạn',NgayCapNhat=SYSDATETIME()
            FROM dbo.DangKyGoi d JOIN dbo.HoaDonGoiTap x ON x.MaDKG=d.MaDKG
            JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD
            WHERE c.MaHD=@MaHD AND d.TrangThai=N'Chờ thanh toán';
            UPDATE d SET TrangThai=N'Đang học'
            FROM dbo.DangKyPT d JOIN dbo.HoaDonPT x ON x.MaDKPT=d.MaDKPT
            JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD
            WHERE c.MaHD=@MaHD AND d.TrangThai=N'Chờ thanh toán';
        END;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK; THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_HoanTien
    @MaHD INT, @SoTien DECIMAL(18,2), @PhuongThuc NVARCHAR(50), @MaNV INT=NULL, @MaTT INT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        DECLARE @DaThu DECIMAL(18,2), @Tong DECIMAL(18,2);
        SELECT @Tong=TongTien FROM dbo.HoaDon WITH (UPDLOCK,HOLDLOCK) WHERE MaHD=@MaHD;
        IF @Tong IS NULL THROW 50020, N'Hóa đơn không tồn tại.', 1;
        SELECT @DaThu=ISNULL(SUM(CASE WHEN LoaiGiaoDich=N'Thanh toán' THEN SoTien ELSE -SoTien END),0)
        FROM dbo.ThanhToan WITH (UPDLOCK,HOLDLOCK) WHERE MaHD=@MaHD AND TrangThai=N'Thành công';
        IF @SoTien<=0 OR @SoTien>@DaThu THROW 50022, N'Số tiền hoàn không hợp lệ.', 1;
        INSERT dbo.ThanhToan(MaHD,SoTien,PhuongThuc,TrangThai,LoaiGiaoDich,MaNV)
        VALUES(@MaHD,@SoTien,@PhuongThuc,N'Thành công',N'Hoàn tiền',@MaNV);
        SET @MaTT=SCOPE_IDENTITY();
        UPDATE dbo.HoaDon SET TrangThai=CASE WHEN @SoTien=@DaThu THEN N'Đã hoàn tiền' ELSE N'Đã thanh toán một phần' END,
            NgayCapNhat=SYSDATETIME() WHERE MaHD=@MaHD;

        IF @SoTien=@DaThu
        BEGIN
            UPDATE d SET TrangThai=N'Hủy',NgayCapNhat=SYSDATETIME()
            FROM dbo.DangKyGoi d JOIN dbo.HoaDonGoiTap x ON x.MaDKG=d.MaDKG
            JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD WHERE c.MaHD=@MaHD AND d.TrangThai<>N'Hủy';
            INSERT dbo.LichSuGoiTap(MaDKG,HanhDong,GhiChu,MaNV)
            SELECT d.MaDKG,N'Hủy do hoàn tiền toàn bộ',N'Hóa đơn '+CONVERT(NVARCHAR(30),@MaHD),@MaNV
            FROM dbo.DangKyGoi d JOIN dbo.HoaDonGoiTap x ON x.MaDKG=d.MaDKG
            JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD WHERE c.MaHD=@MaHD;
            UPDATE d SET TrangThai=N'Hủy'
            FROM dbo.DangKyPT d JOIN dbo.HoaDonPT x ON x.MaDKPT=d.MaDKPT
            JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD WHERE c.MaHD=@MaHD AND d.TrangThai<>N'Hủy';
        END;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK; THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_BaoLuuGoiTap
    @MaDKG INT, @TuNgay DATE, @DenNgay DATE, @LyDo NVARCHAR(500), @MaNV INT=NULL
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        DECLARE @HomNay DATE=CAST(SYSDATETIME() AS DATE), @BatDau DATE, @KetThuc DATE;
        IF @DenNgay<@TuNgay THROW 50008, N'Khoảng bảo lưu không hợp lệ.', 1;
        IF @TuNgay<@HomNay THROW 50040, N'Ngày bắt đầu bảo lưu không được ở quá khứ.', 1;
        IF NULLIF(LTRIM(RTRIM(@LyDo)),N'') IS NULL THROW 50041, N'Lý do bảo lưu không được để trống.', 1;
        SELECT @BatDau=NgayBatDau,@KetThuc=NgayKetThuc FROM dbo.DangKyGoi WITH(UPDLOCK,HOLDLOCK)
        WHERE MaDKG=@MaDKG AND TrangThai=N'Còn hạn';
        IF @BatDau IS NULL THROW 50009, N'Chỉ bảo lưu gói còn hạn.', 1;
        IF @TuNgay<@BatDau OR @DenNgay>@KetThuc
            THROW 50042, N'Khoảng bảo lưu phải nằm trong thời hạn gói tập.', 1;
        IF EXISTS
        (
            SELECT 1 FROM dbo.BaoLuuGoiTap WITH(UPDLOCK,HOLDLOCK)
            WHERE MaDKG=@MaDKG AND TrangThai=N'Đang bảo lưu'
              AND @TuNgay<=DenNgay AND @DenNgay>=TuNgay
        ) THROW 50043, N'Khoảng bảo lưu bị trùng với lần bảo lưu đang có.', 1;
        INSERT dbo.BaoLuuGoiTap(MaDKG,TuNgay,DenNgay,LyDo,MaNV)
        VALUES(@MaDKG,@TuNgay,@DenNgay,LTRIM(RTRIM(@LyDo)),@MaNV);
        UPDATE dbo.DangKyGoi SET TrangThai=N'Tạm dừng',NgayTamDung=SYSDATETIME(),
            TongSoNgayBaoLuu=TongSoNgayBaoLuu+DATEDIFF(DAY,@TuNgay,@DenNgay)+1,NgayCapNhat=SYSDATETIME()
        WHERE MaDKG=@MaDKG;
        INSERT dbo.LichSuGoiTap(MaDKG,HanhDong,TuNgay,DenNgay,GhiChu,MaNV)
        VALUES(@MaDKG,N'Bảo lưu',@TuNgay,@DenNgay,LTRIM(RTRIM(@LyDo)),@MaNV);
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK; THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_CheckIn
    @MaTV INT, @MaPhong INT=NULL, @Kenh NVARCHAR(20)=N'Quầy', @MaCheckIn BIGINT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        DECLARE @HienTai DATETIME2(0)=SYSDATETIME(), @GioHienTai TIME(0)=CAST(SYSDATETIME() AS TIME(0)), @MaDKG INT;
        IF @MaPhong IS NOT NULL AND NOT EXISTS
            (SELECT 1 FROM dbo.PhongTap WHERE MaPhong=@MaPhong AND TrangThai=N'Hoạt động')
            THROW 50038, N'Phòng không tồn tại hoặc đang bảo trì/đóng.', 1;
        IF EXISTS(SELECT 1 FROM dbo.LichSuCheckIn WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND ThoiGianRa IS NULL)
            THROW 50010, N'Thành viên đang có lượt check-in mở.', 1;
        SELECT TOP(1) @MaDKG=d.MaDKG
        FROM dbo.DangKyGoi d WITH(UPDLOCK,HOLDLOCK) JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi JOIN dbo.ThanhVien t ON t.MaTV=d.MaTV
        WHERE d.MaTV=@MaTV AND d.TrangThai=N'Còn hạn' AND d.NgayBatDau<=CAST(@HienTai AS DATE) AND d.NgayKetThuc>=CAST(@HienTai AS DATE)
          AND t.TrangThai=N'Hoạt động' AND t.DaXoa=0 AND (g.GioiHanLuot IS NULL OR ISNULL(d.SoLuotDaDung,0)<g.GioiHanLuot)
          AND (g.GioBatDau IS NULL OR g.GioKetThuc IS NULL OR
              (g.GioBatDau<=g.GioKetThuc AND @GioHienTai BETWEEN g.GioBatDau AND g.GioKetThuc) OR
              (g.GioBatDau>g.GioKetThuc AND (@GioHienTai>=g.GioBatDau OR @GioHienTai<=g.GioKetThuc)))
          AND EXISTS(SELECT 1 FROM dbo.HoaDonGoiTap x JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD JOIN dbo.HoaDon h ON h.MaHD=c.MaHD
                     WHERE x.MaDKG=d.MaDKG AND h.TrangThai=N'Đã thanh toán')
        ORDER BY d.NgayKetThuc DESC,d.MaDKG DESC;
        IF @MaDKG IS NULL THROW 50011, N'Không có gói hợp lệ đã thanh toán, còn lượt và đúng khung giờ để check-in.', 1;
        INSERT dbo.LichSuCheckIn(MaTV,MaDKG,MaPhong,Kenh) VALUES(@MaTV,@MaDKG,@MaPhong,@Kenh);
        SET @MaCheckIn=CONVERT(BIGINT,SCOPE_IDENTITY());
        UPDATE dbo.DangKyGoi SET SoLuotDaDung=ISNULL(SoLuotDaDung,0)+1,NgayCapNhat=@HienTai WHERE MaDKG=@MaDKG;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK; THROW;
    END CATCH;
END;
GO

GO

/* ========================================================================================
   PHẦN: 20_sua_bao_mat_validation_phien_12.sql
   ======================================================================================== */
GO
/*
   Migration 20 - Phiên 12: actor bắt buộc ở database, audit/lockout đăng nhập
   và ràng buộc định dạng dữ liệu liên hệ.
   Chạy sau migration 19.
*/
USE QL_PhongGym;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
GO

IF COL_LENGTH(N'dbo.TaiKhoan', N'SoLanDangNhapSai') IS NULL
    ALTER TABLE dbo.TaiKhoan ADD SoLanDangNhapSai INT NOT NULL CONSTRAINT DF_TaiKhoan_SoLanDangNhapSai DEFAULT 0;
IF COL_LENGTH(N'dbo.TaiKhoan', N'KhoaDen') IS NULL
    ALTER TABLE dbo.TaiKhoan ADD KhoaDen DATETIME2(0) NULL;
IF COL_LENGTH(N'dbo.LichSuDangNhap', N'TenDangNhap') IS NULL
    ALTER TABLE dbo.LichSuDangNhap ADD TenDangNhap VARCHAR(50) NULL;
GO

DECLARE @fkLogin NVARCHAR(300) = N'';
SELECT @fkLogin = @fkLogin + N'ALTER TABLE dbo.LichSuDangNhap DROP CONSTRAINT ' + QUOTENAME(fk.name) + N';'
FROM sys.foreign_keys fk
WHERE fk.parent_object_id = OBJECT_ID(N'dbo.LichSuDangNhap')
  AND EXISTS (SELECT 1 FROM sys.foreign_key_columns fkc WHERE fkc.constraint_object_id=fk.object_id
              AND fkc.parent_column_id= (SELECT column_id FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.LichSuDangNhap') AND name=N'MaTK'));
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.LichSuDangNhap') AND name=N'MaTK' AND is_nullable=0)
BEGIN
    IF @fkLogin <> N'' EXEC sys.sp_executesql @fkLogin;
    ALTER TABLE dbo.LichSuDangNhap ALTER COLUMN MaTK INT NULL;
END;
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name=N'FK_LichSuDangNhap_TaiKhoan_v3')
    ALTER TABLE dbo.LichSuDangNhap ADD CONSTRAINT FK_LichSuDangNhap_TaiKhoan_v3 FOREIGN KEY(MaTK) REFERENCES dbo.TaiKhoan(MaTK);
GO

IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_ThanhVien_SDT_v3')
    ALTER TABLE dbo.ThanhVien ADD CONSTRAINT CK_ThanhVien_SDT_v3
        CHECK (SDT NOT LIKE '%[^0-9]%' AND SDT LIKE '0%' AND LEN(SDT) BETWEEN 10 AND 11);
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_ThanhVien_Email_v3')
    ALTER TABLE dbo.ThanhVien ADD CONSTRAINT CK_ThanhVien_Email_v3
        CHECK (Email IS NULL OR (Email NOT LIKE '% %' AND Email LIKE '%_@_%._%'));
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_NhanVien_SDT_v3')
    ALTER TABLE dbo.NhanVien ADD CONSTRAINT CK_NhanVien_SDT_v3
        CHECK (SDT NOT LIKE '%[^0-9]%' AND SDT LIKE '0%' AND LEN(SDT) BETWEEN 10 AND 11);
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_NhanVien_Email_v3')
    ALTER TABLE dbo.NhanVien ADD CONSTRAINT CK_NhanVien_Email_v3
        CHECK (Email IS NULL OR (Email NOT LIKE '% %' AND Email LIKE '%_@_%._%'));
GO

CREATE OR ALTER PROCEDURE dbo.sp_GhiKetQuaDangNhap
    @MaTK INT=NULL,
    @TenDangNhap VARCHAR(50),
    @ThanhCong BIT,
    @ThietBi NVARCHAR(255)=NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRAN;
    IF @MaTK IS NOT NULL
    BEGIN
        IF @ThanhCong=1
            UPDATE dbo.TaiKhoan SET SoLanDangNhapSai=0,KhoaDen=NULL,LanDangNhapCuoi=SYSDATETIME() WHERE MaTK=@MaTK;
        ELSE
            UPDATE dbo.TaiKhoan
            SET SoLanDangNhapSai=ISNULL(SoLanDangNhapSai,0)+1,
                KhoaDen=CASE WHEN ISNULL(SoLanDangNhapSai,0)+1>=5 THEN DATEADD(MINUTE,15,SYSDATETIME()) ELSE KhoaDen END
            WHERE MaTK=@MaTK;
    END;
    INSERT dbo.LichSuDangNhap(MaTK,TenDangNhap,ThanhCong,ThietBi)
    VALUES(@MaTK,@TenDangNhap,@ThanhCong,@ThietBi);
    COMMIT;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_CheckIn
    @MaTV INT, @MaPhong INT=NULL, @Kenh NVARCHAR(20)=N'Quầy', @MaCheckIn BIGINT OUTPUT, @MaNV INT=NULL
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|Lễ tân|Nhân viên')=0
            THROW 52106,N'Tài khoản thực hiện check-in không có quyền hoặc không còn hoạt động.',1;
        DECLARE @HienTai DATETIME2(0)=SYSDATETIME(), @GioHienTai TIME(0)=CAST(SYSDATETIME() AS TIME(0)), @MaDKG INT;
        IF @MaPhong IS NOT NULL AND NOT EXISTS(SELECT 1 FROM dbo.PhongTap WHERE MaPhong=@MaPhong AND TrangThai=N'Hoạt động')
            THROW 50038,N'Phòng không tồn tại hoặc đang bảo trì/đóng.',1;
        IF EXISTS(SELECT 1 FROM dbo.LichSuCheckIn WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND ThoiGianRa IS NULL)
            THROW 50010,N'Thành viên đang có lượt check-in mở.',1;
        SELECT TOP(1) @MaDKG=d.MaDKG
        FROM dbo.DangKyGoi d WITH(UPDLOCK,HOLDLOCK) JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi JOIN dbo.ThanhVien t ON t.MaTV=d.MaTV
        WHERE d.MaTV=@MaTV AND d.TrangThai=N'Còn hạn' AND d.NgayBatDau<=CAST(@HienTai AS DATE) AND d.NgayKetThuc>=CAST(@HienTai AS DATE)
          AND t.TrangThai=N'Hoạt động' AND t.DaXoa=0 AND (g.GioiHanLuot IS NULL OR ISNULL(d.SoLuotDaDung,0)<g.GioiHanLuot)
          AND (g.GioBatDau IS NULL OR g.GioKetThuc IS NULL OR (g.GioBatDau<=g.GioKetThuc AND @GioHienTai BETWEEN g.GioBatDau AND g.GioKetThuc) OR (g.GioBatDau>g.GioKetThuc AND (@GioHienTai>=g.GioBatDau OR @GioHienTai<=g.GioKetThuc)))
          AND EXISTS(SELECT 1 FROM dbo.HoaDonGoiTap x JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD JOIN dbo.HoaDon h ON h.MaHD=c.MaHD WHERE x.MaDKG=d.MaDKG AND h.TrangThai=N'Đã thanh toán')
        ORDER BY d.NgayKetThuc DESC,d.MaDKG DESC;
        IF @MaDKG IS NULL THROW 50011,N'Không có gói hợp lệ đã thanh toán, còn lượt và đúng khung giờ để check-in.',1;
        INSERT dbo.LichSuCheckIn(MaTV,MaDKG,MaPhong,Kenh) VALUES(@MaTV,@MaDKG,@MaPhong,@Kenh);
        SET @MaCheckIn=CONVERT(BIGINT,SCOPE_IDENTITY());
        UPDATE dbo.DangKyGoi SET SoLuotDaDung=ISNULL(SoLuotDaDung,0)+1,NgayCapNhat=@HienTai WHERE MaDKG=@MaDKG;
        INSERT dbo.NhatKyHeThong(MaNV,HanhDong,DoiTuong,KhoaDoiTuong,NoiDung) VALUES(@MaNV,N'Check-in',N'LichSuCheckIn',CONVERT(NVARCHAR(30),@MaCheckIn),@Kenh);
        COMMIT;
    END TRY
    BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_CheckOut @MaTV INT, @MaNV INT=NULL AS
BEGIN TRY
    IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|Lễ tân|Nhân viên')=0
        THROW 52107,N'Tài khoản thực hiện check-out không có quyền hoặc không còn hoạt động.',1;
    UPDATE dbo.LichSuCheckIn SET ThoiGianRa=SYSDATETIME() WHERE MaTV=@MaTV AND ThoiGianRa IS NULL;
    IF @@ROWCOUNT=0 THROW 50012,N'Không tìm thấy lượt check-in mở.',1;
END TRY BEGIN CATCH THROW; END CATCH;
GO

CREATE OR ALTER PROCEDURE dbo.sp_DangKyLop
    @MaTV INT, @MaLop INT, @VaoDanhSachCho BIT=1, @MaDKL INT OUTPUT, @MaNV INT=NULL
AS
BEGIN TRY
    SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
    IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|Lễ tân|Nhân viên')=0
        THROW 52108,N'Tài khoản thực hiện đăng ký lớp không có quyền hoặc không còn hoạt động.',1;
    IF NOT EXISTS(SELECT 1 FROM dbo.ThanhVien WHERE MaTV=@MaTV AND TrangThai=N'Hoạt động' AND DaXoa=0) THROW 50028,N'Thành viên không hoạt động.',1;
    DECLARE @SucChua INT,@DangKy INT;
    SELECT @SucChua=SucChua FROM dbo.LopTap WITH(UPDLOCK,HOLDLOCK) WHERE MaLop=@MaLop AND TrangThai=N'Đang mở';
    IF @SucChua IS NULL THROW 50013,N'Lớp không mở hoặc không tồn tại.',1;
    IF EXISTS(SELECT 1 FROM dbo.DangKyLop WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND MaLop=@MaLop AND TrangThai=N'Đang tham gia') THROW 50029,N'Thành viên đã đăng ký lớp này.',1;
    IF EXISTS(SELECT 1 FROM dbo.DanhSachCho WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND MaLop=@MaLop AND TrangThai=N'Đang chờ') THROW 50030,N'Thành viên đang ở danh sách chờ của lớp này.',1;
    SELECT @DangKy=COUNT(*) FROM dbo.DangKyLop WHERE MaLop=@MaLop AND TrangThai=N'Đang tham gia';
    IF @DangKy>=@SucChua
    BEGIN
        IF @VaoDanhSachCho=1 BEGIN INSERT dbo.DanhSachCho(MaLop,MaTV) VALUES(@MaLop,@MaTV); SET @MaDKL=NULL; COMMIT; RETURN; END;
        THROW 50014,N'Lớp đã đủ sức chứa.',1;
    END;
    INSERT dbo.DangKyLop(MaTV,MaLop) VALUES(@MaTV,@MaLop); SET @MaDKL=SCOPE_IDENTITY(); COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO

CREATE OR ALTER PROCEDURE dbo.sp_TaoLichHoc
    @MaLop INT, @ThoiGianBatDau DATETIME2(0), @ThoiGianKetThuc DATETIME2(0), @MaLichHoc INT OUTPUT, @MaNV INT=NULL
AS
BEGIN TRY
    SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
    IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|HLV|Huấn luyện viên')=0 THROW 52109,N'Tài khoản tạo lịch không có quyền hoặc không còn hoạt động.',1;
    IF @ThoiGianKetThuc<=@ThoiGianBatDau THROW 51070,N'Khung giờ lớp không hợp lệ.',1;
    IF CAST(@ThoiGianBatDau AS DATE)<CAST(SYSDATETIME() AS DATE) THROW 51071,N'Không thể tạo buổi học trong quá khứ.',1;
    DECLARE @HLV INT,@Phong INT,@SucChuaLop INT,@SucChuaPhong INT,@ThuTrongTuan TINYINT;
    SELECT @HLV=MaHLV,@Phong=MaPhong,@SucChuaLop=SucChua,@ThuTrongTuan=ThuTrongTuan FROM dbo.LopTap WITH(UPDLOCK,HOLDLOCK) WHERE MaLop=@MaLop AND TrangThai=N'Đang mở';
    IF @HLV IS NULL OR @Phong IS NULL THROW 51072,N'Lớp chưa mở hoặc chưa được gán phòng.',1;
    SELECT @SucChuaPhong=SucChua FROM dbo.PhongTap WITH(UPDLOCK,HOLDLOCK) WHERE MaPhong=@Phong AND TrangThai=N'Hoạt động';
    IF @SucChuaPhong IS NULL OR @SucChuaPhong<@SucChuaLop THROW 51073,N'Phòng không hoạt động hoặc không đủ sức chứa cho lớp.',1;
    IF @ThuTrongTuan IS NOT NULL AND @ThuTrongTuan<>CONVERT(TINYINT,(DATEDIFF(DAY,'19000101',CAST(@ThoiGianBatDau AS DATE))%7)+2) THROW 51074,N'Ngày tạo buổi học không đúng lịch cố định của lớp.',1;
    IF EXISTS(SELECT 1 FROM dbo.LichHoc WITH(UPDLOCK,HOLDLOCK) WHERE MaLop=@MaLop AND ThoiGianBatDau=@ThoiGianBatDau) THROW 51075,N'Buổi học này đã được tạo.',1;
    IF EXISTS(SELECT 1 FROM dbo.LichHoc WITH(UPDLOCK,HOLDLOCK) WHERE TrangThai=N'Đã lên lịch' AND (MaPhong=@Phong OR MaHLV=@HLV) AND @ThoiGianBatDau<ThoiGianKetThuc AND @ThoiGianKetThuc>ThoiGianBatDau) THROW 51076,N'Phòng hoặc HLV bị trùng lịch.',1;
    INSERT dbo.LichHoc(MaLop,MaHLV,MaPhong,ThoiGianBatDau,ThoiGianKetThuc) VALUES(@MaLop,@HLV,@Phong,@ThoiGianBatDau,@ThoiGianKetThuc); SET @MaLichHoc=SCOPE_IDENTITY(); COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO

CREATE OR ALTER PROCEDURE dbo.sp_DiemDanhLop
    @MaDKL INT, @MaLichHoc INT, @TrangThai NVARCHAR(30), @MaNV INT=NULL
AS
BEGIN TRY
    SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
    IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|Lễ tân|Nhân viên|HLV|Huấn luyện viên')=0 THROW 52110,N'Tài khoản điểm danh không có quyền hoặc không còn hoạt động.',1;
    IF @TrangThai NOT IN(N'Có mặt',N'Vắng mặt') THROW 51050,N'Trạng thái điểm danh không hợp lệ.',1;
    DECLARE @NgayDiemDanh DATE;
    SELECT @NgayDiemDanh=CAST(ThoiGianBatDau AS DATE) FROM dbo.LichHoc WITH(UPDLOCK,HOLDLOCK) WHERE MaLichHoc=@MaLichHoc AND TrangThai IN(N'Đã lên lịch',N'Đã diễn ra');
    IF @NgayDiemDanh IS NULL THROW 51077,N'Buổi học không tồn tại hoặc đã bị hủy.',1;
    IF @NgayDiemDanh>CAST(SYSDATETIME() AS DATE) THROW 51078,N'Không thể điểm danh cho buổi học trong tương lai.',1;
    IF NOT EXISTS(SELECT 1 FROM dbo.DangKyLop d JOIN dbo.LichHoc l ON l.MaLop=d.MaLop WHERE d.MaDKL=@MaDKL AND l.MaLichHoc=@MaLichHoc AND d.TrangThai=N'Đang tham gia') THROW 51079,N'Học viên không thuộc buổi học này hoặc đăng ký không còn hiệu lực.',1;
    UPDATE dbo.DiemDanhLop SET TrangThai=@TrangThai,NgayDiemDanh=@NgayDiemDanh WHERE MaDKL=@MaDKL AND MaLichHoc=@MaLichHoc;
    IF @@ROWCOUNT=0 INSERT dbo.DiemDanhLop(MaDKL,MaLichHoc,NgayDiemDanh,TrangThai) VALUES(@MaDKL,@MaLichHoc,@NgayDiemDanh,@TrangThai);
    UPDATE dbo.LichHoc SET TrangThai=N'Đã diễn ra' WHERE MaLichHoc=@MaLichHoc AND TrangThai=N'Đã lên lịch';
    INSERT dbo.NhatKyHeThong(MaNV,HanhDong,DoiTuong,KhoaDoiTuong,NoiDung) VALUES(@MaNV,N'Điểm danh',N'DiemDanhLop',CONVERT(NVARCHAR(30),@MaDKL),@TrangThai);
    COMMIT;
END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
GO

GO

/* ========================================================================================
   PHẦN: 21_sua_giahan_checkin_baotri.sql
   ======================================================================================== */
GO
-- 21_sua_giahan_checkin_baotri.sql
-- Migration Phiên 13: Sửa tính tiền gia hạn gói, thứ tự check-in FIFO và hoàn tất bảo trì thiết bị
USE QL_PhongGym;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

-- 0. Bỏ ràng buộc UNIQUE 1:1 trên HoaDonGoiTap để 1 gói tập có thể có nhiều hóa đơn (đăng ký gốc + các lần gia hạn)
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UQ_HDGoiTap_DKG' AND object_id = OBJECT_ID('dbo.HoaDonGoiTap'))
    ALTER TABLE dbo.HoaDonGoiTap DROP CONSTRAINT UQ_HDGoiTap_DKG;
GO

-- 1. BUG-021: Cập nhật sp_CheckIn để ưu tiên gói hết hạn sớm nhất (FIFO)
IF OBJECT_ID('dbo.sp_CheckIn', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_CheckIn;
GO

CREATE PROCEDURE dbo.sp_CheckIn
    @MaTV INT, @MaPhong INT=NULL, @Kenh NVARCHAR(20)=N'Quầy', @MaCheckIn BIGINT OUTPUT, @MaNV INT=NULL
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|Lễ tân|Nhân viên')=0
            THROW 52106,N'Tài khoản thực hiện check-in không có quyền hoặc không còn hoạt động.',1;
        DECLARE @HienTai DATETIME2(0)=SYSDATETIME(), @GioHienTai TIME(0)=CAST(SYSDATETIME() AS TIME(0)), @MaDKG INT;
        IF @MaPhong IS NOT NULL AND NOT EXISTS(SELECT 1 FROM dbo.PhongTap WHERE MaPhong=@MaPhong AND TrangThai=N'Hoạt động')
            THROW 50038,N'Phòng không tồn tại hoặc đang bảo trì/đóng.',1;
        IF EXISTS(SELECT 1 FROM dbo.LichSuCheckIn WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND ThoiGianRa IS NULL)
            THROW 50010,N'Thành viên đang có lượt check-in mở.',1;
        -- Ưu tiên gói hết hạn sớm nhất (FIFO: NgayKetThuc ASC, MaDKG ASC)
        SELECT TOP(1) @MaDKG=d.MaDKG
        FROM dbo.DangKyGoi d WITH(UPDLOCK,HOLDLOCK)
        JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi
        JOIN dbo.ThanhVien t ON t.MaTV=d.MaTV
        WHERE d.MaTV=@MaTV AND d.TrangThai=N'Còn hạn' AND d.NgayBatDau<=CAST(@HienTai AS DATE) AND d.NgayKetThuc>=CAST(@HienTai AS DATE)
          AND t.TrangThai=N'Hoạt động' AND t.DaXoa=0 AND (g.GioiHanLuot IS NULL OR ISNULL(d.SoLuotDaDung,0)<g.GioiHanLuot)
          AND (g.GioBatDau IS NULL OR g.GioKetThuc IS NULL OR (g.GioBatDau<=g.GioKetThuc AND @GioHienTai BETWEEN g.GioBatDau AND g.GioKetThuc) OR (g.GioBatDau>g.GioKetThuc AND (@GioHienTai>=g.GioBatDau OR @GioHienTai<=g.GioKetThuc)))
          AND EXISTS(SELECT 1 FROM dbo.HoaDonGoiTap x JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD JOIN dbo.HoaDon h ON h.MaHD=c.MaHD WHERE x.MaDKG=d.MaDKG AND h.TrangThai=N'Đã thanh toán')
        ORDER BY d.NgayKetThuc ASC, d.MaDKG ASC;

        IF @MaDKG IS NULL THROW 50011,N'Không có gói hợp lệ đã thanh toán, còn lượt và đúng khung giờ để check-in.',1;
        INSERT dbo.LichSuCheckIn(MaTV,MaDKG,MaPhong,Kenh) VALUES(@MaTV,@MaDKG,@MaPhong,@Kenh);
        SET @MaCheckIn=CONVERT(BIGINT,SCOPE_IDENTITY());
        UPDATE dbo.DangKyGoi SET SoLuotDaDung=ISNULL(SoLuotDaDung,0)+1,NgayCapNhat=@HienTai WHERE MaDKG=@MaDKG;
        INSERT dbo.NhatKyHeThong(MaNV,HanhDong,DoiTuong,KhoaDoiTuong,NoiDung) VALUES(@MaNV,N'Check-in',N'LichSuCheckIn',CONVERT(NVARCHAR(30),@MaCheckIn),@Kenh);
        COMMIT;
    END TRY
    BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
END;
GO

-- 2. BUG-020: Cập nhật sp_GiaHanGoiTap để tính phí gia hạn và sinh hóa đơn thu tiền
IF OBJECT_ID('dbo.sp_GiaHanGoiTap', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GiaHanGoiTap;
GO

CREATE PROCEDURE dbo.sp_GiaHanGoiTap
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

        DECLARE @MaTV INT, @MaGoi INT, @Cu DATE, @GiaGoi DECIMAL(18,2), @ThoiHanGoi INT;
        SELECT @MaTV = d.MaTV, @MaGoi = d.MaGoi, @Cu = d.NgayKetThuc, @GiaGoi = g.Gia, @ThoiHanGoi = g.ThoiHanNgay
        FROM dbo.DangKyGoi d WITH (UPDLOCK, HOLDLOCK)
        JOIN dbo.GoiTap g ON g.MaGoi = d.MaGoi
        WHERE d.MaDKG = @MaDKG AND d.TrangThai IN (N'Còn hạn', N'Hết hạn');

        IF @Cu IS NULL
            THROW 50007, N'Gói tập không tồn tại hoặc không ở trạng thái có thể gia hạn.', 1;

        -- Tính phí gia hạn theo đơn giá ngày
        DECLARE @DonGiaNgay DECIMAL(18,2) = ROUND(@GiaGoi / CASE WHEN ISNULL(@ThoiHanGoi, 0) > 0 THEN @ThoiHanGoi ELSE 30 END, 0);
        DECLARE @PhiGiaHan DECIMAL(18,2) = @DonGiaNgay * @SoNgay;

        -- Sinh hóa đơn gia hạn
        INSERT dbo.HoaDon(MaTV, MaNV, TongTien, TrangThai)
        VALUES (@MaTV, @MaNV, 0, N'Chưa thanh toán');
        SET @MaHD = SCOPE_IDENTITY();

        INSERT dbo.ChiTietHoaDon(MaHD, LoaiDichVu, MaThamChieu, SoLuong, DonGia)
        VALUES (@MaHD, N'Gói tập', @MaDKG, @SoNgay, @PhiGiaHan);

        INSERT dbo.HoaDonGoiTap(MaCTHD, MaDKG)
        SELECT MaCTHD, @MaDKG FROM dbo.ChiTietHoaDon WHERE MaHD = @MaHD;

        -- Cập nhật ngày kết thúc
        DECLARE @Moc DATE = CASE WHEN @Cu < CAST(SYSDATETIME() AS DATE) THEN CAST(SYSDATETIME() AS DATE) ELSE @Cu END;
        DECLARE @Moi DATE = DATEADD(DAY, @SoNgay, @Moc);

        UPDATE dbo.DangKyGoi
        SET NgayKetThuc = @Moi,
            TrangThai = N'Còn hạn',
            NgayCapNhat = SYSDATETIME()
        WHERE MaDKG = @MaDKG;

        INSERT dbo.LichSuGoiTap(MaDKG, HanhDong, TuNgay, DenNgay, MaNV)
        VALUES (@MaDKG, N'Gia hạn', @Cu, @Moi, @MaNV);

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

-- 3. BUG-024: Cập nhật sp_BaoTriThietBi và tạo sp_HoanTatBaoTri
IF OBJECT_ID('dbo.sp_BaoTriThietBi', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_BaoTriThietBi;
GO

CREATE PROCEDURE dbo.sp_BaoTriThietBi
    @MaTB INT,
    @MaNV INT,
    @NoiDung NVARCHAR(255),
    @ChiPhi DECIMAL(18,2),
    @NgayDuKien DATE = NULL,
    @MaBaoTri INT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV, N'Admin|Quản lý|Nhân viên') = 0
            THROW 52110, N'Tài khoản thực hiện lập phiếu bảo trì không có quyền hoặc không còn hoạt động.', 1;

        IF NOT EXISTS (SELECT 1 FROM dbo.ThietBi WHERE MaTB = @MaTB)
            THROW 50027, N'Thiết bị không tồn tại.', 1;

        IF @ChiPhi < 0
            THROW 50039, N'Chi phí bảo trì không thể âm.', 1;

        -- Khi lập phiếu bảo trì mới, thiết bị luôn chuyển sang 'Bảo trì' và phiếu ở trạng thái 'Đang bảo trì'
        INSERT dbo.BaoTriThietBi(MaTB, MaNV, NoiDung, ChiPhi, NgayDuKien, TrangThai)
        VALUES (@MaTB, @MaNV, @NoiDung, @ChiPhi, @NgayDuKien, N'Đang bảo trì');
        SET @MaBaoTri = SCOPE_IDENTITY();

        UPDATE dbo.ThietBi
        SET TinhTrang = N'Bảo trì'
        WHERE MaTB = @MaTB;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

IF OBJECT_ID('dbo.sp_HoanTatBaoTri', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_HoanTatBaoTri;
GO

CREATE PROCEDURE dbo.sp_HoanTatBaoTri
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
        SELECT @MaTB = MaTB, @TrangThaiHienTai = TrangThai
        FROM dbo.BaoTriThietBi WITH (UPDLOCK, HOLDLOCK)
        WHERE MaBaoTri = @MaBaoTri;

        IF @MaTB IS NULL
            THROW 50040, N'Phiếu bảo trì không tồn tại.', 1;

        IF @TrangThaiHienTai = N'Hoàn thành'
            THROW 50041, N'Phiếu bảo trì này đã hoàn thành trước đó.', 1;

        UPDATE dbo.BaoTriThietBi
        SET TrangThai = N'Hoàn thành',
            NgayHoanThanh = CAST(SYSDATETIME() AS DATE),
            GhiChu = ISNULL(@GhiChu, GhiChu)
        WHERE MaBaoTri = @MaBaoTri;

        -- Nếu không còn phiếu bảo trì nào khác đang mở cho thiết bị này, trả thiết bị về 'Hoạt động'
        IF NOT EXISTS (
            SELECT 1 FROM dbo.BaoTriThietBi
            WHERE MaTB = @MaTB AND MaBaoTri <> @MaBaoTri AND TrangThai IN (N'Đang bảo trì', N'Chờ bảo trì')
        )
        BEGIN
            UPDATE dbo.ThietBi
            SET TinhTrang = N'Hoạt động'
            WHERE MaTB = @MaTB;
        END;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

GO

/* ========================================================================================
   PHẦN: 22_khach_vang_lai_va_lop_tap.sql
   ======================================================================================== */
GO
-- 22_khach_vang_lai_va_lop_tap.sql
-- Migration Phiên 14: Thêm bản ghi hệ thống Khách vãng lai cho bán hàng POS
USE QL_PhongGym;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

-- 1. Bổ sung bản ghi Khách vãng lai cho nghiệp vụ bán hàng POS
IF NOT EXISTS (SELECT 1 FROM dbo.ThanhVien WHERE SDT = '0900000000' OR HoTen = N'Khách vãng lai')
BEGIN
    INSERT INTO dbo.ThanhVien (HoTen, NgaySinh, GioiTinh, SDT, Email, DiaChi, MaLoaiTV, TrangThai)
    VALUES (N'Khách vãng lai', '2000-01-01', N'Khác', '0900000000', NULL, N'Bán lẻ tại quầy', 1, N'Hoạt động');
END;
GO

GO

/* ========================================================================================
   PHẦN: 25_bo_sung_mo_lai_bao_luu.sql
   ======================================================================================== */
GO
USE QL_PhongGym;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.sp_MoLaiBaoLuuGoiTap
    @MaDKG INT,
    @MaNV INT = NULL
AS
BEGIN TRY
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRAN;

    DECLARE @HomNay DATE = CAST(SYSDATETIME() AS DATE),
            @MaBaoLuu INT,
            @TuNgay DATE,
            @DenNgay DATE,
            @NgayKetThucCu DATE,
            @NgayKetThucMoi DATE,
            @SoNgayDuKien INT,
            @SoNgayBaoLuu INT,
            @TrangThaiMoi NVARCHAR(20);

    SELECT TOP (1)
        @MaBaoLuu = b.MaBaoLuu,
        @TuNgay = b.TuNgay,
        @DenNgay = b.DenNgay
    FROM dbo.BaoLuuGoiTap b WITH (UPDLOCK, HOLDLOCK)
    WHERE b.MaDKG = @MaDKG AND b.TrangThai = N'Đang bảo lưu'
    ORDER BY b.MaBaoLuu DESC;

    IF @MaBaoLuu IS NULL
        THROW 50044, N'Không có lần bảo lưu đang hoạt động.', 1;

    SELECT @NgayKetThucCu = NgayKetThuc
    FROM dbo.DangKyGoi WITH (UPDLOCK, HOLDLOCK)
    WHERE MaDKG = @MaDKG AND TrangThai = N'Tạm dừng';

    IF @NgayKetThucCu IS NULL
        THROW 50046, N'Gói không ở trạng thái tạm dừng.', 1;

    SET @SoNgayDuKien = DATEDIFF(DAY, @TuNgay, @DenNgay) + 1;
    SET @SoNgayBaoLuu = CASE
        WHEN @HomNay < @TuNgay THEN 0
        WHEN @HomNay < @DenNgay THEN DATEDIFF(DAY, @TuNgay, @HomNay) + 1
        ELSE @SoNgayDuKien
    END;
    SET @NgayKetThucMoi = DATEADD(DAY, @SoNgayBaoLuu, @NgayKetThucCu);
    SET @TrangThaiMoi = CASE WHEN @NgayKetThucMoi >= @HomNay THEN N'Còn hạn' ELSE N'Hết hạn' END;

    UPDATE dbo.BaoLuuGoiTap
    SET TrangThai = N'Đã kết thúc'
    WHERE MaBaoLuu = @MaBaoLuu;

    UPDATE dbo.DangKyGoi
    SET TrangThai = @TrangThaiMoi,
        NgayKetThuc = @NgayKetThucMoi,
        TongSoNgayBaoLuu = CASE
            WHEN TongSoNgayBaoLuu - @SoNgayDuKien + @SoNgayBaoLuu < 0 THEN 0
            ELSE TongSoNgayBaoLuu - @SoNgayDuKien + @SoNgayBaoLuu
        END,
        NgayCapNhat = SYSDATETIME()
    WHERE MaDKG = @MaDKG;

    INSERT dbo.LichSuGoiTap(MaDKG, HanhDong, TuNgay, DenNgay, GhiChu, MaNV)
    VALUES(@MaDKG, N'Mở lại bảo lưu', @TuNgay, @DenNgay,
           CONCAT(N'Mở lại bảo lưu; số ngày tạm dừng thực tế: ', @SoNgayBaoLuu, N'.'), @MaNV);

    COMMIT;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK;
    THROW;
END CATCH;
GO

GO

/* ========================================================================================
   PHẦN: 26_bo_sung_chuyen_phong_checkin.sql
   ======================================================================================== */
GO
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

GO

/* ========================================================================================
   PHẦN: 27_phan_quyen_dich_vu_theo_goi.sql
   ======================================================================================== */
GO
-- Migration 27: Phân quyền dịch vụ/phòng theo từng gói tập.
-- Mỗi lần Check-in phải dùng đúng gói người dùng chọn và phòng được cấp quyền.
USE QL_PhongGym;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
SET ANSI_WARNINGS ON;
GO
SET ARITHABORT ON;
GO

IF OBJECT_ID(N'dbo.GoiTapDichVu', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.GoiTapDichVu
    (
        MaGoi INT NOT NULL,
        MaDichVu VARCHAR(30) NOT NULL,
        TenDichVu NVARCHAR(100) NOT NULL,
        SoLuotToiDa INT NULL,
        TrangThai BIT NOT NULL CONSTRAINT DF_GoiTapDichVu_TrangThai DEFAULT (1),
        GhiChu NVARCHAR(255) NULL,
        CONSTRAINT PK_GoiTapDichVu PRIMARY KEY (MaGoi, MaDichVu),
        CONSTRAINT FK_GoiTapDichVu_GoiTap FOREIGN KEY (MaGoi) REFERENCES dbo.GoiTap(MaGoi),
        CONSTRAINT CK_GoiTapDichVu_SoLuot CHECK (SoLuotToiDa IS NULL OR SoLuotToiDa > 0)
    );
END;
GO

IF OBJECT_ID(N'dbo.GoiTapPhong', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.GoiTapPhong
    (
        MaGoi INT NOT NULL,
        MaPhong INT NOT NULL,
        TrangThai BIT NOT NULL CONSTRAINT DF_GoiTapPhong_TrangThai DEFAULT (1),
        GhiChu NVARCHAR(255) NULL,
        CONSTRAINT PK_GoiTapPhong PRIMARY KEY (MaGoi, MaPhong),
        CONSTRAINT FK_GoiTapPhong_GoiTap FOREIGN KEY (MaGoi) REFERENCES dbo.GoiTap(MaGoi),
        CONSTRAINT FK_GoiTapPhong_PhongTap FOREIGN KEY (MaPhong) REFERENCES dbo.PhongTap(MaPhong)
    );
END;
GO

-- Mọi gói đang áp dụng đều có quyền sử dụng phòng; phòng cụ thể được giới hạn bên dưới.
INSERT dbo.GoiTapDichVu(MaGoi, MaDichVu, TenDichVu)
SELECT g.MaGoi, N'PHONG_TAP', N'Phòng tập theo quyền gói'
FROM dbo.GoiTap g
WHERE NOT EXISTS
(
    SELECT 1 FROM dbo.GoiTapDichVu q
    WHERE q.MaGoi = g.MaGoi AND q.MaDichVu = N'PHONG_TAP'
);

-- Premium có thêm lớp nhóm.
INSERT dbo.GoiTapDichVu(MaGoi, MaDichVu, TenDichVu)
SELECT g.MaGoi, N'LOP_NHOM', N'Lớp nhóm'
FROM dbo.GoiTap g
WHERE g.TenGoi LIKE N'Premium%'
  AND NOT EXISTS (SELECT 1 FROM dbo.GoiTapDichVu q WHERE q.MaGoi=g.MaGoi AND q.MaDichVu=N'LOP_NHOM');

-- VIP được dùng lớp nhóm, khu vực VIP và PT ưu tiên.
INSERT dbo.GoiTapDichVu(MaGoi, MaDichVu, TenDichVu)
SELECT g.MaGoi, v.MaDichVu, v.TenDichVu
FROM dbo.GoiTap g
CROSS APPLY (VALUES
    ('LOP_NHOM', N'Lớp nhóm'),
    ('KHU_VIP', N'Khu vực VIP'),
    ('PT_UU_TIEN', N'Ưu tiên đặt lịch PT')
) v(MaDichVu, TenDichVu)
WHERE g.TenGoi LIKE N'VIP%'
  AND NOT EXISTS (SELECT 1 FROM dbo.GoiTapDichVu q WHERE q.MaGoi=g.MaGoi AND q.MaDichVu=v.MaDichVu);

-- Gói chuyên đề và gói PT có quyền đúng với nội dung gói.
INSERT dbo.GoiTapDichVu(MaGoi, MaDichVu, TenDichVu)
SELECT g.MaGoi, v.MaDichVu, v.TenDichVu
FROM dbo.GoiTap g
CROSS APPLY (VALUES
    (N'Yoga%', 'LOP_YOGA', N'Lớp Yoga'),
    (N'Cardio%', 'KHU_CARDIO', N'Khu Cardio'),
    (N'PT Combo%', 'PT', N'Huấn luyện cá nhân (PT)')
) v(TenGoiPattern, MaDichVu, TenDichVu)
WHERE g.TenGoi LIKE v.TenGoiPattern
  AND NOT EXISTS (SELECT 1 FROM dbo.GoiTapDichVu q WHERE q.MaGoi=g.MaGoi AND q.MaDichVu=v.MaDichVu);
GO

-- Gán phòng cho dữ liệu demo hiện có.
-- Basic/Sinh viên/Family: phòng gym tổng hợp.
INSERT dbo.GoiTapPhong(MaGoi, MaPhong)
SELECT g.MaGoi, p.MaPhong
FROM dbo.GoiTap g
JOIN dbo.PhongTap p ON p.MaPhong = 1
WHERE (g.TenGoi LIKE N'Basic%' OR g.TenGoi LIKE N'Sinh viên%' OR g.TenGoi LIKE N'Family%')
  AND NOT EXISTS (SELECT 1 FROM dbo.GoiTapPhong q WHERE q.MaGoi=g.MaGoi AND q.MaPhong=p.MaPhong);

-- Premium: gym, cardio và phòng đa năng cho lớp nhóm.
INSERT dbo.GoiTapPhong(MaGoi, MaPhong)
SELECT g.MaGoi, p.MaPhong
FROM dbo.GoiTap g
JOIN dbo.PhongTap p ON p.LoaiPhong IN (N'Phòng gym', N'Phòng cardio', N'Phòng đa năng')
WHERE g.TenGoi LIKE N'Premium%'
  AND NOT EXISTS (SELECT 1 FROM dbo.GoiTapPhong q WHERE q.MaGoi=g.MaGoi AND q.MaPhong=p.MaPhong);

-- VIP: tất cả phòng đang hoạt động.
INSERT dbo.GoiTapPhong(MaGoi, MaPhong)
SELECT g.MaGoi, p.MaPhong
FROM dbo.GoiTap g
CROSS JOIN dbo.PhongTap p
WHERE g.TenGoi LIKE N'VIP%' AND p.TrangThai=N'Hoạt động'
  AND NOT EXISTS (SELECT 1 FROM dbo.GoiTapPhong q WHERE q.MaGoi=g.MaGoi AND q.MaPhong=p.MaPhong);

-- Gói Yoga/Cardio/PT Combo.
INSERT dbo.GoiTapPhong(MaGoi, MaPhong)
SELECT g.MaGoi, p.MaPhong
FROM dbo.GoiTap g
JOIN dbo.PhongTap p ON
       (g.TenGoi LIKE N'Yoga%' AND p.LoaiPhong=N'Phòng yoga')
    OR (g.TenGoi LIKE N'Cardio%' AND p.LoaiPhong=N'Phòng cardio')
    OR (g.TenGoi LIKE N'PT Combo%' AND p.LoaiPhong IN (N'Phòng gym',N'Phòng cardio'))
WHERE NOT EXISTS (SELECT 1 FROM dbo.GoiTapPhong q WHERE q.MaGoi=g.MaGoi AND q.MaPhong=p.MaPhong);
GO

CREATE OR ALTER PROCEDURE dbo.sp_CheckIn
    @MaTV INT,
    @MaPhong INT=NULL,
    @Kenh NVARCHAR(20)=N'Quầy',
    @MaCheckIn BIGINT OUTPUT,
    @MaNV INT=NULL,
    @MaDKG INT=NULL
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;
        SET XACT_ABORT ON;
        BEGIN TRAN;

        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|Lễ tân|Nhân viên')=0
            THROW 52106,N'Tài khoản thực hiện check-in không có quyền hoặc không còn hoạt động.',1;

        DECLARE @HienTai DATETIME2(0)=SYSDATETIME(),
                @GioHienTai TIME(0)=CAST(SYSDATETIME() AS TIME(0)),
                @MaDKGDuocChon INT;

        IF @MaPhong IS NOT NULL AND NOT EXISTS
        (
            SELECT 1 FROM dbo.PhongTap
            WHERE MaPhong=@MaPhong AND TrangThai=N'Hoạt động'
        )
            THROW 50038,N'Phòng không tồn tại hoặc đang bảo trì/đóng.',1;

        IF EXISTS(SELECT 1 FROM dbo.LichSuCheckIn WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND ThoiGianRa IS NULL)
            THROW 50010,N'Thành viên đang có lượt check-in mở.',1;

        -- Có MaDKG: bắt buộc dùng đúng gói nhân viên đã chọn trên màn hình.
        -- Không có MaDKG: giữ tương thích cho các luồng cũ và chọn FIFO.
        SELECT TOP(1) @MaDKGDuocChon=d.MaDKG
        FROM dbo.DangKyGoi d WITH(UPDLOCK,HOLDLOCK)
        JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi
        JOIN dbo.ThanhVien t ON t.MaTV=d.MaTV
        WHERE d.MaTV=@MaTV
          AND (@MaDKG IS NULL OR d.MaDKG=@MaDKG)
          AND d.TrangThai=N'Còn hạn'
          AND d.NgayBatDau<=CAST(@HienTai AS DATE)
          AND d.NgayKetThuc>=CAST(@HienTai AS DATE)
          AND t.TrangThai=N'Hoạt động' AND t.DaXoa=0
          AND (g.GioiHanLuot IS NULL OR ISNULL(d.SoLuotDaDung,0)<g.GioiHanLuot)
          AND (g.GioBatDau IS NULL OR g.GioKetThuc IS NULL OR
               (g.GioBatDau<=g.GioKetThuc AND @GioHienTai BETWEEN g.GioBatDau AND g.GioKetThuc) OR
               (g.GioBatDau>g.GioKetThuc AND (@GioHienTai>=g.GioBatDau OR @GioHienTai<=g.GioKetThuc)))
          AND EXISTS
          (
              SELECT 1 FROM dbo.HoaDonGoiTap x
              JOIN dbo.ChiTietHoaDon c ON c.MaCTHD=x.MaCTHD
              JOIN dbo.HoaDon h ON h.MaHD=c.MaHD
              WHERE x.MaDKG=d.MaDKG AND h.TrangThai=N'Đã thanh toán'
          )
          AND EXISTS
          (
              SELECT 1 FROM dbo.GoiTapDichVu q
              WHERE q.MaGoi=d.MaGoi AND q.MaDichVu='PHONG_TAP' AND q.TrangThai=1
          )
          AND (@MaPhong IS NULL OR EXISTS
          (
              SELECT 1 FROM dbo.GoiTapPhong qp
              WHERE qp.MaGoi=d.MaGoi AND qp.MaPhong=@MaPhong AND qp.TrangThai=1
          ))
        ORDER BY d.NgayKetThuc ASC, d.MaDKG ASC;

        IF @MaDKGDuocChon IS NULL
            THROW 50011,N'Gói đã chọn không hợp lệ, chưa thanh toán, hết hạn, hết lượt hoặc không được cấp quyền phòng này.',1;

        INSERT dbo.LichSuCheckIn(MaTV,MaDKG,MaPhong,Kenh)
        VALUES(@MaTV,@MaDKGDuocChon,@MaPhong,@Kenh);
        SET @MaCheckIn=CONVERT(BIGINT,SCOPE_IDENTITY());

        UPDATE dbo.DangKyGoi
        SET SoLuotDaDung=ISNULL(SoLuotDaDung,0)+1,NgayCapNhat=@HienTai
        WHERE MaDKG=@MaDKGDuocChon;

        INSERT dbo.NhatKyHeThong(MaNV,HanhDong,DoiTuong,KhoaDoiTuong,NoiDung)
        VALUES(@MaNV,N'Check-in',N'LichSuCheckIn',CONVERT(NVARCHAR(30),@MaCheckIn),
               CONCAT(@Kenh,N'; gói đăng ký #',@MaDKGDuocChon));
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK;
        THROW;
    END CATCH;
END;
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
        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|Lễ tân|Nhân viên')=0
            THROW 52110,N'Tài khoản thực hiện chuyển phòng không có quyền hoặc không còn hoạt động.',1;

        DECLARE @MaPhongCu INT,@MaDKG INT,@MaGoi INT,@SucChua INT,@SoNguoiDangTap INT;
        SELECT TOP(1) @MaPhongCu=c.MaPhong,@MaDKG=c.MaDKG,@MaGoi=d.MaGoi
        FROM dbo.LichSuCheckIn c WITH(UPDLOCK,HOLDLOCK)
        JOIN dbo.DangKyGoi d ON d.MaDKG=c.MaDKG
        WHERE c.MaTV=@MaTV AND c.ThoiGianRa IS NULL
        ORDER BY c.ThoiGianVao DESC;

        IF @MaDKG IS NULL THROW 50012,N'Không tìm thấy lượt check-in mở.',1;
        IF @MaPhongCu=@MaPhongMoi THROW 50047,N'Phòng mới phải khác phòng hiện tại.',1;
        IF NOT EXISTS(SELECT 1 FROM dbo.PhongTap WITH(UPDLOCK,HOLDLOCK) WHERE MaPhong=@MaPhongMoi AND TrangThai=N'Hoạt động')
            THROW 50038,N'Phòng không tồn tại hoặc đang bảo trì/đóng.',1;
        IF NOT EXISTS(SELECT 1 FROM dbo.GoiTapPhong WHERE MaGoi=@MaGoi AND MaPhong=@MaPhongMoi AND TrangThai=1)
            THROW 50049,N'Gói hiện tại không được phép sử dụng phòng mới.',1;

        SELECT @SucChua=SucChua FROM dbo.PhongTap WHERE MaPhong=@MaPhongMoi;
        SELECT @SoNguoiDangTap=COUNT(*) FROM dbo.LichSuCheckIn WITH(UPDLOCK,HOLDLOCK)
        WHERE MaPhong=@MaPhongMoi AND ThoiGianRa IS NULL;
        IF @SoNguoiDangTap>=@SucChua THROW 50048,N'Phòng mới đã đủ sức chứa.',1;

        UPDATE dbo.LichSuCheckIn SET MaPhong=@MaPhongMoi
        WHERE MaTV=@MaTV AND ThoiGianRa IS NULL;
        INSERT dbo.NhatKyHeThong(MaNV,HanhDong,DoiTuong,KhoaDoiTuong,NoiDung)
        VALUES(@MaNV,N'Chuyển phòng',N'LichSuCheckIn',CONVERT(NVARCHAR(30),@MaTV),
               CONCAT(N'Chuyển từ phòng #',COALESCE(CONVERT(NVARCHAR(20),@MaPhongCu),N'Chưa gán'),N' sang phòng #',@MaPhongMoi));
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO

GO

/* ========================================================================================
   PHẦN: 28_dong_bo_trang_thai_thiet_bi.sql
   ======================================================================================== */
GO
/*
   Migration 28 - đồng bộ dữ liệu thiết bị sau các phiếu bảo trì lịch sử.
   Chỉ khôi phục thiết bị về Hoạt động khi không còn phiếu bảo trì mở.
   Thiết bị còn phiếu Đang bảo trì/Chờ bảo trì sẽ không bị thay đổi.
*/
USE QL_PhongGym;
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
GO

BEGIN TRANSACTION;
BEGIN TRY
    UPDATE tb
    SET tb.TinhTrang = N'Hoạt động'
    FROM dbo.ThietBi tb
    WHERE tb.TinhTrang = N'Bảo trì'
      AND NOT EXISTS
      (
          SELECT 1
          FROM dbo.BaoTriThietBi bt
          WHERE bt.MaTB = tb.MaTB
            AND bt.TrangThai IN (N'Đang bảo trì', N'Chờ bảo trì')
      );

    PRINT N'Đã đồng bộ ' + CONVERT(NVARCHAR(20), @@ROWCOUNT) + N' thiết bị không còn phiếu bảo trì mở về Hoạt động.';
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

GO


/* ========================================================================================
   PHẦN: 29_rang_buoc_va_validation.sql
   ======================================================================================== */
GO
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

-- ========================================================================================
-- PHẦN 1: BỔ SUNG NOT NULL VỚI DEFAULT (Ưu tiên Cao)
-- ========================================================================================

-- 1.1 ThanhVien.GioiTinh: NOT NULL (default N'Khác' cho dữ liệu cũ)
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID('dbo.ThanhVien') AND name=N'GioiTinh' AND is_nullable=1)
BEGIN
    UPDATE dbo.ThanhVien SET GioiTinh=N'Khác' WHERE GioiTinh IS NULL;
    ALTER TABLE dbo.ThanhVien ALTER COLUMN GioiTinh NVARCHAR(10) NOT NULL;
END;
GO

-- 1.2 NhanVien.GioiTinh: NOT NULL
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID('dbo.NhanVien') AND name=N'GioiTinh' AND is_nullable=1)
BEGIN
    UPDATE dbo.NhanVien SET GioiTinh=N'Khác' WHERE GioiTinh IS NULL;
    ALTER TABLE dbo.NhanVien ALTER COLUMN GioiTinh NVARCHAR(10) NOT NULL;
END;
GO

-- 1.3 NhanVien.Luong: NOT NULL DEFAULT 0
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID('dbo.NhanVien') AND name=N'Luong' AND is_nullable=1)
BEGIN
    UPDATE dbo.NhanVien SET Luong=0 WHERE Luong IS NULL;
    ALTER TABLE dbo.NhanVien ALTER COLUMN Luong DECIMAL(18,2) NOT NULL;
END;
IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE parent_object_id=OBJECT_ID('dbo.NhanVien') AND name='DF_NhanVien_Luong')
    ALTER TABLE dbo.NhanVien ADD CONSTRAINT DF_NhanVien_Luong DEFAULT 0 FOR Luong;
GO

-- 1.4 HuanLuyenVien.KinhNghiem: NOT NULL DEFAULT 0
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID('dbo.HuanLuyenVien') AND name=N'KinhNghiem' AND is_nullable=1)
BEGIN
    UPDATE dbo.HuanLuyenVien SET KinhNghiem=0 WHERE KinhNghiem IS NULL;
    ALTER TABLE dbo.HuanLuyenVien ALTER COLUMN KinhNghiem INT NOT NULL;
END;
IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE parent_object_id=OBJECT_ID('dbo.HuanLuyenVien') AND name='DF_HuanLuyenVien_KinhNghiem')
    ALTER TABLE dbo.HuanLuyenVien ADD CONSTRAINT DF_HuanLuyenVien_KinhNghiem DEFAULT 0 FOR KinhNghiem;
GO

-- 1.5 HuanLuyenVien.PhiThue: NOT NULL DEFAULT 0
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID('dbo.HuanLuyenVien') AND name=N'PhiThue' AND is_nullable=1)
BEGIN
    UPDATE dbo.HuanLuyenVien SET PhiThue=0 WHERE PhiThue IS NULL;
    ALTER TABLE dbo.HuanLuyenVien ALTER COLUMN PhiThue DECIMAL(18,2) NOT NULL;
END;
IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE parent_object_id=OBJECT_ID('dbo.HuanLuyenVien') AND name='DF_HuanLuyenVien_PhiThue')
    ALTER TABLE dbo.HuanLuyenVien ADD CONSTRAINT DF_HuanLuyenVien_PhiThue DEFAULT 0 FOR PhiThue;
GO

-- ========================================================================================
-- PHẦN 2: BỔ SUNG CHECK CONSTRAINTS
-- ========================================================================================

IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_ThanhVien_HoTen_v4')
    ALTER TABLE dbo.ThanhVien ADD CONSTRAINT CK_ThanhVien_HoTen_v4
        CHECK (LEN(LTRIM(RTRIM(HoTen))) >= 2);
GO
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_NhanVien_HoTen_v4')
    ALTER TABLE dbo.NhanVien ADD CONSTRAINT CK_NhanVien_HoTen_v4
        CHECK (LEN(LTRIM(RTRIM(HoTen))) >= 2);
GO
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_BaoTriThietBi_TrangThai_v4')
    ALTER TABLE dbo.BaoTriThietBi ADD CONSTRAINT CK_BaoTriThietBi_TrangThai_v4
        CHECK (TrangThai IN (N'Đang bảo trì', N'Chờ bảo trì', N'Hoàn thành'));
GO
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_GoiTap_TenGoi_v4')
    ALTER TABLE dbo.GoiTap ADD CONSTRAINT CK_GoiTap_TenGoi_v4
        CHECK (LEN(LTRIM(RTRIM(TenGoi))) > 0);
GO
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_SanPham_TenSP_v4')
    ALTER TABLE dbo.SanPham ADD CONSTRAINT CK_SanPham_TenSP_v4
        CHECK (LEN(LTRIM(RTRIM(TenSP))) > 0);
GO
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_PhongTap_Tang_v4')
    ALTER TABLE dbo.PhongTap ADD CONSTRAINT CK_PhongTap_Tang_v4
        CHECK (Tang >= 1);
GO
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name=N'CK_TaiKhoan_TenDangNhap_v4')
    ALTER TABLE dbo.TaiKhoan ADD CONSTRAINT CK_TaiKhoan_TenDangNhap_v4
        CHECK (LEN(TenDangNhap) >= 3);
GO

-- GioiTinh CHECK (đảm bảo tồn tại sau ALTER COLUMN)
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

-- ========================================================================================
-- PHẦN 3: NÂNG CẤP STORED PROCEDURES VỚI VALIDATION & TRANSACTION
-- ========================================================================================

-- 3.1 sp_ThemThanhVien: Thêm validation rõ ràng + TRANSACTION
CREATE OR ALTER PROCEDURE dbo.sp_ThemThanhVien
    @HoTen NVARCHAR(100), @NgaySinh DATE, @GioiTinh NVARCHAR(10),
    @SDT VARCHAR(15), @Email VARCHAR(100)=NULL, @DiaChi NVARCHAR(255)=NULL,
    @MaLoaiTV INT, @MaTV INT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON;
        IF NULLIF(LTRIM(RTRIM(@HoTen)),N'') IS NULL OR LEN(LTRIM(RTRIM(@HoTen)))<2
            THROW 51101,N'Họ tên phải có ít nhất 2 ký tự.',1;
        IF @NgaySinh>=CAST(SYSDATETIME() AS DATE)
            THROW 51102,N'Ngày sinh phải nhỏ hơn ngày hiện tại.',1;
        IF @GioiTinh NOT IN(N'Nam',N'Nữ',N'Khác')
            THROW 51103,N'Giới tính phải là Nam, Nữ hoặc Khác.',1;
        IF @SDT IS NULL OR @SDT NOT LIKE '0%' OR @SDT LIKE '%[^0-9]%' OR LEN(@SDT) NOT BETWEEN 10 AND 11
            THROW 51104,N'Số điện thoại phải gồm 10-11 chữ số và bắt đầu bằng 0.',1;
        IF @Email IS NOT NULL AND(@Email LIKE '% %' OR @Email NOT LIKE '%_@_%._%')
            THROW 51105,N'Email không đúng định dạng (ví dụ: ten@domain.com).',1;
        IF NOT EXISTS(SELECT 1 FROM dbo.LoaiThanhVien WHERE MaLoaiTV=@MaLoaiTV)
            THROW 51106,N'Loại thành viên không tồn tại.',1;
        BEGIN TRAN;
        IF EXISTS(SELECT 1 FROM dbo.ThanhVien WITH(UPDLOCK,HOLDLOCK) WHERE SDT=@SDT AND DaXoa=0)
            THROW 51107,N'Số điện thoại này đã được đăng ký cho thành viên khác.',1;
        INSERT dbo.ThanhVien(HoTen,NgaySinh,GioiTinh,SDT,Email,DiaChi,MaLoaiTV)
        VALUES(LTRIM(RTRIM(@HoTen)),@NgaySinh,@GioiTinh,@SDT,@Email,@DiaChi,@MaLoaiTV);
        SET @MaTV=SCOPE_IDENTITY();
        COMMIT;
    END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
END;
GO

-- 3.2 sp_SuaThanhVien: Thêm validation + TRANSACTION
CREATE OR ALTER PROCEDURE dbo.sp_SuaThanhVien
    @MaTV INT, @HoTen NVARCHAR(100), @SDT VARCHAR(15),
    @Email VARCHAR(100)=NULL, @DiaChi NVARCHAR(255)=NULL, @MaLoaiTV INT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON;
        IF NULLIF(LTRIM(RTRIM(@HoTen)),N'') IS NULL OR LEN(LTRIM(RTRIM(@HoTen)))<2
            THROW 51101,N'Họ tên phải có ít nhất 2 ký tự.',1;
        IF @SDT IS NULL OR @SDT NOT LIKE '0%' OR @SDT LIKE '%[^0-9]%' OR LEN(@SDT) NOT BETWEEN 10 AND 11
            THROW 51104,N'Số điện thoại phải gồm 10-11 chữ số và bắt đầu bằng 0.',1;
        IF @Email IS NOT NULL AND(@Email LIKE '% %' OR @Email NOT LIKE '%_@_%._%')
            THROW 51105,N'Email không đúng định dạng (ví dụ: ten@domain.com).',1;
        IF NOT EXISTS(SELECT 1 FROM dbo.LoaiThanhVien WHERE MaLoaiTV=@MaLoaiTV)
            THROW 51106,N'Loại thành viên không tồn tại.',1;
        BEGIN TRAN;
        IF EXISTS(SELECT 1 FROM dbo.ThanhVien WITH(UPDLOCK,HOLDLOCK) WHERE SDT=@SDT AND MaTV<>@MaTV AND DaXoa=0)
            THROW 51107,N'Số điện thoại này đã được đăng ký cho thành viên khác.',1;
        UPDATE dbo.ThanhVien SET HoTen=LTRIM(RTRIM(@HoTen)),SDT=@SDT,Email=@Email,DiaChi=@DiaChi,MaLoaiTV=@MaLoaiTV,NgayCapNhat=SYSDATETIME()
        WHERE MaTV=@MaTV AND DaXoa=0;
        IF @@ROWCOUNT=0 THROW 50001,N'Thành viên không tồn tại hoặc đã xóa mềm.',1;
        COMMIT;
    END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
END;
GO

-- 3.3 sp_KhoaThanhVien: Bọc TRANSACTION
CREATE OR ALTER PROCEDURE dbo.sp_KhoaThanhVien @MaTV INT, @LyDo NVARCHAR(255), @MaNV INT=NULL AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON;
        IF NULLIF(LTRIM(RTRIM(@LyDo)),N'') IS NULL THROW 51108,N'Lý do khóa thành viên không được để trống.',1;
        BEGIN TRAN;
        UPDATE dbo.ThanhVien SET TrangThai=N'Tạm khóa',LyDoKhoa=LTRIM(RTRIM(@LyDo)),NgayCapNhat=SYSDATETIME() WHERE MaTV=@MaTV AND DaXoa=0;
        IF @@ROWCOUNT=0 THROW 50002,N'Không tìm thấy thành viên.',1;
        INSERT dbo.NhatKyHeThong(MaNV,HanhDong,DoiTuong,KhoaDoiTuong,NoiDung) VALUES(@MaNV,N'Khóa thành viên',N'ThanhVien',CONVERT(NVARCHAR(30),@MaTV),LTRIM(RTRIM(@LyDo)));
        COMMIT;
    END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
END;
GO

-- 3.4 sp_DangKyGoiTap: Kiểm tra trùng gói cùng loại
CREATE OR ALTER PROCEDURE dbo.sp_DangKyGoiTap
    @MaTV INT, @MaGoi INT, @MaKM INT=NULL, @NgayBatDau DATE=NULL,
    @MaNV INT, @MaDKG INT OUTPUT, @MaHD INT OUTPUT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        IF NOT EXISTS(SELECT 1 FROM dbo.ThanhVien WHERE MaTV=@MaTV AND TrangThai=N'Hoạt động' AND DaXoa=0)
            THROW 50003,N'Thành viên không hoạt động.',1;
        DECLARE @Gia DECIMAL(18,2),@Han INT,@Giam DECIMAL(5,2);
        SELECT @Gia=Gia,@Han=ThoiHanNgay FROM dbo.GoiTap WHERE MaGoi=@MaGoi AND TrangThai=N'Đang áp dụng';
        IF @Gia IS NULL THROW 50004,N'Gói tập không hợp lệ hoặc đã ngừng áp dụng.',1;
        IF EXISTS(SELECT 1 FROM dbo.DangKyGoi WITH(UPDLOCK,HOLDLOCK) WHERE MaTV=@MaTV AND MaGoi=@MaGoi AND TrangThai IN(N'Còn hạn',N'Chờ thanh toán'))
            THROW 51109,N'Thành viên đã có gói tập cùng loại đang hoạt động. Vui lòng gia hạn thay vì đăng ký mới.',1;
        IF @MaKM IS NOT NULL SELECT @Giam=PhanTramGiam FROM dbo.KhuyenMai WHERE MaKM=@MaKM AND TrangThai=N'Hoạt động' AND CAST(SYSDATETIME() AS DATE) BETWEEN NgayBatDau AND NgayKetThuc;
        IF @MaKM IS NOT NULL AND @Giam IS NULL THROW 50005,N'Khuyến mãi không còn hiệu lực.',1;
        SET @Giam=ISNULL(@Giam,0); SET @NgayBatDau=ISNULL(@NgayBatDau,CAST(SYSDATETIME() AS DATE));
        INSERT dbo.DangKyGoi(MaTV,MaGoi,MaKM,NgayBatDau,NgayKetThuc,GiaSauGiam,TrangThai) VALUES(@MaTV,@MaGoi,@MaKM,@NgayBatDau,DATEADD(DAY,@Han-1,@NgayBatDau),ROUND(@Gia*(1-@Giam/100),2),N'Chờ thanh toán');
        SET @MaDKG=SCOPE_IDENTITY();
        INSERT dbo.HoaDon(MaTV,MaNV,TongTien,TrangThai) VALUES(@MaTV,@MaNV,0,N'Chưa thanh toán'); SET @MaHD=SCOPE_IDENTITY();
        INSERT dbo.ChiTietHoaDon(MaHD,LoaiDichVu,MaThamChieu,SoLuong,DonGia) SELECT @MaHD,N'Gói tập',@MaDKG,1,GiaSauGiam FROM dbo.DangKyGoi WHERE MaDKG=@MaDKG;
        INSERT dbo.HoaDonGoiTap(MaCTHD,MaDKG) SELECT MaCTHD,@MaDKG FROM dbo.ChiTietHoaDon WHERE MaHD=@MaHD;
        INSERT dbo.LichSuGoiTap(MaDKG,HanhDong,TuNgay,DenNgay,MaNV) SELECT @MaDKG,N'Đăng ký',NgayBatDau,NgayKetThuc,@MaNV FROM dbo.DangKyGoi WHERE MaDKG=@MaDKG;
        COMMIT;
    END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
END;
GO

-- 3.5 sp_GiaHanGoiTap: Giới hạn @SoNgay tối đa 365 ngày
CREATE OR ALTER PROCEDURE dbo.sp_GiaHanGoiTap @MaDKG INT, @SoNgay INT, @MaNV INT=NULL, @MaHD INT=NULL OUTPUT AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|Lễ tân|Nhân viên')=0
            THROW 52109,N'Tài khoản thực hiện gia hạn không có quyền hoặc không còn hoạt động.',1;
        IF @SoNgay<=0 THROW 50006,N'Số ngày gia hạn phải lớn hơn 0.',1;
        IF @SoNgay>365 THROW 51110,N'Số ngày gia hạn không được vượt quá 365 ngày.',1;
        DECLARE @MaTV INT,@MaGoi INT,@Cu DATE,@GiaGoi DECIMAL(18,2),@ThoiHanGoi INT;
        SELECT @MaTV=d.MaTV,@MaGoi=d.MaGoi,@Cu=d.NgayKetThuc,@GiaGoi=g.Gia,@ThoiHanGoi=g.ThoiHanNgay
        FROM dbo.DangKyGoi d WITH(UPDLOCK,HOLDLOCK) JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi WHERE d.MaDKG=@MaDKG AND d.TrangThai IN(N'Còn hạn',N'Hết hạn');
        IF @Cu IS NULL THROW 50007,N'Gói tập không tồn tại hoặc không ở trạng thái có thể gia hạn.',1;
        DECLARE @DonGiaNgay DECIMAL(18,2)=ROUND(@GiaGoi/CASE WHEN ISNULL(@ThoiHanGoi,0)>0 THEN @ThoiHanGoi ELSE 30 END,0);
        DECLARE @PhiGiaHan DECIMAL(18,2)=@DonGiaNgay*@SoNgay;
        INSERT dbo.HoaDon(MaTV,MaNV,TongTien,TrangThai) VALUES(@MaTV,@MaNV,0,N'Chưa thanh toán'); SET @MaHD=SCOPE_IDENTITY();
        INSERT dbo.ChiTietHoaDon(MaHD,LoaiDichVu,MaThamChieu,SoLuong,DonGia) VALUES(@MaHD,N'Gói tập',@MaDKG,@SoNgay,@PhiGiaHan);
        INSERT dbo.HoaDonGoiTap(MaCTHD,MaDKG) SELECT MaCTHD,@MaDKG FROM dbo.ChiTietHoaDon WHERE MaHD=@MaHD;
        DECLARE @Moc DATE=CASE WHEN @Cu<CAST(SYSDATETIME() AS DATE) THEN CAST(SYSDATETIME() AS DATE) ELSE @Cu END;
        DECLARE @Moi DATE=DATEADD(DAY,@SoNgay,@Moc);
        UPDATE dbo.DangKyGoi SET NgayKetThuc=@Moi,TrangThai=N'Còn hạn',NgayCapNhat=SYSDATETIME() WHERE MaDKG=@MaDKG;
        INSERT dbo.LichSuGoiTap(MaDKG,HanhDong,TuNgay,DenNgay,MaNV) VALUES(@MaDKG,N'Gia hạn',@Cu,@Moi,@MaNV);
        COMMIT;
    END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
END;
GO

-- 3.6 sp_BanSanPham: Kiểm tra @MaNV hoạt động
CREATE OR ALTER PROCEDURE dbo.sp_BanSanPham @MaTV INT,@MaNV INT,@MaSP INT,@SoLuong INT,@MaHD INT OUTPUT AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        DECLARE @Gia DECIMAL(18,2),@Ton INT,@MaCT INT;
        SELECT @Gia=DonGia,@Ton=SoLuongTon FROM dbo.SanPham WITH(UPDLOCK,HOLDLOCK) WHERE MaSP=@MaSP AND TrangThai=N'Còn bán';
        IF @Gia IS NULL OR @Ton<@SoLuong OR @SoLuong<=0 THROW 50026,N'Sản phẩm không hợp lệ hoặc không đủ tồn kho.',1;
        IF NOT EXISTS(SELECT 1 FROM dbo.ThanhVien WHERE MaTV=@MaTV AND DaXoa=0) THROW 50036,N'Thành viên không tồn tại.',1;
        IF NOT EXISTS(SELECT 1 FROM dbo.NhanVien WHERE MaNV=@MaNV AND TrangThai=N'Đang làm') THROW 51111,N'Nhân viên không hoạt động hoặc không tồn tại.',1;
        INSERT dbo.HoaDon(MaTV,MaNV,TongTien,TrangThai) VALUES(@MaTV,@MaNV,0,N'Chưa thanh toán'); SET @MaHD=SCOPE_IDENTITY();
        INSERT dbo.ChiTietHoaDon(MaHD,LoaiDichVu,MaThamChieu,SoLuong,DonGia) VALUES(@MaHD,N'Sản phẩm',@MaSP,@SoLuong,@Gia);
        SET @MaCT=SCOPE_IDENTITY(); INSERT dbo.HoaDonSanPham(MaCTHD,MaSP) VALUES(@MaCT,@MaSP);
        UPDATE dbo.SanPham SET SoLuongTon=SoLuongTon-@SoLuong,NgayCapNhat=SYSDATETIME() WHERE MaSP=@MaSP;
        INSERT dbo.LichSuTonKho(MaSP,LoaiBienDong,SoLuong,TonTruoc,TonSau,NguonThamChieu,MaThamChieu,MaNV) VALUES(@MaSP,N'Xuất',@SoLuong,@Ton,@Ton-@SoLuong,N'Hóa đơn',@MaHD,@MaNV);
        COMMIT;
    END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
END;
GO

-- 3.7 sp_HuyDangKyLop: Kiểm tra đã có điểm danh
CREATE OR ALTER PROCEDURE dbo.sp_HuyDangKyLop @MaDKL INT AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        IF EXISTS(SELECT 1 FROM dbo.DiemDanhLop WHERE MaDKL=@MaDKL AND TrangThai=N'Có mặt')
            THROW 51112,N'Không thể hủy đăng ký lớp vì học viên đã có buổi điểm danh. Vui lòng liên hệ quản lý.',1;
        UPDATE dbo.DangKyLop SET TrangThai=N'Đã hủy' WHERE MaDKL=@MaDKL AND TrangThai=N'Đang tham gia';
        IF @@ROWCOUNT=0 THROW 50015,N'Không thể hủy đăng ký lớp.',1;
        COMMIT;
    END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
END;
GO

-- 3.8 sp_HoanTatBaoTri: Chỉ cho phép từ trạng thái 'Đang bảo trì'
CREATE OR ALTER PROCEDURE dbo.sp_HoanTatBaoTri @MaBaoTri INT, @MaNV INT, @GhiChu NVARCHAR(255)=NULL AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        IF @MaNV IS NULL OR dbo.fn_NhanVienCoVaiTro(@MaNV,N'Admin|Quản lý|Nhân viên')=0
            THROW 52111,N'Tài khoản thực hiện hoàn tất bảo trì không có quyền hoặc không còn hoạt động.',1;
        DECLARE @MaTB INT,@TrangThaiHienTai NVARCHAR(50);
        SELECT @MaTB=MaTB,@TrangThaiHienTai=TrangThai FROM dbo.BaoTriThietBi WITH(UPDLOCK,HOLDLOCK) WHERE MaBaoTri=@MaBaoTri;
        IF @MaTB IS NULL THROW 50040,N'Phiếu bảo trì không tồn tại.',1;
        IF @TrangThaiHienTai=N'Hoàn thành' THROW 50041,N'Phiếu bảo trì này đã hoàn thành trước đó.',1;
        IF @TrangThaiHienTai<>N'Đang bảo trì'
            THROW 51113,N'Chỉ có thể hoàn tất phiếu bảo trì đang ở trạng thái "Đang bảo trì".',1;
        UPDATE dbo.BaoTriThietBi SET TrangThai=N'Hoàn thành',NgayHoanThanh=CAST(SYSDATETIME() AS DATE),GhiChu=ISNULL(@GhiChu,GhiChu) WHERE MaBaoTri=@MaBaoTri;
        IF NOT EXISTS(SELECT 1 FROM dbo.BaoTriThietBi WHERE MaTB=@MaTB AND MaBaoTri<>@MaBaoTri AND TrangThai IN(N'Đang bảo trì',N'Chờ bảo trì'))
            UPDATE dbo.ThietBi SET TinhTrang=N'Hoạt động' WHERE MaTB=@MaTB;
        COMMIT;
    END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
END;
GO

-- 3.9 sp_NhapKho: Validate @MaNCC tồn tại
CREATE OR ALTER PROCEDURE dbo.sp_NhapKho @MaNCC INT,@MaNV INT,@MaSP INT,@SoLuong INT,@DonGiaNhap DECIMAL(18,2),@MaPNK INT OUTPUT AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON; SET XACT_ABORT ON; BEGIN TRAN;
        IF @SoLuong<=0 OR @DonGiaNhap<0 THROW 50023,N'Dữ liệu nhập kho không hợp lệ.',1;
        IF NOT EXISTS(SELECT 1 FROM dbo.NhaCungCap WHERE MaNCC=@MaNCC) THROW 51114,N'Nhà cung cấp không tồn tại.',1;
        DECLARE @Ton INT; SELECT @Ton=SoLuongTon FROM dbo.SanPham WITH(UPDLOCK,HOLDLOCK) WHERE MaSP=@MaSP;
        IF @Ton IS NULL THROW 50024,N'Sản phẩm không tồn tại.',1;
        INSERT dbo.PhieuNhapKho(MaNCC,MaNV,TongTien) VALUES(@MaNCC,@MaNV,0); SET @MaPNK=SCOPE_IDENTITY();
        INSERT dbo.ChiTietPhieuNhap(MaPNK,MaSP,SoLuong,DonGiaNhap) VALUES(@MaPNK,@MaSP,@SoLuong,@DonGiaNhap);
        UPDATE dbo.PhieuNhapKho SET TongTien=(SELECT SUM(ThanhTien) FROM dbo.ChiTietPhieuNhap WHERE MaPNK=@MaPNK) WHERE MaPNK=@MaPNK;
        UPDATE dbo.SanPham SET SoLuongTon=SoLuongTon+@SoLuong,GiaNhap=@DonGiaNhap,MaNCC=@MaNCC,NgayCapNhat=SYSDATETIME() WHERE MaSP=@MaSP;
        INSERT dbo.LichSuTonKho(MaSP,LoaiBienDong,SoLuong,TonTruoc,TonSau,NguonThamChieu,MaThamChieu,MaNV) VALUES(@MaSP,N'Nhập',@SoLuong,@Ton,@Ton+@SoLuong,N'Phiếu nhập',@MaPNK,@MaNV);
        COMMIT;
    END TRY BEGIN CATCH IF XACT_STATE()<>0 ROLLBACK; THROW; END CATCH;
END;
GO


-- ============================================================================
-- PHẦN BỔ SUNG: MIGRATION 30 - BỔ SUNG QUYỀN MẶC ĐỊNH CHO GÓI TẬP CŨ
-- ============================================================================
IF OBJECT_ID(N'dbo.GoiTapDichVu', N'U') IS NOT NULL AND OBJECT_ID(N'dbo.GoiTapPhong', N'U') IS NOT NULL
BEGIN
    INSERT dbo.GoiTapDichVu(MaGoi,MaDichVu,TenDichVu)
    SELECT g.MaGoi,'PHONG_TAP',N'Phòng tập theo quyền gói'
    FROM dbo.GoiTap g
    WHERE NOT EXISTS
    (
        SELECT 1
        FROM dbo.GoiTapDichVu q
        WHERE q.MaGoi=g.MaGoi AND q.MaDichVu='PHONG_TAP'
    );

    INSERT dbo.GoiTapPhong(MaGoi,MaPhong)
    SELECT g.MaGoi,p.MaPhong
    FROM dbo.GoiTap g
    CROSS JOIN dbo.PhongTap p
    WHERE p.TrangThai=N'Hoạt động'
      AND NOT EXISTS
    (
        SELECT 1
        FROM dbo.GoiTapPhong q
        WHERE q.MaGoi=g.MaGoi AND q.MaPhong=p.MaPhong
    );
END;
GO

-- ============================================================================
-- PHẦN BỔ SUNG: MIGRATION 31 - MỞ RỘNG DANH MỤC PHÒNG VÀ THIẾT BỊ THỰC TẾ
-- ============================================================================
DECLARE @ConstraintName SYSNAME;
SELECT TOP (1) @ConstraintName = cc.name
FROM sys.check_constraints cc
WHERE cc.parent_object_id = OBJECT_ID(N'dbo.PhongTap')
  AND cc.definition LIKE N'%LoaiPhong%';

IF @ConstraintName IS NOT NULL
    EXEC(N'ALTER TABLE dbo.PhongTap DROP CONSTRAINT [' + @ConstraintName + N']');

IF NOT EXISTS
(
    SELECT 1 FROM sys.check_constraints
    WHERE parent_object_id=OBJECT_ID(N'dbo.PhongTap')
      AND name=N'CK_PhongTap_LoaiPhong_v5'
)
BEGIN
    ALTER TABLE dbo.PhongTap ADD CONSTRAINT CK_PhongTap_LoaiPhong_v5 CHECK
    (
        LoaiPhong IN
        (
            N'Phòng gym',N'Phòng yoga',N'Phòng cardio',N'Phòng boxing',N'Phòng đa năng',
            N'Phòng functional',N'Phòng spinning',N'Phòng pilates',N'Phòng dance',N'Phòng phục hồi'
        )
    );
END;
GO

INSERT dbo.PhongTap(TenPhong,LoaiPhong,SucChua,DienTich,Tang,TrangThai)
SELECT x.TenPhong,x.LoaiPhong,x.SucChua,x.DienTich,x.Tang,N'Hoạt động'
FROM (VALUES
    (N'Phòng Functional',N'Phòng functional',25,130.00,1),
    (N'Phòng Spinning',N'Phòng spinning',24,90.00,2),
    (N'Phòng Pilates',N'Phòng pilates',20,85.00,2),
    (N'Phòng Dance & Aerobic',N'Phòng dance',35,110.00,3),
    (N'Phòng Phục Hồi & Giãn Cơ',N'Phòng phục hồi',16,70.00,1)
) x(TenPhong,LoaiPhong,SucChua,DienTich,Tang)
WHERE NOT EXISTS (SELECT 1 FROM dbo.PhongTap p WHERE p.TenPhong=x.TenPhong);
GO

INSERT dbo.LoaiThietBi(TenLoaiTB,MoTa)
SELECT x.TenLoaiTB,x.MoTa
FROM (VALUES
    (N'Máy elliptical',N'Tập cardio toàn thân'),
    (N'Xe đạp spinning',N'Tập đạp xe trong nhà'),
    (N'Máy stair climber',N'Tập leo cầu thang'),
    (N'Giàn tạ đa năng',N'Tập nhiều nhóm cơ'),
    (N'Máy Smith',N'Tập sức mạnh có dẫn hướng'),
    (N'Kettlebell',N'Tập chức năng và sức mạnh'),
    (N'Dây battle rope',N'Tập sức bền toàn thân'),
    (N'Dây kháng lực',N'Tập phục hồi và chức năng'),
    (N'Con lăn foam roller',N'Giãn cơ và phục hồi'),
    (N'Máy Pilates Reformer',N'Tập Pilates chuyên sâu'),
    (N'Bao cát boxing',N'Tập boxing'),
    (N'Gương tập',N'Hỗ trợ quan sát kỹ thuật')
) x(TenLoaiTB,MoTa)
WHERE NOT EXISTS (SELECT 1 FROM dbo.LoaiThietBi l WHERE l.TenLoaiTB=x.TenLoaiTB);
GO

INSERT dbo.ThietBi(MaThietBi,TenTB,MaLoaiTB,NgayMua,TinhTrang,MaPhong)
SELECT x.MaThietBi,x.TenTB,l.MaLoaiTB,x.NgayMua,N'Hoạt động',p.MaPhong
FROM (VALUES
    ('TB000016',N'Máy elliptical E1',N'Máy elliptical','2025-01-12',N'Phòng Cardio'),
    ('TB000017',N'Xe đạp spinning SP1',N'Xe đạp spinning','2025-01-20',N'Phòng Spinning'),
    ('TB000018',N'Máy stair climber ST1',N'Máy stair climber','2025-02-05',N'Phòng Cardio'),
    ('TB000019',N'Giàn tạ đa năng GX1',N'Giàn tạ đa năng','2025-02-18',N'Phòng Functional'),
    ('TB000020',N'Máy Smith SM1',N'Máy Smith','2025-03-01',N'Phòng Gym Nâng Cao'),
    ('TB000021',N'Bộ Kettlebell KB1',N'Kettlebell','2025-03-10',N'Phòng Functional'),
    ('TB000022',N'Dây battle rope BR1',N'Dây battle rope','2025-03-15',N'Phòng Functional'),
    ('TB000023',N'Bộ dây kháng lực RB1',N'Dây kháng lực','2025-03-20',N'Phòng Phục Hồi & Giãn Cơ'),
    ('TB000024',N'Bộ foam roller FR1',N'Con lăn foam roller','2025-03-25',N'Phòng Phục Hồi & Giãn Cơ'),
    ('TB000025',N'Pilates Reformer PL1',N'Máy Pilates Reformer','2025-04-01',N'Phòng Pilates'),
    ('TB000026',N'Bao cát boxing BX1',N'Bao cát boxing','2025-04-10',N'Phòng Boxing'),
    ('TB000027',N'Gương tập D1',N'Gương tập','2025-04-15',N'Phòng Dance & Aerobic')
) x(MaThietBi,TenTB,TenLoaiTB,NgayMua,TenPhong)
JOIN dbo.LoaiThietBi l ON l.TenLoaiTB=x.TenLoaiTB
JOIN dbo.PhongTap p ON p.TenPhong=x.TenPhong
WHERE NOT EXISTS (SELECT 1 FROM dbo.ThietBi t WHERE t.MaThietBi=x.MaThietBi);
GO


-- ============================================================================
-- PHẦN BỔ SUNG: MIGRATION 32 - UNIQUE FILTERED INDEX CHO EMAIL NULLABLE
-- ============================================================================
DECLARE @ConstraintName SYSNAME;
SELECT TOP (1) @ConstraintName = kc.name
FROM sys.key_constraints kc
JOIN sys.index_columns ic ON kc.parent_object_id = ic.object_id AND kc.unique_index_id = ic.index_id
JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
WHERE kc.parent_object_id = OBJECT_ID(N'dbo.ThanhVien') AND c.name = N'Email';

IF @ConstraintName IS NOT NULL
    EXEC(N'ALTER TABLE dbo.ThanhVien DROP CONSTRAINT [' + @ConstraintName + N']');

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_ThanhVien_Email' AND object_id = OBJECT_ID(N'dbo.ThanhVien'))
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UX_ThanhVien_Email
    ON dbo.ThanhVien(Email)
    WHERE Email IS NOT NULL;
END;
GO

DECLARE @ConstraintNV SYSNAME;
SELECT TOP (1) @ConstraintNV = kc.name
FROM sys.key_constraints kc
JOIN sys.index_columns ic ON kc.parent_object_id = ic.object_id AND kc.unique_index_id = ic.index_id
JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
WHERE kc.parent_object_id = OBJECT_ID(N'dbo.NhanVien') AND c.name = N'Email';

IF @ConstraintNV IS NOT NULL
    EXEC(N'ALTER TABLE dbo.NhanVien DROP CONSTRAINT [' + @ConstraintNV + N']');

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_NhanVien_Email' AND object_id = OBJECT_ID(N'dbo.NhanVien'))
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UX_NhanVien_Email
    ON dbo.NhanVien(Email)
    WHERE Email IS NOT NULL;
END;
GO
