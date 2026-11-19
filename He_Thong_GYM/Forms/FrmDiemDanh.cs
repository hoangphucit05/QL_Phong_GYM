using He_Thong_GYM.BLL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmDiemDanh : Form
{
    private readonly LopTapBLL _bll = new();
    private readonly DangKyBLL _dk = new();

    public FrmDiemDanh()
    {
        InitializeComponent();
        cboLop.SelectedIndexChanged += cboLop_SelectedIndexChanged;
        dtpNgay.ValueChanged += dtpNgay_ValueChanged;
        btnDangKy.Click += btnDangKy_Click;
        btnDiemDanh.Click += btnDiemDanh_Click;
        dgvHocVien.SelectionChanged += dgvHocVien_SelectionChanged;
    }

    private async void FrmDiemDanh_Load(object? sender, EventArgs e)
    {
        try
        {
            cboLop.DataSource = await _bll.LayDanhSachAsync();
            cboLop.DisplayMember = nameof(LopTap.TenLop);
            cboLop.ValueMember = nameof(LopTap.MaLop);

            cboThanhVien.DataSource = await _dk.LayThanhVienAsync();
            cboThanhVien.DisplayMember = nameof(ThanhVien.HoTen);
            cboThanhVien.ValueMember = nameof(ThanhVien.MaTV);

            cboTrangThai.SelectedIndex = 0;
            await TaiHocVienAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task TaiHocVienAsync()
    {
        if (cboLop.SelectedItem is LopTap l)
        {
            dgvHocVien.DataSource = await _bll.LayHocVienAsync(l.MaLop, dtpNgay.Value);
            DinhDangLuoiHocVien();
        }
        else dgvHocVien.DataSource = null;
    }

    private async void cboLop_SelectedIndexChanged(object? sender, EventArgs e) => await TaiHocVienAsync();

    private async void dtpNgay_ValueChanged(object? sender, EventArgs e) => await TaiHocVienAsync();

    private async void btnDangKy_Click(object? sender, EventArgs e)
    {
        try
        {
            if (cboLop.SelectedItem is not LopTap l || cboThanhVien.SelectedItem is not ThanhVien t)
            {
                throw new InvalidOperationException("Hãy chọn lớp và thành viên.");
            }

            btnDangKy.Enabled = false;
            var ketQua = await _bll.DangKyLopAsync(t.MaTV, l.MaLop);
            await TaiHocVienAsync();
            var thongBao = ketQua.DaVaoDanhSachCho
                ? "Lớp đã đủ chỗ. Thành viên đã được thêm vào danh sách chờ."
                : "Đã đăng ký lớp thành công.";
            MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        finally { btnDangKy.Enabled = true; }
    }

    private async void btnDiemDanh_Click(object? sender, EventArgs e)
    {
        try
        {
            if (dgvHocVien.CurrentRow?.DataBoundItem is not HocVienLop h)
            {
                throw new InvalidOperationException("Hãy chọn một học viên trong danh sách để điểm danh.");
            }

            btnDiemDanh.Enabled = false;
            await _bll.DiemDanhAsync(h.MaDKL, dtpNgay.Value, cboTrangThai.Text);
            await TaiHocVienAsync();
            MessageBox.Show($"Đã lưu điểm danh cho học viên: {cboTrangThai.Text}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể điểm danh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        finally { btnDiemDanh.Enabled = true; }
    }

    private void dgvHocVien_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvHocVien.CurrentRow?.DataBoundItem is not HocVienLop hocVien) return;
        if (hocVien.TrangThaiDiemDanh is "Có mặt" or "Vắng mặt")
            cboTrangThai.SelectedItem = hocVien.TrangThaiDiemDanh;
    }

    private void DinhDangLuoiHocVien()
    {
        if (dgvHocVien.Columns.Count == 0) return;
        DinhDang(nameof(HocVienLop.MaDKL), "Mã ĐK", 70);
        DinhDang(nameof(HocVienLop.MaTV), "Mã TV", 70);
        DinhDang(nameof(HocVienLop.HoTen), "Thành viên", 180);
        DinhDang(nameof(HocVienLop.SDT), "Số điện thoại", 120);
        DinhDang(nameof(HocVienLop.TrangThaiDiemDanh), "Điểm danh", 130);
    }

    private void DinhDang(string tenCot, string tieuDe, int rong)
    {
        if (!dgvHocVien.Columns.Contains(tenCot)) return;
        dgvHocVien.Columns[tenCot]!.HeaderText = tieuDe;
        dgvHocVien.Columns[tenCot]!.Width = rong;
    }
}
