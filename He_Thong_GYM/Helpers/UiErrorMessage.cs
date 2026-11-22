using Microsoft.Data.SqlClient;

namespace He_Thong_GYM.Helpers;

public static class UiErrorMessage
{
    public static string Get(Exception exception)
    {
        try
        {
            var logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GymPro", "logs");
            Directory.CreateDirectory(logDirectory);
            var logFile = Path.Combine(logDirectory, $"app-{DateTime.Today:yyyyMMdd}.log");
            File.AppendAllText(logFile, $"[{DateTime.Now:O}] {exception}\r\n");
        }
        catch
        {
            // Không để lỗi ghi log che mất thông báo nghiệp vụ.
        }

        if (exception is ArgumentException or InvalidOperationException or UnauthorizedAccessException)
            return exception.Message;

        if (exception is SqlException sql)
        {
            return sql.Number switch
            {
                2601 or 2627 => "Dữ liệu bị trùng. Hãy kiểm tra mã hoặc tên đã nhập.",
                547 => "Dữ liệu liên quan không hợp lệ hoặc đang được sử dụng.",
                1205 => "Hệ thống đang bận. Vui lòng thử lại.",
                _ => "Không thể hoàn tất thao tác với dữ liệu. Vui lòng kiểm tra kết nối và thử lại."
            };
        }

        return "Không thể hoàn tất thao tác. Vui lòng thử lại hoặc liên hệ quản trị viên.";
    }
}
