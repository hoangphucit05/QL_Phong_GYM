using He_Thong_GYM.DAL;
using He_Thong_GYM.Models;

using He_Thong_GYM.Helpers;

namespace He_Thong_GYM.BLL;

public sealed class ThanhVienBLL
{
    private readonly ThanhVienDAL _thanhVienDAL = new();
    public Task<List<ThanhVien>> LayDanhSachAsync(string? tuKhoa = null) => _thanhVienDAL.LayDanhSachAsync(tuKhoa);
    public Task<List<LoaiThanhVien>> LayLoaiThanhVienAsync() => _thanhVienDAL.LayLoaiThanhVienAsync();
    public async Task<int> LuuAsync(ThanhVien thanhVien)
    {
        AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên");
        KiemTra(thanhVien);
        if (thanhVien.MaTV == 0) return await _thanhVienDAL.ThemAsync(thanhVien);
        await _thanhVienDAL.CapNhatAsync(thanhVien);
        return thanhVien.MaTV;
    }
    public Task NgungSuDungAsync(int maTV)
    {
        AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên");
        return _thanhVienDAL.NgungSuDungAsync(maTV);
    }
    private static void KiemTra(ThanhVien thanhVien)
    {
        if (string.IsNullOrWhiteSpace(thanhVien.HoTen)) throw new ArgumentException("Họ tên không được để trống.");
        if (thanhVien.NgaySinh.Date >= DateTime.Today) throw new ArgumentException("Ngày sinh phải nhỏ hơn ngày hiện tại.");
        InputValidator.ValidatePhone(thanhVien.SDT);
        InputValidator.ValidateOptionalEmail(thanhVien.Email);
        if (thanhVien.MaLoaiTV <= 0) throw new ArgumentException("Hãy chọn loại thành viên.");
        if (string.IsNullOrWhiteSpace(thanhVien.TrangThai)) throw new ArgumentException("Hãy chọn trạng thái thành viên.");
    }
}
