namespace He_Thong_GYM.Models;

public sealed class TaiKhoan
{
    public int MaTK { get; set; }
    public string TenDangNhap { get; set; } = string.Empty;
    public int MaNV { get; set; }
    public string HoTenNhanVien { get; set; } = string.Empty;
    public string VaiTro { get; set; } = string.Empty;
    public string TrangThai { get; set; } = string.Empty;
}
