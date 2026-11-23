using System.Drawing;
using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmGoiTap : Form
{
    private readonly GoiTapBLL _bll = new();
    private readonly GroupBox _groupPhanQuyen = new();
    private readonly CheckedListBox _clbDichVu = new();
    private readonly CheckedListBox _clbPhong = new();
    private readonly Label _lblDichVu = new();
    private readonly Label _lblPhong = new();
    private int? _maGoi;
    private bool _dangThemMoi;
    private bool _dangNapQuyen;
    private bool _khoaThongTin = true;
    private List<PhongTapModel> _phongHoatDong = new();

    public FrmGoiTap()
    {
        InitializeComponent();
        TaoKhungPhanQuyen();
        txtTenGoi.PlaceholderText = "Nhập tên gói tập...";
        txtMoTa.PlaceholderText = "Nhập mô tả gói tập (không bắt buộc)...";
        cboTrangThai.SelectedIndexChanged += (s, e) => CapNhatTrangThaiNut();
        _clbDichVu.ItemCheck += ClbDichVu_ItemCheck;
    }

    private void TaoKhungPhanQuyen()
    {
        _groupPhanQuyen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _groupPhanQuyen.BackColor = Color.White;
        _groupPhanQuyen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        _groupPhanQuyen.Location = new Point(24, 260);
        _groupPhanQuyen.Name = "groupPhanQuyen";
        _groupPhanQuyen.Padding = new Padding(16, 8, 16, 10);
        _groupPhanQuyen.Size = new Size(1152, 145);
        _groupPhanQuyen.TabStop = false;
        _groupPhanQuyen.Text = "Dịch vụ và phòng được phép sử dụng";

        _lblDichVu.AutoSize = true;
        _lblDichVu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblDichVu.Location = new Point(18, 28);
        _lblDichVu.Text = "Dịch vụ:";

        _lblPhong.AutoSize = true;
        _lblPhong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblPhong.Location = new Point(410, 28);
        _lblPhong.Text = "Phòng tập (chỉ áp dụng khi chọn dịch vụ Phòng tập):";

        _clbDichVu.CheckOnClick = true;
        _clbDichVu.Font = new Font("Segoe UI", 9F);
        _clbDichVu.FormattingEnabled = true;
        _clbDichVu.IntegralHeight = false;
        _clbDichVu.Location = new Point(18, 50);
        _clbDichVu.Name = "clbDichVu";
        _clbDichVu.Size = new Size(375, 80);

        _clbPhong.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _clbPhong.CheckOnClick = true;
        _clbPhong.Font = new Font("Segoe UI", 9F);
        _clbPhong.FormattingEnabled = true;
        _clbPhong.IntegralHeight = false;
        _clbPhong.Location = new Point(410, 50);
        _clbPhong.Name = "clbPhong";
        _clbPhong.Size = new Size(720, 80);

        _groupPhanQuyen.Controls.Add(_clbDichVu);
        _groupPhanQuyen.Controls.Add(_clbPhong);
        _groupPhanQuyen.Controls.Add(_lblDichVu);
        _groupPhanQuyen.Controls.Add(_lblPhong);
        Controls.Add(_groupPhanQuyen);
        _groupPhanQuyen.BringToFront();

        // Chừa chỗ cho khu vực phân quyền mới.
        flowThaoTac.Location = new Point(24, 415);
        panelTimKiem.Location = new Point(24, 468);
        dgvGoiTap.Location = new Point(24, 536);
        dgvGoiTap.Height = 163;
    }

    private async void FrmGoiTap_Load(object? s, EventArgs e)
    {
        cboTrangThai.SelectedIndex = -1;
        NapDanhMucDichVu();
        try
        {
            _phongHoatDong = await _bll.LayPhongHoatDongAsync();
            _clbPhong.Items.Clear();
            foreach (var phong in _phongHoatDong) _clbPhong.Items.Add(phong, false);
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không tải được danh sách phòng", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        KhoaOThongTin(true);
        CapNhatTrangThaiNut();
        await TaiAsync();
        DatTrangThaiNghi();
        BeginInvoke(new Action(DatTrangThaiNghi));
    }

    private void NapDanhMucDichVu()
    {
        _clbDichVu.Items.Clear();
        foreach (var dichVu in GoiTapBLL.DanhMucDichVu) _clbDichVu.Items.Add(dichVu, false);
    }

    private void KhoaOThongTin(bool khoa)
    {
        _khoaThongTin = khoa;
        txtTenGoi.ReadOnly = khoa;
        nudThoiHan.Enabled = !khoa;
        nudGia.Enabled = !khoa;
        txtMoTa.ReadOnly = khoa;
        cboTrangThai.Enabled = !khoa;
        _clbDichVu.Enabled = !khoa;
        CapNhatQuyenPhongUi();

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
        BoChonQuyen();
        UiGridHelper.BoChon(dgvGoiTap);
        KhoaOThongTin(true);
        CapNhatTrangThaiNut();
    }

    private void BoChonQuyen()
    {
        _dangNapQuyen = true;
        for (int i = 0; i < _clbDichVu.Items.Count; i++) _clbDichVu.SetItemChecked(i, false);
        for (int i = 0; i < _clbPhong.Items.Count; i++) _clbPhong.SetItemChecked(i, false);
        _dangNapQuyen = false;
        CapNhatQuyenPhongUi();
    }

    private async Task NapQuyenAsync(int maGoi)
    {
        _dangNapQuyen = true;
        try
        {
            var quyen = await _bll.LayQuyenAsync(maGoi);
            for (int i = 0; i < _clbDichVu.Items.Count; i++)
            {
                if (_clbDichVu.Items[i] is GoiTapDichVuOption dichVu)
                    _clbDichVu.SetItemChecked(i, quyen.MaDichVu.Contains(dichVu.MaDichVu));
            }
            for (int i = 0; i < _clbPhong.Items.Count; i++)
            {
                if (_clbPhong.Items[i] is PhongTapModel phong)
                    _clbPhong.SetItemChecked(i, quyen.MaPhong.Contains(phong.MaPhong));
            }
        }
        finally
        {
            _dangNapQuyen = false;
            CapNhatQuyenPhongUi();
        }
    }

    private void ClbDichVu_ItemCheck(object? sender, ItemCheckEventArgs e)
    {
        if (_dangNapQuyen) return;
        BeginInvoke(new Action(CapNhatQuyenPhongUi));
    }

    private void CapNhatQuyenPhongUi()
    {
        bool coQuyenPhong = _clbDichVu.CheckedItems
            .OfType<GoiTapDichVuOption>()
            .Any(x => x.MaDichVu == "PHONG_TAP");
        if (!coQuyenPhong)
        {
            _dangNapQuyen = true;
            for (int i = 0; i < _clbPhong.Items.Count; i++) _clbPhong.SetItemChecked(i, false);
            _dangNapQuyen = false;
        }
        _clbPhong.Enabled = !_khoaThongTin && coQuyenPhong && !_dangNapQuyen;
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

    private async void dgvGoiTap_SelectionChanged(object? s, EventArgs e)
    {
        if (dgvGoiTap.CurrentRow?.DataBoundItem is not GoiTap g)
        {
            _maGoi = null;
            _dangThemMoi = false;
            BoChonQuyen();
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
        KhoaOThongTin(false);
        await NapQuyenAsync(g.MaGoi);
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
        BoChonQuyen();
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
            var dichVu = _clbDichVu.CheckedItems.OfType<GoiTapDichVuOption>().ToArray();
            var maPhong = _clbPhong.CheckedItems.OfType<PhongTapModel>().Select(x => x.MaPhong).ToArray();
            await _bll.LuuAsync(new GoiTap
            {
                MaGoi = _maGoi ?? 0,
                TenGoi = txtTenGoi.Text.Trim(),
                ThoiHanNgay = (int)nudThoiHan.Value,
                Gia = nudGia.Value,
                MoTa = txtMoTa.Text.Trim(),
                TrangThai = cboTrangThai.Text
            }, dichVu, maPhong);

            await TaiAsync(txtTimKiem.Text);
            DatTrangThaiNghi();
            MessageBox.Show("Đã lưu gói tập và phân quyền thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
