using He_Thong_GYM.BLL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmPhongTap : Form
{
    private readonly TaiSanBLL _bll = new();
    private List<PhongTapModel> _danhSachGoc = new();
    private bool _dangKhoiTao = true;

    public FrmPhongTap()
    {
        InitializeComponent();
    }

    private async void FrmPhongTap_Load(object? sender, EventArgs e)
    {
        _dangKhoiTao = true;
        if (cboLoai.Items.Count > 0) cboLoai.SelectedIndex = 0;
        if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
        await TaiDanhSachAsync();
        _dangKhoiTao = false;
    }

    private async Task TaiDanhSachAsync()
    {
        try
        {
            _danhSachGoc = await _bll.LayPhongAsync();
            ApDungLoc();
        }
        catch (Exception ex)
        {
            lblThongKe.Text = "Lỗi tải phòng tập";
            MessageBox.Show(ex.Message, "Lỗi tải dữ liệu phòng tập", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ketQua = ketQua.Where(p =>
                (!string.IsNullOrEmpty(p.TenPhong) && p.TenPhong.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(p.LoaiPhong) && p.LoaiPhong.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                p.MaPhong.ToString().Contains(tuKhoa));
        }

        if (cboLoai.SelectedIndex > 0)
        {
            var loaiChon = cboLoai.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(loaiChon))
            {
                ketQua = ketQua.Where(p => string.Equals(p.LoaiPhong, loaiChon, StringComparison.OrdinalIgnoreCase));
            }
        }

        if (cboTrangThai.SelectedIndex > 0)
        {
            var trangThaiChon = cboTrangThai.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(trangThaiChon))
            {
                ketQua = ketQua.Where(p => string.Equals(p.TrangThai, trangThaiChon, StringComparison.OrdinalIgnoreCase));
            }
        }

        if (nudSucChua.Value > 0)
        {
            ketQua = ketQua.Where(p => p.SucChua >= (int)nudSucChua.Value);
        }

        var list = ketQua.ToList();
        dgv.DataSource = null;
        dgv.DataSource = list;
        DinhDangLuoi();

        lblThongKe.Text = $"Hiển thị: {list.Count} / {_danhSachGoc.Count} phòng tập";
    }

    private void DinhDangLuoi()
    {
        if (dgv.Columns.Count == 0) return;

        if (dgv.Columns.Contains(nameof(PhongTapModel.MaPhong)))
        {
            dgv.Columns[nameof(PhongTapModel.MaPhong)]!.HeaderText = "Mã phòng";
            dgv.Columns[nameof(PhongTapModel.MaPhong)]!.Width = 85;
        }
        if (dgv.Columns.Contains(nameof(PhongTapModel.TenPhong)))
        {
            dgv.Columns[nameof(PhongTapModel.TenPhong)]!.HeaderText = "Tên phòng tập";
            dgv.Columns[nameof(PhongTapModel.TenPhong)]!.FillWeight = 160;
        }
        if (dgv.Columns.Contains(nameof(PhongTapModel.LoaiPhong)))
        {
            dgv.Columns[nameof(PhongTapModel.LoaiPhong)]!.HeaderText = "Loại phòng";
            dgv.Columns[nameof(PhongTapModel.LoaiPhong)]!.FillWeight = 120;
        }
        if (dgv.Columns.Contains(nameof(PhongTapModel.SucChua)))
        {
            dgv.Columns[nameof(PhongTapModel.SucChua)]!.HeaderText = "Sức chứa";
            dgv.Columns[nameof(PhongTapModel.SucChua)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[nameof(PhongTapModel.SucChua)]!.Width = 95;
        }
        if (dgv.Columns.Contains(nameof(PhongTapModel.DienTich)))
        {
            dgv.Columns[nameof(PhongTapModel.DienTich)]!.HeaderText = "Diện tích (m²)";
            dgv.Columns[nameof(PhongTapModel.DienTich)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[nameof(PhongTapModel.DienTich)]!.DefaultCellStyle.Format = "N1";
            dgv.Columns[nameof(PhongTapModel.DienTich)]!.Width = 110;
        }
        if (dgv.Columns.Contains(nameof(PhongTapModel.Tang)))
        {
            dgv.Columns[nameof(PhongTapModel.Tang)]!.HeaderText = "Tầng";
            dgv.Columns[nameof(PhongTapModel.Tang)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[nameof(PhongTapModel.Tang)]!.Width = 70;
        }
        if (dgv.Columns.Contains(nameof(PhongTapModel.TrangThai)))
        {
            dgv.Columns[nameof(PhongTapModel.TrangThai)]!.HeaderText = "Trạng thái";
            dgv.Columns[nameof(PhongTapModel.TrangThai)]!.Width = 120;
        }
    }

    private async void btnLamMoi_Click(object? sender, EventArgs e)
    {
        txtTen.Clear();
        nudSucChua.Value = 0;
        if (cboLoai.Items.Count > 0) cboLoai.SelectedIndex = 0;
        if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
        await TaiDanhSachAsync();
    }
}
