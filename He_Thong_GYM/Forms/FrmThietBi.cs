using He_Thong_GYM.BLL;

namespace He_Thong_GYM.Forms;

public partial class FrmThietBi : Form
{
    private readonly TaiSanBLL _bll = new();

    public FrmThietBi()
    {
        InitializeComponent();
    }

    private async void FrmThietBi_Load(object? sender, EventArgs e)
    {
        try
        {
            dgv.DataSource = await _bll.LayThietBiAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
