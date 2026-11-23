using He_Thong_GYM.Models;
using Microsoft.Data.SqlClient;

namespace He_Thong_GYM.DAL;

public sealed class NhanVienDAL
{
    public async Task<List<NhanVien>> LayNhanVienDangLamAsync()
    {
        const string sql="SELECT MaNV,MaNhanVien,HoTen,NgaySinh,GioiTinh,SDT,Email,DiaChi,ChucVu,Luong,TrangThai FROM dbo.NhanVien WHERE TrangThai=N'Đang làm' ORDER BY HoTen;";
        var danhSach=new List<NhanVien>(); await using var cn=DbConnectionFactory.CreateConnection();await using var cmd=new SqlCommand(sql,cn);await cn.OpenAsync();await using var rd=await cmd.ExecuteReaderAsync();
        while(await rd.ReadAsync())danhSach.Add(new NhanVien{MaNV=rd.GetInt32(0),MaNhanVien=rd.GetString(1),HoTen=rd.GetString(2),NgaySinh=rd.GetDateTime(3),GioiTinh=rd.IsDBNull(4)?null:rd.GetString(4),SDT=rd.GetString(5),Email=rd.IsDBNull(6)?null:rd.GetString(6),DiaChi=rd.IsDBNull(7)?null:rd.GetString(7),ChucVu=rd.GetString(8),Luong=rd.IsDBNull(9)?null:rd.GetDecimal(9),TrangThai=rd.GetString(10)});
        return danhSach;
    }

    public async Task<List<NhanVien>> LayNhanVienChuaCoTaiKhoanAsync()
    {
        const string sql = """
            SELECT nv.MaNV, nv.MaNhanVien, nv.HoTen, nv.NgaySinh, nv.GioiTinh, nv.SDT, nv.Email, nv.DiaChi, nv.ChucVu, nv.Luong, nv.TrangThai
            FROM dbo.NhanVien nv
            WHERE nv.TrangThai = N'Đang làm'
              AND NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan tk WHERE tk.MaNV = nv.MaNV)
            ORDER BY nv.HoTen;
            """;
        var danhSach = new List<NhanVien>();
        await using var cn = DbConnectionFactory.CreateConnection();
        await using var cmd = new SqlCommand(sql, cn);
        await cn.OpenAsync();
        await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync())
            danhSach.Add(new NhanVien { MaNV=rd.GetInt32(0),MaNhanVien=rd.GetString(1),HoTen=rd.GetString(2),NgaySinh=rd.GetDateTime(3),GioiTinh=rd.IsDBNull(4)?null:rd.GetString(4),SDT=rd.GetString(5),Email=rd.IsDBNull(6)?null:rd.GetString(6),DiaChi=rd.IsDBNull(7)?null:rd.GetString(7),ChucVu=rd.GetString(8),Luong=rd.IsDBNull(9)?null:rd.GetDecimal(9),TrangThai=rd.GetString(10) });
        return danhSach;
    }

    public async Task<List<NhanVien>> LayDanhSachAsync(string? tuKhoa = null)
    {
        const string sql = """
            SELECT nv.MaNV, nv.MaNhanVien, nv.HoTen, nv.NgaySinh, nv.GioiTinh, nv.SDT, nv.Email, nv.DiaChi, nv.ChucVu, nv.Luong, nv.TrangThai,
                   tk.TenDangNhap, tk.VaiTro
            FROM dbo.NhanVien nv
            LEFT JOIN dbo.TaiKhoan tk ON tk.MaNV = nv.MaNV
            WHERE @TuKhoa IS NULL 
               OR nv.MaNhanVien LIKE '%' + @TuKhoa + '%' 
               OR nv.HoTen LIKE N'%' + @TuKhoa + N'%' 
               OR nv.SDT LIKE '%' + @TuKhoa + '%'
               OR tk.TenDangNhap LIKE '%' + @TuKhoa + '%'
            ORDER BY nv.MaNV DESC;
            """;
        var danhSach = new List<NhanVien>();
        await using var cn = DbConnectionFactory.CreateConnection();
        await using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add("@TuKhoa", System.Data.SqlDbType.NVarChar, 100).Value = string.IsNullOrWhiteSpace(tuKhoa) ? DBNull.Value : tuKhoa.Trim();
        await cn.OpenAsync(); await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync())
            danhSach.Add(new NhanVien 
            { 
                MaNV=rd.GetInt32(0),
                MaNhanVien=rd.GetString(1),
                HoTen=rd.GetString(2),
                NgaySinh=rd.GetDateTime(3),
                GioiTinh=rd.IsDBNull(4)?null:rd.GetString(4),
                SDT=rd.GetString(5),
                Email=rd.IsDBNull(6)?null:rd.GetString(6),
                DiaChi=rd.IsDBNull(7)?null:rd.GetString(7),
                ChucVu=rd.GetString(8),
                Luong=rd.IsDBNull(9)?null:rd.GetDecimal(9),
                TrangThai=rd.GetString(10),
                TenDangNhap=rd.IsDBNull(11)?null:rd.GetString(11),
                VaiTro=rd.IsDBNull(12)?null:rd.GetString(12)
            });
        return danhSach;
    }

    public Task<int> ThemAsync(NhanVien nhanVien) => LuuAsync("""INSERT dbo.NhanVien(HoTen,NgaySinh,GioiTinh,SDT,Email,DiaChi,ChucVu,Luong,TrangThai) VALUES(@HoTen,@NgaySinh,@GioiTinh,@SDT,@Email,@DiaChi,@ChucVu,@Luong,@TrangThai);SELECT CAST(SCOPE_IDENTITY() AS int);""", nhanVien, true);
    public Task<int> CapNhatAsync(NhanVien nhanVien) => LuuAsync("""UPDATE dbo.NhanVien SET HoTen=@HoTen,NgaySinh=@NgaySinh,GioiTinh=@GioiTinh,SDT=@SDT,Email=@Email,DiaChi=@DiaChi,ChucVu=@ChucVu,Luong=@Luong,TrangThai=@TrangThai WHERE MaNV=@MaNV;""", nhanVien, false);
    public async Task NghiViecAsync(int maNV)
    {
        await using var cn=DbConnectionFactory.CreateConnection();await cn.OpenAsync();await using var transaction=await cn.BeginTransactionAsync();
        try
        {
            await using var cmdNhanVien=new SqlCommand("UPDATE dbo.NhanVien SET TrangThai=N'Nghỉ việc' WHERE MaNV=@MaNV;",cn,(SqlTransaction)transaction);cmdNhanVien.Parameters.Add("@MaNV",System.Data.SqlDbType.Int).Value=maNV;if(await cmdNhanVien.ExecuteNonQueryAsync()!=1)throw new InvalidOperationException("Không tìm thấy nhân viên.");
            await using var cmdTaiKhoan=new SqlCommand("UPDATE dbo.TaiKhoan SET TrangThai=N'Khóa' WHERE MaNV=@MaNV;",cn,(SqlTransaction)transaction);cmdTaiKhoan.Parameters.Add("@MaNV",System.Data.SqlDbType.Int).Value=maNV;await cmdTaiKhoan.ExecuteNonQueryAsync();await transaction.CommitAsync();
        }
        catch{await transaction.RollbackAsync();throw;}
    }
    private static async Task<int> LuuAsync(string sql,NhanVien n,bool identity)
    {
        await using var cn=DbConnectionFactory.CreateConnection();await using var cmd=new SqlCommand(sql,cn);
        cmd.Parameters.Add("@MaNV",System.Data.SqlDbType.Int).Value=n.MaNV;cmd.Parameters.Add("@HoTen",System.Data.SqlDbType.NVarChar,100).Value=n.HoTen;cmd.Parameters.Add("@NgaySinh",System.Data.SqlDbType.Date).Value=n.NgaySinh.Date;cmd.Parameters.Add("@GioiTinh",System.Data.SqlDbType.NVarChar,10).Value=(object?)n.GioiTinh??DBNull.Value;cmd.Parameters.Add("@SDT",System.Data.SqlDbType.VarChar,15).Value=n.SDT;cmd.Parameters.Add("@Email",System.Data.SqlDbType.VarChar,100).Value=(object?)n.Email??DBNull.Value;cmd.Parameters.Add("@DiaChi",System.Data.SqlDbType.NVarChar,255).Value=(object?)n.DiaChi??DBNull.Value;cmd.Parameters.Add("@ChucVu",System.Data.SqlDbType.NVarChar,100).Value=n.ChucVu;cmd.Parameters.Add("@Luong",System.Data.SqlDbType.Decimal).Value=(object?)n.Luong??DBNull.Value;cmd.Parameters.Add("@TrangThai",System.Data.SqlDbType.NVarChar,30).Value=n.TrangThai;
        await cn.OpenAsync();return identity?(int)(await cmd.ExecuteScalarAsync()??throw new InvalidOperationException()):await cmd.ExecuteNonQueryAsync();
    }
}
