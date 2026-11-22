using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmSanPham : Form
{
    private readonly SanPhamBLL _bll = new();
    private List<SanPham> _danhSachGoc = new();
    private bool _dangKhoiTao = true;
    private int? _maSPDangChon;

    public FrmSanPham()
    {
        InitializeComponent();
    }

    private async void FrmSanPham_Load(object? sender, EventArgs e)
    {
        _dangKhoiTao = true;
        if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
        if (cboTrangThaiSP.Items.Count > 0) cboTrangThaiSP.SelectedIndex = 0;
        await TaiDanhSachAsync();
        DatTrangThaiTaoMoi();
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
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi tải sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                (!string.IsNullOrEmpty(s.MaSanPham) && s.MaSanPham.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
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
            dgvSanPham.Columns[nameof(SanPham.MaSP)]!.Visible = false;
        }
        if (dgvSanPham.Columns.Contains(nameof(SanPham.MaSanPham)))
        {
            dgvSanPham.Columns[nameof(SanPham.MaSanPham)]!.HeaderText = "Mã sản phẩm";
            dgvSanPham.Columns[nameof(SanPham.MaSanPham)]!.Width = 100;
        }
        if (dgvSanPham.Columns.Contains(nameof(SanPham.HienThi)))
        {
            dgvSanPham.Columns[nameof(SanPham.HienThi)]!.Visible = false;
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
        if (dgvSanPham.Columns.Contains(nameof(SanPham.GiaNhap)))
        {
            dgvSanPham.Columns[nameof(SanPham.GiaNhap)]!.HeaderText = "Giá nhập (VNĐ)";
            dgvSanPham.Columns[nameof(SanPham.GiaNhap)]!.DefaultCellStyle.Format = "N0";
            dgvSanPham.Columns[nameof(SanPham.GiaNhap)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSanPham.Columns[nameof(SanPham.GiaNhap)]!.Width = 120;
        }
        if (dgvSanPham.Columns.Contains(nameof(SanPham.TonToiThieu)))
        {
            dgvSanPham.Columns[nameof(SanPham.TonToiThieu)]!.Visible = false;
        }
    }

    private void dgvSanPham_SelectionChanged(object? sender, EventArgs e)
    {
        if (_dangKhoiTao) return;
        if (dgvSanPham.CurrentRow?.DataBoundItem is not SanPham sp)
        {
            _maSPDangChon = null;
            UiButtonHelper.SetTrangThai(btnSua, false, UiButtonHelper.MauChinhSua);
            UiButtonHelper.SetTrangThai(btnNhapKho, false, UiButtonHelper.MauChucNangPhu);
            return;
        }

        _maSPDangChon = sp.MaSP;
        UiButtonHelper.SetTrangThai(btnSua, true, UiButtonHelper.MauChinhSua);
        UiButtonHelper.SetTrangThai(btnNhapKho, true, UiButtonHelper.MauChucNangPhu);
        txtTenSP.Text = sp.TenSP;
        nudDonGia.Value = Math.Clamp(sp.DonGia, nudDonGia.Minimum, nudDonGia.Maximum);
        nudGiaNhap.Value = Math.Clamp(sp.GiaNhap, nudGiaNhap.Minimum, nudGiaNhap.Maximum);
        cboTrangThaiSP.SelectedItem = sp.TrangThai;
    }

    private void DatTrangThaiTaoMoi()
    {
        _maSPDangChon = null;
        txtTenSP.Clear();
        nudDonGia.Value = 0;
        nudGiaNhap.Value = 0;
        if (cboTrangThaiSP.Items.Count > 0) cboTrangThaiSP.SelectedIndex = 0;
        UiButtonHelper.SetTrangThai(btnSua, false, UiButtonHelper.MauChinhSua);
        UiButtonHelper.SetTrangThai(btnNhapKho, false, UiButtonHelper.MauChucNangPhu);
        dgvSanPham.ClearSelection();
        txtTenSP.Focus();
    }

    private async void btnThem_Click(object? sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                throw new InvalidOperationException("Vui lòng nhập tên sản phẩm.");
            }
            if (nudDonGia.Value <= 0)
            {
                throw new InvalidOperationException("Đơn giá bán phải lớn hơn 0.");
            }

            await _bll.ThemAsync(new SanPham
            {
                TenSP = txtTenSP.Text.Trim(),
                DonGia = nudDonGia.Value,
                GiaNhap = nudGiaNhap.Value,
                TrangThai = cboTrangThaiSP.SelectedItem?.ToString() ?? "Còn bán"
            });

            await TaiDanhSachAsync();
            DatTrangThaiTaoMoi();
            MessageBox.Show("Đã thêm sản phẩm mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không thể lưu sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async void btnSua_Click(object? sender, EventArgs e)
    {
        if (_maSPDangChon is null)
        {
            MessageBox.Show("Vui lòng chọn sản phẩm cần cập nhật từ danh sách.", "Chưa chọn sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                throw new InvalidOperationException("Vui lòng nhập tên sản phẩm.");
            }
            if (nudDonGia.Value <= 0)
            {
                throw new InvalidOperationException("Đơn giá bán phải lớn hơn 0.");
            }

            await _bll.CapNhatAsync(new SanPham
            {
                MaSP = _maSPDangChon.Value,
                TenSP = txtTenSP.Text.Trim(),
                DonGia = nudDonGia.Value,
                GiaNhap = nudGiaNhap.Value,
                TrangThai = cboTrangThaiSP.SelectedItem?.ToString() ?? "Còn bán"
            });

            await TaiDanhSachAsync();
            MessageBox.Show("Cập nhật thông tin sản phẩm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không thể cập nhật sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async void btnNhapKho_Click(object? sender, EventArgs e)
    {
        if (_maSPDangChon is null || dgvSanPham.CurrentRow?.DataBoundItem is not SanPham sp)
        {
            MessageBox.Show("Vui lòng chọn sản phẩm cần nhập kho.", "Chưa chọn sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dlg = new Form
        {
            Text = $"Nhập kho: {sp.TenSP}",
            Width = 400,
            Height = 220,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            MaximizeBox = false,
            MinimizeBox = false
        };

        var lblSl = new Label { Text = "Số lượng nhập thêm:", Left = 20, Top = 20, AutoSize = true };
        var nudSl = new NumericUpDown { Left = 160, Top = 18, Width = 180, Minimum = 1, Maximum = 100000, Value = 10 };
        var lblGia = new Label { Text = "Đơn giá nhập (VNĐ):", Left = 20, Top = 60, AutoSize = true };
        var nudGia = new NumericUpDown { Left = 160, Top = 58, Width = 180, Minimum = 0, Maximum = 100000000, Increment = 5000, ThousandsSeparator = true, Value = sp.GiaNhap };
        var btnOk = new Button { Text = "Nhập kho", Left = 160, Top = 110, Width = 100, Height = 35, DialogResult = DialogResult.OK, BackColor = Color.FromArgb(37, 99, 235), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        var btnCancel = new Button { Text = "Hủy", Left = 270, Top = 110, Width = 70, Height = 35, DialogResult = DialogResult.Cancel, FlatStyle = FlatStyle.Flat };

        dlg.Controls.AddRange(new Control[] { lblSl, nudSl, lblGia, nudGia, btnOk, btnCancel });
        dlg.AcceptButton = btnOk;
        dlg.CancelButton = btnCancel;

        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var maNV = SessionManager.NguoiDungHienTai?.MaNV ?? 1;
                var maPNK = await _bll.NhapKhoAsync(sp.MaSP, (int)nudSl.Value, nudGia.Value, maNV);
                await TaiDanhSachAsync();
                MessageBox.Show($"Nhập kho thành công! Đã tạo phiếu nhập #{maPNK} và tăng tồn kho thêm {nudSl.Value}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(UiErrorMessage.Get(ex), "Không thể nhập kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
        DatTrangThaiTaoMoi();
    }
}
