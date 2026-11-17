# Nâng cấp QL_PhongGym v3

## Kết quả

`01_upgrade_ql_phonggym_v3.sql` là migration bổ sung chạy một lần: không `DROP` bảng, không xóa dữ liệu và không đổi tên bảng/cột cũ. Chạy sau file gốc `htql_gym_v2.sql`. `02_seed_v3.sql` thêm dữ liệu tối thiểu; `03_test_v3.sql` là smoke test chạy trong transaction rồi rollback.

## Đánh giá database gốc

| Thành phần | Vấn đề | Ưu tiên | Phương án v3 |
|---|---|---:|---|
| Thành viên | Không mã nghiệp vụ, ảnh, xóa mềm, lịch sử | Cao | Mã `TVxxxxxx`, ảnh, audit và khóa mềm |
| Gói tập | Chưa bảo lưu/gia hạn/lượt/khung giờ | Cao | Lịch sử, bảo lưu, procedure đăng ký/gia hạn |
| Ra vào | Chưa có nghiệp vụ độc lập | Cao | `LichSuCheckIn`, index duy nhất cho lượt mở |
| PT | Không có lịch/buổi thực tế, chống trùng | Cao | `LichLamViecHLV`, `LichTapPT`, khóa khi đặt |
| Lớp | Lịch cố định lẫn buổi thực tế, chưa có chờ | Cao | `LichHoc`, `DanhSachCho`, đăng ký có kiểm sức chứa |
| Tài chính | Tổng tiền nhập tay; tham chiếu đa hình không FK | Rất cao | trigger đồng bộ tổng; bốn bảng mapping FK; giao dịch/hoàn tiền |
| Bán hàng | `PhieuBanHang` trùng vai trò với hóa đơn, không kiểm kho | Rất cao | hóa đơn là luồng bán mới; tồn kho/lịch sử/phieu nhập |
| Thiết bị | Thiếu mã, NCC, giá, bảo hành và lịch định kỳ | Trung bình | mở rộng `ThietBi`/`BaoTriThietBi`, nhà cung cấp |
| Tài khoản | Mật khẩu mẫu plaintext, role quá hẹp, không audit | Rất cao | role N-N, lịch sử đăng nhập/audit, cột hash+salt |

Các lỗi đã phát hiện trong truy vấn gốc: câu 30 dùng `ThietBi.TrangThai` trong khi cột là `TinhTrang`; câu 38 dùng `TenThietBi` và `BaoTriThietBi.TrangThai` chưa tồn tại; thống kê doanh thu dựa `HoaDon.TongTien` không trừ hoàn tiền; query tổng hợp câu 20 JOIN nhiều bảng 1-n nên có thể nhân doanh thu; `PhieuBanHang` và hóa đơn có thể bị cộng trùng; `ChiTietHoaDon.MaThamChieu` không có FK.

## ERD rút gọn

```text
ThanhVien ─< DangKyGoi ─< BaoLuuGoiTap / LichSuGoiTap / LichSuCheckIn
ThanhVien ─< DangKyPT ─< LichTapPT >─ HuanLuyenVien ─ NhanVien
LopTap ─< LichHoc >─ PhongTap; DangKyLop ─< DiemDanhLop; LopTap ─< DanhSachCho
HoaDon ─< ChiTietHoaDon ─1─ HoaDonGoiTap | HoaDonPT | HoaDonLop | HoaDonSanPham
HoaDon ─< ThanhToan
NhaCungCap ─< PhieuNhapKho ─< ChiTietPhieuNhap >─ SanPham ─< LichSuTonKho
NhaCungCap ─< ThietBi ─< BaoTriThietBi
```

## Quy tắc triển khai

1. Sao lưu database và phục hồi bản sao để thử migration.
2. Tạo database bằng `htql_gym_v2.sql` (hoặc dùng bản production tương thích), rồi chạy `01`, `02`, `03` theo thứ tự trong SSMS với SQLCMD tắt.
3. Kiểm tra `03_test_v3.sql` trả về `PASS`; transaction luôn rollback. Chạy `DBCC CHECKCONSTRAINTS` và backup sau migration.
4. Cập nhật ứng dụng chỉ gọi `sp_*` cho nghiệp vụ ghi, đặc biệt là thanh toán, bán hàng và kho. Không INSERT trực tiếp vào các bảng mapping hóa đơn.
5. `PhieuBanHang`/`ChiTietPhieuBanHang` là dữ liệu legacy cần đối soát trước khi chuyển đổi, nên migration cố ý không tự tạo hóa đơn từ chúng (tránh nhân đôi doanh thu). Bán hàng mới dùng `sp_BanSanPham` + `HoaDon`.

## Bảo mật và vận hành

SQL Server không nên tự tạo BCrypt/Argon2. Ứng dụng phải sinh salt ngẫu nhiên và PBKDF2/Argon2/bcrypt, lưu hash vào `TaiKhoan.MatKhauHash` và salt vào `MatKhauSalt`; các mật khẩu `123456` mẫu phải bị buộc đổi trước production. Cấp quyền execute procedure/view thay vì quyền ghi bảng trực tiếp. Sao lưu, kiểm thử migration ở staging và chỉ chạy production trong cửa sổ bảo trì.

## Báo cáo sẵn có

Views cung cấp: danh sách thành viên, gói còn hiệu lực/sắp hết hạn, PT, lịch học, check-in, doanh thu thực theo ngày, công nợ, tồn kho/cảnh báo hàng thấp và thiết bị cần bảo trì. Doanh thu tháng/năm lấy từ `vw_DoanhThuTheoNgay` theo `YEAR/ MONTH(Ngay)`; hiệu suất PT từ `LichTapPT` trạng thái `Hoàn thành`; tỷ lệ tham gia từ `DiemDanhLop` theo `MaLichHoc`.
