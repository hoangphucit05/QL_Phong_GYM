# HƯỚNG DẪN SỬ DỤNG VÀ VẬN HÀNH HỆ THỐNG QUẢN LÝ PHÒNG GYM (GYMPRO)

> **Dự án:** Hệ thống Quản lý Phòng Gym (`He_Thong_GYM`)  
> **Nền tảng:** WinForms (.NET 10.0-windows), Cơ sở dữ liệu Microsoft SQL Server (`QL_PhongGym`)  
> **Trạng thái:** Đạt chuẩn nghiệm thu, bảo mật và vận hành thực tế.

---

## 1. Danh sách Tài khoản & Phân quyền Mẫu để Nghiệm thu

Mật khẩu mặc định của tất cả các tài khoản demo trong hệ thống: `Admin@123` *(đã mã hóa PBKDF2 SHA-512 an toàn)*.

| Tên đăng nhập | Vai trò | Họ và tên | Chức năng chính được phép thao tác |
|---|---|---|---|
| **`admin01`** | **Admin** | Nguyễn Văn An | Toàn quyền quản trị hệ thống, phân quyền tài khoản, cấu hình tham số, xem toàn bộ báo cáo doanh thu |
| **`letan01`** | **Nhân viên / Lễ tân** | Trần Thị Bình | Quản lý hội viên, Đăng ký gói tập, Check-in/Check-out quầy, Thu tiền thanh toán, Điểm danh lớp học |
| **`hlv01`** | **HLV** | Lê Quốc Cường | Quản lý lớp tập, Xem danh sách học viên PT, Tạo lịch học, Điểm danh lớp học phụ trách |
| **`banhang01`** | **Bán hàng** | Trịnh Minh Quân | Bán lẻ nước uống/thực phẩm chức năng cho hội viên và Khách vãng lai, Nhập kho hàng hóa |
| **`ketoan01`** | **Kế toán** | Tạ Văn Nam | Quản lý hóa đơn, Thu tiền/Hoàn tiền, Xem báo cáo thống kê doanh thu và công nợ |
| **`baotri01`** | **Bảo trì** | Phan Quốc Phong | Quản lý thiết bị, Lập phiếu bảo trì, Hoàn tất bảo trì khôi phục máy móc |

---

## 2. Hướng dẫn Luồng Nghiệp vụ Trọng tâm

### 2.1. Luồng Hội viên mới → Đăng ký gói → Thu tiền ngay
1. Đăng nhập bằng `admin01` hoặc `letan01`.
2. Mở menu **Quản lý thành viên** (`FrmThanhVien`): Bấm *Thêm mới*, nhập Họ tên, SĐT, Ngày sinh, Giới tính -> Bấm *Lưu*. Hệ thống có kiểm tra chặn SĐT trùng hoặc sai định dạng.
3. Mở menu **Đăng ký gói tập** (`FrmDangKyGoi`):
   - Chọn Hội viên vừa tạo.
   - Chọn Gói tập (vd: *Gói Tháng Cơ Bản*, *Gói VIP 1 Năm*).
   - Chọn Khuyến mãi (nếu có áp dụng).
   - Bấm **Đăng ký gói**: Sau khi đăng ký thành công, phần mềm tự động hỏi: *"Bạn có muốn mở màn hình thu tiền ngay bây giờ không?"*.
   - Bấm **Yes**: Màn hình **Thu tiền** (`FrmThanhToan`) mở ra với đúng mã hóa đơn và số tiền cần thanh toán -> Chọn phương thức (Tiền mặt / Chuyển khoản) -> Bấm **Xác nhận thanh toán**.

### 2.2. Luồng Check-in / Check-out Quầy (Nguyên tắc FIFO)
1. Mở menu **Vận hành gói / Check-in** (`FrmVanHanhGoi`):
   - Chọn phòng tập (Hệ thống chặn không cho chọn phòng đang bảo trì/tạm ngưng).
   - Quét mã thẻ hoặc chọn Hội viên -> Bấm **Check-in**.
   - **Cơ chế FIFO:** Hệ thống tự động kiểm tra và ưu tiên trừ lượt/hạn của gói tập sắp hết hạn sớm nhất trước, tránh thiệt thòi cho hội viên có nhiều gói.
2. Khi hội viên tập xong: Bấm **Check-out** để kết thúc lượt tập, giải phóng trạng thái mở.

### 2.3. Luồng Bán lẻ Nhanh cho Khách vãng lai & Hủy hóa đơn
1. Mở menu **Sản phẩm và bán hàng** (`FrmSanPham`):
   - Bấm **Bán sản phẩm** -> Mở Form bán hàng nhanh (`FrmBanHang`).
   - Khách mua nước uống hoặc găng tay: Phần mềm tự động chọn sẵn **Khách vãng lai** (`TV001035`) -> Chọn sản phẩm, số lượng -> Bấm **Bán hàng**.
   - Hộp thoại hỏi thu tiền mở ra -> Thu tiền ngay.
2. **Hủy hóa đơn & Tự động hoàn kho:**
   - Mở menu **Hóa đơn và thanh toán** (`FrmHoaDon`):
   - Nếu khách hàng đổi ý hủy đơn chưa thanh toán: Chọn hóa đơn -> Bấm **Hủy hóa đơn**. Hệ thống tự động cộng hoàn trả số lượng tồn kho sản phẩm về nguyên vẹn.
   - Nếu hóa đơn đã thanh toán: Hệ thống tuân thủ nghiệp vụ tài chính, yêu cầu hoàn tiền đầy đủ trước khi hủy.

### 2.4. Luồng Nhập kho hàng hóa
1. Mở menu **Sản phẩm và bán hàng** (`FrmSanPham`).
2. Chọn sản phẩm cần nhập thêm (vd: *Whey Protein 1kg* hoặc *Nước khoáng*).
3. Bấm nút **Nhập kho (+)** -> Nhập số lượng nhập và đơn giá nhập -> Bấm **Nhập kho**.
4. Tồn kho sản phẩm tự động tăng ngay lập tức và sinh phiếu nhập kho lưu vào lịch sử biến động kho.

### 2.5. Luồng Bảo trì Thiết bị & Khôi phục máy
1. Mở menu **Bảo trì thiết bị** (`FrmBaoTri`):
   - Chọn thiết bị hư hỏng -> Nhập nội dung, chi phí dự kiến -> Bấm **Lập phiếu bảo trì**.
   - Thiết bị tự động chuyển sang tình trạng *Bảo trì*.
2. Khi kỹ thuật viên sửa xong:
   - Chọn phiếu bảo trì -> Bấm **Hoàn tất bảo trì**.
   - Phiếu bảo trì chuyển thành *Hoàn thành* và tình trạng thiết bị tự động được khôi phục về *Hoạt động*.

---

## 3. Lệnh Tự động Kiểm thử Hệ thống

```powershell
# 1. Kiểm tra biên dịch mã nguồn (Yêu cầu 0 warning, 0 error)
dotnet build He_Thong_GYM.slnx --no-restore

# 2. Kiểm tra tự động 21 Form giao diện WinForms (Font, Overlap, UTF-8)
dotnet run --project He_Thong_GYM/He_Thong_GYM.csproj --no-build -- --check-ui

# 3. Chạy toàn bộ Suite kiểm thử nghiệp vụ tổng hợp (Tự động Rollback sạch)
sqlcmd -S localhost -d QL_PhongGym -E -f 65001 -i database\24_final_regression_test.sql
```
