using He_Thong_GYM.DAL;
using He_Thong_GYM.Models;
namespace He_Thong_GYM.BLL;
public sealed class NhanVienBLL
{
 private readonly NhanVienDAL _dal=new();public Task<List<NhanVien>> LayDanhSachAsync(string? k=null)=>_dal.LayDanhSachAsync(k);public Task NghiViecAsync(int maNV)=>_dal.NghiViecAsync(maNV);
 public Task<List<NhanVien>> LayNhanVienDangLamAsync()=>_dal.LayNhanVienDangLamAsync();
 public async Task<int> LuuAsync(NhanVien n){if(string.IsNullOrWhiteSpace(n.HoTen))throw new ArgumentException("Họ tên không được để trống.");if(n.NgaySinh.Date>=DateTime.Today)throw new ArgumentException("Ngày sinh không hợp lệ.");if(string.IsNullOrWhiteSpace(n.SDT)||n.SDT.Length is < 9 or > 15)throw new ArgumentException("Số điện thoại phải có từ 9 đến 15 ký tự.");if(string.IsNullOrWhiteSpace(n.ChucVu))throw new ArgumentException("Chức vụ không được để trống.");if(n.Luong<0)throw new ArgumentException("Lương không hợp lệ.");return n.MaNV==0?await _dal.ThemAsync(n):await _dal.CapNhatAsync(n);}
}
