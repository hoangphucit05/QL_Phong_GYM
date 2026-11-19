using He_Thong_GYM.BLL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmLopTap : Form
{
    private readonly LopTapBLL _bll = new();
    private readonly DangKyBLL _dk = new();
    private List<LopTap> _danhSachGoc = new();
    private bool _dangKhoiTao = true;

    public FrmLopTap()
    {
        InitializeComponent();
    }

    private async void FrmLopTap_Load(object? sender, EventArgs e)
    {
        try
        {
            _dangKhoiTao = true;
            cboLoai.DataSource = (await _bll.LayLoaiAsync())
                .Select(x => new KeyValuePair<int, string>(x.Item1, x.Item2))
                .ToList();
            cboLoai.DisplayMember = "Value";
            cboLoai.ValueMember = "Key";

            cboHLV.DataSource = await _dk.LayHLVAsync();
            cboHLV.DisplayMember = nameof(HuanLuyenVien.HoTen);
            cboHLV.ValueMember = nameof(HuanLuyenVien.MaHLV);

            if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
            if (cboLocTrangThai.Items.Count > 0) cboLocTrangThai.SelectedIndex = 0;

            await TaiDanhSachAsync();
            _dangKhoiTao = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task TaiDanhSachAsync()
    {
        try
        {
            _danhSachGoc = await _bll.LayDanhSachAsync();
            ApDungLoc();
        }
        catch (Exception ex)
        {
            lblThongKe.Text = "Lỗi tải dữ liệu";
            MessageBox.Show(ex.Message, "Lỗi tải danh sách lớp", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ketQua = ketQua.Where(l =>
                (!string.IsNullOrEmpty(l.TenLop) && l.TenLop.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(l.TenLoaiLop) && l.TenLoaiLop.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(l.TenHLV) && l.TenHLV.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                l.MaLop.ToString().Contains(tuKhoa));
        }

        if (cboLocTrangThai.SelectedIndex > 0)
        {
            var trangThaiChon = cboLocTrangThai.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(trangThaiChon))
            {
                ketQua = ketQua.Where(l => string.Equals(l.TrangThai, trangThaiChon, StringComparison.OrdinalIgnoreCase));
            }
        }

        var list = ketQua.ToList();
        dgvLop.DataSource = null;
        dgvLop.DataSource = list;
        DinhDangLuoi();

        lblThongKe.Text = $"Hiển thị: {list.Count} / {_danhSachGoc.Count} lớp";
    }

    private void DinhDangLuoi()
    {
        if (dgvLop.Columns.Count == 0) return;

        if (dgvLop.Columns.Contains(nameof(LopTap.MaLop)))
        {
            dgvLop.Columns[nameof(LopTap.MaLop)]!.HeaderText = "Mã lớp";
            dgvLop.Columns[nameof(LopTap.MaLop)]!.Width = 75;
        }
        if (dgvLop.Columns.Contains(nameof(LopTap.TenLop)))
        {
            dgvLop.Columns[nameof(LopTap.TenLop)]!.HeaderText = "Tên lớp tập";
            dgvLop.Columns[nameof(LopTap.TenLop)]!.FillWeight = 160;
        }
        if (dgvLop.Columns.Contains(nameof(LopTap.TenLoaiLop)))
        {
            dgvLop.Columns[nameof(LopTap.TenLoaiLop)]!.HeaderText = "Thể loại";
            dgvLop.Columns[nameof(LopTap.TenLoaiLop)]!.FillWeight = 120;
        }
        if (dgvLop.Columns.Contains(nameof(LopTap.TenHLV)))
        {
            dgvLop.Columns[nameof(LopTap.TenHLV)]!.HeaderText = "Huấn luyện viên";
            dgvLop.Columns[nameof(LopTap.TenHLV)]!.FillWeight = 140;
        }
        if (dgvLop.Columns.Contains(nameof(LopTap.SucChua)))
        {
            dgvLop.Columns[nameof(LopTap.SucChua)]!.HeaderText = "Sức chứa";
            dgvLop.Columns[nameof(LopTap.SucChua)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLop.Columns[nameof(LopTap.SucChua)]!.Width = 90;
        }
        if (dgvLop.Columns.Contains(nameof(LopTap.GioBatDau)))
        {
            dgvLop.Columns[nameof(LopTap.GioBatDau)]!.HeaderText = "Giờ bắt đầu";
            dgvLop.Columns[nameof(LopTap.GioBatDau)]!.DefaultCellStyle.Format = @"hh\:mm";
            dgvLop.Columns[nameof(LopTap.GioBatDau)]!.Width = 100;
        }
        if (dgvLop.Columns.Contains(nameof(LopTap.GioKetThuc)))
        {
            dgvLop.Columns[nameof(LopTap.GioKetThuc)]!.HeaderText = "Giờ kết thúc";
            dgvLop.Columns[nameof(LopTap.GioKetThuc)]!.DefaultCellStyle.Format = @"hh\:mm";
            dgvLop.Columns[nameof(LopTap.GioKetThuc)]!.Width = 100;
        }
        if (dgvLop.Columns.Contains(nameof(LopTap.TrangThai)))
        {
            dgvLop.Columns[nameof(LopTap.TrangThai)]!.HeaderText = "Trạng thái";
            dgvLop.Columns[nameof(LopTap.TrangThai)]!.Width = 110;
        }
        if (dgvLop.Columns.Contains(nameof(LopTap.MaLoaiLop)))
        {
            dgvLop.Columns[nameof(LopTap.MaLoaiLop)]!.Visible = false;
        }
        if (dgvLop.Columns.Contains(nameof(LopTap.MaHLV)))
        {
            dgvLop.Columns[nameof(LopTap.MaHLV)]!.Visible = false;
        }
    }

    private async void btnThem_Click(object? sender, EventArgs e)
    {
        try
        {
            if (cboHLV.SelectedItem is not HuanLuyenVien h || cboLoai.SelectedValue is not int loai)
            {
                throw new InvalidOperationException("Hãy chọn loại lớp và HLV.");
            }

            if (string.IsNullOrWhiteSpace(txtTenLop.Text))
            {
                throw new InvalidOperationException("Vui lòng nhập tên lớp tập.");
            }

            await _bll.ThemAsync(new LopTap
            {
                TenLop = txtTenLop.Text.Trim(),
                MaLoaiLop = loai,
                MaHLV = h.MaHLV,
                SucChua = (int)nudSucChua.Value,
                GioBatDau = dtpBatDau.Value.TimeOfDay,
                GioKetThuc = dtpKetThuc.Value.TimeOfDay,
                TrangThai = cboTrangThai.Text
            });

            await TaiDanhSachAsync();
            txtTenLop.Clear();
            MessageBox.Show("Đã tạo lớp tập mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể lưu lớp tập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async void btnLamMoi_Click(object? sender, EventArgs e)
    {
        txtTimKiem.Clear();
        if (cboLocTrangThai.Items.Count > 0) cboLocTrangThai.SelectedIndex = 0;
        await TaiDanhSachAsync();
    }
}
