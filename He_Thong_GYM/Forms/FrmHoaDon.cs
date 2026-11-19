using He_Thong_GYM.BLL;
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
        if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
        await TaiDanhSachAsync();
        _dangKhoiTao = false;
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
            lblThongKe.Text = "Lỗi tải hóa đơn";
            MessageBox.Show(ex.Message, "Lỗi tải hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dgvHoaDon.Columns[nameof(HoaDonView.MaHD)]!.HeaderText = "Mã HĐ";
            dgvHoaDon.Columns[nameof(HoaDonView.MaHD)]!.Width = 80;
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
        using var frm = new FrmThanhToan();
        if (frm.ShowDialog(this) == DialogResult.OK)
        {
            await TaiDanhSachAsync();
        }
    }

    private async void btnLamMoi_Click(object? sender, EventArgs e)
    {
        txtTimKiem.Clear();
        if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
        await TaiDanhSachAsync();
    }
}
