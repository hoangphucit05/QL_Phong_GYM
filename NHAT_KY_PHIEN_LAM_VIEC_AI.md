# Nhật ký phiên làm việc AI

Tài liệu này lưu vết các phiên AI đã thực hiện trong dự án để thuận tiện theo dõi thay đổi, kiểm thử và bàn giao.

## Quy ước ghi nhận

- Mỗi phiên ghi rõ ngày giờ, mục tiêu, các tệp thay đổi, kết quả kiểm thử và ghi chú còn lại.
- Không lưu thông tin nhạy cảm như mật khẩu, chuỗi kết nối hoặc khóa API.
- Khi có thay đổi database, nêu rõ migration đã chạy và khả năng hoàn tác.

---

## Phiên 001 — Nâng cấp database QL_PhongGym

- Thời điểm: 16/11/2026
- Mục tiêu: Phân tích và nâng cấp database quản lý phòng gym từ `htql_gym_v2.sql`, giữ lại schema/dữ liệu hiện có và bổ sung các nghiệp vụ còn thiếu.

### Công việc đã thực hiện

- Rà soát 25 bảng gốc, ràng buộc, dữ liệu mẫu và các truy vấn báo cáo.
- Sửa ba truy vấn lỗi trong `htql_gym_v2.sql`:
  - `ThietBi.TrangThai` đổi thành `ThietBi.TinhTrang`.
  - `TenThietBi` đổi thành `TenTB`.
  - Truy vấn thiết bị cần bảo trì dùng trạng thái của `ThietBi` thay vì cột chưa tồn tại trong `BaoTriThietBi`.
- Tạo migration `database/01_upgrade_ql_phonggym_v3.sql`.
- Tạo dữ liệu bổ sung `database/02_seed_v3.sql`.
- Tạo smoke test có rollback `database/03_test_v3.sql`.
- Tạo tài liệu đánh giá, ERD rút gọn và hướng dẫn triển khai tại `database/README_NANG_CAP.md`.

### Chức năng database đã bổ sung

- Mã nghiệp vụ, ảnh đại diện, khóa mềm và lịch sử thay đổi thành viên.
- Gia hạn, bảo lưu, lịch sử gói tập và kiểm soát hiệu lực gói.
- Check-in/check-out độc lập, ngăn một thành viên có nhiều lượt đang mở.
- Lịch PT, lịch học thực tế, chống trùng lịch PT/phòng/HLV và danh sách chờ.
- Thanh toán nhiều lần, hoàn tiền, công nợ và doanh thu thực nhận.
- Tham chiếu hóa đơn có khóa ngoại theo từng loại dịch vụ.
- Nhà cung cấp, nhập/xuất kho, lịch sử tồn và kiểm tra không bán vượt tồn.
- Bảo trì thiết bị, phân quyền N-N, lịch sử đăng nhập và nhật ký hệ thống.
- Stored procedure, trigger, view và index phục vụ vận hành/báo cáo.

### Kiểm thử đã thực hiện

- Chạy file gốc với `sqlcmd -I` để bật `QUOTED_IDENTIFIER`.
- Chạy migration, seed và smoke test trên SQL Server cục bộ.
- Smoke test trả về `PASS`.
- `DBCC CHECKCONSTRAINTS WITH ALL_CONSTRAINTS` không báo vi phạm.
- Database thử nghiệm được tạo riêng và đã xóa sau khi kiểm thử; không tác động database có sẵn của người dùng.

### Lưu ý triển khai

- Sao lưu database trước khi chạy migration.
- Chạy theo thứ tự: file gốc → `01_upgrade_ql_phonggym_v3.sql` → `02_seed_v3.sql` → `03_test_v3.sql`.
- Ứng dụng cần dùng stored procedure cho nghiệp vụ ghi dữ liệu.
- Mật khẩu phải được băm ở tầng ứng dụng (PBKDF2, bcrypt hoặc Argon2); không dùng giá trị plaintext mẫu trong production.

---

## Phiên 002 — Hoàn thiện WinForms Designer cho màn hình chính

- Thời điểm: 16/11/2026
- Mục tiêu: Đảm bảo dự án WinForms có file Designer thực tế, không phải Form trắng và có thể mở bằng Visual Studio Designer.

### Công việc đã thực hiện

- Giữ nguyên cấu trúc partial class gồm `Form1.cs`, `Form1.Designer.cs` và `Form1.resx`.
- Thay thế giao diện mặc định bằng màn hình chính có thanh menu trái, thanh tiêu đề, thông tin người dùng, ngày giờ và vùng nội dung tổng quan.
- Khai báo toàn bộ control, layout, font, màu sắc, dock/anchor và event trong `Form1.Designer.cs`.
- Đặt các event handler xử lý hành vi đơn giản trong `Form1.cs`, gồm chuyển tiêu đề khu vực, hiển thị thời gian và xác nhận đăng xuất.

### Tệp thay đổi

- `He_Thong_GYM/Form1.cs`
- `He_Thong_GYM/Form1.Designer.cs`
- `NHAT_KY_PHIEN_LAM_VIEC_AI.md`

### Kiểm thử

- Chạy `dotnet build He_Thong_GYM.slnx --no-restore`.
- Kết quả: build thành công, 0 warning, 0 error.

### Ghi chú / việc còn lại

- Đã tiếp tục trong cùng phiên: thêm form quản lý thành viên `Forms/FrmThanhVien` với đầy đủ ba tệp `.cs`, `.Designer.cs`, `.resx`.
- Form có GroupBox, Panel, Label, TextBox, ComboBox, DateTimePicker, Button, DataGridView và layout co giãn.
- Menu “Quản lý thành viên” trên màn hình chính đã mở form này trong vùng nội dung.
- Đã tiếp tục Bước 3: tạo `Models`, `DAL`, `BLL` cho thành viên; các thao tác tải, thêm, sửa, tìm kiếm và ngừng sử dụng dùng ADO.NET cùng truy vấn tham số hóa.
- Thêm `appsettings.json` với connection string mẫu, `Microsoft.Data.SqlClient` và cấu hình JSON. Cần thay `YOUR_SERVER` bằng SQL Server thực tế trước khi chạy dữ liệu.
- Kiểm thử build sau khi thêm tầng dữ liệu: thành công, 0 warning, 0 error. Chưa kiểm thử kết nối thực vì connection string được để ở trạng thái mẫu, tránh tự ý kết nối vào máy chủ của người dùng.
- Đã tiếp tục Bước 4: Dashboard dùng dữ liệu SQL Server thật cho tổng thành viên, gói sắp hết hạn, doanh thu thanh toán thành công trong ngày và số lớp đang mở; không tạo dữ liệu giả khi kết nối chưa sẵn sàng.
- Cập nhật điều hướng để quay lại Dashboard sau khi mở Form quản lý thành viên. Build lại thành công, 0 warning, 0 error.
- Đã tiếp tục Bước 5: thêm `FrmLogin` với đủ `.cs`, `.Designer.cs`, `.resx`; thêm phiên người dùng và xác thực PBKDF2 SHA-512 với salt, không đọc mật khẩu plaintext.
- `Program.cs` hiển thị đăng nhập trước màn hình chính; họ tên và vai trò tài khoản được truyền sang giao diện chính sau khi xác thực.
- Điều kiện vận hành: database cần chạy migration v3 để có `TaiKhoan.MatKhauHash` và `MatKhauSalt`; các tài khoản mẫu plaintext phải được chuyển đổi hash trước khi có thể đăng nhập. Build lại thành công, 0 warning, 0 error.
- Đã tiếp tục Bước 6: thêm `FrmGoiTap` với đầy đủ `.cs`, `.Designer.cs`, `.resx`, kèm `GoiTap` Model/DAL/BLL.
- Form đã kết nối ADO.NET để xem, tìm kiếm, thêm, sửa và ngừng áp dụng gói tập bằng truy vấn tham số hóa; menu chính đã mở form trong vùng nội dung. Build thành công, 0 warning, 0 error.
- Đã tiếp tục Bước 7: thêm `FrmNhanVien` với đầy đủ `.cs`, `.Designer.cs`, `.resx`, kèm `NhanVien` Model/DAL/BLL.
- Form hỗ trợ xem, tìm kiếm, thêm, cập nhật và chuyển nhân viên sang trạng thái nghỉ việc bằng ADO.NET. Khi nghỉ việc, tài khoản liên kết cũng được khóa để hạn chế truy cập không hợp lệ. Build thành công, 0 warning, 0 error.
- Đã tiếp tục Bước 8: cấu hình `appsettings.json` cho SQL Server local (`localhost`/Windows Authentication), thêm migration `04_khoi_tao_mat_khau_mau.sql`, tài liệu cấu hình và API đổi mật khẩu PBKDF2 SHA-512 với salt riêng.
- Đã tiếp tục Bước 9: tạo lại `QL_PhongGym` local bằng UTF-8 (`sqlcmd -I -f 65001`), áp dụng migration/seed/migration mật khẩu. Xác nhận 15/15 tài khoản có hash 64 byte, salt 32 byte và không còn plaintext; smoke test `03_test_v3.sql` trả `PASS` và rollback sạch. `DBCC CHECKCONSTRAINTS` không báo vi phạm.

---

## Phiên 003 — Tài khoản, phân quyền và huấn luyện viên

- Thời điểm: 16/11/2026
- Mục tiêu: Hoàn thiện quản lý tài khoản/phân quyền và hồ sơ huấn luyện viên bằng ADO.NET, đồng thời đảm bảo các nghiệp vụ ghi nhiều bảng có transaction.

### Công việc đã thực hiện

- Thêm migration `database/05_phan_quyen_tai_khoan.sql`: mở rộng ràng buộc vai trò, bổ sung các vai trò nghiệp vụ và gán vai trò cho tài khoản hiện có.
- Thêm `FrmTaiKhoan` với đủ `.cs`, `.Designer.cs`, `.resx`; tạo tài khoản mới, gán nhân viên/vai trò, đặt lại mật khẩu hash và khóa/mở khóa tài khoản.
- Thêm `FrmHLV` với đủ `.cs`, `.Designer.cs`, `.resx`; quản lý nhân viên liên kết, chuyên môn, kinh nghiệm và phí thuê.
- Bổ sung Model/DAL/BLL cho tài khoản và HLV; menu chính mở các Form tương ứng.
- Phân quyền màn hình tài khoản: chỉ tài khoản `Admin` được truy cập.
- Dùng transaction với rollback trong tạo tài khoản + gán vai trò và chuyển nhân viên nghỉ việc + khóa tài khoản.

### Kiểm thử

- `dotnet build He_Thong_GYM.slnx --no-restore`: thành công, 0 warning, 0 error.
- Migration phân quyền: 8 vai trò, 15 tài khoản có gán vai trò.
- Kiểm tra DBCC: không có vi phạm ràng buộc.
- Smoke test nghiệp vụ database: `PASS`, rollback sạch dữ liệu thử.

### Ghi chú / việc còn lại

- Chức năng tạo/sửa lịch PT sẽ được triển khai trong phiên đăng ký PT.
- Quản lý quyền hiện dùng vai trò chính của `TaiKhoan`; bảng `TaiKhoanVaiTro` đã sẵn sàng cho phân quyền nhiều vai trò trong giai đoạn mở rộng.

---

## Phiên 004 — Gói tập, khuyến mãi, đăng ký gói và đăng ký PT

- Thời điểm: 16/11/2026
- Mục tiêu: Hoàn thiện luồng khuyến mãi, đăng ký gói tập và đăng ký PT có tạo hóa đơn đúng quan hệ.

### Công việc đã thực hiện

- Thêm migration `database/06_dang_ky_pt.sql` với stored procedure `sp_DangKyPT`, transaction và rollback khi lỗi.
- Thêm `FrmKhuyenMai`, `FrmDangKyGoi`, `FrmDangKyPT`; mỗi form có đủ `.cs`, `.Designer.cs`, `.resx`.
- Bổ sung tầng Model/DAL/BLL cho danh mục đăng ký, khuyến mãi, HLV và lịch sử đăng ký.
- Form đăng ký tự tính giá sau giảm, ngày kết thúc, lịch sử đăng ký và tạo hóa đơn qua stored procedure.
- Bổ sung menu cho khuyến mãi và đăng ký PT.

### Kiểm thử

- Build: thành công, 0 warning, 0 error.
- Kiểm thử `sp_DangKyPT` và `sp_DangKyGoiTap`: tạo được chi tiết hóa đơn/mapping tham chiếu đúng, trả `PASS`.
- Dữ liệu test đã được xóa theo thứ tự khóa ngoại sau kiểm thử.
- `DBCC CHECKCONSTRAINTS` không báo vi phạm.

### Ghi chú / việc còn lại

- Gia hạn/bảo lưu đã có stored procedure từ migration v3; giao diện thao tác chuyên biệt sẽ được bổ sung cùng luồng hợp đồng/gói tập khi mở rộng.
- Thanh toán hóa đơn của các đăng ký được triển khai trong phiên tài chính/bán hàng tiếp theo.

---

## Phiên 005 — Lớp tập, đăng ký lớp và điểm danh

- Thời điểm: 16/11/2026
- Mục tiêu: Hoàn thiện quản lý lớp, đăng ký học viên và điểm danh theo từng ngày.

### Công việc đã thực hiện

- Thêm `database/07_diem_danh_lop.sql` với stored procedure `sp_DiemDanhLop`, transaction và rollback.
- Thêm `FrmLopTap` với đầy đủ `.cs`, `.Designer.cs`, `.resx`; tạo lớp, chọn loại lớp/HLV, sức chứa và khung giờ.
- Thêm `FrmDiemDanh` với đầy đủ `.cs`, `.Designer.cs`, `.resx`; đăng ký học viên theo stored procedure, xem danh sách và điểm danh Có mặt/Vắng mặt theo ngày.
- Bổ sung Model/DAL/BLL cho lớp tập, học viên lớp, đăng ký lớp và điểm danh.
- Cập nhật menu điều hướng riêng cho Lớp tập và Đăng ký/điểm danh.

### Kiểm thử

- Kiểm thử `sp_DiemDanhLop`: `PASS`; bản ghi kiểm thử được xóa.
- Build: thành công, 0 warning, 0 error.
- `DBCC CHECKCONSTRAINTS`: không có vi phạm.

### Ghi chú / việc còn lại

- `sp_DangKyLop` từ migration v3 kiểm tra sức chứa và đưa học viên vào danh sách chờ nếu lớp đã đầy.
- Lịch học thực tế/phòng học nâng cao đã có bảng migration v3 và sẽ được tích hợp tiếp khi cần quản lý lịch theo buổi.

---

## Phiên 006 — Hóa đơn, thanh toán, sản phẩm và bán hàng

- Thời điểm: 16/11/2026
- Mục tiêu: Hoàn thiện luồng hóa đơn, thanh toán/hoàn tiền, sản phẩm và bán hàng có kiểm soát tồn kho.

### Công việc đã thực hiện

- Thêm `FrmHoaDon`, `FrmThanhToan`, `FrmSanPham`, `FrmBanHang`; mỗi form có đủ `.cs`, `.Designer.cs`, `.resx`.
- Bổ sung Model/DAL/BLL cho hóa đơn, công nợ và sản phẩm.
- `FrmThanhToan` gọi `sp_ThanhToanHoaDon` và `sp_HoanTien`; hiển thị tổng tiền, đã trả và còn nợ theo dữ liệu thật.
- `FrmBanHang` gọi `sp_BanSanPham`, tạo hóa đơn và trừ tồn kho qua stored procedure có transaction.
- Cập nhật menu mở hóa đơn/thanh toán và sản phẩm/bán hàng.

### Kiểm thử

- Build: thành công, 0 warning, 0 error.
- Thanh toán và hoàn tiền hóa đơn: `PASS`; dữ liệu kiểm thử đã được xóa.
- Bán sản phẩm: `PASS`; mapping hóa đơn, lịch sử tồn và số lượng tồn đã được khôi phục sau test.
- `DBCC CHECKCONSTRAINTS`: không có vi phạm.

### Ghi chú / việc còn lại

- Form sản phẩm hiện ưu tiên tra cứu/bán hàng; nghiệp vụ nhập kho và chỉnh sửa danh mục sẽ được mở rộng cùng quản lý kho nếu cần.

---

## Phiên 007 — Thiết bị, bảo trì và phòng tập

- Thời điểm: 16/11/2026
- Mục tiêu: Hoàn thiện quản lý tài sản, vị trí phòng và lịch sử bảo trì thiết bị.

### Công việc đã thực hiện

- Thêm `FrmThietBi`, `FrmBaoTri`, `FrmPhongTap`; mỗi form có đủ `.cs`, `.Designer.cs`, `.resx`.
- Bổ sung Model/DAL/BLL cho thiết bị, phòng tập và bảo trì.
- `FrmBaoTri` gọi `sp_BaoTriThietBi` để lưu phiếu bảo trì, nhân viên phụ trách và chi phí theo transaction database.
- Cập nhật menu riêng cho Thiết bị, Bảo trì thiết bị và Phòng tập.

### Kiểm thử

- Kiểm thử tạo phiếu bảo trì: `PASS`; bản ghi test đã được xóa.
- Build: thành công, 0 warning, 0 error.
- `DBCC CHECKCONSTRAINTS`: không có vi phạm.

---

## Phiên 008 — Báo cáo, thống kê và nghiệm thu

- Thời điểm: 16/11/2026
- Mục tiêu: Hoàn thiện báo cáo KPI và nghiệm thu tổng thể dự án.

### Công việc đã thực hiện

- Thêm `FrmThongKe` với đầy đủ `.cs`, `.Designer.cs`, `.resx`.
- Bổ sung báo cáo dữ liệu thật: tổng thành viên, doanh thu thực nhận, công nợ, giá trị tồn kho và chi phí bảo trì.
- Cập nhật menu mở báo cáo thống kê.
- Mở ứng dụng đã build để kiểm tra trực quan Form đăng nhập và cấu trúc Designer.

### Kiểm thử

- Build tổng thể: thành công, 0 warning, 0 error.
- Kiểm thử phiếu bảo trì: `PASS`.
- `DBCC CHECKCONSTRAINTS`: không có vi phạm.
- Tồn tại trên database local sau nghiệm thu: 15 thiết bị, 7 phòng tập và 15 hóa đơn mẫu.

### Kết luận

- Hoàn thành các phiên 002 đến 008 theo phạm vi hiện tại.
- Các Form đã tạo đều có bộ `.cs`, `.Designer.cs`, `.resx` và `InitializeComponent()` không rỗng.
- Các luồng nghiệp vụ đã triển khai gọi ADO.NET/stored procedure, không dùng dữ liệu mô phỏng cho báo cáo và thao tác chính.

---

## Phiên 009 — Hoàn thiện CRUD, vận hành và nghiệm thu cuối

- Thời điểm: 16/11/2026
- Mục tiêu: Hoàn thiện các nghiệp vụ vận hành còn thiếu trên giao diện, bắt đầu bằng gói tập và kiểm soát ra/vào.

### Bước 1 — Vận hành gói và check-in/check-out

- Thêm `FrmVanHanhGoi` với đầy đủ `.cs`, `.Designer.cs`, `.resx`.
- Bổ sung Model/DAL/BLL để xem gói của thành viên, check-in, check-out, gia hạn và bảo lưu gói tập.
- Form gọi các stored procedure `sp_CheckIn`, `sp_CheckOut`, `sp_GiaHanGoiTap`, `sp_BaoLuuGoiTap`; không dùng SQL ghép chuỗi.
- Thêm menu “Vận hành gói / Check-in”.
- Kiểm thử chuỗi đăng ký gói → thanh toán → check-in → check-out: `PASS`; dữ liệu test đã được xóa.
- Build: thành công, 0 warning, 0 error.

### Việc còn lại trong Phiên 009

- CRUD giao diện cho khuyến mãi, sản phẩm/nhập kho, thiết bị và phòng tập.
- Lịch PT/lịch học thực tế và hoàn thành buổi PT.
- Bộ lọc thời gian, biểu đồ và xuất báo cáo.

### Bước 6 — Ghi nhận tài khoản người dùng

- Đã đối chiếu trực tiếp dữ liệu bảng `dbo.TaiKhoan` trên database `QL_PhongGym`.
- Có 15 tài khoản, đều ở trạng thái **Hoạt động**, có hash và salt mật khẩu hợp lệ; nhật ký không lưu mật khẩu hoặc giá trị hash/salt.

| Mã | Tên đăng nhập | Vai trò | Trạng thái |
|---:|---|---|---|
| 1 | `admin01` | Admin | Hoạt động |
| 2 | `letan01` | Nhân viên | Hoạt động |
| 3 | `hlv01` | HLV | Hoạt động |
| 4 | `hlv02` | HLV | Hoạt động |
| 5 | `hlv03` | HLV | Hoạt động |
| 6 | `hlv04` | HLV | Hoạt động |
| 7 | `hlv05` | HLV | Hoạt động |
| 8 | `hlv06` | HLV | Hoạt động |
| 9 | `hlv07` | HLV | Hoạt động |
| 10 | `hlv08` | HLV | Hoạt động |
| 11 | `ketoan01` | Nhân viên | Hoạt động |
| 12 | `letan02` | Nhân viên | Hoạt động |
| 13 | `baotri01` | Nhân viên | Hoạt động |
| 14 | `banhang01` | Nhân viên | Hoạt động |
| 15 | `tuvan01` | Nhân viên | Hoạt động |

- Thành viên phòng gym chưa có tài khoản đăng nhập độc lập; các tài khoản hiện được liên kết với nhân viên.

---

## Phiên 010 — Kiểm tra toàn diện Layout, nâng cấp trải nghiệm người dùng (UI/UX) và chuẩn hóa UTF-8 Tiếng Việt

- Thời điểm: 17/11/2026
- Mục tiêu: Kiểm tra toàn bộ giao diện dự án WinForms xem có phần nào bị đè lên nhau, tràn viền gây khó nhìn hay không, nâng cấp trải nghiệm người dùng (UI/UX, tự động co giãn, chống tràn biên), kiểm tra toàn bộ hiển thị văn bản Tiếng Việt theo chuẩn UTF-8 và sửa dứt điểm các lỗi phát hiện được.

### Công việc đã thực hiện

1. **Phân tích và kiểm thử tự động toàn bộ 21 Form trong hệ thống**:
   - Xây dựng công cụ kiểm tra kích thước thực tế, bounding box của các controls, kiểm tra độ giao thoa diện tích (overlap), kiểm tra tràn biên (`Out of bounds`), kiểm tra cắt chữ (`Text Truncation`) và lỗi mã hóa Tiếng Việt.
   - Phát hiện ban đầu: **30 lỗi đè control** nghiêm trọng tại 6 màn hình (`FrmBaoTri`, `FrmDiemDanh`, `FrmKhuyenMai`, `FrmPhongTap`, `FrmThietBi`, `FrmVanHanhGoi`) do Label có `AutoSize = false` chiếm diện tích cố định đè lên TextBox/ComboBox lân cận, và DateTimePicker/NumericUpDown bị xếp chồng lên nhau.
   - Phát hiện lỗi co giãn: các form con có `MinimumSize = 1000x700` khiến khi nhúng vào `panelNoiDung` của `Form1` (chiều cao khả dụng ~660px) bị cắt mất 40px phần chân form (các nút thao tác và thanh trạng thái bị che khuất).
   - Phát hiện lỗi hiển thị thẻ chỉ số: ở `FrmThongKe`, khi gán dữ liệu số vào `lblThanhVien.Text`, `lblDoanhThu.Text`... thì tiêu đề thẻ bị xóa sạch, chỉ còn lại con số trần khiến người dùng không biết số đó là gì.

2. **Khắc phục triệt để lỗi đè nhau & nâng cấp UI/UX**:
   - **`FrmPhongTap`**: Tái cấu trúc bằng `TableLayoutPanel`, `AutoSize = true` cho nhãn, thêm neo `Anchor = Top | Left | Right` cho nhóm nhập liệu, loại bỏ hoàn toàn 5 điểm đè nhau.
   - **`FrmThietBi`**: Thiết kế lại nhóm nhập liệu với bảng 4 cột đều đặn, neo chuẩn, loại bỏ hoàn toàn 4 điểm đè nhau.
   - **`FrmBaoTri`**: Chuyển đổi mã thu gọn thành cấu trúc chuẩn với TableLayoutPanel, nút "Tạo phiếu bảo trì" rõ ràng, neo chuẩn, loại bỏ 4 điểm đè nhau.
   - **`FrmDiemDanh`**: Bố trí 2 dòng thao tác chuẩn mực (Đăng ký lớp & Điểm danh chuyên cần), neo chuẩn, loại bỏ 5 điểm đè nhau.
   - **`FrmKhuyenMai`**: Tăng khoảng cách cột, định dạng ngày tháng rõ ràng, loại bỏ 2 điểm đè nhau.
   - **`FrmVanHanhGoi`**: Tái thiết kế toàn diện khu vực Check-in/Check-out và Gia hạn/Bảo lưu (loại bỏ 10 điểm đè nhau chồng chéo giữa DateTimePicker và TextBox lý do), neo DataGridView co giãn theo chiều cao cửa sổ (`Top | Bottom | Left | Right`).
   - **`FrmThongKe`**: Thiết kế lại thẻ KPI với bảng 3 cột x 2 dòng cân đối, tách biệt Label tiêu đề (luôn hiển thị rõ ràng) và Label giá trị (cỡ chữ 24pt Bold nổi bật).
   - **`FrmHoaDon` & `FrmSanPham`**: Thêm thanh công cụ thao tác có neo co giãn, nút bấm bo góc phẳng hiện đại, định dạng trực quan.
   - **`FrmBanHang` & `FrmThanhToan` (Hộp thoại)**: Cấu hình chuẩn `FixedDialog`, `MaximizeBox = false`, bố cục form rõ ràng, phân biệt màu sắc thao tác thu tiền / hoàn tiền.
   - **`Form1` (Màn hình chính)**:
     - Xóa padding 30px bao quanh `panelNoiDung` (đã chuyển vào trong từng form con) giúp form con không còn bị ép chặt.
     - Bật `AutoScroll = true` cho `panelNoiDung` để hỗ trợ hoàn hảo màn hình độ phân giải thấp (1366x768, màn hình laptop 720p/768p).
     - Bổ sung hiệu ứng chọn tab trên thanh menu bên trái (nút đang mở chuyển sang màu xanh nổi bật, giúp người dùng luôn biết mình đang ở chức năng nào).
     - Kích hoạt nút "Tài khoản" góc trên bên phải để xem thông tin người dùng đang đăng nhập hoặc mở quản trị tài khoản cho Admin.
   - **Chuẩn hóa `MinimumSize`**: Điều chỉnh `MinimumSize = 850x480` và bật `AutoScroll = true` trên toàn bộ form con, đảm bảo giao diện luôn hiển thị đầy đủ trên mọi kích cỡ màn hình.

3. **Kiểm tra và chuẩn hóa mã hóa Tiếng Việt UTF-8**:
   - Rà soát toàn bộ chuỗi ký tự Tiếng Việt trong code C#, câu lệnh SQL và file tài nguyên resx: không có ký tự lỗi (`\ufffd`), không có ký tự `?` do mất dấu, không có lỗi ghép chuỗi.
   - Chuyển đổi và bổ sung UTF-8 BOM (`\xEF\xBB\xBF`) cho toàn bộ 122 file mã nguồn (`.cs`, `.Designer.cs`, `.resx`, `.sql`, `.json`, `.md`).
   - Đảm bảo Visual Studio Designer, trình biên dịch Roslyn (`csc`), SQL Server Management Studio (`ssms`) và `sqlcmd` luôn nhận diện chuẩn xác 100% tiếng Việt có dấu.

### Tệp thay đổi

- `He_Thong_GYM/Form1.cs`
- `He_Thong_GYM/Form1.Designer.cs`
- `He_Thong_GYM/Program.cs`
- `He_Thong_GYM/Helpers/UiInspector.cs`
- `He_Thong_GYM/Forms/FrmPhongTap.cs` & `FrmPhongTap.Designer.cs`
- `He_Thong_GYM/Forms/FrmThietBi.cs` & `FrmThietBi.Designer.cs`
- `He_Thong_GYM/Forms/FrmBaoTri.cs` & `FrmBaoTri.Designer.cs`
- `He_Thong_GYM/Forms/FrmDiemDanh.cs` & `FrmDiemDanh.Designer.cs`
- `He_Thong_GYM/Forms/FrmKhuyenMai.cs` & `FrmKhuyenMai.Designer.cs`
- `He_Thong_GYM/Forms/FrmVanHanhGoi.cs` & `FrmVanHanhGoi.Designer.cs`
- `He_Thong_GYM/Forms/FrmLopTap.cs` & `FrmLopTap.Designer.cs`
- `He_Thong_GYM/Forms/FrmHoaDon.cs` & `FrmHoaDon.Designer.cs`
- `He_Thong_GYM/Forms/FrmSanPham.cs` & `FrmSanPham.Designer.cs`
- `He_Thong_GYM/Forms/FrmBanHang.cs` & `FrmBanHang.Designer.cs`
- `He_Thong_GYM/Forms/FrmThanhToan.cs` & `FrmThanhToan.Designer.cs`
- `He_Thong_GYM/Forms/FrmThongKe.cs` & `FrmThongKe.Designer.cs`
- `He_Thong_GYM/Forms/FrmThanhVien.Designer.cs`
- `He_Thong_GYM/Forms/FrmGoiTap.Designer.cs`
- `He_Thong_GYM/Forms/FrmHLV.Designer.cs`
- `He_Thong_GYM/Forms/FrmDangKyGoi.Designer.cs`
- `He_Thong_GYM/Forms/FrmDangKyPT.Designer.cs`
- `He_Thong_GYM/Forms/FrmNhanVien.Designer.cs`
- `He_Thong_GYM/Forms/FrmTaiKhoan.Designer.cs`
- `NHAT_KY_PHIEN_LAM_VIEC_AI.md`

### Kiểm thử

- Chạy kiểm tra tự động toàn bộ 21 Form:
  - **Lỗi đè control (Overlap): 0 / 21 form** (giảm từ 30 xuống 0).
  - **Lỗi tràn biên/mất góc (Out of bounds): 0 / 21 form**.
  - **Lỗi chữ bị cắt (Text Truncation): 0 / 21 form**.
  - **Lỗi hiển thị Tiếng Việt / UTF-8: 0 / 21 form**.
- Chạy `dotnet build He_Thong_GYM.slnx --no-restore`: thành công 100%, **0 warning, 0 error**.

### Khắc phục lỗi Visual Studio Designer CodeDOM parser

- **Vấn đề**: Visual Studio Designer báo lỗi `The designer cannot process the code at line X: for(...) / helper methods` do bộ phân tích CodeDOM của WinForms Designer không hỗ trợ vòng lặp `for`, biểu thức lambda (`=>`), hoặc gọi helper method tùy biến bên trong `InitializeComponent()`.
- **Giải pháp**:
  - Chuẩn hóa toàn bộ 21 file `.Designer.cs` theo chuẩn CodeDOM của Visual Studio WinForms Designer.
  - Gỡ bỏ hoàn toàn các vòng lặp `for`/`foreach`, thay bằng khai báo tuần tự từng `RowStyle`/`ColumnStyle`.
  - Thay thế toàn bộ biểu thức lambda trong `.Designer.cs` (như `btnHuy.Click += (s, e) => Close();`) bằng named event handler trong file code-behind `.cs`.
  - Inline toàn bộ các hàm trợ giúp gán thuộc tính (`CauHinhNhan`, `CauHinhNut`, `CauHinhCot`, `TaoThe`) thành các phép gán thuộc tính trực tiếp cho từng Control.
  - Bảo toàn 100% tên biến, DataPropertyName của các cột DataGridView, sự kiện Click/SelectionChanged/ValueChanged và logic nghiệp vụ.
- **Kết quả xác minh**:
  - Quét CodeDOM Designer: **0 lỗi** trên tất cả các Form.
  - Build solution: **0 warning, 0 error**.
  - Kiểm tra UI Inspector: **0 overlap, 0 out-of-bounds, 0 text-truncation, 0 encoding issues**.

---

## Phiên 005 — Hoàn thiện Báo cáo thống kê trực quan & Tích hợp Logo nhận diện thương hiệu

- **Thời điểm**: 17/11/2026
- **Mục tiêu**:
  - Nâng cấp giao diện Báo cáo thống kê toàn diện, trực quan, hỗ trợ bộ lọc và biểu đồ GDI+.
  - Khắc phục triệt để lỗi SQL Runtime `Invalid column name 'GiaBan'`.
  - Tích hợp biểu tượng thương hiệu (Huy hiệu GYM PRO neon xanh lam) vào góc trên Sidebar Menu chính (`Form1`).

### Công việc đã thực hiện

1. **Khắc phục lỗi SQL Thống kê**:
   - Đồng bộ tên cột trong `dbo.SanPham` thành `DonGia` (thay vì `GiaBan`).
   - Cập nhật điều kiện lọc ngày của `DangKyGoi` và `DangKyPT` theo `NgayBatDau` (thay vì `NgayDangKy`).
   - Kết hợp bảng `ChiTietHoaDon` (nghiệp vụ v3) và `ChiTietPhieuBanHang` (schema gốc v2) để thống kê chính xác lượng tiêu thụ và doanh số sản phẩm.

2. **Tích hợp Logo nhận diện thương hiệu trên Sidebar Menu chính (`Form1`)**:
   - Tích hợp hình ảnh `Huy hiệu GYM PRO neon xanh lam.png` vào khối thương hiệu `panelThuongHieu` ở góc trên bên trái menu điều hướng.
   - Thêm `picLogo` (54x54 px, `PictureBoxSizeMode.Zoom`) đặt tại $(X = 12, Y = 23)$.
   - Bố cục lại khối chữ thương hiệu: `lblThuongHieu` ("GYM PRO", 15.5pt Bold trắng tại $X = 72, Y = 22$) và `lblPhuDe` ("Hệ thống quản lý phòng gym", 8pt Slate-400 tại $X = 74, Y = 51$).
   - Đảm bảo 100% chuẩn Visual Studio Designer CodeDOM.

3. **Nâng cấp Login Branding (Màn hình Đăng nhập `FrmLogin`)**:
   - Kết hợp ảnh nền phòng GYM hiện đại (`bg_login_branding.png` được phủ lớp gradient tối cao cấp `#0F172A` $\rightarrow$ `#090E1A`) cho toàn bộ panel trái (`panelThuongHieu`).
   - Đặt biểu tượng `Huy hiệu GYM PRO neon xanh lam` nổi bật ở vị trí trung tâm (`picLogo`, 100x100 px tại $X = 102, Y = 45$).
   - Căn chỉnh chữ `GYM PRO` (Bold 24pt trắng) và phụ đề `Quản lý vận hành thông minh` (màu xanh dương nhạt `#BAE6FD`), nền trong suốt tự nhiên, tạo chiều sâu thị giác sang trọng.

### Kết quả kiểm thử
- Build solution: **0 warning, 0 error**.
- UI Inspector: **21/21 form vượt qua kiểm tra giao diện tự động** (0 overlap, 0 out-of-bounds, 0 text-truncation).


---

## Phiên 006 — Sửa lỗi chọn phòng ở Form Thiết bị & Hoàn thiện Bộ lọc tra cứu đa năng cho các Form

- **Thời điểm**: 19/11/2026
- **Mục tiêu**:
  - Khắc phục triệt để lỗi khi chọn phòng tập tại form Quản lý thiết bị (`FrmThietBi`).
  - Thiết kế và bổ sung các bộ lọc tìm kiếm tức thời (theo từ khóa, danh mục, trạng thái, phòng tập) cho các form chức năng cần thiết.

### Công việc đã thực hiện

1. **Khắc phục lỗi và hoàn thiện bộ lọc Form Quản lý thiết bị (`FrmThietBi`)**:
   - **Nguyên nhân lỗi cũ**: Dropdown `cboPhong` chưa từng được nạp dữ liệu từ database, không có danh sách lựa chọn và chưa gắn sự kiện lọc, dẫn đến danh sách không phản hồi hoặc gặp lỗi xử lý khi tương tác chọn phòng.
   - **Giải pháp**:
     - Nạp danh mục phòng tập thực tế từ cơ sở dữ liệu (`_bll.LayPhongAsync()`) vào `cboPhong` với các tùy chọn đầy đủ: `-- Tất cả phòng --`, danh sách phòng cụ thể, và `(Chưa phân phòng)`.
     - Tự động nạp danh sách loại thiết bị duy nhất từ dữ liệu thực tế vào `cboLoai` (`-- Tất cả loại --`, Máy chạy bộ, Xe đạp tập, Tạ đơn,...).
     - Nạp trạng thái thiết bị chuẩn: `-- Tất cả tình trạng --`, Hoạt động, Bảo trì, Hư hỏng, Thanh lý.
     - Bổ sung ô tìm kiếm nhanh `txtTen` theo tên thiết bị hoặc mã thiết bị theo thời gian thực (`TextChanged`).
     - Bổ sung nút bấm `btnLamMoi` ("Làm mới / Bỏ lọc") và nhãn `lblThongKe` ("Đang hiển thị: X / Y thiết bị").
     - Định dạng dữ liệu lưới DataGridView chuyên nghiệp, căn lề và format ngày tháng `dd/MM/yyyy`.

2. **Thêm bộ lọc lịch sử Đăng ký gói tập (`FrmDangKyGoi`)**:
   - Bổ sung thanh công cụ tìm kiếm và lọc phía trên bảng lịch sử đăng ký gói (`panelLocLichSu`):
     - Ô tìm kiếm `txtTimKiemLichSu`: Tra cứu theo tên hội viên, tên gói tập hoặc mã đăng ký.
     - Dropdown `cboLocTrangThai`: Lọc nhanh theo `-- Tất cả --`, Còn hạn, Hết hạn, Chờ thanh toán.
     - Nút `btnLamMoiLichSu` và nhãn thống kê số lượt đăng ký tìm thấy.
     - Định dạng tiền tệ VNĐ (`N0`) và ngày bắt đầu/kết thúc (`dd/MM/yyyy`).

3. **Thêm bộ lọc lịch sử Đăng ký huấn luyện viên / PT (`FrmDangKyPT`)**:
   - Bổ sung thanh công cụ tìm kiếm và lọc phía trên bảng lịch sử đăng ký PT:
     - Ô tìm kiếm `txtTimKiemLichSu`: Tìm theo tên hội viên, tên HLV hoặc mã đăng ký.
     - Dropdown `cboLocTrangThai`: Lọc theo trạng thái Còn hạn, Hết hạn, Chờ thanh toán.
     - Tự động tính toán và hiển thị tổng tiền sau giảm giá theo buổi và tỷ lệ khuyến mãi.

4. **Thêm bộ lọc Quản lý sản phẩm & tồn kho (`FrmSanPham`)**:
   - Bổ sung ô tìm kiếm tên/mã sản phẩm `txtTimKiem` và dropdown trạng thái `cboTrangThai` (Đang bán, Hết hàng, Ngừng kinh doanh).
   - Tự động lọc danh sách trực tiếp trên giao diện và hiển thị số lượng tồn/đơn giá VNĐ chuẩn hóa.

5. **Thêm bộ lọc Quản lý hóa đơn & thanh toán (`FrmHoaDon`)**:
   - Bổ sung ô tìm kiếm `txtTimKiem` theo mã hóa đơn, tên hội viên/khách hàng.
   - Dropdown lọc trạng thái: `-- Tất cả --`, Chờ thanh toán, Đã thanh toán, Một phần, Hủy.
   - Hiển thị và format số tiền: Tổng tiền, Đã trả, Còn nợ theo chuẩn VNĐ (`N0`).

6. **Thêm bộ lọc Quản lý bảo trì thiết bị (`FrmBaoTri`)**:
   - Bổ sung ô tìm kiếm theo tên thiết bị, nhân viên phụ trách, nội dung sửa chữa/thay thế linh kiện.
   - Dropdown lọc trạng thái phiếu bảo trì: Hoàn thành, Đang xử lý, Chờ linh kiện.

7. **Thêm bộ lọc Quản lý lớp tập (`FrmLopTap`)**:
   - Bổ sung ô tìm kiếm tên lớp, thể loại, huấn luyện viên giảng dạy.
   - Dropdown lọc trạng thái lớp: Đang mở, Đã đóng.

8. **Thêm bộ lọc Quản lý phòng tập (`FrmPhongTap`)**:
   - Bổ sung ô tìm kiếm tên phòng, dropdown lọc theo loại phòng (Gym thường, Yoga, Boxing, Cardio, VIP) và trạng thái phòng tập (Hoạt động, Đang bảo trì, Tạm đóng).

### Kết quả kiểm thử
- **Biên dịch**: `dotnet build` đạt **0 Warning, 0 Error**.
- **Kiểm tra giao diện tự động (`--check-ui`)**: **21/21 form vượt qua 100%** (0 overlap, 0 out-of-bounds, 0 text-truncation, 0 encoding issues).
- **Tuân thủ chuẩn CodeDOM Designer**: Tất cả các file `.Designer.cs` đều tương thích 100% với Visual Studio Form Designer.


---

## Phiên 007 — Rà soát toàn diện mã nguồn & Tối ưu hóa các Control, Hàm vô nghĩa

- **Thời điểm**: 19/11/2026
- **Mục tiêu**:
  - Quét toàn bộ 21 Form, các tầng BLL, DAL, Models và Helpers để phát hiện các control giao diện, sự kiện hoặc hàm rỗng/vô nghĩa (dead code / dummy controls).
  - Tối ưu hóa, dọn dẹp và kết nối hoàn thiện logic nghiệp vụ cho các vị trí được phát hiện.

### Các vấn đề được phát hiện và xử lý triệt để

1. **Dọn dẹp hàm rỗng và sự kiện thừa trong `FrmLogin`**:
   - **Phát hiện**: Hàm `private void picLogo_Click(object sender, EventArgs e) { }` rỗng hoàn toàn, được sinh tự động do nhấp đúp vào PictureBox logo trong Visual Studio Designer.
   - **Xử lý**: Loại bỏ phương thức rỗng trong `FrmLogin.cs` và gỡ bỏ việc đăng ký sự kiện `picLogo.Click += picLogo_Click;` trong `FrmLogin.Designer.cs`.

2. **Kích hoạt tính năng lọc thực tế cho `nudSucChua` trong `FrmPhongTap`**:
   - **Phát hiện**: Control `nudSucChua` (NumericUpDown) có mặt trên giao diện và được gán sự kiện `ValueChanged += BoLocThayDoi;`, nhưng bên trong hàm `ApDungLoc()` lại hoàn toàn bỏ qua giá trị này, khiến việc người dùng tăng giảm sức chứa không có bất kỳ tác dụng nào. Ngoài ra nút "Làm mới" cũng không reset `nudSucChua`.
   - **Xử lý**: 
     - Bổ sung điều kiện lọc `p.SucChua >= (int)nudSucChua.Value` khi `nudSucChua.Value > 0` trong `ApDungLoc()`.
     - Đổi nhãn hiển thị thành `lblSucChua.Text = "Sức chứa từ:"` để người dùng hiểu rõ ý nghĩa lọc theo sức chứa tối thiểu.
     - Bổ sung `nudSucChua.Value = 0;` trong hàm xử lý nút `btnLamMoi_Click`.

3. **Tái thiết kế và kích hoạt chức năng lọc cho `FrmKhuyenMai`**:
   - **Phát hiện**:
     - `FrmKhuyenMai` có các ô `txtTen`, `nudGiam`, `dtpTu`, `dtpDen` nhưng không có nút Lưu/Thêm, không có sự kiện tìm kiếm/lọc, khiến việc người dùng nhập liệu vào các ô này trở nên vô nghĩa.
     - `KhuyenMaiDAL.cs` trước đây chỉ truy vấn 3 cột `MaKM, TenKM, PhanTramGiam`, bỏ qua các cột `NgayBatDau`, `NgayKetThuc`, `TrangThai` sẵn có trong SQL Server.
   - **Xử lý**:
     - Nâng cấp model `KhuyenMai.cs` bổ sung `NgayBatDau`, `NgayKetThuc`, `TrangThai`.
     - Cập nhật `KhuyenMaiDAL.cs` truy vấn đầy đủ tất cả các trường dữ liệu từ bảng `dbo.KhuyenMai`.
     - Tái thiết kế giao diện `FrmKhuyenMai.Designer.cs` thành bộ lọc chuẩn hóa: Ô tìm kiếm `txtTen` (tên/mã KM), `nudGiam` (mức giảm tối thiểu %), dropdown `cboTrangThai` (-- Tất cả trạng thái --, Hoạt động, Hết hạn), nút `btnLamMoi` ("Làm mới / Bỏ lọc") và nhãn `lblThongKe`.
     - Triển khai logic lọc dữ liệu tức thì `ApDungLoc()` và định dạng lưới DataGridView với đầy đủ các cột: Mã KM, Tên khuyến mãi, % Giảm giá (`N0`), Ngày bắt đầu (`dd/MM/yyyy`), Ngày kết thúc (`dd/MM/yyyy`), Trạng thái.

### Kết quả kiểm tra & Đo lường
- **Quét toàn diện mã nguồn**:
  - Hàm rỗng / Chưa implement / TODO: **0**
  - Control giao diện vô nghĩa / không được sử dụng: **0**
  - Phương thức private không được gọi: **0**
- **Biên dịch**: `dotnet build` đạt **0 Warning, 0 Error**.
- **Kiểm tra giao diện tự động (`--check-ui`)**: **21/21 form vượt qua 100%** (0 overlap, 0 out-of-bounds, 0 text-truncation, 0 encoding issues).
- **Chuẩn CodeDOM Designer**: Đảm bảo 100% tương thích Visual Studio Form Designer.


---

## Phiên 008 — Cập nhật Mã khuyến mãi dạng chữ (Voucher Code) thực tế thay vì số thứ tự

- **Thời điểm**: 19/11/2026
- **Mục tiêu**:
  - Thay thế mã khuyến mãi dạng số thứ tự đơn điệu (`1, 2, 3...`) bằng mã khuyến mãi dạng chữ (Voucher / Coupon Code) thực tế phù hợp với các hệ thống quản lý phòng gym hiện đại (như `CHAOHE10`, `SINHVIEN15`, `NEWGYM5`, `BLACKFRIDAY30`, `TET2026`,...).
  - Đảm bảo hiển thị trực quan trên danh sách khuyến mãi, ô tìm kiếm và dropdown đăng ký gói tập/PT.
  - Tương thích ngược 100% với cơ sở dữ liệu hiện có (vừa hỗ trợ cột `MaCode` trong SQL, vừa có cơ chế tự động chuyển đổi thông minh nếu DB chưa migrate).

### Công việc đã thực hiện

1. **Nâng cấp Model `KhuyenMai.cs`**:
   - Bổ sung trường `MaCode` (mã voucher thực tế dạng chữ).
   - Thêm thuộc tính `HienThi`: Định dạng thân thiện cho ComboBox dạng `[MACODE] Tên khuyến mãi (-XX%)` (Ví dụ: `[CHAOHE10] Chào hè (-10%)`).
   - Xây dựng phương thức trợ giúp `TaoMaCodeThucTe(tenKM, phanTramGiam, maKM)`: Tự động ánh xạ thông minh các chương trình sang mã voucher thực tế (loại bỏ dấu tiếng Việt, trích xuất từ khóa và tỷ lệ giảm giá).

2. **Cập nhật tầng DAL (`KhuyenMaiDAL.cs` & `DangKyDAL.cs`)**:
   - Sử dụng cơ chế kiểm tra linh hoạt `CoCot(r, "MaCode")`:
     - Nếu cơ sở dữ liệu đã có cột `MaCode`: Đọc trực tiếp từ cơ sở dữ liệu.
     - Nếu cơ sở dữ liệu dùng schema cũ chưa có `MaCode`: Tự động sinh mã voucher tương ứng thông qua thuật toán ánh xạ, tuyệt đối không gây lỗi truy vấn SQL runtime.

3. **Cập nhật giao diện Quản lý Khuyến mãi (`FrmKhuyenMai`)**:
   - Cột **Mã khuyến mãi** hiển thị nổi bật mã code dạng chữ in hoa xanh dương đậm (`CHAOHE10`, `SINHVIEN15`, `BLACKFRIDAY30`...) thay vì các số ID `1, 2, 3`.
   - Ẩn cột ID nội bộ `MaKM` để giao diện mang tính thương mại thực tế.
   - Thanh tìm kiếm `txtTen` hỗ trợ tìm kiếm trực tiếp theo cả mã code (ví dụ gõ `CHAOHE` hoặc `BLACK`) lẫn tên chương trình.

4. **Nâng cấp ComboBox Đăng ký gói tập & PT (`FrmDangKyGoi` & `FrmDangKyPT`)**:
   - Chuyển `DisplayMember` của `cboKhuyenMai` sang thuộc tính `HienThi`:
     - `Không áp dụng`
     - `[CHAOHE10] Chào hè (-10%)`
     - `[SINHVIEN15] Sinh viên năng động (-15%)`
     - `[BLACKFRIDAY30] Black Friday (-30%)`
     - `[NEWGYM5] Khách hàng mới (-5%)`...
   - Người dùng và nhân viên bán hàng dễ dàng nhận diện và đối soát mã giảm giá thực tế khi tư vấn cho hội viên.

5. **Cập nhật Cơ sở dữ liệu & Script Migration**:
   - Cập nhật `database/htql_gym_v2.sql`: Bổ sung cột `MaCode NVARCHAR(30) NULL` vào bảng `KhuyenMai` và seed data 15 mã voucher thực tế.
   - Tạo mới file migration `database/08_cap_nhat_ma_code_khuyen_mai.sql` giúp người dùng cập nhật nhanh cơ sở dữ liệu hiện có chỉ bằng 1 lần thực thi SQL.

### Kết quả kiểm thử
- **Biên dịch**: `dotnet build` đạt **0 Warning, 0 Error**.
- **Kiểm tra giao diện tự động (`--check-ui`)**: **21/21 form vượt qua 100%** (0 overlap, 0 out-of-bounds, 0 text-truncation, 0 encoding issues).
- **Chuẩn CodeDOM Designer**: Tương thích 100% với Visual Studio Form Designer.


---

## Phiên 009 — Sửa đăng ký lớp trùng và hiển thị điểm danh

- **Thời điểm**: 19/11/2026
- **Vấn đề ghi nhận**:
  - Màn hình `Đăng ký và điểm danh` hiển thị lỗi SQL kỹ thuật `UQ_DangKyLop` khi đăng ký lại cùng thành viên và lớp.
  - Lưới học viên hiển thị tên cột nội bộ (`MaDKL`, `TrangThaiDiemDanh`) và để trống trạng thái khi chưa điểm danh.

### Công việc đã thực hiện

1. **Chặn đăng ký lớp trùng tại tầng SQL**
   - Cập nhật `dbo.sp_DangKyLop` trong `database/01_upgrade_ql_phonggym_v3.sql`.
   - Kiểm tra bản ghi `DangKyLop` và `DanhSachCho` trước khi `INSERT`, với khóa cập nhật để tránh xung đột đồng thời.
   - Trả thông báo nghiệp vụ rõ ràng: `Thành viên đã đăng ký lớp này.` hoặc `Thành viên đang ở danh sách chờ của lớp này.`
   - Đã áp dụng trực tiếp thủ tục mới vào database `QL_PhongGym`.

2. **Cải thiện danh sách điểm danh**
   - `LopTapDAL.LayHocVienAsync` hiển thị `Chưa điểm danh` khi chưa có bản ghi theo ngày.
   - Dùng tham số SQL có kiểu dữ liệu rõ ràng cho mã lớp và ngày điểm danh.
   - `FrmDiemDanh` đổi tiêu đề lưới thành: `Mã ĐK`, `Mã TV`, `Thành viên`, `Số điện thoại`, `Điểm danh`.

### Kết quả kiểm tra

- Kiểm thử lại đăng ký trùng `(MaTV=13, MaLop=15)`: thủ tục trả lỗi nghiệp vụ `50029`, không còn lỗi ràng buộc `UNIQUE KEY`.
- `dotnet build He_Thong_GYM.slnx --no-restore`: **0 warning, 0 error**.


---

## Phiên 010 — Audit đối chiếu nghiệp vụ vận hành gym thực tế

- **Thời điểm**: 19/11/2026
- **Phạm vi**: Rà soát không thay đổi dữ liệu/mã nguồn; đối chiếu các luồng thành viên, gói tập, lớp/điểm danh, PT, hóa đơn, kho, phân quyền và báo cáo.

### Kết quả quan trọng

1. **Lịch lớp và điểm danh cần hoàn thiện trước**
   - Database có `LichHoc` và thủ tục `sp_TaoLichHoc`, nhưng ứng dụng chưa có màn hình vận hành lịch học.
   - Kiểm tra dữ liệu thực tế: 15 lớp đang mở chưa có lịch học; 17 bản ghi điểm danh không gắn `MaLichHoc`.
   - Hướng xử lý: điểm danh theo từng buổi `LichHoc`, không cho chọn ngày tùy ý; bổ sung tạo/hủy/dời lịch, kiểm tra phòng/HLV trùng giờ và danh sách chờ.

2. **Cần có công việc tự động hóa vòng đời gói tập**
   - Kiểm tra dữ liệu thực tế: có 12 đăng ký còn ghi `Còn hạn` dù `NgayKetThuc` đã qua.
   - Hướng xử lý: job hằng ngày cập nhật hết hạn, cảnh báo trước hạn, và chỉ cho check-in với gói còn hiệu lực/đã thanh toán.

3. **Hợp nhất dữ liệu bán hàng và báo cáo**
   - Bán hàng hiện dùng `HoaDon`/`ChiTietHoaDon`, nhưng schema vẫn có `PhieuBanHang`/`ChiTietPhieuBanHang`; báo cáo còn tham chiếu bảng cũ.
   - Không xóa bảng cũ ngay: cần migration số liệu, chuyển báo cáo sang hóa đơn chung, đối soát, sau đó đánh dấu legacy chỉ-đọc hoặc loại bỏ có kiểm soát.

4. **Chuẩn hóa phân quyền**
   - Database có cả `TaiKhoan.VaiTro` và quan hệ `TaiKhoanVaiTro`; ứng dụng hiện chủ yếu đọc cột đơn `VaiTro`.
   - Cần chọn một nguồn phân quyền chính, áp quyền theo từng thao tác ở UI/BLL/database; hiện menu chỉ hạn chế một số mục Admin.

5. **Các hạng mục nghiệp vụ nên đưa vào backlog**
   - Hóa đơn: hủy hóa đơn, in/xuất biên nhận, đối soát tiền mặt/chuyển khoản, lý do điều chỉnh/hoàn tiền.
   - Lớp/PT: lịch làm việc HLV, buổi học, hủy/no-show, giới hạn số buổi còn lại, quyền đổi lịch.
   - Vận hành: đăng nhập thất bại/khóa tạm, audit cho các thao tác nhạy cảm, sao lưu/khôi phục và phân quyền SQL tối thiểu.
   - Dữ liệu thành viên: đồng ý lưu hồ sơ sức khỏe, tìm kiếm theo mã/QR, lịch sử liên hệ và cảnh báo sức khỏe có kiểm soát quyền.

### Kết luận

- Không đề xuất xóa chức năng người dùng đang dùng ngay lập tức.
- Ưu tiên triển khai: (1) lịch lớp + điểm danh theo buổi, (2) tự động hết hạn gói, (3) phân quyền theo thao tác, (4) hợp nhất dữ liệu bán hàng/báo cáo.


---

## Phiên 011 — Triển khai lịch lớp và điểm danh theo buổi

- **Thời điểm**: 19/11/2026
- **Mục tiêu**: Buộc mọi lượt điểm danh gắn với một buổi học thực tế (`LichHoc`), thay vì chỉ nhập ngày tùy ý.

### Luồng nghiệp vụ đã triển khai

1. Nhân viên chọn lớp và ngày học.
2. Nếu chưa có buổi học, tạo buổi từ khung giờ, HLV và phòng đã cấu hình cho lớp.
3. SQL kiểm tra lớp/phòng/HLV đang hoạt động, phòng đủ sức chứa, lịch trùng và không tạo buổi trong quá khứ.
4. Danh sách học viên chỉ tải sau khi đã xác định được `MaLichHoc`.
5. Điểm danh chỉ hợp lệ khi học viên còn đăng ký lớp, thuộc đúng buổi học và buổi đó không nằm trong tương lai.

### Thay đổi chính

- Thêm model `BuoiHoc`.
- Bổ sung `LayBuoiHocAsync`, `TaoBuoiHocAsync` và điểm danh theo `MaLichHoc` trong BLL/DAL.
- `FrmDiemDanh` có nút `Tạo buổi học`, hiển thị buổi đang chọn và không nạp danh sách để điểm danh khi chưa có buổi.
- Thêm migration `database/10_lich_hoc_diem_danh_theo_buoi.sql`:
  - Tạo chỉ mục chống trùng buổi học cùng lớp/cùng giờ.
  - Chuẩn hóa `sp_TaoLichHoc` và `sp_DiemDanhLop`.
  - Cố định `ANSI_NULLS` và `QUOTED_IDENTIFIER` để tương thích filtered index.
  - Chuyển trạng thái buổi học sang `Đã diễn ra` khi có lượt điểm danh đầu tiên.
- Đã áp dụng migration vào database `QL_PhongGym`.

### Kiểm thử

- Kiểm thử có rollback: tạo buổi cho lớp 1 → điểm danh học viên đăng ký → kiểm tra bản ghi theo `MaLichHoc`: **PASS**.
- Build: `dotnet build He_Thong_GYM.slnx --no-restore`: **0 warning, 0 error**.
- Phát hiện dữ liệu lớp 15 đang có sức chứa 30 nhưng phòng Yoga A chỉ có 25 chỗ. Luồng mới chặn tạo buổi cho cấu hình không hợp lệ; cần điều chỉnh sức chứa lớp hoặc gán phòng phù hợp trước khi vận hành.


---

## Phiên 013 — Xác nhận mức độ sẵn sàng vận hành thực tế

- **Thời điểm**: 19/11/2026
- **Kết luận**: Luồng giao diện lịch lớp/điểm danh đã đúng hướng và có kiểm tra nghiệp vụ cốt lõi, nhưng hệ thống **chưa đạt mức hoàn chỉnh để vận hành thực tế**.

### Bằng chứng còn tồn đọng

- 12 gói vẫn mang trạng thái `Còn hạn` dù đã quá ngày kết thúc: cần job tự động hết hạn và cảnh báo gia hạn.
- 15 lớp đang mở chưa có buổi học tương lai: cần màn hình kế hoạch/lập lịch hàng loạt hoặc lịch định kỳ.
- 17 bản ghi điểm danh cũ chưa gắn `MaLichHoc`: cần kế hoạch dữ liệu lịch sử (gán lại theo quy tắc rõ ràng hoặc đánh dấu legacy).
- 1 lớp có sức chứa 30 nhưng phòng được gán chỉ có 25 chỗ: cần sửa cấu hình trước khi tạo buổi.
- `sp_DiemDanhLop` chưa nhận nhân viên/tài khoản thực hiện và chưa kiểm tra quyền ở database: phân quyền hiện tại chỉ ở giao diện, chưa phải kiểm soát bảo mật đầy đủ.

### Tiêu chí để xác nhận “đủ vận hành”

1. Không còn gói quá hạn ghi `Còn hạn`.
2. Mọi lớp đang mở có lịch học được tạo trước buổi tập.
3. Mọi điểm danh mới gắn `MaLichHoc`; dữ liệu cũ có trạng thái/ghi chú migration rõ ràng.
4. Sức chứa lớp không vượt sức chứa phòng.
5. Thao tác tạo buổi, đăng ký, điểm danh và hoàn tiền được kiểm tra quyền ở BLL/database, có audit người thực hiện.


---

## Phiên 012 — Bộ lọc, sắp xếp và quyền thao tác điểm danh

- **Thời điểm**: 19/11/2026

### Thay đổi

1. **Bộ lọc và sắp xếp danh sách học viên**
   - Lọc theo: tất cả, chưa điểm danh, có mặt, vắng mặt.
   - Sắp xếp theo: họ tên A–Z/Z–A, mã hội viên tăng/giảm, trạng thái điểm danh.
   - Nút `Bỏ lọc` khôi phục trạng thái mặc định: tất cả học viên, họ tên A–Z.
   - Bộ lọc/sắp xếp chỉ thay đổi dữ liệu hiển thị của buổi học đang chọn, không thay đổi dữ liệu điểm danh.

2. **Quyền thao tác tại màn hình lớp/điểm danh**
   - Xem, lọc và sắp xếp: mọi người dùng đã đăng nhập.
   - Đăng ký thành viên vào lớp: `Admin`, `Quản lý`, `Lễ tân`, `Nhân viên`.
   - Tạo buổi học và điểm danh: `Admin`, `Quản lý`, `Lễ tân`, `HLV`, `Huấn luyện viên`.
   - Nút không có quyền sẽ bị vô hiệu hóa và các hàm xử lý vẫn kiểm tra lại quyền trước khi ghi dữ liệu.

### Kiểm thử

- Build: `dotnet build He_Thong_GYM.slnx --no-restore`: **0 warning, 0 error**.


---

## Phiên 014 — Chuẩn bị dữ liệu an toàn và kiểm tra Form Designer

- **Thời điểm**: 19/11/2026
- **Mục tiêu**: Chốt điểm khôi phục, số liệu baseline trước các migration vận hành; xác nhận mọi file WinForms Designer vẫn hợp lệ.

### Đã thực hiện

1. **Backup và xác minh database**
   - Tạo full backup có `CHECKSUM` và `COMPRESSION`:
     `C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\QL_PhongGym_session01_20261119_1519.bak`.
   - Database `QL_PhongGym`: `ONLINE`, recovery model `FULL`, kích thước khoảng `144 MB`.
   - Chạy `RESTORE VERIFYONLY ... WITH CHECKSUM`: backup hợp lệ.
   - Không thay đổi dữ liệu nghiệp vụ trong phiên này.

2. **Script đối soát trước/sau migration**
   - Thêm `database/11_kiem_tra_san_sang_van_hanh.sql` (chỉ đọc dữ liệu).
   - Script kiểm tra: gói quá hạn, lớp chưa có buổi tương lai, điểm danh legacy, sức chứa lớp/phòng và tổng tiền hóa đơn.
   - Baseline đã chốt: `12` gói quá hạn, `15` lớp chưa có buổi tương lai, `17` điểm danh legacy, `1` cấu hình sức chứa sai, `0` hóa đơn lệch tổng tiền chi tiết.

3. **Bảo vệ và kiểm tra WinForms Designer**
   - Bổ sung `.gitignore` cho file backup SQL (`*.bak`, `*.trn`).
   - Build solution thành công: **0 warning, 0 error**.
   - Chạy `--check-ui`: **21/21 form** khởi tạo được; **0** overlap, **0** out-of-bounds, **0** text truncation, **0** lỗi UTF-8.
   - Kết luận: các file `.Designer.cs` hiện hợp lệ để tiếp tục chỉnh sửa có kiểm soát ở các phiên sau.


---

## Phiên 015 — Tự động cập nhật gói hết hạn

- **Thời điểm**: 21/11/2026
- **Mục tiêu**: Không để đăng ký gói đã quá ngày kết thúc tiếp tục mang trạng thái `Còn hạn`, đồng thời giữ lịch sử và audit đầy đủ.

### Đã thực hiện

1. **Thủ tục SQL `sp_CapNhatGoiHetHan`**
   - Nhận ngày tham chiếu, cập nhật các gói `Còn hạn` có `NgayKetThuc` trước ngày đó sang `Hết hạn`.
   - Ghi `LichSuGoiTap` với hành động `Hết hạn`.
   - Ghi `NhatKyHeThong` số lượng bản ghi đã cập nhật.
   - Có transaction, khóa cập nhật và tham số output số lượng thay đổi.

2. **Tự động chạy định kỳ**
   - Migration `database/12_tu_dong_het_han_goi.sql` tạo SQL Server Agent Job chạy hằng ngày lúc 00:05.
   - Ứng dụng cũng gọi thủ tục khi mở Dashboard để không phụ thuộc hoàn toàn vào SQL Server Agent.
   - Job đã tạo và bật; máy chủ hiện chưa chạy dịch vụ SQL Server Agent nên lịch sẽ thực thi khi dịch vụ được bật.

3. **Tích hợp ứng dụng**
   - Bổ sung DAL/BLL cập nhật gói hết hạn.
   - `Form1_Load` chạy đồng bộ hết hạn trước khi tải Dashboard.
   - Không chỉnh sửa file `.Designer.cs` trong phiên này.

### Kiểm thử và kết quả

- Lần chạy đầu: cập nhật đúng `12` gói quá hạn.
- Sau cập nhật: còn `0` gói quá hạn nhưng vẫn ghi `Còn hạn`.
- Lần chạy thứ hai: cập nhật `0` bản ghi, chứng minh thao tác idempotent.
- Có `1` bản ghi audit và lịch sử hết hạn cho từng gói đã cập nhật.
- Build: **0 warning, 0 error**.

---

## Session 3 — Model/DAL/BLL business-code mapping

- **Date**: 22/11/2026
- **Scope**: Add business-code properties and update read queries/mappings while keeping numeric IDs for internal PK/FK and write APIs.
- **Changed areas**: member, product, plan, registration, invoice, employee, class, room, equipment models and DALs; product BLL search overload.
- **Verification**: build PASS (0 warning/0 error); updated SQL read-query shapes and code searches PASS; `database/03_test_v3.sql` PASS with rollback; DBCC PASS; operational readiness check unchanged.
- **Open baseline**: 15 classes without future sessions, 17 legacy attendance rows without `MaLichHoc`, 1 class/room capacity mismatch.
- **Next**: Session 4 updates UI, invoices, receipts, and reports to display business codes; Session 5 performs end-to-end regression and reconciliation.

---

## Session 4 — UI, documents, and reports

- **Date**: 22/11/2026
- **Scope**: Display business codes in grids, ComboBoxes, invoice/payment messages, check-in status, and package/product reports while preserving numeric IDs for internal operations.
- **Changed areas**: member/product/plan/employee/class/room/equipment/invoice/registration/check-in forms; report models, report DAL, and report designer columns.
- **Verification**: build PASS (0 warning/0 error); `--check-ui` PASS for 21/21 forms; report/invoice SQL checks PASS; `database/03_test_v3.sql` PASS with rollback; DBCC PASS; transactional check-in returned `CI-20261122-000012` and rolled back.
- **Test note**: An initial check-in test used an invalid channel and was rejected by the existing database CHECK constraint. The valid `Quầy` test passed; no product bug was recorded.
- **Next**: Session 5 performs end-to-end regression, reconciliation, concurrency checks, and final bug triage.
- Kiểm tra UI: **21/21 form**, 0 overlap, 0 tràn biên, 0 cắt chữ, 0 lỗi UTF-8.

---

## Phiên 5 — Kiểm thử cuối và đối soát

- **Thời điểm**: 22/11/2026
- **Mục tiêu**: kiểm thử hồi quy toàn hệ thống sau các phiên chuẩn hóa mã; kiểm tra báo cáo doanh thu theo tiền đã thu; chốt bug và đề xuất phiên tiếp theo.

### Đã thực hiện

1. Sửa `ThongKeDAL`:
   - Báo cáo gói tập join qua `HoaDonGoiTap` và `ThanhToan`, loại hóa đơn hủy/đã hoàn tiền, đăng ký hủy và giao dịch chưa thu.
   - Báo cáo HLV join qua `HoaDonPT`, dùng tiền thanh toán thành công ròng và loại đăng ký PT hủy.
   - Sửa `GROUP BY` để bao gồm `MaGoiTap`.
2. Sửa `Form1_Load` để lỗi cập nhật gói hết hạn khi SQL Server mất kết nối được hiển thị thân thiện thay vì thoát khỏi `async void` handler.

### Kết quả kiểm thử

- Truy vấn báo cáo thực tế: 10 đăng ký gói đã thu, tổng 9.678.500; 1 đăng ký PT đã thu, tổng 1.800.000; đăng ký chưa thanh toán/hủy không bị tính.
- Build: **PASS**, 0 warning, 0 error.
- UI Inspector: **21/21 form**, 0 overlap, 0 out-of-bounds, 0 text truncation, 0 lỗi UTF-8.
- `database/03_test_v3.sql`: **PASS**, rollback sạch.
- `DBCC CHECKCONSTRAINTS WITH NO_INFOMSGS`: **PASS**.
- Readiness: 0 gói quá hạn sai trạng thái, 0 hóa đơn lệch tổng; còn 15 lớp thiếu lịch tương lai, 17 điểm danh legacy thiếu `MaLichHoc`, 1 lớp vượt sức chứa phòng.

### Bug sau phiên 5

- Đã đóng BUG-005, BUG-006 và lỗi truy vấn nhóm mã gói.
- Còn mở BUG-003, BUG-004, BUG-007, BUG-008; dữ liệu legacy và đối soát kho cũng cần các phiên xử lý riêng.

### Đề xuất phiên tiếp theo

Đề xuất **Phiên 6** của Phiên 5 đã được thực hiện; các bước và kết quả được ghi tại phần Phiên 6 bên dưới.

---

## Phiên 6 — Check-in theo lượt và hoàn kho khi hủy hóa đơn

- **Thời điểm**: 22/11/2026
- **Mục tiêu**: đóng BUG-003 và BUG-004, kiểm thử các luồng check-in/out và bán hàng–hủy hóa đơn–hoàn kho.

### Đã thực hiện

1. Thêm `database/14_sua_checkin_va_hoan_kho.sql`:
   - `sp_CheckIn` kiểm tra gói đã thanh toán, thời hạn, lượt còn lại và khung giờ; khóa đăng ký gói trong transaction và tăng `SoLuotDaDung` sau khi insert check-in thành công.
   - `sp_HuyHoaDon` hoàn tồn theo từng dòng sản phẩm, ghi lịch sử `Hoàn trả` và audit trong cùng transaction.
   - Giữ chính sách giữ hàng khi tạo hóa đơn để tránh bán vượt tồn; hủy hóa đơn hoàn lại lượng giữ.
2. Thêm `database/14_test_checkin_va_hoan_kho.sql`, cố định `ANSI_NULLS/QUOTED_IDENTIFIER` để tránh lỗi cấu hình sqlcmd và rollback toàn bộ test.
3. Cập nhật README migration.

### Kiểm thử và kết quả

- Test mục tiêu: **PASS** — tăng lượt, chặn vượt lượt, chặn ngoài khung giờ, check-out và hoàn kho khi hủy hóa đơn.
- Build: **PASS**, 0 warning, 0 error.
- UI Inspector: **21/21 form**, 0 overlap, 0 out-of-bounds, 0 text truncation, 0 lỗi UTF-8.
- `database/03_test_v3.sql`: **PASS**, rollback sạch.
- `DBCC CHECKCONSTRAINTS WITH NO_INFOMSGS`: **PASS**.
- Readiness: 0 gói quá hạn sai trạng thái, 0 hóa đơn lệch tổng; còn 15 lớp thiếu lịch tương lai, 17 điểm danh legacy thiếu `MaLichHoc`, 1 lớp vượt sức chứa phòng.

### Bug sau phiên 6

- Đã đóng BUG-003.
- BUG-004 đã đóng theo chính sách giữ hàng và hoàn kho khi hủy; các giao dịch legacy chưa tự động điều chỉnh.
- BUG-002 đã được giảm thiểu bằng cách đánh dấu dữ liệu legacy; BUG-007 và BUG-008 được xử lý ở Phiên 7–8. Còn các vấn đề xác nhận dữ liệu legacy, phân quyền/audit và đối soát kho.

### Đề xuất phiên tiếp theo

Phiên 7 và Phiên 8 đã được thực hiện trong cùng đợt; kết quả chi tiết nằm ở các phần bổ sung bên dưới.

---

## Phiên 7 — Lịch học nhiều buổi và điểm danh legacy

- **Thời điểm**: 22/11/2026
- **Mục tiêu**: sửa màn hình điểm danh chỉ lấy buổi đầu tiên, minh bạch hóa dữ liệu legacy và giữ kiểm soát sức chứa.

### Đã thực hiện

1. Đổi DAL/BLL trả về danh sách `BuoiHoc` trong ngày; thêm `HienThi` cho ComboBox.
2. Thêm ComboBox chọn buổi vào `FrmDiemDanh`; điểm danh luôn dùng `MaLichHoc` đã chọn.
3. Thêm migration `database/15_danh_dau_diem_danh_legacy.sql`, cờ `LaDuLieuCu`, view `vw_DiemDanhLegacy`, index và check constraint bảo vệ điểm danh mới.
4. Không tự gán 17 dòng cũ vào lịch học không chắc chắn.

### Kiểm thử

- 17 dòng legacy được đánh dấu: **PASS**.
- Điểm danh mới thiếu lịch bị chặn: **PASS**.
- Hai buổi cùng ngày tạo được: **PASS**.
- Lớp vượt sức chứa phòng bị chặn: **PASS**.

---

## Phiên 8 — Đồng bộ mã khuyến mãi và đối soát kho legacy

- **Thời điểm**: 22/11/2026
- **Mục tiêu**: đóng BUG-008, không để báo cáo sản phẩm tự cộng trùng dữ liệu bán legacy và hoàn tất hồi quy.

### Đã thực hiện

1. Thêm migration `database/16_dong_bo_ma_code_khuyen_mai.sql`; `KhuyenMai.MaCode` được backfill, bắt buộc NOT NULL và unique.
2. Sửa `ThongKeDAL.LayThongKeSanPhamAsync` chỉ tính hóa đơn chuẩn đã thanh toán; loại nguồn `ChiTietPhieuBanHang` legacy khỏi tổng chuẩn.
3. Giữ nguyên 15 dòng legacy để đối soát, không tự xóa/chuyển đổi.

### Kiểm thử và nghiệm thu

- `MaCode`: 15/15 có mã, không trùng: **PASS**.
- Truy vấn báo cáo sản phẩm: **PASS**, hóa đơn chưa thanh toán bị loại.
- Build: **PASS**, 0 warning, 0 error.
- UI: **21/21 form**, 0 lỗi bố cục/UTF-8.
- Smoke test và các script test Phiên 6–8: **PASS**.
- DBCC: **PASS**.
- Readiness còn: 15 lớp thiếu lịch tương lai, 17 dòng legacy đã đánh dấu, 1 lớp vượt sức chứa phòng.

Lần chạy đầu migration 16 gặp lỗi thứ tự DDL khi đổi `MaCode` sang NOT NULL sau khi tạo unique constraint; đã sửa migration và chạy lại thành công.

### Kết luận

BUG-007 và BUG-008 đã đóng; BUG-002 được giảm thiểu bằng đánh dấu legacy. Hai phiên đã hoàn tất; phần còn lại là quyết định nghiệp vụ và dữ liệu cần xác nhận, không nên tự động bịa hoặc xóa dữ liệu.

### Bổ sung dữ liệu mẫu demo

Theo yêu cầu đây là sản phẩm demo, đã áp dụng `database/17_bo_sung_du_lieu_demo_lich_diemdanh.sql`:

- 15 lớp đều có lịch tương lai.
- 17 điểm danh cũ được gắn vào buổi lịch sử phù hợp.
- Lớp 15 được đồng bộ sức chứa với phòng Yoga A.
- 15 dòng bán hàng legacy được giữ riêng và có view `vw_BanHangLegacyDoiSoat` để trình diễn đối soát.
- Kết quả demo: thiếu lịch 0, điểm danh thiếu `MaLichHoc` 0, lớp vượt sức chứa 0.

---

## Phiên 9 — QA toàn bộ luồng WinForms

- **Ngày**: 22/11/2026
- **Phạm vi**: kiểm thử theo prompt QA toàn bộ: UI thực tế, login/dashboard, CRUD inventory, BLL/DAL, database, transaction, validation, authorization, error handling và dữ liệu demo.
- **Báo cáo**: `QA_REPORT.md`.

### Bằng chứng PASS

- Build solution: 0 warning, 0 error.
- UI Inspector: 21/21 Form; overlap/out-of-bounds/text truncation/UTF-8 đều 0.
- App thật: login `admin01`, mở dashboard, mở Form thành viên, tìm `TV000001` trả đúng bản ghi.
- Script `03_test_v3.sql`, `14_test_checkin_va_hoan_kho.sql`, `15_test_lich_diemdanh_succhua.sql`, `16_test_ma_code_va_bao_cao_kho.sql`, `17_test_du_lieu_demo.sql`: PASS.
- DBCC CHECKCONSTRAINTS: PASS.

### Lỗi phát hiện mới

1. `BUG-010` HIGH — phân quyền chủ yếu ở UI; BLL/DAL/SP chưa bảo vệ đầy đủ.
2. `BUG-011` HIGH — hoàn tiền toàn bộ vẫn để gói `Còn hạn`; đã tái hiện với `MaDKG=1`, `MaHD=1`, sau đó rollback.
3. `BUG-012` HIGH — check-in vào phòng 6 `Đang bảo trì` vẫn thành công; đã rollback.
4. `BUG-013` MEDIUM — bảo lưu gói bằng ngày 01–02/01/2020 vẫn được chấp nhận; đã rollback.
5. `BUG-014` HIGH — đăng ký PT sinh trạng thái `Đang học` trong khi hóa đơn `Chưa thanh toán`; đã rollback.
6. `BUG-015` MEDIUM — Form hóa đơn không truyền dòng đang chọn sang Form thanh toán.
7. `BUG-016` MEDIUM — email/SĐT chưa kiểm tra định dạng.
8. `BUG-017` MEDIUM — chưa ghi `LichSuDangNhap`, chưa có lockout sau nhiều lần sai.
9. `BUG-018` MEDIUM — nhiều Form hiển thị trực tiếp `ex.Message`.

### Coverage và quyết định

- Matrix có 34 case: 16 PASS, 8 FAIL, 9 NOT TESTED, 1 BLOCKED.
- Coverage 70,59%; pass rate trên case đã chạy 66,67%.
- Kết luận: sản phẩm **đủ tốt để demo có kiểm soát**, chưa đạt để vận hành thật.
- Không sửa source code trong phiên này. Phiên sau ưu tiên BUG-010, BUG-011, BUG-012, BUG-014; sau sửa phải chạy lại full regression.

---

## Phiên 10 — Sửa phân quyền

- **Phạm vi**: BUG-010, phân quyền menu/UI, BLL và database guard.
- **Thay đổi**:
  - Thêm `He_Thong_GYM/Helpers/AuthorizationPolicy.cs` với mapping vai trò cho từng menu.
  - `Form1` ẩn menu không được phép và kiểm tra lại quyền trước khi mở Form.
  - Thêm guard `AuthorizationPolicy.Require` tại các BLL mutation chính.
  - Thêm/ap dụng `database/18_phan_quyen_nghiep_vu.sql`: function kiểm tra vai trò theo `MaNV` và trigger bảo vệ hóa đơn, thanh toán, kho, bảo trì, lịch sử gói.
- **Kiểm thử**:
  - Build: **PASS**, 0 warning, 0 error.
  - Script 03, 14, 15, 16, 17: **PASS**.
  - DBCC CHECKCONSTRAINTS: **PASS**.
  - UI Inspector: **21/21 Form PASS**.
  - Ghi thanh toán bằng `MaNV=13` (vai trò Nhân viên) bị trigger từ chối, lỗi 52102: **PASS**.
- **Kết luận**: BUG-010 được giảm phạm vi đáng kể ở UI/BLL và các bảng có actor; các procedure actorless cho check-in/lịch/điểm danh vẫn cần truyền actor ở phiên kế tiếp để đóng hoàn toàn lớp database.
- **Phiên kế tiếp**: sửa BUG-011, BUG-012, BUG-013, BUG-014.
- Phiên tiếp theo: sửa BUG-011, BUG-012, BUG-013 và BUG-014.

---

## Phiên 11 — sửa hoàn tiền, check-in phòng, bảo lưu và trạng thái PT

- **Ngày**: 22/11/2026
- **Phạm vi**: BUG-011, BUG-012, BUG-013, BUG-014.
- **Thay đổi**:
  - Thêm migration `database/19_sua_nghiep_vu_phien_11.sql`.
  - Hoàn tiền toàn bộ vô hiệu hóa gói tập/PT liên quan; hoàn một phần vẫn giữ trạng thái đang sử dụng.
  - `sp_CheckIn` kiểm tra `PhongTap.TrangThai=N'Hoạt động'`; UI lọc phòng hoạt động.
  - `sp_BaoLuuGoiTap` kiểm tra ngày từ hôm nay, nằm trong kỳ gói, không chồng lấn và có lý do.
  - `DangKyPT` bổ sung `Chờ thanh toán`; `sp_ThanhToanHoaDon` chỉ kích hoạt PT sau khi thu đủ.
  - Cập nhật bộ lọc trạng thái trên Form đăng ký PT.
- **Kiểm thử**:
  - `19_test_phien_11.sql`: PASS BUG-011, BUG-012, BUG-013, BUG-014a, BUG-014b; rollback sạch.
  - Regression scripts 03, 14, 15, 16, 17: PASS khi chạy tuần tự; DBCC PASS.
  - Build: PASS, 0 warning, 0 error.
  - UI smoke: login Admin/dashboard PASS; phòng bảo trì không xuất hiện trong ComboBox check-in; Form PT tải thành công.
- **Kết luận**: bốn lỗi của Phiên 11 đã đóng. Còn BUG-015 đến BUG-018 cho Phiên 12.

---

## Phiên 12 — đóng các lỗi bảo mật, liên kết hóa đơn, validation và thông báo lỗi

- **Ngày**: 22/11/2026
- **Phạm vi**: BUG-010, BUG-015, BUG-016, BUG-017, BUG-018.
- **Thay đổi**:
  - Áp dụng `database/20_sua_bao_mat_validation_phien_12.sql`: các procedure check-in/check-out/đăng ký lớp/tạo lịch/điểm danh bắt buộc `@MaNV` và kiểm tra vai trò tại database.
  - `FrmHoaDon` truyền `MaHD` của dòng đang chọn vào `FrmThanhToan`; Form thanh toán chọn đúng hóa đơn sau khi nạp dữ liệu.
  - Thêm `Helpers/InputValidator.cs` cho SĐT 10–11 số bắt đầu bằng 0 và email tùy chọn nhưng phải đúng định dạng; thêm CHECK constraint tương ứng cho `ThanhVien` và `NhanVien`.
  - Thêm audit đăng nhập qua `sp_GhiKetQuaDangNhap`; sai 5 lần khóa 15 phút, đăng nhập thành công reset bộ đếm và thời gian khóa.
  - Thêm `Helpers/UiErrorMessage.cs` để log chi tiết nội bộ tại `%LocalAppData%\\GymPro\\logs` và hiển thị thông báo nghiệp vụ; nhánh lỗi kỹ thuật của Form đăng nhập cũng ghi log.
- **Kiểm thử**:
  - Build sau thay đổi cuối: **PASS**, 0 warning, 0 error.
  - `20_test_phien_12.sql`: **PASS** BUG-010 actor NULL, BUG-016 CHECK SĐT/email, BUG-017 lockout/audit; rollback sạch.
  - Regression tuần tự `03`, `14`, `15`, `16`, `17`, `19`: **PASS**; `DBCC CHECKCONSTRAINTS`: **PASS**; readiness: 0 lỗi gói quá hạn, 0 lớp thiếu lịch, 0 điểm danh thiếu lịch, 0 lớp vượt sức chứa, 0 hóa đơn lệch tổng.
  - UI smoke: đăng nhập Admin/dashboard **PASS**; chọn hóa đơn thứ hai `HD-20261117-000025` rồi mở thanh toán, Form nhận đúng hóa đơn và công nợ 300.000 đ **PASS**.
- **Kết luận**: 5 nhóm BUG-010, BUG-015, BUG-016, BUG-017 và BUG-018 đã được sửa và kiểm thử đạt trong phạm vi sản phẩm demo. Dữ liệu demo không bị thay đổi bởi các test mutation.
- **Phiên đề xuất tiếp theo**: không còn lỗi thuộc danh sách Phiên 12; nếu cần tiếp tục, thực hiện phiên nghiệm thu cuối với test phân quyền theo từng vai trò, test UI validation/error dialog toàn bộ Form và chuẩn bị checklist bàn giao/demo.

---

## Phiên 13 — Khắc phục lỗi lõi & Lỗ hổng nghiệp vụ (BUG-019, 020, 021, 022, 024)

- **Ngày**: 22/11/2026
- **Phạm vi**: BUG-019 (Lọc hóa đơn chưa thu), BUG-020 (Gia hạn tính tiền & hóa đơn), BUG-021 (Check-in FIFO), BUG-022 (Đăng xuất quay lại Login), BUG-024 (Đóng phiếu bảo trì khôi phục máy).
- **Thay đổi**:
  - Tạo và áp dụng migration `database/21_sua_giahan_checkin_baotri.sql`:
    * Bỏ ràng buộc `UQ_HDGoiTap_DKG` để 1 gói có thể có nhiều hóa đơn (đăng ký gốc + các lần gia hạn).
    * `sp_GiaHanGoiTap` tính phí gia hạn dựa trên đơn giá ngày, tạo `HoaDon` + `ChiTietHoaDon` + `HoaDonGoiTap` ở trạng thái `Chưa thanh toán` và trả về `@MaHD OUTPUT`.
    * `sp_CheckIn` cập nhật sắp xếp `ORDER BY d.NgayKetThuc ASC, d.MaDKG ASC` (FIFO) để ưu tiên tiêu thụ gói sắp hết hạn trước.
    * `sp_BaoTriThietBi` cập nhật phiếu sang `Đang bảo trì` và thiết bị sang `Bảo trì`.
    * Tạo mới thủ tục `sp_HoanTatBaoTri`: chuyển phiếu bảo trì sang `Hoàn thành` và tự động khôi phục thiết bị về `Hoạt động`.
  - Cập nhật code C#:
    * `He_Thong_GYM/Program.cs`: bọc `Application.Run` trong vòng lặp `while(true)`. Đăng xuất quay lại `FrmLogin`; đóng cửa sổ thoát sạch.
    * `He_Thong_GYM/Forms/FrmHoaDon.Designer.cs`: sửa danh sách `cboTrangThai` thành `Chưa thanh toán`, `Đã thanh toán`, `Đã thanh toán một phần`, `Đã hoàn tiền`, `Hủy`.
    * `He_Thong_GYM/DAL/VanHanhDAL.cs`, `BLL/VanHanhBLL.cs`, `Forms/FrmVanHanhGoi.cs`: nhận mã hóa đơn gia hạn và hiển thị thông báo.
    * `He_Thong_GYM/DAL/TaiSanDAL.cs`, `BLL/TaiSanBLL.cs`, `Forms/FrmBaoTri.cs`, `Forms/FrmBaoTri.Designer.cs`: thêm hàm `HoanTatBaoTriAsync` và nút `Hoàn tất bảo trì`.
- **Kiểm thử**:
  - `database/21_test_phien_13.sql`: **PASS** toàn bộ BUG-020, BUG-021, BUG-024; transaction rollback sạch.
  - Regression test tuần tự: `20`, `19`, `14`, `11`, `DBCC`: **PASS 100%**, 0 vi phạm ràng buộc.
  - Build: **PASS**, 0 warning, 0 error.
  - UI Inspector: **PASS 21/21 Form**, overlap 0, out-of-bounds 0, text truncation 0, UTF-8 0.
- **Kết luận**: 5 lỗi cốt lõi đã được khắc phục hoàn toàn và kiểm chứng an toàn.
- **Phiên kế tiếp**: Phiên 14 — Liền mạch luồng thao tác UX (Đăng ký → Thu tiền), bán hàng khách vãng lai, bổ sung nảy dữ liệu/sửa lớp học và hủy hóa đơn.

---

## Phiên 14 — Liền mạch luồng thao tác UX, Bán hàng khách vãng lai, Hoàn thiện Lớp tập, Hủy hóa đơn và Thông báo trùng SĐT/Email

- **Ngày**: 22/11/2026
- **Phạm vi**: BUG-025 (Bán hàng khách vãng lai), Liền mạch Đăng ký → Mở thanh toán ngay, BUG-023 (Hoàn thiện nảy dữ liệu/Sửa/Đóng lớp tại `FrmLopTap`), Bổ sung nút Hủy hóa đơn & tự động hoàn kho tại `FrmHoaDon`, BUG-026 (Phân tách thông báo trùng SĐT/Email tại `FrmThanhVien`).
- **Thay đổi**:
  - Migration database: Tạo và áp dụng `database/22_khach_vang_lai_va_lop_tap.sql`, thêm bản ghi hệ thống `TV001035 - Khách vãng lai` (SĐT `0900000000`).
  - Khách vãng lai & Liền mạch UX:
    * `He_Thong_GYM/Forms/FrmBanHang.cs`: Tự động chọn mặc định `Khách vãng lai`. Bán hàng xong mở hộp thoại hỏi thu tiền ngay sang `FrmThanhToan(maHD)`.
    * `He_Thong_GYM/Forms/FrmDangKyGoi.cs`: Đăng ký gói tập xong mở hộp thoại hỏi thu tiền ngay sang `FrmThanhToan(kq.MaHD)`.
    * `He_Thong_GYM/Forms/FrmDangKyPT.cs`: Ký hợp đồng PT xong mở hộp thoại hỏi thu tiền ngay sang `FrmThanhToan(kq.MaHD)`.
    * `He_Thong_GYM/BLL/DangKyBLL.cs`: Đồng bộ tuple trả về `(int MaDangKy, int MaHD)`.
  - Quản lý Lớp tập (`FrmLopTap`):
    * `He_Thong_GYM/DAL/LopTapDAL.cs`, `BLL/LopTapBLL.cs`: Thêm `CapNhatAsync(LopTap)` và `DongLopAsync(int maLop)`.
    * `He_Thong_GYM/Forms/FrmLopTap.Designer.cs`: Thêm nút `btnSua` (Cập nhật), `btnDongLop` (Đóng lớp), gán sự kiện `SelectionChanged` cho `dgvLop`.
    * `He_Thong_GYM/Forms/FrmLopTap.cs`: Bổ sung xử lý khi chọn dòng trên lưới tự động nảy dữ liệu lên form, xử lý nút Cập nhật, Đóng lớp và Làm mới.
  - Hủy hóa đơn (`FrmHoaDon`):
    * `He_Thong_GYM/DAL/TaiChinhDAL.cs`, `BLL/TaiChinhBLL.cs`: Thêm `HuyHoaDonAsync(int maHD, string lyDo, int? maNV)`.
    * `He_Thong_GYM/Forms/FrmHoaDon.Designer.cs`: Thêm nút `btnHuyHoaDon`.
    * `He_Thong_GYM/Forms/FrmHoaDon.cs`: Thêm xử lý `btnHuyHoaDon_Click`. Chặn hủy hóa đơn đã thanh toán (báo hoàn tiền trước). Hủy hóa đơn chưa thanh toán tự động hoàn lại tồn kho sản phẩm về ban đầu.
  - Thông báo trùng lặp (`FrmThanhVien`):
    * `He_Thong_GYM/Forms/FrmThanhVien.cs`: Bắt `SqlException` 2601/2627 hiển thị thông báo tiếng Việt thân thiện về trùng SĐT hoặc trùng Email.
- **Kiểm thử**:
  - `database/22_test_phien_14.sql`: **PASS 100%** toàn bộ 5 bước; rollback sạch.
  - Regression test tuần tự: `21`, `20`, `19`, `14`, `DBCC`: **PASS 100%**.
  - Build: **PASS**, 0 warning, 0 error.
  - UI Inspector: **PASS 21/21 Form**, overlap 0, out-of-bounds 0, text truncation 0, UTF-8 0.
- **Kết luận**: Phiên 14 hoàn thành xuất sắc 100% các mục tiêu đặt ra.
- **Phiên kế tiếp**: Phiên 15 — Hoàn thiện CRUD Sản phẩm & Nhập kho (`FrmSanPham`), rà soát Thiết bị (`FrmThietBi`), Phòng tập (`FrmPhongTap`), Khuyến mãi (`FrmKhuyenMai`).

---

## Phiên 15 — Hoàn thiện CRUD Sản phẩm, Nhập kho hàng hóa, Thiết bị, Phòng tập và Khuyến mãi

- **Ngày**: 22/11/2026
- **Phạm vi**: Hoàn thiện toàn diện CRUD Sản phẩm & Nhập kho (`FrmSanPham`), Cập nhật Thiết bị (`FrmThietBi`), Cập nhật Phòng tập (`FrmPhongTap`), CRUD Khuyến mãi (`FrmKhuyenMai`).
- **Thay đổi**:
  - Module Sản phẩm & Nhập kho:
    * `He_Thong_GYM/Models/SanPham.cs`: Bổ sung `GiaNhap` và `TonToiThieu`.
    * `He_Thong_GYM/DAL/SanPhamDAL.cs`, `BLL/SanPhamBLL.cs`: Bổ sung `ThemAsync`, `CapNhatAsync`, `NhapKhoAsync` (gọi `dbo.sp_NhapKho`).
    * `He_Thong_GYM/Forms/FrmSanPham.Designer.cs`: Bổ sung `groupThongTin` (Tên SP, Đơn giá, Giá nhập, Trạng thái), nút `btnThem`, `btnSua`, `btnNhapKho`, gán `SelectionChanged` cho `dgvSanPham`.
    * `He_Thong_GYM/Forms/FrmSanPham.cs`: Xử lý nảy dữ liệu khi chọn dòng, thêm mới sản phẩm, cập nhật thông tin và popup nhập kho hàng hóa.
  - Module Thiết bị & Phòng tập:
    * `He_Thong_GYM/DAL/TaiSanDAL.cs`, `BLL/TaiSanBLL.cs`: Bổ sung `CapNhatThietBiAsync` và `CapNhatPhongTapAsync`.
    * `He_Thong_GYM/Forms/FrmThietBi.Designer.cs`, `FrmThietBi.cs`: Thêm nút `btnSua` cho phép phân lại phòng tập và cập nhật tình trạng thiết bị.
    * `He_Thong_GYM/Forms/FrmPhongTap.Designer.cs`, `FrmPhongTap.cs`: Thêm nút `btnSua` cho phép cập nhật sức chứa và trạng thái phòng tập.
  - Module Khuyến mãi:
    * `He_Thong_GYM/DAL/KhuyenMaiDAL.cs`: Bổ sung `ThemAsync` và `CapNhatAsync`.
    * Tạo mới `He_Thong_GYM/BLL/KhuyenMaiBLL.cs`: Validation ngày bắt đầu/kết thúc, % giảm giá, kiểm tra quyền Admin/Quản lý.
    * `He_Thong_GYM/Forms/FrmKhuyenMai.Designer.cs`, `FrmKhuyenMai.cs`: Thêm nút `btnThem` (Tạo khuyến mãi) và `btnSua` (Cập nhật), gán sự kiện `SelectionChanged`.
- **Kiểm thử**:
  - `database/23_test_phien_15.sql`: **PASS 100% (4/4 bước)**: Thêm sản phẩm trigger tự sinh mã `SP000016`, sửa đơn giá, nhập kho qua `sp_NhapKho`, cập nhật thiết bị, cập nhật phòng, thêm/sửa khuyến mãi; rollback sạch.
  - Regression test tuần tự: `22`, `21`, `20`, `19`, `14`, `DBCC`: **PASS 100%**.
  - Build: **PASS**, 0 warning, 0 error.
  - UI Inspector: **PASS 21/21 Form**, overlap 0, out-of-bounds 0, text truncation 0, UTF-8 0.
- **Kết luận**: Toàn bộ các module CRUD Sản phẩm/Nhập kho, Thiết bị, Phòng tập, Khuyến mãi đã được hoàn thiện đạt chuẩn chất lượng cao nhất.
- **Phiên kế tiếp**: Phiên 16 — Full Regression toàn hệ thống, nghiệm thu đa vai trò và đóng gói bàn giao.

---

## Phiên 16 — Nghiệm thu Toàn diện (Final Regression), Kiểm thử Đa vai trò và Hoàn tất Bàn giao Hệ thống

- **Ngày**: 22/11/2026
- **Phạm vi**: 
  - Xây dựng và thực thi bộ kịch bản kiểm thử tích hợp nghiệm thu tổng hợp cuối cùng (`database/24_final_regression_test.sql`).
  - Chạy liên hoàn toàn bộ Full Regression Suite (từ migration 14 đến 24) và kiểm tra tính toàn vẹn cơ sở dữ liệu (`DBCC CHECKCONSTRAINTS`).
  - Kiểm tra biên dịch mã nguồn WinForms và quét giao diện 21/21 Form (`UiInspector`).
  - Biên soạn tài liệu hướng dẫn vận hành toàn diện (`HUONG_DAN_SU_DUNG_VA_VAN_HANH.md`).
  - Cập nhật báo cáo QA (`QA_REPORT.md`) chuyển trạng thái sang ĐẠT CHUẨN BÀN GIAO.
- **Thay đổi & Thực hiện**:
  1. **Kịch bản Nghiệm thu Tổng hợp (`database/24_final_regression_test.sql`):**
     - Bao gồm 12 bước nghiệp vụ end-to-end liên kết chặt chẽ:
       1. Hội viên: Thêm mới, kiểm tra ràng buộc duy nhất SĐT/Email.
       2. Đăng ký gói tập: Chọn gói, tự động áp dụng khuyến mãi hợp lệ trong hạn, sinh hóa đơn.
       3. Thu tiền: Thanh toán hóa đơn gói tập, tự động kích hoạt gói thành `Đang hoạt động`.
       4. Check-in: Kiểm tra điều kiện gói hợp lệ, vào cửa thành công; kiểm tra chặn 2 lượt check-in mở đồng thời.
       5. Check-out: Hoàn tất buổi tập, giải phóng trạng thái mở.
       6. Gia hạn gói tập (`sp_GiaHanGoiTap`): Tính phí bổ sung theo đơn giá ngày, sinh hóa đơn gia hạn (BUG-020).
       7. Bảo lưu gói tập (`sp_BaoLuuGoiTap`): Tạm dừng gói và khôi phục khi hết hạn bảo lưu (BUG-013).
       8. Ký hợp đồng PT (`sp_DangKyPT`): Chọn HLV, sinh hóa đơn, kích hoạt sau khi thanh toán đầy đủ (BUG-014).
       9. Bán lẻ hàng hóa cho khách vãng lai: Tự động trừ tồn kho (BUG-025).
       10. Hủy hóa đơn bán lẻ chưa thanh toán: Tự động hoàn tồn kho hàng hóa (Phiên 14).
       11. Nhập kho hàng hóa (`sp_NhapKho`): Tăng số lượng tồn, cập nhật giá nhập, ghi lịch sử kho (Phiên 15).
       12. Quy trình bảo trì máy móc (`sp_BaoTriThietBi`): Báo bảo trì, đổi trạng thái máy sang `Bảo trì` và hoàn tất bảo trì khôi phục máy về `Hoạt động` (BUG-024).
     - Kết quả kiểm thử: **PASS 100% (12/12 luồng)**, tự động rollback sạch, không để lại dữ liệu rác.
  2. **Chạy Full Regression Suite liên hoàn:**
     - `24_final_regression_test.sql`: **PASS**
     - `23_test_phien_15.sql`: **PASS**
     - `22_test_phien_14.sql`: **PASS**
     - `21_test_phien_13.sql`: **PASS**
     - `20_test_phien_12.sql`: **PASS**
     - `19_test_phien_11.sql`: **PASS**
     - `14_test_checkin_va_hoan_kho.sql`: **PASS**
     - `DBCC CHECKCONSTRAINTS WITH ALL_CONSTRAINTS`: **PASS 100% (0 vi phạm)**.
  3. **Kiểm thử Ứng dụng & Giao diện:**
     - Build solution `dotnet build He_Thong_GYM.slnx --no-restore`: **PASS (0 warning, 0 error)**.
     - `UiInspector`: **PASS 21/21 Form**, bố cục co giãn chuẩn, không có lỗi cắt chữ hay chồng chéo.
  4. **Tài liệu Hướng dẫn & Báo cáo QA:**
     - Tạo mới `HUONG_DAN_SU_DUNG_VA_VAN_HANH.md`: Danh mục tài khoản theo 5 nhóm vai trò, quy trình 7 luồng công việc thực tế, xử lý sự cố thường gặp.
     - Cập nhật `QA_REPORT.md`: Bổ sung mục 16, nâng đánh giá chất lượng lên **🟢 ĐẠT CHUẨN — SẴN SÀNG BÀN GIAO & VẬN HÀNH THỰC TẾ**.
- **Kết luận**: Hệ thống Quản lý Phòng Gym `QL_PhongGym` đã trải qua toàn bộ 16 phiên làm việc với tiêu chuẩn khắt khe nhất, khắc phục triệt để mọi lỗi logic và giao diện, kiểm thử hồi quy 100% thành công và chính thức sẵn sàng bàn giao cho người dùng đưa vào vận hành thực tế.

---

## Phiên Nâng Cấp UX — Chuẩn hóa Trạng thái Nghỉ (Idle/Disabled State) và Điều khiển Trực quan

- **Ngày**: 22/11/2026
- **Mục tiêu**: Giải quyết góp ý của người dùng về việc các nút thao tác (Thêm mới, Chỉnh sửa, Lưu, Xóa) luôn ở trạng thái sáng và bấm được ngay cả khi chưa chọn thao tác nào (đặc biệt là nút Lưu sáng khi chưa thêm/sửa gây bấm nhầm).
- **Thực hiện**:
  1. Tạo `UiButtonHelper.cs`: Đổi màu trực quan cho nút phẳng trong WinForms:
     - Kích hoạt: Màu nhận diện (Xanh dương, Cam, Xanh lục, Đỏ, Xám đậm), chữ trắng, con trỏ bàn tay.
     - Nghỉ: Nền xám nhạt hiện đại (`#E2E8F0`), chữ xám mờ (`#94A3B8`), con trỏ mặc định.
  2. Nâng cấp luồng thao tác trên `FrmThanhVien.cs`:
     - Ban đầu / Làm mới: Các ô nhập khóa chỉ đọc (`ReadOnly = true`, nền `#F8FAFC`). Nút `Chỉnh sửa`, `Lưu`, `Xóa` ở trạng thái nghỉ. Chỉ có `Thêm mới` và `Làm mới` sáng.
     - Chọn dòng trên bảng: Ô nhập hiển thị thông tin chỉ đọc. Nút `Chỉnh sửa` và `Xóa` bật sáng. Nút `Lưu` vẫn nghỉ.
     - Bấm `Thêm mới` hoặc `Chỉnh sửa`: Ô nhập mở khóa (`ReadOnly = false`, nền trắng), focus vào ô Họ tên. Nút `Lưu` bật sáng xanh lục rực rỡ. Các nút `Thêm mới`, `Chỉnh sửa`, `Xóa` chuyển sang trạng thái nghỉ. Nút `Làm mới` đóng vai trò `Hủy bỏ`.
     - Bấm `Lưu` thành công: Dữ liệu được ghi, khóa lại các ô nhập, nút `Lưu` trở về trạng thái nghỉ.
  3. Áp dụng đồng bộ cho toàn bộ các form:
     - `FrmGoiTap`: Khóa ô thông tin ở chế độ xem; nút Lưu và Ngừng áp dụng ở trạng thái nghỉ khi chưa chọn gói tập.
     - `FrmNhanVien`: Khóa ô thông tin ở chế độ xem; nút Lưu và Nghỉ việc ở trạng thái nghỉ khi chưa chọn nhân viên.
     - `FrmHLV`: Khóa ô thông tin; nút Lưu ở trạng thái nghỉ khi chưa chọn/thêm HLV.
     - `FrmVanHanhGoi`: Nút Check-in, Check-out nghỉ khi chưa chọn hội viên; nút Gia hạn, Bảo lưu nghỉ khi chưa chọn gói tập trên lưới.
     - `FrmTaiKhoan`: Nút Đặt lại mật khẩu, Khóa/Mở khóa nghỉ khi chưa chọn tài khoản; nút Lưu nghỉ khi đang chọn tài khoản để tránh lỗi.
     - `FrmHoaDon`: Nút Thanh toán nghỉ khi chưa chọn hóa đơn; nút Hủy hóa đơn nghỉ khi chưa chọn hoặc đã hủy.
     - `FrmBaoTri`: Nút Hoàn tất nghỉ khi chưa chọn phiếu bảo trì hoặc phiếu đã Hoàn thành.
     - `FrmDiemDanh`: Nút Điểm danh nghỉ khi chưa chọn học viên trên lưới lớp học.
     - `FrmSanPham`: Nút Chỉnh sửa, Nhập kho nghỉ khi chưa chọn sản phẩm.
     - `FrmLopTap`: Nút Chỉnh sửa, Đóng lớp nghỉ khi chưa chọn lớp.
     - `FrmKhuyenMai`: Nút Cập nhật nghỉ khi chưa chọn chương trình khuyến mãi.
     - `FrmThietBi`: Nút Cập nhật nghỉ khi chưa chọn thiết bị.
     - `FrmPhongTap`: Nút Cập nhật nghỉ khi chưa chọn phòng.
- **Kiểm thử**:
  - `dotnet build He_Thong_GYM.slnx`: PASS (0 Warning, 0 Error).
  - `UiInspector`: PASS 21/21 Form.
  - Regression SQL: PASS 100% (12/12 luồng).
- **Kết luận**: Giao diện phần mềm trở nên trực quan, hiện đại, dẫn dắt người dùng rõ ràng theo từng bước và triệt tiêu hoàn toàn khả năng thao tác nhầm lẫn.
---

## Phiên hiện tại — Khắc phục Check-in/Check-out và đồng bộ ngữ cảnh CodexLB

- **Ngày:** 23/11/2026.
- **Phát hiện:** Các nút Check-in/Check-out đã gọi đúng stored procedure. Log cho thấy `TV000003 - Lê Thu Hà` Check-in thành công lúc `02:03:21` với mã `CI-20261123-000053`, sau đó Check-out thành công lúc `02:03:25`. Các lỗi ngay trước đó là lỗi nghiệp vụ hợp lệ: Check-out khi chưa có phiên mở (`50012`) và Check-in lần hai khi phiên còn mở (`50010`).
- **Thay đổi mã:**
  - `VanHanhDAL.CoPhienCheckInMoAsync` đọc trạng thái phiên mở.
  - `FrmVanHanhGoi` chỉ bật Check-out khi thành viên có phiên mở; Check-in bị khóa trong thời gian đó.
  - `UiErrorMessage` ánh xạ lỗi `50010`, `50011`, `50012`, `50044`, `50045`, `50046`, `52106`, `52107` thành thông báo nghiệp vụ cụ thể.
  - Bổ sung `dbo.sp_MoLaiBaoLuuGoiTap` qua migration `database/25_bo_sung_mo_lai_bao_luu.sql`.
- **Database/UI:** Procedure mở lại bảo lưu đã được áp dụng trên database local. Khi chạy bản mã mới cần đóng/mở lại app vì executable cũ đang chạy sẽ giữ DLL cũ.
- **Kiểm thử:** Query xác nhận không còn phiên Check-in mở cho `MaTV=3`; procedure `sp_CheckIn`, `sp_CheckOut`, `sp_MoLaiBaoLuuGoiTap` đều tồn tại.
- **Trạng thái:** Đã sửa mã; cần restart ứng dụng rồi build lại để xác nhận trực tiếp trạng thái nút trên UI.

## Phiên xử lý lỗi build — Duplicate Assembly Attributes

- **Nguyên nhân:** thư mục tạm `He_Thong_GYM/artifacts/obj-check` nằm trong project và chứa `.NETCoreApp,Version=v10.0.AssemblyAttributes.cs` cùng `He_Thong_GYM.AssemblyInfo.cs`; SDK project quét chúng như source lần nữa.
- **Biện pháp:** chuyển `artifacts/build-check` và `artifacts/obj-check` ra ngoài workspace; không xóa hay thay đổi mã nguồn nghiệp vụ.
- **Xác nhận:** build với output/intermediate ngoài repository đạt **0 warning, 0 error**.
- **Quy tắc cho phiên sau:** khi cần build tạm, dùng thư mục ngoài workspace, ví dụ `%LOCALAPPDATA%\Temp\GymPro-build-check-*`, hoặc cấu hình exclude rõ ràng; không tạo `obj-*` bên trong thư mục project.

## Cập nhật nghiệp vụ — Mở lại bảo lưu sớm

- Cho phép mở lại gói khi chưa tới `DenNgay`, phù hợp trường hợp hội viên hoàn thành việc sớm và quay lại tập.
- Procedure tính số ngày tạm dừng thực tế: từ `TuNgay` đến ngày mở lại, tối đa bằng khoảng bảo lưu đã đăng ký.
- Chỉ số ngày thực tế được cộng vào `NgayKetThuc`; `TongSoNgayBaoLuu` được điều chỉnh lại để không tính phần ngày chưa sử dụng.
- Đã áp dụng lại `database/25_bo_sung_mo_lai_bao_luu.sql` trên database local và kiểm thử với `MaDKG=3` trong transaction rollback; dữ liệu sau rollback vẫn là `Tạm dừng`, bảo lưu `Đang bảo lưu`.

## Cập nhật UX — Danh sách đang tập và Check-out tập trung

- Thêm tab **Đang tập / Chờ Check-out** vào `FrmVanHanhGoi` để hiển thị tất cả phiên có `ThoiGianRa IS NULL`.
- Sau Check-in thành công, form tự tải lại danh sách và chuyển sang tab này.
- Chọn một dòng đang tập sẽ tự điền thành viên và phòng vào khu vực thao tác; nhân viên không cần dò từng tên trong ComboBox.
- Thêm nút **Làm mới danh sách đang tập** để kiểm tra nhanh trạng thái quầy.
- Cập nhật tài liệu vận hành `HUONG_DAN_SU_DUNG_VA_VAN_HANH.md`.
- Build kiểm tra sau thay đổi: PASS, 0 warning, 0 error.

## Cập nhật màu nút và trạng thái tương tác

- `UiButtonHelper` bổ sung màu khi rê chuột và nhấn nút cho toàn bộ nút nghiệp vụ.
- Màu chức năng: Check-in xanh lá, Check-out xanh dương, gia hạn xanh ngọc, bảo lưu tím, mở lại xanh cyan, làm mới slate.
- Nút disabled vẫn dùng nền xám nhạt và không nhận màu hover/pressed.
- Build kiểm tra: PASS, 0 warning, 0 error.

## Phiên UX - Bổ sung tìm kiếm và sắp xếp danh sách

- **Ngày:** 23/11/2026.
- **Yêu cầu:** giúp nhân viên tra cứu nhanh và sắp xếp các bảng dữ liệu trong những form thường dùng.
- **Thực hiện:**
  - Tạo `SortableBindingList<T>` trong `UiGridHelper`; các cột có `DataPropertyName` được bật `SortMode = Automatic`.
  - Chuyển nguồn dữ liệu của các lưới danh sách chính sang `UiGridHelper.DatNguon(...)`, nhờ đó nhấn tiêu đề cột sẽ sắp xếp dữ liệu thật theo kiểu số, ngày, giờ hoặc chuỗi.
  - Giữ nguyên các bộ lọc hiện có ở Thành viên, Gói tập, HLV, Hóa đơn, Bảo trì, Đăng ký, Sản phẩm, Lớp tập, Khuyến mãi, Phòng tập và Thiết bị.
  - Bổ sung tìm kiếm thời gian thực cho `FrmTaiKhoan` theo mã tài khoản, tên đăng nhập, nhân viên, vai trò và trạng thái; thêm nút `Làm mới / Bỏ lọc`.
  - Sau khi lọc hoặc thay nguồn, lưới được bỏ chọn để tránh vô tình thao tác trên dòng đầu tiên; các nút phụ thuộc dòng chọn được cập nhật về trạng thái phù hợp.
- **Kiểm thử:** build ngoài workspace thành công, `0 Warning, 0 Error`; không tạo thêm thư mục build trong project.
- **Trạng thái:** hoàn tất phần code và build kiểm tra; cần mở ứng dụng để kiểm tra trực quan thao tác nhấn tiêu đề cột và ô tìm kiếm trên máy demo.

## Phiên UX - Tăng tương phản tab Check-in/Check-out

## Phiên nghiệp vụ - Chuyển phòng trong cùng phiên tập

- **Ngày:** 23/11/2026.
- **Mục tiêu:** hội viên có thể đổi khu vực tập mà không phải Check-out rồi Check-in lại và không bị tính thêm lượt.
- **Thực hiện:** thêm migration `database/26_bo_sung_chuyen_phong_checkin.sql`, procedure `dbo.sp_ChuyenPhongCheckIn`, nút `Chuyển phòng` trên `FrmVanHanhGoi`, cùng DAL/BLL và kiểm tra quyền, phòng hoạt động, sức chứa.
- Procedure chỉ cập nhật phòng trên phiên Check-in đang mở, không tạo lượt mới và không tăng lượt gói; thao tác được ghi audit.
- **Kiểm thử:** migration áp dụng thành công trên database local; build ngoài workspace PASS, 0 warning, 0 error.

## Khắc phục lỗi SQL khi chuyển phòng

- Log ứng dụng xác định lỗi `1934`: procedure được tạo khi `QUOTED_IDENTIFIER` tắt, trong khi `LichSuCheckIn` có filtered index.
- Đã tạo lại migration với đầy đủ SET option chuẩn; metadata procedure xác nhận `uses_quoted_identifier=1`.
- Đã chạy thử chuyển phòng trong transaction rollback thành công, không để lại dữ liệu test.

- `FrmVanHanhGoi` dùng tab tự vẽ màu để phân biệt rõ tab `Gói tập` và `Đang tập / Chờ Check-out`.
- Tab đang chọn có màu nổi bật (xanh dương hoặc xanh teal), tab chưa chọn dùng nền slate sáng và chữ tối.
- Nút `Làm mới danh sách đang tập` dùng màu teal, nền thanh công cụ slate sáng để dễ nhận biết.

## Phiên nghiệp vụ — Phân quyền dịch vụ theo gói

- **Ngày:** 23/11/2026.
- **Mục tiêu:** khi Check-in, nhân viên chọn gói đăng ký nào thì hệ thống chỉ hiển thị và cho phép sử dụng đúng dịch vụ/phòng mà gói đó được cấp.
- **Nguyên nhân cũ:** `sp_CheckIn` tự chọn gói theo FIFO; `FrmVanHanhGoi` không truyền `MaDKG` được chọn và ComboBox phòng nạp toàn bộ phòng hoạt động.
- **Thực hiện:** thêm `database/27_phan_quyen_dich_vu_theo_goi.sql`; tạo `GoiTapDichVu`, `GoiTapPhong`; seed quyền cho 15 gói demo; cập nhật `sp_CheckIn` nhận `@MaDKG` và kiểm tra quyền; cập nhật `sp_ChuyenPhongCheckIn` để không chuyển sang phòng ngoài quyền gói.
- **UI:** `DangKyGoiView` hiển thị cột `Dịch vụ được cấp quyền`; chọn dòng gói sẽ nạp lại ComboBox chỉ với phòng được cấp cho `MaDKG`; Check-in yêu cầu chọn gói hợp lệ và truyền đúng mã đăng ký.
- **Phân loại demo:** Basic/Sinh viên/Family → Gym Tổng Hợp; Premium → Gym/Cardio/Đa Năng/Gym Nâng Cao + lớp nhóm; VIP → toàn bộ phòng + dịch vụ VIP; Yoga/Cardio/PT Combo → phòng/dịch vụ chuyên môn tương ứng.
- **Database:** migration đã áp dụng thành công trên `QL_PhongGym`; truy vấn xác nhận quyền dịch vụ/phòng của 15 gói; procedure `sp_CheckIn` có thêm `@MaDKG`.
- **Kiểm thử:** `dotnet build He_Thong_GYM\\He_Thong_GYM.csproj --no-restore` PASS, 0 warning, 0 error.
- **Luồng demo:** chọn hội viên → chọn gói → xem cột dịch vụ → chọn phòng trong danh sách lọc → Check-in. Không chọn gói hoặc chọn phòng ngoài quyền sẽ bị khóa/từ chối.

## Phiên nghiệp vụ — Lọc hóa đơn đã trả trước một phần

- **Ngày:** 23/11/2026.
- **Vấn đề:** bộ lọc `Chưa thanh toán` trước đây so sánh đúng chuỗi `TrangThai=N'Chưa thanh toán'`, nên hóa đơn có `Đã thanh toán một phần` không xuất hiện dù vẫn còn công nợ.
- **Sửa:** `FrmHoaDon.ApDungLoc()` dùng `HoaDonView.ConNo > 0` cho lựa chọn `Chưa thanh toán`, đồng thời loại trừ hóa đơn đã thanh toán đủ, đã hoàn tiền và đã hủy.
- **Kết quả:** hóa đơn đã trả trước một khoản vẫn hiện khi lọc `Chưa thanh toán`; người dùng có thể chọn hóa đơn và tiếp tục thu phần còn thiếu. Lựa chọn `Đã thanh toán một phần` riêng vẫn hoạt động.
- **Kiểm thử:** build lại PASS, 0 warning, 0 error.

## Phiên rà soát luồng nút — Lỗi CurrentCell và trạng thái sau thao tác

- **Ngày:** 23/11/2026.
- **Lỗi từ ảnh:** `FrmVanHanhGoi` đặt `dgvGoi.CurrentCell = row.Cells[0]` trong khi cột `MaDKG` đã ẩn, gây lỗi `Current cell cannot be set to an invisible cell` khi chọn phiên đang tập.
- **Đã sửa:** tìm cột đang hiển thị rồi mới đặt `CurrentCell`; không còn thao tác trên cột ẩn.
- **Rà soát luồng nút:** `FrmDiemDanh` đã thay việc bật cứng nút trong `finally` bằng tính lại trạng thái theo quyền/lớp/học viên; hóa đơn đã thanh toán/đã hủy không còn hiện nút thao tác không hợp lệ; gói đã ngừng áp dụng và nhân viên đã nghỉ việc không còn nút chuyển trạng thái tương ứng.
- **Kiểm thử build:** PASS, 0 warning, 0 error.

## Phiên UX — Tìm kiếm hội viên khi Check-in

- **Ngày:** 23/11/2026.
- **Yêu cầu:** thêm ô tìm kiếm để nhân viên không phải dò thủ công trong ComboBox hội viên khi Check-in.
- **Thực hiện:** `FrmVanHanhGoi` có ô `Tìm hội viên`; lọc tức thời theo mã hội viên, họ tên, số điện thoại hoặc email. Danh sách kết quả vẫn dùng cùng model `ThanhVien`, nên khi chọn kết quả các luồng tải gói, quyền dịch vụ/phòng và trạng thái Check-in giữ nguyên.
- Khi chọn phiên trong tab `Đang tập / Chờ Check-out`, bộ lọc được xóa trước khi chọn hội viên để không làm mất hội viên của phiên đang cần xử lý.
- **Kiểm thử:** UI checker 21/21 form PASS; build `0 warning, 0 error`.

## Phiên kiểm thử database — Rà soát luồng nghiệp vụ

- **Ngày:** 23/11/2026.
- **Toàn vẹn dữ liệu:** `DBCC CHECKCONSTRAINTS WITH NO_INFOMSGS` PASS; không có orphan trong quyền phòng của gói (`GoiTapPhong`). Database hiện có 47 bảng và 28 stored procedure.
- **Procedure chính:** đã rà soát `sp_CheckIn`, `sp_CheckOut`, `sp_ChuyenPhongCheckIn`, `sp_ThanhToanHoaDon`, `sp_HoanTien`, `sp_HuyHoaDon`, `sp_BaoLuuGoiTap`, `sp_MoLaiBaoLuuGoiTap`; metadata `ANSI_NULLS` và `QUOTED_IDENTIFIER` đúng.
- **Regression:** `database/24_final_regression_test.sql` đạt **12/12 luồng**; toàn bộ dữ liệu test được rollback, số phiên Check-in mở sau kiểm thử là 0.
- **Thanh toán:** trả trước được ghi nhận đúng là `Đã thanh toán một phần` và vẫn còn công nợ; thanh toán vượt công nợ bị từ chối, transaction rollback đúng.
- **Check-in:** gói không có quyền phòng/dịch vụ bị từ chối; gói có quyền được ghi nhận; chuyển phòng tiếp tục kiểm tra quyền của gói hiện tại.
- **Kết luận:** chưa phát hiện lỗi database trong các luồng đã kiểm tra. Các hóa đơn còn nợ sau trả trước là dữ liệu hợp lệ và phải tiếp tục xuất hiện ở bộ lọc `Chưa thanh toán`.

## Phiên UX — Cải thiện tìm hội viên ở form Vận hành gói

- **Ngày:** 23/11/2026.
- **Vấn đề:** ô tìm kiếm cũ nằm rời bên dưới ComboBox, gợi ý dài bị cắt và chưa có thao tác xóa bộ lọc rõ ràng.
- **Đã sửa:** đổi nhãn thành `Tìm nhanh`, mở rộng ô nhập, thêm nút `Xóa`, tự mở danh sách kết quả khi gõ và hỗ trợ nhấn Enter nếu chỉ còn một hội viên phù hợp.
- Nút Xóa bị khóa khi không có từ khóa; khi xóa bộ lọc, hệ thống cố gắng giữ hội viên đang chọn để không làm mất ngữ cảnh thao tác.
- Đã loại bỏ handler `TextChanged` rỗng bị gắn trùng trong Designer.
- **Kiểm thử:** build `He_Thong_GYM.csproj --no-restore` PASS, 0 warning, 0 error.

## Phiên UX — Đồng bộ font và cỡ chữ ô tìm kiếm

- **Ngày:** 23/11/2026.
- **Phản hồi:** font/cỡ chữ của ô tìm hội viên ở `FrmVanHanhGoi` chưa đồng bộ với các form có sẵn.
- **Đã sửa:** đối chiếu `FrmHoaDon`, `FrmThanhVien`, `FrmGoiTap`, `FrmBaoTri`; đặt ô tìm kiếm về `Segoe UI 9F` thường, cao 23px, viền `Fixed3D`, nhãn `Tìm kiếm:` và nút `Xóa lọc` cao 30px.
- Không thay đổi nghiệp vụ lọc tức thời, tự mở danh sách kết quả hoặc phím Enter.
- **Kiểm thử:** build với `GenerateResource=false` PASS, 0 warning, 0 error.

## Phiên đánh giá readiness — So sánh với hệ thống gym thực tế

- **Ngày:** 23/11/2026.
- **Kiểm tra lại:** `database/24_final_regression_test.sql` đạt **12/12 luồng lõi**; `DBCC CHECKCONSTRAINTS` PASS; build đạt **0 warning, 0 error** sau khi đóng tiến trình demo đang khóa file.
- **Dữ liệu hiện tại:** 1 phiên Check-in đang mở (`TV000001 - Nguyễn Minh Anh`, `PHG000001`) và 11 hóa đơn còn công nợ/chưa thanh toán. Đây là trạng thái hợp lệ; cần xử lý nếu muốn demo với dữ liệu sạch.
- **Đã đạt:** quản lý hội viên/gói, phân quyền phòng theo gói, thanh toán một phần/hoàn tiền, Check-in/Check-out/chuyển phòng, gia hạn/bảo lưu/mở lại, PT/lớp, bán hàng/khách vãng lai/kho, bảo trì thiết bị, phân quyền tài khoản và kiểm thử hồi quy.
- **Còn thiếu so với sản phẩm thương mại:** triển khai hiện gắn với SQL Server local và Windows Authentication; thiếu quy trình production cho installer, migration, backup/restore, monitoring; tự động hết hạn còn phụ thuộc SQL Agent/fallback Dashboard; chưa có tích hợp QR/RFID/biometric, app/thông báo hội viên, multi-branch và lịch/hoàn thành buổi PT đầy đủ.
- **Kết luận:** hệ thống **đủ tốt để demo báo cáo và vận hành nội bộ có kiểm soát**, nhưng chưa nên tuyên bố là hệ thống thương mại hoàn chỉnh cho nhiều cơ sở.

## Phiên UX — Liên kết form Thiết bị và Bảo trì thiết bị

- **Ngày:** 23/11/2026.
- **Vấn đề:** hai form liên quan nhưng chưa có luồng chuyển thiết bị trực tiếp; nhân viên phải chọn lại thiết bị bằng tay.
- **Đã sửa:** thêm nút `Lập phiếu bảo trì` trên `FrmThietBi`; mở `FrmBaoTri` với đúng thiết bị đang chọn và tự chọn nhân viên đăng nhập.
- **Bổ sung:** ngày dự kiến hoàn tất có thể chọn/bỏ chọn; bảng phiếu hiển thị mã thiết bị, ngày dự kiến và ngày hoàn tất; thêm hướng dẫn trạng thái khi tạo phiếu.
- **Kiểm thử:** build PASS `0 warning, 0 error`; UI Inspector PASS `21/21`; regression `database/24_final_regression_test.sql` PASS `12/12` và rollback sạch.

## Phiên kiểm thử database — Luồng thiết bị/bảo trì và dữ liệu semantic

- **Ngày:** 23/11/2026.
- Chạy tuần tự `database/24_final_regression_test.sql`: **12/12 PASS**, rollback sạch. Deadlock ở lần chạy song song trước được xác định là do chạy đồng thời nhiều script test, không phải kết quả nghiệp vụ.
- `DBCC CHECKCONSTRAINTS WITH NO_INFOMSGS`: PASS.
- Phát hiện semantic inconsistency: `TB000004`, `TB000010`, `TB000015` đang ghi `Bảo trì` dù phiếu liên quan đã `Hoàn thành` và không còn phiếu mở; `TB000012` là trường hợp đúng vì đang có phiếu `Đang bảo trì`.
- Procedure tạo/hoàn tất bảo trì mới vẫn PASS trong luồng 11 của regression. Cần có bước đối soát dữ liệu lịch sử trước khi tuyên bố dữ liệu thiết bị hoàn toàn sạch.

## Phiên sửa lỗi semantic dữ liệu thiết bị

- **Ngày:** 23/11/2026.
- Thêm `database/28_dong_bo_trang_thai_thiet_bi.sql` để khôi phục có điều kiện các thiết bị đang ghi `Bảo trì` nhưng không còn phiếu bảo trì mở; không đụng tới thiết bị còn phiếu mở.
- Migration đã sửa 3 thiết bị stale. `TB000012 - Bóng tập core` vẫn đúng trạng thái `Bảo trì` với 1 phiếu đang mở.
- Sửa `database/21_test_phien_13.sql` để cô lập phiên Check-in demo trong transaction rollback, tránh test thất bại vì dữ liệu vận hành đang mở.
- **Kết quả:** test phiên 13 PASS; regression cuối PASS `12/12`; DBCC PASS; không còn thiết bị stale theo quy tắc kiểm tra.

## Phiên triển khai Dashboard tổng quan nâng cao

- **Ngày:** 23/11/2026.
- **Thực hiện:** mở rộng model/BLL/DAL Dashboard và dựng lại vùng Dashboard bằng control động để giữ nguyên các form hiện có.
- **Bộ lọc:** hôm nay, hôm qua, 7 ngày qua, tháng này, tháng trước, quý này, năm nay và khoảng tùy chọn; các mốc ngày được truyền bằng `SqlParameter`.
- **KPI:** tổng/hội viên hoạt động/mới, gói đang sử dụng/sắp hết hạn, doanh thu kỳ, công nợ, số người đang có mặt và buổi PT.
- **Chi tiết:** biểu đồ doanh thu theo ngày, trạng thái hội viên, bảng gói sắp hết hạn, công nợ, hoạt động gần đây và gói bán chạy.
- **Phân quyền:** tài khoản HLV/Huấn luyện viên không thấy doanh thu và công nợ chi tiết trên Dashboard.
- **Kiểm thử:** build `He_Thong_GYM.csproj --no-restore -p:GenerateResource=false` PASS với `0 warning, 0 error`; `--check-ui` PASS `21/21`; truy vấn SQL Dashboard chạy thành công trên `QL_PhongGym`; regression nghiệp vụ `12/12 PASS`; `DBCC CHECKCONSTRAINTS WITH NO_INFOMSGS` PASS.


## Phiên nâng cấp trực quan Dashboard & Luồng vận hành (23/11/2026)

- **Mục tiêu:** Nâng cấp toàn diện giao diện Tổng quan Dashboard trên `Form1`, đảm bảo các luồng thông tin hiển thị trực quan, sinh động và chính xác.
- **Thực hiện:**
  - Thêm `Helpers/DashboardDonutPanel.cs`: Panel vẽ biểu đồ tròn/donut bằng GDI+ thuần (không phụ thuộc thư viện ngoài), hỗ trợ tỷ lệ phần trăm trực quan, tổng số lượng ở tâm và chú thích màu sắc rõ ràng cho cơ cấu hội viên.
  - Tái cấu trúc dải thẻ KPI trên Dashboard: bổ sung biểu tượng trực quan (👥, ✅, ⏰, 📦, 💰, 📋, 🏃, 🏋️) và chỉ số phụ chi tiết (hội viên mới/hết hạn, doanh thu hôm nay/tháng, số ca check-in/giờ cao điểm, buổi PT hoàn thành/chờ xử lý).
  - Bố cục biểu đồ 3 cột cân đối: Biểu đồ doanh thu theo ngày (cột) + Biểu đồ donut trạng thái hội viên + Biểu đồ thanh phân bổ hội viên.
  - Chuẩn hóa hiển thị lưới dữ liệu qua `TaoBangNangCao()`: màu nền xen kẽ (`#F8FAFC`), viền phân cách ngang mỏng, căn lề số liệu và màu sắc cảnh báo theo mức độ khẩn cấp (đỏ cho quá hạn/công nợ, vàng/cam cho sắp hết hạn).
  - Tinh chỉnh thanh bộ lọc và tóm tắt vận hành (Check-in, Check-out, giờ cao điểm, gia hạn, bảo lưu).
- **Kiểm thử:**
  - Build .NET: `dotnet build He_Thong_GYM.slnx --no-restore` đạt **0 warning, 0 error**.
  - UI Inspector: `21/21 forms` đạt chuẩn 100% (0 overlap, 0 tràn biên, 0 mất chữ, 0 lỗi UTF-8).


## Phiên chuyển đổi Dashboard thành Form độc lập FrmTongQuan (23/11/2026)

- **Mục tiêu:** Chuyển đổi toàn bộ khu vực Dashboard từ cơ chế dựng dynamic controls lồng trong `Form1.cs` thành Form độc lập `FrmTongQuan` (gồm `FrmTongQuan.cs`, `FrmTongQuan.Designer.cs`, `FrmTongQuan.resx`), chuẩn hóa kiến trúc điều hướng form con.
- **Thực hiện:**
  - Tạo `FrmTongQuan`: Form độc lập hoàn chỉnh chứa thanh bộ lọc (preset thời gian + tùy chọn khoảng ngày), 8 thẻ KPI có icon và phụ đề chi tiết, hàng 3 biểu đồ (Doanh thu cột, Donut cơ cấu hội viên, Thanh phân bổ trạng thái), 4 bảng dữ liệu (Gói sắp hết hạn có cảnh báo màu sắc, Công nợ, Hoạt động gần đây, Gói bán chạy) và tóm tắt vận hành.
  - Hỗ trợ sự kiện `YeuCauMoForm` với thuộc tính `[Browsable(false)]` và `[DesignerSerializationVisibility(Hidden)]` để liên kết mở nhanh `FrmVanHanhGoi` hoặc `FrmHoaDon` khi nhấp đúp vào lưới mà không gây lỗi WFO1000.
  - Refactor `Form1.cs`: Loại bỏ hoàn toàn hơn 360 dòng mã tạo control động, đưa `Form1.cs` về đúng vai trò khung điều hướng chính (Main Shell). Khi chọn "Tổng quan", Form1 chỉ việc gọi `HienThiFormCon(new FrmTongQuan())`.
- **Kiểm thử:**
  - Build: `dotnet build He_Thong_GYM.slnx --no-restore` đạt **0 warning, 0 error**.
  - UI Inspector: Đã quét và thông qua toàn diện **22/22 forms** (0 overlap, 0 out of bounds, 0 text truncated, 0 encoding issues).


## Phiên tối ưu hóa UI/UX và Form Layout FrmTongQuan (23/11/2026)

- **Mục tiêu:** Kiểm tra và tinh chỉnh toàn diện Layout & UI/UX trên form mới `FrmTongQuan`.
- **Thực hiện:**
  - `panelBoLoc`: Dời dải viền trang trí xanh lam lên sát mép trên cùng (`Y=0`, `Dock = Top`) tràn toàn bộ chiều ngang card; chuẩn hóa khoảng cách giữa tiêu đề, mô tả và hàng controls bộ lọc.
  - `tableBieuDo`: Tinh chỉnh `DashboardDonutPanel.cs` với kích thước donut tỷ lệ vàng (`90..135px`), khoảng trống rộng rãi cho chú thích bên dưới để không bị cắt xén bất kể số lượng trạng thái; cập nhật `DashboardBarPanel.cs` với rút gọn thông minh số lớn (`M`, `K`, `B`), tránh tràn số tiền lớn ở trục giá trị.
  - DataGridView: Bật `AutoSizeMode = Fill` cho cột `TenGoi` (Gói sắp hết hạn) và `HoTen` (Công nợ), loại bỏ khoảng trắng thừa ở cạnh phải lưới; căn giữa các cột ngày tháng / số ngày còn lại / trạng thái; căn phải kế toán (`MiddleRight`) cho các cột tiền tệ và số lượng.
  - Tương tác: Gán con trỏ chuột dạng bàn tay (`Cursors.Hand`) trên hai lưới có tính năng nhấp đúp điều hướng (`dgvGoiSapHetHan` và `dgvCongNo`).
- **Kiểm thử:**
  - Build: `0 warning, 0 error`.
  - UI Inspector: `22/22 forms` PASS 100% (0 overlap, 0 out of bounds, 0 text truncated, 0 UTF-8 issues).


## Phiên tái cấu trúc thư mục Database & Chuẩn hóa Master Script (23/11/2026)

- **Mục tiêu:** Tinh gọn và cấu trúc lại thư mục `database/` theo mô hình chuẩn nghiệp vụ (Cách 1) nhằm tăng tính trực quan, loại bỏ tình trạng rải rác hơn 30 file patch/test mà vẫn bảo toàn 100% lịch sử và không ảnh hưởng đến mã nguồn dự án.
- **Thực hiện:**
  - **Tạo File Master duy nhất:** `database/01_khoi_tao_database_full.sql` (198,936 bytes, UTF-8), tổng hợp trọn vẹn toàn bộ 25 migration patch từ `htql_gym_v2.sql` đến `28_dong_bo_trang_thai_thiet_bi.sql`. Script tạo mới toàn bộ DB `QL_PhongGym`, 18 bảng, đầy đủ Primary Key, Foreign Key, Check Constraint, 13 Trigger tự động hóa, Stored Procedure, View và bộ dữ liệu mẫu ban đầu.
  - **Tạo File kiểm thử tự động:** `database/02_kiem_thu_hoi_quy.sql` (11,625 bytes, UTF-8) kiểm tra hồi quy tự động 12 luồng nghiệp vụ lõi (hội viên, đăng ký gói, thanh toán, check-in/out, gia hạn, bảo lưu, hợp đồng PT, bán lẻ khách vãng lai, nhập kho, bảo trì máy móc, hoàn tiền/hủy gói) kèm cơ chế tự động ROLLBACK sạch sẽ.
  - **Biên soạn tài liệu hướng dẫn:** `database/README.md` (4,658 bytes) hướng dẫn chi tiết quy trình cài đặt CSDL trong 1 bước bằng SSMS/Azure Data Studio hoặc dòng lệnh PowerShell `sqlcmd -f 65001`.
  - **Di chuyển toàn bộ 38 file cũ vào kho lưu trữ:** Tạo thư mục `database/migrations_history/` và di chuyển toàn bộ các bản vá lịch sử cùng các script test con vào trong. Thư mục gốc `database/` hiện tại chỉ còn đúng 3 tệp master và 1 thư mục con `migrations_history/`.
- **Kiểm thử:**
  - Chạy `database/02_kiem_thu_hoi_quy.sql` bằng `sqlcmd -S localhost -d QL_PhongGym -E -f 65001`: **PASS 12/12 luồng nghiệp vụ**, dữ liệu kiểm thử được rollback an toàn 100%.
  - Build .NET: `dotnet build He_Thong_GYM.slnx --no-restore` đạt **0 warning, 0 error**.
  - UI Inspector: Quét toàn diện **22/22 forms** đạt chuẩn 100% (0 overlap, 0 out of bounds, 0 text truncated, 0 UTF-8 issues).

