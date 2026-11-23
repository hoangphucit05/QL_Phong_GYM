# -*- coding: utf-8 -*-
"""
Dữ liệu chi tiết cho bài báo cáo bài tập lớn Phân tích Thiết kế Hệ thống Thông tin - Hệ thống GymPro
"""

ABBREVIATIONS = [
    ("PTTKHTTT", "Phân tích và Thiết kế Hệ thống Thông tin", "Môn học chuyên ngành CNTT"),
    ("HTTT", "Hệ thống thông tin", "Information System"),
    ("MIS", "Hệ thống thông tin quản lý", "Management Information System"),
    ("BFD", "Sơ đồ phân cấp chức năng", "Business Function Diagram"),
    ("DFD", "Biểu đồ luồng dữ liệu", "Data Flow Diagram"),
    ("ERD", "Mô hình thực thể liên kết", "Entity-Relationship Diagram"),
    ("RDBMS", "Hệ quản trị cơ sở dữ liệu quan hệ", "Relational Database Management System"),
    ("UI / UX", "Giao diện và trải nghiệm người dùng", "User Interface / User Experience"),
    ("BLL", "Tầng xử lý logic nghiệp vụ", "Business Logic Layer"),
    ("DAL", "Tầng truy xuất cơ sở dữ liệu", "Data Access Layer"),
    ("PK", "Khóa chính", "Primary Key"),
    ("FK", "Khóa ngoại", "Foreign Key"),
    ("POS", "Hệ thống điểm bán lẻ tại quầy", "Point of Sale"),
    ("PT", "Huấn luyện viên thể hình cá nhân", "Personal Trainer"),
    ("FIFO", "Vào trước - Ra trước", "First In First Out (nguyên tắc trừ hạn gói)"),
    ("RBAC", "Kiểm soát truy cập dựa trên vai trò", "Role-Based Access Control"),
    ("PBKDF2", "Chuẩn mã hóa dẫn xuất khóa an toàn", "Password-Based Key Derivation Function 2"),
    ("CRUD", "Thao tác Tạo, Đọc, Cập nhật, Xóa", "Create, Read, Update, Delete"),
]

TABLES_LIST = [
    ("Bảng 1.1", "Cơ cấu nhân sự và nhiệm vụ các phòng ban tại Trung tâm GymPro"),
    ("Bảng 3.1", "Bảng phân tích xác định Tiến trình, Tác nhân và Hồ sơ dữ liệu"),
    ("Bảng 3.2", "Bảng phân rã sơ đồ chức năng BFD toàn hệ thống"),
    ("Bảng 4.1", "Từ điển dữ liệu - Bảng ThanhVien (Quản lý hồ sơ hội viên)"),
    ("Bảng 4.2", "Từ điển dữ liệu - Bảng GoiTap (Danh mục gói dịch vụ tập luyện)"),
    ("Bảng 4.3", "Từ điển dữ liệu - Bảng DangKyGoi (Quản lý hợp đồng đăng ký gói tập)"),
    ("Bảng 4.4", "Từ điển dữ liệu - Bảng HuanLuyenVien (Hồ sơ Huấn luyện viên)"),
    ("Bảng 4.5", "Từ điển dữ liệu - Bảng DangKyPT (Hợp đồng huấn luyện cá nhân 1:1)"),
    ("Bảng 4.6", "Từ điển dữ liệu - Bảng LopTap & DangKyLop (Lớp học nhóm & Đăng ký lớp)"),
    ("Bảng 4.7", "Từ điển dữ liệu - Bảng HoaDon & ChiTietHoaDon (Quản lý hóa đơn tài chính)"),
    ("Bảng 4.8", "Từ điển dữ liệu - Bảng ThanhToan (Quản lý giao dịch thu tiền)"),
    ("Bảng 4.9", "Từ điển dữ liệu - Bảng SanPham & PhieuBanHang (Hàng hóa & Bán lẻ POS)"),
    ("Bảng 4.10", "Từ điển dữ liệu - Bảng ThietBi & BaoTriThietBi (Trang thiết bị & Bảo trì)"),
    ("Bảng 4.11", "Từ điển dữ liệu - Bảng NhanVien & TaiKhoan (Hồ sơ nhân sự & Phân quyền)"),
    ("Bảng 4.12", "Tổng hợp các Stored Procedures và Triggers toàn vẹn dữ liệu trong hệ thống"),
]

FIGURES_LIST = [
    ("Sơ đồ 1.1", "Sơ đồ cơ cấu tổ chức bộ máy quản lý Trung tâm GymPro"),
    ("Sơ đồ 3.1", "Sơ đồ phân cấp chức năng BFD toàn hệ thống GymPro"),
    ("Sơ đồ 3.2", "Biểu đồ luồng dữ liệu DFD mức ngữ cảnh (Context Diagram)"),
    ("Sơ đồ 3.3", "Biểu đồ luồng dữ liệu DFD mức đỉnh (Level 0)"),
    ("Sơ đồ 3.4", "Biểu đồ luồng dữ liệu DFD mức 2 - Phân hệ Đăng ký & Vận hành Gói tập"),
    ("Sơ đồ 3.5", "Biểu đồ luồng dữ liệu DFD mức 2 - Phân hệ Bán lẻ POS & Quản lý Kho"),
    ("Sơ đồ 3.6", "Biểu đồ luồng dữ liệu DFD mức 2 - Phân hệ Check-in & Điểm danh"),
    ("Sơ đồ 4.1", "Mô hình thực thể liên kết (ERD) hệ thống Quản lý Gym"),
    ("Hình 4.1", "Giao diện Đăng nhập hệ thống và cơ chế phòng vệ lockout (FrmLogin)"),
    ("Hình 4.2", "Giao diện Điều khiển chính và Dashboard tổng quan (Form1 / FrmTongQuan)"),
    ("Hình 4.3", "Giao diện Quản lý Thành viên và Cấp phát mã tự động (FrmThanhVien)"),
    ("Hình 4.4", "Giao diện Đăng ký Gói tập và Tự động tạo hóa đơn thu tiền (FrmDangKyGoi)"),
    ("Hình 4.5", "Giao diện Vận hành Gói tập: Bảo lưu, Chuyển nhượng, Nâng cấp (FrmVanHanhGoi)"),
    ("Hình 4.6", "Giao diện Đăng ký Huấn luyện viên cá nhân PT (FrmDangKyPT)"),
    ("Hình 4.7", "Giao diện Quản lý Lớp tập và Điểm danh lớp học (FrmLopTap, FrmDiemDanh)"),
    ("Hình 4.8", "Giao diện Bán lẻ hàng hóa POS và Trừ tồn kho tự động (FrmBanHang)"),
    ("Hình 4.9", "Giao diện Quản lý Trang thiết bị và Lập phiếu bảo trì (FrmThietBi, FrmBaoTri)"),
    ("Hình 4.10", "Giao diện Quản lý Hóa đơn, Thanh toán & Công nợ (FrmHoaDon, FrmThanhToan)"),
    ("Hình 4.11", "Giao diện Báo cáo Thống kê tài chính và Tần suất tập luyện (FrmThongKe)"),
]
