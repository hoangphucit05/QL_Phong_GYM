namespace He_Thong_GYM.Models;

public sealed class NhanVien
{
    public int MaNV { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public DateTime NgaySinh { get; set; }
    public string? GioiTinh { get; set; }
    public string SDT { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? DiaChi { get; set; }
    public string ChucVu { get; set; } = string.Empty;
    public decimal? Luong { get; set; }
    public string TrangThai { get; set; } = string.Empty;
}
