using He_Thong_GYM.BLL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmPhongTap : Form
{
    private readonly TaiSanBLL _bll = new();
    private List<PhongTapModel> _danhSachGoc = new();
    private bool _dangKhoiTao = true;
    private PhongTapModel? _phongDangChon;

    public FrmPhongTap()
    {
        InitializeComponent();
    }

    private async void FrmPhongTap_Load(object? sender, EventArgs e)
    {
        _dangKhoiTao = true;
        UiButtonHelper.SetTrangThai(btnSua, false, UiButtonHelper.MauChinhSua);
        if (cboLoai.Items.Count > 0) cboLoai.SelectedIndex = 0;
        if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
        await TaiDanhSachAsync();
        NapDanhMucLoaiPhong();
        _phongDangChon = null;
        UiGridHelper.BoChon(dgv);
        UiButtonHelper.SetTrangThai(btnSua, false, UiButtonHelper.MauChinhSua);
        BeginInvoke(new Action(() =>
        {
            _phongDangChon = null;
            UiGridHelper.BoChon(dgv);
            UiButtonHelper.SetTrangThai(btnSua, false, UiButtonHelper.MauChinhSua);
        }));
        _dangKhoiTao = false;
    }

    private void NapDanhMucLoaiPhong()
    {
        var loaiPhong = _danhSachGoc
            .Select(x => x.LoaiPhong)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(x => x)
            .ToList();

        cboLoai.Items.Clear();
        cboLoai.Items.Add("-- Tất cả loại --");
        foreach (var loai in loaiPhong) cboLoai.Items.Add(loai);
        cboLoai.SelectedIndex = 0;
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
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi tải dữ liệu phòng tập", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                (!string.IsNullOrEmpty(p.MaPhongTap) && p.MaPhongTap.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
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
        UiGridHelper.DatNguon(dgv, list);
        DinhDangLuoi();

        lblThongKe.Text = $"Hiển thị: {list.Count} / {_danhSachGoc.Count} phòng tập";
    }

    private void DinhDangLuoi()
    {
        if (dgv.Columns.Count == 0) return;

        if (dgv.Columns.Contains(nameof(PhongTapModel.MaPhong)))
        {
            dgv.Columns[nameof(PhongTapModel.MaPhong)]!.Visible = false;
        }
        if (dgv.Columns.Contains(nameof(PhongTapModel.MaPhongTap)))
        {
            dgv.Columns[nameof(PhongTapModel.MaPhongTap)]!.HeaderText = "Mã phòng";
            dgv.Columns[nameof(PhongTapModel.MaPhongTap)]!.Width = 100;
        }
        if (dgv.Columns.Contains(nameof(PhongTapModel.HienThi)))
        {
            dgv.Columns[nameof(PhongTapModel.HienThi)]!.Visible = false;
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

    private void dgv_SelectionChanged(object? sender, EventArgs e)
    {
        if (_dangKhoiTao) return;
        _phongDangChon = dgv.CurrentRow?.DataBoundItem as PhongTapModel;
        UiButtonHelper.SetTrangThai(btnSua, _phongDangChon != null, UiButtonHelper.MauChinhSua);
    }

    private async void btnSua_Click(object? sender, EventArgs e)
    {
        if (_phongDangChon is null)
        {
            MessageBox.Show("Vui lòng chọn phòng tập cần cập nhật từ danh sách.", "Chưa chọn phòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dlg = new Form
        {
            Text = $"Cập nhật phòng tập: {_phongDangChon.TenPhong} ({_phongDangChon.MaPhongTap})",
            Width = 400,
            Height = 220,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            MaximizeBox = false,
            MinimizeBox = false
        };

        var lblSc = new Label { Text = "Sức chứa tối đa:", Left = 20, Top = 25, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var nudSc = new NumericUpDown { Left = 140, Top = 22, Width = 220, Minimum = 1, Maximum = 500, Value = _phongDangChon.SucChua };

        var lblTT = new Label { Text = "Trạng thái:", Left = 20, Top = 65, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var cboChonTT = new ComboBox { Left = 140, Top = 62, Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
        cboChonTT.Items.AddRange(new object[] { "Hoạt động", "Bảo trì", "Tạm ngưng" });
        cboChonTT.SelectedItem = _phongDangChon.TrangThai ?? "Hoạt động";

        var btnLuu = new Button { Text = "Lưu thay đổi", Left = 150, Top = 115, Width = 110, Height = 34, DialogResult = DialogResult.OK, BackColor = Color.FromArgb(16, 185, 129), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        var btnHuy = new Button { Text = "Hủy", Left = 270, Top = 115, Width = 80, Height = 34, DialogResult = DialogResult.Cancel, FlatStyle = FlatStyle.Flat };

        dlg.Controls.AddRange(new Control[] { lblSc, nudSc, lblTT, cboChonTT, btnLuu, btnHuy });
        dlg.AcceptButton = btnLuu;
        dlg.CancelButton = btnHuy;

        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var trangThaiMoi = cboChonTT.SelectedItem?.ToString() ?? "Hoạt động";
                await _bll.CapNhatPhongTapAsync(_phongDangChon.MaPhong, (int)nudSc.Value, trangThaiMoi);
                await TaiDanhSachAsync();
                MessageBox.Show("Cập nhật thông tin phòng tập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(UiErrorMessage.Get(ex), "Không thể cập nhật phòng tập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
