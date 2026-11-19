using He_Thong_GYM.DAL;using He_Thong_GYM.Models;
namespace He_Thong_GYM.BLL;
public sealed class LopTapBLL
{
    private readonly LopTapDAL _d = new();

    public Task<List<LopTap>> LayDanhSachAsync() => _d.LayDanhSachAsync();
    public Task<List<(int, string)>> LayLoaiAsync() => _d.LayLoaiAsync();
    public Task<List<HocVienLop>> LayHocVienAsync(int maLop, DateTime ngay) => _d.LayHocVienAsync(maLop, ngay);

    public Task<DangKyLopKetQua> DangKyLopAsync(int maTV, int maLop) => _d.DangKyLopAsync(maTV, maLop);

    public Task DiemDanhAsync(int maDKL, DateTime ngay, string trangThai)
    {
        if (ngay.Date > DateTime.Today)
            throw new ArgumentException("Chỉ có thể điểm danh cho ngày hôm nay hoặc ngày đã qua.");
        if (trangThai is not ("Có mặt" or "Vắng mặt"))
            throw new ArgumentException("Trạng thái điểm danh không hợp lệ.");

        return _d.DiemDanhAsync(maDKL, ngay, trangThai);
    }

    public Task ThemAsync(LopTap lop)
    {
        if (string.IsNullOrWhiteSpace(lop.TenLop) || lop.SucChua <= 0 || lop.GioKetThuc <= lop.GioBatDau)
            throw new ArgumentException("Thông tin lớp tập không hợp lệ.");
        return _d.ThemAsync(lop);
    }
}
