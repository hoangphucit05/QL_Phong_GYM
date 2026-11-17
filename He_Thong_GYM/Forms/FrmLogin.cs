using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;

namespace He_Thong_GYM.Forms;

public partial class FrmLogin : Form
{
    private readonly DangNhapBLL _dangNhapBLL = new();

    public FrmLogin()
    {
        InitializeComponent();
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
        catch
        {
            lblThongBao.Text = "Không thể kết nối dữ liệu. Kiểm tra appsettings.json và SQL Server.";
        }
        finally
        {
            btnDangNhap.Enabled = true;
        }
    }

    private void chkHienMatKhau_CheckedChanged(object? sender, EventArgs e) => txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
}
