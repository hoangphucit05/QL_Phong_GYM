using He_Thong_GYM.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace He_Thong_GYM.DAL;

public sealed class VanHanhDAL
{
    public async Task<List<PhienCheckInView>> LayPhienCheckInMoAsync()
    {
        const string sql = """
            SELECT c.MaCheckIn,c.MaPhienCheckIn,t.MaTV,c.MaDKG,t.MaThanhVien,t.HoTen,g.TenGoi,c.MaPhong,p.MaPhongTap,p.TenPhong,c.ThoiGianVao
            FROM dbo.LichSuCheckIn c
            JOIN dbo.ThanhVien t ON t.MaTV=c.MaTV
            JOIN dbo.DangKyGoi d ON d.MaDKG=c.MaDKG
            JOIN dbo.GoiTap g ON g.MaGoi=d.MaGoi
            LEFT JOIN dbo.PhongTap p ON p.MaPhong=c.MaPhong
            WHERE c.ThoiGianRa IS NULL
            ORDER BY c.ThoiGianVao ASC;
            """;
        var ketQua = new List<PhienCheckInView>();
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            ketQua.Add(new PhienCheckInView
            {
                MaCheckIn = reader.GetInt64(0),
                MaPhienCheckIn = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                MaTV = reader.GetInt32(2),
                MaDKG = reader.GetInt32(3),
                MaThanhVien = reader.GetString(4),
                HoTen = reader.GetString(5),
                TenGoi = reader.GetString(6),
                MaPhong = reader.IsDBNull(7) ? null : reader.GetInt32(7),
                MaPhongTap = reader.IsDBNull(8) ? string.Empty : reader.GetString(8),
                TenPhong = reader.IsDBNull(9) ? string.Empty : reader.GetString(9),
                ThoiGianVao = reader.GetDateTime(10)
            });
        }
        return ketQua;
    }

    public async Task<List<DangKyGoiView>> LayGoiThanhVienAsync(int maTV)
    {
        const string sql = """
            SELECT d.MaDKG,d.MaGoi,d.MaDangKyGoi,g.TenGoi,
                   COALESCE((SELECT STRING_AGG(q.TenDichVu,N', ') WITHIN GROUP (ORDER BY q.TenDichVu)
                             FROM dbo.GoiTapDichVu q
                             WHERE q.MaGoi=g.MaGoi AND q.TrangThai=1),N'Chưa cấu hình') AS QuyenDichVu,
                   d.NgayBatDau,d.NgayKetThuc,d.TrangThai
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
            ketQua.Add(new DangKyGoiView
            {
                MaDKG = reader.GetInt32(0),
                MaGoi = reader.GetInt32(1),
                MaDangKyGoi = reader.GetString(2),
                TenGoi = reader.GetString(3),
                QuyenDichVu = reader.GetString(4),
                NgayBatDau = reader.GetDateTime(5),
                NgayKetThuc = reader.GetDateTime(6),
                TrangThai = reader.GetString(7)
            });
        return ketQua;
    }

    public async Task<List<PhongTapModel>> LayPhongDuocPhepAsync(int maDKG)
    {
        const string sql = """
            SELECT p.MaPhong,p.MaPhongTap,p.TenPhong,p.LoaiPhong,p.SucChua,p.DienTich,p.Tang,p.TrangThai
            FROM dbo.GoiTapPhong qp
            JOIN dbo.DangKyGoi d ON d.MaGoi=qp.MaGoi
            JOIN dbo.PhongTap p ON p.MaPhong=qp.MaPhong
            WHERE d.MaDKG=@MaDKG AND qp.TrangThai=1 AND p.TrangThai=N'Hoạt động'
            ORDER BY p.Tang,p.TenPhong;
            """;
        var ketQua = new List<PhongTapModel>();
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@MaDKG", SqlDbType.Int).Value = maDKG;
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            ketQua.Add(new PhongTapModel
            {
                MaPhong = reader.GetInt32(0),
                MaPhongTap = reader.GetString(1),
                TenPhong = reader.GetString(2),
                LoaiPhong = reader.GetString(3),
                SucChua = reader.GetInt32(4),
                DienTich = reader.IsDBNull(5) ? null : reader.GetDecimal(5),
                Tang = reader.GetInt32(6),
                TrangThai = reader.GetString(7)
            });
        }
        return ketQua;
    }

    public async Task<string> CheckInAsync(int maTV, int maDKG, int? maPhong, int maNV)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand("dbo.sp_CheckIn", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.Add("@MaTV", SqlDbType.Int).Value = maTV;
        command.Parameters.Add("@MaDKG", SqlDbType.Int).Value = maDKG;
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

    public async Task<bool> CoPhienCheckInMoAsync(int maTV)
    {
        const string sql = "SELECT CONVERT(bit, CASE WHEN EXISTS (SELECT 1 FROM dbo.LichSuCheckIn WHERE MaTV=@MaTV AND ThoiGianRa IS NULL) THEN 1 ELSE 0 END);";
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@MaTV", SqlDbType.Int).Value = maTV;
        await connection.OpenAsync();
        return Convert.ToBoolean(await command.ExecuteScalarAsync());
    }

    public async Task<int?> LayMaPhongPhienMoAsync(int maTV)
    {
        const string sql = "SELECT TOP (1) MaPhong FROM dbo.LichSuCheckIn WHERE MaTV=@MaTV AND ThoiGianRa IS NULL ORDER BY ThoiGianVao DESC;";
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@MaTV", SqlDbType.Int).Value = maTV;
        await connection.OpenAsync();
        var value = await command.ExecuteScalarAsync();
        return value is null or DBNull ? null : Convert.ToInt32(value);
    }

    public async Task ChuyenPhongAsync(int maTV, int maPhongMoi, int maNV)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand("dbo.sp_ChuyenPhongCheckIn", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.Add("@MaTV", SqlDbType.Int).Value = maTV;
        command.Parameters.Add("@MaPhongMoi", SqlDbType.Int).Value = maPhongMoi;
        command.Parameters.Add("@MaNV", SqlDbType.Int).Value = maNV;
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
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

    public async Task MoLaiBaoLuuAsync(int maDKG, int? maNV)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand("dbo.sp_MoLaiBaoLuuGoiTap", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@MaDKG", maDKG);
        command.Parameters.AddWithValue("@MaNV", (object?)maNV ?? DBNull.Value);
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }
}
