using He_Thong_GYM.DAL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.BLL;

public sealed class VanHanhBLL
{
    private readonly VanHanhDAL _dal = new();
    public Task<List<DangKyGoiView>> LayGoiThanhVienAsync(int maTV) => _dal.LayGoiThanhVienAsync(maTV);
    public Task<string> CheckInAsync(int maTV, int? maPhong){He_Thong_GYM.Helpers.AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên");return _dal.CheckInAsync(maTV, maPhong, He_Thong_GYM.Helpers.SessionManager.NguoiDungHienTai!.MaNV);}
    public Task CheckOutAsync(int maTV){He_Thong_GYM.Helpers.AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên");return _dal.CheckOutAsync(maTV, He_Thong_GYM.Helpers.SessionManager.NguoiDungHienTai!.MaNV);}
    public Task<int> GiaHanAsync(int maDKG, int soNgay, int? maNV){He_Thong_GYM.Helpers.AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên");return soNgay <= 0 ? throw new ArgumentException("Số ngày gia hạn phải lớn hơn 0.") : _dal.GiaHanAsync(maDKG, soNgay, maNV);}
    public Task BaoLuuAsync(int maDKG, DateTime tu, DateTime den, string lyDo, int? maNV){He_Thong_GYM.Helpers.AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên");return tu.Date < DateTime.Today || den.Date < tu.Date || string.IsNullOrWhiteSpace(lyDo) ? throw new ArgumentException("Ngày bảo lưu phải từ hôm nay, theo đúng thứ tự và có lý do.") : _dal.BaoLuuAsync(maDKG, tu, den, lyDo.Trim(), maNV);}
}
