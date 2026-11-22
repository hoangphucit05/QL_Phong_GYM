using He_Thong_GYM.DAL;using He_Thong_GYM.Models;
namespace He_Thong_GYM.BLL;
public sealed class SanPhamBLL
{
    private readonly SanPhamDAL _d = new();
    public Task<List<SanPham>> LayAsync(string? tuKhoa = null) => _d.LayAsync(tuKhoa);
    public Task<int> BanAsync(int t, int n, int s, int sl)
    {
        He_Thong_GYM.Helpers.AuthorizationPolicy.Require("Admin", "Quản lý", "Nhân viên bán hàng", "Nhân viên");
        if (sl <= 0) throw new ArgumentException("Số lượng phải lớn hơn 0.");
        return _d.BanAsync(t, n, s, sl);
    }
    public Task ThemAsync(SanPham sp)
    {
        He_Thong_GYM.Helpers.AuthorizationPolicy.Require("Admin", "Quản lý");
        if (string.IsNullOrWhiteSpace(sp.TenSP)) throw new ArgumentException("Tên sản phẩm không được để trống.");
        if (sp.DonGia <= 0) throw new ArgumentException("Đơn giá bán phải lớn hơn 0.");
        if (sp.GiaNhap < 0) throw new ArgumentException("Giá nhập không được âm.");
        return _d.ThemAsync(sp);
    }
    public Task CapNhatAsync(SanPham sp)
    {
        He_Thong_GYM.Helpers.AuthorizationPolicy.Require("Admin", "Quản lý");
        if (sp.MaSP <= 0) throw new ArgumentException("Mã sản phẩm không hợp lệ.");
        if (string.IsNullOrWhiteSpace(sp.TenSP)) throw new ArgumentException("Tên sản phẩm không được để trống.");
        if (sp.DonGia <= 0) throw new ArgumentException("Đơn giá bán phải lớn hơn 0.");
        return _d.CapNhatAsync(sp);
    }
    public Task<int> NhapKhoAsync(int sp, int sl, decimal giaNhap, int nv, int ncc = 1)
    {
        He_Thong_GYM.Helpers.AuthorizationPolicy.Require("Admin", "Quản lý", "Nhân viên bán hàng", "Nhân viên");
        if (sp <= 0) throw new ArgumentException("Sản phẩm không hợp lệ.");
        if (sl <= 0) throw new ArgumentException("Số lượng nhập kho phải lớn hơn 0.");
        if (giaNhap < 0) throw new ArgumentException("Đơn giá nhập không được âm.");
        return _d.NhapKhoAsync(sp, sl, giaNhap, nv, ncc);
    }
}
