namespace He_Thong_GYM.Helpers;

public static class AuthorizationPolicy
{
private static readonly IReadOnlyDictionary<string, string[]> MenuRoles =
new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
{
["Tổng quan"] = [],
["Quản lý thành viên"] = ["Admin", "Quản lý", "Lễ tân", "Nhân viên"],
["Quản lý gói tập"] = ["Admin", "Quản lý"],
["Đăng ký gói tập"] = ["Admin", "Quản lý", "Lễ tân", "Nhân viên"],
["Vận hành gói / Check-in"] = ["Admin", "Quản lý", "Lễ tân", "Nhân viên"],
["Khuyến mãi"] = ["Admin", "Quản lý"],
["Huấn luyện viên / PT"] = ["Admin", "Quản lý", "HLV", "Huấn luyện viên"],
["Đăng ký PT"] = ["Admin", "Quản lý", "Lễ tân", "Nhân viên"],
["Lớp tập"] = ["Admin", "Quản lý", "HLV", "Huấn luyện viên"],
["Đăng ký và điểm danh"] = ["Admin", "Quản lý", "Lễ tân", "Nhân viên", "HLV", "Huấn luyện viên"],
["Hóa đơn và thanh toán"] = ["Admin", "Quản lý", "Kế toán", "Lễ tân"],
["Sản phẩm và bán hàng"] = ["Admin", "Quản lý", "Nhân viên bán hàng", "Nhân viên"],
["Thiết bị"] = ["Admin", "Quản lý", "Nhân viên"],
["Bảo trì thiết bị"] = ["Admin", "Quản lý", "Nhân viên"],
["Phòng tập"] = ["Admin", "Quản lý"],
["Nhân viên và tài khoản"] = ["Admin", "Quản lý"],
["Tài khoản và phân quyền"] = ["Admin"],
["Báo cáo thống kê"] = ["Admin", "Quản lý", "Kế toán"]
};

public static bool CanAccessMenu(string? menu) =>
!string.IsNullOrWhiteSpace(menu) &&
(!MenuRoles.TryGetValue(menu, out var roles) || roles.Length == 0 || SessionManager.CoVaiTro(roles));

public static void RequireMenu(string menu)
{
if (!CanAccessMenu(menu)) throw new UnauthorizedAccessException($"Bạn không có quyền truy cập chức năng: {menu}.");
}

public static void RequireAuthenticated()
{
if (SessionManager.NguoiDungHienTai is null) throw new UnauthorizedAccessException("Phiên đăng nhập không hợp lệ hoặc đã hết hạn.");
}

public static void Require(params string[] roles)
{
RequireAuthenticated();
if (!SessionManager.CoVaiTro(roles)) throw new UnauthorizedAccessException("Tài khoản hiện tại không có quyền thực hiện thao tác này.");
}
}
