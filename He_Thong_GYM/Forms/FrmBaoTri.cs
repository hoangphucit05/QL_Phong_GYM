using He_Thong_GYM.BLL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmBaoTri : Form
{
    private readonly TaiSanBLL _bll = new();
    private readonly NhanVienBLL _nv = new();
    private List<ThietBi> _tb = new();

    public FrmBaoTri()
    {
        InitializeComponent();
    }

    private async void FrmBaoTri_Load(object? sender, EventArgs e)
    {
        try
        {
            _tb = await _bll.LayThietBiAsync();
            cboThietBi.DataSource = _tb;
            cboThietBi.DisplayMember = nameof(ThietBi.TenTB);

            cboNhanVien.DataSource = await _nv.LayNhanVienDangLamAsync();
            cboNhanVien.DisplayMember = nameof(NhanVien.HoTen);

            dgv.DataSource = await _bll.LayBaoTriAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnTao_Click(object? sender, EventArgs e)
    {
        try
        {
            if (cboThietBi.SelectedItem is not ThietBi t || cboNhanVien.SelectedItem is not NhanVien n)
            {
                throw new InvalidOperationException("Hãy chọn thiết bị và nhân viên.");
            }

            await _bll.TaoBaoTriAsync(t.MaTB, n.MaNV, txtNoiDung.Text, nudChiPhi.Value);
            MessageBox.Show("Đã tạo phiếu bảo trì thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtNoiDung.Clear();
            nudChiPhi.Value = 0;
            dgv.DataSource = await _bll.LayBaoTriAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể tạo bảo trì", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
