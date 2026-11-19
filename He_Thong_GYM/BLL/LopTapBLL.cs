using He_Thong_GYM.DAL;using He_Thong_GYM.Models;
namespace He_Thong_GYM.BLL;
public sealed class LopTapBLL
{
    private readonly LopTapDAL _d = new();

    public Task<List<LopTap>> LayDanhSachAsync() => _d.LayDanhSachAsync();
    public Task<List<(int, string)>> LayLoaiAsync() => _d.LayLoaiAsync();
    public Task<List<HocVienLop>> LayHocVienAsync(int maLop, int maLichHoc) => _d.LayHocVienAsync(maLop, maLichHoc);
    public Task<BuoiHoc?> LayBuoiHocAsync(int maLop, DateTime ngay) => _d.LayBuoiHocAsync(maLop, ngay);
    public Task DangKyLopAsync(int maTV, int maLop) => _d.DangKyLopAsync(maTV, maLop);

    public Task<int> TaoBuoiHocAsync(LopTap lop, DateTime ngay)
    {
        if (ngay.Date < DateTime.Today)
            throw new ArgumentException("Không thể tạo buổi học trong quá khứ.");

        return _d.TaoBuoiHocAsync(lop.MaLop, ngay.Date.Add(lop.GioBatDau), ngay.Date.Add(lop.GioKetThuc));
    }

    public Task DiemDanhAsync(int maDKL, int maLichHoc, string trangThai)
    {
        if (trangThai is not ("Có mặt" or "Vắng mặt"))
            throw new ArgumentException("Trạng thái điểm danh không hợp lệ.");

        return _d.DiemDanhAsync(maDKL, maLichHoc, trangThai);
    }

    public Task ThemAsync(LopTap lop)
    {
        if (string.IsNullOrWhiteSpace(lop.TenLop) || lop.SucChua <= 0 || lop.GioKetThuc <= lop.GioBatDau)
            throw new ArgumentException("Thông tin lớp tập không hợp lệ.");
        return _d.ThemAsync(lop);
    }
}
