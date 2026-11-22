using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmDiemDanh : Form
{
    private readonly LopTapBLL _bll = new();
    private readonly DangKyBLL _dk = new();
    private BuoiHoc? _buoiHocDangChon;
    private List<BuoiHoc> _buoiHocTrongNgay = new();
    private List<HocVienLop> _hocVienGoc = new();
    private bool _dangTaiBuoi;

    public FrmDiemDanh()
    {
        InitializeComponent();
        cboLop.SelectedIndexChanged += cboLop_SelectedIndexChanged;
        dtpNgay.ValueChanged += dtpNgay_ValueChanged;
        cboBuoiHoc.SelectedIndexChanged += cboBuoiHoc_SelectedIndexChanged;
        btnDangKy.Click += btnDangKy_Click;
        btnDiemDanh.Click += btnDiemDanh_Click;
        btnTaoBuoi.Click += btnTaoBuoi_Click;
        cboLocDiemDanh.SelectedIndexChanged += BoLocHoacSapXepThayDoi;
        cboSapXep.SelectedIndexChanged += BoLocHoacSapXepThayDoi;
        btnBoLoc.Click += btnBoLoc_Click;
        dgvHocVien.SelectionChanged += (s, e) => CapNhatTrangThaiDiemDanh();
    }

    private async void FrmDiemDanh_Load(object? sender, EventArgs e)
    {
        try
        {
            cboLop.DataSource = await _bll.LayDanhSachAsync();
        cboLop.DisplayMember = nameof(LopTap.HienThi);
            cboLop.ValueMember = nameof(LopTap.MaLop);

            cboThanhVien.DataSource = await _dk.LayThanhVienAsync();
        cboThanhVien.DisplayMember = nameof(ThanhVien.HienThi);
            cboThanhVien.ValueMember = nameof(ThanhVien.MaTV);

            cboTrangThai.SelectedIndex = 0;
            cboLocDiemDanh.SelectedIndex = 0;
            cboSapXep.SelectedIndex = 0;
            CapNhatQuyenThaoTac();
            await TaiHocVienAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task TaiHocVienAsync(int? maLichHocUuTien = null)
    {
        _buoiHocDangChon = null;
        _buoiHocTrongNgay.Clear();
        _dangTaiBuoi = true;
        cboBuoiHoc.DataSource = null;
        if (cboLop.SelectedItem is not LopTap lop)
        {
            _dangTaiBuoi = false;
            cboBuoiHoc.Enabled = false;
            dgvHocVien.DataSource = null;
            _hocVienGoc.Clear();
            lblTrangThaiBuoi.Text = "Hãy chọn lớp và ngày học.";
            return;
        }

        _buoiHocTrongNgay = await _bll.LayBuoiHocAsync(lop.MaLop, dtpNgay.Value);
        if (_buoiHocTrongNgay.Count == 0)
        {
            _dangTaiBuoi = false;
            cboBuoiHoc.Enabled = false;
            dgvHocVien.DataSource = null;
            _hocVienGoc.Clear();
            lblTrangThaiBuoi.Text = "Chưa có buổi học cho lớp này trong ngày đã chọn. Hãy tạo buổi học trước khi điểm danh.";
            return;
        }

        cboBuoiHoc.DataSource = _buoiHocTrongNgay;
        cboBuoiHoc.DisplayMember = nameof(BuoiHoc.HienThi);
        cboBuoiHoc.ValueMember = nameof(BuoiHoc.MaLichHoc);
        cboBuoiHoc.Enabled = true;
        var viTriUuTien = maLichHocUuTien.HasValue
            ? _buoiHocTrongNgay.FindIndex(x => x.MaLichHoc == maLichHocUuTien.Value)
            : 0;
        cboBuoiHoc.SelectedIndex = viTriUuTien >= 0 ? viTriUuTien : 0;
        _dangTaiBuoi = false;
        await TaiHocVienTheoBuoiAsync(lop);
    }

    private async Task TaiHocVienTheoBuoiAsync(LopTap? lop = null)
    {
        lop ??= cboLop.SelectedItem as LopTap;
        _buoiHocDangChon = cboBuoiHoc.SelectedItem as BuoiHoc;
        if (lop is null || _buoiHocDangChon is null)
            return;

        _hocVienGoc = await _bll.LayHocVienAsync(lop.MaLop, _buoiHocDangChon.MaLichHoc);
        ApDungLocVaSapXep();
        lblTrangThaiBuoi.Text = $"Đang chọn buổi #{_buoiHocDangChon.MaLichHoc}: {_buoiHocDangChon.ThoiGianBatDau:dd/MM/yyyy HH:mm} - {_buoiHocDangChon.ThoiGianKetThuc:HH:mm}";
    }

    private async void cboLop_SelectedIndexChanged(object? sender, EventArgs e) => await TaiHocVienAsync();

    private async void dtpNgay_ValueChanged(object? sender, EventArgs e) => await TaiHocVienAsync();

    private async void cboBuoiHoc_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_dangTaiBuoi)
            return;

        await TaiHocVienTheoBuoiAsync();
    }

    private async void btnDangKy_Click(object? sender, EventArgs e)
    {
        try
        {
            KiemTraQuyenDangKy();
            if (cboLop.SelectedItem is not LopTap l || cboThanhVien.SelectedItem is not ThanhVien t)
            {
                throw new InvalidOperationException("Hãy chọn lớp và thành viên.");
            }

            await _bll.DangKyLopAsync(t.MaTV, l.MaLop);
            await TaiHocVienAsync();
            MessageBox.Show("Đã đăng ký lớp hoặc thêm vào danh sách chờ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không thể đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async void btnDiemDanh_Click(object? sender, EventArgs e)
    {
        try
        {
            KiemTraQuyenDiemDanh();
            if (dgvHocVien.CurrentRow?.DataBoundItem is not HocVienLop h)
            {
                throw new InvalidOperationException("Hãy chọn một học viên trong danh sách để điểm danh.");
            }
            if (_buoiHocDangChon is null)
            {
                throw new InvalidOperationException("Hãy tạo hoặc chọn buổi học hợp lệ trước khi điểm danh.");
            }

            await _bll.DiemDanhAsync(h.MaDKL, _buoiHocDangChon.MaLichHoc, cboTrangThai.Text);
            await TaiHocVienAsync();
            MessageBox.Show($"Đã lưu điểm danh cho học viên: {cboTrangThai.Text}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không thể điểm danh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async void btnTaoBuoi_Click(object? sender, EventArgs e)
    {
        try
        {
            KiemTraQuyenDiemDanh();
            if (cboLop.SelectedItem is not LopTap lop)
                throw new InvalidOperationException("Hãy chọn lớp tập trước khi tạo buổi học.");

            btnTaoBuoi.Enabled = false;
            var maLichHoc = await _bll.TaoBuoiHocAsync(lop, dtpNgay.Value);
            await TaiHocVienAsync(maLichHoc);
            MessageBox.Show($"Đã tạo buổi học #{maLichHoc} cho lớp {lop.TenLop}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không thể tạo buổi học", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        finally
        {
            btnTaoBuoi.Enabled = true;
        }
    }

    private void DinhDangLuoiHocVien()
    {
        if (dgvHocVien.Columns.Count == 0) return;

        DinhDangCot(nameof(HocVienLop.MaDKL), "Mã ĐK", 70);
        DinhDangCot(nameof(HocVienLop.MaTV), "Mã TV", 70);
        DinhDangCot(nameof(HocVienLop.HoTen), "Thành viên", 180);
        DinhDangCot(nameof(HocVienLop.SDT), "Số điện thoại", 120);
        DinhDangCot(nameof(HocVienLop.TrangThaiDiemDanh), "Điểm danh", 130);
    }

    private void DinhDangCot(string tenCot, string tieuDe, int chieuRong)
    {
        if (!dgvHocVien.Columns.Contains(tenCot)) return;

        dgvHocVien.Columns[tenCot]!.HeaderText = tieuDe;
        dgvHocVien.Columns[tenCot]!.Width = chieuRong;
    }

    private void BoLocHoacSapXepThayDoi(object? sender, EventArgs e) => ApDungLocVaSapXep();

    private void btnBoLoc_Click(object? sender, EventArgs e)
    {
        cboLocDiemDanh.SelectedIndex = 0;
        cboSapXep.SelectedIndex = 0;
        ApDungLocVaSapXep();
    }

    private void ApDungLocVaSapXep()
    {
        IEnumerable<HocVienLop> ketQua = _hocVienGoc;

        ketQua = cboLocDiemDanh.SelectedIndex switch
        {
            1 => ketQua.Where(x => x.TrangThaiDiemDanh == "Chưa điểm danh"),
            2 => ketQua.Where(x => x.TrangThaiDiemDanh == "Có mặt"),
            3 => ketQua.Where(x => x.TrangThaiDiemDanh == "Vắng mặt"),
            _ => ketQua
        };

        ketQua = cboSapXep.SelectedIndex switch
        {
            1 => ketQua.OrderByDescending(x => x.HoTen),
            2 => ketQua.OrderBy(x => x.MaTV),
            3 => ketQua.OrderByDescending(x => x.MaTV),
            4 => ketQua.OrderBy(x => ThuTuDiemDanh(x.TrangThaiDiemDanh)).ThenBy(x => x.HoTen),
            _ => ketQua.OrderBy(x => x.HoTen)
        };

        dgvHocVien.DataSource = null;
        dgvHocVien.DataSource = ketQua.ToList();
        DinhDangLuoiHocVien();
    }

    private static int ThuTuDiemDanh(string? trangThai) => trangThai switch
    {
        "Chưa điểm danh" => 0,
        "Có mặt" => 1,
        "Vắng mặt" => 2,
        _ => 3
    };

    private void CapNhatQuyenThaoTac()
    {
        var coQuyenDangKy = SessionManager.CoVaiTro("Admin", "Quản lý", "Lễ tân", "Nhân viên");
        var coQuyenDiemDanh = SessionManager.CoVaiTro("Admin", "Quản lý", "Lễ tân", "HLV", "Huấn luyện viên");

        UiButtonHelper.SetTrangThai(btnDangKy, coQuyenDangKy, UiButtonHelper.MauThemMoi);
        UiButtonHelper.SetTrangThai(btnTaoBuoi, coQuyenDiemDanh, UiButtonHelper.MauChinhSua);
        cboTrangThai.Enabled = coQuyenDiemDanh;

        CapNhatTrangThaiDiemDanh();

        if (!coQuyenDangKy || !coQuyenDiemDanh)
            lblQuyenThaoTac.Text = "Bạn chỉ có thể thực hiện các thao tác được cấp quyền; bộ lọc và sắp xếp luôn khả dụng.";
    }

    private void CapNhatTrangThaiDiemDanh()
    {
        var coQuyenDiemDanh = SessionManager.CoVaiTro("Admin", "Quản lý", "Lễ tân", "HLV", "Huấn luyện viên");
        bool coHocVien = dgvHocVien.CurrentRow?.DataBoundItem is HocVienLop;
        UiButtonHelper.SetTrangThai(btnDiemDanh, coQuyenDiemDanh && coHocVien, Color.FromArgb(5, 150, 105));
    }

    private static void KiemTraQuyenDangKy()
    {
        if (!SessionManager.CoVaiTro("Admin", "Quản lý", "Lễ tân", "Nhân viên"))
            throw new UnauthorizedAccessException("Bạn không có quyền đăng ký thành viên vào lớp.");
    }

    private static void KiemTraQuyenDiemDanh()
    {
        if (!SessionManager.CoVaiTro("Admin", "Quản lý", "Lễ tân", "HLV", "Huấn luyện viên"))
            throw new UnauthorizedAccessException("Bạn không có quyền tạo buổi học hoặc điểm danh.");
    }
}
