using He_Thong_GYM.DAL;
using He_Thong_GYM.Models;
using He_Thong_GYM.Helpers;
namespace He_Thong_GYM.BLL;
public sealed class NhanVienBLL
{
 private readonly NhanVienDAL _dal=new();public Task<List<NhanVien>> LayDanhSachAsync(string? k=null)=>_dal.LayDanhSachAsync(k);public Task NghiViecAsync(int maNV){AuthorizationPolicy.Require("Admin", "Quản lý");return _dal.NghiViecAsync(maNV);}
 public Task<List<NhanVien>> LayNhanVienDangLamAsync()=>_dal.LayNhanVienDangLamAsync();
 public Task<List<NhanVien>> LayNhanVienChuaCoTaiKhoanAsync()=>_dal.LayNhanVienChuaCoTaiKhoanAsync();
 public async Task<int> LuuAsync(NhanVien n){AuthorizationPolicy.Require("Admin", "Quản lý");if(string.IsNullOrWhiteSpace(n.HoTen)||n.HoTen.Trim().Length<2)throw new ArgumentException("Họ tên phải có ít nhất 2 ký tự.");if(n.NgaySinh.Date>=DateTime.Today)throw new ArgumentException("Ngày sinh không hợp lệ.");InputValidator.ValidatePhone(n.SDT);InputValidator.ValidateOptionalEmail(n.Email);if(string.IsNullOrWhiteSpace(n.ChucVu))throw new ArgumentException("Chức vụ không được để trống.");if(n.Luong<0)throw new ArgumentException("Lương không hợp lệ.");return n.MaNV==0?await _dal.ThemAsync(n):await _dal.CapNhatAsync(n);}
}
