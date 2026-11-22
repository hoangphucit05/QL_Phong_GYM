using System.Drawing;
using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmThanhVien : Form
{
    private enum CheDoThaoTac
    {
        Xem,       // Chế độ xem: các ô nhập chỉ đọc, nút Sửa/Xóa/Lưu ở trạng thái nghỉ trừ khi có dòng được chọn
        DangThem,  // Đang thêm mới: các ô nhập mở khóa, nút Lưu sáng, các nút khác ở trạng thái nghỉ
        DangSua    // Đang sửa: các ô nhập mở khóa, nút Lưu sáng, các nút khác ở trạng thái nghỉ
    }

    private readonly ThanhVienBLL _thanhVienBLL = new();
    private int? _maTVDangChon;
    private CheDoThaoTac _cheDo = CheDoThaoTac.Xem;

    public FrmThanhVien()
    {
        InitializeComponent();
        txtHoTen.PlaceholderText = "Nhập họ và tên hội viên...";
        txtSoDienThoai.PlaceholderText = "Nhập số điện thoại...";
        txtEmail.PlaceholderText = "Nhập email (không bắt buộc)...";
        txtDiaChi.PlaceholderText = "Nhập địa chỉ (không bắt buộc)...";
    }

    private async void FrmThanhVien_Load(object? sender, EventArgs e)
    {
        cboGioiTinh.SelectedIndex = -1;
        cboTrangThai.SelectedIndex = -1;
        dgvThanhVien.AutoGenerateColumns = false;
        KhoaOThongTin(true);
        CapNhatTrangThaiNut();

        await TaiDuLieuAsync();
        DatTrangThaiNghiBanDau();
        BeginInvoke(new Action(DatTrangThaiNghiBanDau));
    }

    private void KhoaOThongTin(bool khoa)
    {
        txtHoTen.ReadOnly = khoa;
        txtSoDienThoai.ReadOnly = khoa;
        txtEmail.ReadOnly = khoa;
        txtDiaChi.ReadOnly = khoa;
        dtpNgaySinh.Enabled = !khoa;
        cboGioiTinh.Enabled = !khoa;
        cboLoaiThanhVien.Enabled = !khoa;
        cboTrangThai.Enabled = !khoa;

        Color mauNen = khoa ? Color.FromArgb(248, 250, 252) : Color.White;
        txtHoTen.BackColor = mauNen;
        txtSoDienThoai.BackColor = mauNen;
        txtEmail.BackColor = mauNen;
        txtDiaChi.BackColor = mauNen;
    }

    private void CapNhatTrangThaiNut()
    {
        switch (_cheDo)
        {
            case CheDoThaoTac.Xem:
                bool coChon = _maTVDangChon.HasValue;
                UiButtonHelper.SetTrangThai(btnThem, true, UiButtonHelper.MauThemMoi);
                UiButtonHelper.SetTrangThai(btnSua, coChon, UiButtonHelper.MauChinhSua);
                UiButtonHelper.SetTrangThai(btnLuu, false, UiButtonHelper.MauLuu); // Ở trạng thái nghỉ khi chưa thao tác
                UiButtonHelper.SetTrangThai(btnXoa, coChon, UiButtonHelper.MauXoa);
                UiButtonHelper.SetTrangThai(btnLamMoi, true, UiButtonHelper.MauLamMoi);
                btnLamMoi.Text = "Làm mới";
                break;

            case CheDoThaoTac.DangThem:
                UiButtonHelper.SetTrangThai(btnThem, false, UiButtonHelper.MauThemMoi);
                UiButtonHelper.SetTrangThai(btnSua, false, UiButtonHelper.MauChinhSua);
                UiButtonHelper.SetTrangThai(btnLuu, true, UiButtonHelper.MauLuu); // Kích hoạt sáng để lưu bản ghi mới
                UiButtonHelper.SetTrangThai(btnXoa, false, UiButtonHelper.MauXoa);
                UiButtonHelper.SetTrangThai(btnLamMoi, true, UiButtonHelper.MauLamMoi);
                btnLamMoi.Text = "Hủy bỏ";
                break;

            case CheDoThaoTac.DangSua:
                UiButtonHelper.SetTrangThai(btnThem, false, UiButtonHelper.MauThemMoi);
                UiButtonHelper.SetTrangThai(btnSua, false, UiButtonHelper.MauChinhSua);
                UiButtonHelper.SetTrangThai(btnLuu, true, UiButtonHelper.MauLuu); // Kích hoạt sáng để lưu thông tin sửa
                UiButtonHelper.SetTrangThai(btnXoa, false, UiButtonHelper.MauXoa);
                UiButtonHelper.SetTrangThai(btnLamMoi, true, UiButtonHelper.MauLamMoi);
                btnLamMoi.Text = "Hủy bỏ";
                break;
        }
    }

    private void DatTrangThaiNghiBanDau()
    {
        _cheDo = CheDoThaoTac.Xem;
        _maTVDangChon = null;
        txtHoTen.Clear();
        txtSoDienThoai.Clear();
        txtEmail.Clear();
        txtDiaChi.Clear();
        txtTimKiem.Clear();
        dtpNgaySinh.Value = DateTime.Today.AddYears(-18);
        cboGioiTinh.SelectedIndex = -1;
        cboTrangThai.SelectedIndex = -1;
        if (cboLoaiThanhVien.Items.Count > 0) cboLoaiThanhVien.SelectedIndex = -1;
        UiGridHelper.BoChon(dgvThanhVien);
        KhoaOThongTin(true);
        CapNhatTrangThaiNut();
    }

    private void btnLamMoi_Click(object? sender, EventArgs e)
    {
        DatTrangThaiNghiBanDau();
    }

    private async void btnTimKiem_Click(object? sender, EventArgs e)
    {
        await TaiDanhSachAsync(txtTimKiem.Text);
        DatTrangThaiNghiBanDau();
    }

    private void dgvThanhVien_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvThanhVien.CurrentRow?.DataBoundItem is not ThanhVien thanhVien)
        {
            _maTVDangChon = null;
            if (_cheDo == CheDoThaoTac.Xem)
            {
                CapNhatTrangThaiNut();
            }
            return;
        }

        _maTVDangChon = thanhVien.MaTV;
        txtHoTen.Text = thanhVien.HoTen;
        dtpNgaySinh.Value = thanhVien.NgaySinh;
        cboGioiTinh.SelectedItem = thanhVien.GioiTinh ?? "Khác";
        txtSoDienThoai.Text = thanhVien.SDT;
        txtEmail.Text = thanhVien.Email ?? string.Empty;
        txtDiaChi.Text = thanhVien.DiaChi ?? string.Empty;
        cboLoaiThanhVien.SelectedValue = thanhVien.MaLoaiTV;
        cboTrangThai.SelectedItem = thanhVien.TrangThai;

        _cheDo = CheDoThaoTac.Xem;
        KhoaOThongTin(true);
        CapNhatTrangThaiNut();
    }

    private void btnThem_Click(object? sender, EventArgs e)
    {
        _cheDo = CheDoThaoTac.DangThem;
        _maTVDangChon = null;
        txtHoTen.Clear();
        txtSoDienThoai.Clear();
        txtEmail.Clear();
        txtDiaChi.Clear();
        dtpNgaySinh.Value = DateTime.Today.AddYears(-18);
        cboGioiTinh.SelectedIndex = 0;
        cboTrangThai.SelectedIndex = 0;
        if (cboLoaiThanhVien.Items.Count > 0) cboLoaiThanhVien.SelectedIndex = 0;
        UiGridHelper.BoChon(dgvThanhVien);
        KhoaOThongTin(false);
        CapNhatTrangThaiNut();
        txtHoTen.Focus();
    }

    private void btnSua_Click(object? sender, EventArgs e)
    {
        if (_maTVDangChon is null)
        {
            MessageBox.Show("Hãy chọn một thành viên từ danh sách cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        _cheDo = CheDoThaoTac.DangSua;
        KhoaOThongTin(false);
        CapNhatTrangThaiNut();
        txtHoTen.Focus();
        txtHoTen.SelectAll();
    }

    private async void btnLuu_Click(object? sender, EventArgs e)
    {
        if (_cheDo == CheDoThaoTac.Xem)
        {
            MessageBox.Show("Vui lòng chọn thao tác 'Thêm mới' hoặc 'Chỉnh sửa' trước khi lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            var thanhVien = DocDuLieuForm();
            var maTV = await _thanhVienBLL.LuuAsync(thanhVien);
            MessageBox.Show("Đã lưu thông tin thành viên thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _cheDo = CheDoThaoTac.Xem;
            KhoaOThongTin(true);
            await TaiDanhSachAsync(txtTimKiem.Text);
            _maTVDangChon = maTV;
            CapNhatTrangThaiNut();
        }
        catch (Microsoft.Data.SqlClient.SqlException sqlEx) when (sqlEx.Number is 2601 or 2627)
        {
            var msg = sqlEx.Message;
            if (msg.Contains("SDT", StringComparison.OrdinalIgnoreCase) || msg.Contains("UQ__ThanhVie__CA1930A5", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Số điện thoại này đã được sử dụng bởi hội viên khác. Vui lòng nhập số điện thoại khác.", "Trùng lặp dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
            }
            else if (msg.Contains("Email", StringComparison.OrdinalIgnoreCase) || msg.Contains("UQ__ThanhVie__A9D10534", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Địa chỉ email này đã được sử dụng bởi hội viên khác. Vui lòng kiểm tra lại.", "Trùng lặp dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
            }
            else
            {
                MessageBox.Show($"Dữ liệu bị trùng lặp với bản ghi đã có:\n{UiErrorMessage.Get(sqlEx)}", "Trùng lặp dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không thể lưu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi lưu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnXoa_Click(object? sender, EventArgs e)
    {
        if (_maTVDangChon is null)
        {
            MessageBox.Show("Vui lòng chọn một thành viên từ danh sách để ngừng sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (MessageBox.Show("Ngừng sử dụng thành viên này? Dữ liệu lịch sử sẽ được giữ lại.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
        {
            return;
        }

        try
        {
            await _thanhVienBLL.NgungSuDungAsync(_maTVDangChon.Value);
            DatTrangThaiNghiBanDau();
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
        MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
