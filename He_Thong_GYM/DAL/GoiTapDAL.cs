using Microsoft.Data.SqlClient;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.DAL;

public sealed class GoiTapDAL
{
    public async Task<List<GoiTap>> LayDanhSachAsync(string? tuKhoa = null)
    {
        const string sql = """
            SELECT MaGoi,TenGoi,ThoiHanNgay,Gia,MoTa,TrangThai FROM dbo.GoiTap
            WHERE @TuKhoa IS NULL OR TenGoi LIKE N'%' + @TuKhoa + N'%'
            ORDER BY MaGoi DESC;
            """;
        var result = new List<GoiTap>();
        await using var cn = DbConnectionFactory.CreateConnection(); await using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add("@TuKhoa", System.Data.SqlDbType.NVarChar, 100).Value = string.IsNullOrWhiteSpace(tuKhoa) ? DBNull.Value : tuKhoa.Trim();
        await cn.OpenAsync(); await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync()) result.Add(new GoiTap { MaGoi=rd.GetInt32(0), TenGoi=rd.GetString(1), ThoiHanNgay=rd.GetInt32(2), Gia=rd.GetDecimal(3), MoTa=rd.IsDBNull(4)?null:rd.GetString(4), TrangThai=rd.GetString(5) });
        return result;
    }

    public Task<int> ThemAsync(GoiTap goiTap) => LuuAsync("""INSERT dbo.GoiTap(TenGoi,ThoiHanNgay,Gia,MoTa,TrangThai) VALUES(@TenGoi,@ThoiHanNgay,@Gia,@MoTa,@TrangThai); SELECT CAST(SCOPE_IDENTITY() AS int);""", goiTap, true);
    public Task<int> CapNhatAsync(GoiTap goiTap) => LuuAsync("""UPDATE dbo.GoiTap SET TenGoi=@TenGoi,ThoiHanNgay=@ThoiHanNgay,Gia=@Gia,MoTa=@MoTa,TrangThai=@TrangThai WHERE MaGoi=@MaGoi;""", goiTap, false);
    public async Task NgungApDungAsync(int maGoi)
    {
        await using var cn=DbConnectionFactory.CreateConnection(); await using var cmd=new SqlCommand("UPDATE dbo.GoiTap SET TrangThai=N'Ngừng áp dụng' WHERE MaGoi=@MaGoi;",cn);
        cmd.Parameters.Add("@MaGoi",System.Data.SqlDbType.Int).Value=maGoi; await cn.OpenAsync(); await cmd.ExecuteNonQueryAsync();
    }
    private static async Task<int> LuuAsync(string sql,GoiTap g,bool identity)
    {
        await using var cn=DbConnectionFactory.CreateConnection(); await using var cmd=new SqlCommand(sql,cn);
        cmd.Parameters.Add("@MaGoi",System.Data.SqlDbType.Int).Value=g.MaGoi; cmd.Parameters.Add("@TenGoi",System.Data.SqlDbType.NVarChar,100).Value=g.TenGoi;
        cmd.Parameters.Add("@ThoiHanNgay",System.Data.SqlDbType.Int).Value=g.ThoiHanNgay; cmd.Parameters.Add("@Gia",System.Data.SqlDbType.Decimal).Value=g.Gia;
        cmd.Parameters.Add("@MoTa",System.Data.SqlDbType.NVarChar,255).Value=(object?)g.MoTa??DBNull.Value; cmd.Parameters.Add("@TrangThai",System.Data.SqlDbType.NVarChar,30).Value=g.TrangThai;
        await cn.OpenAsync(); return identity?(int)(await cmd.ExecuteScalarAsync()??throw new InvalidOperationException()):await cmd.ExecuteNonQueryAsync();
    }
}
