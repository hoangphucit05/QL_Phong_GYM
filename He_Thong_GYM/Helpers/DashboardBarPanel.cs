using System.Drawing.Drawing2D;

namespace He_Thong_GYM.Helpers;

public sealed class DashboardBarPanel : Panel
{
    private IReadOnlyList<(string Label, decimal Value, Color Color)> _items = [];

    public DashboardBarPanel()
    {
        BackColor = Color.White;
        DoubleBuffered = true;
        Padding = new Padding(12);
    }

    public void SetData(IEnumerable<(string Label, decimal Value, Color Color)> items)
    {
        _items = items.Where(x => x.Value >= 0).Take(12).ToList();
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        if (_items.Count == 0)
        {
            TextRenderer.DrawText(e.Graphics, "Chưa có dữ liệu trong khoảng thời gian đã chọn.", Font,
                new Rectangle(12, 12, Math.Max(1, Width - 24), 30), Color.FromArgb(100, 116, 139));
            return;
        }

        var max = Math.Max(1m, _items.Max(x => x.Value));
        var rowHeight = Math.Max(28, (Height - Padding.Vertical) / _items.Count);
        var labelWidth = Math.Min(86, Math.Max(62, Width / 5));
        var barLeft = Padding.Left + labelWidth;
        var barWidth = Math.Max(45, Width - barLeft - Padding.Right - 70);

        for (var i = 0; i < _items.Count; i++)
        {
            var item = _items[i];
            var y = Padding.Top + (i * rowHeight) + 3;
            TextRenderer.DrawText(e.Graphics, item.Label, Font, new Rectangle(Padding.Left, y, labelWidth - 6, 22), Color.FromArgb(71, 85, 105), TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter);

            using var background = new SolidBrush(Color.FromArgb(226, 232, 240));
            e.Graphics.FillRoundedRectangle(background, new Rectangle(barLeft, y + 5, barWidth, 13), 6);
            using var foreground = new SolidBrush(item.Color);
            var currentWidth = (int)Math.Round(barWidth * item.Value / max);
            if (currentWidth > 0)
                e.Graphics.FillRoundedRectangle(foreground, new Rectangle(barLeft, y + 5, Math.Max(8, currentWidth), 13), 6);

            string valStr;
            if (item.Value >= 1_000_000_000m)
                valStr = $"{item.Value / 1_000_000_000m:0.##}B";
            else if (item.Value >= 1_000_000m)
                valStr = $"{item.Value / 1_000_000m:0.##}M";
            else if (item.Value >= 100_000m)
                valStr = $"{item.Value / 1_000m:0.#}K";
            else
                valStr = item.Value.ToString("N0");

            TextRenderer.DrawText(e.Graphics, valStr, Font,
                new Rectangle(barLeft + barWidth + 6, y, 64, 22), Color.FromArgb(15, 23, 42), TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }
    }
}

internal static class GraphicsExtensions
{
    public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle rectangle, int radius)
    {
        using var path = new GraphicsPath();
        var diameter = radius * 2;
        path.AddArc(rectangle.X, rectangle.Y, diameter, diameter, 180, 90);
        path.AddArc(rectangle.Right - diameter, rectangle.Y, diameter, diameter, 270, 90);
        path.AddArc(rectangle.Right - diameter, rectangle.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(rectangle.X, rectangle.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();
        graphics.FillPath(brush, path);
    }
}
