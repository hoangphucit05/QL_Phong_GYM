using He_Thong_GYM.DAL;

namespace He_Thong_GYM.Forms;

public partial class FrmKhuyenMai : Form
{
    private readonly KhuyenMaiDAL _dal = new();

    public FrmKhuyenMai()
    {
        InitializeComponent();
        btnLamMoi.Click += btnLamMoi_Click;
    }

    private async void FrmKhuyenMai_Load(object? sender, EventArgs e)
    {
        await TaiDanhSachAsync();
    }

    private async Task TaiDanhSachAsync()
    {
        try
        {
            dgvKhuyenMai.DataSource = await _dal.LayTatCaAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnLamMoi_Click(object? sender, EventArgs e)
    {
        txtTen.Clear();
        nudGiam.Value = 0;
        dtpTu.Value = DateTime.Today;
        dtpDen.Value = DateTime.Today.AddMonths(1);
        await TaiDanhSachAsync();
    }
}
