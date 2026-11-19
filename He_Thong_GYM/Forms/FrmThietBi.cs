using He_Thong_GYM.BLL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmThietBi : Form
{
    private readonly TaiSanBLL _bll = new();
    private List<ThietBi> _danhSachGoc = new();
    private bool _dangKhoiTao = true;

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
            MessageBox.Show(ex.Message, "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void DinhDangLuoi()
    {
        if (dgv.Columns.Count == 0) return;

        if (dgv.Columns.Contains(nameof(ThietBi.MaTB)))
        {
            dgv.Columns[nameof(ThietBi.MaTB)]!.HeaderText = "Mã TB";
            dgv.Columns[nameof(ThietBi.MaTB)]!.Width = 80;
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
            lblThongKe.Text = "Lỗi khi lọc dữ liệu: " + ex.Message;
        }
    }

    private async void btnLamMoi_Click(object? sender, EventArgs e)
    {
        txtTen.Clear();
        await TaiDuLieuAsync();
    }
}
