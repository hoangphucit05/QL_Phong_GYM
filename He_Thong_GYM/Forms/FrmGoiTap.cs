using System.Drawing;
using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmGoiTap : Form
{
    private readonly GoiTapBLL _bll = new();
    private int? _maGoi;
    private bool _dangThemMoi;

    public FrmGoiTap()
    {
        InitializeComponent();
        txtTenGoi.PlaceholderText = "Nhập tên gói tập...";
        txtMoTa.PlaceholderText = "Nhập mô tả gói tập (không bắt buộc)...";
        cboTrangThai.SelectedIndexChanged += (s, e) => CapNhatTrangThaiNut();
    }

    private async void FrmGoiTap_Load(object? s, EventArgs e)
    {
        cboTrangThai.SelectedIndex = -1;
        KhoaOThongTin(true);
        CapNhatTrangThaiNut();
        await TaiAsync();
        DatTrangThaiNghi();
        BeginInvoke(new Action(DatTrangThaiNghi));
    }

    private void KhoaOThongTin(bool khoa)
    {
        txtTenGoi.ReadOnly = khoa;
        nudThoiHan.Enabled = !khoa;
        nudGia.Enabled = !khoa;
        txtMoTa.ReadOnly = khoa;
        cboTrangThai.Enabled = !khoa;

        Color mauNen = khoa ? Color.FromArgb(248, 250, 252) : Color.White;
        txtTenGoi.BackColor = mauNen;
        txtMoTa.BackColor = mauNen;
    }

    private void CapNhatTrangThaiNut()
    {
        bool coChon = _maGoi.HasValue;
        bool coQuyenLuu = coChon || _dangThemMoi;
        bool dangApDung = !string.Equals(cboTrangThai.Text, "Ngừng áp dụng", StringComparison.OrdinalIgnoreCase);

        UiButtonHelper.SetTrangThai(btnThem, !_dangThemMoi, UiButtonHelper.MauThemMoi);
        UiButtonHelper.SetTrangThai(btnLuu, coQuyenLuu, UiButtonHelper.MauLuu);
        UiButtonHelper.SetTrangThai(btnNgungApDung, coChon && !_dangThemMoi && dangApDung, UiButtonHelper.MauXoa);
    }

    private void DatTrangThaiNghi()
    {
        _maGoi = null;
        _dangThemMoi = false;
        txtTenGoi.Clear();
        txtMoTa.Clear();
        nudThoiHan.Value = 30;
        nudGia.Value = 0;
        cboTrangThai.SelectedIndex = -1;
        UiGridHelper.BoChon(dgvGoiTap);
        KhoaOThongTin(true);
        CapNhatTrangThaiNut();
    }

    private async Task TaiAsync(string? k = null)
    {
        try
        {
            UiGridHelper.DatNguon(dgvGoiTap, await _bll.LayDanhSachAsync(k));
            lblTrangThai.Text = string.IsNullOrWhiteSpace(k) ? "Hiển thị toàn bộ gói tập." : $"Đã lọc: {k}";
        }
        catch (Exception ex)
        {
            lblTrangThai.Text = "Không tải được dữ liệu. Kiểm tra SQL Server.";
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void dgvGoiTap_SelectionChanged(object? s, EventArgs e)
    {
        if (dgvGoiTap.CurrentRow?.DataBoundItem is not GoiTap g)
        {
            _maGoi = null;
            _dangThemMoi = false;
            KhoaOThongTin(true);
            CapNhatTrangThaiNut();
            return;
        }

        _maGoi = g.MaGoi;
        _dangThemMoi = false;
        txtTenGoi.Text = g.TenGoi;
        nudThoiHan.Value = g.ThoiHanNgay;
        nudGia.Value = g.Gia;
        txtMoTa.Text = g.MoTa;
        cboTrangThai.SelectedItem = g.TrangThai;

        KhoaOThongTin(false); // Cho phép chỉnh sửa khi đã chọn gói
        CapNhatTrangThaiNut();
    }

    private void btnThem_Click(object? s, EventArgs e)
    {
        _maGoi = null;
        _dangThemMoi = true;
        txtTenGoi.Clear();
        txtMoTa.Clear();
        nudThoiHan.Value = 30;
        nudGia.Value = 0;
        cboTrangThai.SelectedIndex = 0;
        UiGridHelper.BoChon(dgvGoiTap);
        KhoaOThongTin(false);
        CapNhatTrangThaiNut();
        txtTenGoi.Focus();
    }

    private async void btnLuu_Click(object? s, EventArgs e)
    {
        if (!_maGoi.HasValue && !_dangThemMoi)
        {
            MessageBox.Show("Vui lòng chọn một gói tập hoặc bấm 'Thêm mới' trước khi lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            await _bll.LuuAsync(new GoiTap
            {
                MaGoi = _maGoi ?? 0,
                TenGoi = txtTenGoi.Text.Trim(),
                ThoiHanNgay = (int)nudThoiHan.Value,
                Gia = nudGia.Value,
                MoTa = txtMoTa.Text.Trim(),
                TrangThai = cboTrangThai.Text
            });

            await TaiAsync(txtTimKiem.Text);
            DatTrangThaiNghi();
            MessageBox.Show("Đã lưu gói tập thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    private async void btnNgungApDung_Click(object? s, EventArgs e)
    {
        if (_maGoi is null)
        {
            MessageBox.Show("Vui lòng chọn gói tập cần ngừng áp dụng từ danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        if (MessageBox.Show("Ngừng áp dụng gói này? Dữ liệu lịch sử sẽ được bảo lưu.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        await _bll.NgungApDungAsync(_maGoi.Value);
        await TaiAsync(txtTimKiem.Text);
        DatTrangThaiNghi();
    }
}
