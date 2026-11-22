namespace He_Thong_GYM.Models;

public sealed class NguoiDung
{
    public int MaTK { get; set; }
    public int MaNV { get; set; }
    public string TenDangNhap { get; set; } = string.Empty;
    public string HoTen { get; set; } = string.Empty;
    public string VaiTro { get; set; } = string.Empty;
    public byte[] MatKhauHash { get; set; } = Array.Empty<byte>();
    public byte[] MatKhauSalt { get; set; } = Array.Empty<byte>();
    public string TrangThaiTaiKhoan { get; set; } = string.Empty;
    public string TrangThaiNhanVien { get; set; } = string.Empty;
    public int SoLanDangNhapSai { get; set; }
    public DateTime? KhoaDen { get; set; }
}
