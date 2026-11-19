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
