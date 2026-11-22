namespace He_Thong_GYM.Models;

public sealed class TongQuanDashboard
{
    public int TongThanhVien { get; set; }
    public int ThanhVienHoatDong { get; set; }
    public int ThanhVienHetHan { get; set; }
    public int ThanhVienBaoLuu { get; set; }
    public int ThanhVienMoi { get; set; }
    public int TongGoiTapDangBan { get; set; }
    public int GoiDangSuDung { get; set; }
    public int GoiSapHetHan { get; set; }
    public int GoiHetHan { get; set; }
    public int GoiBaoLuu { get; set; }
    public int GoiMoi { get; set; }
    public int GoiGiaHan { get; set; }
    public decimal DoanhThuHomNay { get; set; }
    public decimal DoanhThuThangNay { get; set; }
    public decimal DoanhThuTrongKy { get; set; }
    public decimal TongDaThuTrongKy { get; set; }
    public decimal TongCongNo { get; set; }
    public int CheckInTrongKy { get; set; }
    public int CheckOutTrongKy { get; set; }
    public int DangCoMat { get; set; }
    public int GioCaoDiem { get; set; } = -1;
    public int LopDangMo { get; set; }
    public int TongPT { get; set; }
    public int BuoiPTTrongKy { get; set; }
    public int BuoiPTDaHoanThanh { get; set; }
    public int BuoiPTChuaThucHien { get; set; }
    public List<DashboardSapHetHanView> GoiSapHetHanChiTiet { get; set; } = [];
    public List<DashboardCongNoView> CongNoChiTiet { get; set; } = [];
    public List<DashboardHoatDongView> HoatDongGanDay { get; set; } = [];
    public List<DashboardDoanhThuView> DoanhThuTheoNgay { get; set; } = [];
    public List<DashboardTrangThaiView> ThanhVienTheoTrangThai { get; set; } = [];
    public List<DashboardGoiBanChayView> GoiBanChay { get; set; } = [];
}

public sealed class DashboardSapHetHanView
{
    public string MaThanhVien { get; set; } = string.Empty;
    public string HoTen { get; set; } = string.Empty;
    public string SDT { get; set; } = string.Empty;
    public string TenGoi { get; set; } = string.Empty;
    public DateTime NgayBatDau { get; set; }
    public DateTime NgayKetThuc { get; set; }
    public int SoNgayConLai { get; set; }
    public string TrangThai { get; set; } = string.Empty;
}

public sealed class DashboardCongNoView
{
    public string SoHoaDon { get; set; } = string.Empty;
    public string MaThanhVien { get; set; } = string.Empty;
    public string HoTen { get; set; } = string.Empty;
    public decimal TongTien { get; set; }
    public decimal DaThanhToan { get; set; }
    public decimal ConNo { get; set; }
    public DateTime NgayLap { get; set; }
    public string TrangThai { get; set; } = string.Empty;
}

public sealed class DashboardHoatDongView
{
    public DateTime ThoiGian { get; set; }
    public string HanhDong { get; set; } = string.Empty;
    public string DoiTuong { get; set; } = string.Empty;
    public string NoiDung { get; set; } = string.Empty;
}

public sealed class DashboardDoanhThuView
{
    public DateTime Ngay { get; set; }
    public decimal DoanhThu { get; set; }
}

public sealed class DashboardTrangThaiView
{
    public string TrangThai { get; set; } = string.Empty;
    public int SoLuong { get; set; }
}

public sealed class DashboardGoiBanChayView
{
    public string TenGoi { get; set; } = string.Empty;
    public int SoLuotDangKy { get; set; }
    public decimal DoanhThu { get; set; }
}
