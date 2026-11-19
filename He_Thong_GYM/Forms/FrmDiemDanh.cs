using He_Thong_GYM.BLL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmDiemDanh : Form
{
    private readonly LopTapBLL _bll = new();
    private readonly DangKyBLL _dk = new();
    private BuoiHoc? _buoiHocDangChon;

    public FrmDiemDanh()
    {
        InitializeComponent();
        cboLop.SelectedIndexChanged += cboLop_SelectedIndexChanged;
        dtpNgay.ValueChanged += dtpNgay_ValueChanged;
        btnDangKy.Click += btnDangKy_Click;
        btnDiemDanh.Click += btnDiemDanh_Click;
        btnTaoBuoi.Click += btnTaoBuoi_Click;
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
        _buoiHocDangChon = null;
        if (cboLop.SelectedItem is not LopTap lop)
        {
            dgvHocVien.DataSource = null;
            lblTrangThaiBuoi.Text = "Hãy chọn lớp và ngày học.";
            return;
        }

        var buoiHoc = await _bll.LayBuoiHocAsync(lop.MaLop, dtpNgay.Value);
        if (buoiHoc is null)
        {
            dgvHocVien.DataSource = null;
            lblTrangThaiBuoi.Text = "Chưa có buổi học cho lớp này trong ngày đã chọn. Hãy tạo buổi học trước khi điểm danh.";
            return;
        }

        _buoiHocDangChon = buoiHoc;
        dgvHocVien.DataSource = await _bll.LayHocVienAsync(lop.MaLop, buoiHoc.MaLichHoc);
        DinhDangLuoiHocVien();
        lblTrangThaiBuoi.Text = $"Buổi #{buoiHoc.MaLichHoc}: {buoiHoc.ThoiGianBatDau:dd/MM/yyyy HH:mm} - {buoiHoc.ThoiGianKetThuc:HH:mm}";
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

            await _bll.DangKyLopAsync(t.MaTV, l.MaLop);
            await TaiHocVienAsync();
            MessageBox.Show("Đã đăng ký lớp hoặc thêm vào danh sách chờ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async void btnDiemDanh_Click(object? sender, EventArgs e)
    {
        try
        {
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
            MessageBox.Show(ex.Message, "Không thể điểm danh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async void btnTaoBuoi_Click(object? sender, EventArgs e)
    {
        try
        {
            if (cboLop.SelectedItem is not LopTap lop)
                throw new InvalidOperationException("Hãy chọn lớp tập trước khi tạo buổi học.");

            btnTaoBuoi.Enabled = false;
            var maLichHoc = await _bll.TaoBuoiHocAsync(lop, dtpNgay.Value);
            await TaiHocVienAsync();
            MessageBox.Show($"Đã tạo buổi học #{maLichHoc} cho lớp {lop.TenLop}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể tạo buổi học", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
}
