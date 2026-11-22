using Microsoft.Data.SqlClient;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.DAL;

public sealed class KhuyenMaiDAL
{
    public async Task<List<KhuyenMai>> LayTatCaAsync()
    {
        const string q = "SELECT * FROM dbo.KhuyenMai ORDER BY MaKM DESC;";
        var list = new List<KhuyenMai>();
        await using var c = DbConnectionFactory.CreateConnection();
        await using var m = new SqlCommand(q, c);
        await c.OpenAsync();
        await using var r = await m.ExecuteReaderAsync();

        // Kiểm tra vị trí cột linh hoạt để tương thích cả khi database có hoặc chưa có cột MaCode
        var colMaKM = CoCot(r, "MaKM") ? r.GetOrdinal("MaKM") : -1;
        var colMaCode = CoCot(r, "MaCode") ? r.GetOrdinal("MaCode") : -1;
        var colTenKM = CoCot(r, "TenKM") ? r.GetOrdinal("TenKM") : -1;
        var colPhanTram = CoCot(r, "PhanTramGiam") ? r.GetOrdinal("PhanTramGiam") : -1;
        var colNgayBD = CoCot(r, "NgayBatDau") ? r.GetOrdinal("NgayBatDau") : -1;
        var colNgayKT = CoCot(r, "NgayKetThuc") ? r.GetOrdinal("NgayKetThuc") : -1;
        var colTrangThai = CoCot(r, "TrangThai") ? r.GetOrdinal("TrangThai") : -1;

        while (await r.ReadAsync())
        {
            var maKM = colMaKM >= 0 && !r.IsDBNull(colMaKM) ? r.GetInt32(colMaKM) : (int?)null;
            var tenKM = colTenKM >= 0 && !r.IsDBNull(colTenKM) ? r.GetString(colTenKM) : string.Empty;
            var giam = colPhanTram >= 0 && !r.IsDBNull(colPhanTram) ? r.GetDecimal(colPhanTram) : 0m;
            var maCode = colMaCode >= 0 && !r.IsDBNull(colMaCode) ? r.GetString(colMaCode) : null;

            if (string.IsNullOrWhiteSpace(maCode))
            {
                maCode = KhuyenMai.TaoMaCodeThucTe(tenKM, giam, maKM);
            }

            list.Add(new KhuyenMai
            {
                MaKM = maKM,
                MaCode = maCode,
                TenKM = tenKM,
                PhanTramGiam = giam,
                NgayBatDau = colNgayBD >= 0 && !r.IsDBNull(colNgayBD) ? r.GetDateTime(colNgayBD) : null,
                NgayKetThuc = colNgayKT >= 0 && !r.IsDBNull(colNgayKT) ? r.GetDateTime(colNgayKT) : null,
                TrangThai = colTrangThai >= 0 && !r.IsDBNull(colTrangThai) ? r.GetString(colTrangThai) : null
            });
        }
        return list;
    }

    private static bool CoCot(SqlDataReader reader, string columnName)
    {
        for (int i = 0; i < reader.FieldCount; i++)
        {
            if (string.Equals(reader.GetName(i), columnName, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }

    public async Task ThemAsync(KhuyenMai km)
    {
        const string q = "INSERT dbo.KhuyenMai(TenKM,PhanTramGiam,NgayBatDau,NgayKetThuc,TrangThai,MaCode) VALUES(@Ten,@Giam,@BD,@KT,@TT,@Code);";
        await using var c = DbConnectionFactory.CreateConnection();
        await using var m = new SqlCommand(q, c);
        m.Parameters.AddWithValue("@Ten", km.TenKM);
        m.Parameters.AddWithValue("@Giam", km.PhanTramGiam);
        m.Parameters.AddWithValue("@BD", km.NgayBatDau.HasValue ? km.NgayBatDau.Value.Date : (object)DBNull.Value);
        m.Parameters.AddWithValue("@KT", km.NgayKetThuc.HasValue ? km.NgayKetThuc.Value.Date : (object)DBNull.Value);
        m.Parameters.AddWithValue("@TT", string.IsNullOrWhiteSpace(km.TrangThai) ? "Hoạt động" : km.TrangThai);
        m.Parameters.AddWithValue("@Code", string.IsNullOrWhiteSpace(km.MaCode) ? KhuyenMai.TaoMaCodeThucTe(km.TenKM, km.PhanTramGiam, null) : km.MaCode.Trim().ToUpperInvariant());
        await c.OpenAsync();
        await m.ExecuteNonQueryAsync();
    }

    public async Task CapNhatAsync(KhuyenMai km)
    {
        const string q = "UPDATE dbo.KhuyenMai SET TenKM=@Ten,PhanTramGiam=@Giam,NgayBatDau=@BD,NgayKetThuc=@KT,TrangThai=@TrangThai,MaCode=@Code WHERE MaKM=@MaKM;";
        await using var c = DbConnectionFactory.CreateConnection();
        await using var m = new SqlCommand(q, c);
        m.Parameters.AddWithValue("@Ten", km.TenKM);
        m.Parameters.AddWithValue("@Giam", km.PhanTramGiam);
        m.Parameters.AddWithValue("@BD", km.NgayBatDau.HasValue ? km.NgayBatDau.Value.Date : (object)DBNull.Value);
        m.Parameters.AddWithValue("@KT", km.NgayKetThuc.HasValue ? km.NgayKetThuc.Value.Date : (object)DBNull.Value);
        m.Parameters.AddWithValue("@TrangThai", km.TrangThai ?? "Hoạt động");
        m.Parameters.AddWithValue("@Code", string.IsNullOrWhiteSpace(km.MaCode) ? KhuyenMai.TaoMaCodeThucTe(km.TenKM, km.PhanTramGiam, km.MaKM) : km.MaCode.Trim().ToUpperInvariant());
        m.Parameters.AddWithValue("@MaKM", km.MaKM ?? 0);
        await c.OpenAsync();
        await m.ExecuteNonQueryAsync();
    }
}
