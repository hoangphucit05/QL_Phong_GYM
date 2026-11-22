namespace He_Thong_GYM.Models;
public sealed class BaoTri
{
    public int MaBaoTri { get; set; }
    public int MaTB { get; set; }
    public string MaThietBi { get; set; } = string.Empty;
    public string ThietBi { get; set; } = string.Empty;
    public string NhanVien { get; set; } = string.Empty;
    public DateTime NgayBaoTri { get; set; }
    public string NoiDung { get; set; } = string.Empty;
    public decimal ChiPhi { get; set; }
    public string TrangThai { get; set; } = string.Empty;
    public DateTime? NgayDuKien { get; set; }
    public DateTime? NgayHoanThanh { get; set; }
    public string? GhiChu { get; set; }
}
