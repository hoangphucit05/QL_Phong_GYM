using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;

namespace He_Thong_GYM.Forms;

public partial class FrmLogin : Form
{
    private readonly DangNhapBLL _dangNhapBLL = new();

    public FrmLogin()
    {
        InitializeComponent();
        TaiBranding();
    }

    private void TaiBranding()
    {
        // 1. Tải ảnh nền phòng gym (kèm lớp phủ tối) cho panel bên trái
        try
        {
            string[] cacDuongDanNen = [
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "bg_login_branding.png"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources", "bg_login_branding.png"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "banner_GYM.png"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", "banner_GYM.png"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "images", "banner_GYM.png")
            ];

            foreach (var path in cacDuongDanNen)
            {
                if (File.Exists(path))
                {
                    using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    using var temp = Image.FromStream(stream);
                    panelThuongHieu.BackgroundImage = new Bitmap(temp);
                    break;
                }
            }
        }
        catch
        {
        }

        // 2. Tải huy hiệu neon xanh lam ở trung tâm
        try
        {
            string[] cacDuongDanLogo = [
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Huy hiệu GYM PRO neon xanh lam.png"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "logo_neon.png"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", "Huy hiệu GYM PRO neon xanh lam.png"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "images", "Huy hiệu GYM PRO neon xanh lam.png"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources", "logo_neon.png"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "logo_gym.png")
            ];

            foreach (var path in cacDuongDanLogo)
            {
                if (File.Exists(path))
                {
                    using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    using var temp = Image.FromStream(stream);
                    picLogo.Image = new Bitmap(temp);
                    return;
                }
            }
        }
        catch
        {
        }

        try
        {
            picLogo.Image = TaoLogoGymMoi(100, 100);
        }
        catch
        {
        }
    }

    private static Bitmap TaoLogoGymMoi(int width, int height)
    {
        var bmp = new Bitmap(width, height);
        using var g = Graphics.FromImage(bmp);
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        // Vòng tròn bảo hộ / viền vàng kim loại cao cấp
        using var penVang = new Pen(Color.FromArgb(245, 158, 11), 3.5f);
        g.DrawEllipse(penVang, 6, 6, width - 12, height - 12);

        // Biểu tượng tạ đòn GYM (Dumbbell) ở trung tâm màu trắng
        using var brushTrang = new SolidBrush(Color.White);
        using var penTrang = new Pen(Color.White, 4f);

        int cx = width / 2;
        int cy = height / 2;

        // Thanh đòn
        g.DrawLine(penTrang, cx - 22, cy, cx + 22, cy);

        // Bánh tạ lớn 2 bên
        g.FillRectangle(brushTrang, cx - 24, cy - 14, 5, 28);
        g.FillRectangle(brushTrang, cx + 19, cy - 14, 5, 28);

        // Bánh tạ nhỏ phía ngoài
        g.FillRectangle(brushTrang, cx - 30, cy - 10, 4, 20);
        g.FillRectangle(brushTrang, cx + 26, cy - 10, 4, 20);

        // Chốt tạ
        g.FillRectangle(brushTrang, cx - 33, cy - 4, 2, 8);
        g.FillRectangle(brushTrang, cx + 31, cy - 4, 2, 8);

        // Khối trung tâm
        using var brushVang = new SolidBrush(Color.FromArgb(245, 158, 11));
        g.FillEllipse(brushVang, cx - 5, cy - 5, 10, 10);

        return bmp;
    }

    private async void btnDangNhap_Click(object? sender, EventArgs e)
    {
        btnDangNhap.Enabled = false;
        lblThongBao.Text = "Đang xác thực...";
        try
        {
            var nguoiDung = await _dangNhapBLL.DangNhapAsync(txtTenDangNhap.Text, txtMatKhau.Text);
            SessionManager.DangNhap(nguoiDung);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex) when (ex is ArgumentException or UnauthorizedAccessException or InvalidOperationException)
        {
            lblThongBao.Text = ex.Message;
        }
        catch (Exception ex)
        {
            _ = UiErrorMessage.Get(ex);
            lblThongBao.Text = "Không thể kết nối dữ liệu. Kiểm tra appsettings.json và SQL Server.";
        }
        finally
        {
            btnDangNhap.Enabled = true;
        }
    }

    private void chkHienMatKhau_CheckedChanged(object? sender, EventArgs e) => txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
}
