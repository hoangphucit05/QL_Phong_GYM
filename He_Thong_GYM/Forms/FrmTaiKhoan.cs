using System.Drawing;
using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmTaiKhoan : Form
{
    private readonly DangNhapBLL _bll = new();
    private readonly NhanVienBLL _nhanVienBLL = new();
    private List<TaiKhoan> _danhSachGoc = new();
    private TaiKhoan? _chon;

    public FrmTaiKhoan()
    {
        InitializeComponent();
        txtTenDangNhap.PlaceholderText = "Nhập tên đăng nhập...";
        txtMatKhau.PlaceholderText = "Nhập mật khẩu tối thiểu 8 ký tự...";
    }

    private async void FrmTaiKhoan_Load(object? s, EventArgs e)
    {
        try
        {
            cboVaiTro.DataSource = await _bll.LayVaiTroAsync();
            CapNhatTrangThaiNut();
            await TaiAsync();
            await DatTrangThaiTaoMoiAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CapNhatTrangThaiNut()
    {
        bool coChon = _chon != null;

        UiButtonHelper.SetTrangThai(btnTaoMoi, true, UiButtonHelper.MauThemMoi);
        UiButtonHelper.SetTrangThai(btnLuu, !coChon, UiButtonHelper.MauLuu); // Chỉ kích hoạt khi đang ở chế độ tạo mới
        UiButtonHelper.SetTrangThai(btnCapNhatVaiTro, coChon, UiButtonHelper.MauChucNangPhu); // Kích hoạt khi đã chọn tài khoản
        UiButtonHelper.SetTrangThai(btnDatLaiMatKhau, coChon, UiButtonHelper.MauChinhSua);
        UiButtonHelper.SetTrangThai(btnKhoaMo, coChon, UiButtonHelper.MauXoa);
    }

    private async Task DatTrangThaiTaoMoiAsync()
    {
        _chon = null;
        txtTenDangNhap.Clear();
        txtTenDangNhap.ReadOnly = false;
        txtMatKhau.Clear();
        txtMatKhau.Enabled = true;

        var dsChuaCoTK = await _nhanVienBLL.LayNhanVienChuaCoTaiKhoanAsync();
        cboNhanVien.DataSource = dsChuaCoTK;
        cboNhanVien.DisplayMember = nameof(NhanVien.HienThi);
        cboNhanVien.ValueMember = nameof(NhanVien.MaNV);
        cboNhanVien.Enabled = true;
        if (cboNhanVien.Items.Count > 0) cboNhanVien.SelectedIndex = 0;
        else cboNhanVien.SelectedIndex = -1;

        if (cboVaiTro.Items.Count > 0) cboVaiTro.SelectedIndex = 0;
        cboVaiTro.Enabled = true;

        UiGridHelper.BoChon(dgvTaiKhoan);
        lblTrangThai.Text = dsChuaCoTK.Count > 0
            ? $"Có {dsChuaCoTK.Count} nhân viên chưa có tài khoản. Nhập thông tin để tạo mới."
            : "Tất cả nhân viên đang làm đều đã có tài khoản.";
        CapNhatTrangThaiNut();
        txtTenDangNhap.Focus();
    }

    private async Task TaiAsync()
    {
        _danhSachGoc = await _bll.LayDanhSachTaiKhoanAsync();
        ApDungLoc();
    }

    private void BoLocThayDoi(object? sender, EventArgs e)
    {
        ApDungLoc();
    }

    private void ApDungLoc()
    {
        var tuKhoa = txtTimKiem.Text.Trim();
        var ketQua = _danhSachGoc.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(tuKhoa))
        {
            ketQua = ketQua.Where(x =>
                x.TenDangNhap.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                x.HoTenNhanVien.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                x.VaiTro.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                x.TrangThai.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                x.MaTK.ToString().Contains(tuKhoa, StringComparison.OrdinalIgnoreCase));
        }

        var list = ketQua.ToList();
        UiGridHelper.DatNguon(dgvTaiKhoan, list);
        UiGridHelper.BoChon(dgvTaiKhoan);
        _chon = null;
        CapNhatTrangThaiNut();
        lblTrangThai.Text = string.IsNullOrWhiteSpace(tuKhoa)
            ? $"Hiển thị {list.Count} tài khoản. Nhấn tiêu đề cột để sắp xếp."
            : $"Hiển thị {list.Count} / {_danhSachGoc.Count} tài khoản theo từ khóa: {tuKhoa}";
    }

    private void btnLamMoi_Click(object? sender, EventArgs e)
    {
        txtTimKiem.Clear();
    }

    private void dgvTaiKhoan_SelectionChanged(object? s, EventArgs e)
    {
        if (dgvTaiKhoan.CurrentRow?.DataBoundItem is not TaiKhoan t)
        {
            _chon = null;
            CapNhatTrangThaiNut();
            return;
        }

        _chon = t;
        txtTenDangNhap.Text = t.TenDangNhap;
        txtTenDangNhap.ReadOnly = true;
        txtMatKhau.Clear();
        txtMatKhau.Enabled = false;

        // Hiển thị nhân viên sở hữu tài khoản (khóa không cho chuyển sang nhân viên khác)
        cboNhanVien.DataSource = null;
        cboNhanVien.Items.Clear();
        cboNhanVien.Items.Add(t.HoTenNhanVien);
        cboNhanVien.SelectedIndex = 0;
        cboNhanVien.Enabled = false;

        cboVaiTro.SelectedItem = t.VaiTro;
        cboVaiTro.Enabled = true;

        lblTrangThai.Text = $"Tài khoản: {t.TenDangNhap} | Nhân viên: {t.HoTenNhanVien} | Vai trò: {t.VaiTro} ({t.TrangThai})";
        CapNhatTrangThaiNut();
    }

    private async void btnTaoMoi_Click(object? s, EventArgs e)
    {
        await DatTrangThaiTaoMoiAsync();
    }

    private async void btnLuu_Click(object? s, EventArgs e)
    {
        if (_chon is not null)
        {
            MessageBox.Show("Chỉ tạo mới tài khoản tại nút này. Hãy dùng nút 'Cập nhật vai trò' để phân quyền lại cho tài khoản đã có.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                throw new InvalidOperationException("Vui lòng nhập tên đăng nhập.");
            }
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text) || txtMatKhau.Text.Length < 8)
            {
                throw new InvalidOperationException("Mật khẩu khởi tạo phải có tối thiểu 8 ký tự.");
            }
            if (cboNhanVien.SelectedValue is not int maNV || maNV <= 0)
            {
                throw new InvalidOperationException("Vui lòng chọn nhân viên chưa có tài khoản.");
            }
            if (string.IsNullOrWhiteSpace(cboVaiTro.Text))
            {
                throw new InvalidOperationException("Vui lòng chọn vai trò cho tài khoản.");
            }

            await _bll.TaoTaiKhoanAsync(new TaiKhoan
            {
                TenDangNhap = txtTenDangNhap.Text.Trim(),
                MaNV = maNV,
                VaiTro = cboVaiTro.Text.Trim()
            }, txtMatKhau.Text);

            await TaiAsync();
            await DatTrangThaiTaoMoiAsync();
            MessageBox.Show("Đã tạo tài khoản với mật khẩu hash bảo mật thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không thể lưu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async void btnCapNhatVaiTro_Click(object? s, EventArgs e)
    {
        if (_chon is null)
        {
            MessageBox.Show("Vui lòng chọn tài khoản cần cập nhật vai trò từ danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var vaiTroMoi = cboVaiTro.Text.Trim();
        if (string.IsNullOrWhiteSpace(vaiTroMoi))
        {
            MessageBox.Show("Vui lòng chọn vai trò mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.Equals(vaiTroMoi, _chon.VaiTro, StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show($"Tài khoản này hiện đã có vai trò là '{vaiTroMoi}'. Hãy chọn vai trò khác để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            await _bll.CapNhatVaiTroAsync(_chon.MaTK, vaiTroMoi);
            MessageBox.Show($"Đã cập nhật vai trò tài khoản '{_chon.TenDangNhap}' thành '{vaiTroMoi}' thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await TaiAsync();
            await DatTrangThaiTaoMoiAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi cập nhật vai trò", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnKhoaMo_Click(object? s, EventArgs e)
    {
        if (_chon is null)
        {
            MessageBox.Show("Vui lòng chọn tài khoản cần thay đổi trạng thái từ danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var next = _chon.TrangThai == "Hoạt động" ? "Khóa" : "Hoạt động";
        try
        {
            await _bll.CapNhatTrangThaiTaiKhoanAsync(_chon.MaTK, next);
            await TaiAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnDatLaiMatKhau_Click(object? s, EventArgs e)
    {
        if (_chon is null)
        {
            MessageBox.Show("Vui lòng chọn tài khoản cần đặt lại mật khẩu từ danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (txtMatKhau.Text.Length < 8)
        {
            MessageBox.Show("Nhập mật khẩu mới tối thiểu 8 ký tự vào ô Mật khẩu.", "Yêu cầu mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtMatKhau.Focus();
            return;
        }

        try
        {
            var n = new NguoiDung { MaTK = _chon.MaTK };
            await _bll.DoiMatKhauAsync(n, txtMatKhau.Text);
            txtMatKhau.Clear();
            MessageBox.Show("Đã đặt lại mật khẩu thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
