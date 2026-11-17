using He_Thong_GYM.DAL;using He_Thong_GYM.Models;
namespace He_Thong_GYM.BLL;
public sealed class SanPhamBLL{private readonly SanPhamDAL _d=new();public Task<List<SanPham>> LayAsync()=>_d.LayAsync();public Task<int> BanAsync(int t,int n,int s,int sl){if(sl<=0)throw new ArgumentException("Số lượng phải lớn hơn 0.");return _d.BanAsync(t,n,s,sl);}}
