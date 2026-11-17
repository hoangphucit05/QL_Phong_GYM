using He_Thong_GYM.DAL;using He_Thong_GYM.Models;
namespace He_Thong_GYM.BLL;
public sealed class TaiChinhBLL{private readonly TaiChinhDAL _d=new();public Task<List<HoaDonView>> LayHoaDonAsync()=>_d.LayHoaDonAsync();public Task ThanhToanAsync(int h,decimal t,string p,int n){if(t<=0)throw new ArgumentException("Số tiền phải lớn hơn 0.");return _d.ThanhToanAsync(h,t,p,n);}public Task HoanTienAsync(int h,decimal t,string p,int n){if(t<=0)throw new ArgumentException("Số tiền hoàn phải lớn hơn 0.");return _d.HoanTienAsync(h,t,p,n);}}
