using He_Thong_GYM.DAL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.BLL;

public sealed class VanHanhBLL
{
    private readonly VanHanhDAL _dal = new();
    public Task<List<DangKyGoiView>> LayGoiThanhVienAsync(int maTV) => _dal.LayGoiThanhVienAsync(maTV);
    public Task<long> CheckInAsync(int maTV, int? maPhong) => _dal.CheckInAsync(maTV, maPhong);
    public Task CheckOutAsync(int maTV) => _dal.CheckOutAsync(maTV);
    public Task GiaHanAsync(int maDKG, int soNgay, int? maNV) => soNgay <= 0 ? throw new ArgumentException("Số ngày gia hạn phải lớn hơn 0.") : _dal.GiaHanAsync(maDKG, soNgay, maNV);
    public Task BaoLuuAsync(int maDKG, DateTime tu, DateTime den, string lyDo, int? maNV) => den < tu || string.IsNullOrWhiteSpace(lyDo) ? throw new ArgumentException("Khoảng bảo lưu hoặc lý do không hợp lệ.") : _dal.BaoLuuAsync(maDKG, tu, den, lyDo.Trim(), maNV);
}
