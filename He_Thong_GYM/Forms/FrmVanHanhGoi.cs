using System.Drawing;
using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmVanHanhGoi : Form
{
    private bool _coPhienCheckInMo;
    private bool _dangTaiPhong;
    private bool _dangChonPhien;
    private bool _dangLocThanhVien;
    private int? _maPhongPhienMo;
    private TabControl _tabDanhSach = null!;
    private TabPage _tabGoi = null!;
    private TabPage _tabDangTap = null!;
    private DataGridView _dgvPhienMo = null!;
    private Button _btnLamMoiPhien = null!;
    private readonly DangKyBLL _dangKyBLL = new();
    private readonly VanHanhBLL _vanHanhBLL = new();
    private List<ThanhVien> _thanhVienGoc = new();

    public FrmVanHanhGoi()
    {
        InitializeComponent();
        TaoTabDanhSach();
        txtTimThanhVien.PlaceholderText = "Mã TV, tên hội viên hoặc SĐT...";
        txtTimThanhVien.TextChanged += txtTimThanhVien_TextChanged;
        txtTimThanhVien.KeyDown += txtTimThanhVien_KeyDown;
        btnXoaTimThanhVien.Click += btnXoaTimThanhVien_Click;
        cboThanhVien.SelectedIndexChanged += cboThanhVien_SelectedIndexChanged;
        cboPhong.SelectedIndexChanged += (s, e) => CapNhatTrangThaiNut();
        dgvGoi.SelectionChanged += dgvGoi_SelectionChanged;
        _dgvPhienMo.SelectionChanged += dgvPhienMo_SelectionChanged;
        _btnLamMoiPhien.Click += async (s, e) => await TaiPhienMoAsync();
        btnCheckIn.Click += btnCheckIn_Click;
        btnCheckOut.Click += btnCheckOut_Click;
        btnChuyenPhong.Click += btnChuyenPhong_Click;
        btnGiaHan.Click += btnGiaHan_Click;
        btnBaoLuu.Click += btnBaoLuu_Click;
        btnMoLaiBaoLuu.Click += btnMoLaiBaoLuu_Click;
    }

    private async void FrmVanHanhGoi_Load(object? sender, EventArgs e)
    {
        try
        {
            _thanhVienGoc = await _dangKyBLL.LayThanhVienAsync();
            LocThanhVien();
            cboPhong.DataSource = null;
            await TaiGoiAsync();
            await TaiPhienMoAsync();
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private void CapNhatTrangThaiNut()
    {
        bool coThanhVien = cboThanhVien.SelectedItem is ThanhVien;
        var goiChon = dgvGoi.CurrentRow?.DataBoundItem as DangKyGoiView;
        bool coGoiChon = goiChon is not null;
        bool goiDangTamDung = string.Equals(goiChon?.TrangThai, "T\u1ea1m d\u1eebng", StringComparison.OrdinalIgnoreCase);
        bool goiConHan = string.Equals(goiChon?.TrangThai, "C\u00f2n h\u1ea1n", StringComparison.OrdinalIgnoreCase);
        bool goiHetHan = string.Equals(goiChon?.TrangThai, "H\u1ebft h\u1ea1n", StringComparison.OrdinalIgnoreCase);

        bool goiHopLeDeCheckIn = coGoiChon && string.Equals(goiChon!.TrangThai, "C\u00f2n h\u1ea1n", StringComparison.OrdinalIgnoreCase);
        bool coPhong = cboPhong.SelectedItem is PhongTapModel;
        bool coTheChuyenPhong = coThanhVien && _coPhienCheckInMo && coPhong &&
            (!_maPhongPhienMo.HasValue || cboPhong.SelectedValue is int maPhong && maPhong != _maPhongPhienMo.Value);
        UiButtonHelper.SetTrangThai(btnCheckIn, coThanhVien && goiHopLeDeCheckIn && coPhong && !_coPhienCheckInMo, Color.FromArgb(5, 150, 105));
        UiButtonHelper.SetTrangThai(btnCheckOut, coThanhVien && _coPhienCheckInMo, Color.FromArgb(30, 64, 175));
        UiButtonHelper.SetTrangThai(btnChuyenPhong, coTheChuyenPhong, Color.FromArgb(13, 148, 136));
        UiButtonHelper.SetTrangThai(btnGiaHan, coGoiChon && (goiConHan || goiHetHan), Color.FromArgb(16, 185, 129));
        UiButtonHelper.SetTrangThai(btnBaoLuu, coGoiChon && goiConHan, Color.FromArgb(124, 58, 237));
        UiButtonHelper.SetTrangThai(btnMoLaiBaoLuu, goiDangTamDung, Color.FromArgb(14, 116, 144));
    }

    private async void dgvGoi_SelectionChanged(object? sender, EventArgs e)
    {
        try
        {
            await TaiPhongTheoGoiAsync();
            CapNhatTrangThaiNut();
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private void txtTimThanhVien_TextChanged(object? sender, EventArgs e)
    {
        LocThanhVien();
    }

    private void LocThanhVien() => LocThanhVien(null);

    private void LocThanhVien(int? maThanhVienCanGiuLai)
    {
        if (_dangLocThanhVien) return;

        var tuKhoa = txtTimThanhVien.Text.Trim();
        var danhSach = string.IsNullOrWhiteSpace(tuKhoa)
            ? _thanhVienGoc
            : _thanhVienGoc.Where(x =>
                x.MaThanhVien.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                x.HoTen.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                x.SDT.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                (x.Email?.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ?? false))
                .ToList();

        _dangLocThanhVien = true;
        try
        {
            cboThanhVien.DataSource = null;
            cboThanhVien.DataSource = danhSach.ToList();
            cboThanhVien.DisplayMember = nameof(ThanhVien.HienThi);
            cboThanhVien.ValueMember = nameof(ThanhVien.MaTV);
            cboThanhVien.SelectedIndex = -1;
            if (maThanhVienCanGiuLai.HasValue && danhSach.Any(x => x.MaTV == maThanhVienCanGiuLai.Value))
                cboThanhVien.SelectedValue = maThanhVienCanGiuLai.Value;
        }
        finally { _dangLocThanhVien = false; }

        UiButtonHelper.SetTrangThai(btnXoaTimThanhVien, !string.IsNullOrWhiteSpace(tuKhoa), Color.FromArgb(100, 116, 139));
        if (!string.IsNullOrWhiteSpace(tuKhoa))
        {
            lblTrangThai.Text = danhSach.Count == 0
                ? $"Không tìm thấy hội viên phù hợp với: {tuKhoa}"
                : $"Tìm thấy {danhSach.Count} hội viên. Hãy chọn hội viên để xem gói.";

            if (txtTimThanhVien.Focused && danhSach.Count > 0 && IsHandleCreated)
            {
                BeginInvoke(new Action(() =>
                {
                    if (!IsDisposed && txtTimThanhVien.Focused && cboThanhVien.Items.Count > 0)
                        cboThanhVien.DroppedDown = true;
                }));
            }
        }
    }

    private void txtTimThanhVien_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter || cboThanhVien.Items.Count != 1) return;

        cboThanhVien.SelectedIndex = 0;
        cboThanhVien.DroppedDown = false;
        e.Handled = true;
        e.SuppressKeyPress = true;
    }

    private void btnXoaTimThanhVien_Click(object? sender, EventArgs e)
    {
        var maThanhVienDangChon = (cboThanhVien.SelectedItem as ThanhVien)?.MaTV;
        _dangLocThanhVien = true;
        try { txtTimThanhVien.Clear(); }
        finally { _dangLocThanhVien = false; }

        LocThanhVien(maThanhVienDangChon);
        txtTimThanhVien.Focus();
    }

    private async Task TaiPhongTheoGoiAsync()
    {
        if (_dangTaiPhong) return;
        var goi = dgvGoi.CurrentRow?.DataBoundItem as DangKyGoiView;
        if (goi is null)
        {
            cboPhong.DataSource = null;
            cboPhong.SelectedIndex = -1;
            return;
        }

        _dangTaiPhong = true;
        try
        {
            var phong = await _vanHanhBLL.LayPhongDuocPhepAsync(goi.MaDKG);
            cboPhong.DataSource = phong;
            cboPhong.DisplayMember = nameof(PhongTapModel.HienThi);
            cboPhong.ValueMember = nameof(PhongTapModel.MaPhong);
            cboPhong.SelectedIndex = -1;
            lblTrangThai.Text = phong.Count == 0
                ? "Gói này chưa được cấp quyền phòng tập. Hãy cấu hình quyền trước khi Check-in."
                : $"Gói {goi.TenGoi}: được sử dụng {phong.Count} phòng theo quyền gói.";
        }
        finally { _dangTaiPhong = false; }
    }

    private async Task TaiGoiAsync()
    {
        if (cboThanhVien.SelectedItem is not ThanhVien thanhVien)
        {
            _coPhienCheckInMo = false;
            _maPhongPhienMo = null;
            dgvGoi.DataSource = null;
            cboPhong.DataSource = null;
            CapNhatTrangThaiNut();
            return;
        }

        UiGridHelper.DatNguon(dgvGoi, await _vanHanhBLL.LayGoiThanhVienAsync(thanhVien.MaTV));
        DinhDangLuoiGoi();
        UiGridHelper.BoChon(dgvGoi);
        cboPhong.DataSource = null;
        _coPhienCheckInMo = await _vanHanhBLL.CoPhienCheckInMoAsync(thanhVien.MaTV);
        _maPhongPhienMo = await _vanHanhBLL.LayMaPhongPhienMoAsync(thanhVien.MaTV);
        CapNhatTrangThaiNut();
    }

    private void TaoTabDanhSach()
    {
        var bounds = dgvGoi.Bounds;
        var anchor = dgvGoi.Anchor;
        Controls.Remove(dgvGoi);

        _tabDanhSach = new TabControl
        {
            Name = "tabDanhSachVanHanh",
            Location = bounds.Location,
            Size = bounds.Size,
            Anchor = anchor,
            DrawMode = TabDrawMode.OwnerDrawFixed,
            SizeMode = TabSizeMode.Fixed,
            ItemSize = new Size(178, 23),
            Padding = new Point(12, 5),
            BackColor = Color.FromArgb(226, 232, 240)
        };
        _tabGoi = new TabPage("Gói tập");
        _tabDangTap = new TabPage("Đang tập / Chờ Check-out");
        _tabGoi.BackColor = Color.White;
        _tabDangTap.BackColor = Color.FromArgb(248, 250, 252);
        _tabDanhSach.DrawItem += VeTabDanhSach;
        _tabDanhSach.SelectedIndexChanged += (s, e) => _tabDanhSach.Invalidate();

        dgvGoi.Dock = DockStyle.Fill;
        _tabGoi.Controls.Add(dgvGoi);

        _btnLamMoiPhien = new Button
        {
            Text = "Làm mới danh sách đang tập",
            AutoSize = true,
            Height = 30,
            Location = new Point(8, 6),
            BackColor = Color.FromArgb(15, 118, 110),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = { BorderSize = 0 },
            Cursor = Cursors.Hand
        };
        _btnLamMoiPhien.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Color.FromArgb(15, 118, 110), 0.16f);
        _btnLamMoiPhien.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(Color.FromArgb(15, 118, 110), 0.16f);
        var thanhCongCu = new Panel { Dock = DockStyle.Top, Height = 42, BackColor = Color.FromArgb(241, 245, 249) };
        thanhCongCu.Controls.Add(_btnLamMoiPhien);

        _dgvPhienMo = new DataGridView
        {
            Name = "dgvPhienMo",
            Dock = DockStyle.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None,
            ColumnHeadersHeight = 36,
            MultiSelect = false,
            ReadOnly = true,
            RowHeadersVisible = false,
            RowTemplate = { Height = 32 },
            SelectionMode = DataGridViewSelectionMode.FullRowSelect
        };
        _tabDangTap.Controls.Add(_dgvPhienMo);
        _tabDangTap.Controls.Add(thanhCongCu);

        _tabDanhSach.TabPages.Add(_tabGoi);
        _tabDanhSach.TabPages.Add(_tabDangTap);
        Controls.Add(_tabDanhSach);
        _tabDanhSach.BringToFront();
    }

    private void VeTabDanhSach(object? sender, DrawItemEventArgs e)
    {
        if (sender is not TabControl tab || e.Index < 0 || e.Index >= tab.TabPages.Count) return;

        var selected = e.Index == tab.SelectedIndex;
        var mauChinh = e.Index == 1 ? Color.FromArgb(13, 148, 136) : Color.FromArgb(37, 99, 235);
        var mauNen = selected ? mauChinh : Color.FromArgb(226, 232, 240);
        var mauChu = selected ? Color.White : Color.FromArgb(30, 41, 59);
        var rect = e.Bounds;
        rect.Inflate(-1, -1);

        using var brush = new SolidBrush(mauNen);
        using var pen = new Pen(selected ? mauChinh : Color.FromArgb(203, 213, 225));
        using var font = new Font(Font, FontStyle.Bold);
        e.Graphics.FillRectangle(brush, rect);
        e.Graphics.DrawRectangle(pen, rect);
        TextRenderer.DrawText(
            e.Graphics,
            tab.TabPages[e.Index].Text,
            font,
            rect,
            mauChu,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
    }

    private async Task TaiPhienMoAsync()
    {
        UiGridHelper.DatNguon(_dgvPhienMo, await _vanHanhBLL.LayPhienCheckInMoAsync());
        DinhDangLuoiPhienMo();
        UiGridHelper.BoChon(_dgvPhienMo);
    }

    private void DinhDangLuoiPhienMo()
    {
        if (_dgvPhienMo.Columns.Contains(nameof(PhienCheckInView.MaCheckIn))) _dgvPhienMo.Columns[nameof(PhienCheckInView.MaCheckIn)]!.Visible = false;
        if (_dgvPhienMo.Columns.Contains(nameof(PhienCheckInView.MaTV))) _dgvPhienMo.Columns[nameof(PhienCheckInView.MaTV)]!.Visible = false;
        if (_dgvPhienMo.Columns.Contains(nameof(PhienCheckInView.MaDKG))) _dgvPhienMo.Columns[nameof(PhienCheckInView.MaDKG)]!.Visible = false;
        if (_dgvPhienMo.Columns.Contains(nameof(PhienCheckInView.MaPhong))) _dgvPhienMo.Columns[nameof(PhienCheckInView.MaPhong)]!.Visible = false;
        if (_dgvPhienMo.Columns.Contains(nameof(PhienCheckInView.MaPhienCheckIn))) _dgvPhienMo.Columns[nameof(PhienCheckInView.MaPhienCheckIn)]!.HeaderText = "Mã phiên";
        if (_dgvPhienMo.Columns.Contains(nameof(PhienCheckInView.MaThanhVien))) _dgvPhienMo.Columns[nameof(PhienCheckInView.MaThanhVien)]!.HeaderText = "Mã thành viên";
        if (_dgvPhienMo.Columns.Contains(nameof(PhienCheckInView.HoTen))) _dgvPhienMo.Columns[nameof(PhienCheckInView.HoTen)]!.HeaderText = "Họ tên";
        if (_dgvPhienMo.Columns.Contains(nameof(PhienCheckInView.TenGoi))) _dgvPhienMo.Columns[nameof(PhienCheckInView.TenGoi)]!.HeaderText = "Gói đã dùng";
        if (_dgvPhienMo.Columns.Contains(nameof(PhienCheckInView.PhongHienThi))) _dgvPhienMo.Columns[nameof(PhienCheckInView.PhongHienThi)]!.HeaderText = "Phòng tập";
        if (_dgvPhienMo.Columns.Contains(nameof(PhienCheckInView.ThoiGianVao)))
        {
            _dgvPhienMo.Columns[nameof(PhienCheckInView.ThoiGianVao)]!.HeaderText = "Check-in lúc";
            _dgvPhienMo.Columns[nameof(PhienCheckInView.ThoiGianVao)]!.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }
        if (_dgvPhienMo.Columns.Contains(nameof(PhienCheckInView.TenPhong))) _dgvPhienMo.Columns[nameof(PhienCheckInView.TenPhong)]!.Visible = false;
        if (_dgvPhienMo.Columns.Contains(nameof(PhienCheckInView.MaPhongTap))) _dgvPhienMo.Columns[nameof(PhienCheckInView.MaPhongTap)]!.Visible = false;
    }

    private async void dgvPhienMo_SelectionChanged(object? sender, EventArgs e)
    {
        if (_dgvPhienMo.CurrentRow?.DataBoundItem is not PhienCheckInView phien) return;
        _maPhongPhienMo = phien.MaPhong;
        _dangChonPhien = true;
        try
        {
            if (!string.IsNullOrWhiteSpace(txtTimThanhVien.Text))
                txtTimThanhVien.Clear();
            cboThanhVien.SelectedValue = phien.MaTV;
            await TaiGoiAsync();
            foreach (DataGridViewRow row in dgvGoi.Rows)
            {
                if (row.DataBoundItem is DangKyGoiView goi && goi.MaDKG == phien.MaDKG)
                {
                    row.Selected = true;
                    // Cột đầu tiên là MaDKG và đã ẩn; CurrentCell không được trỏ vào cột ẩn.
                    var cotHienThi = dgvGoi.Columns
                        .Cast<DataGridViewColumn>()
                        .FirstOrDefault(c => c.Visible && c.DataPropertyName != nameof(DangKyGoiView.MaDKG));
                    if (cotHienThi is not null)
                        dgvGoi.CurrentCell = row.Cells[cotHienThi.Index];
                    break;
                }
            }
            await TaiPhongTheoGoiAsync();
            if (phien.MaPhong.HasValue) cboPhong.SelectedValue = phien.MaPhong.Value;
            CapNhatTrangThaiNut();
        }
        catch (Exception ex) { HienThiLoi(ex); }
        finally { _dangChonPhien = false; }
    }

    private void DinhDangLuoiGoi()
    {
        if (dgvGoi.Columns.Contains(nameof(DangKyGoiView.MaDKG))) dgvGoi.Columns[nameof(DangKyGoiView.MaDKG)]!.Visible = false;
        if (dgvGoi.Columns.Contains(nameof(DangKyGoiView.MaGoi))) dgvGoi.Columns[nameof(DangKyGoiView.MaGoi)]!.Visible = false;
        if (dgvGoi.Columns.Contains(nameof(DangKyGoiView.HienThi))) dgvGoi.Columns[nameof(DangKyGoiView.HienThi)]!.Visible = false;
        if (dgvGoi.Columns.Contains(nameof(DangKyGoiView.MaDangKyGoi)))
        {
            dgvGoi.Columns[nameof(DangKyGoiView.MaDangKyGoi)]!.HeaderText = "Mã đăng ký";
            dgvGoi.Columns[nameof(DangKyGoiView.MaDangKyGoi)]!.Width = 155;
        }
        if (dgvGoi.Columns.Contains(nameof(DangKyGoiView.QuyenDichVu)))
        {
            dgvGoi.Columns[nameof(DangKyGoiView.QuyenDichVu)]!.HeaderText = "Dịch vụ được cấp quyền";
            dgvGoi.Columns[nameof(DangKyGoiView.QuyenDichVu)]!.FillWeight = 180;
        }
    }

    private async void cboThanhVien_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_dangChonPhien || _dangLocThanhVien) return;
        try { await TaiGoiAsync(); } catch (Exception ex) { HienThiLoi(ex); }
    }

    private async void btnCheckIn_Click(object? sender, EventArgs e)
    {
        try
        {
            if (cboThanhVien.SelectedItem is not ThanhVien tv) throw new InvalidOperationException("Hãy chọn thành viên.");
            if (dgvGoi.CurrentRow?.DataBoundItem is not DangKyGoiView goi) throw new InvalidOperationException("Hãy chọn gói muốn sử dụng cho Check-in.");
            var maPhong = cboPhong.SelectedValue is int value ? value : (int?)null;
            var maCheckIn = await _vanHanhBLL.CheckInAsync(tv.MaTV, goi.MaDKG, maPhong);
            lblTrangThai.Text = $"Check-in thành công, mã phiên: {maCheckIn}.";
            _coPhienCheckInMo = true;
            CapNhatTrangThaiNut();
            await TaiPhienMoAsync();
            _tabDanhSach.SelectedTab = _tabDangTap;
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private async void btnCheckOut_Click(object? sender, EventArgs e)
    {
        try
        {
            if (cboThanhVien.SelectedItem is not ThanhVien tv) throw new InvalidOperationException("Hãy chọn thành viên.");
            await _vanHanhBLL.CheckOutAsync(tv.MaTV);
            lblTrangThai.Text = "Check-out thành công.";
            _coPhienCheckInMo = false;
            CapNhatTrangThaiNut();
            await TaiPhienMoAsync();
            _tabDanhSach.SelectedTab = _tabDangTap;
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private async void btnChuyenPhong_Click(object? sender, EventArgs e)
    {
        try
        {
            if (cboThanhVien.SelectedItem is not ThanhVien tv) throw new InvalidOperationException("Hãy chọn thành viên đang tập.");
            if (cboPhong.SelectedValue is not int maPhongMoi) throw new InvalidOperationException("Hãy chọn phòng mới.");
            if (_maPhongPhienMo == maPhongMoi) throw new InvalidOperationException("Phòng mới phải khác phòng hiện tại.");

            await _vanHanhBLL.ChuyenPhongAsync(tv.MaTV, maPhongMoi);
            _maPhongPhienMo = maPhongMoi;
            lblTrangThai.Text = "Đã chuyển phòng trong cùng phiên tập, không tính thêm lượt.";
            CapNhatTrangThaiNut();
            await TaiPhienMoAsync();
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private async void btnGiaHan_Click(object? sender, EventArgs e)
    {
        try
        {
            if (dgvGoi.CurrentRow?.DataBoundItem is not DangKyGoiView goi) throw new InvalidOperationException("Hãy chọn một gói đã đăng ký.");
            var maHD = await _vanHanhBLL.GiaHanAsync(goi.MaDKG, (int)nudSoNgay.Value, SessionManager.NguoiDungHienTai?.MaNV);
            await TaiGoiAsync();
            lblTrangThai.Text = $"Đã gia hạn gói tập. Hóa đơn #{maHD} đã được tạo và đang chờ thanh toán.";
            MessageBox.Show($"Đã gia hạn gói tập thành công!\nHóa đơn #{maHD} đã được tạo và chuyển sang trạng thái Chưa thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private async void btnBaoLuu_Click(object? sender, EventArgs e)
    {
        try
        {
            if (dgvGoi.CurrentRow?.DataBoundItem is not DangKyGoiView goi) throw new InvalidOperationException("Hãy chọn một gói đã đăng ký.");
            await _vanHanhBLL.BaoLuuAsync(goi.MaDKG, dtpTuNgay.Value, dtpDenNgay.Value, txtLyDo.Text, SessionManager.NguoiDungHienTai?.MaNV);
            await TaiGoiAsync();
            lblTrangThai.Text = "Đã bảo lưu gói tập.";
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private async void btnMoLaiBaoLuu_Click(object? sender, EventArgs e)
    {
        try
        {
            if (dgvGoi.CurrentRow?.DataBoundItem is not DangKyGoiView goi ||
                !string.Equals(goi.TrangThai, "T\u1ea1m d\u1eebng", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("H\u00e3y ch\u1ecdn g\u00f3i \u0111ang b\u1ea3o l\u01b0u.");

            await _vanHanhBLL.MoLaiBaoLuuAsync(goi.MaDKG, SessionManager.NguoiDungHienTai?.MaNV);
            await TaiGoiAsync();
            lblTrangThai.Text = "\u0110\u00e3 m\u1edf l\u1ea1i g\u00f3i b\u1ea3o l\u01b0u.";
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private void HienThiLoi(Exception ex)
    {
        var message = UiErrorMessage.Get(ex);
        lblTrangThai.Text = message;
        MessageBox.Show(message, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

}
