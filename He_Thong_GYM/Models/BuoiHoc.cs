namespace He_Thong_GYM.Models;

public sealed class BuoiHoc
{
    public int MaLichHoc { get; set; }
    public int MaLop { get; set; }
    public DateTime ThoiGianBatDau { get; set; }
    public DateTime ThoiGianKetThuc { get; set; }
    public string TrangThai { get; set; } = string.Empty;
}
