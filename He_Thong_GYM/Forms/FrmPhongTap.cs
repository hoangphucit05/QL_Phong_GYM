using He_Thong_GYM.BLL;

namespace He_Thong_GYM.Forms;

public partial class FrmPhongTap : Form
{
    private readonly TaiSanBLL _bll = new();

    public FrmPhongTap()
    {
        InitializeComponent();
    }

    private async void FrmPhongTap_Load(object? sender, EventArgs e)
    {
        try
        {
            dgv.DataSource = await _bll.LayPhongAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
