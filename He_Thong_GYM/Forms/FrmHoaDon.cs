using He_Thong_GYM.BLL;

namespace He_Thong_GYM.Forms;

public partial class FrmHoaDon : Form
{
    private readonly TaiChinhBLL _bll = new();

    public FrmHoaDon()
    {
        InitializeComponent();
    }

    private async void FrmHoaDon_Load(object? sender, EventArgs e)
    {
        await TaiDanhSachAsync();
    }

    private async Task TaiDanhSachAsync()
    {
        try
        {
            dgvHoaDon.DataSource = await _bll.LayHoaDonAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi tải hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnThanhToan_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmThanhToan();
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
