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
            cboNhanVien.DataSource = await _nhanVienBLL.LayNhanVienDangLamAsync();
            cboNhanVien.DisplayMember = nameof(NhanVien.HienThi);
            cboNhanVien.ValueMember = nameof(NhanVien.MaNV);
            cboVaiTro.DataSource = await _bll.LayVaiTroAsync();
            CapNhatTrangThaiNut();
            await TaiAsync();
            DatTrangThaiTaoMoi();
            BeginInvoke(new Action(DatTrangThaiTaoMoi));
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
        UiButtonHelper.SetTrangThai(btnDatLaiMatKhau, coChon, UiButtonHelper.MauChinhSua);
        UiButtonHelper.SetTrangThai(btnKhoaMo, coChon, UiButtonHelper.MauXoa);
    }

    private void DatTrangThaiTaoMoi()
    {
        _chon = null;
        txtTenDangNhap.Clear();
        txtMatKhau.Clear();
        if (cboNhanVien.Items.Count > 0) cboNhanVien.SelectedIndex = -1;
        if (cboVaiTro.Items.Count > 0) cboVaiTro.SelectedIndex = -1;
        UiGridHelper.BoChon(dgvTaiKhoan);
        lblTrangThai.Text = "Nhập thông tin để tạo tài khoản mới.";
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
        cboNhanVien.SelectedValue = t.MaNV;
        cboVaiTro.SelectedItem = t.VaiTro;
        lblTrangThai.Text = $"Tài khoản đang chọn: {t.TenDangNhap} - {t.TrangThai}";
        CapNhatTrangThaiNut();
    }

    private void btnTaoMoi_Click(object? s, EventArgs e)
    {
        DatTrangThaiTaoMoi();
    }

    private async void btnLuu_Click(object? s, EventArgs e)
    {
        if (_chon is not null)
        {
            MessageBox.Show("Chỉ tạo mới tài khoản tại màn hình này. Dùng nút 'Đặt lại mật khẩu' hoặc 'Khóa / mở khóa' cho tài khoản đã có.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            await _bll.TaoTaiKhoanAsync(new TaiKhoan
            {
                TenDangNhap = txtTenDangNhap.Text.Trim(),
                MaNV = cboNhanVien.SelectedValue is int maNV ? maNV : 0,
                VaiTro = cboVaiTro.Text
            }, txtMatKhau.Text);

            await TaiAsync();
            DatTrangThaiTaoMoi();
            MessageBox.Show("Đã tạo tài khoản với mật khẩu hash bảo mật thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không thể lưu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
