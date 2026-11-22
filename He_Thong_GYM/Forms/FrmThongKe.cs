using System.Drawing.Drawing2D;
using He_Thong_GYM.DAL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.Forms;

public partial class FrmThongKe : Form
{
    private readonly ThongKeDAL _dal = new();
    private List<ThongKeDoanhThuThangView> _duLieuThang = new();

    public FrmThongKe()
    {
        InitializeComponent();
        panelBieuDo.Paint += PanelBieuDo_Paint;
        panelBieuDo.Resize += (s, e) => panelBieuDo.Invalidate();
    }

    private async void FrmThongKe_Load(object? sender, EventArgs e)
    {
        // Mặc định xem từ đầu tháng hiện tại đến hôm nay
        var now = DateTime.Today;
        dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
        dtpDenNgay.Value = now;

        await TaiTatCaBaoCaoAsync();
    }

    private async Task TaiTatCaBaoCaoAsync(DateTime? tuNgay = null, DateTime? denNgay = null)
    {
        try
        {
            Cursor = Cursors.WaitCursor;

            // 1. Tải chỉ số tổng quan (KPI)
            var kpi = await _dal.LayTongQuanAsync(tuNgay, denNgay);
            lblThanhVien.Text = $"{kpi.GetValueOrDefault("Thành viên"):N0}";
            lblDoanhThu.Text = $"{kpi.GetValueOrDefault("Doanh thu thực"):N0} đ";
            lblCongNo.Text = $"{kpi.GetValueOrDefault("Công nợ"):N0} đ";
            lblTonKho.Text = $"{kpi.GetValueOrDefault("Giá trị tồn"):N0} đ";
            lblBaoTri.Text = $"{kpi.GetValueOrDefault("Chi phí bảo trì"):N0} đ";

            // 2. Tải doanh thu theo Gói tập
            var dsGoi = await _dal.LayDoanhThuGoiTapAsync(tuNgay, denNgay);
            dgvDoanhThuGoi.DataSource = dsGoi;

            // 3. Tải doanh thu theo Tháng (Năm hiện tại)
            var namHienTai = tuNgay?.Year ?? DateTime.Today.Year;
            _duLieuThang = await _dal.LayDoanhThuTheoThangAsync(namHienTai);
            dgvDoanhThuThang.DataSource = _duLieuThang;
            panelBieuDo.Invalidate(); // Vẽ lại biểu đồ

            // 4. Tải cơ cấu Hội viên
            var dsHoiVien = await _dal.LayCoCauHoiVienAsync();
            dgvCoCauHoiVien.DataSource = dsHoiVien;

            // 5. Tải top Huấn luyện viên
            var dsHLV = await _dal.LayTopHuanLuyenVienAsync(tuNgay, denNgay);
            dgvTopHLV.DataSource = dsHLV;

            // 6. Tải tình hình Sản phẩm
            var dsSP = await _dal.LayThongKeSanPhamAsync();
            dgvSanPham.DataSource = dsSP;
        }
        catch (Exception ex)
        {
            MessageBox.Show(UiErrorMessage.Get(ex), "Lỗi tải báo cáo thống kê", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private async void btnLoc_Click(object? sender, EventArgs e)
    {
        var tuNgay = dtpTuNgay.Value.Date;
        var denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);
        if (tuNgay > denNgay)
        {
            MessageBox.Show("Từ ngày không thể lớn hơn Đến ngày.", "Khoảng thời gian không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        await TaiTatCaBaoCaoAsync(tuNgay, denNgay);
    }

    private async void btnHomNay_Click(object? sender, EventArgs e)
    {
        var today = DateTime.Today;
        dtpTuNgay.Value = today;
        dtpDenNgay.Value = today;
        await TaiTatCaBaoCaoAsync(today, today.AddDays(1).AddSeconds(-1));
    }

    private async void btnThangNay_Click(object? sender, EventArgs e)
    {
        var now = DateTime.Today;
        dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
        dtpDenNgay.Value = now;
        await TaiTatCaBaoCaoAsync(dtpTuNgay.Value.Date, dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1));
    }

    private async void btnTatCa_Click(object? sender, EventArgs e)
    {
        dtpTuNgay.Value = new DateTime(2020, 1, 1);
        dtpDenNgay.Value = DateTime.Today;
        await TaiTatCaBaoCaoAsync(null, null);
    }

    private void PanelBieuDo_Paint(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Color.White);

        int pW = panelBieuDo.Width;
        int pH = panelBieuDo.Height;
        if (pW < 200 || pH < 150) return;

        // Vùng vẽ biểu đồ
        int marginL = 70;
        int marginR = 30;
        int marginT = 40;
        int marginB = 50;
        int plotW = pW - marginL - marginR;
        int plotH = pH - marginT - marginB;

        // Tạo mảng 12 tháng
        decimal[] doanhThu12Thang = new decimal[12];
        foreach (var item in _duLieuThang)
        {
            if (item.Thang >= 1 && item.Thang <= 12)
            {
                doanhThu12Thang[item.Thang - 1] += item.DoanhThu;
            }
        }

        decimal maxVal = 0;
        foreach (var val in doanhThu12Thang)
        {
            if (val > maxVal) maxVal = val;
        }

        if (maxVal <= 0) maxVal = 10000000; // Giá trị trần mặc định 10 triệu nếu chưa có dữ liệu

        // Làm tròn mốc cao nhất
        decimal stepY = Math.Ceiling(maxVal / 4 / 1000000) * 1000000;
        if (stepY <= 0) stepY = 2000000;
        decimal trầnY = stepY * 4;

        // Vẽ đường lưới ngang (Grid lines) và nhãn trục Y
        using var penGrid = new Pen(Color.FromArgb(241, 245, 249), 1);
        using var penAxis = new Pen(Color.FromArgb(203, 213, 225), 1.5f);
        using var fontLabel = new Font("Segoe UI", 8.5f);
        using var fontValue = new Font("Segoe UI", 7.5f, FontStyle.Bold);
        using var brushLabel = new SolidBrush(Color.FromArgb(100, 116, 139));
        using var brushVal = new SolidBrush(Color.FromArgb(15, 23, 42));

        for (int i = 0; i <= 4; i++)
        {
            float y = marginT + plotH - (i * plotH / 4.0f);
            decimal giaTri = i * stepY;

            // Đường lưới
            g.DrawLine(penGrid, marginL, y, marginL + plotW, y);

            // Nhãn số tiền trục Y
            string nhanY = giaTri >= 1000000 ? $"{giaTri / 1000000:N0}M" : $"{giaTri:N0}";
            var sz = g.MeasureString(nhanY, fontLabel);
            g.DrawString(nhanY, fontLabel, brushLabel, marginL - sz.Width - 8, y - sz.Height / 2);
        }

        // Vẽ trục X và trục Y chính
        g.DrawLine(penAxis, marginL, marginT + plotH, marginL + plotW, marginT + plotH);
        g.DrawLine(penAxis, marginL, marginT, marginL, marginT + plotH);

        // Vẽ 12 cột doanh thu
        float barSlotW = plotW / 12.0f;
        float barW = Math.Max(12, barSlotW * 0.55f);

        for (int m = 0; m < 12; m++)
        {
            float slotX = marginL + (m * barSlotW);
            float barX = slotX + (barSlotW - barW) / 2.0f;

            decimal val = doanhThu12Thang[m];
            float barH = (float)(val / trầnY) * plotH;
            if (barH > plotH) barH = plotH;
            float barY = marginT + plotH - barH;

            // Cột gradient màu xanh ngọc / xanh dương
            if (val > 0)
            {
                var barRect = new RectangleF(barX, barY, barW, barH);
                using var barBrush = new LinearGradientBrush(
                    barRect,
                    Color.FromArgb(37, 99, 235),  // Xanh dương
                    Color.FromArgb(5, 150, 105),  // Xanh ngọc
                    LinearGradientMode.Vertical);

                g.FillRectangle(barBrush, barRect);

                // Viền cột nhẹ
                using var penBorder = new Pen(Color.FromArgb(29, 78, 216), 1);
                g.DrawRectangle(penBorder, barRect.X, barRect.Y, barRect.Width, barRect.Height);

                // Hiển thị giá trị trên đỉnh cột
                string strVal = val >= 1000000 ? $"{val / 1000000:F1}M" : $"{val / 1000:N0}K";
                var szVal = g.MeasureString(strVal, fontValue);
                g.DrawString(strVal, fontValue, brushVal, barX + (barW - szVal.Width) / 2.0f, barY - szVal.Height - 3);
            }
            else
            {
                // Cột rỗng (dấu chấm hoặc vạch xám nhỏ)
                using var penEmpty = new Pen(Color.FromArgb(226, 232, 240), 2);
                g.DrawLine(penEmpty, barX + barW * 0.2f, marginT + plotH - 2, barX + barW * 0.8f, marginT + plotH - 2);
            }

            // Nhãn tháng trục X
            string nhanThang = $"T{m + 1}";
            var szT = g.MeasureString(nhanThang, fontLabel);
            g.DrawString(nhanThang, fontLabel, brushLabel, slotX + (barSlotW - szT.Width) / 2.0f, marginT + plotH + 8);
        }
    }
}
