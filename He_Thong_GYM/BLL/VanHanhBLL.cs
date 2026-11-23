using He_Thong_GYM.DAL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.BLL;

public sealed class VanHanhBLL
{
    private readonly VanHanhDAL _dal = new();

    public Task<List<PhienCheckInView>> LayPhienCheckInMoAsync() => _dal.LayPhienCheckInMoAsync();
    public Task<List<DangKyGoiView>> LayGoiThanhVienAsync(int maTV) => _dal.LayGoiThanhVienAsync(maTV);
    public Task<List<PhongTapModel>> LayPhongDuocPhepAsync(int maDKG) => _dal.LayPhongDuocPhepAsync(maDKG);

    public Task<string> CheckInAsync(int maTV, int maDKG, int? maPhong)
    {
        AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên");
        return _dal.CheckInAsync(maTV, maDKG, maPhong, SessionManager.NguoiDungHienTai!.MaNV);
    }

    public Task CheckOutAsync(int maTV)
    {
        AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên");
        return _dal.CheckOutAsync(maTV, SessionManager.NguoiDungHienTai!.MaNV);
    }

    public Task<bool> CoPhienCheckInMoAsync(int maTV) => _dal.CoPhienCheckInMoAsync(maTV);
    public Task<int?> LayMaPhongPhienMoAsync(int maTV) => _dal.LayMaPhongPhienMoAsync(maTV);

    public Task ChuyenPhongAsync(int maTV, int maPhongMoi)
    {
        AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên");
        return _dal.ChuyenPhongAsync(maTV, maPhongMoi, SessionManager.NguoiDungHienTai!.MaNV);
    }

    public Task<int> GiaHanAsync(int maDKG, int soNgay, int? maNV)
    {
        AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên");
        if (soNgay <= 0) throw new ArgumentException("Số ngày gia hạn phải lớn hơn 0.");
        if (soNgay > 365) throw new ArgumentException("Số ngày gia hạn không được vượt quá 365 ngày.");
        return _dal.GiaHanAsync(maDKG, soNgay, maNV);
    }

    public Task BaoLuuAsync(int maDKG, DateTime tu, DateTime den, string lyDo, int? maNV)
    {
        AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên");
        return tu.Date < DateTime.Today || den.Date < tu.Date || string.IsNullOrWhiteSpace(lyDo)
            ? throw new ArgumentException("Ngày bảo lưu phải từ hôm nay, theo đúng thứ tự và có lý do.")
            : _dal.BaoLuuAsync(maDKG, tu, den, lyDo.Trim(), maNV);
    }

    public Task MoLaiBaoLuuAsync(int maDKG, int? maNV)
    {
        AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên");
        return _dal.MoLaiBaoLuuAsync(maDKG, maNV);
    }
}
