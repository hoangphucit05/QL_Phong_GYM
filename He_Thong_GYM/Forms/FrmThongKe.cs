using He_Thong_GYM.DAL;

namespace He_Thong_GYM.Forms;

public partial class FrmThongKe : Form
{
    private readonly ThongKeDAL _dal = new();

    public FrmThongKe()
    {
        InitializeComponent();
    }

    private async void FrmThongKe_Load(object? sender, EventArgs e)
    {
        try
        {
            var k = await _dal.LayAsync();
            lblThanhVien.Text = $"{k.GetValueOrDefault("Thành viên"):N0}";
            lblDoanhThu.Text = $"{k.GetValueOrDefault("Doanh thu thực"):N0} đ";
            lblCongNo.Text = $"{k.GetValueOrDefault("Công nợ"):N0} đ";
            lblTonKho.Text = $"{k.GetValueOrDefault("Giá trị tồn"):N0} đ";
            lblBaoTri.Text = $"{k.GetValueOrDefault("Chi phí bảo trì"):N0} đ";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi tải báo cáo thống kê", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
