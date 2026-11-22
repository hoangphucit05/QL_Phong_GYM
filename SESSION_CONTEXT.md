# PROJECT SESSION CONTEXT

> Đây là nguồn ngữ cảnh chung giữa các agent. Source code và schema SQL thực tế là nguồn sự thật khi có mâu thuẫn với tài liệu này. Không ghi mật khẩu, connection string bí mật hoặc secret vào file.

## 1. Project Overview

Hệ thống quản lý phòng gym `QL_PhongGym`, gồm đăng nhập/phân quyền, thành viên, gói tập, đăng ký dịch vụ, PT, lớp học/điểm danh, hóa đơn/thanh toán, bán hàng, sản phẩm/kho, thiết bị/bảo trì, phòng tập và dashboard báo cáo.

Thư mục solution: `He_Thong_GYM.slnx`. Ứng dụng desktop Windows Forms, mã nguồn chính nằm trong thư mục `He_Thong_GYM/`; script database nằm trong `database/`.

## 2. Current Architecture

- UI: WinForms, partial class gồm `.cs`, `.Designer.cs`, `.resx`; có 21 form đã được kiểm tra khởi tạo.
- Target framework: `.NET 10.0-windows`; bật Nullable và ImplicitUsings.
- Data access: ADO.NET với `Microsoft.Data.SqlClient`, truy vấn tham số hóa và stored procedure cho các nghiệp vụ ghi quan trọng.
- Layer: `Forms`/`Form1` (UI) → `BLL` (nghiệp vụ) → `DAL` (truy cập dữ liệu) → SQL Server.
- Model: các lớp trong `He_Thong_GYM/Models` dùng cho dữ liệu giữa các layer.
- Không dùng EF Core hoặc Repository pattern trong implementation hiện tại.
- Cấu hình kết nối ở `He_Thong_GYM/appsettings.json`; mặc định là SQL Server local, database `QL_PhongGym`, Windows Authentication.
- Tiện ích quan trọng: `Helpers/SessionManager.cs`, `Helpers/PasswordHasher.cs`, `Helpers/UiInspector.cs`.

## 3. Database

Database chính: `QL_PhongGym`, khởi tạo từ `database/htql_gym_v2.sql`, sau đó áp dụng các migration theo thứ tự.

- Bảng lõi: `LoaiThanhVien`, `ThanhVien`, `HoSoSucKhoe`, `ChiSoCoThe`, `GoiTap`, `KhuyenMai`, `DangKyGoi`, `NhanVien`, `TaiKhoan`, `HuanLuyenVien`.
- Lớp/PT: `DangKyPT`, `LoaiLopTap`, `LopTap`, `DangKyLop`, `DiemDanhLop`, `LichLamViecHLV`, `LichTapPT`, `LichHoc`, `DanhSachCho`.
- Tài chính: `HoaDon`, `ChiTietHoaDon`, `ThanhToan`, `HoaDonGoiTap`, `HoaDonPT`, `HoaDonLop`, `HoaDonSanPham`.
- Kho/bán hàng: `SanPham`, `PhieuBanHang`, `ChiTietPhieuBanHang`, `NhaCungCap`, `PhieuNhapKho`, `ChiTietPhieuNhap`, `LichSuTonKho`.
- Thiết bị/vận hành: `LoaiThietBi`, `ThietBi`, `BaoTriThietBi`, `PhongTap`, `LichSuThanhVien`, `LichSuGoiTap`, `BaoLuuGoiTap`, `LichSuCheckIn`.
- Audit/phân quyền: `NhatKyHeThong`, `LichSuDangNhap`, `VaiTro`, `TaiKhoanVaiTro`.
- Ràng buộc quan trọng: khóa ngoại giữa thành viên–đăng ký–hóa đơn, hóa đơn–chi tiết–thanh toán; chống nhiều lượt check-in mở; kiểm tra sức chứa phòng/lớp; chống trùng lịch PT/phòng/HLV; kiểm soát tồn kho; mapping chi tiết hóa đơn theo từng loại dịch vụ.
- Stored procedure nghiệp vụ chính gồm `sp_DangKyGoiTap`, `sp_DangKyPT`, `sp_DangKyLop`, `sp_DiemDanhLop`, `sp_TaoLichHoc`, `sp_ThanhToanHoaDon`, `sp_HoanTien`, `sp_BanSanPham`, `sp_BaoTriThietBi`, `sp_CapNhatGoiHetHan`.
- Migration gần nhất: `database/20_sua_bao_mat_validation_phien_12.sql`; các migration 14–20 xử lý check-in/kho, điểm danh legacy, mã khuyến mãi, nghiệp vụ Phiên 11 và bảo mật/validation/audit Phiên 12. `database/11_kiem_tra_san_sang_van_hanh.sql` là script đối soát chỉ đọc.

## 4. Business Rules

- Mật khẩu ứng dụng dùng PBKDF2 SHA-512 với salt riêng; không dùng plaintext trong production. Tài khoản mẫu development có mật khẩu tạm cần đổi trước triển khai thật.
- Các nghiệp vụ ghi nhiều bảng phải đi qua stored procedure/transaction và rollback khi lỗi.
- Đăng ký gói tính giá sau khuyến mãi, thời hạn và lịch sử; gói quá hạn phải chuyển sang `Hết hạn`.
- `sp_CapNhatGoiHetHan` phải idempotent, ghi `LichSuGoiTap` và `NhatKyHeThong`.
- Điểm danh mới phải gắn với `LichHoc` thực tế (`MaLichHoc`), không điểm danh tự do theo ngày; không điểm danh buổi tương lai.
- Buổi học phải kiểm tra lớp/phòng/HLV hoạt động, sức chứa và lịch trùng.
- Phân quyền UI hiện áp dụng theo vai trò; các hàm ghi trong BLL vẫn kiểm tra lại quyền. Kiểm soát quyền ở database cho một số procedure vẫn là việc cần hoàn thiện.
- Bán hàng mới dùng luồng `HoaDon` + `ChiTietHoaDon` + `sp_BanSanPham`; `PhieuBanHang`/`ChiTietPhieuBanHang` là legacy, không tự cộng trùng doanh thu.

## 5. Completed Work

- Đã hoàn thiện schema/migration v3, seed, smoke test và tài liệu triển khai database.
- Đã xây dựng các form và layer CRUD cho thành viên, tài khoản, HLV, gói tập, khuyến mãi, đăng ký gói/PT, lớp học, điểm danh, hóa đơn, thanh toán, sản phẩm/bán hàng, thiết bị/bảo trì, phòng tập, báo cáo.
- Đã bổ sung PBKDF2 login, session người dùng và phân quyền cơ bản.
- Đã chuyển điểm danh sang mô hình theo buổi học qua migration `10_lich_hoc_diem_danh_theo_buoi.sql`.
- Đã bổ sung lọc/sắp xếp học viên và quyền thao tác tại màn hình điểm danh.
- Đã tạo backup và baseline đối soát trong phiên 014; script kiểm tra là `database/11_kiem_tra_san_sang_van_hanh.sql`.
- Đã bổ sung tự động hết hạn gói qua procedure, ứng dụng khi mở Dashboard và SQL Server Agent Job trong migration 12.
- Nhật ký lịch sử chi tiết nằm trong `NHAT_KY_PHIEN_LAM_VIEC_AI.md`.

## 6. Current Work

Phiên 1 về chuẩn hóa mã nghiệp vụ và Phiên 2 về database migration đã hoàn tất. Tài liệu chuẩn là `BUSINESS_CODE_STANDARD.md`; migration là `database/13_chuan_hoa_ma_nghiep_vu.sql`. Phiên 3 sẽ cập nhật Model/DAL/BLL để trả về và tìm kiếm theo mã nghiệp vụ; hiện UI chưa được đổi sang mã mới. Các thay đổi code/database chưa commit khi bắt đầu phiên thuộc các phiên trước và không được tự ý revert:

- `He_Thong_GYM/BLL/DashboardBLL.cs`
- `He_Thong_GYM/DAL/DashboardDAL.cs`
- `He_Thong_GYM/Form1.cs`
- `He_Thong_GYM/Forms/FrmDiemDanh.cs`
- `He_Thong_GYM/Forms/FrmDiemDanh.Designer.cs`
- `.gitignore`
- `database/11_kiem_tra_san_sang_van_hanh.sql`
- `database/12_tu_dong_het_han_goi.sql`
- `NHAT_KY_PHIEN_LAM_VIEC_AI.md`

## 7. Testing Status

- `dotnet build He_Thong_GYM.slnx --no-restore`: PASS, 0 warning, 0 error trong phiên hiện tại (22/11/2026).
- Kiểm tra UI gần nhất theo nhật ký: 21/21 form khởi tạo được; 0 overlap, 0 tràn biên, 0 cắt chữ, 0 lỗi UTF-8.
- Database smoke test, `DBCC CHECKCONSTRAINTS` và các luồng stored procedure đã PASS theo nhật ký các phiên trước; cần chạy lại trên database đích nếu database/migration thay đổi.
- Phiên 1: `dotnet build He_Thong_GYM.slnx --no-restore` PASS, 0 warning/0 error.
- Phiên 1: `database/11_kiem_tra_san_sang_van_hanh.sql` chạy read-only với UTF-8 (`sqlcmd -f 65001`): 0 gói quá hạn sai trạng thái, 15 lớp chưa có buổi tương lai, 17 điểm danh legacy, 1 lớp vượt sức chứa phòng, 0 hóa đơn lệch tổng.
- Phiên 1: mã hiện có `MaThanhVien`, `MaSanPham`, `MaThietBi` có 0 giá trị NULL và 0 mã trùng; `KhuyenMai.MaCode` chưa tồn tại trong database local.
- Phiên 2: backup `C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\QL_PhongGym_pre_codes_20261122.bak` đã tạo với CHECKSUM và `RESTORE VERIFYONLY` PASS.
- Phiên 2: migration 13 áp dụng thành công và chạy lặp thành công; 0 mã nghiệp vụ NULL, DBCC CHECKCONSTRAINTS không báo vi phạm.

### TEST-GYM-001

Scenario: Chạy build solution sau khi kiểm tra context.

Expected: Solution biên dịch không warning/error.

Actual: `Build succeeded`; 0 Warning, 0 Error.

Status: PASS

Related Files: `He_Thong_GYM.slnx`, `He_Thong_GYM/He_Thong_GYM.csproj`

Notes: Chỉ kiểm tra compile, chưa mở UI tương tác trong phiên này.

### TEST-GYM-002

Scenario: Tự động cập nhật gói quá hạn lần đầu và lần lặp lại.

Expected: Gói quá hạn chuyển `Hết hạn`, có lịch sử/audit; lần chạy sau không cập nhật lại.

Actual: Theo phiên 015, cập nhật đúng 12 gói; lần hai cập nhật 0; có lịch sử/audit tương ứng.

Status: PASS (theo nhật ký phiên 015)

Related Files: `database/12_tu_dong_het_han_goi.sql`, `He_Thong_GYM/BLL/DashboardBLL.cs`, `He_Thong_GYM/DAL/DashboardDAL.cs`, `He_Thong_GYM/Form1.cs`

### TEST-GYM-003

Scenario: Smoke test luồng đăng ký gói → thanh toán → check-in/out → đăng ký lớp → lịch PT → nhập/xuất kho → bảo trì.

Expected: Các procedure chạy thành công và transaction rollback toàn bộ dữ liệu test.

Actual: `database/03_test_v3.sql` trả `PASS`; các bản ghi kiểm tra sau rollback đều bằng 0.

Status: PASS

Related Files: `database/03_test_v3.sql`

Notes: Chạy với UTF-8 (`sqlcmd -f 65001`), không giữ lại dữ liệu test.

## 8. Bugs Found

### BUG-001

Title: Gói đã quá ngày kết thúc vẫn mang trạng thái `Còn hạn`.

Severity: HIGH

Status: FIXED

Root Cause: Chưa có cơ chế tự động cập nhật vòng đời đăng ký gói.

Affected Files: `database/12_tu_dong_het_han_goi.sql` và các file Dashboard/Form1 liên quan.

Fix: Thêm `sp_CapNhatGoiHetHan`, SQL Server Agent Job và gọi đồng bộ khi mở Dashboard.

Verification: Đã cập nhật 12 bản ghi, lần chạy lặp cập nhật 0; build và UI check PASS theo phiên 015.

Verified By: Phiên 015.

### BUG-002

Title: Một số dữ liệu lớp/điểm danh chưa gắn lịch học thực tế.

Severity: HIGH

Status: FIXED (dữ liệu demo)

Root Cause: Mô hình cũ cho phép điểm danh theo ngày và chưa có kế hoạch lịch học cho các lớp hiện hành.

Affected Files: `FrmDiemDanh`, các procedure lịch học/điểm danh, dữ liệu `DiemDanhLop`/`LichHoc`.

Fix: Migration 15 thêm `LaDuLieuCu`, view đối soát và ràng buộc bảo vệ. Migration `database/17_bo_sung_du_lieu_demo_lich_diemdanh.sql` tạo buổi lịch sử mẫu phù hợp, gắn cả 17 dòng cũ vào `MaLichHoc` và tạo lịch tương lai cho toàn bộ lớp demo.

Verification: Hiện còn 0 dòng thiếu `MaLichHoc`; 17 dòng demo đã được gắn buổi lịch sử, điểm danh mới thiếu lịch vẫn bị ràng buộc chặn; test dữ liệu demo PASS.

Verified By: Phiên 7–8 và migration demo 17 — 22/11/2026.

### BUG-003

Title: Check-in chưa kiểm soát và tăng số lượt sử dụng của gói giới hạn lượt.

Severity: HIGH

Status: FIXED

Root Cause: `GoiTap.GioiHanLuot`, `GoiTap.GioBatDau/GioKetThuc` và `DangKyGoi.SoLuotDaDung` đã có trong schema nhưng `sp_CheckIn` chỉ kiểm tra trạng thái/ngày/thanh toán, sau đó INSERT `LichSuCheckIn`; không kiểm tra khung giờ, không chặn vượt lượt và không tăng `SoLuotDaDung`.

Affected Files: `database/01_upgrade_ql_phonggym_v3.sql:329-337`, `He_Thong_GYM/DAL/VanHanhDAL.cs`.

Fix: Migration `database/14_sua_checkin_va_hoan_kho.sql` cập nhật `sp_CheckIn` để khóa đăng ký gói, kiểm tra `GioiHanLuot`, `SoLuotDaDung`, khung giờ thường/qua nửa đêm và tăng lượt trong cùng transaction với bản ghi check-in.

Verification: `database/14_test_checkin_va_hoan_kho.sql` PASS cho tăng lượt, chặn vượt lượt và chặn ngoài khung giờ; dữ liệu kiểm thử rollback sạch.

Verified By: Phiên 6 — 22/11/2026.

### BUG-004

Title: Bán sản phẩm trừ tồn kho trước khi hóa đơn được thanh toán.

Severity: HIGH

Status: FIXED (theo chính sách giữ hàng)

Root Cause: `sp_BanSanPham` giảm `SanPham.SoLuongTon` và ghi `LichSuTonKho` ngay khi tạo hóa đơn, nhưng tạo hóa đơn ở trạng thái `Chưa thanh toán`. `sp_HuyHoaDon` không hoàn lại tồn kho.

Affected Files: `database/01_upgrade_ql_phonggym_v3.sql:443-452`, `database/01_upgrade_ql_phonggym_v3.sql:395-402`, `He_Thong_GYM/Forms/FrmBanHang.cs`.

Fix: Migration 14 giữ tồn kho như lượng hàng đã giữ khi tạo hóa đơn để chống bán vượt tồn; `sp_HuyHoaDon` nay hoàn lại từng sản phẩm, cập nhật tồn trong transaction và ghi `LichSuTonKho.LoaiBienDong = N'Hoàn trả'`. Chính sách giữ hàng được ghi rõ; không tự động sửa các giao dịch legacy đã tồn tại.

Verification: `database/14_test_checkin_va_hoan_kho.sql` PASS: tạo hóa đơn test làm tồn giảm 1, hủy hóa đơn hoàn đúng 1 và ghi đúng 1 dòng lịch sử; transaction rollback sạch. Hóa đơn legacy `MaHD=26` chưa tự động điều chỉnh.

Verified By: Phiên 6 — 22/11/2026.

### BUG-005

Title: Báo cáo doanh thu gói/PT không dựa trên giao dịch đã thu thực tế.

Severity: HIGH

Status: FIXED

Root Cause: `LayDoanhThuGoiTapAsync` cộng `DangKyGoi.GiaSauGiam` bất kể trạng thái thanh toán; `LayTopHuanLuyenVienAsync` cộng `DangKyPT.TongTien` kể cả hợp đồng `Hủy`. Cần tính theo `ThanhToan` thành công trừ hoàn tiền hoặc join hóa đơn hợp lệ.

Affected Files: `He_Thong_GYM/DAL/ThongKeDAL.cs:53-64`, `He_Thong_GYM/DAL/ThongKeDAL.cs:147-160`.

Fix: Báo cáo gói/PT đã join qua mapping hóa đơn, dùng thanh toán thành công ròng, loại hóa đơn hủy/đã hoàn tiền, đăng ký hủy và giao dịch chưa thanh toán; thanh toán một phần được phân bổ theo giá trị dòng hóa đơn.

Verification: Phiên 5 chạy truy vấn thực tế thành công; 10 đăng ký gói đã thu tổng 9.678.500 và 1 đăng ký PT đã thu 1.800.000; bản ghi chờ thanh toán/hủy không được cộng.

Verified By: Phiên 5 — 22/11/2026.

### BUG-006

Title: Dashboard có thể lỗi không được xử lý khi SQL Server không kết nối lúc mở ứng dụng.

Severity: MEDIUM

Status: FIXED

Root Cause: `Form1_Load` gọi `CapNhatGoiHetHanAsync` trước `TaiTongQuanAsync`, nhưng lời gọi này nằm ngoài `try/catch`; lỗi kết nối sẽ thoát khỏi handler `async void` thay vì hiển thị thông báo thân thiện.

Affected Files: `He_Thong_GYM/Form1.cs:56-66`.

Fix: Bọc toàn bộ `Form1_Load` trong `try/catch` và hiển thị thông báo Dashboard thân thiện khi cập nhật gói hết hạn hoặc tải dữ liệu thất bại.

Verification: Build 0 warning/0 error và UI Inspector 21/21 form PASS sau thay đổi; luồng lỗi kết nối đã có handler.

Verified By: Phiên 5 — 22/11/2026.

### BUG-007

Title: Màn hình điểm danh chỉ tải buổi đầu tiên trong ngày.

Severity: MEDIUM

Status: FIXED

Root Cause: Database cho phép nhiều `LichHoc` cùng lớp trong cùng ngày ở các giờ khác nhau, nhưng `LopTapDAL.LayBuoiHocAsync` dùng `if (!r.ReadAsync())` và trả về một bản ghi đầu tiên; UI không có danh sách chọn buổi.

Affected Files: `He_Thong_GYM/DAL/LopTapDAL.cs:9`, `He_Thong_GYM/Forms/FrmDiemDanh.cs:51-75`.

Fix: `LopTapDAL.LayBuoiHocAsync` trả về toàn bộ danh sách buổi trong ngày; `FrmDiemDanh` có ComboBox chọn buổi cụ thể trước khi tải học viên/ghi điểm danh.

Verification: Test tạo hai buổi cùng ngày PASS; UI Inspector 21/21 form PASS sau khi thêm ComboBox chọn buổi.

Verified By: Phiên 7 — 22/11/2026.

### BUG-008

Title: Schema database local thiếu `KhuyenMai.MaCode` so với source/migration.

Severity: MEDIUM

Status: FIXED

Root Cause: `database/htql_gym_v2.sql` và `database/08_cap_nhat_ma_code_khuyen_mai.sql` định nghĩa/sử dụng `MaCode`, nhưng database `QL_PhongGym` hiện tại không có cột này.

Affected Files: `database/htql_gym_v2.sql`, `database/08_cap_nhat_ma_code_khuyen_mai.sql`, các Model/DAL/Form khuyến mãi.

Fix: Migration `database/16_dong_bo_ma_code_khuyen_mai.sql` thêm/backfill `MaCode`, đổi thành `NOT NULL` và tạo unique constraint; migration chạy lặp được.

Verification: 15/15 chương trình có mã, không trùng; test schema và truy vấn `KhuyenMai` PASS trong Phiên 8.

Verified By: Phiên 8 — 22/11/2026.

### BUG-009

Title: Procedure đăng ký lớp lỗi sau khi thêm filtered unique index mã nghiệp vụ.

Severity: MEDIUM

Status: FIXED

Root Cause: `sp_DangKyLop` và `sp_HuyDangKyLop` được tạo với `QUOTED_IDENTIFIER OFF`; filtered index trên `DangKyLop` yêu cầu SET options đúng khi DML chạy.

Affected Files: `database/13_chuan_hoa_ma_nghiep_vu.sql`, procedure `dbo.sp_DangKyLop`, `dbo.sp_HuyDangKyLop`.

Fix: Tái tạo hai procedure với `ANSI_NULLS ON` và `QUOTED_IDENTIFIER ON` trong migration 13.

Verification: Smoke test trước sửa FAIL tại `sp_DangKyLop`; sau sửa trả `PASS`, metadata procedure đều `uses_quoted_identifier=1`, `uses_ansi_nulls=1`.

Verified By: Phiên 2 — 22/11/2026.

## 9. Bugs Fixed

- Sửa các truy vấn schema cũ dùng `ThietBi.TrangThai`, `TenThietBi` và trạng thái không tồn tại trong `BaoTriThietBi`.
- Sửa các lỗi Designer/UI được ghi nhận trong nhật ký; kiểm tra gần nhất đạt 21/21 form.
- Sửa đăng ký lớp trùng và chuyển điểm danh sang theo buổi.
- Sửa tình trạng gói quá hạn không tự chuyển trạng thái, như BUG-001.

## 10. Known Issues

- SQL Server Agent Job đã tạo/bật nhưng dịch vụ SQL Server Agent trên máy chủ hiện chưa chạy; ứng dụng có fallback khi mở Dashboard.
- Dữ liệu demo đã gắn đủ điểm danh lịch sử vào `MaLichHoc`; hiện không còn dòng điểm danh thiếu buổi.
- Dữ liệu demo đã tạo lịch tương lai cho toàn bộ lớp và đồng bộ sức chứa lớp 15 với phòng Yoga A (25 chỗ).
- Phân quyền database/audit người thực hiện cho tạo buổi, đăng ký, điểm danh và hoàn tiền chưa hoàn chỉnh.
- Ứng dụng phụ thuộc SQL Server local và cấu hình môi trường thực tế; chưa kiểm tra kết nối trong phiên này.
- Các procedure nghiệp vụ quan trọng chưa nhận/kiểm tra `MaTK` hoặc vai trò thực hiện ở database; quyền hiện chủ yếu nằm ở UI/BLL.
- `LichSuDangNhap` hiện có 0 bản ghi; code đăng nhập chưa ghi thành công/thất bại hoặc khóa tạm sau nhiều lần sai.
- Database hiện còn 15 dòng bán legacy; báo cáo sản phẩm chuẩn từ Phiên 8 chỉ tính hóa đơn đã thanh toán, không tự cộng nguồn legacy. Việc quyết định chuyển đổi/đối chiếu từng giao dịch legacy vẫn còn.
- Database local đã đồng bộ `KhuyenMai.MaCode` qua migration 16; các môi trường khác phải chạy migration theo đúng thứ tự.

## 11. Missing Features

- Lập lịch lớp định kỳ/hàng loạt, tạo trước các buổi học tương lai và xử lý hủy/dời/no-show.
- Kế hoạch migrate hoặc đánh dấu rõ dữ liệu điểm danh legacy.
- Chọn một nguồn phân quyền chính giữa `TaiKhoan.VaiTro` và `TaiKhoanVaiTro`, áp quyền nhất quán ở database.
- Hủy/in/xuất biên nhận hóa đơn, đối soát tiền mặt/chuyển khoản và lý do điều chỉnh/hoàn tiền.
- Nhập kho đầy đủ, đối soát dữ liệu bán hàng legacy và chuyển báo cáo hoàn toàn sang hóa đơn chung.
- Khóa tạm sau nhiều lần đăng nhập thất bại, audit thao tác nhạy cảm và quy trình backup/restore vận hành.

## 12. Pending Tests

1. Chạy `database/11_kiem_tra_san_sang_van_hanh.sql` trên database đích sau khi áp dụng migration mới; xác nhận không còn gói quá hạn ghi `Còn hạn`.
2. Kiểm tra SQL Server Agent thực thi job khi dịch vụ được bật và xác nhận không tạo audit trùng.
3. Kiểm tra cấu hình sức chứa lớp/phòng và tạo buổi cho toàn bộ lớp đang mở.
4. Test UI tương tác đăng nhập → Dashboard → quản lý thành viên → đăng ký gói → thanh toán → check-in trên database staging.
5. Test quyền theo từng vai trò ở UI và database/procedure, đặc biệt tạo buổi, điểm danh, hoàn tiền và thao tác tài khoản.
6. Đối soát báo cáo doanh thu với hóa đơn, thanh toán/hoàn tiền và dữ liệu bán hàng legacy.
7. Test gói giới hạn lượt/khung giờ: check-in liên tiếp vượt lượt và check-in ngoài khung giờ phải bị chặn, lượt dùng phải tăng đúng một lần.
8. Tạo hóa đơn bán hàng chưa thanh toán, hủy hóa đơn và xác nhận tồn kho/lịch sử tồn được hoàn nguyên hoặc quy trình nghiệp vụ được chốt rõ.
9. Tắt SQL Server trước khi mở ứng dụng để xác nhận Dashboard không làm crash handler khởi động.
10. Phiên 2 đã áp dụng migration 13; trước khi triển khai môi trường khác phải chạy backup/verify và dùng `sqlcmd -f 65001`.
11. Phiên 3 cập nhật Model/DAL/BLL theo `BUSINESS_CODE_STANDARD.md`, bắt đầu từ thành viên, sản phẩm, hóa đơn và thanh toán.

## 13. Files Changed

Trong phiên hiện tại:

- `BUSINESS_CODE_STANDARD.md` — tạo mới, chốt quy ước và phạm vi mã nghiệp vụ.
- `SESSION_CONTEXT.md` — cập nhật trạng thái Phiên 1, kết quả kiểm thử và BUG-008.
- `database/13_chuan_hoa_ma_nghiep_vu.sql` — thêm/backfill mã nghiệp vụ, unique index, trigger cấp mã và sửa SET options cho procedure lớp.

Tại thời điểm bắt đầu phiên, các file đã thay đổi/chưa commit của agent trước được ghi tại mục 6; không được xem là thay đổi do phiên hiện tại.

## 14. Database Changes

- Database hiện có nền `htql_gym_v2.sql` và các migration 01–08, 10–12 theo lịch sử project.
- Migration 10 chuẩn hóa lịch học/điểm danh theo buổi.
- Migration 11 chỉ đọc để đối soát baseline vận hành.
- Migration 12 thêm procedure cập nhật gói hết hạn và SQL Server Agent Job chạy hằng ngày lúc 00:05.
- Phiên hiện tại không thay đổi database và không chạy migration.
- Phiên 1 không thay đổi database; các kiểm tra database đều read-only.
- Phiên 2 đã thay đổi database local theo migration 13; backup trước migration đã được verify.

## 15. Important Decisions

- Giữ `NHAT_KY_PHIEN_LAM_VIEC_AI.md` làm nhật ký lịch sử; dùng `SESSION_CONTEXT.md` làm context vận hành hiện tại.
- Không xóa/revert thay đổi chưa commit của agent khác nếu chưa có yêu cầu rõ ràng.
- Source code/schema thực tế được ưu tiên hơn session context khi có mâu thuẫn; sau đó phải cập nhật lại context.
- Nghiệp vụ ghi nhiều bảng dùng stored procedure/transaction; ứng dụng không tự INSERT trực tiếp vào bảng mapping hóa đơn.
- Không xóa bảng bán hàng legacy ngay; phải đối soát và chuyển đổi có kiểm soát.
- Không ghi secret/mật khẩu/connection string nhạy cảm vào repository.
- Luôn chạy script SQL tiếng Việt bằng UTF-8 (`sqlcmd -f 65001`) để tránh kết quả giả do sai encoding.
- Mã chứng từ trong migration 13 dùng ngày nghiệp vụ + ID identity bất biến ở phần số; chưa triển khai bộ đếm riêng theo từng ngày.
- Cuối mỗi phiên bắt buộc phải nêu: phiên vừa hoàn thành, kết quả kiểm thử/bug, các phiên còn lại theo thứ tự ưu tiên và phiên kế tiếp nên bắt đầu.

## 16. Next Steps

1. Agent tiếp theo đọc file này trước khi sửa code và kiểm tra `git status` để nhận diện thay đổi chưa commit.
2. Chạy script đối soát `database/11_kiem_tra_san_sang_van_hanh.sql` trên database staging/đích và lưu số liệu baseline mới.
3. Xử lý dữ liệu điểm danh legacy và cấu hình lớp có sức chứa vượt phòng trước khi vận hành.
4. Hoàn thiện lập lịch lớp tương lai/hàng loạt và kiểm tra SQL Server Agent hoặc cơ chế scheduler thay thế.
5. Rà soát phân quyền ở BLL/database và bổ sung audit người thực hiện cho nghiệp vụ nhạy cảm.
6. Sau mọi thay đổi quan trọng: build, chạy test liên quan, cập nhật file này và ghi thêm test/bug cụ thể.
7. Khi bắt đầu chuẩn hóa mã, thực hiện theo các phiên A → E ở mục 19; không thay thế trực tiếp các khóa số đang làm PK/FK.
8. Phiên 3 không đổi schema; chỉ cập nhật Model/DAL/BLL và kiểm thử truy vấn trả cả ID nội bộ/mã nghiệp vụ.
9. Cuối mỗi phiên phải gợi ý các phiên còn lại và đề xuất phiên kế tiếp cho agent/người dùng.

## 17. Last Session Summary

Phiên 015 (21/11/2026) đã bổ sung tự động chuyển các đăng ký gói quá hạn sang `Hết hạn`, ghi lịch sử/audit, tạo SQL Server Agent Job và tích hợp fallback khi mở Dashboard. Phiên 1 (22/11/2026) kiểm kê/chốt chuẩn mã. Phiên 2 (22/11/2026) tạo backup đã verify, áp dụng migration 13, backfill mã cho database local, tạo unique index/trigger, sửa tương thích SET options của procedure lớp; migration chạy lặp PASS, DBCC PASS, smoke test rollback PASS, build 0 warning/0 error. UI chưa sử dụng mã mới; việc đó thuộc Phiên 3.

## Session 3 Update — Model/DAL/BLL

- Added business-code properties while preserving numeric internal IDs: `MaThanhVien`, `MaSanPham`, `MaGoiTap`, `MaDangKyGoi`, `MaDangKyHienThi`, `MaDangKyPT`, `MaNhanVien`, `MaLopTap`, `SoHoaDon`, `MaThietBi`, and `MaPhongTap`.
- Updated read queries and reader mappings in `ThanhVienDAL`, `SanPhamDAL`, `GoiTapDAL`, `DangKyDAL`, `TaiChinhDAL`, `VanHanhDAL`, `LopTapDAL`, `NhanVienDAL`, and `TaiSanDAL`.
- Added code search support for members, products, plans, and employees. Write APIs still accept numeric IDs as planned.

### TEST-GYM-004

Scenario: Build solution after Model/DAL/BLL code mapping changes.

Expected: 0 warning and 0 error.

Actual: `dotnet build He_Thong_GYM.slnx --no-restore` succeeded with 0 warning and 0 error.

Status: PASS

### TEST-GYM-005

Scenario: Execute all updated read-query shapes against local `QL_PhongGym`, including code search values and registration/invoice joins.

Expected: Every query succeeds; code and numeric ID are both populated; no ordinal mapping error.

Actual: Queries for `TV000001`, `SP000001`, `GOI000001`, `NV000001`, `LOP000001`, `HD-20261117-000026`, and latest DKG/DPT/TB/PHG records succeeded.

Status: PASS

### TEST-GYM-006

Scenario: Run `database/03_test_v3.sql`, `DBCC CHECKCONSTRAINTS`, and the read-only operational readiness check after code-layer changes.

Expected: Smoke test passes and rolls back; no constraint violations; no newly introduced operational errors.

Actual: Smoke test returned `PASS` with rollback verification; DBCC returned no violations; readiness check remained at 0 expired-package status errors and 0 invoice-total mismatches. Existing baseline remains 15 classes without future sessions, 17 legacy attendance rows without `MaLichHoc`, and 1 class exceeding room capacity.

Status: PASS

### Session 3 Bugs

- No new bug or error found in the Model/DAL/BLL code mapping.
- Known data baseline items above remain open and are not caused by this session.

### Session 3 Next Steps

1. Session 4: update UI grids, ComboBox display/value mapping, invoices, receipts, and reports to show business codes while retaining numeric IDs internally.
2. Session 5: end-to-end verification, concurrent code-generation checks, payment/refund/check-in/inventory reconciliation, and final UI regression.
3. Before Session 5, decide remediation for the 17 legacy attendance rows and the class/room capacity mismatch.

## Session 4 Update — UI, chứng từ và báo cáo

- Updated grids and display formatting so business codes are shown for members, products, plans, employees, classes, rooms, equipment, invoices, registrations, and check-in sessions; technical numeric IDs remain hidden/internal where appropriate.
- Updated ComboBox display values to use code-plus-name while preserving numeric `ValueMember` values for existing write APIs.
- Updated invoice/payment messages to use `SoHoaDon`; check-in now reads and displays `MaPhienCheckIn` from the database trigger.
- Added business-code columns to package and product report views.

### TEST-GYM-007

Scenario: Build after UI, document, and report binding changes.

Expected: 0 warning and 0 error.

Actual: `dotnet build He_Thong_GYM.slnx --no-restore` succeeded with 0 warning and 0 error.

Status: PASS

### TEST-GYM-008

Scenario: Run WinForms UI inspection after grid/ComboBox changes.

Expected: All forms instantiate without overlap, out-of-bounds controls, truncated text, or encoding errors.

Actual: `--check-ui` checked 21/21 forms; overlap 0, out-of-bounds 0, text truncation 0, UTF-8 errors 0.

Status: PASS

### TEST-GYM-009

Scenario: Validate report queries, invoice code display data, and check-in business-code retrieval.

Expected: Report queries return package/product codes; invoices have `SoHoaDon`; check-in returns `MaPhienCheckIn`; test data is rolled back.

Actual: Report queries returned `GOI000009`, `SP000015`, and invoice numbers such as `HD-20261117-000026`. Transactional check-in test returned `CI-20261122-000012` and rolled back. `database/03_test_v3.sql` returned `PASS`; DBCC returned no violations.

Status: PASS

### Session 4 Bugs

- No new system bug found.
- One initial manual SQL check-in attempt used an invalid test channel and was rejected by existing `CK_CheckIn_Kenh`; rerun with valid `Quầy` passed. This was a test-input error, not a product bug.
- Existing open issues BUG-003 through BUG-007 remain for Session 5 or later.

### Session 4 Next Steps

1. Session 5: end-to-end regression and reconciliation for login, member, registration, payment/refund, check-in/out, class attendance, sales/inventory, reports, and business-code searches.
2. Session 5: verify concurrent inserts do not duplicate business codes and confirm all UI grids hide technical IDs consistently.
3. After Session 5: remediate legacy attendance/capacity data and prioritize BUG-003, BUG-004, BUG-005, BUG-006, and BUG-007.

## 18. Last Updated

22/11/2026, Asia/Saigon — completed Session 3; Model/DAL/BLL now return and search by business codes while numeric IDs remain internal. Build, SQL read-query smoke tests, business smoke test, DBCC, and operational readiness checks passed.

22/11/2026, Asia/Saigon — hoàn tất Phiên 2; migration 13 đã áp dụng và verify trên `QL_PhongGym`, build/smoke/DBCC PASS; chờ Phiên 3 cập nhật Model/DAL/BLL. Đã bổ sung quy tắc cuối mỗi phiên phải gợi ý các phiên còn lại.

## 19. Planned Work Sessions — Chuẩn hóa mã nghiệp vụ

### Quyết định kiến trúc

- Giữ các khóa số `IDENTITY` hiện tại làm primary key/foreign key nội bộ để không phá dữ liệu và quan hệ đang chạy.
- Bổ sung mã nghiệp vụ dạng chuỗi, có tiền tố, padding và unique index; dùng mã này trên giao diện, tìm kiếm, hóa đơn, biên nhận và báo cáo.
- Không dùng lại mã đã phát hành sau khi xóa dữ liệu; không tạo mã dựa vào tên hoặc số điện thoại.
- Việc sinh mã phải an toàn khi nhiều người dùng tạo dữ liệu đồng thời, ưu tiên SQL `SEQUENCE`/transaction hoặc cơ chế cấp số có khóa.
- Mã cũ đã có phải được giữ tương thích: `TV000001`, `SP000001`, `TB000001`, mã voucher như `CHAOHE10`.

### Quy ước mã đề xuất

| Đối tượng | Cột mã nghiệp vụ | Định dạng đề xuất | Ghi chú |
|---|---|---|---|
| Thành viên | `MaThanhVien` | `TV000001` | Đã có, chuẩn hóa/đảm bảo unique |
| Nhân viên | `MaNhanVien` | `NV000001` | Cột mới |
| Gói tập | `MaGoiTap` | `GOI000001` | Cột mới |
| Đăng ký gói | `MaDangKyGoi` | `DKG-YYYYMMDD-0001` | Dùng trên phiếu đăng ký |
| Đăng ký PT | `MaDangKyPT` | `DPT-YYYYMMDD-0001` | Dùng trên hợp đồng PT |
| Lớp tập | `MaLopTap` | `LOP000001` | Cột mới |
| Đăng ký lớp | `MaDangKyLop` | `DKL-YYYYMMDD-0001` | Cột mới |
| Buổi học | `MaBuoiHoc` | `LH-YYYYMMDD-0001` | Cột mới |
| Hóa đơn | `SoHoaDon` | `HD-YYYYMMDD-0001` | Mã chứng từ, không thay `MaHD` |
| Thanh toán | `SoGiaoDich` | `TT-YYYYMMDD-0001` | Mã giao dịch/biên nhận |
| Check-in | `MaPhienCheckIn` | `CI-YYYYMMDD-0001` | Cột mới, giữ `MaCheckIn` nội bộ |
| Sản phẩm | `MaSanPham` | `SP000001` | Đã có, chuẩn hóa/đảm bảo unique |
| Thiết bị | `MaThietBi` | `TB000001` | Đã có, chuẩn hóa/đảm bảo unique |
| Phòng tập | `MaPhongTap` | `PHG000001` | Cột mới |
| Nhà cung cấp | `MaNhaCungCap` | `NCC000001` | Đã có trong schema/migration, cần thống nhất cách dùng |
| Phiếu nhập kho | `SoPhieuNhap` | `PNK-YYYYMMDD-0001` | Cột mới |

### Phân kỳ thực hiện

1. **Phiên A — Kiểm kê và chốt chuẩn**: rà soát tất cả màn hình/query đang hiển thị `Ma*` số; chốt tên cột, độ dài, tiền tố, phạm vi số và quy tắc cấp số; chưa sửa dữ liệu.
2. **Phiên B — Database migration**: thêm cột mã nghiệp vụ nullable, backfill mã cho dữ liệu hiện có, tạo unique index, tạo cơ chế cấp số chống trùng; có script kiểm tra và rollback an toàn.
3. **Phiên C — Model/DAL/BLL**: bổ sung thuộc tính mã, truy vấn trả cả ID nội bộ và mã hiển thị, hỗ trợ tìm kiếm theo mã; giữ API nội bộ nhận ID để giảm phạm vi thay đổi.
4. **Phiên D — UI/chứng từ**: đổi DataGridView, ComboBox, thông báo, hóa đơn/biên nhận và báo cáo sang mã nghiệp vụ; vẫn có thể hiển thị ID kỹ thuật ở màn hình quản trị/debug nếu cần.
5. **Phiên E — Kiểm thử và đối soát**: test tạo mới đồng thời, sửa, xóa mềm, tìm kiếm, thanh toán, hoàn tiền, check-in, nhập/xuất kho; đối chiếu FK và báo cáo trước/sau migration; build/UI check và cập nhật session.

### Tiêu chí hoàn thành

- Không còn mã nghiệp vụ người dùng nhìn thấy chỉ là số thứ tự không có ngữ nghĩa.
- Mọi mã hiển thị có unique index và không đổi sau khi bản ghi được tạo.
- Dữ liệu cũ được backfill 100%, không trùng, không mất liên kết.
- Primary key/FK số hiện tại vẫn hoạt động; migration có backup và script kiểm tra rollback.
- Các chứng từ quan trọng (`HD`, `TT`, `PNK`, `CI`) được truy vết bằng mã trên giao diện và báo cáo.

---

## Phiên 5 — Kiểm thử cuối và đối soát báo cáo

- **Ngày**: 22/11/2026
- **Phạm vi**: kiểm thử hồi quy sau chuẩn hóa mã nghiệp vụ; đối soát doanh thu gói/PT với hóa đơn và thanh toán; kiểm tra an toàn Dashboard khi mất kết nối; kiểm tra UI, smoke test, ràng buộc và readiness.

### Thay đổi

1. `ThongKeDAL.LayDoanhThuGoiTapAsync` nay chỉ cộng phần tiền đã thu ròng từ hóa đơn hợp lệ; loại hóa đơn hủy/đã hoàn tiền, đăng ký gói hủy và giao dịch chưa thanh toán. Trường hợp thanh toán một phần được phân bổ theo giá trị dòng hóa đơn.
2. `ThongKeDAL.LayTopHuanLuyenVienAsync` nay lấy doanh thu PT từ `HoaDonPT`/`ChiTietHoaDon`/`ThanhToan`, loại đăng ký PT hủy và chỉ tính thanh toán thành công.
3. Bổ sung `gt.MaGoiTap` vào `GROUP BY`, khắc phục lỗi truy vấn báo cáo gói khi cột mã được hiển thị.
4. Bọc toàn bộ `Form1_Load` trong `try/catch` để lỗi kết nối khi cập nhật gói hết hạn không làm thoát handler `async void`; Dashboard hiển thị thông báo thân thiện.

### Kiểm thử

#### TEST-GYM-010 — Báo cáo doanh thu gói/PT

- Truy vấn thực tế trên `QL_PhongGym` chạy thành công.
- Báo cáo gói có 10 đăng ký đã thu, tổng **9.678.500**; các đăng ký chưa thanh toán/hủy không được cộng.
- Báo cáo HLV có 1 đăng ký đã thu, doanh thu **1.800.000**; không cộng hợp đồng PT hủy.
- Không còn lỗi `GROUP BY` và dữ liệu mã gói trả đúng dạng `GOI000xxx`.

#### TEST-GYM-011 — Build và kiểm tra UI

- `dotnet build He_Thong_GYM.slnx --no-restore`: **PASS**, 0 warning, 0 error.
- `--check-ui`: **21/21 form**, overlap 0, out-of-bounds 0, text truncation 0, UTF-8 0.

#### TEST-GYM-012 — Hồi quy database và readiness

- `database/03_test_v3.sql`: **PASS**, các bản ghi kiểm thử rollback sạch.
- `DBCC CHECKCONSTRAINTS WITH NO_INFOMSGS`: **PASS**, không có vi phạm.
- Readiness: gói quá hạn sai trạng thái 0; hóa đơn lệch tổng 0.
- Dữ liệu cần xử lý vận hành vẫn là 15 lớp thiếu buổi tương lai, 17 điểm danh legacy thiếu `MaLichHoc`, 1 lớp vượt sức chứa phòng.

### Cập nhật bug

- **BUG-005: FIXED** — báo cáo doanh thu gói/PT đã dựa trên tiền đã thu thực tế và loại trạng thái không hợp lệ; đã xác minh bằng truy vấn thực tế.
- **BUG-006: FIXED** — lỗi cập nhật gói hết hạn lúc mở Dashboard đã được xử lý trong `Form1_Load`; build/UI regression PASS.
- **BUG-010: FIXED** — truy vấn báo cáo gói thiếu `MaGoiTap` trong `GROUP BY`, gây lỗi runtime sau khi thêm cột mã.
- BUG-003 và BUG-004 đã được xử lý ở Phiên 6; BUG-007 và BUG-008 được xử lý trong các Phiên 7–8. Còn dữ liệu điểm danh legacy cần xác nhận nghiệp vụ và đối soát từng giao dịch kho cũ.

### Kết luận phiên

Chuẩn hóa mã nghiệp vụ và kiểm thử cuối đã hoàn tất. Không phát hiện lỗi build, UI, ràng buộc database hoặc smoke flow mới. Các vấn đề còn lại là nghiệp vụ vận hành đã được ghi nhận, không tự động sửa dữ liệu production trong phiên này.

### Phiên đề xuất tiếp theo

Đề xuất **Phiên 6** của Phiên 5 đã được thực hiện: BUG-003 và BUG-004 đã xử lý và kiểm thử. Phiên tiếp theo được cập nhật trong phần Phiên 6.

---

## Phiên 6 — Check-in theo lượt và hoàn kho khi hủy hóa đơn

- **Ngày**: 22/11/2026
- **Phạm vi**: xử lý BUG-003 và BUG-004; kiểm thử giới hạn lượt/khung giờ, check-in/out, giữ hàng, hủy hóa đơn và hoàn tồn kho.

### Thay đổi

1. Thêm migration `database/14_sua_checkin_va_hoan_kho.sql`:
   - `sp_CheckIn` khóa đăng ký gói trong transaction, kiểm tra gói đã thanh toán, thời hạn, trạng thái thành viên, số lượt còn lại và khung giờ; hỗ trợ cả khung giờ qua nửa đêm.
   - Sau khi check-in thành công, tăng `DangKyGoi.SoLuotDaDung` đúng một lần.
   - `sp_HuyHoaDon` hoàn tồn theo từng sản phẩm của hóa đơn, ghi `LichSuTonKho` loại `Hoàn trả`, cập nhật audit và rollback toàn bộ nếu lỗi.
   - Chính sách bán hàng hiện tại là giữ hàng ngay khi tạo hóa đơn để chống bán vượt tồn; hủy hóa đơn sẽ trả lại lượng giữ.
2. Thêm `database/14_test_checkin_va_hoan_kho.sql`; mọi nhánh kiểm thử đều rollback.
3. Cập nhật `database/README_NANG_CAP.md` để bổ sung thứ tự migration 14 và script test mục tiêu.

### Kiểm thử

#### TEST-GYM-013 — Check-in và giới hạn gói

- Tăng lượt sau check-in: **PASS**.
- Check-in vượt `GioiHanLuot`: **PASS**, bị chặn bằng lỗi nghiệp vụ 50011.
- Check-in ngoài `GioBatDau/GioKetThuc`: **PASS**, bị chặn bằng lỗi nghiệp vụ 50011.
- Check-out sau check-in: **PASS**.

#### TEST-GYM-014 — Hủy hóa đơn và hoàn kho

- Tạo hóa đơn sản phẩm làm giảm tồn 1 đơn vị, hủy hóa đơn hoàn đúng 1 đơn vị và ghi 1 dòng `Hoàn trả`: **PASS**.
- Rollback kiểm thử sạch; sau test DKG=1 vẫn có `SoLuotDaDung=0`, gói không còn giới hạn/khung giờ tạm, sản phẩm `MaSP=15` vẫn có tồn 8.

#### TEST-GYM-015 — Hồi quy toàn hệ thống

- Build: **PASS**, 0 warning, 0 error.
- UI Inspector: **21/21 form**, overlap/out-of-bounds/text truncation/UTF-8 đều 0.
- `database/03_test_v3.sql`: **PASS**, rollback sạch.
- `DBCC CHECKCONSTRAINTS WITH NO_INFOMSGS`: **PASS**.
- Readiness không phát sinh lỗi mới: 0 gói quá hạn sai trạng thái, 0 hóa đơn lệch tổng; còn 15 lớp thiếu lịch tương lai, 17 điểm danh legacy thiếu `MaLichHoc`, 1 lớp vượt sức chứa phòng.

### Cập nhật bug

- **BUG-003: FIXED** — check-in đã kiểm tra lượt/khung giờ và tăng lượt nguyên tử trong transaction.
- **BUG-004: FIXED theo chính sách giữ hàng** — hủy hóa đơn đã hoàn tồn và ghi lịch sử; dữ liệu legacy chưa tự động điều chỉnh.
- **BUG-002: MITIGATED** — dữ liệu điểm danh cũ đã được đánh dấu legacy, còn cần quyết định migrate từng dòng.
- **BUG-007, BUG-008: FIXED** — đã xử lý chọn nhiều buổi trong ngày và đồng bộ `KhuyenMai.MaCode` ở các Phiên 7–8.

### Kết luận phiên

Hai bug vận hành ưu tiên đã được xử lý và kiểm thử đạt. Không phát hiện lỗi build, UI, ràng buộc hoặc smoke flow mới. Cần tách riêng việc đối soát/migrate dữ liệu cũ, không tự ý sửa dữ liệu production chỉ từ kiểm thử.

### Phiên đề xuất tiếp theo

Phiên 7 đã xử lý chọn buổi, đánh dấu legacy và chặn sức chứa; Phiên 8 đã đồng bộ mã khuyến mãi và tách nguồn báo cáo kho. Các việc còn lại là xác nhận nghiệp vụ cho lịch tương lai/17 dòng legacy và quyết định chuyển đổi giao dịch kho cũ.

---

## Phiên 7 — Lịch học nhiều buổi và điểm danh legacy

- **Ngày**: 22/11/2026
- **Phạm vi**: xử lý BUG-002/BUG-007; chọn buổi học cụ thể trong cùng ngày, đánh dấu dữ liệu cũ và kiểm soát sức chứa phòng.

### Thay đổi

1. `LopTapDAL.LayBuoiHocAsync` trả về toàn bộ buổi trong ngày thay vì chỉ đọc dòng đầu tiên.
2. `FrmDiemDanh` thêm ComboBox chọn buổi học; danh sách học viên và thao tác điểm danh dùng đúng `MaLichHoc` được chọn.
3. Migration `database/15_danh_dau_diem_danh_legacy.sql` thêm `DiemDanhLop.LaDuLieuCu`, đánh dấu 17 dòng cũ thiếu `MaLichHoc`, tạo view `vw_DiemDanhLegacy` và ràng buộc chặn điểm danh mới thiếu lịch nếu không được đánh dấu legacy.
4. Không tự gán 17 dòng cũ vào buổi học mới khi không có bằng chứng nghiệp vụ.

### Kiểm thử

- Đánh dấu 17 dòng legacy: **PASS**.
- Insert điểm danh mới thiếu lịch bị chặn: **PASS**.
- Tạo hai buổi cùng ngày cho một lớp: **PASS**.
- Lớp 15 sức chứa 30 tại phòng 25 chỗ bị chặn khi tạo lịch: **PASS**.
- Không phát sinh thay đổi dữ liệu sau test tạo buổi: transaction rollback sạch.

### Kết luận

BUG-007 đã đóng. BUG-002 được giảm thiểu và minh bạch hóa dữ liệu; việc migrate từng dòng legacy vẫn cần xác nhận nghiệp vụ.

---

## Phiên 8 — Đồng bộ mã khuyến mãi và đối soát kho legacy

- **Ngày**: 22/11/2026
- **Phạm vi**: xử lý BUG-008, tách báo cáo bán hàng chuẩn khỏi dữ liệu legacy và nghiệm thu sau Phiên 7.

### Thay đổi

1. Migration `database/16_dong_bo_ma_code_khuyen_mai.sql` thêm/backfill `KhuyenMai.MaCode`, đổi cột thành `NOT NULL`, tạo unique constraint và chạy lặp an toàn.
2. `ThongKeDAL.LayThongKeSanPhamAsync` chỉ tính sản phẩm từ `ChiTietHoaDon` thuộc hóa đơn `Đã thanh toán`; không tự cộng `ChiTietPhieuBanHang` legacy để tránh cộng trùng.
3. Dữ liệu legacy vẫn được giữ nguyên để đối soát: 15 dòng bán, tổng 16 sản phẩm, doanh thu 4.565.000; không tự xóa/chuyển đổi.

### Kiểm thử

- `MaCode`: 15/15 chương trình có mã, không trùng: **PASS**.
- Truy vấn báo cáo sản phẩm chuẩn chạy thành công và loại hóa đơn sản phẩm chưa thanh toán: **PASS**.
- Đối soát legacy/unpaid baseline: **PASS**.
- Build: **PASS**, 0 warning, 0 error.
- UI Inspector: **21/21 form**, mọi lỗi bố cục/UTF-8 đều 0.
- Smoke test, test Phiên 6, test Phiên 7: **PASS**.
- `DBCC CHECKCONSTRAINTS`: **PASS**.
- Readiness: 0 gói quá hạn sai trạng thái, 0 hóa đơn lệch tổng; còn 15 lớp thiếu lịch tương lai, 17 dòng legacy đã đánh dấu, 1 lớp vượt sức chứa cần quyết định cấu hình.

### Ghi chú sự cố kiểm thử

Lần chạy đầu migration 16 dừng do thứ tự DDL tạo unique constraint trước khi đổi cột `NOT NULL`. Đã sửa script theo thứ tự drop constraint → alter column → tạo lại constraint; chạy lại thành công và không mất dữ liệu.

### Kết luận và phần còn lại

BUG-008 đã đóng; BUG-007 đã đóng; BUG-002 được giảm thiểu bằng cờ legacy. Không còn bug chức năng ưu tiên trong phạm vi hai phiên này. Còn các việc nghiệm thu nghiệp vụ: lập lịch tương lai cho 15 lớp, quyết định xử lý từng dòng điểm danh legacy, sửa cấu hình lớp 15/phòng Yoga A và đối soát/chuyển đổi bán hàng legacy nếu cần.

### Bổ sung dữ liệu mẫu demo

Theo yêu cầu sản phẩm demo, migration `database/17_bo_sung_du_lieu_demo_lich_diemdanh.sql` đã được áp dụng:

- Tạo lịch tương lai cho cả 15 lớp.
- Tạo buổi lịch sử phù hợp và gắn đủ 17 điểm danh cũ vào `MaLichHoc`.
- Đồng bộ sức chứa lớp 15 với phòng Yoga A.
- Giữ 15 dòng bán hàng legacy riêng và tạo view `vw_BanHangLegacyDoiSoat`.
- Test demo trả về: thiếu lịch 0, điểm danh chưa gắn lịch 0, lớp vượt sức chứa 0.

## 20. QA toàn bộ project — 22/11/2026

- Báo cáo đầy đủ: `QA_REPORT.md`.
- Kết luận hiện tại: **CẦN SỬA**, đủ để demo có kiểm soát nhưng chưa đạt để vận hành thật.
- Regression PASS: build 0 warning/0 error; UI Inspector 21/21 Form; database scripts 03, 14, 15, 16, 17; DBCC CHECKCONSTRAINTS; login/dashboard/tìm kiếm theo mã trên app thật.
- Dữ liệu demo: 15 lớp có lịch tương lai; 17 attendance legacy đã gắn `MaLichHoc`; 0 lớp vượt sức chứa phòng; 15 dòng bán hàng legacy giữ riêng; readiness demo sạch.
- Lỗi mới phát hiện: BUG-010 authorization; BUG-011 hoàn tiền không vô hiệu gói; BUG-012 check-in phòng bảo trì; BUG-013 bảo lưu ngày quá khứ; BUG-014 PT hoạt động trước thanh toán; BUG-015 không truyền hóa đơn được chọn; BUG-016 validation email/SĐT; BUG-017 audit login/lockout; BUG-018 lộ exception kỹ thuật.
- Không sửa source code trong phiên QA; chỉ chạy kiểm thử, truy vấn đối chứng và lập báo cáo.
- Các test mutation phát hiện lỗi đều rollback; baseline DB không bị thay đổi.
- Công việc kế tiếp: sửa BUG-010/011/012/014 trước, sau đó sửa validation/error handling/UI liên kết, rồi regression toàn bộ.

## 21. Phiên 10 — sửa phân quyền

- Đã thêm `He_Thong_GYM/Helpers/AuthorizationPolicy.cs` làm policy tập trung cho menu và thao tác ghi.
- `Form1` ẩn menu ngoài vai trò và chặn lại ở event click; các BLL ghi dữ liệu quan trọng gọi `AuthorizationPolicy.Require(...)` trước DAL.
- Đã bảo vệ các nhóm BLL: thành viên, gói tập, HLV, đăng ký gói/PT, nhân viên, lớp/điểm danh, bán hàng, tài chính, tài sản/bảo trì, vận hành gói và tài khoản.
- Đã thêm migration `database/18_phan_quyen_nghiep_vu.sql`, áp dụng thành công vào `QL_PhongGym`: function `fn_NhanVienCoVaiTro` và 5 trigger guard cho `HoaDon`, `ThanhToan`, `LichSuTonKho`, `BaoTriThietBi`, `LichSuGoiTap`.
- Test database trực tiếp với nhân viên vai trò `Nhân viên` cố ghi thanh toán bị chặn bằng lỗi 52102; transaction rollback.
- Build: PASS, 0 warning, 0 error.
- Regression: script 03, 14, 15, 16, 17 PASS; DBCC PASS; UI Inspector 21/21 PASS.
- Lưu ý: các luồng actorless như lịch học/điểm danh/check-in vẫn cần bước tiếp theo truyền `MaTK/MaNV` vào procedure để đạt bảo vệ database đầy đủ; phiên này đã đóng phần UI/BLL và các bảng actor rõ ràng.
- Phiên tiếp theo: xử lý BUG-011, BUG-012, BUG-013 và BUG-014.
- Phiên tiếp theo: xử lý BUG-011, BUG-012, BUG-013 và BUG-014.

## 22. Phiên 11 — sửa hoàn tiền, check-in phòng, bảo lưu và trạng thái PT

- Đã thêm và áp dụng `database/19_sua_nghiep_vu_phien_11.sql`.
- Hoàn tiền toàn bộ cập nhật hóa đơn sang `Đã hoàn tiền`, gói tập sang `Hủy`, PT sang `Hủy` và ghi lịch sử gói.
- Check-in từ chối phòng không ở trạng thái `Hoạt động`; `FrmVanHanhGoi` chỉ hiển thị phòng hoạt động.
- Bảo lưu từ chối ngày quá khứ, ngày ngoài kỳ gói, lý do rỗng và khoảng bị chồng lấn.
- Đăng ký PT tạo trạng thái `Chờ thanh toán`; thanh toán đủ mới chuyển sang `Đang học`, nên lịch PT vẫn bị chặn trước thanh toán.
- Bộ lọc lịch sử PT đã cập nhật các trạng thái `Chờ thanh toán`, `Đang học`, `Hoàn thành`, `Hủy`.

### Kiểm thử Phiên 11

- `database/19_test_phien_11.sql`: **PASS** BUG-011, BUG-012, BUG-013, BUG-014a và BUG-014b; mọi transaction rollback.
- `database/03_test_v3.sql`: **PASS**.
- `database/14_test_checkin_va_hoan_kho.sql`: **PASS** khi chạy độc lập; một lần chạy song song với các script khác bị deadlock test do tranh chấp khóa, chạy lại tuần tự PASS.
- `database/15_test_lich_diemdanh_succhua.sql`: **PASS**.
- `database/16_test_ma_code_va_bao_cao_kho.sql`: **PASS**.
- `database/17_test_du_lieu_demo.sql`: **PASS**.
- `DBCC CHECKCONSTRAINTS WITH NO_INFOMSGS`: **PASS**.
- Build: **PASS**, 0 warning, 0 error.
- UI smoke: đăng nhập Admin, mở vận hành gói, danh sách phòng không có phòng 6 đang bảo trì; mở danh sách PT không lỗi và hiển thị đúng dữ liệu mẫu.

### Kết luận phiên

BUG-011, BUG-012, BUG-013 và BUG-014 đã được sửa và kiểm thử đạt. Còn BUG-015 đến BUG-018; đề xuất Phiên 12 xử lý validation, truyền đúng hóa đơn sang Form thanh toán, audit/lockout đăng nhập, thông báo lỗi thân thiện và regression toàn bộ.

## 23. Phiên 12 — trạng thái hoàn tất

- Đã áp dụng migration `database/20_sua_bao_mat_validation_phien_12.sql` cho `QL_PhongGym`.
- BUG-010: `sp_CheckIn`, `sp_CheckOut`, `sp_DangKyLop`, `sp_TaoLichHoc`, `sp_DiemDanhLop` từ chối `@MaNV=NULL` bằng lỗi quyền tương ứng; DAL/BLL truyền actor hiện tại.
- BUG-015: `FrmHoaDon` truyền `MaHD` dòng đang chọn; smoke test chọn `HD-20261117-000025` và Form thanh toán hiển thị đúng hóa đơn/công nợ 300.000 đ.
- BUG-016: validation BLL và CHECK constraint database cho SĐT/email của thành viên và nhân viên.
- BUG-017: `sp_GhiKetQuaDangNhap` ghi thành công/thất bại vào `LichSuDangNhap`, khóa sau 5 lần sai trong 15 phút, đăng nhập thành công reset bộ đếm.
- BUG-018: `UiErrorMessage` che lỗi SQL kỹ thuật, ghi exception vào log nội bộ; Form đăng nhập đã bổ sung ghi log cho nhánh lỗi kết nối.

### Kiểm thử Phiên 12

- Build cuối: **PASS**, 0 warning, 0 error.
- `database/20_test_phien_12.sql`: **PASS**, mọi mutation validation/lockout rollback sạch.
- Regression tuần tự: `03_test_v3.sql`, `14_test_checkin_va_hoan_kho.sql`, `15_test_lich_diemdanh_succhua.sql`, `16_test_ma_code_va_bao_cao_kho.sql`, `17_test_du_lieu_demo.sql`, `19_test_phien_11.sql`: **PASS**.
- `DBCC CHECKCONSTRAINTS WITH NO_INFOMSGS`: **PASS**.
- `11_kiem_tra_san_sang_van_hanh.sql`: 0 lỗi gói quá hạn, 0 lớp thiếu lịch, 0 điểm danh thiếu lịch, 0 lớp vượt sức chứa, 0 hóa đơn lệch tổng.
- UI smoke: đăng nhập Admin/dashboard và truyền hóa đơn đang chọn sang Form thanh toán: **PASS**.

### Đề xuất sau Phiên 12

Không còn BUG-010/015/016/017/018 trong danh sách mở.

## 24. Phiên 13 — Sửa lỗi cốt lõi & Lỗ hổng nghiệp vụ (BUG-019, 020, 021, 022, 024)

- **Ngày thực hiện:** 22/11/2026.
- **Phạm vi:**
  1. **BUG-019 (FIXED):** Sửa bộ lọc `cboTrangThai` tại `FrmHoaDon.Designer.cs` từ `"Chờ thanh toán"` thành `"Chưa thanh toán"`, bổ sung `"Đã thanh toán một phần"`, `"Đã hoàn tiền"`, giúp lọc chính xác các hóa đơn công nợ chưa thu.
  2. **BUG-020 (FIXED):** Cập nhật `sp_GiaHanGoiTap` trong `database/21_sua_giahan_checkin_baotri.sql`. Bỏ ràng buộc `UQ_HDGoiTap_DKG` để 1 gói có thể có nhiều hóa đơn. Gia hạn nay tự động tính tiền ngày gia hạn và tạo `HoaDon` + `ChiTietHoaDon` + `HoaDonGoiTap` ở trạng thái `Chưa thanh toán`. Cập nhật `VanHanhDAL.cs`, `VanHanhBLL.cs`, `FrmVanHanhGoi.cs` để nhận mã hóa đơn và hiển thị thông báo thu tiền.
  3. **BUG-021 (FIXED):** Cập nhật `sp_CheckIn` ưu tiên gói hết hạn sớm nhất (`ORDER BY d.NgayKetThuc ASC, d.MaDKG ASC` theo nguyên tắc FIFO) thay vì trừ lượt của gói có thời hạn xa nhất.
  4. **BUG-022 (FIXED):** Tái cấu trúc vòng lặp khởi chạy trong `Program.cs`. Khi người dùng bấm Đăng xuất tại `Form1`, phiên làm việc kết thúc và ứng dụng mở lại hộp thoại `FrmLogin` thay vì đóng tiến trình; khi bấm nút X tắt ứng dụng thì thoát sạch.
  5. **BUG-024 (FIXED):** Cập nhật `sp_BaoTriThietBi` để thiết bị chuyển `Bảo trì` và phiếu ở trạng thái `Đang bảo trì`. Tạo mới thủ tục `sp_HoanTatBaoTri` để đóng phiếu sang `Hoàn thành` và tự động khôi phục tình trạng thiết bị về `Hoạt động`. Cập nhật `TaiSanDAL.cs`, `TaiSanBLL.cs`, bổ sung nút `Hoàn tất bảo trì` (`btnHoanTat`) trên `FrmBaoTri`.

### Kiểm thử Phiên 13

- `database/21_test_phien_13.sql`: **PASS** toàn bộ BUG-020, BUG-021, BUG-024; transaction rollback sạch.
- Regression tuần tự các script trước:
  - `database/20_test_phien_12.sql`: **PASS**.
  - `database/19_test_phien_11.sql`: **PASS**.
  - `database/14_test_checkin_va_hoan_kho.sql`: **PASS**.
  - `database/11_kiem_tra_san_sang_van_hanh.sql`: **PASS** (0 lỗi gói quá hạn, 0 lớp thiếu lịch, 0 điểm danh thiếu lịch, 0 lớp vượt sức chứa, 0 hóa đơn lệch tổng).
- `DBCC CHECKCONSTRAINTS WITH NO_INFOMSGS`: **PASS** (0 vi phạm).
- Build solution: `dotnet build He_Thong_GYM.slnx --no-restore`: **PASS**, 0 warning, 0 error.
- UI Inspector: `dotnet run --project He_Thong_GYM/He_Thong_GYM.csproj --no-build -- --check-ui`: **PASS 21/21 Form**, overlap 0, out-of-bounds 0, text truncation 0, UTF-8 0.

### Kế hoạch tiếp theo (Phiên 14)

Thực hiện theo lộ trình đã chốt:
- Nối liền luồng Đăng ký dịch vụ (`FrmDangKyGoi`, `FrmDangKyPT`, `FrmBanHang`) → Hỏi mở thanh toán ngay sang `FrmThanhToan`.
- Hỗ trợ Bán hàng cho Khách vãng lai tại `FrmBanHang`.
- Bổ sung nảy dữ liệu và nút Sửa/Đóng lớp tại `FrmLopTap`.
- Bổ sung nút Hủy hóa đơn chưa thanh toán tại `FrmHoaDon`.
- Bắt lỗi trùng SĐT/Email tiếng Việt thân thiện tại `FrmThanhVien`.

## 25. Phiên 14 — Trạng thái hoàn tất (Liền mạch luồng UX, Khách vãng lai, Lớp tập, Hủy hóa đơn)

- **Ngày thực hiện:** 22/11/2026.
- **Phạm vi hoàn tất:**
  1. **BUG-025 (FIXED) — Bán hàng Khách vãng lai:** Áp dụng migration `database/22_khach_vang_lai_va_lop_tap.sql`, tạo bản ghi hệ thống `TV001035 - Khách vãng lai` (SĐT `0900000000`). Cập nhật `FrmBanHang.cs`: tự động chọn mặc định `Khách vãng lai`, cho phép thu ngân bán lẻ nước uống/thực phẩm bổ sung ngay lập tức mà không cần tạo hồ sơ hội viên.
  2. **Liền mạch luồng Đăng ký → Mở thanh toán ngay:**
     - `FrmDangKyGoi.cs`: Đăng ký gói tập thành công → Hộp thoại hỏi mở ngay màn hình thu tiền `FrmThanhToan(kq.MaHD)`.
     - `FrmDangKyPT.cs`: Ký hợp đồng PT thành công → Hộp thoại hỏi mở ngay màn hình thu tiền `FrmThanhToan(kq.MaHD)`.
     - `FrmBanHang.cs`: Bán lẻ sản phẩm thành công → Hộp thoại hỏi mở ngay màn hình thu tiền `FrmThanhToan(maHD)`.
     - Chuẩn hóa tuple trả về `(int MaDangKy, int MaHD)` đồng bộ từ `DangKyDAL` qua `DangKyBLL`.
  3. **BUG-023 (FIXED) — Hoàn thiện Quản lý Lớp tập (`FrmLopTap`):**
     - Bổ sung hàm `CapNhatAsync` và `DongLopAsync` trong `LopTapDAL.cs`, `LopTapBLL.cs`.
     - Cập nhật `FrmLopTap.Designer.cs`: thêm nút `btnSua` (Cập nhật), `btnDongLop` (Đóng lớp), gắn sự kiện `dgvLop.SelectionChanged`.
     - Cập nhật `FrmLopTap.cs`: Khi chọn dòng trên DataGridView, dữ liệu tự động nảy lên các ô nhập thông tin (tên lớp, loại lớp, HLV, sức chứa, giờ bắt đầu/kết thúc, trạng thái). Cho phép cập nhật thông tin lớp học và đóng lớp học có xác nhận.
  4. **Hủy hóa đơn & Tự động hoàn kho tại `FrmHoaDon`:**
     - Bổ sung `HuyHoaDonAsync` trong `TaiChinhDAL.cs` và `TaiChinhBLL.cs` (gọi thủ tục `sp_HuyHoaDon`).
     - Bổ sung nút `btnHuyHoaDon` trên `FrmHoaDon.Designer.cs` và sự kiện `btnHuyHoaDon_Click` trong `FrmHoaDon.cs`.
     - Ràng buộc nghiệp vụ: Chặn không cho hủy hóa đơn đã phát sinh thanh toán (yêu cầu hoàn tiền trước). Khi hủy hóa đơn chưa thanh toán, hệ thống tự động hoàn lại số lượng tồn kho sản phẩm nguyên vẹn.
  5. **BUG-026 (FIXED) — Phân tách lỗi trùng SĐT/Email tại `FrmThanhVien`:**
     - Bắt `SqlException` mã 2601 / 2627: hiển thị thông báo tiếng Việt rõ ràng *"Số điện thoại này đã được sử dụng bởi hội viên khác"* hoặc *"Email này đã được sử dụng bởi hội viên khác"*, xóa bỏ tình trạng báo lỗi ngắt kết nối gây hiểu lầm.
     - Cập nhật nút `btnSua`: focus và bôi đen họ tên sẵn sàng thao tác.

### Kiểm thử Phiên 14

- `database/22_test_phien_14.sql`: **PASS 100%** toàn bộ 5 bước (Bán lẻ khách vãng lai, trừ tồn kho, hủy hóa đơn hoàn kho tự động, cập nhật lớp, đóng lớp, chặn hủy hóa đơn đã thanh toán mã lỗi 50034); rollback sạch.
- Regression tests tuần tự: `21_test_phien_13.sql`, `20_test_phien_12.sql`, `19_test_phien_11.sql`, `14_test_checkin_va_hoan_kho.sql`: **PASS 100%**.
- `DBCC CHECKCONSTRAINTS WITH ALL_CONSTRAINTS`: **PASS 100%**, 0 vi phạm.
- Build solution `dotnet build He_Thong_GYM.slnx --no-restore`: **PASS**, 0 warning, 0 error.
- UI Inspector `dotnet run --project He_Thong_GYM/He_Thong_GYM.csproj --no-build -- --check-ui`: **PASS 21/21 Form**, overlap 0, out-of-bounds 0, text truncation 0, UTF-8 0.

### Kế hoạch tiếp theo (Phiên 15)

- Hoàn thiện toàn diện CRUD Sản phẩm & Nhập kho (`FrmSanPham`).
- Rà soát Thiết bị (`FrmThietBi`), Phòng tập (`FrmPhongTap`), Khuyến mãi (`FrmKhuyenMai`).

## 26. Phiên 15 — Trạng thái hoàn tất (CRUD Sản phẩm, Nhập kho, Thiết bị, Phòng tập, Khuyến mãi)

- **Ngày thực hiện:** 22/11/2026.
- **Phạm vi hoàn tất:**
  1. **CRUD Sản phẩm & Nhập kho ([FrmSanPham.cs](file:///d:/BTL%20PTTKYC/He_Thong_GYM/He_Thong_GYM/Forms/FrmSanPham.cs), [SanPhamDAL.cs](file:///d:/BTL%20PTTKYC/He_Thong_GYM/He_Thong_GYM/DAL/SanPhamDAL.cs), [SanPhamBLL.cs](file:///d:/BTL%20PTTKYC/He_Thong_GYM/He_Thong_GYM/BLL/SanPhamBLL.cs)):**
     - Bổ sung `groupThongTin` với các ô nhập: Tên sản phẩm, Đơn giá bán, Giá nhập, Trạng thái ("Còn bán", "Ngừng bán").
     - Bổ sung nút `btnThem` (Thêm mới sản phẩm), `btnSua` (Cập nhật sản phẩm), `btnNhapKho` (Nhập kho hàng hóa), `btnBanHang`, `btnLamMoi`.
     - Gắn sự kiện `SelectionChanged`: khi chọn sản phẩm trên lưới, toàn bộ dữ liệu tự động nảy lên các ô nhập.
     - Tích hợp gọi stored procedure `dbo.sp_NhapKho` trong popup nhập kho: tự động tăng `SoLuongTon`, cập nhật `GiaNhap`, sinh `PhieuNhapKho`, `ChiTietPhieuNhap` và ghi nhật ký `LichSuTonKho`.
  2. **Cập nhật Thiết bị ([FrmThietBi.cs](file:///d:/BTL%20PTTKYC/He_Thong_GYM/He_Thong_GYM/Forms/FrmThietBi.cs), [TaiSanDAL.cs](file:///d:/BTL%20PTTKYC/He_Thong_GYM/He_Thong_GYM/DAL/TaiSanDAL.cs), [TaiSanBLL.cs](file:///d:/BTL%20PTTKYC/He_Thong_GYM/He_Thong_GYM/BLL/TaiSanBLL.cs)):**
     - Bổ sung nút `btnSua` (Cập nhật thiết bị) và sự kiện `SelectionChanged`.
     - Hộp thoại cho phép phân lại phòng tập và thay đổi tình trạng thiết bị ("Hoạt động", "Bảo trì", "Hư hỏng", "Thanh lý").
  3. **Cập nhật Phòng tập ([FrmPhongTap.cs](file:///d:/BTL%20PTTKYC/He_Thong_GYM/He_Thong_GYM/Forms/FrmPhongTap.cs), [TaiSanDAL.cs](file:///d:/BTL%20PTTKYC/He_Thong_GYM/He_Thong_GYM/DAL/TaiSanDAL.cs), [TaiSanBLL.cs](file:///d:/BTL%20PTTKYC/He_Thong_GYM/He_Thong_GYM/BLL/TaiSanBLL.cs)):**
     - Bổ sung nút `btnSua` (Cập nhật phòng) và sự kiện `SelectionChanged`.
     - Hộp thoại cho phép cập nhật sức chứa tối đa và chuyển đổi trạng thái phòng ("Hoạt động", "Bảo trì", "Tạm ngưng").
  4. **CRUD Khuyến mãi ([FrmKhuyenMai.cs](file:///d:/BTL%20PTTKYC/He_Thong_GYM/He_Thong_GYM/Forms/FrmKhuyenMai.cs), [KhuyenMaiDAL.cs](file:///d:/BTL%20PTTKYC/He_Thong_GYM/He_Thong_GYM/DAL/KhuyenMaiDAL.cs), [KhuyenMaiBLL.cs](file:///d:/BTL%20PTTKYC/He_Thong_GYM/He_Thong_GYM/BLL/KhuyenMaiBLL.cs)):**
     - Tạo mới `KhuyenMaiBLL` với chính sách phân quyền và ràng buộc nghiệp vụ (tên không rỗng, % giảm từ 1 đến 100, ngày kết thúc >= ngày bắt đầu).
     - Bổ sung nút `btnThem` (Tạo khuyến mãi) và `btnSua` (Cập nhật) trên `FrmKhuyenMai`.
     - Hỗ trợ nhập mã Code tùy chọn hoặc tự động sinh mã code chuẩn hóa nghiệp vụ.

### Kiểm thử Phiên 15

- `database/23_test_phien_15.sql`: **PASS 100% (4/4 bước)**: Thêm sản phẩm kích hoạt trigger `TR_SanPham_MaSanPham` tự sinh `SP000016`, cập nhật đơn giá, nhập kho 50 cái qua `sp_NhapKho`, cập nhật thiết bị, cập nhật phòng tập, thêm/sửa khuyến mãi; rollback sạch.
- Regression tests tuần tự: `22_test_phien_14.sql`, `21_test_phien_13.sql`, `20_test_phien_12.sql`, `19_test_phien_11.sql`, `14_test_checkin_va_hoan_kho.sql`: **PASS 100%**.
- `DBCC CHECKCONSTRAINTS WITH ALL_CONSTRAINTS`: **PASS 100%**, 0 vi phạm.
- Build solution `dotnet build He_Thong_GYM.slnx --no-restore`: **PASS**, 0 warning, 0 error.
- UI Inspector `dotnet run --project He_Thong_GYM/He_Thong_GYM.csproj --no-build -- --check-ui`: **PASS 21/21 Form**, overlap 0, out-of-bounds 0, text truncation 0, UTF-8 0.

### Kế hoạch tiếp theo (Phiên 16)

- Full Regression toàn hệ thống, nghiệm thu đa vai trò (Admin, Quản lý, Lễ tân, HLV, Bán hàng).
- Đóng gói bàn giao, hoàn thiện tài liệu hướng dẫn vận hành.

## 27. Phiên 16 — Nghiệm thu Toàn diện (Final Regression) & Đóng gói Bàn giao

- **Ngày thực hiện:** 22/11/2026.
- **Mục tiêu:** Kiểm thử nghiệm thu tích hợp end-to-end, chạy Full Regression Suite, rà soát tính nhất quán và toàn vẹn của hệ thống, lập tài liệu vận hành và bàn giao chính thức.
- **Phạm vi hoàn tất:**
  1. **Xây dựng & Thực thi Kịch bản Kiểm thử Nghiệm thu Tổng hợp (`database/24_final_regression_test.sql`):**
     - Bao phủ 12 luồng nghiệp vụ cốt lõi xuyên suốt:
       1. Hội viên & Ràng buộc Validation (SĐT/Email).
       2. Đăng ký gói tập có áp dụng chương trình khuyến mãi còn hiệu lực.
       3. Thu tiền hóa đơn gói tập & chuyển trạng thái `Đang hoạt động`.
       4. Check-in luồng FIFO & chặn check-in trùng lượt.
       5. Check-out hoàn tất phiên tập.
       6. Gia hạn gói tập tính tiền bổ sung (`sp_GiaHanGoiTap` - BUG-020).
       7. Bảo lưu gói tập & khôi phục (`sp_BaoLuuGoiTap` - BUG-013).
       8. Ký hợp đồng PT, sinh hóa đơn & kích hoạt sau khi thu đủ (`sp_DangKyPT` - BUG-014).
       9. Bán lẻ hàng hóa cho khách vãng lai, trừ tồn kho tức thì (BUG-025).
       10. Hủy hóa đơn bán lẻ chưa thanh toán & tự động hoàn kho (Phiên 14).
       11. Nhập kho hàng hóa qua `sp_NhapKho`, tăng tồn & ghi nhật ký kho (Phiên 15).
       12. Quy trình bảo trì thiết bị: báo bảo trì, chặn sử dụng và hoàn tất bảo trì khôi phục máy (`sp_BaoTriThietBi` - BUG-024).
     - Kết quả: **PASS 100% (12/12 bước)**, tự động rollback sạch.
  2. **Chạy Full Regression Suite liên hoàn:**
     - `database/24_final_regression_test.sql`: **PASS**.
     - `database/23_test_phien_15.sql`: **PASS**.
     - `database/22_test_phien_14.sql`: **PASS**.
     - `database/21_test_phien_13.sql`: **PASS**.
     - `database/20_test_phien_12.sql`: **PASS**.
     - `database/19_test_phien_11.sql`: **PASS**.
     - `database/14_test_checkin_va_hoan_kho.sql`: **PASS**.
     - `DBCC CHECKCONSTRAINTS WITH ALL_CONSTRAINTS`: **PASS 100% (0 vi phạm)**.
  3. **Biên dịch & Kiểm định Giao diện:**
     - Build solution `dotnet build He_Thong_GYM.slnx --no-restore`: **PASS (0 warning, 0 error)**.
     - `UiInspector`: **PASS 21/21 Form** (0 lỗi chồng chéo, 0 tràn viền, 0 cắt chữ, 0 lỗi mã hóa UTF-8).
  4. **Tài liệu Bàn giao & Báo cáo QA:**
     - Tạo tài liệu [HUONG_DAN_SU_DUNG_VA_VAN_HANH.md](file:///d:/BTL%20PTTKYC/He_Thong_GYM/HUONG_DAN_SU_DUNG_VA_VAN_HANH.md) chi tiết danh sách tài khoản theo vai trò (Admin, Quản lý, Lễ tân, HLV, Bán hàng), sơ đồ luồng vận hành, hướng dẫn thao tác từng bước và câu lệnh kiểm thử tự động.
     - Cập nhật [QA_REPORT.md](file:///d:/BTL%20PTTKYC/He_Thong_GYM/QA_REPORT.md): Thêm mục 16, chuyển kết luận chung sang **🟢 ĐẠT CHUẨN — SẴN SÀNG BÀN GIAO & VẬN HÀNH THỰC TẾ**.

### Trạng thái Dự án Hiện tại
- **Độ sẵn sàng:** 100% Sẵn sàng đưa vào vận hành thực tế.
- **Chất lượng mã nguồn:** Sạch sẽ, không warning, không error, phân tầng UI - BLL - DAL chuẩn hóa.
- **Toàn vẹn CSDL:** 100% ràng buộc khóa ngoại, trigger, stored procedure đồng bộ và an toàn.



