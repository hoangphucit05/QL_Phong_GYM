# -*- coding: utf-8 -*-
"""
Chương 4: Thiết kế hệ thống
"""

def add_chapter_4(doc, helpers):
    add_h1 = helpers['add_h1']
    add_h2 = helpers['add_h2']
    add_h3 = helpers['add_h3']
    add_h4 = helpers['add_h4']
    p_styled = helpers['p_styled']
    add_tbl = helpers['add_tbl']
    add_diagram_box = helpers['add_diagram_box']
    add_image_box = helpers['add_image_box']

    add_h1("CHƯƠNG 4: THIẾT KẾ HỆ THỐNG")

    # 4.1
    add_h2("4.1. Thiết kế Cơ sở dữ liệu")
    p_styled("Cơ sở dữ liệu của Hệ thống Quản lý Phòng Gym GymPro được thiết kế trên hệ quản trị Microsoft SQL Server với tên CSDL: QL_PhongGym. Hệ thống bao gồm 47 bảng dữ liệu được chuẩn hóa cao, bảo đảm tính toàn vẹn tham chiếu, tính nhất quán giao dịch và hỗ trợ đa người dùng đồng thời.")

    add_h3("4.1.1. Các thực thể và thuộc tính chi tiết (Data Dictionary)")
    p_styled("Dưới đây là từ điển dữ liệu chi tiết của các thực thể cốt lõi trong hệ thống:")

    # Table 4.1: ThanhVien
    headers_tv = ["Tên thuộc tính", "Kiểu dữ liệu", "Khóa / RB", "Cho phép Null", "Ý nghĩa / Mô tả nghiệp vụ"]
    rows_tv = [
        ["MaTV", "INT IDENTITY(1,1)", "PK", "No", "Mã số định danh nội bộ hệ thống"],
        ["MaThanhVien", "VARCHAR(20)", "UNIQUE", "No", "Mã nghiệp vụ hội viên (TV000001) tự động sinh"],
        ["HoTen", "NVARCHAR(100)", "CHECK len>0", "No", "Họ và tên đầy đủ của hội viên"],
        ["SoDienThoai", "VARCHAR(15)", "UNIQUE", "No", "Số điện thoại liên lạc chính (định dạng VN)"],
        ["Email", "VARCHAR(100)", "None", "Yes", "Địa chỉ thư điện tử nhận thông báo"],
        ["NgaySinh", "DATE", "None", "Yes", "Ngày tháng năm sinh của hội viên"],
        ["GioiTinh", "NVARCHAR(10)", "CHECK Nam/Nu", "Yes", "Giới tính (Nam, Nữ, Khác)"],
        ["DiaChi", "NVARCHAR(255)", "None", "Yes", "Địa chỉ nơi cư trú của hội viên"],
        ["MaLoaiTV", "INT", "FK", "No", "Khóa ngoại tham chiếu bảng LoaiThanhVien"],
        ["NgayDangKy", "DATETIME", "DEFAULT GETDATE()", "No", "Thời điểm tạo hồ sơ hội viên"],
        ["DiemTichLuy", "INT", "DEFAULT 0", "No", "Điểm thưởng tích lũy khi mua dịch vụ"],
        ["TrangThai", "NVARCHAR(50)", "DEFAULT HoatDong", "No", "Trạng thái: Hoạt động, Đã khóa, Thôi tập"]
    ]
    add_tbl(headers_tv, rows_tv, col_widths=[2.8, 2.8, 2.5, 2.2, 5.2], caption="Bảng 4.1: Từ điển dữ liệu - Bảng ThanhVien (Quản lý hồ sơ hội viên)")

    # Table 4.2: GoiTap
    rows_goi = [
        ["MaGoi", "INT IDENTITY(1,1)", "PK", "No", "Khóa chính định danh gói tập"],
        ["MaGoiTap", "VARCHAR(20)", "UNIQUE", "No", "Mã gói hiển thị (GOI000001)"],
        ["TenGoi", "NVARCHAR(100)", "None", "No", "Tên thương mại của gói tập"],
        ["ThoiHanNgay", "INT", "CHECK > 0", "No", "Thời lượng sử dụng tính theo ngày (30, 90, 365)"],
        ["GiaTien", "DECIMAL(18,2)", "CHECK >= 0", "No", "Đơn giá niêm yết của gói tập"],
        ["MoTa", "NVARCHAR(500)", "None", "Yes", "Mô tả chi tiết quyền lợi gói"],
        ["GioBatDau", "TIME", "None", "Yes", "Khung giờ bắt đầu được phép vào tập"],
        ["GioKetThuc", "TIME", "None", "Yes", "Khung giờ kết thúc được phép vào tập"],
        ["KichHoat", "BIT", "DEFAULT 1", "No", "Trạng thái gói: 1-Đang bán, 0-Ngừng kinh doanh"]
    ]
    add_tbl(headers_tv, rows_goi, col_widths=[2.8, 2.8, 2.5, 2.2, 5.2], caption="Bảng 4.2: Từ điển dữ liệu - Bảng GoiTap (Danh mục gói dịch vụ tập luyện)")

    # Table 4.3: DangKyGoi
    rows_dkg = [
        ["MaDKG", "INT IDENTITY(1,1)", "PK", "No", "Khóa chính hợp đồng đăng ký gói"],
        ["MaDangKyGoi", "VARCHAR(30)", "UNIQUE", "No", "Mã đăng ký (DKG-YYYYMMDD-xxxx)"],
        ["MaTV", "INT", "FK", "No", "Khóa ngoại tham chiếu bảng ThanhVien"],
        ["MaGoi", "INT", "FK", "No", "Khóa ngoại tham chiếu bảng GoiTap"],
        ["MaKM", "INT", "FK", "Yes", "Khóa ngoại tham chiếu bảng KhuyenMai"],
        ["NgayDangKy", "DATETIME", "DEFAULT GETDATE()", "No", "Thời điểm tạo hợp đồng"],
        ["NgayBatDau", "DATE", "None", "No", "Ngày bắt đầu có hiệu lực tập luyện"],
        ["NgayKetThuc", "DATE", "None", "No", "Ngày hết hạn sử dụng của gói"],
        ["GiaGoiLucDK", "DECIMAL(18,2)", "CHECK >= 0", "No", "Giá gốc của gói tại thời điểm ký"],
        ["GiamGia", "DECIMAL(18,2)", "DEFAULT 0", "No", "Số tiền được giảm giá khuyến mãi"],
        ["ThanhTien", "DECIMAL(18,2)", "CHECK >= 0", "No", "Số tiền thực tế phải nộp sau giảm giá"],
        ["TrangThai", "NVARCHAR(50)", "None", "No", "Trạng thái: Hoạt động, Đang bảo lưu, Hết hạn, Chuyển nhượng"]
    ]
    add_tbl(headers_tv, rows_dkg, col_widths=[2.8, 2.8, 2.5, 2.2, 5.2], caption="Bảng 4.3: Từ điển dữ liệu - Bảng DangKyGoi (Quản lý hợp đồng đăng ký gói tập)")

    # Table 4.4: HuanLuyenVien & DangKyPT
    rows_pt = [
        ["MaDKPT", "INT IDENTITY(1,1)", "PK", "No", "Khóa chính đăng ký PT"],
        ["MaDangKyPT", "VARCHAR(30)", "UNIQUE", "No", "Mã hợp đồng PT (DPT-YYYYMMDD-xxxx)"],
        ["MaTV", "INT", "FK", "No", "Khóa ngoại tham chiếu bảng ThanhVien"],
        ["MaHLV", "INT", "FK", "No", "Khóa ngoại tham chiếu bảng HuanLuyenVien"],
        ["SoBuoiDangKy", "INT", "CHECK > 0", "No", "Tổng số buổi huấn luyện đăng ký (12, 24, 36)"],
        ["SoBuoiDaTap", "INT", "DEFAULT 0", "No", "Số buổi học viên đã hoàn thành thực tế"],
        ["NgayBatDau", "DATE", "None", "No", "Ngày bắt đầu khóa tập kèm 1:1"],
        ["NgayKetThuc", "DATE", "None", "No", "Hạn chót hoàn thành các buổi tập"],
        ["GiaTien", "DECIMAL(18,2)", "CHECK >= 0", "No", "Tổng giá trị hợp đồng PT"],
        ["TrangThai", "NVARCHAR(50)", "None", "No", "Trạng thái: Đang tập, Hoàn thành, Hủy"]
    ]
    add_tbl(headers_tv, rows_pt, col_widths=[2.8, 2.8, 2.5, 2.2, 5.2], caption="Bảng 4.4: Từ điển dữ liệu - Bảng DangKyPT (Hợp đồng huấn luyện cá nhân 1:1)")

    # Table 4.5: HoaDon & ChiTietHoaDon & ThanhToan
    rows_hd = [
        ["MaHD", "INT IDENTITY(1,1)", "PK", "No", "Khóa chính hóa đơn"],
        ["SoHoaDon", "VARCHAR(30)", "UNIQUE", "No", "Số hóa đơn nghiệp vụ (HD-YYYYMMDD-xxxx)"],
        ["MaTV", "INT", "FK", "Yes", "Mã hội viên (cho phép NULL nếu là Khách vãng lai)"],
        ["NgayLap", "DATETIME", "DEFAULT GETDATE()", "No", "Thời điểm lập hóa đơn"],
        ["TongTienGoc", "DECIMAL(18,2)", "CHECK >= 0", "No", "Tổng tiền trước khi giảm trừ"],
        ["TienGiamGia", "DECIMAL(18,2)", "DEFAULT 0", "No", "Tổng số tiền chiết khấu khuyến mãi"],
        ["TongThanhToan", "DECIMAL(18,2)", "CHECK >= 0", "No", "Số tiền thực tế khách hàng phải trả"],
        ["DaThanhToan", "DECIMAL(18,2)", "DEFAULT 0", "No", "Tổng số tiền khách đã thanh toán lũy kế"],
        ["ConNo", "AS TongTT - DaTT", "COMPUTED", "No", "Số dư nợ còn lại của hóa đơn"],
        ["TrangThai", "NVARCHAR(50)", "None", "No", "Trạng thái: Chưa thanh toán, Đã thanh toán, Đã hủy"]
    ]
    add_tbl(headers_tv, rows_hd, col_widths=[2.8, 2.8, 2.5, 2.2, 5.2], caption="Bảng 4.5: Từ điển dữ liệu - Bảng HoaDon (Quản lý hóa đơn tài chính)")

    rows_tt = [
        ["MaTT", "INT IDENTITY(1,1)", "PK", "No", "Khóa chính giao dịch thanh toán"],
        ["SoGiaoDich", "VARCHAR(30)", "UNIQUE", "No", "Số biên nhận giao dịch (TT-YYYYMMDD-xxxx)"],
        ["MaHD", "INT", "FK", "No", "Khóa ngoại tham chiếu bảng HoaDon"],
        ["NgayThanhToan", "DATETIME", "DEFAULT GETDATE()", "No", "Thời điểm thực hiện giao dịch"],
        ["SoTien", "DECIMAL(18,2)", "CHECK > 0", "No", "Số tiền thu trong đợt này"],
        ["HinhThucTT", "NVARCHAR(50)", "CHECK TienMat/CK/The", "No", "Phương thức: Tiền mặt, Chuyển khoản QR, Thẻ POS"],
        ["MaNguoiThu", "INT", "FK", "No", "Khóa ngoại tham chiếu NhanVien thu ngân"],
        ["GhiChu", "NVARCHAR(255)", "None", "Yes", "Mã chuẩn chi ngân hàng hoặc ghi chú"]
    ]
    add_tbl(headers_tv, rows_tt, col_widths=[2.8, 2.8, 2.5, 2.2, 5.2], caption="Bảng 4.6: Từ điển dữ liệu - Bảng ThanhToan (Quản lý giao dịch thu tiền)")

    # Table 4.7: SanPham & PhieuBanHang
    rows_sp = [
        ["MaSP", "INT IDENTITY(1,1)", "PK", "No", "Khóa chính sản phẩm"],
        ["MaSanPham", "VARCHAR(20)", "UNIQUE", "No", "Mã hàng hóa (SP000001)"],
        ["TenSP", "NVARCHAR(100)", "None", "No", "Tên sản phẩm (Nước khoáng, Whey, BCAA...)"],
        ["LoaiSP", "NVARCHAR(50)", "None", "Yes", "Phân loại: Đồ uống, Thực phẩm chức năng, Đồ tập"],
        ["GiaNhap", "DECIMAL(18,2)", "CHECK >= 0", "No", "Giá vốn mua vào từ nhà cung cấp"],
        ["GiaBan", "DECIMAL(18,2)", "CHECK >= 0", "No", "Đơn giá bán lẻ niêm yết tại quầy POS"],
        ["SoLuongTon", "INT", "CHECK >= 0", "No", "Số lượng hàng hóa hiện còn trong kho"]
    ]
    add_tbl(headers_tv, rows_sp, col_widths=[2.8, 2.8, 2.5, 2.2, 5.2], caption="Bảng 4.7: Từ điển dữ liệu - Bảng SanPham (Hàng hóa & Kho quầy Bar)")

    # Table 4.8: ThietBi & BaoTriThietBi
    rows_tb = [
        ["MaTB", "INT IDENTITY(1,1)", "PK", "No", "Khóa chính máy tập"],
        ["MaThietBi", "VARCHAR(20)", "UNIQUE", "No", "Mã quản lý thiết bị (TB000001)"],
        ["TenThietBi", "NVARCHAR(100)", "None", "No", "Tên máy tập (Máy chạy bộ Impulse, Giàn tạ đa năng...)"],
        ["MaPhong", "INT", "FK", "No", "Khóa ngoại tham chiếu PhongTap đang đặt máy"],
        ["NgayMua", "DATE", "None", "Yes", "Thời điểm mua sắm đưa vào sử dụng"],
        ["TrangThai", "NVARCHAR(50)", "None", "No", "Trạng thái: Hoạt động, Đang bảo trì, Hỏng hóc"]
    ]
    add_tbl(headers_tv, rows_tb, col_widths=[2.8, 2.8, 2.5, 2.2, 5.2], caption="Bảng 4.8: Từ điển dữ liệu - Bảng ThietBi (Trang thiết bị máy tập)")

    rows_bt = [
        ["MaBaoTri", "INT IDENTITY(1,1)", "PK", "No", "Khóa chính phiếu bảo trì"],
        ["MaPhieuBaoTri", "VARCHAR(30)", "UNIQUE", "No", "Số phiếu bảo trì (BT-YYYYMMDD-xxxx)"],
        ["MaTB", "INT", "FK", "No", "Khóa ngoại tham chiếu ThietBi cần sửa"],
        ["NgayBaoTri", "DATE", "None", "No", "Ngày bắt đầu tiến hành kiểm tra/sửa chữa"],
        ["ChiPhi", "DECIMAL(18,2)", "CHECK >= 0", "No", "Chi phí sửa chữa thực tế phát sinh"],
        ["DonViBaoTri", "NVARCHAR(150)", "None", "Yes", "Tên đơn vị/thợ kỹ thuật nhận sửa"],
        ["MoTaLoi", "NVARCHAR(500)", "None", "Yes", "Hiện trạng hỏng hóc cần khắc phục"],
        ["TrangThai", "NVARCHAR(50)", "None", "No", "Trạng thái phiếu: Đang xử lý, Đã hoàn tất"]
    ]
    add_tbl(headers_tv, rows_bt, col_widths=[2.8, 2.8, 2.5, 2.2, 5.2], caption="Bảng 4.9: Từ điển dữ liệu - Bảng BaoTriThietBi (Lịch sử bảo trì sửa chữa máy tập)")

    # Table 4.10: NhanVien & TaiKhoan
    rows_nv = [
        ["MaNV", "INT IDENTITY(1,1)", "PK", "No", "Khóa chính nhân viên"],
        ["MaNhanVien", "VARCHAR(20)", "UNIQUE", "No", "Mã nghiệp vụ nhân viên (NV000001)"],
        ["HoTen", "NVARCHAR(100)", "None", "No", "Họ và tên nhân sự"],
        ["ChucVu", "NVARCHAR(50)", "None", "No", "Chức danh: Lễ tân, Thu ngân, HLV, Bán hàng, Quản lý"],
        ["SoDienThoai", "VARCHAR(15)", "UNIQUE", "No", "Số điện thoại liên lạc"],
        ["LuongCoBan", "DECIMAL(18,2)", "CHECK >= 0", "Yes", "Mức lương cơ bản hàng tháng"]
    ]
    add_tbl(headers_tv, rows_nv, col_widths=[2.8, 2.8, 2.5, 2.2, 5.2], caption="Bảng 4.10: Từ điển dữ liệu - Bảng NhanVien (Hồ sơ nhân sự)")

    rows_tk = [
        ["MaTK", "INT IDENTITY(1,1)", "PK", "No", "Khóa chính tài khoản"],
        ["TenDangNhap", "VARCHAR(50)", "UNIQUE", "No", "Tên đăng nhập hệ thống (admin01, letan01...)"],
        ["MatKhauHash", "VARCHAR(255)", "PBKDF2 SHA512", "No", "Chuỗi băm mật khẩu bảo mật cao kèm Salt"],
        ["MaNV", "INT", "FK", "No", "Khóa ngoại tham chiếu nhân viên sở hữu tài khoản"],
        ["TrangThaiKhoa", "BIT", "DEFAULT 0", "No", "Cờ khóa tài khoản khi vi phạm đăng nhập sai"],
        ["SoLanDangNhapSai", "INT", "DEFAULT 0", "No", "Bộ đếm số lần nhập sai mật khẩu (tối đa 5 lần)"]
    ]
    add_tbl(headers_tv, rows_tk, col_widths=[2.8, 2.8, 2.5, 2.2, 5.2], caption="Bảng 4.11: Từ điển dữ liệu - Bảng TaiKhoan (Tài khoản người dùng và bảo mật)")

    # 4.1.2
    add_h3("4.1.2. Xây dựng mô hình thực thể liên kết (ERD)")
    p_styled("Mô hình thực thể liên kết (ERD) thể hiện mối quan hệ logic giữa các thực thể cốt lõi trong phòng gym GymPro:")
    p_styled("• Thực thể ThanhVien có quan hệ 1 - N với DangKyGoi (một hội viên có thể đăng ký nhiều gói tập theo thời gian).")
    p_styled("• Thực thể GoiTap có quan hệ 1 - N với DangKyGoi (một gói tập được nhiều hội viên đăng ký).")
    p_styled("• Thực thể ThanhVien có quan hệ 1 - N với DangKyPT và DangKyLop.")
    p_styled("• Thực thể HuanLuyenVien có quan hệ 1 - N với DangKyPT và LopTap.")
    p_styled("• Thực thể HoaDon có quan hệ 1 - N với ChiTietHoaDon và ThanhToan (một hóa đơn có thể được thanh toán qua nhiều đợt).")
    p_styled("• Thực thể SanPham có quan hệ 1 - N với ChiTietPhieuBanHang và ChiTietPhieuNhap.")
    p_styled("• Thực thể ThietBi có quan hệ 1 - N với BaoTriThietBi; thực thể PhongTap có quan hệ 1 - N với ThietBi.")

    erd_txt = """
+------------------+         1:N         +------------------+         N:1         +------------------+
|    THANH VIEN    | -------------------< |   DANG KY GOI    | >------------------- |     GOI TAP      |
| PK: MaTV         |                     | PK: MaDKG        |                      | PK: MaGoi        |
|     MaThanhVien  |                     | FK: MaTV, MaGoi  |                      |     MaGoiTap     |
+------------------+                     +------------------+                      +------------------+
         | 1                                      | 1                                       | 1
         |                                        |                                         |
         | 1:N                                    | 1:N                                     | N:M
         v                                        v                                         v
+------------------+                     +------------------+                      +------------------+
|    DANG KY PT    |                     |   BAO LUU GOI    |                      |  GOI TAP PHONG   |
| PK: MaDKPT       |                     | PK: MaBaoLuu     |                      | FK: MaGoi        |
| FK: MaTV, MaHLV  |                     | FK: MaDKG        |                      | FK: MaPhong      |
+------------------+                     +------------------+                      +------------------+
         ^                                                                                  ^
         | N:1                                                                              | 1:N
+------------------+         1:N         +------------------+         1:N         +------------------+
| HUAN LUYEN VIEN  | -------------------< |     LOP TAP      | -------------------> |    PHONG TAP     |
| PK: MaHLV        |                     | PK: MaLop        |                      | PK: MaPhong      |
|     MaHLVien     |                     | FK: MaHLV,MaPhong|                      |     TenPhong     |
+------------------+                     +------------------+                      +------------------+
                                                  | 1                                       | 1
                                                  | 1:N                                     | 1:N
                                                  v                                         v
+------------------+         1:N         +------------------+         1:N         +------------------+
|  DIEM DANH LOP   | >------------------ |   DANG KY LOP    |                     |     THIET BI     |
| PK: MaDiemDanh   |                     | PK: MaDKL        |                     | PK: MaTB         |
| FK: MaDKL        |                     | FK: MaTV, MaLop  |                     | FK: MaPhong      |
+------------------+                     +------------------+                      +------------------+
                                                                                            | 1
                                                                                            | 1:N
                                                                                            v
+------------------+         1:N         +------------------+         N:1         +------------------+
|    THANH TOAN    | >------------------ |     HOA DON      | ------------------- | BAO TRI THIET BI |
| PK: MaTT         |                     | PK: MaHD         |                     | PK: MaBaoTri     |
| FK: MaHD,MaNV    |                     | FK: MaTV         |                     | FK: MaTB         |
+------------------+                     +------------------+                      +------------------+
                                                  | 1
                                                  | 1:N
                                                  v
                                         +------------------+         N:1         +------------------+
                                         | CHI TIET HOA DON | >------------------ |     SAN PHAM     |
                                         | PK: MaCTHD       |                     | PK: MaSP         |
                                         | FK: MaHD, MaSP   |                     |     MaSanPham    |
                                         +------------------+                      +------------------+
"""
    add_diagram_box(erd_txt, caption="Sơ đồ 4.1: Mô hình thực thể liên kết (ERD) hệ thống Quản lý GymPro")

    # 4.1.3
    add_h3("4.1.3. Chuyển từ mô hình thực thể liên kết sang mô hình quan hệ")
    p_styled("Từ sơ đồ ERD, hệ thống được ánh xạ thành lược đồ quan hệ chuẩn (Relational Schema) với đầy đủ các bảng dữ liệu, khóa chính gạch chân và khóa ngoại ký hiệu mũi tên tham chiếu:")
    
    schema_txt = """
1. ThanhVien(#MaTV, MaThanhVien, HoTen, SoDienThoai, Email, NgaySinh, GioiTinh, DiaChi, MaLoaiTV*, NgayDangKy, DiemTichLuy, TrangThai)
2. GoiTap(#MaGoi, MaGoiTap, TenGoi, ThoiHanNgay, GiaTien, MoTa, GioBatDau, GioKetThuc, KichHoat)
3. DangKyGoi(#MaDKG, MaDangKyGoi, MaTV*, MaGoi*, MaKM*, NgayDangKy, NgayBatDau, NgayKetThuc, GiaGoiLucDK, GiamGia, ThanhTien, TrangThai)
4. HuanLuyenVien(#MaHLV, MaHLVien, HoTen, ChuyenMon, SoDienThoai, Email, LuongCoBan, TyLeHoaHong, TrangThai)
5. DangKyPT(#MaDKPT, MaDangKyPT, MaTV*, MaHLV*, SoBuoiDangKy, SoBuoiDaTap, NgayBatDau, NgayKetThuc, GiaTien, TrangThai)
6. LopTap(#MaLop, MaLopTap, TenLop, MaLoaiLop*, MaHLV*, MaPhong*, SiSoToiDa, HocPhi, TrangThai)
7. DangKyLop(#MaDKL, MaDangKyLop, MaTV*, MaLop*, NgayDangKy, ThanhTien, TrangThai)
8. DiemDanhLop(#MaDiemDanh, MaDKL*, NgayHoc, CoMat, GhiChu)
9. LichSuCheckIn(#MaCheckIn, MaPhienCheckIn, MaTV*, MaDKG*, ThoiGianVao, ThoiGianRa, MaPhong*, TrangThaiHopLe)
10. HoaDon(#MaHD, SoHoaDon, MaTV*, NgayLap, TongTienGoc, TienGiamGia, TongThanhToan, DaThanhToan, ConNo, TrangThai)
11. ChiTietHoaDon(#MaCTHD, MaHD*, LoaiDichVu, MaDichVu, SoLuong, DonGia, ThanhTien)
12. ThanhToan(#MaTT, SoGiaoDich, MaHD*, NgayThanhToan, SoTien, HinhThucTT, MaNguoiThu*, GhiChu)
13. SanPham(#MaSP, MaSanPham, TenSP, LoaiSP, GiaNhap, GiaBan, SoLuongTon, DonViTinh)
14. PhieuBanHang(#MaPBH, SoPhieuBanHang, MaTV*, NgayBan, TongTien, MaNV*, GhiChu)
15. ChiTietPhieuBanHang(#MaCTPBH, MaPBH*, MaSP*, SoLuong, DonGia, ThanhTien)
16. ThietBi(#MaTB, MaThietBi, TenThietBi, MaLoaiTB*, MaPhong*, NgayMua, GiaTri, TrangThai)
17. BaoTriThietBi(#MaBaoTri, MaPhieuBaoTri, MaTB*, NgayBaoTri, ChiPhi, DonViBaoTri, MoTaLoi, TrangThai)
18. PhongTap(#MaPhong, TenPhong, SucChua, TrangThai)
19. NhanVien(#MaNV, MaNhanVien, HoTen, ChucVu, SoDienThoai, Email, LuongCoBan, NgayVaoLam, TrangThai)
20. TaiKhoan(#MaTK, TenDangNhap, MatKhauHash, Salt, MaNV*, TrangThaiKhoa, SoLanDangNhapSai)
(Ghi chú: # là Khóa chính (PK), * là Khóa ngoại (FK))
"""
    add_diagram_box(schema_txt, caption="Lược đồ quan hệ chuẩn (Relational Schema) của hệ thống GymPro")

    add_h3("4.1.4. Quá trình chuẩn hóa cơ sở dữ liệu đạt chuẩn 3NF")
    p_styled("Để minh chứng cho chất lượng thiết kế CSDL, nhóm nghiên cứu phân tích quá trình chuẩn hóa từ dạng dữ liệu thô (UNF) của nghiệp vụ Hóa đơn & Đăng ký gói:")
    
    p_styled("1. Dạng chưa chuẩn hóa (UNF - Unnormalized Form):")
    p_styled("Hóa đơn đăng ký và mua hàng thô ban đầu có dạng:")
    p_styled("HOADON_THO (SoHD, NgayLap, MaTV, HoTenTV, SoDienThoai, MaGoi, TenGoi, ThoiHan, GiaGoi, MaSP, TenSP, DonGiaSP, SoLuongMua, MaNV, TenNV, SoTienTT, HinhThucTT)")
    p_styled("Trong lược đồ này tồn tại các nhóm thuộc tính lặp lại (Danh sách nhiều sản phẩm, nhiều gói dịch vụ trong 1 hóa đơn) và các thuộc tính đa trị.")

    p_styled("2. Đưa về Dạng chuẩn 1 (1NF):")
    p_styled("• Loại bỏ nhóm thuộc tính lặp bằng cách tách các mặt hàng/dịch vụ mua thành các dòng riêng biệt với khóa chính hợp thành (SoHD, MaDichVu).")
    p_styled("• Đảm bảo mọi giá trị trong mỗi cột đều là nguyên tố (Atomic). Lúc này quan hệ đạt 1NF nhưng xuất hiện tình trạng lặp lại thông tin của Hội viên (HoTen, SĐT) và Nhân viên lập hóa đơn trên nhiều dòng.")

    p_styled("3. Đưa về Dạng chuẩn 2 (2NF):")
    p_styled("• Trong quan hệ có khóa chính hợp thành (SoHD, MaSP), các thuộc tính TenSP, DonGiaSP chỉ phụ thuộc hàm vào một phần của khóa chính là MaSP (MaSP -> TenSP, DonGiaSP), chứ không phụ thuộc vào SoHD.")
    p_styled("• Tiến hành tách thành hai bảng riêng biệt: Bảng SANPHAM (MaSP, TenSP, DonGiaSP) và Bảng CHITIETHOADON (SoHD, MaSP, SoLuong, DonGia, ThanhTien). Khi đó mọi thuộc tính không khóa đều phụ thuộc đầy đủ vào toàn bộ khóa chính, đạt chuẩn 2NF.")

    p_styled("4. Đưa về Dạng chuẩn 3 (3NF):")
    p_styled("• Trong bảng HOADON (SoHD, NgayLap, MaTV, HoTenTV, SĐT, MaNV, TenNV), tồn tại phụ thuộc hàm bắc cầu: SoHD -> MaTV, mà MaTV -> HoTenTV, SoDienThoai (thuộc tính không khóa HoTenTV phụ thuộc bắc cầu vào khóa chính SoHD thông qua thuộc tính không khóa MaTV). Tương tự: SoHD -> MaNV -> TenNV.")
    p_styled("• Giải pháp chuẩn hóa: Tách các thuộc tính hội viên thành bảng riêng THANHVIEN (MaTV, HoTen, SoDienThoai); tách thuộc tính nhân viên thành bảng NHANVIEN (MaNV, HoTen, ChucVu). Trong bảng HOADON chỉ giữ lại các khóa ngoại MaTV và MaNV.")
    p_styled("Kết luận: Sau khi tách, không còn bất kỳ phụ thuộc hàm bắc cầu nào. Toàn bộ các bảng trong CSDL QL_PhongGym đều đạt chuẩn 3NF và BCNF, triệt tiêu hoàn toàn sự dư thừa dữ liệu và loại bỏ nguy cơ dị thường khi cập nhật.")

    # 4.2
    add_h2("4.2. Xây dựng chương trình và Thiết kế giao diện")
    p_styled("Chương trình được hiện thực hóa dưới dạng ứng dụng desktop Windows Forms trên nền tảng .NET 10.0, thiết kế theo phong cách hiện đại (Modern Dark / Neon Theme) kết hợp sắc xanh thể thao, mang lại trải nghiệm chuyên nghiệp, trực quan cho nhân viên thao tác.")

    add_h3("4.2.1. Thiết kế giao diện chính")
    
    add_h4("a. Màn hình Đăng nhập (FrmLogin)")
    p_styled("Màn hình đăng nhập là chốt chặn bảo mật đầu tiên của hệ thống. Giao diện bao gồm ô nhập Tên đăng nhập, Mật khẩu (với biểu tượng hiển thị/ẩn mật khẩu), nút Đăng nhập và tùy chọn Ghi nhớ tài khoản. Hệ thống kết nối với DangNhapBLL để kiểm tra mật khẩu đã băm PBKDF2; nếu người dùng nhập sai quá 5 lần, tài khoản bị tạm khóa 15 phút chống tấn công brute-force.")

    add_h4("b. Giao diện điều hướng chính (Form1)")
    p_styled("Đóng vai trò là Navigation Container với thanh menu bên trái (Sidebar) tổ chức khoa học các chức năng theo nhóm: Quản lý Hội viên, Gói tập & Vận hành, Lớp tập & PT, Bán hàng POS & Kho, Thiết bị & Phòng tập, Tài chính hóa đơn, Báo cáo thống kê và Quản trị người dùng. Hệ thống tự động phân quyền giao diện (RBAC): tài khoản Lễ tân chỉ hiển thị các chức năng quầy, tài khoản HLV chỉ hiển thị lịch tập và điểm danh, chỉ tài khoản Admin mới có quyền truy cập toàn bộ menu.")

    add_h4("c. Dashboard Tổng quan hệ thống (FrmTongQuan)")
    p_styled("Trang chủ hiển thị tức thời các chỉ số vận hành quan trọng (KPIs): Tổng số hội viên đang hoạt động, Doanh thu trong ngày, Doanh thu lũy kế tháng này, Số lượt hội viên đã check-in hôm nay và Số lượng máy móc đang cần bảo trì. Bên cạnh đó là biểu đồ trực quan về xu hướng doanh thu và danh sách hội viên sắp hết hạn gói trong 7 ngày tới.")
    add_image_box("Giao diện GYM PRO hiện đại xanh neon-1.png", width_cm=14.5, caption="Hình 4.2: Giao diện Trung tâm điều khiển chính và Dashboard tổng quan GymPro")

    add_h3("4.2.2. Thiết kế giao diện cập nhật dữ liệu danh mục")
    p_styled("Các màn hình quản lý danh mục tuân thủ nghiêm ngặt chuẩn thiết kế nhất quán (Consistent UI Layout): nửa trên hoặc bên trái là khu vực Form nhập liệu chi tiết; nửa dưới hoặc bên phải là lưới dữ liệu DataGridView có thanh tìm kiếm thông minh, hỗ trợ phân trang và lọc nhanh:")
    
    p_styled("• Giao diện Quản lý Thành viên (FrmThanhVien): Cho phép thêm mới, chỉnh sửa thông tin cá nhân hội viên; kiểm tra chặn trùng số điện thoại; tự động cấp Mã thành viên (TVxxxxxx); phân hạng hội viên; xem lịch sử tập luyện và đo chỉ số InBody.")
    p_styled("• Giao diện Quản lý Gói tập (FrmGoiTap): Thiết lập mã gói (GOIxxxxxx), tên gói, thời hạn ngày, đơn giá, khung giờ tập hợp lệ, phân quyền phòng tập được phép vào.")
    p_styled("• Giao diện Quản lý Nhân viên & HLV (FrmNhanVien, FrmHLV): Quản lý hồ sơ lý lịch, chức vụ, mức lương, chứng chỉ HLV và phân công ca làm việc.")
    p_styled("• Giao diện Quản lý Sản phẩm (FrmSanPham): Danh mục nước giải khát, thực phẩm chức năng, phụ kiện; giá nhập, giá bán lẻ và cảnh báo sản phẩm sắp hết hàng trong kho.")
    p_styled("• Giao diện Quản lý Trang thiết bị (FrmThietBi): Quản lý danh mục máy móc theo từng phòng tập, theo dõi tình trạng kỹ thuật và lịch sử sử dụng máy.")

    add_h3("4.2.3. Thiết kế giao diện xử lý nghiệp vụ và vận hành hệ thống")
    p_styled("Đây là nhóm màn hình phục vụ trực tiếp các quy trình tác nghiệp liên hoàn hàng ngày tại phòng gym:")

    add_h4("a. Giao diện Đăng ký Gói tập (FrmDangKyGoi)")
    p_styled("Cho phép lễ tân chọn hội viên (hoặc tạo nhanh hội viên mới), chọn gói tập mong muốn và áp dụng mã khuyến mãi. Sau khi hoàn tất đăng ký, hệ thống tự động sinh mã DKG-YYYYMMDD-xxxx và mở hộp thoại thu tiền ngay lập tức, liên kết dữ liệu tự động sang màn hình thanh toán mà không cần nhập lại.")

    add_h4("b. Giao diện Vận hành Gói tập (FrmVanHanhGoi)")
    p_styled("Tích hợp 3 nghiệp vụ linh hoạt: Bảo lưu gói tập (chặn chọn ngày quá khứ, tính lại ngày kết thúc), Chuyển nhượng gói tập (chuyển số ngày còn lại sang hội viên khác có thu phí) và Nâng cấp gói tập (khấu trừ giá trị gói cũ vào gói mới). Mọi tác động đều được ghi vết vào bảng LichSuGoiTap.")

    add_h4("c. Giao diện Đăng ký PT & Lớp học (FrmDangKyPT, FrmLopTap, FrmDiemDanh)")
    p_styled("• FrmDangKyPT: Đăng ký hợp đồng huấn luyện viên kèm 1:1, phân công HLV, nhập số buổi đăng ký và xuất hóa đơn.")
    p_styled("• FrmDiemDanh: Cho phép HLV và lễ tân điểm danh nhanh học viên tham gia lớp học nhóm hoặc xác nhận buổi tập PT hoàn thành, tự động trừ số buổi còn lại của học viên.")

    add_h4("d. Giao diện Bán hàng lẻ POS tại quầy (FrmBanHang)")
    p_styled("Thiết kế tối ưu cho quầy Bar: hỗ trợ tìm kiếm nhanh sản phẩm theo tên hoặc quét mã vạch Barcode. Cho phép bán cho Hội viên tích điểm hoặc Khách vãng lai. Sau khi bấm 'Thanh toán', Trigger CSDL tự động trừ kho tức thời và xuất hóa đơn bán lẻ.")

    add_h4("e. Giao diện Quản lý Thiết bị & Lập phiếu Bảo trì (FrmBaoTri)")
    p_styled("Cho phép kỹ thuật viên chọn thiết bị báo hỏng, nhập mô tả lỗi và bấm 'Lập phiếu bảo trì'. Hệ thống kích hoạt Stored Procedure sp_BaoTriThietBi chuyển trạng thái máy sang 'Bảo trì'. Sau khi sửa xong, bấm 'Hoàn tất bảo trì' (sp_HoanTatBaoTri) để nhập chi phí thực tế và khôi phục trạng thái máy sang 'Hoạt động'.")

    add_h4("f. Giao diện Hóa đơn & Thu tiền thanh toán (FrmHoaDon, FrmThanhToan)")
    p_styled("Quản lý danh sách mọi hóa đơn trong hệ thống. Hỗ trợ thu tiền thanh toán nhiều đợt, tự động tính công nợ còn lại (ConNo). Tích hợp chức năng in biên lai nhiệt và cơ chế chặn hủy hóa đơn đã thanh toán để bảo vệ tính minh bạch tài chính.")

    add_h4("g. Giao diện Báo cáo Thống kê (FrmThongKe)")
    p_styled("Hệ thống cung cấp bộ lọc báo cáo đa chiều: Báo cáo tổng doanh thu theo mảng dịch vụ (Gói tập, PT, Lớp học, Bán hàng POS); Biểu đồ cột so sánh doanh thu theo tháng; Thống kê tần suất check-in theo các khung giờ trong ngày giúp ban quản lý tối ưu hóa nhân sự và lịch vận hành.")

    add_h3("4.2.4. Tổng hợp các Ràng buộc Toàn vẹn và Thủ tục Lưu trữ (Stored Procedures & Triggers)")
    p_styled("Để bảo đảm tính toàn vẹn dữ liệu ở mức cơ sở dữ liệu cao nhất, hệ thống triển khai hệ thống ràng buộc, trigger và stored procedure chặt chẽ:")

    headers_proc = ["Tên đối tượng CSDL", "Loại", "Bảng tác động", "Mục đích nghiệp vụ"]
    rows_proc = [
        ["TR_ThanhVien_MaTuDong", "Trigger", "ThanhVien", "Tự động sinh mã hội viên TVxxxxxx sau khi thêm mới"],
        ["TR_DangKyGoi_MaDangKyGoi", "Trigger", "DangKyGoi", "Tự động sinh mã hợp đồng DKG-YYYYMMDD-xxxx"],
        ["TR_DangKyPT_MaDangKyPT", "Trigger", "DangKyPT", "Tự động sinh mã hợp đồng PT DPT-YYYYMMDD-xxxx"],
        ["TR_HoaDon_SoHoaDon", "Trigger", "HoaDon", "Tự động sinh số hóa đơn tài chính HD-YYYYMMDD-xxxx"],
        ["TR_ThanhToan_SoGiaoDich", "Trigger", "ThanhToan", "Tự động sinh số biên nhận thu tiền TT-YYYYMMDD-xxxx"],
        ["TR_ChiTietPhieuBanHang_CapNhatTonKho", "Trigger", "ChiTietPhieuBanHang", "Tự động trừ số lượng tồn kho sản phẩm và ghi LichSuTonKho"],
        ["TR_ChiTietHoaDon_CapNhatTong", "Trigger", "ChiTietHoaDon", "Tự động cập nhật tổng tiền hóa đơn khi thêm/sửa/xóa chi tiết"],
        ["sp_CheckIn", "Stored Proc", "LichSuCheckIn", "Kiểm tra gói tập theo thuật toán FIFO, kiểm tra phòng bảo trì và ghi check-in"],
        ["sp_GiaHanGoiTap", "Stored Proc", "DangKyGoi", "Gia hạn thêm thời gian sử dụng gói tập và tự động tạo hóa đơn"],
        ["sp_BaoTriThietBi", "Stored Proc", "BaoTriThietBi", "Tạo phiếu bảo trì và tự động chuyển trạng thái thiết bị sang Bảo trì"],
        ["sp_HoanTatBaoTri", "Stored Proc", "BaoTriThietBi", "Nghiệm thu sửa chữa và khôi phục trạng thái thiết bị sang Hoạt động"]
    ]
    add_tbl(headers_proc, rows_proc, col_widths=[4.5, 2.0, 3.2, 5.8], caption="Bảng 4.12: Tổng hợp các Stored Procedures và Triggers toàn vẹn dữ liệu trong hệ thống")
