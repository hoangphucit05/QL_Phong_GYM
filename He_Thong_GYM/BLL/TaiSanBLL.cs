using He_Thong_GYM.DAL;using He_Thong_GYM.Models;
namespace He_Thong_GYM.BLL;
public sealed class TaiSanBLL{private readonly TaiSanDAL _d=new();public Task<List<ThietBi>> LayThietBiAsync()=>_d.LayThietBiAsync();public Task<List<PhongTapModel>> LayPhongAsync()=>_d.LayPhongAsync();public Task<List<BaoTri>> LayBaoTriAsync()=>_d.LayBaoTriAsync();public Task TaoBaoTriAsync(int t,int n,string x,decimal c){if(string.IsNullOrWhiteSpace(x)||c<0)throw new ArgumentException("Thông tin bảo trì không hợp lệ.");return _d.TaoBaoTriAsync(t,n,x,c);}}
