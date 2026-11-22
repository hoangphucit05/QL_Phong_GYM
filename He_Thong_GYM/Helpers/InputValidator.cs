using System.Net.Mail;
using System.Text.RegularExpressions;

namespace He_Thong_GYM.Helpers;

public static partial class InputValidator
{
    [GeneratedRegex(@"^0\d{9,10}$", RegexOptions.CultureInvariant)]
    private static partial Regex SoDienThoaiRegex();

    public static void ValidatePhone(string? value, string fieldName = "Số điện thoại")
    {
        var phone = value?.Trim();
        if (string.IsNullOrWhiteSpace(phone) || !SoDienThoaiRegex().IsMatch(phone))
            throw new ArgumentException($"{fieldName} phải gồm 10 hoặc 11 chữ số và bắt đầu bằng số 0.");
    }

    public static void ValidateOptionalEmail(string? value, string fieldName = "Email")
    {
        var email = value?.Trim();
        if (string.IsNullOrWhiteSpace(email)) return;
        try
        {
            var address = new MailAddress(email);
            if (!string.Equals(address.Address, email, StringComparison.OrdinalIgnoreCase) ||
                !email.Contains('@') || email[(email.LastIndexOf('@') + 1)..].Contains('.') is false)
                throw new FormatException();
        }
        catch
        {
            throw new ArgumentException($"{fieldName} không đúng định dạng.");
        }
    }
}
