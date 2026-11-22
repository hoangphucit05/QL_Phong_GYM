using He_Thong_GYM.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace He_Thong_GYM.DAL;

public sealed class VanHanhDAL
{
    public async Task<List<DangKyGoiView>> LayGoiThanhVienAsync(int maTV)
    {
        const string sql = """
            SELECT d.MaDKG,d.MaDangKyGoi,g.TenGoi,d.NgayBatDau,d.NgayKetThuc,d.TrangThai
            FROM dbo.DangKyGoi d JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi
            WHERE d.MaTV=@MaTV ORDER BY d.MaDKG DESC;
            """;
        var ketQua = new List<DangKyGoiView>();
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@MaTV", SqlDbType.Int).Value = maTV;
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            ketQua.Add(new DangKyGoiView { MaDKG = reader.GetInt32(0), MaDangKyGoi = reader.GetString(1), TenGoi = reader.GetString(2), NgayBatDau = reader.GetDateTime(3), NgayKetThuc = reader.GetDateTime(4), TrangThai = reader.GetString(5) });
        return ketQua;
    }

    public async Task<string> CheckInAsync(int maTV, int? maPhong, int maNV)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand("dbo.sp_CheckIn", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.Add("@MaTV", SqlDbType.Int).Value = maTV;
        command.Parameters.Add("@MaPhong", SqlDbType.Int).Value = (object?)maPhong ?? DBNull.Value;
        command.Parameters.Add("@Kenh", SqlDbType.NVarChar, 20).Value = "Quầy";
        command.Parameters.Add("@MaNV", SqlDbType.Int).Value = maNV;
        var output = command.Parameters.Add("@MaCheckIn", SqlDbType.BigInt); output.Direction = ParameterDirection.Output;
        await connection.OpenAsync(); await command.ExecuteNonQueryAsync();
        await using var codeCommand = new SqlCommand("SELECT MaPhienCheckIn FROM dbo.LichSuCheckIn WHERE MaCheckIn=@MaCheckIn;", connection);
        codeCommand.Parameters.Add("@MaCheckIn", SqlDbType.BigInt).Value = output.Value;
        return Convert.ToString(await codeCommand.ExecuteScalarAsync()) ?? $"CI-{output.Value}";
    }

    public async Task CheckOutAsync(int maTV, int maNV)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand("dbo.sp_CheckOut", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.Add("@MaTV", SqlDbType.Int).Value = maTV;
        command.Parameters.Add("@MaNV", SqlDbType.Int).Value = maNV;
        await connection.OpenAsync(); await command.ExecuteNonQueryAsync();
    }

    public async Task<int> GiaHanAsync(int maDKG, int soNgay, int? maNV)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand("dbo.sp_GiaHanGoiTap", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@MaDKG", maDKG);
        command.Parameters.AddWithValue("@SoNgay", soNgay);
        command.Parameters.AddWithValue("@MaNV", (object?)maNV ?? DBNull.Value);
        var output = command.Parameters.Add("@MaHD", SqlDbType.Int);
        output.Direction = ParameterDirection.Output;
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
        return output.Value is int maHD ? maHD : 0;
    }

    public async Task BaoLuuAsync(int maDKG, DateTime tuNgay, DateTime denNgay, string lyDo, int? maNV)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand("dbo.sp_BaoLuuGoiTap", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@MaDKG", maDKG); command.Parameters.AddWithValue("@TuNgay", tuNgay.Date); command.Parameters.AddWithValue("@DenNgay", denNgay.Date); command.Parameters.AddWithValue("@LyDo", lyDo); command.Parameters.AddWithValue("@MaNV", (object?)maNV ?? DBNull.Value);
        await connection.OpenAsync(); await command.ExecuteNonQueryAsync();
    }
}
