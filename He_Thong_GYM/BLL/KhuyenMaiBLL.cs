using He_Thong_GYM.DAL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.BLL;

public sealed class KhuyenMaiBLL
{
    private readonly KhuyenMaiDAL _dal = new();

    public Task<List<KhuyenMai>> LayTatCaAsync() => _dal.LayTatCaAsync();

    public Task ThemAsync(KhuyenMai km)
    {
        AuthorizationPolicy.Require("Admin", "Quản lý");
        if (string.IsNullOrWhiteSpace(km.TenKM)) throw new ArgumentException("Tên chương trình khuyến mãi không được để trống.");
        if (km.PhanTramGiam < 0 || km.PhanTramGiam > 100) throw new ArgumentException("Phần trăm giảm phải từ 0% đến 100%.");
        if (km.NgayBatDau.HasValue && km.NgayKetThuc.HasValue && km.NgayKetThuc.Value.Date < km.NgayBatDau.Value.Date)
            throw new ArgumentException("Ngày kết thúc không được nhỏ hơn ngày bắt đầu.");
        return _dal.ThemAsync(km);
    }

    public Task CapNhatAsync(KhuyenMai km)
    {
        AuthorizationPolicy.Require("Admin", "Quản lý");
        if ((km.MaKM ?? 0) <= 0) throw new ArgumentException("Mã khuyến mãi không hợp lệ.");
        if (string.IsNullOrWhiteSpace(km.TenKM)) throw new ArgumentException("Tên chương trình khuyến mãi không được để trống.");
        if (km.PhanTramGiam < 0 || km.PhanTramGiam > 100) throw new ArgumentException("Phần trăm giảm phải từ 0% đến 100%.");
        if (km.NgayBatDau.HasValue && km.NgayKetThuc.HasValue && km.NgayKetThuc.Value.Date < km.NgayBatDau.Value.Date)
            throw new ArgumentException("Ngày kết thúc không được nhỏ hơn ngày bắt đầu.");
        return _dal.CapNhatAsync(km);
    }
}
