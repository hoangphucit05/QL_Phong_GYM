namespace He_Thong_GYM.Models;

public sealed class HuanLuyenVien
{
    public int MaHLV { get; set; }
    public int MaNV { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public string? ChuyenMon { get; set; }
    public int? KinhNghiem { get; set; }
    public decimal? PhiThue { get; set; }
    public string TrangThaiNhanVien { get; set; } = string.Empty;
}
