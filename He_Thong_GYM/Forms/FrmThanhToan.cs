using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmThanhToan : Form
{
    private readonly TaiChinhBLL _bll = new();
    private List<HoaDonView> _hd = new();

    public FrmThanhToan()
    {
        InitializeComponent();
        cboHoaDon.SelectedIndexChanged += (s, e) => HienThi();
    }

    private async void FrmThanhToan_Load(object? sender, EventArgs e)
    {
        try
        {
            _hd = await _bll.LayHoaDonAsync();
            cboHoaDon.DataSource = _hd;
            cboHoaDon.DisplayMember = nameof(HoaDonView.MaHD);
            cboHoaDon.ValueMember = nameof(HoaDonView.MaHD);
            HienThi();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi tải hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void HienThi()
    {
        if (cboHoaDon.SelectedItem is HoaDonView h)
        {
            lblCongNo.Text = $"Tổng tiền: {h.TongTien:N0} đ | Đã trả: {h.DaThanhToan:N0} đ | Còn nợ: {h.ConNo:N0} đ";
            lblCongNo.ForeColor = h.ConNo > 0 ? Color.FromArgb(185, 28, 28) : Color.FromArgb(5, 150, 105);
            nudSoTien.Value = h.ConNo > 0 ? h.ConNo : 0;
        }
        else
        {
            lblCongNo.Text = "Không có thông tin hóa đơn.";
            lblCongNo.ForeColor = Color.FromArgb(71, 85, 105);
        }
    }

    private async void btnThanhToan_Click(object? sender, EventArgs e)
    {
        try
        {
            if (SessionManager.NguoiDungHienTai is null || cboHoaDon.SelectedItem is not HoaDonView h)
            {
                throw new InvalidOperationException("Chưa chọn hóa đơn cần thanh toán.");
            }

            if (nudSoTien.Value <= 0)
            {
                throw new InvalidOperationException("Vui lòng nhập số tiền thanh toán lớn hơn 0.");
            }

            await _bll.ThanhToanAsync(h.MaHD, nudSoTien.Value, cboPhuongThuc.Text, SessionManager.NguoiDungHienTai.MaNV);
            MessageBox.Show($"Thanh toán thành công {nudSoTien.Value:N0} đ cho hóa đơn #{h.MaHD}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể thanh toán", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async void btnHoanTien_Click(object? sender, EventArgs e)
    {
        try
        {
            if (SessionManager.NguoiDungHienTai is null || cboHoaDon.SelectedItem is not HoaDonView h)
            {
                throw new InvalidOperationException("Chưa chọn hóa đơn cần hoàn tiền.");
            }

            if (nudSoTien.Value <= 0)
            {
                throw new InvalidOperationException("Vui lòng nhập số tiền hoàn lại lớn hơn 0.");
            }

            var confirm = MessageBox.Show(
                $"Xác nhận hoàn tiền {nudSoTien.Value:N0} đ cho hóa đơn #{h.MaHD}?",
                "Xác nhận hoàn tiền",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            await _bll.HoanTienAsync(h.MaHD, nudSoTien.Value, cboPhuongThuc.Text, SessionManager.NguoiDungHienTai.MaNV);
            MessageBox.Show($"Đã hoàn tiền thành công {nudSoTien.Value:N0} đ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể hoàn tiền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnHuy_Click(object? sender, EventArgs e) => Close();
}
