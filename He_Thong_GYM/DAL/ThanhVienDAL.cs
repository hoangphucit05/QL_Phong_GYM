using Microsoft.Data.SqlClient;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.DAL;

public sealed class ThanhVienDAL
{
    public async Task<List<ThanhVien>> LayDanhSachAsync(string? tuKhoa = null)
    {
        const string sql = """
            SELECT tv.MaTV, tv.MaThanhVien, tv.HoTen, tv.NgaySinh, tv.GioiTinh, tv.SDT, tv.Email, tv.DiaChi, tv.NgayDangKy,
                   tv.MaLoaiTV, ltv.TenLoai, tv.TrangThai
            FROM dbo.ThanhVien tv INNER JOIN dbo.LoaiThanhVien ltv ON ltv.MaLoaiTV = tv.MaLoaiTV
            WHERE (@TuKhoa IS NULL OR tv.MaThanhVien LIKE '%' + @TuKhoa + '%' OR tv.HoTen LIKE N'%' + @TuKhoa + N'%' OR tv.SDT LIKE '%' + @TuKhoa + '%')
            ORDER BY tv.MaTV DESC;
            """;
        var danhSach = new List<ThanhVien>();
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@TuKhoa", System.Data.SqlDbType.NVarChar, 100).Value = string.IsNullOrWhiteSpace(tuKhoa) ? DBNull.Value : tuKhoa.Trim();
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            danhSach.Add(new ThanhVien
            {
                MaTV = reader.GetInt32(0), MaThanhVien = reader.GetString(1), HoTen = reader.GetString(2), NgaySinh = reader.GetDateTime(3),
                GioiTinh = reader.IsDBNull(4) ? null : reader.GetString(4), SDT = reader.GetString(5),
                Email = reader.IsDBNull(6) ? null : reader.GetString(6), DiaChi = reader.IsDBNull(7) ? null : reader.GetString(7),
                NgayDangKy = reader.GetDateTime(8), MaLoaiTV = reader.GetInt32(9), TenLoai = reader.GetString(10), TrangThai = reader.GetString(11)
            });
        }
        return danhSach;
    }

    public async Task<List<LoaiThanhVien>> LayLoaiThanhVienAsync()
    {
        const string sql = "SELECT MaLoaiTV, TenLoai FROM dbo.LoaiThanhVien ORDER BY TenLoai;";
        var danhSach = new List<LoaiThanhVien>();
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync()) danhSach.Add(new LoaiThanhVien { MaLoaiTV = reader.GetInt32(0), TenLoai = reader.GetString(1) });
        return danhSach;
    }

    public async Task<int> ThemAsync(ThanhVien thanhVien)
    {
        const string sql = """
            INSERT INTO dbo.ThanhVien (HoTen, NgaySinh, GioiTinh, SDT, Email, DiaChi, MaLoaiTV, TrangThai)
            VALUES (@HoTen, @NgaySinh, @GioiTinh, @SDT, @Email, @DiaChi, @MaLoaiTV, @TrangThai);
            SELECT CAST(SCOPE_IDENTITY() AS int);
            """;
        return await ThucThiLuuAsync(sql, thanhVien, true);
    }

    public Task CapNhatAsync(ThanhVien thanhVien) => ThucThiLuuAsync("""
        UPDATE dbo.ThanhVien SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, SDT=@SDT,
        Email=@Email, DiaChi=@DiaChi, MaLoaiTV=@MaLoaiTV, TrangThai=@TrangThai WHERE MaTV=@MaTV;
        """, thanhVien, false);

    public async Task NgungSuDungAsync(int maTV)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand("UPDATE dbo.ThanhVien SET TrangThai=N'Ngừng sử dụng' WHERE MaTV=@MaTV;", connection);
        command.Parameters.Add("@MaTV", System.Data.SqlDbType.Int).Value = maTV;
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    private static async Task<int> ThucThiLuuAsync(string sql, ThanhVien thanhVien, bool layIdentity)
    {
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@MaTV", System.Data.SqlDbType.Int).Value = thanhVien.MaTV;
        command.Parameters.Add("@HoTen", System.Data.SqlDbType.NVarChar, 100).Value = thanhVien.HoTen;
        command.Parameters.Add("@NgaySinh", System.Data.SqlDbType.Date).Value = thanhVien.NgaySinh.Date;
        command.Parameters.Add("@GioiTinh", System.Data.SqlDbType.NVarChar, 10).Value = (object?)thanhVien.GioiTinh ?? DBNull.Value;
        command.Parameters.Add("@SDT", System.Data.SqlDbType.VarChar, 15).Value = thanhVien.SDT;
        command.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 100).Value = (object?)thanhVien.Email ?? DBNull.Value;
        command.Parameters.Add("@DiaChi", System.Data.SqlDbType.NVarChar, 255).Value = (object?)thanhVien.DiaChi ?? DBNull.Value;
        command.Parameters.Add("@MaLoaiTV", System.Data.SqlDbType.Int).Value = thanhVien.MaLoaiTV;
        command.Parameters.Add("@TrangThai", System.Data.SqlDbType.NVarChar, 30).Value = thanhVien.TrangThai;
        await connection.OpenAsync();
        return layIdentity
            ? (int)(await command.ExecuteScalarAsync() ?? throw new InvalidOperationException("Không lấy được mã thành viên mới."))
            : await command.ExecuteNonQueryAsync();
    }
}
