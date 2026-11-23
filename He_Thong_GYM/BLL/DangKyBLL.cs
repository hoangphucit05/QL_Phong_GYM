using He_Thong_GYM.DAL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.BLL;

public sealed class DangKyBLL
{
    private readonly DangKyDAL _dal = new();

    public Task<List<ThanhVien>> LayThanhVienAsync() => _dal.LayThanhVienAsync();
    public Task<List<GoiTap>> LayGoiTapAsync() => _dal.LayGoiTapAsync();
    public Task<List<KhuyenMai>> LayKhuyenMaiAsync() => _dal.LayKhuyenMaiAsync();
    public Task<List<HuanLuyenVien>> LayHLVAsync() => _dal.LayHLVAsync();
    public Task<List<LichSuDangKy>> LayLichSuGoiAsync() => _dal.LayLichSuGoiAsync();
    public Task<List<LichSuDangKy>> LayLichSuPTAsync() => _dal.LayLichSuPTAsync();

    public Task<(int MaDangKy, int MaHD)> DangKyGoiAsync(int tv, int goi, int? km, DateTime ngay, int nv)
    {
        AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên");
        return _dal.DangKyGoiAsync(tv, goi, km, ngay, nv);
    }

    public Task<(int MaDangKy, int MaHD)> DangKyPTAsync(int tv, int hlv, int buoi, int? km, DateTime ngay, DateTime ketThuc, int nv)
    {
        AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "Nhân viên");
        return _dal.DangKyPTAsync(tv, hlv, buoi, km, ngay, ketThuc, nv);
    }

    public Task<(int SoBuoiDaTap, int SoBuoiConLai, string TrangThaiMoi)> HoanThanhBuoiPTAsync(int maDKPT, string? nhanXet, int? maNV)
    {
        AuthorizationPolicy.Require("Admin", "Quản lý", "Lễ tân", "HLV", "Huấn luyện viên", "Nhân viên");
        if (maNV is null || maNV <= 0) throw new InvalidOperationException("Chưa xác định được tài khoản nhân viên thực hiện.");
        return _dal.HoanThanhBuoiPTAsync(maDKPT, nhanXet, maNV.Value);
    }

    public Task<List<BuoiTapPTModel>> LayLichSuBuoiTapPTAsync(int maDKPT)
    {
        return _dal.LayLichSuBuoiTapPTAsync(maDKPT);
    }
}
