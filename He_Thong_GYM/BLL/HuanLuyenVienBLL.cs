using He_Thong_GYM.DAL;using He_Thong_GYM.Models;
using He_Thong_GYM.Helpers;
namespace He_Thong_GYM.BLL;
public sealed class HuanLuyenVienBLL
{private readonly HuanLuyenVienDAL _dal=new();public Task<List<HuanLuyenVien>> LayDanhSachAsync(string? k=null)=>_dal.LayDanhSachAsync(k);public async Task<int>LuuAsync(HuanLuyenVien h){AuthorizationPolicy.Require("Admin", "Quản lý");if(h.MaNV<=0)throw new ArgumentException("Hãy chọn nhân viên.");if(h.KinhNghiem<0)throw new ArgumentException("Kinh nghiệm không hợp lệ.");if(h.PhiThue<0)throw new ArgumentException("Phí thuê không hợp lệ.");return h.MaHLV==0?await _dal.ThemAsync(h):await _dal.CapNhatAsync(h);}}
