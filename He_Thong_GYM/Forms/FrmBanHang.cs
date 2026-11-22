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
            var dsThanhVien = await _dk.LayThanhVienAsync();
            cboThanhVien.DataSource = dsThanhVien;
            cboThanhVien.DisplayMember = nameof(ThanhVien.HienThi);
            cboThanhVien.ValueMember = nameof(ThanhVien.MaTV);
            var khachLe = dsThanhVien.FirstOrDefault(x => x.HoTen.Contains("Khách vãng lai") || x.SDT == "0900000000");
            if (khachLe != null)
            {
                cboThanhVien.SelectedItem = khachLe;
            }

            cboSanPham.DataSource = await _sp.LayAsync();
            cboSanPham.DisplayMember = nameof(SanPham.HienThi);

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
