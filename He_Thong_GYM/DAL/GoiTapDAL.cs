using System.Data;
using He_Thong_GYM.Models;
using Microsoft.Data.SqlClient;

namespace He_Thong_GYM.DAL;

public sealed class GoiTapDAL
{
    public async Task<List<GoiTap>> LayDanhSachAsync(string? tuKhoa = null)
    {
        const string sql = """
            SELECT MaGoi,MaGoiTap,TenGoi,ThoiHanNgay,Gia,MoTa,TrangThai FROM dbo.GoiTap
            WHERE @TuKhoa IS NULL OR MaGoiTap LIKE '%' + @TuKhoa + '%' OR TenGoi LIKE N'%' + @TuKhoa + N'%'
            ORDER BY MaGoi DESC;
            """;
        var result = new List<GoiTap>();
        await using var cn = DbConnectionFactory.CreateConnection();
        await using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add("@TuKhoa", SqlDbType.NVarChar, 100).Value = string.IsNullOrWhiteSpace(tuKhoa) ? DBNull.Value : tuKhoa.Trim();
        await cn.OpenAsync();
        await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync())
        {
            result.Add(new GoiTap
            {
                MaGoi = rd.GetInt32(0),
                MaGoiTap = rd.GetString(1),
                TenGoi = rd.GetString(2),
                ThoiHanNgay = rd.GetInt32(3),
                Gia = rd.GetDecimal(4),
                MoTa = rd.IsDBNull(5) ? null : rd.GetString(5),
                TrangThai = rd.GetString(6)
            });
        }
        return result;
    }

    public async Task<List<PhongTapModel>> LayPhongHoatDongAsync()
    {
        const string sql = """
            SELECT MaPhong,MaPhongTap,TenPhong,LoaiPhong,SucChua,DienTich,Tang,TrangThai
            FROM dbo.PhongTap
            WHERE TrangThai=N'Hoạt động'
            ORDER BY Tang,TenPhong;
            """;
        var result = new List<PhongTapModel>();
        await using var cn = DbConnectionFactory.CreateConnection();
        await using var cmd = new SqlCommand(sql, cn);
        await cn.OpenAsync();
        await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync())
        {
            result.Add(new PhongTapModel
            {
                MaPhong = rd.GetInt32(0),
                MaPhongTap = rd.GetString(1),
                TenPhong = rd.GetString(2),
                LoaiPhong = rd.GetString(3),
                SucChua = rd.GetInt32(4),
                DienTich = rd.IsDBNull(5) ? null : rd.GetDecimal(5),
                Tang = rd.GetInt32(6),
                TrangThai = rd.GetString(7)
            });
        }
        return result;
    }

    public async Task<CauHinhQuyenGoiTap> LayQuyenAsync(int maGoi)
    {
        const string sql = """
            SELECT MaDichVu FROM dbo.GoiTapDichVu WHERE MaGoi=@MaGoi AND TrangThai=1;
            SELECT MaPhong FROM dbo.GoiTapPhong WHERE MaGoi=@MaGoi AND TrangThai=1;
            """;
        var result = new CauHinhQuyenGoiTap();
        await using var cn = DbConnectionFactory.CreateConnection();
        await using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add("@MaGoi", SqlDbType.Int).Value = maGoi;
        await cn.OpenAsync();
        await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync()) result.MaDichVu.Add(rd.GetString(0));
        await rd.NextResultAsync();
        while (await rd.ReadAsync()) result.MaPhong.Add(rd.GetInt32(0));
        return result;
    }

    public async Task<int> LuuAsync(
        GoiTap goiTap,
        IReadOnlyCollection<GoiTapDichVuOption> dichVu,
        IReadOnlyCollection<int> maPhong)
    {
        await using var cn = DbConnectionFactory.CreateConnection();
        await cn.OpenAsync();
        using var transaction = cn.BeginTransaction();

        try
        {
            int maGoi;
            const string setOptions = """
                SET ANSI_NULLS ON;
                SET QUOTED_IDENTIFIER ON;
                SET ANSI_WARNINGS ON;
                SET ARITHABORT ON;
                SET CONCAT_NULL_YIELDS_NULL ON;
                SET NUMERIC_ROUNDABORT OFF;
                """;

            if (goiTap.MaGoi == 0)
            {
                const string sql = setOptions + """
                    INSERT dbo.GoiTap(TenGoi,ThoiHanNgay,Gia,MoTa,TrangThai)
                    VALUES(@TenGoi,@ThoiHanNgay,@Gia,@MoTa,@TrangThai);
                    SELECT CONVERT(INT,SCOPE_IDENTITY());
                    """;
                await using var cmd = new SqlCommand(sql, cn, transaction);
                AddGoiParameters(cmd, goiTap);
                maGoi = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
            else
            {
                const string sql = setOptions + """
                    UPDATE dbo.GoiTap
                    SET TenGoi=@TenGoi,ThoiHanNgay=@ThoiHanNgay,Gia=@Gia,MoTa=@MoTa,TrangThai=@TrangThai
                    WHERE MaGoi=@MaGoi;
                    """;
                await using var cmd = new SqlCommand(sql, cn, transaction);
                AddGoiParameters(cmd, goiTap);
                await cmd.ExecuteNonQueryAsync();
                maGoi = goiTap.MaGoi;
            }

            await using (var cmd = new SqlCommand("DELETE FROM dbo.GoiTapDichVu WHERE MaGoi=@MaGoi; DELETE FROM dbo.GoiTapPhong WHERE MaGoi=@MaGoi;", cn, transaction))
            {
                cmd.Parameters.Add("@MaGoi", SqlDbType.Int).Value = maGoi;
                await cmd.ExecuteNonQueryAsync();
            }

            foreach (var dichVuItem in dichVu)
            {
                await using var cmd = new SqlCommand(
                    "INSERT dbo.GoiTapDichVu(MaGoi,MaDichVu,TenDichVu) VALUES(@MaGoi,@MaDichVu,@TenDichVu);",
                    cn, transaction);
                cmd.Parameters.Add("@MaGoi", SqlDbType.Int).Value = maGoi;
                cmd.Parameters.Add("@MaDichVu", SqlDbType.VarChar, 30).Value = dichVuItem.MaDichVu;
                cmd.Parameters.Add("@TenDichVu", SqlDbType.NVarChar, 100).Value = dichVuItem.TenDichVu;
                await cmd.ExecuteNonQueryAsync();
            }

            foreach (var phong in maPhong.Distinct())
            {
                await using var cmd = new SqlCommand(
                    "INSERT dbo.GoiTapPhong(MaGoi,MaPhong) VALUES(@MaGoi,@MaPhong);",
                    cn, transaction);
                cmd.Parameters.Add("@MaGoi", SqlDbType.Int).Value = maGoi;
                cmd.Parameters.Add("@MaPhong", SqlDbType.Int).Value = phong;
                await cmd.ExecuteNonQueryAsync();
            }

            transaction.Commit();
            return maGoi;
        }
        catch
        {
            try { transaction.Rollback(); } catch { /* giữ lại lỗi gốc */ }
            throw;
        }
    }

    public async Task NgungApDungAsync(int maGoi)
    {
        await using var cn = DbConnectionFactory.CreateConnection();
        await using var cmd = new SqlCommand("UPDATE dbo.GoiTap SET TrangThai=N'Ngừng áp dụng' WHERE MaGoi=@MaGoi;", cn);
        cmd.Parameters.Add("@MaGoi", SqlDbType.Int).Value = maGoi;
        await cn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    private static void AddGoiParameters(SqlCommand cmd, GoiTap goiTap)
    {
        cmd.Parameters.Add("@MaGoi", SqlDbType.Int).Value = goiTap.MaGoi;
        cmd.Parameters.Add("@TenGoi", SqlDbType.NVarChar, 100).Value = goiTap.TenGoi;
        cmd.Parameters.Add("@ThoiHanNgay", SqlDbType.Int).Value = goiTap.ThoiHanNgay;
        cmd.Parameters.Add("@Gia", SqlDbType.Decimal).Value = goiTap.Gia;
        cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar, 255).Value = (object?)goiTap.MoTa ?? DBNull.Value;
        cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 30).Value = goiTap.TrangThai;
    }
}
