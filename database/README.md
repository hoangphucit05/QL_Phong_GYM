# Hướng Dẫn Cơ Sở Dữ Liệu - Hệ Thống Quản Lý Phòng GYM

Thư mục `database/` đã được tinh gọn và tổ chức chuẩn mực theo mô hình **Master Script & Migrations History**.

---

## 📁 Cấu Trúc Thư Mục

```text
database/
├── 01_khoi_tao_database_full.sql   # [FILE MASTER] Khởi tạo trọn gói database: Bảng, Ràng buộc, Trigger, Stored Procedure, View và Dữ liệu mẫu ban đầu
├── 02_kiem_thu_hoi_quy.sql         # [KIỂM THỬ] Script tự động test 12 luồng nghiệp vụ lõi (có Rollback an toàn)
├── README.md                       # Tài liệu hướng dẫn cài đặt và vận hành DB
└── migrations_history/             # Thư mục lưu trữ lịch sử toàn bộ các bản vá (patch) từ phiên bản đầu tiên
```

---

## 🚀 Hướng Dẫn Cài Đặt Database (Chỉ 1 Bước)

### Cách 1: Sử dụng SQL Server Management Studio (SSMS) / Azure Data Studio
1. Mở SSMS hoặc Azure Data Studio và kết nối tới SQL Server instance của bạn (`localhost` hoặc `.` hoặc `.\SQLEXPRESS`).
2. Mở file `database/01_khoi_tao_database_full.sql`.
3. Nhấn **F5** (hoặc nút **Execute**).
4. Hệ thống sẽ tự động:
   - Tạo mới database `QL_PhongGym` (hoặc tái tạo cấu trúc chuẩn nếu đã có).
   - Thiết lập toàn bộ 18 bảng dữ liệu, khóa chính, khóa ngoại, chỉ mục và CHECK constraints.
   - Cài đặt 13+ Trigger tự động hóa (sinh mã hội viên/nhân viên, đồng bộ trạng thái, chặn xóa...).
   - Cài đặt toàn bộ Stored Procedures nghiệp vụ (Đăng ký gói, Đăng ký PT, Bán hàng, Nhập kho, Check-in, Hoàn tiền, Đổi mật khẩu...).
   - Nạp sẵn bộ dữ liệu mẫu ban đầu (Loại hội viên, Gói tập, Hội viên, Nhân viên, Tài khoản mặc định, Thiết bị, Sản phẩm...).

> **Tài khoản đăng nhập mặc định sau khi tạo DB:**
> - Quản trị viên: `admin` / Mật khẩu: `admin123` (hoặc `ADMIN01`)
> - Nhân viên thu ngân: `letan` / Mật khẩu: `123456`

---

### Cách 2: Sử dụng dòng lệnh (PowerShell / Command Prompt)

```powershell
sqlcmd -S localhost -E -f 65001 -i "database\01_khoi_tao_database_full.sql"
```
*(Cờ `-f 65001` đảm bảo nạp dữ liệu tiếng Việt UTF-8 chính xác tuyệt đối)*.

---

## 🧪 Kiểm Thử Hồi Quy 12 Luồng Nghiệp Vụ Lõi

Sau khi tạo database hoặc sau mỗi lần cập nhật phần mềm, bạn có thể kiểm thử toàn bộ hệ sinh thái nghiệp vụ:

### Chạy bằng SSMS:
Mở file `database/02_kiem_thu_hoi_quy.sql` và nhấn **F5**.

### Chạy bằng PowerShell:
```powershell
sqlcmd -S localhost -d QL_PhongGym -E -f 65001 -i "database\02_kiem_thu_hoi_quy.sql"
```

### 12 Luồng nghiệp vụ được kiểm tra tự động:
1. **Luồng 1:** Thêm mới hội viên & Chặn trùng lặp SĐT/Email.
2. **Luồng 2:** Đăng ký gói tập có áp dụng chương trình khuyến mãi (`sp_DangKyGoiTap`).
3. **Luồng 3:** Thu tiền & Quyết toán hóa đơn (`sp_ThanhToanHoaDon`).
4. **Luồng 4:** Check-in lượt tập tự động theo cơ chế FIFO (`sp_CheckIn`).
5. **Luồng 5:** Check-out ghi nhận thời gian ra về (`sp_CheckOut`).
6. **Luồng 6:** Gia hạn gói tập và tự động sinh hóa đơn thu phí (`sp_GiaHanGoiTap`).
7. **Luồng 7:** Bảo lưu gói tập theo thời hạn (`sp_BaoLuuGoiTap`).
8. **Luồng 8:** Đăng ký hợp đồng PT (Chờ thanh toán -> Tự kích hoạt khi thu đủ tiền) (`sp_DangKyPT`).
9. **Luồng 9:** Bán lẻ sản phẩm cho Khách vãng lai & Hủy hóa đơn tự động hoàn kho (`sp_BanSanPham`, `sp_HuyHoaDon`).
10. **Luồng 10:** Nhập kho sản phẩm từ Nhà cung cấp (`sp_NhapKho`).
11. **Luồng 11:** Báo hỏng thiết bị đi bảo trì và hoàn tất bảo trì khôi phục máy (`sp_BaoTriThietBi`, `sp_HoanTatBaoTri`).
12. **Luồng 12:** Hoàn tiền toàn bộ và tự động hủy gói tập liên quan (`sp_HoanTien`).

*Lưu ý: File kiểm thử chạy trong TRANSACTION và tự động `ROLLBACK`, không làm sai lệch hay phát sinh rác trong CSDL.*

---

## 📦 Thư Mục `migrations_history/`

Thư mục `migrations_history/` lưu giữ toàn bộ 28 bản vá lịch sử (từ `htql_gym_v2.sql` đến `28_dong_bo_trang_thai_thiet_bi.sql`) và tài liệu nâng cấp chi tiết của các phiên làm việc trước đó. Mục đích phục vụ tra cứu khi cần đối soát hoặc xem lại lịch sử tiến hóa của cơ sở dữ liệu.
