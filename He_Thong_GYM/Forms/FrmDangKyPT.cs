using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmDangKyPT : Form
{
    private readonly DangKyBLL _bll = new();
    private List<LichSuDangKy> _lichSuGoc = new();
    private bool _dangKhoiTao = true;

    public FrmDangKyPT()
    {
        InitializeComponent();
        UiButtonHelper.SetTrangThai(btnDangKy, false, UiButtonHelper.MauThemMoi);
        cboThanhVien.SelectedIndexChanged += (s, e) => CapNhatTrangThaiNut();
    }

    private async void FrmDangKyPT_Load(object? s, EventArgs e)
    {
        try
        {
            _dangKhoiTao = true;
            cboThanhVien.DataSource = await _bll.LayThanhVienAsync();
            cboThanhVien.DisplayMember = nameof(ThanhVien.HienThi);
            cboThanhVien.ValueMember = nameof(ThanhVien.MaTV);
            cboThanhVien.SelectedIndex = -1;

            cboHLV.DataSource = await _bll.LayHLVAsync();
            cboHLV.DisplayMember = nameof(HuanLuyenVien.HoTen);
            cboHLV.ValueMember = nameof(HuanLuyenVien.MaHLV);
            cboHLV.SelectedIndex = -1;

            cboKhuyenMai.DataSource = await _bll.LayKhuyenMaiAsync();
            cboKhuyenMai.DisplayMember = nameof(KhuyenMai.HienThi);
            cboKhuyenMai.ValueMember = nameof(KhuyenMai.MaKM);
            cboKhuyenMai.SelectedIndex = -1;

            cboLocTrangThai.SelectedIndex = 0;
            TinhGia();

            await TaiLichSuAsync();
            CapNhatTrangThaiNut();
            _dangKhoiTao = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task TaiLichSuAsync()
    {
        try
        {
            _lichSuGoc = await _bll.LayLichSuPTAsync();
            ApDungLocLichSu();
        }
        catch (Exception ex)
        {
            lblThongKeLichSu.Text = "Lỗi tải lịch sử";
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LocLichSuThayDoi(object? sender, EventArgs e)
    {
        if (_dangKhoiTao) return;
        ApDungLocLichSu();
    }

    private void ApDungLocLichSu()
    {
        var ketQua = _lichSuGoc.AsEnumerable();

        var tuKhoa = txtTimKiemLichSu.Text.Trim();
        if (!string.IsNullOrEmpty(tuKhoa))
        {
            ketQua = ketQua.Where(x =>
                (!string.IsNullOrEmpty(x.ThanhVien) && x.ThanhVien.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(x.DichVu) && x.DichVu.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(x.MaDangKyHienThi) && x.MaDangKyHienThi.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)) ||
                x.MaDangKy.ToString().Contains(tuKhoa));
        }

        if (cboLocTrangThai.SelectedIndex > 0)
        {
            var trangThaiChon = cboLocTrangThai.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(trangThaiChon))
            {
                ketQua = ketQua.Where(x => string.Equals(x.TrangThai, trangThaiChon, StringComparison.OrdinalIgnoreCase));
            }
        }

        var list = ketQua.ToList();
        UiGridHelper.DatNguon(dgvLichSu, list);
        DinhDangLuoiLichSu();

        lblThongKeLichSu.Text = $"Hiển thị: {list.Count} / {_lichSuGoc.Count} lượt";
    }

    private void DinhDangLuoiLichSu()
    {
        if (dgvLichSu.Columns.Count == 0) return;

        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.MaDangKy)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.MaDangKy)]!.Visible = false;
        }
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.MaDangKyHienThi)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.MaDangKyHienThi)]!.HeaderText = "Mã đăng ký PT";
            dgvLichSu.Columns[nameof(LichSuDangKy.MaDangKyHienThi)]!.Width = 155;
        }
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.HienThi)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.HienThi)]!.Visible = false;
        }
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.ThanhVien)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.ThanhVien)]!.HeaderText = "Thành viên";
            dgvLichSu.Columns[nameof(LichSuDangKy.ThanhVien)]!.FillWeight = 150;
        }
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.DichVu)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.DichVu)]!.HeaderText = "Huấn luyện viên";
            dgvLichSu.Columns[nameof(LichSuDangKy.DichVu)]!.FillWeight = 140;
        }
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.NgayBatDau)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.NgayBatDau)]!.HeaderText = "Ngày bắt đầu";
            dgvLichSu.Columns[nameof(LichSuDangKy.NgayBatDau)]!.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLichSu.Columns[nameof(LichSuDangKy.NgayBatDau)]!.Width = 110;
        }
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.NgayKetThuc)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.NgayKetThuc)]!.HeaderText = "Ngày kết thúc";
            dgvLichSu.Columns[nameof(LichSuDangKy.NgayKetThuc)]!.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLichSu.Columns[nameof(LichSuDangKy.NgayKetThuc)]!.Width = 110;
        }
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.TongTien)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.TongTien)]!.HeaderText = "Tổng tiền (VNĐ)";
            dgvLichSu.Columns[nameof(LichSuDangKy.TongTien)]!.DefaultCellStyle.Format = "N0";
            dgvLichSu.Columns[nameof(LichSuDangKy.TongTien)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLichSu.Columns[nameof(LichSuDangKy.TongTien)]!.Width = 130;
        }
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.TrangThai)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.TrangThai)]!.HeaderText = "Trạng thái";
            dgvLichSu.Columns[nameof(LichSuDangKy.TrangThai)]!.Width = 120;
        }
    }

    private async void btnLamMoiLichSu_Click(object? sender, EventArgs e)
    {
        txtTimKiemLichSu.Clear();
        if (cboLocTrangThai.Items.Count > 0) cboLocTrangThai.SelectedIndex = 0;
        await TaiLichSuAsync();
    }

    private void TinhGia()
    {
        if (cboHLV.SelectedItem is not HuanLuyenVien h)
        {
            lblTongTien.Text = "Chưa chọn HLV";
            CapNhatTrangThaiNut();
            return;
        }
        var km = cboKhuyenMai.SelectedItem as KhuyenMai;
        dtpKetThuc.Value = dtpBatDau.Value.AddDays((int)nudSoBuoi.Value * 7);
        lblTongTien.Text = $"{(h.PhiThue ?? 0) * nudSoBuoi.Value * (1 - (km?.PhanTramGiam ?? 0) / 100):N0} đ";
        CapNhatTrangThaiNut();
    }

    private void CapNhatTrangThaiNut()
    {
        bool sanSang = cboThanhVien.SelectedItem is ThanhVien && cboHLV.SelectedItem is HuanLuyenVien;
        UiButtonHelper.SetTrangThai(btnDangKy, sanSang, UiButtonHelper.MauThemMoi);
    }

    private async void btnDangKy_Click(object? s, EventArgs e)
    {
        try
        {
            if (SessionManager.NguoiDungHienTai is null) throw new InvalidOperationException("Chưa có phiên đăng nhập.");
            if (cboThanhVien.SelectedItem is not ThanhVien tv || cboHLV.SelectedItem is not HuanLuyenVien h)
                throw new InvalidOperationException("Hãy chọn thành viên và huấn luyện viên.");

            var km = cboKhuyenMai.SelectedItem as KhuyenMai;
            var kq = await _bll.DangKyPTAsync(tv.MaTV, h.MaHLV, (int)nudSoBuoi.Value, km?.MaKM, dtpBatDau.Value, dtpKetThuc.Value, SessionManager.NguoiDungHienTai.MaNV);
            await TaiLichSuAsync();

            var hoiThanhToan = MessageBox.Show(
                $"Đã đăng ký hợp đồng PT thành công!\nHóa đơn #{kq.MaHD} đang chờ thanh toán.\nBạn có muốn mở màn hình thu tiền ngay bây giờ không?",
                "Đăng ký thành công",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (hoiThanhToan == DialogResult.Yes)
            {
                using var frmTT = new FrmThanhToan(kq.MaHD);
                if (frmTT.ShowDialog(this) == DialogResult.OK)
                {
                    await TaiLichSuAsync();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không thể đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void GiaThayDoi(object? s, EventArgs e) => TinhGia();
}
