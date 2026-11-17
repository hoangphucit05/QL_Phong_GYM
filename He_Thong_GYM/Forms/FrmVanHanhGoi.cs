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
            cboThanhVien.DisplayMember = nameof(ThanhVien.HoTen);
            cboThanhVien.ValueMember = nameof(ThanhVien.MaTV);
            cboPhong.DataSource = await _taiSanBLL.LayPhongAsync();
            cboPhong.DisplayMember = "TenPhong";
            cboPhong.ValueMember = "MaPhong";
            await TaiGoiAsync();
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private async Task TaiGoiAsync()
    {
        if (cboThanhVien.SelectedItem is not ThanhVien thanhVien) return;
        dgvGoi.DataSource = await _vanHanhBLL.LayGoiThanhVienAsync(thanhVien.MaTV);
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
            lblTrangThai.Text = $"Check-in thành công, mã lượt: {maCheckIn}.";
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
            await _vanHanhBLL.GiaHanAsync(goi.MaDKG, (int)nudSoNgay.Value, SessionManager.NguoiDungHienTai?.MaNV);
            await TaiGoiAsync(); lblTrangThai.Text = "Đã gia hạn gói tập.";
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private async void btnBaoLuu_Click(object? sender, EventArgs e)
    {
        try
        {
            if (dgvGoi.CurrentRow?.DataBoundItem is not DangKyGoiView goi) throw new InvalidOperationException("Hãy chọn một gói đã đăng ký.");
            await _vanHanhBLL.BaoLuuAsync(goi.MaDKG, dtpTuNgay.Value, dtpDenNgay.Value, txtLyDo.Text, SessionManager.NguoiDungHienTai?.MaNV);
            await TaiGoiAsync(); lblTrangThai.Text = "Đã bảo lưu gói tập.";
        }
        catch (Exception ex) { HienThiLoi(ex); }
    }

    private void HienThiLoi(Exception ex) { lblTrangThai.Text = ex.Message; MessageBox.Show(ex.Message, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
}
