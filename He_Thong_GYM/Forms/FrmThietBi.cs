using He_Thong_GYM.BLL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmThietBi : Form
{
    private readonly TaiSanBLL _bll = new();
    private List<ThietBi> _danhSachGoc = new();
    private bool _dangKhoiTao = true;
    private ThietBi? _thietBiDangChon;

    public FrmThietBi()
    {
        InitializeComponent();
    }

    private async void FrmThietBi_Load(object? sender, EventArgs e)
    {
        await TaiDuLieuAsync();
    }

    private async Task TaiDuLieuAsync()
    {
        try
        {
            _dangKhoiTao = true;
            lblThongKe.Text = "Đang tải dữ liệu thiết bị và phòng tập...";

            // 1. Tải danh sách thiết bị từ CSDL
            _danhSachGoc = await _bll.LayThietBiAsync();

            // 2. Tải danh sách phòng tập và nạp vào cboPhong
            cboPhong.Items.Clear();
            cboPhong.Items.Add("-- Tất cả phòng --");
            try
            {
                var phongs = await _bll.LayPhongAsync();
                foreach (var p in phongs)
                {
                    if (!string.IsNullOrWhiteSpace(p.TenPhong))
                    {
                        cboPhong.Items.Add(p.TenPhong);
                    }
                }
            }
            catch
            {
                // Nếu không tải được phòng, dự phòng lấy danh sách phòng từ thiết bị hiện có
                var phongsTuTB = _danhSachGoc
                    .Select(x => x.Phong)
                    .Where(p => !string.IsNullOrWhiteSpace(p))
                    .Distinct()
                    .OrderBy(p => p);
                foreach (var p in phongsTuTB)
                {
                    cboPhong.Items.Add(p!);
                }
            }
            cboPhong.Items.Add("(Chưa phân phòng)");
            cboPhong.SelectedIndex = 0;

            // 3. Nạp danh sách loại thiết bị vào cboLoai
            cboLoai.Items.Clear();
            cboLoai.Items.Add("-- Tất cả loại --");
            var danhSachLoai = _danhSachGoc
                .Select(x => x.LoaiThietBi)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Distinct()
                .OrderBy(l => l)
                .ToList();

            if (danhSachLoai.Count == 0)
            {
                danhSachLoai.AddRange(new[] { "Máy chạy bộ", "Xe đạp tập", "Giàn tạ đa năng", "Ghế tập ngực", "Tạ đơn", "Máy kéo xô" });
            }

            foreach (var l in danhSachLoai)
            {
                cboLoai.Items.Add(l);
            }
            cboLoai.SelectedIndex = 0;

            // 4. Nạp trạng thái vào cboTinhTrang
            cboTinhTrang.Items.Clear();
            cboTinhTrang.Items.Add("-- Tất cả tình trạng --");
            cboTinhTrang.Items.Add("Hoạt động");
            cboTinhTrang.Items.Add("Bảo trì");
            cboTinhTrang.Items.Add("Hư hỏng");
            cboTinhTrang.Items.Add("Thanh lý");
            cboTinhTrang.SelectedIndex = 0;

            _dangKhoiTao = false;
            ApDungLoc();
            DinhDangLuoi();
        }
        catch (Exception ex)
        {
            lblThongKe.Text = "Không thể tải dữ liệu. Kiểm tra kết nối SQL Server.";
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void DinhDangLuoi()
    {
        if (dgv.Columns.Count == 0) return;

        if (dgv.Columns.Contains(nameof(ThietBi.MaTB)))
        {
            dgv.Columns[nameof(ThietBi.MaTB)]!.Visible = false;
        }
        if (dgv.Columns.Contains(nameof(ThietBi.MaThietBi)))
        {
            dgv.Columns[nameof(ThietBi.MaThietBi)]!.HeaderText = "Mã thiết bị";
            dgv.Columns[nameof(ThietBi.MaThietBi)]!.Width = 105;
        }
        if (dgv.Columns.Contains(nameof(ThietBi.HienThi)))
        {
            dgv.Columns[nameof(ThietBi.HienThi)]!.Visible = false;
        }
        if (dgv.Columns.Contains(nameof(ThietBi.TenTB)))
        {
            dgv.Columns[nameof(ThietBi.TenTB)]!.HeaderText = "Tên thiết bị";
            dgv.Columns[nameof(ThietBi.TenTB)]!.FillWeight = 160;
        }
        if (dgv.Columns.Contains(nameof(ThietBi.LoaiThietBi)))
        {
            dgv.Columns[nameof(ThietBi.LoaiThietBi)]!.HeaderText = "Loại thiết bị";
            dgv.Columns[nameof(ThietBi.LoaiThietBi)]!.FillWeight = 120;
        }
        if (dgv.Columns.Contains(nameof(ThietBi.Phong)))
        {
            dgv.Columns[nameof(ThietBi.Phong)]!.HeaderText = "Phòng tập";
            dgv.Columns[nameof(ThietBi.Phong)]!.FillWeight = 120;
        }
        if (dgv.Columns.Contains(nameof(ThietBi.NgayMua)))
        {
            dgv.Columns[nameof(ThietBi.NgayMua)]!.HeaderText = "Ngày mua";
            dgv.Columns[nameof(ThietBi.NgayMua)]!.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns[nameof(ThietBi.NgayMua)]!.Width = 110;
        }
        if (dgv.Columns.Contains(nameof(ThietBi.TinhTrang)))
        {
            dgv.Columns[nameof(ThietBi.TinhTrang)]!.HeaderText = "Tình trạng";
            dgv.Columns[nameof(ThietBi.TinhTrang)]!.Width = 120;
        }
    }

    private void BoLocThayDoi(object? sender, EventArgs e)
    {
        if (_dangKhoiTao) return;
        ApDungLoc();
    }

    private void ApDungLoc()
    {
        if (_dangKhoiTao) return;

        try
        {
            var ketQua = _danhSachGoc.AsEnumerable();

            // 1. Lọc theo từ khóa tên hoặc mã
            var tuKhoa = txtTen.Text.Trim();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                ketQua = ketQua.Where(t =>
                    (!string.IsNullOrEmpty(t.TenTB) && t.TenTB.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(t.MaThietBi) && t.MaThietBi.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                    t.MaTB.ToString().Contains(tuKhoa));
            }

            // 2. Lọc theo phòng tập
            if (cboPhong.SelectedIndex > 0)
            {
                var phongChon = cboPhong.SelectedItem?.ToString();
                if (phongChon == "(Chưa phân phòng)")
                {
                    ketQua = ketQua.Where(t => string.IsNullOrWhiteSpace(t.Phong));
                }
                else if (!string.IsNullOrEmpty(phongChon))
                {
                    ketQua = ketQua.Where(t => string.Equals(t.Phong, phongChon, StringComparison.OrdinalIgnoreCase));
                }
            }

            // 3. Lọc theo loại thiết bị
            if (cboLoai.SelectedIndex > 0)
            {
                var loaiChon = cboLoai.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(loaiChon))
                {
                    ketQua = ketQua.Where(t => string.Equals(t.LoaiThietBi, loaiChon, StringComparison.OrdinalIgnoreCase));
                }
            }

            // 4. Lọc theo tình trạng
            if (cboTinhTrang.SelectedIndex > 0)
            {
                var tinhTrangChon = cboTinhTrang.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(tinhTrangChon))
                {
                    ketQua = ketQua.Where(t => string.Equals(t.TinhTrang, tinhTrangChon, StringComparison.OrdinalIgnoreCase));
                }
            }

            var danhSachDaLoc = ketQua.ToList();
            dgv.DataSource = null;
            dgv.DataSource = danhSachDaLoc;
            DinhDangLuoi();

            lblThongKe.Text = $"Đang hiển thị: {danhSachDaLoc.Count} / {_danhSachGoc.Count} thiết bị";
        }
        catch (Exception ex)
        {
            lblThongKe.Text = "Lỗi khi lọc dữ liệu: " + UiErrorMessage.Get(ex);
        }
    }

    private void dgv_SelectionChanged(object? sender, EventArgs e)
    {
        if (_dangKhoiTao) return;
        _thietBiDangChon = dgv.CurrentRow?.DataBoundItem as ThietBi;
        btnSua.Enabled = _thietBiDangChon != null;
    }

    private async void btnSua_Click(object? sender, EventArgs e)
    {
        if (_thietBiDangChon is null)
        {
            MessageBox.Show("Vui lòng chọn thiết bị cần cập nhật từ danh sách.", "Chưa chọn thiết bị", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dlg = new Form
        {
            Text = $"Cập nhật thiết bị: {_thietBiDangChon.TenTB} ({_thietBiDangChon.MaThietBi})",
            Width = 430,
            Height = 220,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            MaximizeBox = false,
            MinimizeBox = false
        };

        var lblPhong = new Label { Text = "Phòng tập:", Left = 20, Top = 25, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var cboChonPhong = new ComboBox { Left = 130, Top = 22, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };

        var lblTT = new Label { Text = "Tình trạng:", Left = 20, Top = 65, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        var cboChonTT = new ComboBox { Left = 130, Top = 62, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
        cboChonTT.Items.AddRange(new object[] { "Hoạt động", "Bảo trì", "Hư hỏng", "Thanh lý" });
        cboChonTT.SelectedItem = _thietBiDangChon.TinhTrang ?? "Hoạt động";

        try
        {
            var phongs = await _bll.LayPhongAsync();
            cboChonPhong.Items.Add(new KeyValuePair<int?, string>(null, "(Chưa phân phòng)"));
            foreach (var p in phongs)
            {
                cboChonPhong.Items.Add(new KeyValuePair<int?, string>(p.MaPhong, p.TenPhong));
            }
            cboChonPhong.DisplayMember = "Value";
            cboChonPhong.ValueMember = "Key";

            cboChonPhong.SelectedIndex = 0;
            for (int i = 0; i < cboChonPhong.Items.Count; i++)
            {
                if (cboChonPhong.Items[i] is KeyValuePair<int?, string> kv && kv.Value == _thietBiDangChon.Phong)
                {
                    cboChonPhong.SelectedIndex = i;
                    break;
                }
            }
        }
        catch
        {
            cboChonPhong.Items.Add(new KeyValuePair<int?, string>(null, "(Chưa phân phòng)"));
            cboChonPhong.SelectedIndex = 0;
        }

        var btnLuu = new Button { Text = "Lưu thay đổi", Left = 160, Top = 115, Width = 110, Height = 34, DialogResult = DialogResult.OK, BackColor = Color.FromArgb(16, 185, 129), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        var btnHuy = new Button { Text = "Hủy", Left = 280, Top = 115, Width = 80, Height = 34, DialogResult = DialogResult.Cancel, FlatStyle = FlatStyle.Flat };

        dlg.Controls.AddRange(new Control[] { lblPhong, cboChonPhong, lblTT, cboChonTT, btnLuu, btnHuy });
        dlg.AcceptButton = btnLuu;
        dlg.CancelButton = btnHuy;

        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var phongItem = (KeyValuePair<int?, string>)cboChonPhong.SelectedItem!;
                var tinhTrangMoi = cboChonTT.SelectedItem?.ToString() ?? "Hoạt động";
                await _bll.CapNhatThietBiAsync(_thietBiDangChon.MaTB, phongItem.Key, tinhTrangMoi);
                await TaiDuLieuAsync();
                MessageBox.Show("Cập nhật thông tin thiết bị thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(UiErrorMessage.Get(ex), "Không thể cập nhật thiết bị", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    private async void btnLamMoi_Click(object? sender, EventArgs e)
    {
        txtTen.Clear();
        await TaiDuLieuAsync();
    }
}
