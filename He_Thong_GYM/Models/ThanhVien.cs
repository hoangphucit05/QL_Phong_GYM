namespace He_Thong_GYM.Models;

public sealed class ThanhVien
{
    public int MaTV { get; set; }
    public string MaThanhVien { get; set; } = string.Empty;
    public string HoTen { get; set; } = string.Empty;
    public DateTime NgaySinh { get; set; }
    public string? GioiTinh { get; set; }
    public string SDT { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? DiaChi { get; set; }
    public DateTime NgayDangKy { get; set; }
    public int MaLoaiTV { get; set; }
    public string TenLoai { get; set; } = string.Empty;
    public string TrangThai { get; set; } = string.Empty;
    public string HienThi => string.IsNullOrWhiteSpace(MaThanhVien) ? HoTen : $"{MaThanhVien} - {HoTen}";
}
