using He_Thong_GYM.BLL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmSanPham : Form
{
    private readonly SanPhamBLL _bll = new();
    private List<SanPham> _danhSachGoc = new();
    private bool _dangKhoiTao = true;

    public FrmSanPham()
    {
        InitializeComponent();
    }

    private async void FrmSanPham_Load(object? sender, EventArgs e)
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
            _danhSachGoc = await _bll.LayAsync();
            ApDungLoc();
        }
        catch (Exception ex)
        {
            lblThongKe.Text = "Lỗi tải sản phẩm";
            MessageBox.Show(ex.Message, "Lỗi tải sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ketQua = ketQua.Where(s =>
                (!string.IsNullOrEmpty(s.TenSP) && s.TenSP.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                s.MaSP.ToString().Contains(tuKhoa));
        }

        if (cboTrangThai.SelectedIndex > 0)
        {
            var trangThaiChon = cboTrangThai.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(trangThaiChon))
            {
                ketQua = ketQua.Where(s => string.Equals(s.TrangThai, trangThaiChon, StringComparison.OrdinalIgnoreCase));
            }
        }

        var list = ketQua.ToList();
        dgvSanPham.DataSource = null;
        dgvSanPham.DataSource = list;
        DinhDangLuoi();

        lblThongKe.Text = $"Hiển thị: {list.Count} / {_danhSachGoc.Count} sản phẩm";
    }

    private void DinhDangLuoi()
    {
        if (dgvSanPham.Columns.Count == 0) return;

        if (dgvSanPham.Columns.Contains(nameof(SanPham.MaSP)))
        {
            dgvSanPham.Columns[nameof(SanPham.MaSP)]!.HeaderText = "Mã SP";
            dgvSanPham.Columns[nameof(SanPham.MaSP)]!.Width = 80;
        }
        if (dgvSanPham.Columns.Contains(nameof(SanPham.TenSP)))
        {
            dgvSanPham.Columns[nameof(SanPham.TenSP)]!.HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns[nameof(SanPham.TenSP)]!.FillWeight = 180;
        }
        if (dgvSanPham.Columns.Contains(nameof(SanPham.DonGia)))
        {
            dgvSanPham.Columns[nameof(SanPham.DonGia)]!.HeaderText = "Đơn giá (VNĐ)";
            dgvSanPham.Columns[nameof(SanPham.DonGia)]!.DefaultCellStyle.Format = "N0";
            dgvSanPham.Columns[nameof(SanPham.DonGia)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSanPham.Columns[nameof(SanPham.DonGia)]!.Width = 130;
        }
        if (dgvSanPham.Columns.Contains(nameof(SanPham.SoLuongTon)))
        {
            dgvSanPham.Columns[nameof(SanPham.SoLuongTon)]!.HeaderText = "Số lượng tồn";
            dgvSanPham.Columns[nameof(SanPham.SoLuongTon)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSanPham.Columns[nameof(SanPham.SoLuongTon)]!.Width = 120;
        }
        if (dgvSanPham.Columns.Contains(nameof(SanPham.TrangThai)))
        {
            dgvSanPham.Columns[nameof(SanPham.TrangThai)]!.HeaderText = "Trạng thái";
            dgvSanPham.Columns[nameof(SanPham.TrangThai)]!.Width = 140;
        }
    }

    private async void btnBanHang_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmBanHang();
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
