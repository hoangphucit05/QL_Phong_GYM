using System.Text;
using System.Text.RegularExpressions;

namespace He_Thong_GYM.Models;

public sealed class KhuyenMai
{
    public int? MaKM { get; set; }
    public string MaCode { get; set; } = string.Empty;
    public string TenKM { get; set; } = string.Empty;
    public decimal PhanTramGiam { get; set; }
    public DateTime? NgayBatDau { get; set; }
    public DateTime? NgayKetThuc { get; set; }
    public string? TrangThai { get; set; }

    /// <summary>
    /// Hiển thị thân thiện và thực tế trên giao diện: [MACODE] Tên khuyến mãi (-XX%)
    /// </summary>
    public string HienThi
    {
        get
        {
            if (MaKM is null) return TenKM; // Ví dụ: "Không áp dụng"
            var code = string.IsNullOrWhiteSpace(MaCode) ? TaoMaCodeThucTe(TenKM, PhanTramGiam, MaKM) : MaCode;
            return $"[{code}] {TenKM} (-{PhanTramGiam:0}%)";
        }
    }

    /// <summary>
    /// Tạo mã khuyến mãi dạng chữ (voucher code) thực tế thay vì số thứ tự đơn điệu
    /// </summary>
    public static string TaoMaCodeThucTe(string? tenKM, decimal phanTramGiam, int? maKM)
    {
        if (string.IsNullOrWhiteSpace(tenKM))
            return $"KM{maKM ?? 0:D3}";

        var t = tenKM.Trim().ToLowerInvariant();
        if (t.Contains("chào hè") || t.Contains("hè") || t.Contains("summer")) return $"CHAOHE{(int)phanTramGiam}";
        if (t.Contains("sinh viên") || t.Contains("student")) return $"SINHVIEN{(int)phanTramGiam}";
        if (t.Contains("khách hàng mới") || t.Contains("mới") || t.Contains("welcome")) return $"NEWGYM{(int)phanTramGiam}";
        if (t.Contains("black friday")) return $"BLACKFRIDAY{(int)phanTramGiam}";
        if (t.Contains("tết")) return "TET2026";
        if (t.Contains("gia đình") || t.Contains("family")) return $"FAMILY{(int)phanTramGiam}";
        if (t.Contains("pt") || t.Contains("huấn luyện")) return $"PROPT{(int)phanTramGiam}";
        if (t.Contains("yoga")) return $"YOGAMAY{(int)phanTramGiam}";
        if (t.Contains("cardio")) return $"CARDIO{(int)phanTramGiam}";
        if (t.Contains("vip")) return $"VIPPRO{(int)phanTramGiam}";
        if (t.Contains("noel") || t.Contains("giáng sinh")) return $"NOEL{(int)phanTramGiam}";
        if (t.Contains("tri ân")) return $"TRIAN{(int)phanTramGiam}";
        if (t.Contains("thử") || t.Contains("free")) return "FREETRIAL";
        if (t.Contains("tựu trường")) return $"BACK2GYM{(int)phanTramGiam}";
        if (t.Contains("vàng") || t.Contains("sức khỏe")) return $"GOLDHEALTH{(int)phanTramGiam}";

        // Tự động sinh mã chuẩn theo từ ngữ đầu của tên
        var normalized = LoaiBoDauTiengViet(tenKM).ToUpperInvariant();
        var words = normalized.Split(new[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
        var prefix = words.Length > 0 ? Regex.Replace(words[0], "[^A-Z0-9]", "") : "KM";
        if (prefix.Length > 8) prefix = prefix.Substring(0, 8);
        return $"{prefix}{(int)phanTramGiam}";
    }

    private static string LoaiBoDauTiengViet(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return string.Empty;
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var c in normalizedString)
        {
            var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }
        return sb.ToString().Normalize(NormalizationForm.FormC).Replace('đ', 'd').Replace('Đ', 'D');
    }
}
