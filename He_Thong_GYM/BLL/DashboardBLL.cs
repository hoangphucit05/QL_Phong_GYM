using He_Thong_GYM.DAL;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.BLL;

public sealed class DashboardBLL
{
    private readonly DashboardDAL _dashboardDAL = new();
    public Task<TongQuanDashboard> LayTongQuanAsync() => _dashboardDAL.LayTongQuanAsync();
}
