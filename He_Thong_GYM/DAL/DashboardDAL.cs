using Microsoft.Data.SqlClient;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.DAL;

public sealed class DashboardDAL
{
    public async Task<int> CapNhatGoiHetHanAsync(int? maNV)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand("dbo.sp_CapNhatGoiHetHan", connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        command.Parameters.Add("@MaNV", System.Data.SqlDbType.Int).Value = (object?)maNV ?? DBNull.Value;
        var output = command.Parameters.Add("@SoLuongCapNhat", System.Data.SqlDbType.Int);
        output.Direction = System.Data.ParameterDirection.Output;
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
        return output.Value is DBNull ? 0 : Convert.ToInt32(output.Value);
    }

    public async Task<TongQuanDashboard> LayTongQuanAsync()
    {
        const string sql = """
            SELECT COUNT(*) FROM dbo.ThanhVien;
            SELECT COUNT(*) FROM dbo.DangKyGoi
            WHERE TrangThai = N'Còn hạn'
              AND NgayKetThuc BETWEEN CAST(GETDATE() AS date) AND DATEADD(day, 7, CAST(GETDATE() AS date));
            SELECT ISNULL(SUM(SoTien), 0) FROM dbo.ThanhToan
            WHERE TrangThai = N'Thành công' AND CAST(NgayThanhToan AS date) = CAST(GETDATE() AS date);
            SELECT COUNT(*) FROM dbo.LopTap WHERE TrangThai = N'Đang mở';
            """;

        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        var tongQuan = new TongQuanDashboard();
        if (await reader.ReadAsync()) tongQuan.TongThanhVien = reader.GetInt32(0);
        if (await reader.NextResultAsync() && await reader.ReadAsync()) tongQuan.GoiSapHetHan = reader.GetInt32(0);
        if (await reader.NextResultAsync() && await reader.ReadAsync()) tongQuan.DoanhThuHomNay = reader.GetDecimal(0);
        if (await reader.NextResultAsync() && await reader.ReadAsync()) tongQuan.LopDangMo = reader.GetInt32(0);
        return tongQuan;
    }
}
