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
        UiButtonHelper.SetTrangThai(btnBan, false, UiButtonHelper.MauThemMoi);
        cboThanhVien.SelectedIndexChanged += GiaThayDoi;
        cboSanPham.SelectedIndexChanged += GiaThayDoi;
        nudSoLuong.ValueChanged += GiaThayDoi;
    }

    private async void FrmBanHang_Load(object? sender, EventArgs e)
    {
        try
        {
            var dsThanhVien = await _dk.LayThanhVienAsync();
            cboThanhVien.DataSource = dsThanhVien;
            cboThanhVien.DisplayMember = nameof(ThanhVien.HienThi);
            cboThanhVien.ValueMember = nameof(ThanhVien.MaTV);
            cboThanhVien.SelectedIndex = -1;

            cboSanPham.DataSource = await _sp.LayAsync();
            cboSanPham.DisplayMember = nameof(SanPham.HienThi);
            cboSanPham.SelectedIndex = -1;

            TinhTien();
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi tải danh mục", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            lblThanhTien.Text = "Chưa chọn sản phẩm";
        }

        bool sanSang = cboThanhVien.SelectedItem is ThanhVien &&
                       cboSanPham.SelectedItem is SanPham p2 &&
                       p2.SoLuongTon > 0 &&
                       nudSoLuong.Value > 0;
        UiButtonHelper.SetTrangThai(btnBan, sanSang, UiButtonHelper.MauThemMoi);
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
            var hoiThanhToan = MessageBox.Show(
                $"Đã tạo hóa đơn bán hàng #{maHD} thành công!\nBạn có muốn mở màn hình thu tiền ngay bây giờ không?",
                "Bán hàng thành công",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (hoiThanhToan == DialogResult.Yes)
            {
                using var frmTT = new FrmThanhToan(maHD);
                frmTT.ShowDialog(this);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không thể bán hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void GiaThayDoi(object? sender, EventArgs e) => TinhTien();

    private void btnHuy_Click(object? sender, EventArgs e) => Close();
}
