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
                50010 => "Thành viên đang có lượt Check-in mở. Hãy Check-out trước.",
                50011 => "Gói đã chọn chưa thanh toán, hết hạn/hết lượt, sai khung giờ hoặc không được cấp quyền phòng này.",
                50012 => "Thành viên chưa có lượt Check-in mở. Hãy Check-in trước.",
                50044 => "Không có lần bảo lưu đang hoạt động.",
                50045 => "Chưa đến ngày kết thúc bảo lưu.",
                50046 => "Gói không ở trạng thái tạm dừng.",
                50047 => "Phòng mới phải khác phòng hiện tại.",
                50048 => "Phòng mới đã đủ sức chứa.",
                50049 => "Gói hiện tại không được phép sử dụng phòng mới.",
                52106 => "Tài khoản hiện tại không có quyền Check-in hoặc không còn hoạt động.",
                52107 => "Tài khoản hiện tại không có quyền Check-out hoặc không còn hoạt động.",
                52110 => "Tài khoản hiện tại không có quyền chuyển phòng hoặc không còn hoạt động.",
                2601 or 2627 => "Dữ liệu bị trùng. Hãy kiểm tra mã hoặc tên đã nhập.",
                547 => "Dữ liệu liên quan không hợp lệ hoặc đang được sử dụng.",
                1205 => "Hệ thống đang bận. Vui lòng thử lại.",
                _ => "Không thể hoàn tất thao tác với dữ liệu. Vui lòng kiểm tra kết nối và thử lại."
            };
        }

        return "Không thể hoàn tất thao tác. Vui lòng thử lại hoặc liên hệ quản trị viên.";
    }
}
