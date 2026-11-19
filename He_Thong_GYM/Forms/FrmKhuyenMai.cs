using He_Thong_GYM.DAL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmKhuyenMai : Form
{
    private readonly KhuyenMaiDAL _dal = new();
    private List<KhuyenMai> _danhSachGoc = new();
    private bool _dangKhoiTao = true;

    public FrmKhuyenMai()
    {
        InitializeComponent();
    }

    private async void FrmKhuyenMai_Load(object? sender, EventArgs e)
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
            _danhSachGoc = await _dal.LayTatCaAsync();
            ApDungLoc();
        }
        catch (Exception ex)
        {
            lblThongKe.Text = "Lỗi tải dữ liệu";
            MessageBox.Show(ex.Message, "Lỗi tải dữ liệu khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        var tuKhoa = txtTen.Text.Trim();
        if (!string.IsNullOrEmpty(tuKhoa))
        {
            ketQua = ketQua.Where(k =>
                (!string.IsNullOrEmpty(k.TenKM) && k.TenKM.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(k.MaCode) && k.MaCode.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                (k.MaKM.HasValue && k.MaKM.Value.ToString().Contains(tuKhoa)));
        }

        if (nudGiam.Value > 0)
        {
            ketQua = ketQua.Where(k => k.PhanTramGiam >= nudGiam.Value);
        }

        if (cboTrangThai.SelectedIndex > 0)
        {
            var trangThaiChon = cboTrangThai.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(trangThaiChon))
            {
                ketQua = ketQua.Where(k => string.Equals(k.TrangThai, trangThaiChon, StringComparison.OrdinalIgnoreCase));
            }
        }

        var list = ketQua.ToList();
        dgvKhuyenMai.DataSource = null;
        dgvKhuyenMai.DataSource = list;
        DinhDangLuoi();

        lblThongKe.Text = $"Hiển thị: {list.Count} / {_danhSachGoc.Count} chương trình";
    }

    private void DinhDangLuoi()
    {
        if (dgvKhuyenMai.Columns.Count == 0) return;

        if (dgvKhuyenMai.Columns.Contains(nameof(KhuyenMai.MaCode)))
        {
            dgvKhuyenMai.Columns[nameof(KhuyenMai.MaCode)]!.HeaderText = "Mã khuyến mãi";
            dgvKhuyenMai.Columns[nameof(KhuyenMai.MaCode)]!.Width = 145;
            dgvKhuyenMai.Columns[nameof(KhuyenMai.MaCode)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvKhuyenMai.Columns[nameof(KhuyenMai.MaCode)]!.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvKhuyenMai.Columns[nameof(KhuyenMai.MaCode)]!.DefaultCellStyle.ForeColor = Color.FromArgb(37, 99, 235);
            dgvKhuyenMai.Columns[nameof(KhuyenMai.MaCode)]!.DisplayIndex = 0;
        }

        if (dgvKhuyenMai.Columns.Contains(nameof(KhuyenMai.TenKM)))
        {
            dgvKhuyenMai.Columns[nameof(KhuyenMai.TenKM)]!.HeaderText = "Tên chương trình khuyến mãi";
            dgvKhuyenMai.Columns[nameof(KhuyenMai.TenKM)]!.FillWeight = 200;
            dgvKhuyenMai.Columns[nameof(KhuyenMai.TenKM)]!.DisplayIndex = 1;
        }

        if (dgvKhuyenMai.Columns.Contains(nameof(KhuyenMai.PhanTramGiam)))
        {
            dgvKhuyenMai.Columns[nameof(KhuyenMai.PhanTramGiam)]!.HeaderText = "Giảm giá (%)";
            dgvKhuyenMai.Columns[nameof(KhuyenMai.PhanTramGiam)]!.Width = 110;
            dgvKhuyenMai.Columns[nameof(KhuyenMai.PhanTramGiam)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvKhuyenMai.Columns[nameof(KhuyenMai.PhanTramGiam)]!.DefaultCellStyle.Format = "N0";
            dgvKhuyenMai.Columns[nameof(KhuyenMai.PhanTramGiam)]!.DisplayIndex = 2;
        }

        if (dgvKhuyenMai.Columns.Contains(nameof(KhuyenMai.NgayBatDau)))
        {
            dgvKhuyenMai.Columns[nameof(KhuyenMai.NgayBatDau)]!.HeaderText = "Ngày bắt đầu";
            dgvKhuyenMai.Columns[nameof(KhuyenMai.NgayBatDau)]!.Width = 120;
            dgvKhuyenMai.Columns[nameof(KhuyenMai.NgayBatDau)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvKhuyenMai.Columns[nameof(KhuyenMai.NgayBatDau)]!.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvKhuyenMai.Columns[nameof(KhuyenMai.NgayBatDau)]!.DisplayIndex = 3;
        }

        if (dgvKhuyenMai.Columns.Contains(nameof(KhuyenMai.NgayKetThuc)))
        {
            dgvKhuyenMai.Columns[nameof(KhuyenMai.NgayKetThuc)]!.HeaderText = "Ngày kết thúc";
            dgvKhuyenMai.Columns[nameof(KhuyenMai.NgayKetThuc)]!.Width = 120;
            dgvKhuyenMai.Columns[nameof(KhuyenMai.NgayKetThuc)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvKhuyenMai.Columns[nameof(KhuyenMai.NgayKetThuc)]!.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvKhuyenMai.Columns[nameof(KhuyenMai.NgayKetThuc)]!.DisplayIndex = 4;
        }

        if (dgvKhuyenMai.Columns.Contains(nameof(KhuyenMai.TrangThai)))
        {
            dgvKhuyenMai.Columns[nameof(KhuyenMai.TrangThai)]!.HeaderText = "Trạng thái";
            dgvKhuyenMai.Columns[nameof(KhuyenMai.TrangThai)]!.Width = 110;
            dgvKhuyenMai.Columns[nameof(KhuyenMai.TrangThai)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvKhuyenMai.Columns[nameof(KhuyenMai.TrangThai)]!.DisplayIndex = 5;
        }

        if (dgvKhuyenMai.Columns.Contains(nameof(KhuyenMai.MaKM)))
        {
            dgvKhuyenMai.Columns[nameof(KhuyenMai.MaKM)]!.Visible = false;
        }

        if (dgvKhuyenMai.Columns.Contains(nameof(KhuyenMai.HienThi)))
        {
            dgvKhuyenMai.Columns[nameof(KhuyenMai.HienThi)]!.Visible = false;
        }
    }

    private async void btnLamMoi_Click(object? sender, EventArgs e)
    {
        txtTen.Clear();
        nudGiam.Value = 0;
        if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
        await TaiDanhSachAsync();
    }
}
