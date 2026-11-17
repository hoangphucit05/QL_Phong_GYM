using He_Thong_GYM.Models;

namespace He_Thong_GYM.Helpers;

public static class SessionManager
{
    public static NguoiDung? NguoiDungHienTai { get; private set; }
    public static void DangNhap(NguoiDung nguoiDung) => NguoiDungHienTai = nguoiDung;
    public static void DangXuat() => NguoiDungHienTai = null;
    public static bool CoVaiTro(params string[] vaiTro) => NguoiDungHienTai is not null && vaiTro.Contains(NguoiDungHienTai.VaiTro, StringComparer.OrdinalIgnoreCase);
}
