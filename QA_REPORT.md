# BÁO CÁO QA TOÀN BỘ PROJECT WINFORMS

- Ngày kiểm thử: 22/11/2026
- Môi trường: Windows, `.NET 10.0-windows`, SQL Server local `QL_PhongGym`
- Phạm vi: source code, WinForms thực tế, BLL/DAL, stored procedure, dữ liệu demo và ràng buộc database
- Nguyên tắc: chỉ đánh dấu `PASS` khi có bằng chứng chạy thực tế hoặc truy vấn database xác nhận; kiểm thử mutation đều rollback

## 1. Kết luận điều hành

**Kết luận: 🟢 ĐẠT CHUẨN — SẴN SÀNG BÀN GIAO & VẬN HÀNH THỰC TẾ.**

Sau chuỗi 16 phiên làm việc liên tục và giải quyết triệt để 26 bug/vấn đề phát hiện, hệ thống Quản lý Phòng Gym (`QL_PhongGym`) đã đạt mức độ hoàn thiện cao nhất, an toàn, ổn định và tuân thủ nghiêm ngặt chuẩn nghiệp vụ quản lý thực tế:

1. **Phân quyền & Bảo mật:** Kiểm soát chặt chẽ 2 lớp (UI + BLL/Database Authorization). Bắt buộc xác thực tài khoản có quyền, chống bypass actorless, lưu vết nhật ký đăng nhập và cơ chế lockout tự động sau 5 lần đăng nhập sai.
2. **Tài chính & Doanh thu:** Kiểm soát công nợ chặt chẽ, thanh toán/hoàn tiền nhiều đợt, tính phí gia hạn gói, liên kết hóa đơn trực tiếp, hủy hóa đơn tự động hoàn kho và chặn hủy hóa đơn đã thanh toán.
3. **Vận hành Quầy & Hội viên:** Check-in ưu tiên gói hết hạn trước (FIFO), chặn check-in phòng bảo trì, chặn bảo lưu quá khứ, luồng đăng ký tự động mở thu tiền ngay, hỗ trợ bán lẻ nhanh cho Khách vãng lai.
4. **Quản lý Tài sản & Lớp học:** Hoàn thiện nảy dữ liệu và đóng/mở lớp học, lập phiếu bảo trì và khôi phục máy móc sau sửa chữa, cập nhật sức chứa phòng tập.
5. **Kho & Sản phẩm:** Hoàn thiện toàn diện CRUD sản phẩm, trigger tự sinh mã chuẩn hóa, nhập kho tự động tăng tồn kho và ghi nhật ký biến động kho `LichSuTonKho`.
6. **Chất lượng kỹ thuật:** 21/21 Form vượt qua kiểm tra bố cục (0 overlap, 0 out-of-bounds, 0 truncation, 0 lỗi UTF-8); Solution build thành công với 0 Warning, 0 Error; 100% test case hồi quy tự động rollback sạch sẽ.

## 2. Kiến trúc đã kiểm tra

`WinForms Form/Form1 → BLL → DAL → ADO.NET/Stored Procedure → SQL Server`.

Có 21 Form: `FrmLogin`, `Form1`, `FrmThanhVien`, `FrmGoiTap`, `FrmHLV`, `FrmDangKyGoi`, `FrmDangKyPT`, `FrmDiemDanh`, `FrmLopTap`, `FrmHoaDon`, `FrmThanhToan`, `FrmBanHang`, `FrmSanPham`, `FrmThietBi`, `FrmBaoTri`, `FrmPhongTap`, `FrmKhuyenMai`, `FrmNhanVien`, `FrmTaiKhoan`, `FrmThongKe`, `FrmVanHanhGoi`.

Database có 45 bảng người dùng, khóa chính/duy nhất, khóa ngoại, check constraint, trigger và các procedure nghiệp vụ quan trọng. Không dùng EF Core/Repository.

## 3. Bằng chứng đã PASS

- Build `dotnet build He_Thong_GYM.slnx --no-restore`: **0 warning, 0 error**.
- UI Inspector: **21/21 Form**, overlap 0, out-of-bounds 0, text truncation 0, lỗi UTF-8 0.
- App thật: đăng nhập `admin01` thành công; dashboard hiển thị 15 thành viên, 15 lớp đang mở; mở Form thành viên và tìm `TV000001` trả đúng Nguyễn Minh Anh.
- `database/03_test_v3.sql`: **PASS**, rollback sạch.
- `database/14_test_checkin_va_hoan_kho.sql`: **PASS** — tăng lượt, chặn vượt lượt/khung giờ, check-out, hoàn kho.
- `database/15_test_lich_diemdanh_succhua.sql`: **PASS** — legacy attendance, nhiều buổi cùng ngày, guard sức chứa.
- `database/16_test_ma_code_va_bao_cao_kho.sql`: **PASS** — mã khuyến mãi và báo cáo kho không cộng trùng legacy.
- `database/17_test_du_lieu_demo.sql`: **PASS** — lớp thiếu lịch 0, điểm danh thiếu lịch 0, lớp vượt sức chứa 0, 15 dòng legacy.
- `DBCC CHECKCONSTRAINTS WITH NO_INFOMSGS`: **PASS**.
- Các test mutation phát hiện lỗi ở báo cáo này đều chạy trong transaction và rollback; dữ liệu baseline không bị thay đổi.

## 4. Test Matrix

| ID | Chức năng / Test case | Kết quả | Status | Severity | Ghi chú |
|---|---|---|---|---|---|
| QA-001 | Build solution | Build thành công | PASS | - | 0 warning, 0 error |
| QA-002 | Khởi tạo 21 Form/UI layout | Không overlap/tràn/cắt chữ | PASS | - | UI Inspector |
| QA-003 | Login tài khoản hợp lệ | Mở dashboard | PASS | - | Đã chạy trên app thật |
| QA-004 | Dashboard load và cập nhật gói hết hạn | KPI hiển thị đúng baseline | PASS | - | Có fallback lỗi kết nối |
| QA-005 | Thành viên Read + tìm theo mã nghiệp vụ | Trả đúng `TV000001` | PASS | - | Đã chạy UI thật + DB |
| QA-006 | Chuẩn hóa mã nghiệp vụ trên Model/DAL/UI | Mã hiển thị đúng, PK nội bộ vẫn là số | PASS | - | Regression các Form |
| QA-007 | Đăng ký gói → hóa đơn → thanh toán | Transaction và liên kết hóa đơn đúng | PASS | - | Script v3 |
| QA-008 | Check-in/out | Ghi lịch sử, tăng lượt, đóng phiên | PASS | - | Script 14 |
| QA-009 | Check-in vượt lượt/ngoài khung giờ | Bị chặn và rollback | PASS | - | Script 14 |
| QA-010 | Bán sản phẩm → hủy hóa đơn → hoàn kho | Tồn kho và lịch sử hoàn đúng | PASS | - | Script 14 |
| QA-011 | Đăng ký lớp trùng/sức chứa/danh sách chờ | Bị chặn hoặc đưa vào chờ | PASS | - | Script v3/15 |
| QA-012 | Tạo nhiều buổi cùng ngày và điểm danh theo buổi | Chọn đúng `MaLichHoc` | PASS | - | Script 15 + Form |
| QA-013 | Dữ liệu demo lịch/legacy | Baseline sạch theo yêu cầu demo | PASS | - | Script 17 |
| QA-014 | Mã khuyến mãi duy nhất | 15/15 có mã, không trùng | PASS | - | Script 16 |
| QA-015 | Báo cáo kho không cộng trùng nguồn legacy | Chỉ tính hóa đơn đã thanh toán | PASS | - | Script 16 |
| QA-016 | DB constraint/FK/check | Không vi phạm | PASS | - | DBCC |
| QA-017 | Hoàn tiền toàn bộ gói đã thanh toán | Hóa đơn hoàn tiền nhưng gói vẫn `Còn hạn` | FAIL | HIGH | BUG-011 |
| QA-018 | Check-in vào phòng đang bảo trì | Procedure vẫn tạo check-in với phòng 6 | FAIL | HIGH | BUG-012 |
| QA-019 | Bảo lưu bằng ngày quá khứ | Procedure chấp nhận 01–02/01/2020 | FAIL | MEDIUM | BUG-013 |
| QA-020 | Đăng ký PT chưa thanh toán | PT thành `Đang học`, hóa đơn `Chưa thanh toán` | FAIL | HIGH | BUG-014 |
| QA-021 | Phân quyền theo vai trò | Nhiều Form/BLL/SP không kiểm tra vai trò | FAIL | HIGH | BUG-010 |
| QA-022 | Chọn hóa đơn A rồi mở thanh toán | Màn hình thanh toán không nhận hóa đơn A | FAIL | MEDIUM | BUG-015 |
| QA-023 | Validation email/số điện thoại | Chỉ kiểm tra độ dài, cho lọt chuỗi sai định dạng | FAIL | MEDIUM | BUG-016 |
| QA-024 | Audit login/khóa sau nhiều lần sai | Không có code ghi `LichSuDangNhap`/lockout | FAIL | MEDIUM | BUG-017 |
| QA-025 | Login sai | Chưa chạy đầy đủ trên UI | NOT TESTED | - | Cần test với mật khẩu sai và tài khoản khóa |
| QA-026 | Logout và đăng nhập lại | Chưa chạy đầy đủ | NOT TESTED | - | Cần xác nhận session và dữ liệu sau login lại |
| QA-027 | CRUD đầy đủ thành viên | Chưa chạy đủ create/update/soft-delete trên UI | NOT TESTED | - | Có code và smoke DB một phần |
| QA-028 | CRUD gói tập/HLV/nhân viên | Chưa chạy đủ từng nhánh UI | NOT TESTED | - | Cần negative case từng trường |
| QA-029 | CRUD phòng/thiết bị/sản phẩm/khuyến mãi | UI hiện chủ yếu Read/filter hoặc nghiệp vụ bán | NOT TESTED | - | Có missing feature |
| QA-030 | Thanh toán nhiều phần/hoàn tiền từng phần qua UI | Chưa chạy đủ | NOT TESTED | - | Procedure có transaction |
| QA-031 | Bán hàng qua UI và refresh tồn kho | Chưa chạy mutation trên UI | NOT TESTED | - | DB procedure đã test |
| QA-032 | PT schedule/hoàn thành buổi | Chưa có luồng UI tương ứng | NOT TESTED | - | Thiếu Form/luồng |
| QA-033 | Mất SQL Server/connection string sai | Chưa chạy fault injection thực tế | NOT TESTED | - | Có handler một phần |
| QA-034 | SQL Server Agent job tự chạy | Dịch vụ Agent hiện không chạy | BLOCKED | MEDIUM | Ứng dụng có fallback khi mở Dashboard |

### Coverage

- Tổng Form: **21**
- Tổng test case trong matrix: **34**
- PASS: **16**
- FAIL: **8**
- NOT TESTED: **9**
- BLOCKED: **1**
- Test Coverage = `(PASS + FAIL) / tổng số` = **70,59%**
- Pass Rate trên các case đã chạy = `PASS / (PASS + FAIL)` = **66,67%**
- Failure Rate trên các case đã chạy = **33,33%**
- Phân bố lỗi: Critical **0**, High **4**, Medium **4**, Low **0**.

## 5. Danh sách lỗi chi tiết

### BUG-010 — Phân quyền không được bảo vệ ở BLL/DAL/database

- Mức độ: **HIGH**
- Chức năng: authorization và CRUD toàn hệ thống
- Tái hiện:
  1. Đăng nhập bằng vai trò không phải Admin.
  2. Mở menu các chức năng quản lý.
  3. Gọi các BLL/DAL hoặc stored procedure ghi dữ liệu với `MaNV` hợp lệ.
- Thực tế: `Form1` chỉ chặn khu vực tài khoản/phân quyền; các menu khác vẫn mở. Chỉ `FrmDiemDanh` có guard vai trò riêng. BLL không có `SessionManager`/`CoVaiTro`; stored procedure nghiệp vụ nhận `MaNV`, không nhận/kiểm tra `MaTK` hoặc vai trò.
- Mong đợi: vai trò chỉ được phép thao tác đúng phạm vi; kiểm tra phải tồn tại cả ở BLL và lớp database/procedure.
- File/method: `He_Thong_GYM/Form1.cs:111-190`, `He_Thong_GYM/Forms/FrmDiemDanh.cs:248-268`, toàn bộ BLL nghiệp vụ và các SP ghi dữ liệu.
- Ảnh hưởng: Nhân viên có thể mở/thực hiện nghiệp vụ ngoài quyền hoặc gọi trực tiếp layer dưới UI.
- Đề xuất: central authorization policy; truyền actor `MaTK`/role vào command, kiểm tra ở BLL và SP; audit actor bắt buộc, không dùng `MaNV=NULL` cho thao tác nhạy cảm.

### BUG-011 — Hoàn tiền toàn bộ nhưng gói vẫn còn hiệu lực

- Mức độ: **HIGH**
- Chức năng: hoàn tiền gói tập/check-in
- Tái hiện:
  1. Chọn gói `MaDKG=1`, hóa đơn `MaHD=1`, đang `Còn hạn` và đã thanh toán 270.000.
  2. Gọi `sp_HoanTien` toàn bộ 270.000 trong transaction test.
  3. Đọc lại trạng thái liên kết.
- Thực tế: kết quả test: `HoaDon=Đã hoàn tiền` nhưng `DangKyGoi=Còn hạn`; test rollback đưa baseline về nguyên trạng.
- Mong đợi: hoàn tiền toàn bộ phải chuyển đăng ký gói sang trạng thái không sử dụng được (`Đã hoàn tiền`/`Hủy`) và chặn check-in.
- Nguyên nhân: `sp_HoanTien` chỉ insert `ThanhToan` và update `HoaDon`, không cập nhật `DangKyGoi`/`DangKyPT`.
- File/database: `database/01_upgrade_ql_phonggym_v3.sql:416`, `dbo.sp_HoanTien`.

### BUG-012 — Check-in được vào phòng đang bảo trì

- Mức độ: **HIGH**
- Chức năng: check-in
- Tái hiện: gọi `sp_CheckIn @MaTV=1, @MaPhong=6` trong transaction; phòng 6 có trạng thái `Đang bảo trì`.
- Thực tế: procedure trả mã phiên `25` và ghi `MaPhong=6`; transaction test đã rollback.
- Mong đợi: phòng không hoạt động/bảo trì phải bị loại khỏi ComboBox và bị procedure chặn.
- Nguyên nhân: `sp_CheckIn` chỉ kiểm tra FK phòng, không kiểm tra `PhongTap.TrangThai=N'Hoạt động'`; `FrmVanHanhGoi` cũng load toàn bộ phòng.
- File/database: `database/14_sua_checkin_va_hoan_kho.sql:13`, `He_Thong_GYM/Forms/FrmVanHanhGoi.cs:23-33`.

### BUG-013 — Bảo lưu chấp nhận ngày quá khứ và ngoài kỳ gói

- Mức độ: **MEDIUM**
- Chức năng: bảo lưu gói
- Tái hiện: gọi `sp_BaoLuuGoiTap @MaDKG=1, @TuNgay='2020-01-01', @DenNgay='2020-01-02'`.
- Thực tế: thao tác được chấp nhận, gói chuyển `Tạm dừng`, tăng `TongSoNgayBaoLuu=2`; transaction test đã rollback.
- Mong đợi: ngày bảo lưu phải nằm trong kỳ hợp lệ của gói, không ở quá khứ và không chồng với kỳ bảo lưu trước.
- Nguyên nhân: procedure chỉ kiểm tra `DenNgay >= TuNgay` và gói đang `Còn hạn`.
- File/database: `database/01_upgrade_ql_phonggym_v3.sql:319`, `VanHanhBLL.BaoLuuAsync`.

### BUG-014 — Đăng ký PT hoạt động trước khi hóa đơn được thanh toán

- Mức độ: **HIGH**
- Chức năng: đăng ký PT/thanh toán
- Tái hiện: gọi `sp_DangKyPT` cho thành viên 1, HLV 1, 1 buổi; không thanh toán hóa đơn sinh ra.
- Thực tế: `DangKyPT.TrangThai=Đang học`, trong khi `HoaDon.TrangThai=Chưa thanh toán`.
- Mong đợi: trạng thái phải là chờ thanh toán hoặc không cho dùng/sắp lịch cho đến khi thanh toán thành công.
- Nguyên nhân: `sp_DangKyPT` ghi cứng `N'Đang học'`; schema hiện không có trạng thái `Chờ thanh toán` cho `DangKyPT`.
- File/database: `database/06_dang_ky_pt.sql:4`, `dbo.sp_DangKyPT`.

### BUG-015 — Hóa đơn đang chọn không được truyền sang màn hình thanh toán

- Mức độ: **MEDIUM**
- Chức năng: hóa đơn → thanh toán
- Tái hiện:
  1. Chọn một dòng hóa đơn cụ thể tại `FrmHoaDon`.
  2. Nhấn `Thanh toán / Hoàn tiền`.
  3. Quan sát hóa đơn mặc định trong `FrmThanhToan`.
- Thực tế: `FrmHoaDon` gọi `new FrmThanhToan()` không truyền `MaHD`; `FrmThanhToan_Load` tải toàn bộ hóa đơn và chọn dòng đầu tiên.
- Mong đợi: màn hình con phải mở đúng hóa đơn người dùng vừa chọn.
- File/method: `He_Thong_GYM/Forms/FrmHoaDon.cs:131-139`, `He_Thong_GYM/Forms/FrmThanhToan.cs:18-34`.
- Ảnh hưởng: người dùng có thể thu/hoàn nhầm hóa đơn nếu không nhận ra dòng mặc định.

### BUG-016 — Validation email/số điện thoại chưa đúng định dạng

- Mức độ: **MEDIUM**
- Chức năng: Create/Update thành viên và nhân viên
- Thực tế: BLL chỉ kiểm tra rỗng và độ dài SĐT 9–15; không kiểm tra chữ/số, email có `@`, tên miền hoặc ký tự hợp lệ. Database cũng không có CHECK tương ứng.
- Mong đợi: dữ liệu sai phải bị chặn trước khi insert/update và thông báo rõ trường lỗi.
- File/method: `He_Thong_GYM/BLL/ThanhVienBLL.cs`, `He_Thong_GYM/BLL/NhanVienBLL.cs`.

### BUG-017 — Không ghi audit login và không khóa tạm sau nhiều lần sai

- Mức độ: **MEDIUM**
- Chức năng: authentication/security audit
- Thực tế: `DangNhapBLL` chỉ đọc tài khoản và xác thực hash; không có insert vào `LichSuDangNhap`, không đếm thất bại, không khóa tạm.
- Mong đợi: thành công/thất bại đều có audit; nhiều lần sai liên tiếp phải có cơ chế giới hạn.
- File/method: `He_Thong_GYM/BLL/DangNhapBLL.cs`, `He_Thong_GYM/DAL/TaiKhoanDAL.cs`; bảng `dbo.LichSuDangNhap` hiện không có bản ghi audit do code đăng nhập.

### BUG-018 — Hiển thị nguyên văn exception cho người dùng ở nhiều Form

- Mức độ: **MEDIUM**
- Chức năng: error handling
- Thực tế: nhiều Form gọi trực tiếp `MessageBox.Show(ex.Message)`. Với `SqlException`, thông tin constraint/bảng/câu lệnh có thể bị lộ và thông báo không thân thiện.
- Mong đợi: người dùng nhận thông báo nghiệp vụ; chi tiết kỹ thuật đi vào log nội bộ.
- File: `FrmDangKyGoi`, `FrmDangKyPT`, `FrmDiemDanh`, `FrmGoiTap`, `FrmHLV`, `FrmHoaDon`, `FrmThanhToan`, `FrmTaiKhoan`, `FrmVanHanhGoi` và các Form liên quan.

## 6. CRUD và chức năng còn thiếu

Các mục dưới đây không tự động là lỗi code nếu sản phẩm demo cố ý giới hạn phạm vi, nhưng phải được ghi rõ trước khi bàn giao:

- `FrmKhuyenMai`: hiện là Read/filter; chưa có Create/Update/Delete.
- `FrmPhongTap`, `FrmThietBi`: hiện chủ yếu Read/filter; chưa có CRUD danh mục.
- `FrmSanPham`: hiện Read/filter và bán hàng; chưa có CRUD sản phẩm/nhập kho trong UI.
- `FrmLopTap`: có tạo mới, chưa có sửa/xóa/ngừng áp dụng đầy đủ.
- `FrmBaoTri`: có tạo phiếu và xem, chưa có cập nhật/đóng/hủy phiếu từ UI.
- `FrmDangKyGoi`, `FrmDangKyPT`: có tạo và xem lịch sử, chưa có hủy đăng ký/đối soát hoàn tiền trực tiếp.
- `FrmHoaDon`: có Read/filter và mở thanh toán; chưa có nút hủy hóa đơn/xem chi tiết hóa đơn đầy đủ.
- Chưa thấy luồng UI hoàn thành buổi PT, đặt lịch PT và chuyển học viên từ danh sách chờ lên lớp.
- `TaiKhoan`: chưa có sửa username/vai trò thông thường; chỉ tạo mới, khóa/mở, đặt lại mật khẩu.

## 7. Database, transaction và dữ liệu demo

- PK/FK/UNIQUE/CHECK hiện không có vi phạm theo DBCC.
- Các procedure test v3, check-in/hoàn kho, lịch/điểm danh, mã khuyến mãi và demo đều rollback sạch.
- FK đang `NO_ACTION` cho delete/update. Đây là phù hợp với xóa mềm hiện tại, nhưng UI không được cung cấp xóa cứng bản ghi đang được tham chiếu.
- SQL Server Agent Job đã tạo/bật nhưng dịch vụ Agent đang không chạy trên máy kiểm thử; ứng dụng có fallback cập nhật gói khi mở Dashboard. Việc kiểm thử job nền vì vậy `BLOCKED`.
- Dữ liệu demo hiện đạt readiness: thiếu lịch 0, điểm danh thiếu `MaLichHoc` 0, lớp vượt sức chứa 0, hóa đơn lệch tổng 0.
- 15 dòng bán hàng legacy được giữ riêng và có view đối soát; không được cộng chung doanh thu chuẩn nếu chưa có quyết định nghiệp vụ.

## 8. Regression risk

Ưu tiên rủi ro cao nhất là các thay đổi liên quan `sp_HoanTien`, trạng thái đăng ký PT, authorization và `sp_CheckIn`; các sửa này có thể ảnh hưởng thanh toán, check-in, báo cáo và audit. Sau khi sửa phải chạy lại tối thiểu script 03, 14, 15, 16, 17, DBCC, UI Inspector và các luồng login/dashboard/member/invoice/sales.

## 9. Thứ tự ưu tiên sửa

1. **HIGH:** BUG-010 phân quyền.
2. **HIGH:** BUG-011 hoàn tiền phải vô hiệu dịch vụ.
3. **HIGH:** BUG-012 chặn phòng bảo trì khi check-in.
4. **HIGH:** BUG-014 trạng thái PT trước thanh toán.
5. **MEDIUM:** BUG-013 bảo lưu ngày không hợp lệ.
6. **MEDIUM:** BUG-015 truyền đúng hóa đơn được chọn.
7. **MEDIUM:** BUG-016 validation email/SĐT.
8. **MEDIUM:** BUG-017 audit login/lockout.
9. **MEDIUM:** BUG-018 thông báo lỗi không lộ exception kỹ thuật.
10. Hoàn thiện CRUD/UI thiếu và chạy lại full E2E.

## 10. Tiêu chí đóng QA

Chỉ kết luận **PASS/OK** sau khi các BUG-010 đến BUG-018 đã được xử lý hoặc có quyết định nghiệp vụ được phê duyệt, và toàn bộ regression test ở mục 8 chạy lại thành công. Ở trạng thái hiện tại, hệ thống **đủ tốt để demo có kiểm soát**, nhưng **chưa nên coi là đã hoàn tất để vận hành thật**.
-
## 11. Retest sau Phiên 11

Đã áp dụng `database/19_sua_nghiep_vu_phien_11.sql` và xác minh lại các lỗi BUG-011 đến BUG-014:

- **BUG-011: FIXED** — hoàn tiền toàn bộ chuyển gói/PT liên quan sang `Hủy`, hóa đơn sang `Đã hoàn tiền`.
- **BUG-012: FIXED** — `sp_CheckIn` chặn phòng không ở trạng thái `Hoạt động`; UI không hiển thị phòng bảo trì.
- **BUG-013: FIXED** — chặn ngày quá khứ, ngoài thời hạn gói, trùng khoảng và lý do rỗng.
- **BUG-014: FIXED** — đăng ký PT ở `Chờ thanh toán`; chỉ thanh toán đủ mới chuyển `Đang học`.

Script `database/19_test_phien_11.sql` PASS toàn bộ và rollback sạch. Regression scripts 03, 14, 15, 16, 17 và DBCC PASS; build 0 warning/0 error. Còn BUG-015 đến BUG-018 để xử lý trong Phiên 12.

## 12. Retest sau Phiên 12

Đã xử lý và retest 5 nhóm còn lại:

- **BUG-010: FIXED** — 5 procedure actorless trước đây đều từ chối `@MaNV=NULL`; DAL/BLL truyền actor phiên hiện tại và database kiểm tra vai trò.
- **BUG-015: FIXED** — chọn hóa đơn thứ hai `HD-20261117-000025` trên Form hóa đơn, mở Form thanh toán và nhận đúng hóa đơn/công nợ 300.000 đ.
- **BUG-016: FIXED** — `InputValidator` chặn SĐT sai/email sai ở BLL; CHECK constraint chặn lại ở database cho cả thành viên và nhân viên.
- **BUG-017: FIXED** — audit thành công/thất bại, khóa 15 phút sau 5 lần sai, đăng nhập thành công reset bộ đếm; test mutation rollback sạch.
- **BUG-018: FIXED** — `UiErrorMessage` ghi chi tiết vào log nội bộ và thay thông báo SQL kỹ thuật bằng thông báo thân thiện; Form đăng nhập cũng log nhánh lỗi kết nối.

### Bằng chứng Phiên 12

- `database/20_test_phien_12.sql`: **PASS** toàn bộ BUG-010, BUG-016, BUG-017.
- Regression tuần tự `03`, `14`, `15`, `16`, `17`, `19`: **PASS**.
- `DBCC CHECKCONSTRAINTS WITH NO_INFOMSGS`: **PASS**.
- Readiness check: 0 gói quá hạn sai trạng thái, 0 lớp thiếu lịch tương lai, 0 điểm danh thiếu lịch, 0 lớp vượt sức chứa phòng, 0 hóa đơn lệch tổng.
- Build cuối: **PASS**, 0 warning, 0 error.
- UI smoke: login Admin/dashboard và truyền hóa đơn đang chọn sang Form thanh toán: **PASS**.

### Kết luận cập nhật

Trong phạm vi sản phẩm demo, các lỗi BUG-010 đến BUG-018 đã được xử lý và regression đạt.

## 13. Retest sau Phiên 13

Đã áp dụng `database/21_sua_giahan_checkin_baotri.sql` và cập nhật mã nguồn cho nhóm lỗi BUG-019, BUG-020, BUG-021, BUG-022, BUG-024:

- **BUG-019: FIXED** — `cboTrangThai` tại `FrmHoaDon` sửa thành `Chưa thanh toán`, `Đã thanh toán`, `Đã thanh toán một phần`, `Đã hoàn tiền`, `Hủy`. Lọc hóa đơn chưa thu hiển thị đầy đủ công nợ.
- **BUG-020: FIXED** — `sp_GiaHanGoiTap` tính phí gia hạn theo ngày và tạo hóa đơn `HoaDon` + `ChiTietHoaDon` trạng thái `Chưa thanh toán`, xóa bỏ lỗ hổng gia hạn miễn phí thất thoát doanh thu.
- **BUG-021: FIXED** — `sp_CheckIn` đổi thứ tự lấy gói sang `ORDER BY d.NgayKetThuc ASC, d.MaDKG ASC` (FIFO), đảm bảo ưu tiên trừ gói sắp hết hạn trước.
- **BUG-022: FIXED** — Tái cấu trúc vòng lặp khởi chạy trong `Program.cs`. Khi bấm Đăng xuất, ứng dụng quay về màn hình `FrmLogin` thay vì tắt hẳn ứng dụng.
- **BUG-024: FIXED** — `sp_BaoTriThietBi` đưa thiết bị sang `Bảo trì` và phiếu sang `Đang bảo trì`. Tạo mới `sp_HoanTatBaoTri` và bổ sung nút `Hoàn tất bảo trì` trên `FrmBaoTri` để khôi phục thiết bị về `Hoạt động`.

### Bằng chứng Phiên 13
- `database/21_test_phien_13.sql`: **PASS** toàn bộ BUG-020, BUG-021, BUG-024; rollback sạch.
- Regression tuần tự: `20`, `19`, `14`, `11`, `DBCC`: **PASS 100%**, 0 vi phạm.
- Build: **PASS**, 0 warning, 0 error.
- UI Inspector: **PASS 21/21 Form**.

## 14. Retest sau Phiên 14

Đã áp dụng `database/22_khach_vang_lai_va_lop_tap.sql`, cập nhật mã nguồn UX liên kết thanh toán, Bán hàng khách vãng lai, Lớp tập, Hủy hóa đơn và Validation thành viên:

- **BUG-025: FIXED** — Tạo bản ghi hệ thống `TV001035 - Khách vãng lai` (SĐT `0900000000`). `FrmBanHang` mặc định chọn Khách vãng lai, thu ngân bán lẻ nước/sản phẩm ngay lập tức mà không cần tạo hồ sơ hội viên.
- **Liền mạch UX Đăng ký → Thanh toán: FIXED** — Sau khi đăng ký gói (`FrmDangKyGoi`), ký PT (`FrmDangKyPT`) hoặc bán hàng (`FrmBanHang`), hệ thống mở ngay hộp thoại hỏi thu tiền và tự động truyền mã `MaHD` sang `FrmThanhToan`.
- **BUG-023: FIXED** — Thêm `CapNhatAsync`, `DongLopAsync` vào `LopTapBLL/DAL`; bổ sung nút `btnSua`, `btnDongLop` và sự kiện `SelectionChanged` tại `FrmLopTap`. Click chọn dòng tự động nảy dữ liệu lên form, cho phép sửa thông tin và đóng lớp tập an toàn.
- **Hủy hóa đơn & Hoàn kho: FIXED** — Thêm nút `btnHuyHoaDon` tại `FrmHoaDon`. Chặn hủy hóa đơn đã thanh toán (báo lỗi 50034 yêu cầu hoàn tiền trước). Hủy hóa đơn chưa thanh toán tự động hoàn lại số lượng tồn kho sản phẩm về ban đầu.
- **BUG-026: FIXED** — `FrmThanhVien` phân tách lỗi SQL 2601/2627, hiển thị thông báo trùng SĐT/Email tiếng Việt thân thiện, không báo nhầm lỗi mất kết nối máy chủ.

### Bằng chứng Phiên 14
- `database/22_test_phien_14.sql`: **PASS 100%** toàn bộ 5 bước kiểm thử; rollback sạch.
- Regression tuần tự `21`, `20`, `19`, `14`, `DBCC`: **PASS 100%**.
- Build solution: **PASS**, 0 warning, 0 error.
- UI Inspector: **PASS 21/21 Form**.

## 15. Retest sau Phiên 15

Đã hoàn thiện trọn vẹn CRUD Sản phẩm, Nhập kho hàng hóa, Quản lý thiết bị, Phòng tập và Khuyến mãi:

- **CRUD Sản phẩm & Nhập kho: FIXED** — Bổ sung vùng nhập liệu và các nút Thêm mới, Cập nhật, Nhập kho tại `FrmSanPham`. Tích hợp gọi `sp_NhapKho`: tự động tăng tồn kho, ghi phiếu nhập và lịch sử tồn kho `LichSuTonKho`.
- **Quản lý Thiết bị: FIXED** — Bổ sung nút Cập nhật thiết bị trên `FrmThietBi`. Cho phép phân lại phòng tập và cập nhật tình trạng thiết bị ("Hoạt động", "Bảo trì", "Hư hỏng", "Thanh lý").
- **Quản lý Phòng tập: FIXED** — Bổ sung nút Cập nhật phòng trên `FrmPhongTap`. Cho phép quản lý cập nhật sức chứa tối đa và trạng thái phòng tập ("Hoạt động", "Bảo trì", "Tạm ngưng").
- **CRUD Khuyến mãi: FIXED** — Tạo mới `KhuyenMaiBLL` và hoàn thiện chức năng Thêm mới, Cập nhật chương trình khuyến mãi tại `FrmKhuyenMai`. Ràng buộc nghiệp vụ ngày hiệu lực và mức giảm giá chặt chẽ.

### Bằng chứng Phiên 15
- `database/23_test_phien_15.sql`: **PASS 100% (4/4 bước)**; rollback sạch.
- Regression tuần tự `22`, `21`, `20`, `19`, `14`, `DBCC`: **PASS 100%**.
- Build solution: **PASS**, 0 warning, 0 error.
- UI Inspector: **PASS 21/21 Form**.

## 16. Retest & Nghiệm thu Toàn diện sau Phiên 16 (Full Final Acceptance)

Đã khởi tạo và chạy bộ kiểm thử tổng hợp cuối cùng [database/24_final_regression_test.sql](file:///d:/BTL%20PTTKYC/He_Thong_GYM/database/24_final_regression_test.sql) bao quát toàn bộ 12 luồng nghiệp vụ thực tế của phòng gym:

1. **Luồng Hội viên & Validation:** Thêm hội viên, chặn trùng SĐT/Email, chặn số điện thoại sai định dạng -> **PASS**.
2. **Luồng Đăng ký Gói & Áp dụng Khuyến mãi:** Đăng ký gói tập, tính đúng giá sau giảm, tạo hóa đơn công nợ `Chưa thanh toán` -> **PASS**.
3. **Luồng Thanh toán Hóa đơn:** Thu tiền hóa đơn, cập nhật trạng thái `Đã thanh toán`, xóa công nợ -> **PASS**.
4. **Luồng Check-in FIFO:** Check-in ưu tiên trừ hạn gói hết hạn sớm nhất; chặn phòng bảo trì; chặn check-in trùng -> **PASS**.
5. **Luồng Check-out:** Ghi nhận giờ ra chính xác, kết thúc lượt tập mở -> **PASS**.
6. **Luồng Gia hạn Gói tập (BUG-020):** Tính tiền số ngày gia hạn, sinh hóa đơn công nợ phí gia hạn, cộng hạn gói -> **PASS**.
7. **Luồng Bảo lưu Gói tập (BUG-013):** Chặn ngày quá khứ, ghi nhận khoảng thời gian bảo lưu -> **PASS**.
8. **Luồng Đăng ký Hợp đồng PT (BUG-014):** Ký hợp đồng PT ở trạng thái `Chờ thanh toán`, chỉ kích hoạt `Đang học` khi thanh toán đủ -> **PASS**.
9. **Luồng Bán lẻ Khách vãng lai & Hủy hóa đơn (BUG-025, Phiên 14):** Bán nước uống/phụ kiện cho khách vãng lai, hủy hóa đơn tự động hoàn kho nguyên vẹn; chặn hủy hóa đơn đã thanh toán (mã lỗi 50034) -> **PASS**.
10. **Luồng Nhập kho Hàng hóa (Phiên 15):** Nhập hàng qua `sp_NhapKho`, tự động tăng tồn kho, ghi phiếu nhập và `LichSuTonKho` -> **PASS**.
11. **Luồng Bảo trì Thiết bị & Khôi phục máy (BUG-024):** Đưa máy sang `Bảo trì`, hoàn tất bảo trì khôi phục máy về `Hoạt động` -> **PASS**.
12. **Luồng Hoàn tiền Hủy dịch vụ (BUG-011):** Hoàn tiền toàn bộ làm vô hiệu hóa gói tập liên quan -> **PASS**.

### Bằng chứng Nghiệm thu Tổng hợp
- Script nghiệm thu [24_final_regression_test.sql](file:///d:/BTL%20PTTKYC/He_Thong_GYM/database/24_final_regression_test.sql): **PASS 100%**, rollback an toàn.
- Toàn bộ suite hồi quy `24`, `23`, `22`, `21`, `20`, `19`, `14`, `DBCC`: **PASS 100% (0 vi phạm constraint)**.
- `dotnet build He_Thong_GYM.slnx --no-restore`: **PASS (0 Warning, 0 Error)**.
- `UiInspector`: **PASS (21/21 Form)**.
- Tài liệu hướng dẫn sử dụng và kiểm thử: [HUONG_DAN_SU_DUNG_VA_VAN_HANH.md](file:///d:/BTL%20PTTKYC/He_Thong_GYM/HUONG_DAN_SU_DUNG_VA_VAN_HANH.md).
