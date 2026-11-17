
using He_Thong_GYM.BLL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmThanhVien : Form
{
    private readonly ThanhVienBLL _thanhVienBLL = new();
    private int? _maTVDangChon;

    public FrmThanhVien()
    {
        InitializeComponent();
    }

    private async void FrmThanhVien_Load(object? sender, EventArgs e)
    {
        cboGioiTinh.SelectedIndex = 0;
        cboTrangThai.SelectedIndex = 0;
        dgvThanhVien.AutoGenerateColumns = false;
        await TaiDuLieuAsync();
    }

    private void btnLamMoi_Click(object? sender, EventArgs e)
    {
        DatTrangThaiTaoMoi();
    }

    private void DatTrangThaiTaoMoi()
    {
        _maTVDangChon = null;
        txtHoTen.Clear();
        txtSoDienThoai.Clear();
        txtEmail.Clear();
        txtDiaChi.Clear();
        txtTimKiem.Clear();
        dtpNgaySinh.Value = DateTime.Today.AddYears(-18);
        cboGioiTinh.SelectedIndex = 0;
        cboTrangThai.SelectedIndex = 0;
        if (cboLoaiThanhVien.Items.Count > 0) cboLoaiThanhVien.SelectedIndex = 0;
        dgvThanhVien.ClearSelection();
        txtHoTen.Focus();
    }

    private async void btnTimKiem_Click(object? sender, EventArgs e)
    {
        await TaiDanhSachAsync(txtTimKiem.Text);
    }

    private void dgvThanhVien_SelectionChanged(object? sender, EventArgs e)
    {
        btnSua.Enabled = dgvThanhVien.SelectedRows.Count > 0;
        btnXoa.Enabled = dgvThanhVien.SelectedRows.Count > 0;

        if (dgvThanhVien.CurrentRow?.DataBoundItem is not ThanhVien thanhVien)
        {
            return;
        }

        _maTVDangChon = thanhVien.MaTV;
        txtHoTen.Text = thanhVien.HoTen;
        dtpNgaySinh.Value = thanhVien.NgaySinh;
        cboGioiTinh.SelectedItem = thanhVien.GioiTinh ?? "Khác";
        txtSoDienThoai.Text = thanhVien.SDT;
        txtEmail.Text = thanhVien.Email;
        txtDiaChi.Text = thanhVien.DiaChi;
        cboLoaiThanhVien.SelectedValue = thanhVien.MaLoaiTV;
        cboTrangThai.SelectedItem = thanhVien.TrangThai;
    }

    private void btnThem_Click(object? sender, EventArgs e) => DatTrangThaiTaoMoi();

    private void btnSua_Click(object? sender, EventArgs e)
    {
        if (_maTVDangChon is null)
        {
            MessageBox.Show("Hãy chọn một thành viên cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        txtHoTen.Focus();
    }

    private async void btnLuu_Click(object? sender, EventArgs e)
    {
        try
        {
            var thanhVien = DocDuLieuForm();
            var maTV = await _thanhVienBLL.LuuAsync(thanhVien);
            await TaiDanhSachAsync(txtTimKiem.Text);
            _maTVDangChon = maTV;
            MessageBox.Show("Đã lưu thông tin thành viên.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            MessageBox.Show(ex.Message, "Không thể lưu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            HienThiLoiKetNoi(ex);
        }
    }

    private async void btnXoa_Click(object? sender, EventArgs e)
    {
        if (_maTVDangChon is null) return;
        if (MessageBox.Show("Ngừng sử dụng thành viên này? Dữ liệu lịch sử sẽ được giữ lại.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

        try
        {
            await _thanhVienBLL.NgungSuDungAsync(_maTVDangChon.Value);
            DatTrangThaiTaoMoi();
            await TaiDanhSachAsync(txtTimKiem.Text);
        }
        catch (Exception ex)
        {
            HienThiLoiKetNoi(ex);
        }
    }

    private async Task TaiDuLieuAsync()
    {
        try
        {
            var loaiThanhVien = await _thanhVienBLL.LayLoaiThanhVienAsync();
            cboLoaiThanhVien.DataSource = loaiThanhVien;
            cboLoaiThanhVien.DisplayMember = nameof(LoaiThanhVien.TenLoai);
            cboLoaiThanhVien.ValueMember = nameof(LoaiThanhVien.MaLoaiTV);
            await TaiDanhSachAsync();
        }
        catch (Exception ex)
        {
            HienThiLoiKetNoi(ex);
        }
    }

    private async Task TaiDanhSachAsync(string? tuKhoa = null)
    {
        try
        {
            dgvThanhVien.DataSource = await _thanhVienBLL.LayDanhSachAsync(tuKhoa);
            lblTrangThaiTimKiem.Text = string.IsNullOrWhiteSpace(tuKhoa)
                ? "Hiển thị toàn bộ danh sách thành viên."
                : $"Đã lọc theo từ khóa: {tuKhoa.Trim()}";
        }
        catch (Exception ex)
        {
            HienThiLoiKetNoi(ex);
        }
    }

    private ThanhVien DocDuLieuForm()
    {
        return new ThanhVien
        {
            MaTV = _maTVDangChon ?? 0,
            HoTen = txtHoTen.Text.Trim(),
            NgaySinh = dtpNgaySinh.Value,
            GioiTinh = cboGioiTinh.SelectedItem?.ToString(),
            SDT = txtSoDienThoai.Text.Trim(),
            Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
            DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim(),
            MaLoaiTV = cboLoaiThanhVien.SelectedValue is int maLoai ? maLoai : 0,
            TrangThai = cboTrangThai.SelectedItem?.ToString() ?? string.Empty
        };
    }

    private void HienThiLoiKetNoi(Exception ex)
    {
        lblTrangThaiTimKiem.Text = "Không thể tải dữ liệu. Kiểm tra appsettings.json và SQL Server.";
        MessageBox.Show(ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
