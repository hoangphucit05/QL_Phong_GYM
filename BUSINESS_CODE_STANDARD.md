# BUSINESS CODE STANDARD

## 1. Mục tiêu

Chuẩn hóa mã nghiệp vụ người dùng nhìn thấy trong hệ thống GYM. Mã nghiệp vụ dùng cho giao diện, tìm kiếm, hóa đơn, biên nhận, báo cáo và trao đổi với người dùng; khóa số hiện tại tiếp tục giữ vai trò primary key/foreign key nội bộ.

## 2. Nguyên tắc bắt buộc

1. Không đổi hoặc thay thế trực tiếp các khóa `IDENTITY` đang làm PK/FK.
2. Mỗi mã nghiệp vụ là chuỗi có tiền tố, không dấu, dễ đọc và có `UNIQUE` constraint/index.
3. Mã được cấp một lần, không tái sử dụng sau khi bản ghi bị xóa mềm hoặc ngừng sử dụng.
4. Không sinh mã từ tên, số điện thoại hoặc dữ liệu có thể thay đổi.
5. Cơ chế cấp số phải an toàn khi nhiều người dùng tạo đồng thời; migration phải backfill dữ liệu cũ trước khi đặt `NOT NULL`.
6. Các procedure/API nội bộ tiếp tục nhận ID số trong giai đoạn đầu; chỉ đổi sang mã nghiệp vụ ở lớp tra cứu và hiển thị để giảm rủi ro.

## 3. Quy ước mã đã chốt cho phiên 1

| Đối tượng | Khóa nội bộ hiện tại | Cột mã nghiệp vụ | Định dạng | Trạng thái |
|---|---|---|---|---|
| Thành viên | `MaTV` | `MaThanhVien` | `TV000001` | Đã có, cần kiểm tra/backfill/unique |
| Nhân viên | `MaNV` | `MaNhanVien` | `NV000001` | Cần bổ sung |
| Gói tập | `MaGoi` | `MaGoiTap` | `GOI000001` | Cần bổ sung |
| Đăng ký gói | `MaDKG` | `MaDangKyGoi` | `DKG-YYYYMMDD-0001` | Cần bổ sung |
| Đăng ký PT | `MaDKPT` | `MaDangKyPT` | `DPT-YYYYMMDD-0001` | Cần bổ sung |
| Lớp tập | `MaLop` | `MaLopTap` | `LOP000001` | Cần bổ sung |
| Đăng ký lớp | `MaDKL` | `MaDangKyLop` | `DKL-YYYYMMDD-0001` | Cần bổ sung |
| Buổi học | `MaLichHoc` | `MaBuoiHoc` | `LH-YYYYMMDD-0001` | Cần bổ sung |
| Hóa đơn | `MaHD` | `SoHoaDon` | `HD-YYYYMMDD-0001` | Cần bổ sung |
| Chi tiết hóa đơn | `MaCTHD` | Không cần mã riêng | — | Dùng qua `SoHoaDon` |
| Thanh toán | `MaTT` | `SoGiaoDich` | `TT-YYYYMMDD-0001` | Cần bổ sung |
| Check-in | `MaCheckIn` | `MaPhienCheckIn` | `CI-YYYYMMDD-0001` | Cần bổ sung |
| Sản phẩm | `MaSP` | `MaSanPham` | `SP000001` | Đã có, cần kiểm tra/backfill/unique |
| Thiết bị | `MaTB` | `MaThietBi` | `TB000001` | Đã có, cần kiểm tra/backfill/unique |
| Phòng tập | `MaPhong` | `MaPhongTap` | `PHG000001` | Cần bổ sung |
| Nhà cung cấp | `MaNCC` | `MaNhaCungCap` | `NCC000001` | Có dấu hiệu đã có, cần thống nhất tên/cách dùng |
| Phiếu nhập kho | `MaPNK` | `SoPhieuNhap` | `PNK-YYYYMMDD-0001` | Cần bổ sung |
| Khuyến mãi | `MaKM` | `MaCode` | Ví dụ `CHAOHE10` | Đã có; giữ mã do nghiệp vụ nhập |

## 4. Phạm vi kiểm kê đã thực hiện

- Database gốc và migration: xác định các bảng đang dùng khóa `IDENTITY` và các cột mã đã tồn tại.
- Model/DAL: xác định nơi đang trả về/nhận mã số, đặc biệt thành viên, lớp, hóa đơn, sản phẩm, thanh toán và check-in.
- UI: các DataGridView/ComboBox và thông báo cần chuyển từ ID kỹ thuật sang mã nghiệp vụ.
- Báo cáo/chứng từ: hóa đơn, thanh toán, tồn kho, báo cáo thành viên và báo cáo dịch vụ cần hiển thị mã mới.
- Bảng legacy `PhieuBanHang`/`ChiTietPhieuBanHang`: chưa đổi trong phiên này; chỉ lập mã nếu tiếp tục hỗ trợ hiển thị legacy.

## 5. Quy tắc sinh mã

- Mã dạng chỉ số: dùng sequence/cơ chế cấp số riêng theo loại, padding cố định 6 chữ số.
- Mã chứng từ: dùng ngày lập + số tăng dần trong ngày, ví dụ `HD-20261122-0001`.
- Không dùng `MAX(...) + 1` vì có thể trùng khi chạy đồng thời.
- Mã cũ được backfill từ ID hiện tại để ổn định và dễ đối soát; không tự đổi mã sau khi migration.
- Mỗi cột mã phải có kiểm tra trùng, kiểm tra rỗng và index unique trước khi ứng dụng sử dụng.

## 6. Thứ tự triển khai các phiên sau

### Phiên 2 — Database migration

- **Đã hoàn tất trên database local `QL_PhongGym` bằng migration `database/13_chuan_hoa_ma_nghiep_vu.sql`.**
- Đã thêm các cột mã nghiệp vụ còn thiếu, khôi phục `KhuyenMai.MaCode`, backfill dữ liệu cũ và tạo unique index/constraint.
- Đã tạo trigger cấp mã tự động cho bản ghi mới khi caller không truyền mã.
- Mã chứng từ hiện dùng ngày nghiệp vụ + ID identity bất biến ở phần số, ví dụ `HD-20261116-000001`; chưa dùng bộ đếm riêng theo từng ngày.
- Đã tạo backup trước migration, chạy migration lặp, `DBCC CHECKCONSTRAINTS` và smoke test rollback.
- Đã sửa `QUOTED_IDENTIFIER/ANSI_NULLS` cho procedure đăng ký/hủy lớp vì filtered index yêu cầu SET options đúng.

### Phiên 3 — Model/DAL/BLL (tiếp theo)

- Bổ sung thuộc tính mã vào model.
- SELECT trả cả ID nội bộ và mã nghiệp vụ.
- Thêm tìm kiếm theo mã.
- Không đổi FK/API số trong giai đoạn đầu.

### Phiên 4 — UI/chứng từ

- Hiển thị mã nghiệp vụ trong bảng, ComboBox, thông báo, hóa đơn, thanh toán, check-in và báo cáo.
- Giữ ID số chỉ ở màn hình kỹ thuật nếu cần.

### Phiên 5 — Kiểm thử/đối soát

- Kiểm thử tạo đồng thời, sửa, xóa mềm, tìm kiếm và in/tra cứu chứng từ.
- Đối chiếu số bản ghi, quan hệ FK và báo cáo trước/sau migration.
- Build, UI check, test scenario và cập nhật `SESSION_CONTEXT.md`.

## 7. Tiêu chí hoàn thành

- 100% mã nghiệp vụ người dùng nhìn thấy không còn là số ID trần.
- Không có mã trùng/rỗng.
- Mã cũ được giữ ổn định và truy vết được về ID nội bộ.
- Không phá khóa ngoại hoặc dữ liệu lịch sử.
- Các luồng thành viên → đăng ký → hóa đơn → thanh toán → check-in và sản phẩm → hóa đơn → tồn kho đều hiển thị mã thống nhất.
