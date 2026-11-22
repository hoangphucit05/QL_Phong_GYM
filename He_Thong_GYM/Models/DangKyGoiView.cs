namespace He_Thong_GYM.Models;

public sealed class DangKyGoiView
{
    public int MaDKG { get; set; }
    public string MaDangKyGoi { get; set; } = string.Empty;
    public string TenGoi { get; set; } = string.Empty;
    public DateTime NgayBatDau { get; set; }
    public DateTime NgayKetThuc { get; set; }
    public string TrangThai { get; set; } = string.Empty;
    public string HienThi => string.IsNullOrWhiteSpace(MaDangKyGoi) ? $"ĐK {MaDKG}" : $"{MaDangKyGoi} - {TenGoi}";
}
