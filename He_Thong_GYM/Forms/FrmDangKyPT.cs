using System.Drawing;
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
        UiButtonHelper.SetTrangThai(btnDiemDanhPT, false, Color.FromArgb(5, 150, 105));
        UiButtonHelper.SetTrangThai(btnXemLichSuBuoi, false, Color.FromArgb(79, 70, 229));
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
            CapNhatNutThaoTacPT();
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
        UiGridHelper.BoChon(dgvLichSu);
        CapNhatNutThaoTacPT();

        lblThongKeLichSu.Text = $"Hiển thị: {list.Count} / {_lichSuGoc.Count} lượt";
    }

    private void DinhDangLuoiLichSu()
    {
        if (dgvLichSu.Columns.Count == 0) return;

        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.MaDangKy)))
            dgvLichSu.Columns[nameof(LichSuDangKy.MaDangKy)]!.Visible = false;
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.HienThi)))
            dgvLichSu.Columns[nameof(LichSuDangKy.HienThi)]!.Visible = false;
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.SoBuoi)))
            dgvLichSu.Columns[nameof(LichSuDangKy.SoBuoi)]!.Visible = false;
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.SoBuoiDaTap)))
            dgvLichSu.Columns[nameof(LichSuDangKy.SoBuoiDaTap)]!.Visible = false;
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.SoBuoiConLai)))
            dgvLichSu.Columns[nameof(LichSuDangKy.SoBuoiConLai)]!.Visible = false;

        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.MaDangKyHienThi)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.MaDangKyHienThi)]!.HeaderText = "Mã đăng ký PT";
            dgvLichSu.Columns[nameof(LichSuDangKy.MaDangKyHienThi)]!.Width = 150;
        }
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.ThanhVien)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.ThanhVien)]!.HeaderText = "Thành viên";
            dgvLichSu.Columns[nameof(LichSuDangKy.ThanhVien)]!.FillWeight = 140;
        }
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.DichVu)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.DichVu)]!.HeaderText = "Huấn luyện viên";
            dgvLichSu.Columns[nameof(LichSuDangKy.DichVu)]!.FillWeight = 130;
        }
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.TienDo)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.TienDo)]!.HeaderText = "Tiến độ (Đã tập/Tổng)";
            dgvLichSu.Columns[nameof(LichSuDangKy.TienDo)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLichSu.Columns[nameof(LichSuDangKy.TienDo)]!.Width = 145;
        }
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.NgayBatDau)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.NgayBatDau)]!.HeaderText = "Ngày bắt đầu";
            dgvLichSu.Columns[nameof(LichSuDangKy.NgayBatDau)]!.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLichSu.Columns[nameof(LichSuDangKy.NgayBatDau)]!.Width = 105;
        }
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.NgayKetThuc)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.NgayKetThuc)]!.HeaderText = "Ngày kết thúc";
            dgvLichSu.Columns[nameof(LichSuDangKy.NgayKetThuc)]!.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLichSu.Columns[nameof(LichSuDangKy.NgayKetThuc)]!.Width = 105;
        }
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.TongTien)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.TongTien)]!.HeaderText = "Tổng tiền (VNĐ)";
            dgvLichSu.Columns[nameof(LichSuDangKy.TongTien)]!.DefaultCellStyle.Format = "N0";
            dgvLichSu.Columns[nameof(LichSuDangKy.TongTien)]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLichSu.Columns[nameof(LichSuDangKy.TongTien)]!.Width = 125;
        }
        if (dgvLichSu.Columns.Contains(nameof(LichSuDangKy.TrangThai)))
        {
            dgvLichSu.Columns[nameof(LichSuDangKy.TrangThai)]!.HeaderText = "Trạng thái";
            dgvLichSu.Columns[nameof(LichSuDangKy.TrangThai)]!.Width = 110;
        }
    }

    private void dgvLichSu_SelectionChanged(object? sender, EventArgs e)
    {
        if (_dangKhoiTao) return;
        CapNhatNutThaoTacPT();
    }

    private void CapNhatNutThaoTacPT()
    {
        var item = dgvLichSu.CurrentRow?.DataBoundItem as LichSuDangKy;
        bool coTheDiemDanh = item is not null &&
            string.Equals(item.TrangThai, "Đang học", StringComparison.OrdinalIgnoreCase) &&
            item.SoBuoiConLai > 0;
        bool coTheXemLichSu = item is not null && item.SoBuoiDaTap > 0;

        UiButtonHelper.SetTrangThai(btnDiemDanhPT, coTheDiemDanh, Color.FromArgb(5, 150, 105));
        UiButtonHelper.SetTrangThai(btnXemLichSuBuoi, coTheXemLichSu, Color.FromArgb(79, 70, 229));
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

    private async void btnDiemDanhPT_Click(object? sender, EventArgs e)
    {
        if (dgvLichSu.CurrentRow?.DataBoundItem is not LichSuDangKy item)
        {
            MessageBox.Show("Vui lòng chọn một hợp đồng PT từ danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (!string.Equals(item.TrangThai, "Đang học", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show($"Hợp đồng này đang ở trạng thái '{item.TrangThai}', không thể điểm danh.", "Không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (item.SoBuoiConLai <= 0)
        {
            MessageBox.Show("Hợp đồng này đã hoàn thành đủ số buổi tập.", "Đã hoàn thành", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dlg = new Form
        {
            Text = "Xác nhận điểm danh buổi tập PT",
            Width = 460,
            Height = 280,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            MaximizeBox = false,
            MinimizeBox = false,
            BackColor = Color.White
        };

        var lblInfo = new Label
        {
            Text = $"Hội viên: {item.ThanhVien}\n" +
                   $"Huấn luyện viên: {item.DichVu}\n" +
                   $"Tiến độ: Buổi {item.SoBuoiDaTap + 1} / Tổng {item.SoBuoi} buổi (Còn lại: {item.SoBuoiConLai} buổi)",
            Left = 20,
            Top = 15,
            Width = 405,
            Height = 65,
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
            ForeColor = Color.FromArgb(30, 41, 59)
        };

        var lblNhanXet = new Label
        {
            Text = "Nhận xét / Đánh giá buổi tập (không bắt buộc):",
            Left = 20,
            Top = 85,
            AutoSize = true,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            ForeColor = Color.FromArgb(71, 85, 105)
        };

        var txtNhanXet = new TextBox
        {
            Left = 20,
            Top = 110,
            Width = 405,
            Height = 60,
            Multiline = true,
            Font = new Font("Segoe UI", 9F),
            PlaceholderText = "Ví dụ: Hoàn thành tốt bài tập ngực và tay sau..."
        };

        var btnXacNhan = new Button
        {
            Text = "Xác nhận điểm danh",
            Left = 175,
            Top = 185,
            Width = 150,
            Height = 36,
            BackColor = Color.FromArgb(5, 150, 105),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            DialogResult = DialogResult.OK
        };
        btnXacNhan.FlatAppearance.BorderSize = 0;

        var btnHuy = new Button
        {
            Text = "Hủy",
            Left = 335,
            Top = 185,
            Width = 90,
            Height = 36,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9F),
            DialogResult = DialogResult.Cancel
        };

        dlg.Controls.AddRange(new Control[] { lblInfo, lblNhanXet, txtNhanXet, btnXacNhan, btnHuy });
        dlg.AcceptButton = btnXacNhan;
        dlg.CancelButton = btnHuy;

        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var kq = await _bll.HoanThanhBuoiPTAsync(item.MaDangKy, txtNhanXet.Text.Trim(), SessionManager.NguoiDungHienTai?.MaNV);
                await TaiLichSuAsync();

                string thongBao = kq.SoBuoiConLai == 0
                    ? $"Đã hoàn thành buổi tập thứ {kq.SoBuoiDaTap}!\nHợp đồng PT này đã hoàn thành toàn bộ {kq.SoBuoiDaTap} buổi tập."
                    : $"Đã điểm danh buổi tập thứ {kq.SoBuoiDaTap} thành công!\nSố buổi còn lại: {kq.SoBuoiConLai} buổi.";

                MessageBox.Show(thongBao, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(UiErrorMessage.Get(ex), "Không thể điểm danh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    private async void btnXemLichSuBuoi_Click(object? sender, EventArgs e)
    {
        if (dgvLichSu.CurrentRow?.DataBoundItem is not LichSuDangKy item)
        {
            MessageBox.Show("Vui lòng chọn một hợp đồng PT để xem lịch sử.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            var danhSach = await _bll.LayLichSuBuoiTapPTAsync(item.MaDangKy);
            if (danhSach.Count == 0)
            {
                MessageBox.Show("Hợp đồng này chưa có buổi tập nào được điểm danh.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dlg = new Form
            {
                Text = $"Lịch sử buổi tập - Hợp đồng: {item.MaDangKyHienThi} ({item.ThanhVien} - {item.DichVu})",
                Width = 720,
                Height = 420,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.White
            };

            var dgvBuoi = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeight = 32,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            var pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45,
                BackColor = Color.FromArgb(241, 245, 249),
                Padding = new Padding(12, 10, 12, 10)
            };
            var lblTieuDe = new Label
            {
                Text = $"Tổng số buổi đã tập: {danhSach.Count} / {item.SoBuoi} buổi",
                Dock = DockStyle.Left,
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59)
            };
            pnlTop.Controls.Add(lblTieuDe);

            dlg.Controls.Add(dgvBuoi);
            dlg.Controls.Add(pnlTop);

            UiGridHelper.DatNguon(dgvBuoi, danhSach);
            if (dgvBuoi.Columns.Contains(nameof(BuoiTapPTModel.MaLichPT)))
                dgvBuoi.Columns[nameof(BuoiTapPTModel.MaLichPT)]!.HeaderText = "Mã buổi";
            if (dgvBuoi.Columns.Contains(nameof(BuoiTapPTModel.ThoiGianBatDau)))
            {
                dgvBuoi.Columns[nameof(BuoiTapPTModel.ThoiGianBatDau)]!.HeaderText = "Bắt đầu";
                dgvBuoi.Columns[nameof(BuoiTapPTModel.ThoiGianBatDau)]!.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dgvBuoi.Columns.Contains(nameof(BuoiTapPTModel.ThoiGianKetThuc)))
            {
                dgvBuoi.Columns[nameof(BuoiTapPTModel.ThoiGianKetThuc)]!.HeaderText = "Kết thúc";
                dgvBuoi.Columns[nameof(BuoiTapPTModel.ThoiGianKetThuc)]!.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dgvBuoi.Columns.Contains(nameof(BuoiTapPTModel.TrangThai)))
                dgvBuoi.Columns[nameof(BuoiTapPTModel.TrangThai)]!.HeaderText = "Trạng thái";
            if (dgvBuoi.Columns.Contains(nameof(BuoiTapPTModel.NhanXet)))
                dgvBuoi.Columns[nameof(BuoiTapPTModel.NhanXet)]!.HeaderText = "Nhận xét / Đánh giá";
            if (dgvBuoi.Columns.Contains(nameof(BuoiTapPTModel.NgayTao)))
            {
                dgvBuoi.Columns[nameof(BuoiTapPTModel.NgayTao)]!.HeaderText = "Thời điểm ghi";
                dgvBuoi.Columns[nameof(BuoiTapPTModel.NgayTao)]!.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            dlg.ShowDialog(this);
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Không thể tải lịch sử buổi tập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void GiaThayDoi(object? s, EventArgs e) => TinhGia();
}
