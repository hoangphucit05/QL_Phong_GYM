using System.Drawing.Drawing2D;

namespace He_Thong_GYM.Helpers;

/// <summary>
/// Panel vẽ biểu đồ donut (vòng tròn) cho Dashboard.
/// </summary>
public sealed class DashboardDonutPanel : Panel
{
    private IReadOnlyList<(string Label, decimal Value, Color Color)> _items = [];

    public DashboardDonutPanel()
    {
        BackColor = Color.White;
        DoubleBuffered = true;
        Padding = new Padding(8);
    }

    public void SetData(IEnumerable<(string Label, decimal Value, Color Color)> items)
    {
        _items = items.Where(x => x.Value > 0).ToList();
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        if (_items.Count == 0)
        {
            TextRenderer.DrawText(g, "Chưa có dữ liệu.", Font,
                new Rectangle(12, 12, Math.Max(1, Width - 24), 30), Color.FromArgb(100, 116, 139));
            return;
        }

        var total = _items.Sum(x => x.Value);
        if (total <= 0) return;

        // Kích thước vùng vẽ cân đối, dành không gian rộng rãi cho chú thích bên dưới
        var donutSize = Math.Min(Width - 30, Height - 85);
        donutSize = Math.Clamp(donutSize, 90, 135);
        var centerX = Width / 2;
        var centerY = donutSize / 2 + 10;
        var rect = new Rectangle(centerX - donutSize / 2, centerY - donutSize / 2, donutSize, donutSize);

        // Vẽ từng phần donut
        float startAngle = -90f;
        foreach (var item in _items)
        {
            var sweepAngle = (float)(item.Value / total * 360m);
            using var brush = new SolidBrush(item.Color);
            g.FillPie(brush, rect, startAngle, sweepAngle);
            startAngle += sweepAngle;
        }

        // Vẽ lỗ trắng ở giữa (donut hole)
        var holeSize = (int)(donutSize * 0.54);
        var holeRect = new Rectangle(centerX - holeSize / 2, centerY - holeSize / 2, holeSize, holeSize);
        using var holeBrush = new SolidBrush(Color.White);
        g.FillEllipse(holeBrush, holeRect);

        // Vẽ tổng ở giữa
        var totalText = total.ToString("N0");
        using var fontTotal = new Font("Segoe UI", 13F, FontStyle.Bold);
        var szTotal = g.MeasureString(totalText, fontTotal);
        g.DrawString(totalText, fontTotal, new SolidBrush(Color.FromArgb(15, 23, 42)),
            centerX - szTotal.Width / 2, centerY - szTotal.Height / 2 - 5);
        using var fontSub = new Font("Segoe UI", 7.5F);
        var subText = "Tổng";
        var szSub = g.MeasureString(subText, fontSub);
        g.DrawString(subText, fontSub, new SolidBrush(Color.FromArgb(100, 116, 139)),
            centerX - szSub.Width / 2, centerY + szTotal.Height / 2 - 8);

        // Vẽ chú thích (legend) phía dưới
        var legendTop = centerY + donutSize / 2 + 10;
        var legendLeft = 14;
        using var fontLegend = new Font("Segoe UI", 8F);
        using var brushLegend = new SolidBrush(Color.FromArgb(51, 65, 85));
        foreach (var item in _items)
        {
            if (legendTop + 14 > Height) break;
            using var colorBrush = new SolidBrush(item.Color);
            g.FillRoundedRectangle(colorBrush, new Rectangle(legendLeft, legendTop + 2, 10, 10), 2);
            var pct = (item.Value / total * 100m).ToString("F0");
            var legendText = $"{item.Label}: {item.Value:N0} ({pct}%)";
            g.DrawString(legendText, fontLegend, brushLegend, legendLeft + 16, legendTop);
            legendTop += 18;
        }
    }
}
