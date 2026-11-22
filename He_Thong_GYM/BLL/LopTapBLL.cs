using He_Thong_GYM.DAL;using He_Thong_GYM.Models;
namespace He_Thong_GYM.BLL;
public sealed class LopTapBLL
{
    private readonly LopTapDAL _d = new();

    public Task<List<LopTap>> LayDanhSachAsync() => _d.LayDanhSachAsync();
    public Task<List<(int, string)>> LayLoaiAsync() => _d.LayLoaiAsync();
    public Task<List<HocVienLop>> LayHocVienAsync(int maLop, int maLichHoc) => _d.LayHocVienAsync(maLop, maLichHoc);
    public Task<List<BuoiHoc>> LayBuoiHocAsync(int maLop, DateTime ngay) => _d.LayBuoiHocAsync(maLop, ngay);
    public Task DangKyLopAsync(int maTV, int maLop){He_Thong_GYM.Helpers.AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên");return _d.DangKyLopAsync(maTV, maLop, He_Thong_GYM.Helpers.SessionManager.NguoiDungHienTai!.MaNV);}

    public Task<int> TaoBuoiHocAsync(LopTap lop, DateTime ngay)
    {
        He_Thong_GYM.Helpers.AuthorizationPolicy.Require("Admin", "Quản lý", "HLV", "Huấn luyện viên");
        if (ngay.Date < DateTime.Today)
            throw new ArgumentException("Không thể tạo buổi học trong quá khứ.");

        return _d.TaoBuoiHocAsync(lop.MaLop, ngay.Date.Add(lop.GioBatDau), ngay.Date.Add(lop.GioKetThuc), He_Thong_GYM.Helpers.SessionManager.NguoiDungHienTai!.MaNV);
    }

    public Task DiemDanhAsync(int maDKL, int maLichHoc, string trangThai)
    {
        He_Thong_GYM.Helpers.AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên", "HLV", "Huấn luyện viên");
        if (trangThai is not ("Có mặt" or "Vắng mặt"))
            throw new ArgumentException("Trạng thái điểm danh không hợp lệ.");

        return _d.DiemDanhAsync(maDKL, maLichHoc, trangThai, He_Thong_GYM.Helpers.SessionManager.NguoiDungHienTai!.MaNV);
    }

    public Task ThemAsync(LopTap lop)
    {
        He_Thong_GYM.Helpers.AuthorizationPolicy.Require("Admin", "Quản lý");
        if (string.IsNullOrWhiteSpace(lop.TenLop) || lop.SucChua <= 0 || lop.GioKetThuc <= lop.GioBatDau)
            throw new ArgumentException("Thông tin lớp tập không hợp lệ.");
        return _d.ThemAsync(lop);
    }

    public Task CapNhatAsync(LopTap lop)
    {
        He_Thong_GYM.Helpers.AuthorizationPolicy.Require("Admin", "Quản lý");
        if (lop.MaLop <= 0 || string.IsNullOrWhiteSpace(lop.TenLop) || lop.SucChua <= 0 || lop.GioKetThuc <= lop.GioBatDau)
            throw new ArgumentException("Thông tin lớp tập cần cập nhật không hợp lệ.");
        return _d.CapNhatAsync(lop);
    }

    public Task DongLopAsync(int maLop)
    {
        He_Thong_GYM.Helpers.AuthorizationPolicy.Require("Admin", "Quản lý");
        if (maLop <= 0) throw new ArgumentException("Mã lớp không hợp lệ.");
        return _d.DongLopAsync(maLop);
    }
}
