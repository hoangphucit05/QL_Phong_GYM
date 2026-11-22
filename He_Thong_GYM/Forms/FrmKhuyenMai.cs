using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmKhuyenMai : Form
{
    private readonly KhuyenMaiBLL _bll = new();
    private List<KhuyenMai> _danhSachGoc = new();
    private bool _dangKhoiTao = true;
    private KhuyenMai? _khuyenMaiDangChon;

    public FrmKhuyenMai()
    {
        InitializeComponent();
    }

    private async void FrmKhuyenMai_Load(object? sender, EventArgs e)
    {
        _dangKhoiTao = true;
        if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
        UiButtonHelper.SetTrangThai(btnSua, false, UiButtonHelper.MauChinhSua);
        await TaiDanhSachAsync();
        _khuyenMaiDangChon = null;
        UiGridHelper.BoChon(dgvKhuyenMai);
        UiButtonHelper.SetTrangThai(btnSua, false, UiButtonHelper.MauChinhSua);
        BeginInvoke(new Action(() =>
        {
            _khuyenMaiDangChon = null;
            UiGridHelper.BoChon(dgvKhuyenMai);
            UiButtonHelper.SetTrangThai(btnSua, false, UiButtonHelper.MauChinhSua);
        }));
        _dangKhoiTao = false;
    }

    private async Task TaiDanhSachAsync()
    {
        try
        {
            _danhSachGoc = await _bll.LayTatCaAsync();
            ApDungLoc();
        }
        catch (Exception ex)
        {
            lblThongKe.Text = "Lỗi tải dữ liệu";
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi tải dữ liệu khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        UiGridHelper.BoChon(dgvKhuyenMai);

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

    private void dgvKhuyenMai_SelectionChanged(object? sender, EventArgs e)
    {
        if (_dangKhoiTao) return;
        _khuyenMaiDangChon = dgvKhuyenMai.CurrentRow?.DataBoundItem as KhuyenMai;
        UiButtonHelper.SetTrangThai(btnSua, _khuyenMaiDangChon != null, UiButtonHelper.MauChinhSua);
    }

    private async void btnThem_Click(object? sender, EventArgs e)
    {
        using var dlg = new Form
        {
            Text = "Thêm chương trình khuyến mãi mới",
            Width = 450,
            Height = 330,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            MaximizeBox = false,
            MinimizeBox = false
        };

        var lblTen = new Label { Text = "Tên chương trình:", Left = 20, Top = 20, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var txtTenKM = new TextBox { Left = 150, Top = 18, Width = 260, PlaceholderText = "Nhập tên chương trình..." };

        var lblCode = new Label { Text = "Mã Code (tùy chọn):", Left = 20, Top = 55, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var txtCode = new TextBox { Left = 150, Top = 53, Width = 260, PlaceholderText = "Bỏ trống để tự sinh..." };

        var lblGiam = new Label { Text = "Mức giảm (%):", Left = 20, Top = 90, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var nudPhanTram = new NumericUpDown { Left = 150, Top = 88, Width = 120, Minimum = 1, Maximum = 100, Value = 10 };

        var lblBD = new Label { Text = "Ngày bắt đầu:", Left = 20, Top = 125, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var dtpBD = new DateTimePicker { Left = 150, Top = 123, Width = 160, Format = DateTimePickerFormat.Short, Value = DateTime.Today };

        var lblKT = new Label { Text = "Ngày kết thúc:", Left = 20, Top = 160, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var dtpKT = new DateTimePicker { Left = 150, Top = 158, Width = 160, Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddMonths(1) };

        var lblTT = new Label { Text = "Trạng thái:", Left = 20, Top = 195, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var cboTT = new ComboBox { Left = 150, Top = 193, Width = 160, DropDownStyle = ComboBoxStyle.DropDownList };
        cboTT.Items.AddRange(new object[] { "Hoạt động", "Tạm dừng" });
        cboTT.SelectedIndex = 0;

        var btnLuu = new Button { Text = "Tạo mới", Left = 150, Top = 240, Width = 110, Height = 34, DialogResult = DialogResult.OK, BackColor = Color.FromArgb(37, 99, 235), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        var btnHuy = new Button { Text = "Hủy", Left = 270, Top = 240, Width = 80, Height = 34, DialogResult = DialogResult.Cancel, FlatStyle = FlatStyle.Flat };

        dlg.Controls.AddRange(new Control[] { lblTen, txtTenKM, lblCode, txtCode, lblGiam, nudPhanTram, lblBD, dtpBD, lblKT, dtpKT, lblTT, cboTT, btnLuu, btnHuy });
        dlg.AcceptButton = btnLuu;
        dlg.CancelButton = btnHuy;

        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenKM.Text))
                {
                    throw new InvalidOperationException("Vui lòng nhập tên chương trình khuyến mãi.");
                }

                await _bll.ThemAsync(new KhuyenMai
                {
                    TenKM = txtTenKM.Text.Trim(),
                    MaCode = string.IsNullOrWhiteSpace(txtCode.Text) ? string.Empty : txtCode.Text.Trim(),
                    PhanTramGiam = nudPhanTram.Value,
                    NgayBatDau = dtpBD.Value.Date,
                    NgayKetThuc = dtpKT.Value.Date,
                    TrangThai = cboTT.SelectedItem?.ToString() ?? "Hoạt động"
                });

                await TaiDanhSachAsync();
                MessageBox.Show("Thêm chương trình khuyến mãi mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(UiErrorMessage.Get(ex), "Không thể thêm khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    private async void btnSua_Click(object? sender, EventArgs e)
    {
        if (_khuyenMaiDangChon is null)
        {
            MessageBox.Show("Vui lòng chọn chương trình khuyến mãi cần cập nhật.", "Chưa chọn khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dlg = new Form
        {
            Text = $"Cập nhật khuyến mãi: {_khuyenMaiDangChon.TenKM}",
            Width = 450,
            Height = 330,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            MaximizeBox = false,
            MinimizeBox = false
        };

        var lblTen = new Label { Text = "Tên chương trình:", Left = 20, Top = 20, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var txtTenKM = new TextBox { Left = 150, Top = 18, Width = 260, Text = _khuyenMaiDangChon.TenKM, PlaceholderText = "Nhập tên chương trình..." };

        var lblCode = new Label { Text = "Mã Code:", Left = 20, Top = 55, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var txtCode = new TextBox { Left = 150, Top = 53, Width = 260, Text = _khuyenMaiDangChon.MaCode };

        var lblGiam = new Label { Text = "Mức giảm (%):", Left = 20, Top = 90, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var nudPhanTram = new NumericUpDown { Left = 150, Top = 88, Width = 120, Minimum = 1, Maximum = 100, Value = Math.Clamp(_khuyenMaiDangChon.PhanTramGiam, 1, 100) };

        var lblBD = new Label { Text = "Ngày bắt đầu:", Left = 20, Top = 125, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var dtpBD = new DateTimePicker { Left = 150, Top = 123, Width = 160, Format = DateTimePickerFormat.Short, Value = _khuyenMaiDangChon.NgayBatDau ?? DateTime.Today };

        var lblKT = new Label { Text = "Ngày kết thúc:", Left = 20, Top = 160, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var dtpKT = new DateTimePicker { Left = 150, Top = 158, Width = 160, Format = DateTimePickerFormat.Short, Value = _khuyenMaiDangChon.NgayKetThuc ?? DateTime.Today.AddMonths(1) };

        var lblTT = new Label { Text = "Trạng thái:", Left = 20, Top = 195, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var cboTT = new ComboBox { Left = 150, Top = 193, Width = 160, DropDownStyle = ComboBoxStyle.DropDownList };
        cboTT.Items.AddRange(new object[] { "Hoạt động", "Tạm dừng", "Hết hạn" });
        cboTT.SelectedItem = _khuyenMaiDangChon.TrangThai ?? "Hoạt động";

        var btnLuu = new Button { Text = "Lưu thay đổi", Left = 150, Top = 240, Width = 110, Height = 34, DialogResult = DialogResult.OK, BackColor = Color.FromArgb(16, 185, 129), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        var btnHuy = new Button { Text = "Hủy", Left = 270, Top = 240, Width = 80, Height = 34, DialogResult = DialogResult.Cancel, FlatStyle = FlatStyle.Flat };

        dlg.Controls.AddRange(new Control[] { lblTen, txtTenKM, lblCode, txtCode, lblGiam, nudPhanTram, lblBD, dtpBD, lblKT, dtpKT, lblTT, cboTT, btnLuu, btnHuy });
        dlg.AcceptButton = btnLuu;
        dlg.CancelButton = btnHuy;

        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenKM.Text))
                {
                    throw new InvalidOperationException("Vui lòng nhập tên chương trình khuyến mãi.");
                }

                await _bll.CapNhatAsync(new KhuyenMai
                {
                    MaKM = _khuyenMaiDangChon.MaKM,
                    TenKM = txtTenKM.Text.Trim(),
                    MaCode = string.IsNullOrWhiteSpace(txtCode.Text) ? string.Empty : txtCode.Text.Trim(),
                    PhanTramGiam = nudPhanTram.Value,
                    NgayBatDau = dtpBD.Value.Date,
                    NgayKetThuc = dtpKT.Value.Date,
                    TrangThai = cboTT.SelectedItem?.ToString() ?? "Hoạt động"
                });

                await TaiDanhSachAsync();
                MessageBox.Show("Cập nhật chương trình khuyến mãi thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(UiErrorMessage.Get(ex), "Không thể cập nhật khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
