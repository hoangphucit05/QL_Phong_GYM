namespace He_Thong_GYM.Models;

public sealed class CauHinhQuyenGoiTap
{
    public HashSet<string> MaDichVu { get; init; } = new(StringComparer.OrdinalIgnoreCase);
    public HashSet<int> MaPhong { get; init; } = new();
}
