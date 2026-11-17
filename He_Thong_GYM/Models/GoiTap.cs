namespace He_Thong_GYM.Models;

public sealed class GoiTap
{
    public int MaGoi { get; set; }
    public string TenGoi { get; set; } = string.Empty;
    public int ThoiHanNgay { get; set; }
    public decimal Gia { get; set; }
    public string? MoTa { get; set; }
    public string TrangThai { get; set; } = string.Empty;
}
