using He_Thong_GYM.DAL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.BLL;

public sealed class DangNhapBLL
{
    private readonly TaiKhoanDAL _taiKhoanDAL = new();

    public async Task<NguoiDung> DangNhapAsync(string tenDangNhap, string matKhau)
    {
        if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            throw new ArgumentException("Hãy nhập tên đăng nhập và mật khẩu.");

        var ten = tenDangNhap.Trim();
        var nguoiDung = await _taiKhoanDAL.LayTheoTenDangNhapAsync(ten);
        if (nguoiDung is null)
        {
            await _taiKhoanDAL.GhiKetQuaDangNhapAsync(null, ten, false);
            throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không đúng.");
        }

        if (nguoiDung.KhoaDen is not null && nguoiDung.KhoaDen > DateTime.Now)
        {
            await _taiKhoanDAL.GhiKetQuaDangNhapAsync(nguoiDung.MaTK, ten, false);
            throw new UnauthorizedAccessException($"Tài khoản đang bị tạm khóa đến {nguoiDung.KhoaDen:HH:mm dd/MM/yyyy}.");
        }

        var hopLe = nguoiDung.TrangThaiTaiKhoan == "Hoạt động" &&
                    nguoiDung.TrangThaiNhanVien == "Đang làm" &&
                    PasswordHasher.XacThuc(matKhau, nguoiDung.MatKhauSalt, nguoiDung.MatKhauHash);
        await _taiKhoanDAL.GhiKetQuaDangNhapAsync(nguoiDung.MaTK, ten, hopLe);
        if (!hopLe)
            throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không đúng.");

        return nguoiDung;
    }

    public async Task DoiMatKhauAsync(NguoiDung nguoiDung, string matKhauMoi)
    {
        AuthorizationPolicy.Require("Admin");
        if (matKhauMoi.Length < 8) throw new ArgumentException("Mật khẩu mới phải có ít nhất 8 ký tự.");
        var (hash, salt) = PasswordHasher.TaoHash(matKhauMoi);
        await _taiKhoanDAL.CapNhatMatKhauAsync(nguoiDung.MaTK, hash, salt);
    }

    public async Task<int> TaoTaiKhoanAsync(TaiKhoan taiKhoan,string matKhau)
    {
        AuthorizationPolicy.Require("Admin");
        if(string.IsNullOrWhiteSpace(taiKhoan.TenDangNhap))throw new ArgumentException("Tên đăng nhập không được để trống.");
        if(taiKhoan.MaNV<=0)throw new ArgumentException("Hãy chọn nhân viên.");
        if(string.IsNullOrWhiteSpace(taiKhoan.VaiTro))throw new ArgumentException("Hãy chọn vai trò.");
        if(matKhau.Length<8)throw new ArgumentException("Mật khẩu phải có ít nhất 8 ký tự.");
        var (hash,salt)=PasswordHasher.TaoHash(matKhau);return await _taiKhoanDAL.TaoTaiKhoanAsync(taiKhoan,hash,salt);
    }
    public Task<List<TaiKhoan>> LayDanhSachTaiKhoanAsync()=>_taiKhoanDAL.LayDanhSachAsync();
    public Task<List<string>> LayVaiTroAsync()=>_taiKhoanDAL.LayVaiTroAsync();
    public Task CapNhatTrangThaiTaiKhoanAsync(int maTK,string trangThai){AuthorizationPolicy.Require("Admin");return _taiKhoanDAL.CapNhatTrangThaiAsync(maTK,trangThai);}
    public async Task CapNhatVaiTroAsync(int maTK, string vaiTroMoi)
    {
        AuthorizationPolicy.Require("Admin");
        if (maTK <= 0) throw new ArgumentException("Mã tài khoản không hợp lệ.");
        if (string.IsNullOrWhiteSpace(vaiTroMoi)) throw new ArgumentException("Hãy chọn vai trò mới.");
        await _taiKhoanDAL.CapNhatVaiTroAsync(maTK, vaiTroMoi.Trim());
    }
}
