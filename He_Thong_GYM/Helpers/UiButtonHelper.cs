using System.Drawing;
using System.Windows.Forms;

namespace He_Thong_GYM.Helpers;

/// <summary>
/// Quản lý trạng thái trực quan cho các nút bấm:
/// - Khi kích hoạt: màu sắc nhận diện chuẩn, chữ trắng, con trỏ bàn tay.
/// - Khi ở trạng thái nghỉ (disabled): nền xám nhạt hiện đại, chữ xám mờ, con trỏ mặc định, ngăn thao tác nhầm.
/// </summary>
public static class UiButtonHelper
{
    // Màu trạng thái nghỉ (Modern Slate Theme)
    public static readonly Color MauNghiNen = Color.FromArgb(226, 232, 240); // Slate-200
    public static readonly Color MauNghiChu = Color.FromArgb(148, 163, 184); // Slate-400

    // Bảng màu nhận diện chức năng chuẩn
    public static readonly Color MauThemMoi = Color.FromArgb(37, 99, 235);    // Blue-600
    public static readonly Color MauChinhSua = Color.FromArgb(217, 119, 6);    // Amber-600
    public static readonly Color MauLuu = Color.FromArgb(16, 185, 129);        // Emerald-500
    public static readonly Color MauXoa = Color.FromArgb(220, 38, 38);         // Red-600
    public static readonly Color MauLamMoi = Color.FromArgb(71, 85, 105);      // Slate-600
    public static readonly Color MauChucNangPhu = Color.FromArgb(79, 70, 229); // Indigo-600

    /// <summary>
    /// Đặt trạng thái hoạt động hoặc trạng thái nghỉ cho nút bấm kèm phong cách màu sắc chuẩn.
    /// </summary>
    public static void SetTrangThai(Button btn, bool bat, Color? mauNenKichHoat = null, Color? mauChuKichHoat = null)
    {
        btn.Enabled = bat;
        if (bat)
        {
            if (mauNenKichHoat.HasValue)
            {
                btn.BackColor = mauNenKichHoat.Value;
            }
            btn.ForeColor = mauChuKichHoat ?? Color.White;
            btn.Cursor = Cursors.Hand;
        }
        else
        {
            btn.BackColor = MauNghiNen;
            btn.ForeColor = MauNghiChu;
            btn.Cursor = Cursors.Default;
        }
    }
}
