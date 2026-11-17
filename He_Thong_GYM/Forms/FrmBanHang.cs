using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmBanHang : Form
{
    private readonly SanPhamBLL _sp = new();
    private readonly DangKyBLL _dk = new();

    public FrmBanHang()
    {
        InitializeComponent();
        cboSanPham.SelectedIndexChanged += GiaThayDoi;
        nudSoLuong.ValueChanged += GiaThayDoi;
    }

    private async void FrmBanHang_Load(object? sender, EventArgs e)
    {
        try
        {
            cboThanhVien.DataSource = await _dk.LayThanhVienAsync();
            cboThanhVien.DisplayMember = nameof(ThanhVien.HoTen);

            cboSanPham.DataSource = await _sp.LayAsync();
            cboSanPham.DisplayMember = nameof(SanPham.TenSP);

            TinhTien();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi tải danh mục", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void TinhTien()
    {
        if (cboSanPham.SelectedItem is SanPham p)
        {
            lblThanhTien.Text = $"{p.DonGia * nudSoLuong.Value:N0} đ | Tồn kho: {p.SoLuongTon}";
        }
        else
        {
            lblThanhTien.Text = "0 đ";
        }
    }

    private async void btnBan_Click(object? sender, EventArgs e)
    {
        try
        {
            if (SessionManager.NguoiDungHienTai is null ||
                cboThanhVien.SelectedItem is not ThanhVien t ||
                cboSanPham.SelectedItem is not SanPham p)
            {
                throw new InvalidOperationException("Hãy chọn đầy đủ thành viên và sản phẩm cần bán.");
            }

            var maHD = await _sp.BanAsync(t.MaTV, SessionManager.NguoiDungHienTai.MaNV, p.MaSP, (int)nudSoLuong.Value);
            MessageBox.Show($"Đã tạo hóa đơn bán hàng #{maHD} thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể bán hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void GiaThayDoi(object? sender, EventArgs e) => TinhTien();

    private void btnHuy_Click(object? sender, EventArgs e) => Close();
}
