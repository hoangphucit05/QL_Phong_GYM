using He_Thong_GYM.DAL;
using He_Thong_GYM.Models;
namespace He_Thong_GYM.BLL;
public sealed class GoiTapBLL
{
    private readonly GoiTapDAL _dal=new();
    public Task<List<GoiTap>> LayDanhSachAsync(string? tuKhoa=null)=>_dal.LayDanhSachAsync(tuKhoa);
    public async Task<int> LuuAsync(GoiTap g){if(string.IsNullOrWhiteSpace(g.TenGoi))throw new ArgumentException("Tên gói không được để trống.");if(g.ThoiHanNgay<=0)throw new ArgumentException("Thời hạn phải lớn hơn 0.");if(g.Gia<0)throw new ArgumentException("Giá gói không hợp lệ.");return g.MaGoi==0?await _dal.ThemAsync(g):await _dal.CapNhatAsync(g);}
    public Task NgungApDungAsync(int maGoi)=>_dal.NgungApDungAsync(maGoi);
}
