using System.Drawing;
using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmNhanVien : Form
{
    private readonly NhanVienBLL _bll = new();
    private int? _maNV;
    private bool _dangThemMoi;

    public FrmNhanVien()
    {
        InitializeComponent();
        txtHoTen.PlaceholderText = "Nhập họ và tên nhân viên...";
        txtSDT.PlaceholderText = "Nhập số điện thoại...";
        txtEmail.PlaceholderText = "Nhập email (không bắt buộc)...";
        txtDiaChi.PlaceholderText = "Nhập địa chỉ (không bắt buộc)...";
        txtChucVu.PlaceholderText = "Nhập chức vụ...";
        cboTrangThai.SelectedIndexChanged += (s, e) => CapNhatTrangThaiNut();
    }

    private async void FrmNhanVien_Load(object? s, EventArgs e)
    {
        cboGioiTinh.SelectedIndex = -1;
        cboTrangThai.SelectedIndex = -1;
        KhoaOThongTin(true);
        CapNhatTrangThaiNut();
        await TaiAsync();
        DatTrangThaiNghi();
        BeginInvoke(new Action(DatTrangThaiNghi));
    }

    private void KhoaOThongTin(bool khoa)
    {
        txtHoTen.ReadOnly = khoa;
        txtSDT.ReadOnly = khoa;
        txtEmail.ReadOnly = khoa;
        txtDiaChi.ReadOnly = khoa;
        txtChucVu.ReadOnly = khoa;
        nudLuong.Enabled = !khoa;
        dtpNgaySinh.Enabled = !khoa;
        cboGioiTinh.Enabled = !khoa;
        cboTrangThai.Enabled = !khoa;

        Color mauNen = khoa ? Color.FromArgb(248, 250, 252) : Color.White;
        txtHoTen.BackColor = mauNen;
        txtSDT.BackColor = mauNen;
        txtEmail.BackColor = mauNen;
        txtDiaChi.BackColor = mauNen;
        txtChucVu.BackColor = mauNen;
    }

    private void CapNhatTrangThaiNut()
    {
        bool coChon = _maNV.HasValue;
        bool coQuyenLuu = coChon || _dangThemMoi;
        bool dangLamViec = !string.Equals(cboTrangThai.Text, "Nghỉ việc", StringComparison.OrdinalIgnoreCase);

        UiButtonHelper.SetTrangThai(btnThem, !_dangThemMoi, UiButtonHelper.MauThemMoi);
        UiButtonHelper.SetTrangThai(btnLuu, coQuyenLuu, UiButtonHelper.MauLuu);
        UiButtonHelper.SetTrangThai(btnNghiViec, coChon && !_dangThemMoi && dangLamViec, UiButtonHelper.MauXoa);
    }

    private void DatTrangThaiNghi()
    {
        _maNV = null;
        _dangThemMoi = false;
        txtHoTen.Clear();
        txtSDT.Clear();
        txtEmail.Clear();
        txtDiaChi.Clear();
        txtChucVu.Clear();
        nudLuong.Value = 0;
        dtpNgaySinh.Value = DateTime.Today.AddYears(-22);
        cboGioiTinh.SelectedIndex = -1;
        cboTrangThai.SelectedIndex = -1;
        UiGridHelper.BoChon(dgvNhanVien);
        KhoaOThongTin(true);
        CapNhatTrangThaiNut();
    }

    private async Task TaiAsync(string? k = null)
    {
        try
        {
            UiGridHelper.DatNguon(dgvNhanVien, await _bll.LayDanhSachAsync(k));
            DinhDangLuoi();
            lblTrangThaiTimKiem.Text = string.IsNullOrWhiteSpace(k) ? "Hiển thị toàn bộ nhân viên." : $"Đã lọc theo mã, tên hoặc số điện thoại: {k}";
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void DinhDangLuoi()
    {
        if (dgvNhanVien.Columns.Contains(nameof(NhanVien.MaNV))) dgvNhanVien.Columns[nameof(NhanVien.MaNV)]!.Visible = false;
        if (dgvNhanVien.Columns.Contains(nameof(NhanVien.HienThi))) dgvNhanVien.Columns[nameof(NhanVien.HienThi)]!.Visible = false;
        if (dgvNhanVien.Columns.Contains(nameof(NhanVien.MaNhanVien)))
        {
            dgvNhanVien.Columns[nameof(NhanVien.MaNhanVien)]!.HeaderText = "Mã nhân viên";
            dgvNhanVien.Columns[nameof(NhanVien.MaNhanVien)]!.Width = 105;
        }
    }

    private void dgvNhanVien_SelectionChanged(object? s, EventArgs e)
    {
        if (dgvNhanVien.CurrentRow?.DataBoundItem is not NhanVien n)
        {
            _maNV = null;
            _dangThemMoi = false;
            KhoaOThongTin(true);
            CapNhatTrangThaiNut();
            return;
        }

        _maNV = n.MaNV;
        _dangThemMoi = false;
        txtHoTen.Text = n.HoTen;
        dtpNgaySinh.Value = n.NgaySinh;
        cboGioiTinh.SelectedItem = n.GioiTinh ?? "Khác";
        txtSDT.Text = n.SDT;
        txtEmail.Text = n.Email;
        txtDiaChi.Text = n.DiaChi;
        txtChucVu.Text = n.ChucVu;
        nudLuong.Value = n.Luong ?? 0;
        cboTrangThai.SelectedItem = n.TrangThai;

        KhoaOThongTin(false);
        CapNhatTrangThaiNut();
    }

    private void btnThem_Click(object? s, EventArgs e)
    {
        _maNV = null;
        _dangThemMoi = true;
        txtHoTen.Clear();
        txtSDT.Clear();
        txtEmail.Clear();
        txtDiaChi.Clear();
        txtChucVu.Clear();
        nudLuong.Value = 0;
        dtpNgaySinh.Value = DateTime.Today.AddYears(-22);
        cboGioiTinh.SelectedIndex = 0;
        cboTrangThai.SelectedIndex = 0;
        UiGridHelper.BoChon(dgvNhanVien);
        KhoaOThongTin(false);
        CapNhatTrangThaiNut();
        txtHoTen.Focus();
    }

    private async void btnLuu_Click(object? s, EventArgs e)
    {
        if (!_maNV.HasValue && !_dangThemMoi)
        {
            MessageBox.Show("Vui lòng chọn nhân viên hoặc bấm 'Thêm mới' trước khi lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            await _bll.LuuAsync(new NhanVien
            {
                MaNV = _maNV ?? 0,
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = cboGioiTinh.Text,
                SDT = txtSDT.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim(),
                ChucVu = txtChucVu.Text.Trim(),
                Luong = nudLuong.Value,
                TrangThai = cboTrangThai.Text
            });

            await TaiAsync(txtTimKiem.Text);
            DatTrangThaiNghi();
            MessageBox.Show("Đã lưu nhân viên thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không thể lưu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async void btnTimKiem_Click(object? s, EventArgs e)
    {
        await TaiAsync(txtTimKiem.Text);
        DatTrangThaiNghi();
    }

    private async void btnNghiViec_Click(object? s, EventArgs e)
    {
        if (_maNV is null)
        {
            MessageBox.Show("Vui lòng chọn nhân viên cần chuyển trạng thái nghỉ việc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        if (MessageBox.Show("Chuyển nhân viên sang trạng thái nghỉ việc và khóa tài khoản?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        try
        {
            await _bll.NghiViecAsync(_maNV.Value);
            await TaiAsync(txtTimKiem.Text);
            DatTrangThaiNghi();
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
