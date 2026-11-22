using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmHoaDon : Form
{
    private readonly TaiChinhBLL _bll = new();
    private List<HoaDonView> _danhSachGoc = new();
    private bool _dangKhoiTao = true;

    public FrmHoaDon()
    {
        InitializeComponent();
    }

    private async void FrmHoaDon_Load(object? sender, EventArgs e)
    {
        _dangKhoiTao = true;
        dgvHoaDon.SelectionChanged += dgvHoaDon_SelectionChanged;
        if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
        CapNhatTrangThaiNut();
        await TaiDanhSachAsync();
        CapNhatTrangThaiNut();
        _dangKhoiTao = false;
    }

    private void dgvHoaDon_SelectionChanged(object? sender, EventArgs e)
    {
        CapNhatTrangThaiNut();
    }

    private void CapNhatTrangThaiNut()
    {
        var hoaDon = dgvHoaDon.CurrentRow?.DataBoundItem as HoaDonView;
        bool coChon = hoaDon != null;
        bool chuaHuy = coChon && !string.Equals(hoaDon!.TrangThai, "Hủy", StringComparison.OrdinalIgnoreCase);

        UiButtonHelper.SetTrangThai(btnThanhToan, coChon, Color.FromArgb(5, 150, 105));
        UiButtonHelper.SetTrangThai(btnHuyHoaDon, chuaHuy, Color.FromArgb(220, 38, 38));
    }

    private async Task TaiDanhSachAsync()
    {
        try
        {
            _danhSachGoc = await _bll.LayHoaDonAsync();
            ApDungLoc();
        }
        catch (Exception ex)
        {
            lblThongKe.Text = "Không tải được hóa đơn.";
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi tải hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BoLocThayDoi(object? sender, EventArgs e)
    {
        if (_dangKhoiTao) return;
        ApDungLoc();
    }

    private void ApDungLoc()
    {
        var ketQua = _danhSachGoc.AsEnumerable();

        var tuKhoa = txtTimKiem.Text.Trim();
        if (!string.IsNullOrEmpty(tuKhoa))
        {
            ketQua = ketQua.Where(h =>
                (!string.IsNullOrEmpty(h.ThanhVien) && h.ThanhVien.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(h.SoHoaDon) && h.SoHoaDon.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                h.MaHD.ToString().Contains(tuKhoa));
        }

        if (cboTrangThai.SelectedIndex > 0)
        {
            var trangThaiChon = cboTrangThai.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(trangThaiChon))
            {
                ketQua = ketQua.Where(h => string.Equals(h.TrangThai, trangThaiChon, StringComparison.OrdinalIgnoreCase));
            }
        }

        var list = ketQua.ToList();
        dgvHoaDon.DataSource = null;
        dgvHoaDon.DataSource = list;
        DinhDangLuoi();

        lblThongKe.Text = $"Hiển thị: {list.Count} / {_danhSachGoc.Count} hóa đơn";
    }

    private void DinhDangLuoi()
    {
        if (dgvHoaDon.Columns.Count == 0) return;

        if (dgvHoaDon.Columns.Contains(nameof(HoaDonView.MaHD)))
        {
            dgvHoaDon.Columns[nameof(HoaDonView.MaHD)]!.Visible = false;
        }
        if (dgvHoaDon.Columns.Contains(nameof(HoaDonView.SoHoaDon)))
        {
            dgvHoaDon.Columns[nameof(HoaDonView.SoHoaDon)]!.HeaderText = "Số hóa đơn";
            dgvHoaDon.Columns[nameof(HoaDonView.SoHoaDon)]!.Width = 155;
        }
        if (dgvHoaDon.Columns.Contains(nameof(HoaDonView.HienThi)))
        {
            dgvHoaDon.Columns[nameof(HoaDonView.HienThi)]!.Visible = false;
        }
        if (dgvHoaDon.Columns.Contains(nameof(HoaDonView.ThanhVien)))
        {
            dgvHoaDon.Columns[nameof(HoaDonView.ThanhVien)]!.HeaderText = "Hội viên / Khách hàng";
            dgvHoaDon.Columns[nameof(HoaDonView.ThanhVien)]!.FillWeight = 160;
        }
        if (dgvHoaDon.Columns.Contains(nameof(HoaDonView.NgayLap)))
        {
            dgvHoaDon.Columns[nameof(HoaDonView.NgayLap)]!.HeaderText = "Ngày lập";
            dgvHoaDon.Columns[nameof(HoaDonView.NgayLap)]!.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvHoaDon.Columns[nameof(HoaDonView.NgayLap)]!.Width = 140;
        }
        if (dgvHoaDon.Columns.Contains(nameof(HoaDonView.TongTien)))
        {
            dgvHoaDon.Columns[nameof(HoaDonView.TongTien)]!.HeaderText = "Tổng tiền (VNĐ)";
            dgvHoaDon.Columns[nameof(HoaDonView.TongTien)]!.DefaultCellStyle.Format = "N0";
            dgvHoaDon.Columns[nameof(HoaDonView.TongTien)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHoaDon.Columns[nameof(HoaDonView.TongTien)]!.Width = 130;
        }
        if (dgvHoaDon.Columns.Contains(nameof(HoaDonView.DaThanhToan)))
        {
            dgvHoaDon.Columns[nameof(HoaDonView.DaThanhToan)]!.HeaderText = "Đã trả (VNĐ)";
            dgvHoaDon.Columns[nameof(HoaDonView.DaThanhToan)]!.DefaultCellStyle.Format = "N0";
            dgvHoaDon.Columns[nameof(HoaDonView.DaThanhToan)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHoaDon.Columns[nameof(HoaDonView.DaThanhToan)]!.Width = 130;
        }
        if (dgvHoaDon.Columns.Contains(nameof(HoaDonView.ConNo)))
        {
            dgvHoaDon.Columns[nameof(HoaDonView.ConNo)]!.HeaderText = "Còn nợ (VNĐ)";
            dgvHoaDon.Columns[nameof(HoaDonView.ConNo)]!.DefaultCellStyle.Format = "N0";
            dgvHoaDon.Columns[nameof(HoaDonView.ConNo)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHoaDon.Columns[nameof(HoaDonView.ConNo)]!.Width = 130;
        }
        if (dgvHoaDon.Columns.Contains(nameof(HoaDonView.TrangThai)))
        {
            dgvHoaDon.Columns[nameof(HoaDonView.TrangThai)]!.HeaderText = "Trạng thái";
            dgvHoaDon.Columns[nameof(HoaDonView.TrangThai)]!.Width = 130;
        }
    }

    private async void btnThanhToan_Click(object? sender, EventArgs e)
    {
        if (dgvHoaDon.CurrentRow?.DataBoundItem is not HoaDonView hoaDon)
        {
            MessageBox.Show("Hãy chọn hóa đơn cần thanh toán hoặc hoàn tiền.", "Chưa chọn hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var frm = new FrmThanhToan(hoaDon.MaHD);
        if (frm.ShowDialog(this) == DialogResult.OK)
        {
            await TaiDanhSachAsync();
        }
    }

    private async void btnHuyHoaDon_Click(object? sender, EventArgs e)
    {
        if (dgvHoaDon.CurrentRow?.DataBoundItem is not HoaDonView hoaDon)
        {
            MessageBox.Show("Hãy chọn hóa đơn cần hủy.", "Chưa chọn hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (string.Equals(hoaDon.TrangThai, "Hủy", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show("Hóa đơn này đã được hủy trước đó.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (hoaDon.DaThanhToan > 0 || string.Equals(hoaDon.TrangThai, "Đã thanh toán", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show("Hóa đơn đã có phát sinh thanh toán. Theo quy định nghiệp vụ, vui lòng thực hiện Hoàn tiền đầy đủ cho khách hàng trước khi hủy hóa đơn.", "Yêu cầu hoàn tiền trước", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var xacNhan = MessageBox.Show(
            $"Bạn có chắc chắn muốn hủy hóa đơn #{hoaDon.SoHoaDon} (Tổng tiền: {hoaDon.TongTien:N0} đ)?\n\nThao tác này sẽ ghi nhận hủy hóa đơn và tự động hoàn trả số lượng tồn kho sản phẩm (nếu có).",
            "Xác nhận hủy hóa đơn",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (xacNhan != DialogResult.Yes) return;

        try
        {
            await _bll.HuyHoaDonAsync(hoaDon.MaHD, "Hủy hóa đơn từ giao diện Quản lý", SessionManager.NguoiDungHienTai?.MaNV);
            MessageBox.Show("Đã hủy hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await TaiDanhSachAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không thể hủy hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnLamMoi_Click(object? sender, EventArgs e)
    {
        txtTimKiem.Clear();
        if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
        await TaiDanhSachAsync();
    }
}
