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
INSERT INTO KhuyenMai (TenKM, PhanTramGiam, NgayBatDau, NgayKetThuc, TrangThai) VALUES
(N'Chào hè', 10, '2026-05-01', '2026-06-30', N'Hoạt động'),
(N'Sinh viên năng động', 15, '2026-01-01', '2026-12-31', N'Hoạt động'),
(N'Khách hàng mới', 5, '2026-03-01', '2026-08-31', N'Hoạt động'),
(N'Black Friday', 30, '2026-11-20', '2026-11-30', N'Hoạt động'),
(N'Tết khỏe mạnh', 20, '2026-01-01', '2026-02-28', N'Hết hạn'),
(N'Combo gia đình', 12, '2026-04-01', '2026-09-30', N'Hoạt động'),
(N'Giảm giá PT', 10, '2026-05-01', '2026-10-31', N'Hoạt động'),
(N'Yoga tháng 5', 8, '2026-05-01', '2026-05-31', N'Hoạt động'),
(N'Cardio Day', 7, '2026-06-01', '2026-06-30', N'Hoạt động'),
(N'Ưu đãi VIP', 18, '2026-05-01', '2026-12-31', N'Hoạt động'),
(N'Noel Fit', 25, '2026-12-01', '2026-12-31', N'Hoạt động'),
(N'Tri ân khách hàng', 15, '2026-07-01', '2026-07-31', N'Hoạt động'),
(N'Tập thử miễn phí', 100, '2026-05-01', '2026-05-20', N'Hết hạn'),
(N'Mùa tựu trường', 10, '2026-08-01', '2026-09-15', N'Hoạt động'),
(N'Sức khỏe vàng', 9, '2026-05-10', '2026-08-10', N'Hoạt động');

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
