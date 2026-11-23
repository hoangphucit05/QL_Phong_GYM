namespace He_Thong_GYM.Models;

public sealed class BuoiTapPTModel
{
    public int MaLichPT { get; set; }
    public DateTime ThoiGianBatDau { get; set; }
    public DateTime ThoiGianKetThuc { get; set; }
    public string TrangThai { get; set; } = string.Empty;
    public string? NhanXet { get; set; }
    public DateTime NgayTao { get; set; }
}
