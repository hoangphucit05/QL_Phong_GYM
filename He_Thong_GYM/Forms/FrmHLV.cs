using System.Drawing;
using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmHLV : Form
{
    private readonly HuanLuyenVienBLL _hlvBLL = new();
    private readonly NhanVienBLL _nhanVienBLL = new();
    private int? _maHLV;
    private bool _dangThemMoi;

    public FrmHLV()
    {
        InitializeComponent();
        txtChuyenMon.PlaceholderText = "Nhập chuyên môn của HLV...";
    }

    private async void FrmHLV_Load(object? s, EventArgs e)
    {
        try
        {
            cboNhanVien.DataSource = await _nhanVienBLL.LayNhanVienDangLamAsync();
            cboNhanVien.DisplayMember = nameof(NhanVien.HienThi);
            cboNhanVien.ValueMember = nameof(NhanVien.MaNV);
            KhoaOThongTin(true);
            CapNhatTrangThaiNut();
            await TaiAsync();
            DatTrangThaiNghi();
            BeginInvoke(new Action(DatTrangThaiNghi));
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void KhoaOThongTin(bool khoa)
    {
        cboNhanVien.Enabled = !khoa;
        txtChuyenMon.ReadOnly = khoa;
        nudKinhNghiem.Enabled = !khoa;
        nudPhiThue.Enabled = !khoa;

        Color mauNen = khoa ? Color.FromArgb(248, 250, 252) : Color.White;
        txtChuyenMon.BackColor = mauNen;
    }

    private void CapNhatTrangThaiNut()
    {
        bool coChon = _maHLV.HasValue;
        bool coQuyenLuu = coChon || _dangThemMoi;

        UiButtonHelper.SetTrangThai(btnThem, !_dangThemMoi, UiButtonHelper.MauThemMoi);
        UiButtonHelper.SetTrangThai(btnLuu, coQuyenLuu, UiButtonHelper.MauLuu);
    }

    private void DatTrangThaiNghi()
    {
        _maHLV = null;
        _dangThemMoi = false;
        txtChuyenMon.Clear();
        nudKinhNghiem.Value = 0;
        nudPhiThue.Value = 0;
        if (cboNhanVien.Items.Count > 0) cboNhanVien.SelectedIndex = -1;
        UiGridHelper.BoChon(dgvHLV);
        KhoaOThongTin(true);
        CapNhatTrangThaiNut();
    }

    private async Task TaiAsync(string? k = null)
    {
        try
        {
            UiGridHelper.DatNguon(dgvHLV, await _hlvBLL.LayDanhSachAsync(k));
            lblTrangThai.Text = string.IsNullOrWhiteSpace(k) ? "Hiển thị toàn bộ huấn luyện viên." : $"Đã lọc: {k}";
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void dgvHLV_SelectionChanged(object? s, EventArgs e)
    {
        if (dgvHLV.CurrentRow?.DataBoundItem is not HuanLuyenVien h)
        {
            _maHLV = null;
            _dangThemMoi = false;
            KhoaOThongTin(true);
            CapNhatTrangThaiNut();
            return;
        }

        _maHLV = h.MaHLV;
        _dangThemMoi = false;
        cboNhanVien.SelectedValue = h.MaNV;
        txtChuyenMon.Text = h.ChuyenMon;
        nudKinhNghiem.Value = h.KinhNghiem ?? 0;
        nudPhiThue.Value = h.PhiThue ?? 0;

        KhoaOThongTin(false);
        CapNhatTrangThaiNut();
    }

    private void btnThem_Click(object? s, EventArgs e)
    {
        _maHLV = null;
        _dangThemMoi = true;
        txtChuyenMon.Clear();
        nudKinhNghiem.Value = 0;
        nudPhiThue.Value = 0;
        if (cboNhanVien.Items.Count > 0) cboNhanVien.SelectedIndex = 0;
        UiGridHelper.BoChon(dgvHLV);
        KhoaOThongTin(false);
        CapNhatTrangThaiNut();
        txtChuyenMon.Focus();
    }

    private async void btnLuu_Click(object? s, EventArgs e)
    {
        if (!_maHLV.HasValue && !_dangThemMoi)
        {
            MessageBox.Show("Vui lòng chọn HLV hoặc bấm 'Thêm mới' trước khi lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            await _hlvBLL.LuuAsync(new HuanLuyenVien
            {
                MaHLV = _maHLV ?? 0,
                MaNV = cboNhanVien.SelectedValue is int maNV ? maNV : 0,
                ChuyenMon = string.IsNullOrWhiteSpace(txtChuyenMon.Text) ? null : txtChuyenMon.Text.Trim(),
                KinhNghiem = (int)nudKinhNghiem.Value,
                PhiThue = nudPhiThue.Value
            });

            await TaiAsync(txtTimKiem.Text);
            DatTrangThaiNghi();
            MessageBox.Show("Đã lưu hồ sơ HLV thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
}
