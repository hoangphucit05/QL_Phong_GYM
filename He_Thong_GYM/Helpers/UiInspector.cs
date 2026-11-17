using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using He_Thong_GYM.Forms;

namespace He_Thong_GYM.Helpers
{
    public static class UiInspector
    {
        public static void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("==================================================================");
            Console.WriteLine("   BÁO CÁO KIỂM TRA TOÀN DIỆN GIAO DIỆN & VĂN BẢN TIẾNG VIỆT     ");
            Console.WriteLine("==================================================================");

            var formTypes = typeof(Form1).Assembly.GetTypes()
                .Where(t => typeof(Form).IsAssignableFrom(t) && !t.IsAbstract)
                .OrderBy(t => t.Name)
                .ToArray();

            int totalForms = formTypes.Length;
            int totalOverlaps = 0;
            int totalOutOfBounds = 0;
            int totalTextTruncated = 0;
            int totalVietnameseIssues = 0;

            var issuesReport = new List<string>();

            foreach (var type in formTypes)
            {
                Console.WriteLine($"\n>>> ĐANG KIỂM TRA FORM: {type.Name} <<<");

                Form? form = null;
                try
                {
                    var ctor = type.GetConstructor(Type.EmptyTypes);
                    if (ctor != null)
                    {
                        form = (Form)ctor.Invoke(null);
                    }
                    else
                    {
                        var paramCtors = type.GetConstructors();
                        if (paramCtors.Length > 0)
                        {
                            var p = paramCtors[0].GetParameters();
                            object?[] args = new object?[p.Length];
                            form = (Form)paramCtors[0].Invoke(args);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  [LỖI TẠO ĐỐI TƯỢNG] {ex.Message}");
                    continue;
                }

                if (form == null) continue;

                // 1. Kiểm tra kích thước thiết kế gốc
                Console.WriteLine($"  * Kích thước thiết kế: {form.ClientSize.Width}x{form.ClientSize.Height}, AutoScroll={form.AutoScroll}, Font={form.Font.Name} {form.Font.Size}pt");
                CheckContainerLayout(form, form, "Kích thước gốc", issuesReport, ref totalOverlaps, ref totalOutOfBounds, ref totalTextTruncated, ref totalVietnameseIssues);

                // 2. Nếu là form con nhúng vào panelNoiDung của Form1
                if (type != typeof(FrmLogin) && type != typeof(FrmBanHang) && type != typeof(FrmThanhToan) && type != typeof(Form1))
                {
                    using var pnlContainer = new Panel { Size = new Size(1090, 660), AutoScroll = false };
                    form.TopLevel = false;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.Dock = DockStyle.Fill;
                    pnlContainer.Controls.Add(form);
                    form.Show();
                    form.PerformLayout();
                    pnlContainer.PerformLayout();

                    Console.WriteLine($"  * Khi nhúng vào panelNoiDung (1090x660): Form.ClientSize={form.ClientSize.Width}x{form.ClientSize.Height}");
                    CheckContainerLayout(form, form, "Nhúng panelNoiDung (1090x660)", issuesReport, ref totalOverlaps, ref totalOutOfBounds, ref totalTextTruncated, ref totalVietnameseIssues);
                }

                form.Dispose();
            }

            Console.WriteLine("\n==================================================================");
            Console.WriteLine($"TỔNG KẾT KIỂM TRA:");
            Console.WriteLine($"- Số Form đã kiểm tra: {totalForms}");
            Console.WriteLine($"- Lỗi đè control (Overlap): {totalOverlaps}");
            Console.WriteLine($"- Lỗi tràn biên/mất góc (Out of bounds): {totalOutOfBounds}");
            Console.WriteLine($"- Lỗi chữ bị cắt/thiếu chỗ (Text Truncated): {totalTextTruncated}");
            Console.WriteLine($"- Lỗi hiển thị Tiếng Việt / UTF-8: {totalVietnameseIssues}");
            Console.WriteLine("==================================================================");

            if (issuesReport.Count > 0)
            {
                Console.WriteLine("\nDANH SÁCH CHI TIẾT CÁC ĐIỂM CẦN CẢI THIỆN:");
                foreach (var issue in issuesReport)
                {
                    Console.WriteLine("  " + issue);
                }
            }
            else
            {
                Console.WriteLine("\nKHÔNG PHÁT HIỆN LỖI ĐÈ NHAU HAY LỖI ENCODING!");
            }
        }

        private static void CheckContainerLayout(
            Control rootForm,
            Control container,
            string phase,
            List<string> issues,
            ref int overlaps,
            ref int outOfBounds,
            ref int textTruncated,
            ref int vietnameseIssues)
        {
            var children = container.Controls.Cast<Control>().Where(c => c.Visible).ToList();

            // Kiểm tra Tiếng Việt & text clipping
            foreach (var c in children)
            {
                string text = c.Text;
                if (!string.IsNullOrEmpty(text))
                {
                    // Check Unicode replacement or broken characters
                    if (text.Contains('\ufffd') || text.Contains('?'))
                    {
                        // Check if it's a legitimate question mark or corrupt char
                        if (text.Contains('\ufffd') || text.Contains("???") || text.Contains("?n") || text.Contains("?ng"))
                        {
                            string msg = $"[LỖI ENCODING] {rootForm.GetType().Name} -> {c.Name}: Text=\"{text}\" chứa ký tự nghi vấn lỗi UTF-8!";
                            issues.Add(msg);
                            vietnameseIssues++;
                        }
                    }

                    // Kiểm tra chuỗi bị lỗi mã hóa UTF-8 (mojibake thực sự, ví dụ "Ã¡", "Ã ", "á» ", "Ã¢" bị giải mã nhầm latin1)
                    if (System.Text.RegularExpressions.Regex.IsMatch(text, @"[\u00C2\u00C3][\u0080-\u00BF]") ||
                        System.Text.RegularExpressions.Regex.IsMatch(text, @"\u00E1\u00BB[\u0080-\u00BF]") ||
                        text.Contains("Ã¡") || text.Contains("Ã ") || text.Contains("áº") || text.Contains("á» ") || text.Contains("Ä‘"))
                    {
                        string msg = $"[LỖI MOJIBAKE] {rootForm.GetType().Name} -> {c.Name}: Text=\"{text}\" bị lỗi hiển thị tiếng Việt!";
                        issues.Add(msg);
                        vietnameseIssues++;
                    }

                    // Check text clipping
                    if (c is Label lbl && !lbl.AutoSize && lbl.Dock == DockStyle.None && lbl.Width > 0 && lbl.Height > 0)
                    {
                        var measured = TextRenderer.MeasureText(text, lbl.Font);
                        if (measured.Width > lbl.Width + 4 || measured.Height > lbl.Height + 4)
                        {
                            string msg = $"[CHỮ BỊ CẮT] ({phase}) {rootForm.GetType().Name} -> Label '{lbl.Name}' Text=\"{text}\": cần {measured.Width}x{measured.Height}px, nhưng ô chỉ có {lbl.Width}x{lbl.Height}px.";
                            issues.Add(msg);
                            textTruncated++;
                        }
                    }
                    else if (c is Button btn && btn.Dock == DockStyle.None && btn.Width > 0 && btn.Height > 0)
                    {
                        var measured = TextRenderer.MeasureText(text, btn.Font);
                        if (measured.Width > btn.Width - 10 || measured.Height > btn.Height - 6)
                        {
                            string msg = $"[NÚT BỊ CẮT CHỮ] ({phase}) {rootForm.GetType().Name} -> Button '{btn.Name}' Text=\"{text}\": cần {measured.Width}x{measured.Height}px, nhưng nút chỉ có {btn.Width}x{btn.Height}px.";
                            issues.Add(msg);
                            textTruncated++;
                        }
                    }
                }

                // Check DataGridView headers
                if (c is DataGridView dgv)
                {
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (!string.IsNullOrEmpty(col.HeaderText))
                        {
                            if (col.HeaderText.Contains('\ufffd') || col.HeaderText.Contains("Ã") || col.HeaderText.Contains("á»"))
                            {
                                string msg = $"[LỖI ENCODING CỘT] {rootForm.GetType().Name} -> DataGridView '{dgv.Name}' Cột '{col.Name}': Header=\"{col.HeaderText}\" lỗi UTF-8!";
                                issues.Add(msg);
                                vietnameseIssues++;
                            }
                        }
                    }
                }
            }

            // Kiểm tra bounds và overlap nếu container không phải FlowLayoutPanel hay TableLayoutPanel
            if (container is not FlowLayoutPanel && container is not TableLayoutPanel)
            {
                // Kiểm tra control tràn mép ngoài container
                bool hasAutoScroll = (container is ScrollableControl sc && sc.AutoScroll);
                if (!hasAutoScroll && container.ClientSize.Width > 0 && container.ClientSize.Height > 0)
                {
                    foreach (var c in children)
                    {
                        if (c.Dock == DockStyle.None)
                        {
                            if (c.Right > container.ClientSize.Width + 4)
                            {
                                string msg = $"[TRÀN BIÊN NGANG] ({phase}) {rootForm.GetType().Name} -> '{c.Name}' (Right={c.Right}) tràn mép phải container '{container.Name}' (Width={container.ClientSize.Width}).";
                                issues.Add(msg);
                                outOfBounds++;
                            }
                            if (c.Bottom > container.ClientSize.Height + 4)
                            {
                                string msg = $"[TRÀN BIÊN DỌC] ({phase}) {rootForm.GetType().Name} -> '{c.Name}' (Bottom={c.Bottom}) tràn mép dưới container '{container.Name}' (Height={container.ClientSize.Height}).";
                                issues.Add(msg);
                                outOfBounds++;
                            }
                        }
                    }
                }

                // Kiểm tra overlap giữa 2 control cùng cấp có Dock == None
                for (int i = 0; i < children.Count; i++)
                {
                    for (int j = i + 1; j < children.Count; j++)
                    {
                        var c1 = children[i];
                        var c2 = children[j];

                        if (c1.Dock == DockStyle.None && c2.Dock == DockStyle.None)
                        {
                            var r1 = c1.Bounds;
                            var r2 = c2.Bounds;
                            var inter = Rectangle.Intersect(r1, r2);

                            // Nếu diện tích giao nhau lớn hơn 10px thì coi là đè lên nhau
                            if (inter.Width > 4 && inter.Height > 4)
                            {
                                string msg = $"[ĐÈ LÊN NHAU] ({phase}) {rootForm.GetType().Name} -> '{c1.Name}' (Bounds={r1}) đè lên '{c2.Name}' (Bounds={r2}) trong '{container.Name}'.";
                                issues.Add(msg);
                                overlaps++;
                            }
                        }
                    }
                }
            }

            // Đệ quy kiểm tra cấp con
            foreach (var c in children)
            {
                if (c.HasChildren && c is not DataGridView)
                {
                    CheckContainerLayout(rootForm, c, phase, issues, ref overlaps, ref outOfBounds, ref textTruncated, ref vietnameseIssues);
                }
            }
        }
    }
}
