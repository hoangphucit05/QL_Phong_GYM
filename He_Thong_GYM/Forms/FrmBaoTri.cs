using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmBaoTri : Form
{
    private readonly TaiSanBLL _bll = new();
    private readonly NhanVienBLL _nv = new();
    private List<ThietBi> _tb = new();
    private List<BaoTri> _danhSachGoc = new();
    private bool _dangKhoiTao = true;

    public FrmBaoTri()
    {
        InitializeComponent();
        txtNoiDung.TextChanged += (s, e) => CapNhatTrangThaiNut();
        cboThietBi.SelectedIndexChanged += (s, e) => CapNhatTrangThaiNut();
        cboNhanVien.SelectedIndexChanged += (s, e) => CapNhatTrangThaiNut();
    }

    private async void FrmBaoTri_Load(object? sender, EventArgs e)
    {
        try
        {
            _dangKhoiTao = true;
            dgv.SelectionChanged += dgv_SelectionChanged;
            _tb = await _bll.LayThietBiAsync();
            cboThietBi.DataSource = _tb;
            cboThietBi.DisplayMember = nameof(ThietBi.HienThi);
            cboThietBi.SelectedIndex = -1;

            cboNhanVien.DataSource = await _nv.LayNhanVienDangLamAsync();
            cboNhanVien.DisplayMember = nameof(NhanVien.HienThi);
            cboNhanVien.SelectedIndex = -1;

            if (cboLocTrangThai.Items.Count > 0) cboLocTrangThai.SelectedIndex = 0;

            CapNhatTrangThaiNut();
            await TaiDanhSachAsync();
            CapNhatTrangThaiNut();
            _dangKhoiTao = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void dgv_SelectionChanged(object? sender, EventArgs e)
    {
        CapNhatTrangThaiNut();
    }

    private void CapNhatTrangThaiNut()
    {
        var baoTri = dgv.CurrentRow?.DataBoundItem as BaoTri;
        bool coChon = baoTri != null;
        bool chuaHoanThanh = coChon && !string.Equals(baoTri!.TrangThai, "Hoàn thành", StringComparison.OrdinalIgnoreCase);

        bool duThongTinTao = cboThietBi.SelectedItem is ThietBi &&
                             cboNhanVien.SelectedItem is NhanVien &&
                             !string.IsNullOrWhiteSpace(txtNoiDung.Text);
        UiButtonHelper.SetTrangThai(btnTao, duThongTinTao, UiButtonHelper.MauThemMoi);
        UiButtonHelper.SetTrangThai(btnHoanTat, chuaHoanThanh, UiButtonHelper.MauLuu);
        UiButtonHelper.SetTrangThai(btnLamMoi, true, UiButtonHelper.MauLamMoi);
    }

    private async Task TaiDanhSachAsync()
    {
        try
        {
            _danhSachGoc = await _bll.LayBaoTriAsync();
            ApDungLoc();
        }
        catch (Exception ex)
        {
            lblThongKe.Text = "Lỗi tải dữ liệu";
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi tải danh sách bảo trì", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ketQua = ketQua.Where(b =>
                (!string.IsNullOrEmpty(b.ThietBi) && b.ThietBi.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(b.NhanVien) && b.NhanVien.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(b.NoiDung) && b.NoiDung.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                b.MaBaoTri.ToString().Contains(tuKhoa));
        }

        if (cboLocTrangThai.SelectedIndex > 0)
        {
            var trangThaiChon = cboLocTrangThai.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(trangThaiChon))
            {
                ketQua = ketQua.Where(b => string.Equals(b.TrangThai, trangThaiChon, StringComparison.OrdinalIgnoreCase));
            }
        }

        var list = ketQua.ToList();
        UiGridHelper.DatNguon(dgv, list);
        DinhDangLuoi();

        lblThongKe.Text = $"Hiển thị: {list.Count} / {_danhSachGoc.Count} phiếu";
    }

    private void DinhDangLuoi()
    {
        if (dgv.Columns.Count == 0) return;

        if (dgv.Columns.Contains(nameof(BaoTri.MaBaoTri)))
        {
            dgv.Columns[nameof(BaoTri.MaBaoTri)]!.HeaderText = "Mã BT";
            dgv.Columns[nameof(BaoTri.MaBaoTri)]!.Width = 75;
        }
        if (dgv.Columns.Contains(nameof(BaoTri.ThietBi)))
        {
            dgv.Columns[nameof(BaoTri.ThietBi)]!.HeaderText = "Thiết bị";
            dgv.Columns[nameof(BaoTri.ThietBi)]!.FillWeight = 140;
        }
        if (dgv.Columns.Contains(nameof(BaoTri.NhanVien)))
        {
            dgv.Columns[nameof(BaoTri.NhanVien)]!.HeaderText = "NV phụ trách";
            dgv.Columns[nameof(BaoTri.NhanVien)]!.FillWeight = 120;
        }
        if (dgv.Columns.Contains(nameof(BaoTri.NgayBaoTri)))
        {
            dgv.Columns[nameof(BaoTri.NgayBaoTri)]!.HeaderText = "Ngày bảo trì";
            dgv.Columns[nameof(BaoTri.NgayBaoTri)]!.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns[nameof(BaoTri.NgayBaoTri)]!.Width = 110;
        }
        if (dgv.Columns.Contains(nameof(BaoTri.NoiDung)))
        {
            dgv.Columns[nameof(BaoTri.NoiDung)]!.HeaderText = "Nội dung bảo trì";
            dgv.Columns[nameof(BaoTri.NoiDung)]!.FillWeight = 180;
        }
        if (dgv.Columns.Contains(nameof(BaoTri.ChiPhi)))
        {
            dgv.Columns[nameof(BaoTri.ChiPhi)]!.HeaderText = "Chi phí (VNĐ)";
            dgv.Columns[nameof(BaoTri.ChiPhi)]!.DefaultCellStyle.Format = "N0";
            dgv.Columns[nameof(BaoTri.ChiPhi)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[nameof(BaoTri.ChiPhi)]!.Width = 120;
        }
        if (dgv.Columns.Contains(nameof(BaoTri.TrangThai)))
        {
            dgv.Columns[nameof(BaoTri.TrangThai)]!.HeaderText = "Trạng thái";
            dgv.Columns[nameof(BaoTri.TrangThai)]!.Width = 120;
        }
    }

    private async void btnTao_Click(object? sender, EventArgs e)
    {
        try
        {
            if (cboThietBi.SelectedItem is not ThietBi t || cboNhanVien.SelectedItem is not NhanVien n)
            {
                throw new InvalidOperationException("Hãy chọn thiết bị và nhân viên.");
            }

            await _bll.TaoBaoTriAsync(t.MaTB, n.MaNV, txtNoiDung.Text, nudChiPhi.Value);
            MessageBox.Show("Đã tạo phiếu bảo trì thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtNoiDung.Clear();
            nudChiPhi.Value = 0;
            await TaiDanhSachAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không thể tạo bảo trì", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async void btnHoanTat_Click(object? sender, EventArgs e)
    {
        try
        {
            if (dgv.CurrentRow?.DataBoundItem is not BaoTri b)
            {
                MessageBox.Show("Hãy chọn phiếu bảo trì cần hoàn tất.", "Chưa chọn phiếu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (b.TrangThai == "Hoàn thành")
            {
                MessageBox.Show("Phiếu bảo trì này đã ở trạng thái Hoàn thành.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var xacNhan = MessageBox.Show($"Xác nhận hoàn tất bảo trì cho phiếu #{b.MaBaoTri} ({b.ThietBi})?\nThiết bị sẽ được khôi phục về trạng thái Hoạt động.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (xacNhan != DialogResult.Yes) return;

            var maNV = Helpers.SessionManager.NguoiDungHienTai?.MaNV ?? 1;
            await _bll.HoanTatBaoTriAsync(b.MaBaoTri, maNV, "Đã sửa chữa và kiểm tra hoạt động tốt.");
            MessageBox.Show("Đã hoàn tất bảo trì thiết bị thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await TaiDanhSachAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không thể hoàn tất bảo trì", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async void btnLamMoi_Click(object? sender, EventArgs e)
    {
        txtTimKiem.Clear();
        if (cboLocTrangThai.Items.Count > 0) cboLocTrangThai.SelectedIndex = 0;
        await TaiDanhSachAsync();
    }
}
