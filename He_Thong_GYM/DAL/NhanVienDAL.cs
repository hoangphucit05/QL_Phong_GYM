using He_Thong_GYM.Models;
using Microsoft.Data.SqlClient;

namespace He_Thong_GYM.DAL;

public sealed class NhanVienDAL
{
    public async Task<List<NhanVien>> LayNhanVienDangLamAsync()
    {
        const string sql="SELECT MaNV,HoTen,NgaySinh,GioiTinh,SDT,Email,DiaChi,ChucVu,Luong,TrangThai FROM dbo.NhanVien WHERE TrangThai=N'Đang làm' ORDER BY HoTen;";
        var danhSach=new List<NhanVien>(); await using var cn=DbConnectionFactory.CreateConnection();await using var cmd=new SqlCommand(sql,cn);await cn.OpenAsync();await using var rd=await cmd.ExecuteReaderAsync();
        while(await rd.ReadAsync())danhSach.Add(new NhanVien{MaNV=rd.GetInt32(0),HoTen=rd.GetString(1),NgaySinh=rd.GetDateTime(2),GioiTinh=rd.IsDBNull(3)?null:rd.GetString(3),SDT=rd.GetString(4),Email=rd.IsDBNull(5)?null:rd.GetString(5),DiaChi=rd.IsDBNull(6)?null:rd.GetString(6),ChucVu=rd.GetString(7),Luong=rd.IsDBNull(8)?null:rd.GetDecimal(8),TrangThai=rd.GetString(9)});
        return danhSach;
    }
    public async Task<List<NhanVien>> LayDanhSachAsync(string? tuKhoa = null)
    {
        const string sql = """
            SELECT MaNV,HoTen,NgaySinh,GioiTinh,SDT,Email,DiaChi,ChucVu,Luong,TrangThai
            FROM dbo.NhanVien
            WHERE @TuKhoa IS NULL OR HoTen LIKE N'%' + @TuKhoa + N'%' OR SDT LIKE '%' + @TuKhoa + '%'
            ORDER BY MaNV DESC;
            """;
        var danhSach = new List<NhanVien>();
        await using var cn = DbConnectionFactory.CreateConnection();
        await using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add("@TuKhoa", System.Data.SqlDbType.NVarChar, 100).Value = string.IsNullOrWhiteSpace(tuKhoa) ? DBNull.Value : tuKhoa.Trim();
        await cn.OpenAsync(); await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync())
            danhSach.Add(new NhanVien { MaNV=rd.GetInt32(0),HoTen=rd.GetString(1),NgaySinh=rd.GetDateTime(2),GioiTinh=rd.IsDBNull(3)?null:rd.GetString(3),SDT=rd.GetString(4),Email=rd.IsDBNull(5)?null:rd.GetString(5),DiaChi=rd.IsDBNull(6)?null:rd.GetString(6),ChucVu=rd.GetString(7),Luong=rd.IsDBNull(8)?null:rd.GetDecimal(8),TrangThai=rd.GetString(9) });
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
