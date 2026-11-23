namespace He_Thong_GYM.Models;

public sealed class GoiTapDichVuOption
{
    public string MaDichVu { get; init; } = string.Empty;
    public string TenDichVu { get; init; } = string.Empty;

    public override string ToString() => TenDichVu;
}
