using He_Thong_GYM.DAL;
using He_Thong_GYM.Models;
using He_Thong_GYM.Helpers;

namespace He_Thong_GYM.BLL;
public sealed class GoiTapBLL
{
    private readonly GoiTapDAL _dal=new();
    public static IReadOnlyList<GoiTapDichVuOption> DanhMucDichVu { get; } = new[]
    {
        new GoiTapDichVuOption { MaDichVu="PHONG_TAP", TenDichVu="Phòng tập" },
        new GoiTapDichVuOption { MaDichVu="LOP_NHOM", TenDichVu="Lớp nhóm" },
        new GoiTapDichVuOption { MaDichVu="LOP_YOGA", TenDichVu="Lớp Yoga" },
        new GoiTapDichVuOption { MaDichVu="KHU_CARDIO", TenDichVu="Khu Cardio" },
        new GoiTapDichVuOption { MaDichVu="KHU_VIP", TenDichVu="Khu vực VIP" },
        new GoiTapDichVuOption { MaDichVu="PT_UU_TIEN", TenDichVu="Ưu tiên đặt lịch PT" },
        new GoiTapDichVuOption { MaDichVu="PT", TenDichVu="Huấn luyện cá nhân (PT)" }
    };

    public Task<List<GoiTap>> LayDanhSachAsync(string? tuKhoa=null)=>_dal.LayDanhSachAsync(tuKhoa);
    public Task<List<PhongTapModel>> LayPhongHoatDongAsync()=>_dal.LayPhongHoatDongAsync();
    public Task<CauHinhQuyenGoiTap> LayQuyenAsync(int maGoi)=>_dal.LayQuyenAsync(maGoi);

    public async Task<int> LuuAsync(
        GoiTap g,
        IReadOnlyCollection<GoiTapDichVuOption> dichVu,
        IReadOnlyCollection<int> maPhong)
    {
        AuthorizationPolicy.Require("Admin", "Quản lý");
        if (string.IsNullOrWhiteSpace(g.TenGoi)) throw new ArgumentException("Tên gói không được để trống.");
        if (g.ThoiHanNgay<=0) throw new ArgumentException("Thời hạn phải lớn hơn 0.");
        if (g.Gia<0) throw new ArgumentException("Giá gói không hợp lệ.");

        var dichVuHopLe = DanhMucDichVu
            .Where(x => dichVu.Any(y => string.Equals(y.MaDichVu, x.MaDichVu, StringComparison.OrdinalIgnoreCase)))
            .ToList();
        if (dichVuHopLe.Count == 0) throw new ArgumentException("Gói phải có ít nhất một dịch vụ được cấp quyền.");

        bool coQuyenPhong = dichVuHopLe.Any(x => x.MaDichVu == "PHONG_TAP");
        if (coQuyenPhong && maPhong.Count == 0)
            throw new ArgumentException("Đã chọn dịch vụ Phòng tập nhưng chưa chọn phòng được phép sử dụng.");
        if (!coQuyenPhong && maPhong.Count > 0)
            throw new ArgumentException("Chỉ được chọn phòng khi gói có dịch vụ Phòng tập.");

        return await _dal.LuuAsync(g, dichVuHopLe, maPhong.Distinct().ToArray());
    }

    public Task NgungApDungAsync(int maGoi){AuthorizationPolicy.Require("Admin", "Quản lý");return _dal.NgungApDungAsync(maGoi);}
}
