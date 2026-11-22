using System.Drawing;
using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmVanHanhGoi : Form
{
    private readonly DangKyBLL _dangKyBLL = new();
    private readonly TaiSanBLL _taiSanBLL = new();
    private readonly VanHanhBLL _vanHanhBLL = new();

    public FrmVanHanhGoi()
    {
        InitializeComponent();
        cboThanhVien.SelectedIndexChanged += cboThanhVien_SelectedIndexChanged;
        dgvGoi.SelectionChanged += (s, e) => CapNhatTrangThaiNut();
        btnCheckIn.Click += btnCheckIn_Click;
        btnCheckOut.Click += btnCheckOut_Click;
        btnGiaHan.Click += btnGiaHan_Click;
        btnBaoLuu.Click += btnBaoLuu_Click;
    }

    private async void FrmVanHanhGoi_Load(object? sender, EventArgs e)
    {
        try
        {
            cboThanhVien.DataSource = await _dangKyBLL.LayThanhVienAsync();
            cboThanhVien.DisplayMember = nameof(ThanhVien.HienThi);
            cboThanhVien.ValueMember = nameof(ThanhVien.MaTV);
            // Chỉ cho chọn phòng đang hoạt động; procedure vẫn kiểm tra lại ở database.
            cboPhong.DataSource = (await _taiSanBLL.LayPhongAsync())
                .Where(x => string.Equals(x.TrangThai, "Hoạt động", StringComparison.OrdinalIgnoreCase))
                .ToList();
            cboPhong.DisplayMember = nameof(PhongTapModel.HienThi);
            cboPhong.ValueMember = "MaPhong";
            await TaiGoiAsync();
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private void CapNhatTrangThaiNut()
    {
        bool coThanhVien = cboThanhVien.SelectedItem is ThanhVien;
        bool coGoiChon = dgvGoi.CurrentRow?.DataBoundItem is DangKyGoiView;

        UiButtonHelper.SetTrangThai(btnCheckIn, coThanhVien, Color.FromArgb(5, 150, 105));
        UiButtonHelper.SetTrangThai(btnCheckOut, coThanhVien, Color.FromArgb(30, 64, 175));
        UiButtonHelper.SetTrangThai(btnGiaHan, coGoiChon, Color.FromArgb(16, 185, 129));
        UiButtonHelper.SetTrangThai(btnBaoLuu, coGoiChon, Color.FromArgb(124, 58, 237));
    }

    private async Task TaiGoiAsync()
    {
        if (cboThanhVien.SelectedItem is not ThanhVien thanhVien)
        {
            dgvGoi.DataSource = null;
            CapNhatTrangThaiNut();
            return;
        }

        dgvGoi.DataSource = await _vanHanhBLL.LayGoiThanhVienAsync(thanhVien.MaTV);
        DinhDangLuoiGoi();
        CapNhatTrangThaiNut();
    }

    private void DinhDangLuoiGoi()
    {
        if (dgvGoi.Columns.Contains(nameof(DangKyGoiView.MaDKG))) dgvGoi.Columns[nameof(DangKyGoiView.MaDKG)]!.Visible = false;
        if (dgvGoi.Columns.Contains(nameof(DangKyGoiView.HienThi))) dgvGoi.Columns[nameof(DangKyGoiView.HienThi)]!.Visible = false;
        if (dgvGoi.Columns.Contains(nameof(DangKyGoiView.MaDangKyGoi)))
        {
            dgvGoi.Columns[nameof(DangKyGoiView.MaDangKyGoi)]!.HeaderText = "Mã đăng ký";
            dgvGoi.Columns[nameof(DangKyGoiView.MaDangKyGoi)]!.Width = 155;
        }
    }

    private async void cboThanhVien_SelectedIndexChanged(object? sender, EventArgs e)
    {
        try { await TaiGoiAsync(); } catch (Exception ex) { HienThiLoi(ex); }
    }

    private async void btnCheckIn_Click(object? sender, EventArgs e)
    {
        try
        {
            if (cboThanhVien.SelectedItem is not ThanhVien tv) throw new InvalidOperationException("Hãy chọn thành viên.");
            var maPhong = cboPhong.SelectedValue is int value ? value : (int?)null;
            var maCheckIn = await _vanHanhBLL.CheckInAsync(tv.MaTV, maPhong);
            lblTrangThai.Text = $"Check-in thành công, mã phiên: {maCheckIn}.";
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private async void btnCheckOut_Click(object? sender, EventArgs e)
    {
        try
        {
            if (cboThanhVien.SelectedItem is not ThanhVien tv) throw new InvalidOperationException("Hãy chọn thành viên.");
            await _vanHanhBLL.CheckOutAsync(tv.MaTV);
            lblTrangThai.Text = "Check-out thành công.";
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private async void btnGiaHan_Click(object? sender, EventArgs e)
    {
        try
        {
            if (dgvGoi.CurrentRow?.DataBoundItem is not DangKyGoiView goi) throw new InvalidOperationException("Hãy chọn một gói đã đăng ký.");
            var maHD = await _vanHanhBLL.GiaHanAsync(goi.MaDKG, (int)nudSoNgay.Value, SessionManager.NguoiDungHienTai?.MaNV);
            await TaiGoiAsync();
            lblTrangThai.Text = $"Đã gia hạn gói tập. Hóa đơn #{maHD} đã được tạo và đang chờ thanh toán.";
            MessageBox.Show($"Đã gia hạn gói tập thành công!\nHóa đơn #{maHD} đã được tạo và chuyển sang trạng thái Chưa thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private async void btnBaoLuu_Click(object? sender, EventArgs e)
    {
        try
        {
            if (dgvGoi.CurrentRow?.DataBoundItem is not DangKyGoiView goi) throw new InvalidOperationException("Hãy chọn một gói đã đăng ký.");
            await _vanHanhBLL.BaoLuuAsync(goi.MaDKG, dtpTuNgay.Value, dtpDenNgay.Value, txtLyDo.Text, SessionManager.NguoiDungHienTai?.MaNV);
            await TaiGoiAsync();
            lblTrangThai.Text = "Đã bảo lưu gói tập.";
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private void HienThiLoi(Exception ex)
    {
        var message = UiErrorMessage.Get(ex);
        lblTrangThai.Text = message;
        MessageBox.Show(message, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
