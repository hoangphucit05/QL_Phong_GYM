using He_Thong_GYM.BLL;
using He_Thong_GYM.Helpers;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmTongQuan : Form
{
    private readonly DashboardBLL _dashboardBLL = new();
    private readonly Dictionary<string, Label> _kpiDashboard = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, Label> _kpiSubDashboard = new(StringComparer.OrdinalIgnoreCase);
    private DashboardBarPanel? _chartDoanhThu;
    private DashboardDonutPanel? _chartDonut;
    private DashboardBarPanel? _chartTrangThai;
    private bool _dangKhoiTao = true;

    /// <summary>
    /// Cho phép Form1 hoặc màn hình cha đăng ký chuyển form khi người dùng nhấn đúp vào danh sách.
    /// </summary>
    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public Action<Form>? YeuCauMoForm { get; set; }

    public FrmTongQuan()
    {
        InitializeComponent();
        DinhDangLuoi();
        KhoiTaoBieuDo();
        KhoiTaoTheKpi();
    }

    private void DinhDangLuoi()
    {
        DinhDangBang(dgvGoiSapHetHan);
        DinhDangBang(dgvCongNo);
        DinhDangBang(dgvHoatDong);
        DinhDangBang(dgvGoiBanChay);

        colGoiHetHan.DefaultCellStyle.Format = "dd/MM/yyyy";
        colCNTongTien.DefaultCellStyle.Format = "N0";
        colCNDaThu.DefaultCellStyle.Format = "N0";
        colCNConNo.DefaultCellStyle.Format = "N0";
        colHDThoiGian.DefaultCellStyle.Format = "dd/MM HH:mm";
        colBCSoLuong.DefaultCellStyle.Format = "N0";
        colBCDoanhThu.DefaultCellStyle.Format = "N0";
    }

    private static void DinhDangBang(DataGridView dgv)
    {
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
        dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
        dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
        dgv.DefaultCellStyle.Padding = new Padding(2, 0, 2, 0);
    }

    private void KhoiTaoBieuDo()
    {
        _chartDoanhThu = new DashboardBarPanel { Dock = DockStyle.Fill };
        _chartDonut = new DashboardDonutPanel { Dock = DockStyle.Fill };
        _chartTrangThai = new DashboardBarPanel { Dock = DockStyle.Fill };

        boxDoanhThu.Controls.Add(_chartDoanhThu);
        boxDonut.Controls.Add(_chartDonut);
        boxTrangThai.Controls.Add(_chartTrangThai);
    }

    private void KhoiTaoTheKpi()
    {
        var cards = new[]
        {
            ("👥", "Tổng hội viên", "TongThanhVien", "TV_Sub", Color.FromArgb(37, 99, 235)),
            ("✅", "Hội viên hoạt động", "ThanhVienHoatDong", "TVHD_Sub", Color.FromArgb(5, 150, 105)),
            ("⏰", "Gói sắp hết hạn", "GoiSapHetHan", "GSH_Sub", Color.FromArgb(217, 119, 6)),
            ("📦", "Gói đang sử dụng", "GoiDangSuDung", "GSD_Sub", Color.FromArgb(124, 58, 237)),
            ("💰", "Doanh thu trong kỳ", "DoanhThuTrongKy", "DT_Sub", Color.FromArgb(5, 150, 105)),
            ("📋", "Tổng công nợ", "TongCongNo", "CN_Sub", Color.FromArgb(220, 38, 38)),
            ("🏃", "Đang có mặt", "DangCoMat", "CM_Sub", Color.FromArgb(14, 116, 144)),
            ("🏋️", "Buổi PT trong kỳ", "BuoiPTTrongKy", "PT_Sub", Color.FromArgb(190, 24, 93))
        };

        for (var i = 0; i < cards.Length; i++)
        {
            var (icon, tieuDe, khoa, khoaPhu, mau) = cards[i];
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(4, 4, 8, 4),
                Padding = new Padding(14, 8, 8, 6)
            };

            var vach = new Panel { Dock = DockStyle.Left, Width = 5, BackColor = mau };
            var iconLabel = new Label { AutoSize = false, Size = new Size(38, 26), Text = icon, Font = new Font("Segoe UI", 12F), Location = new Point(12, 6) };
            var label = new Label { AutoSize = true, Text = tieuDe.ToUpperInvariant(), Font = new Font("Segoe UI", 7.5F, FontStyle.Bold), ForeColor = Color.FromArgb(100, 116, 139), Location = new Point(52, 12) };
            var giaTri = new Label { AutoSize = true, Text = "--", Font = new Font("Segoe UI", 18F, FontStyle.Bold), ForeColor = mau, Location = new Point(14, 34) };
            var phuDe = new Label { AutoSize = true, Text = "", Font = new Font("Segoe UI", 7.5F), ForeColor = Color.FromArgb(100, 116, 139), Location = new Point(14, 64) };

            _kpiDashboard[khoa] = giaTri;
            _kpiSubDashboard[khoaPhu] = phuDe;

            panel.Controls.Add(phuDe);
            panel.Controls.Add(giaTri);
            panel.Controls.Add(label);
            panel.Controls.Add(iconLabel);
            panel.Controls.Add(vach);

            tableKpi.Controls.Add(panel, i % 4, i / 4);
        }
    }

    private async void FrmTongQuan_Load(object? sender, EventArgs e)
    {
        _dangKhoiTao = true;
        cboKhoang.SelectedIndex = 0;
        CapNhatNgayTheoLuaChon();
        _dangKhoiTao = false;

        await TaiDuLieuAsync();
    }

    public async Task TaiDuLieuAsync()
    {
        try
        {
            Cursor = Cursors.WaitCursor;
            var (tuNgay, denNgay) = LayKhoangDashboard();
            var data = await _dashboardBLL.LayTongQuanAsync(tuNgay, denNgay);
            HienThiSoLieu(data, tuNgay, denNgay);
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi tải Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private void HienThiSoLieu(TongQuanDashboard data, DateTime tuNgay, DateTime denNgay)
    {
        var coQuyenTaiChinh = !SessionManager.CoVaiTro("HLV", "Huấn luyện viên");

        GanKpi("TongThanhVien", data.TongThanhVien.ToString("N0"));
        GanKpi("ThanhVienHoatDong", data.ThanhVienHoatDong.ToString("N0"));
        GanKpi("GoiSapHetHan", data.GoiSapHetHan.ToString("N0"));
        GanKpi("GoiDangSuDung", data.GoiDangSuDung.ToString("N0"));
        GanKpi("DoanhThuTrongKy", coQuyenTaiChinh ? $"{data.DoanhThuTrongKy:N0} đ" : "Ẩn");
        GanKpi("TongCongNo", coQuyenTaiChinh ? $"{data.TongCongNo:N0} đ" : "Ẩn");
        GanKpi("DangCoMat", data.DangCoMat.ToString("N0"));
        GanKpi("BuoiPTTrongKy", data.BuoiPTTrongKy.ToString("N0"));

        GanKpiSub("TV_Sub", $"Mới: {data.ThanhVienMoi:N0} • Hết hạn: {data.ThanhVienHetHan:N0}");
        GanKpiSub("TVHD_Sub", $"Bảo lưu: {data.ThanhVienBaoLuu:N0}");
        GanKpiSub("GSH_Sub", $"Hết hạn: {data.GoiHetHan:N0} • Bảo lưu: {data.GoiBaoLuu:N0}");
        GanKpiSub("GSD_Sub", $"Gói tập đang bán: {data.TongGoiTapDangBan:N0}");
        GanKpiSub("DT_Sub", coQuyenTaiChinh ? $"Hôm nay: {data.DoanhThuHomNay:N0} đ • Tháng: {data.DoanhThuThangNay:N0} đ" : "");
        GanKpiSub("CN_Sub", coQuyenTaiChinh ? $"{data.CongNoChiTiet.Count:N0} hóa đơn chưa thu đủ" : "");
        GanKpiSub("CM_Sub", $"Check-in: {data.CheckInTrongKy:N0} • Cao điểm: {(data.GioCaoDiem >= 0 ? $"{data.GioCaoDiem:00}:00" : "--")}");
        GanKpiSub("PT_Sub", $"Hoàn thành: {data.BuoiPTDaHoanThanh:N0} • Chờ: {data.BuoiPTChuaThucHien:N0}");

        lblMoTa.Text = $"Dữ liệu từ {tuNgay:dd/MM/yyyy} đến {denNgay:dd/MM/yyyy} • Cập nhật lúc {DateTime.Now:dd/MM/yyyy HH:mm}";

        if (_chartDoanhThu is not null)
        {
            _chartDoanhThu.SetData(data.DoanhThuTheoNgay.Select(x => ($"{x.Ngay:dd/MM}", x.DoanhThu, Color.FromArgb(37, 99, 235))));
        }

        if (_chartDonut is not null)
        {
            var colors = new[]
            {
                Color.FromArgb(5, 150, 105),
                Color.FromArgb(220, 38, 38),
                Color.FromArgb(217, 119, 6),
                Color.FromArgb(100, 116, 139),
                Color.FromArgb(14, 116, 144),
                Color.FromArgb(124, 58, 237)
            };
            _chartDonut.SetData(data.ThanhVienTheoTrangThai.Select((x, i) => (x.TrangThai, (decimal)x.SoLuong, colors[i % colors.Length])));
        }

        if (_chartTrangThai is not null)
        {
            var barColors = new[] { Color.FromArgb(5, 150, 105), Color.FromArgb(220, 38, 38), Color.FromArgb(217, 119, 6), Color.FromArgb(100, 116, 139) };
            _chartTrangThai.SetData(data.ThanhVienTheoTrangThai.Select((x, i) => (x.TrangThai, (decimal)x.SoLuong, barColors[i % barColors.Length])));
        }

        // Bảng gói sắp hết hạn
        dgvGoiSapHetHan.Rows.Clear();
        foreach (var x in data.GoiSapHetHanChiTiet)
        {
            var idx = dgvGoiSapHetHan.Rows.Add(x.MaThanhVien, x.HoTen, x.SDT, x.TenGoi, x.NgayKetThuc, x.SoNgayConLai, x.TrangThai);
            var row = dgvGoiSapHetHan.Rows[idx];
            row.DefaultCellStyle.BackColor = x.SoNgayConLai <= 0
                ? Color.FromArgb(254, 226, 226)
                : x.SoNgayConLai <= 3
                    ? Color.FromArgb(255, 237, 213)
                    : Color.FromArgb(254, 249, 195);
        }

        // Bảng công nợ
        dgvCongNo.Rows.Clear();
        foreach (var x in data.CongNoChiTiet)
        {
            dgvCongNo.Rows.Add(x.SoHoaDon, x.HoTen, x.TongTien, x.DaThanhToan, x.ConNo);
        }

        // Bảng hoạt động
        dgvHoatDong.Rows.Clear();
        foreach (var x in data.HoatDongGanDay)
        {
            dgvHoatDong.Rows.Add(x.ThoiGian, x.HanhDong, x.DoiTuong, x.NoiDung);
        }

        // Bảng gói bán chạy
        dgvGoiBanChay.Rows.Clear();
        foreach (var x in data.GoiBanChay)
        {
            dgvGoiBanChay.Rows.Add(x.TenGoi, x.SoLuotDangKy, x.DoanhThu);
        }

        var gioCaoDiem = data.GioCaoDiem >= 0 ? $"{data.GioCaoDiem:00}:00" : "--";
        lblVanHanhTomTat.Text = $"📊 Check-in: {data.CheckInTrongKy:N0}  •  Check-out: {data.CheckOutTrongKy:N0}  •  Cao điểm: {gioCaoDiem}\n🔄 Bảo lưu: {data.GoiBaoLuu:N0} gói  •  Gia hạn: {data.GoiGiaHan:N0}  •  PT hoàn thành: {data.BuoiPTDaHoanThanh:N0}/{data.BuoiPTTrongKy:N0}";

        boxDoanhThu.Visible = coQuyenTaiChinh;
        boxCongNo.Visible = coQuyenTaiChinh;
    }

    private void GanKpi(string khoa, string giaTri)
    {
        if (_kpiDashboard.TryGetValue(khoa, out var label)) label.Text = giaTri;
    }

    private void GanKpiSub(string khoa, string giaTri)
    {
        if (_kpiSubDashboard.TryGetValue(khoa, out var label)) label.Text = giaTri;
    }

    private (DateTime TuNgay, DateTime DenNgay) LayKhoangDashboard()
    {
        var homNay = DateTime.Today;
        return cboKhoang.SelectedIndex switch
        {
            1 => (homNay.AddDays(-1), homNay.AddDays(-1)),
            2 => (homNay.AddDays(-6), homNay),
            3 => (new DateTime(homNay.Year, homNay.Month, 1), homNay),
            4 => (new DateTime(homNay.Year, homNay.Month, 1).AddMonths(-1), new DateTime(homNay.Year, homNay.Month, 1).AddDays(-1)),
            5 => (new DateTime(homNay.Year, ((homNay.Month - 1) / 3) * 3 + 1, 1), homNay),
            6 => (new DateTime(homNay.Year, 1, 1), homNay),
            7 => (dtpTuNgay.Value.Date, dtpDenNgay.Value.Date),
            _ => (homNay, homNay)
        };
    }

    private void CapNhatNgayTheoLuaChon()
    {
        var khoang = cboKhoang.SelectedIndex;
        if (khoang == 7)
        {
            dtpTuNgay.Enabled = true;
            dtpDenNgay.Enabled = true;
            return;
        }

        var (tu, den) = LayKhoangDashboard();
        dtpTuNgay.Value = tu;
        dtpDenNgay.Value = den;
        dtpTuNgay.Enabled = false;
        dtpDenNgay.Enabled = false;
    }

    private void cboKhoang_SelectedIndexChanged(object? sender, EventArgs e)
    {
        CapNhatNgayTheoLuaChon();
        if (!_dangKhoiTao && cboKhoang.SelectedIndex != 7)
        {
            _ = TaiDuLieuAsync();
        }
    }

    private void DatePicker_ValueChanged(object? sender, EventArgs e)
    {
        btnApDung.Enabled = dtpTuNgay.Value.Date <= dtpDenNgay.Value.Date;
    }

    private async void btnApDung_Click(object? sender, EventArgs e)
    {
        await TaiDuLieuAsync();
    }

    private async void btnLamMoi_Click(object? sender, EventArgs e)
    {
        await TaiDuLieuAsync();
    }

    private void dgvGoiSapHetHan_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            YeuCauMoForm?.Invoke(new FrmVanHanhGoi());
        }
    }

    private void dgvCongNo_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            YeuCauMoForm?.Invoke(new FrmHoaDon());
        }
    }
}
