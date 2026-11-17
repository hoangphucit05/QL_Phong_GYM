using He_Thong_GYM.BLL;

namespace He_Thong_GYM.Forms;

public partial class FrmSanPham : Form
{
    private readonly SanPhamBLL _bll = new();

    public FrmSanPham()
    {
        InitializeComponent();
    }

    private async void FrmSanPham_Load(object? sender, EventArgs e)
    {
        await TaiDanhSachAsync();
    }

    private async Task TaiDanhSachAsync()
    {
        try
        {
            dgvSanPham.DataSource = await _bll.LayAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi tải sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnBanHang_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmBanHang();
        if (frm.ShowDialog(this) == DialogResult.OK)
        {
            await TaiDanhSachAsync();
        }
    }

    private async void btnLamMoi_Click(object? sender, EventArgs e)
    {
        await TaiDanhSachAsync();
    }
}
