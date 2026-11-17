# Cấu hình và kiểm thử SQL Server

## Thiết lập local

1. Mở SQL Server Management Studio hoặc SQLCMD bằng Windows Authentication.
2. Chạy lần lượt: `database/htql_gym_v2.sql`, `database/01_upgrade_ql_phonggym_v3.sql`, `database/02_seed_v3.sql`, rồi `database/04_khoi_tao_mat_khau_mau.sql`.
   Khi dùng SQLCMD, luôn thêm `-I -f 65001` để giữ đúng tiếng Việt UTF-8, ví dụ: `sqlcmd -S localhost -E -I -f 65001 -i database/htql_gym_v2.sql`.
3. `appsettings.json` mặc định kết nối `localhost`, database `QL_PhongGym`, Windows Authentication. Nếu dùng instance khác, chỉ sửa giá trị `Server` trong file này; không ghi chuỗi kết nối trực tiếp vào Form.
4. Tài khoản mẫu được khởi tạo bằng hash PBKDF2 có mật khẩu tạm `123456`. Đây chỉ là thiết lập development; đăng nhập xong phải đổi sang mật khẩu riêng ít nhất 8 ký tự trước khi triển khai thật.

## Kiểm thử tối thiểu

- Mở ứng dụng, đăng nhập bằng một tài khoản đang hoạt động (ví dụ `admin01`) và mật khẩu tạm sau khi chạy migration khởi tạo.
- Kiểm tra Dashboard và các form Thành viên, Gói tập, Nhân viên nạp dữ liệu SQL Server.
- Thử thêm/sửa dữ liệu trên bản database thử nghiệm; không chạy thử ghi dữ liệu vào production.

## An toàn

- `04_khoi_tao_mat_khau_mau.sql` có chủ đích xóa nội dung password plaintext sau khi hash.
- Không đưa file cấu hình có SQL Authentication hoặc secret vào source control.
- Dùng công cụ `DangNhapBLL.DoiMatKhauAsync` trong màn hình quản lý tài khoản ở phiên sau để đổi mật khẩu; helper này sinh salt riêng cho từng lần đổi.
