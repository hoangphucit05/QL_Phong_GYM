namespace He_Thong_GYM.Models;

public sealed class PhienCheckInView
{
    public long MaCheckIn { get; set; }
    public string MaPhienCheckIn { get; set; } = string.Empty;
    public int MaTV { get; set; }
    public int MaDKG { get; set; }
    public string MaThanhVien { get; set; } = string.Empty;
    public string HoTen { get; set; } = string.Empty;
    public string TenGoi { get; set; } = string.Empty;
    public int? MaPhong { get; set; }
    public string MaPhongTap { get; set; } = string.Empty;
    public string TenPhong { get; set; } = string.Empty;
    public DateTime ThoiGianVao { get; set; }
    public string PhongHienThi => string.IsNullOrWhiteSpace(TenPhong) ? "Chưa chọn phòng" : $"{MaPhongTap} - {TenPhong}";
}
