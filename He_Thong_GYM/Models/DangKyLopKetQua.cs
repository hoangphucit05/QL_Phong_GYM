namespace He_Thong_GYM.Models;

/// <summary>
/// Kết quả đăng ký lớp. MaDangKy bằng null khi hội viên được đưa vào danh sách chờ.
/// </summary>
public sealed class DangKyLopKetQua
{
    public int? MaDangKy { get; init; }
    public bool DaVaoDanhSachCho => MaDangKy is null;
}
