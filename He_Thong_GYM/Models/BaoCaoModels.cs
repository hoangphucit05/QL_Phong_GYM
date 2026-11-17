namespace He_Thong_GYM.Models;

public sealed class ThongKeGoiTapView
{
    public string TenGoi { get; set; } = string.Empty;
    public int SoLuotDangKy { get; set; }
    public decimal DoanhThu { get; set; }
}

public sealed class ThongKeDoanhThuThangView
{
    public int Thang { get; set; }
    public int Nam { get; set; }
    public int SoHoaDon { get; set; }
    public decimal DoanhThu { get; set; }
}

public sealed class ThongKeHoiVienView
{
    public string TenLoai { get; set; } = string.Empty;
    public int SoLuong { get; set; }
    public int DangHoatDong { get; set; }
}

public sealed class ThongKeHLVView
{
    public string HoTen { get; set; } = string.Empty;
    public string ChuyenMon { get; set; } = string.Empty;
    public int SoHocVien { get; set; }
    public decimal DoanhThu { get; set; }
}

public sealed class ThongKeSanPhamView
{
    public string TenSP { get; set; } = string.Empty;
    public decimal DonGia { get; set; }
    public decimal GiaBan { get => DonGia; set => DonGia = value; }
    public int DaBan { get; set; }
    public int TonKho { get; set; }
    public decimal DoanhThu { get; set; }
}
