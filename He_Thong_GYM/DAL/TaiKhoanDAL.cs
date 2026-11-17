using Microsoft.Data.SqlClient;
using He_Thong_GYM.Models;

namespace He_Thong_GYM.DAL;

public sealed class TaiKhoanDAL
{
    public async Task<List<TaiKhoan>> LayDanhSachAsync()
    {
        const string sql="SELECT tk.MaTK,tk.TenDangNhap,tk.MaNV,nv.HoTen,tk.VaiTro,tk.TrangThai FROM dbo.TaiKhoan tk JOIN dbo.NhanVien nv ON nv.MaNV=tk.MaNV ORDER BY tk.MaTK DESC;";
        var kq=new List<TaiKhoan>();await using var cn=DbConnectionFactory.CreateConnection();await using var cmd=new SqlCommand(sql,cn);await cn.OpenAsync();await using var rd=await cmd.ExecuteReaderAsync();while(await rd.ReadAsync())kq.Add(new TaiKhoan{MaTK=rd.GetInt32(0),TenDangNhap=rd.GetString(1),MaNV=rd.GetInt32(2),HoTenNhanVien=rd.GetString(3),VaiTro=rd.GetString(4),TrangThai=rd.GetString(5)});return kq;
    }

    public async Task<List<string>> LayVaiTroAsync()
    {
        const string sql="SELECT TenVaiTro FROM dbo.VaiTro ORDER BY TenVaiTro;";var kq=new List<string>();await using var cn=DbConnectionFactory.CreateConnection();await using var cmd=new SqlCommand(sql,cn);await cn.OpenAsync();await using var rd=await cmd.ExecuteReaderAsync();while(await rd.ReadAsync())kq.Add(rd.GetString(0));return kq;
    }

    public async Task<int> TaoTaiKhoanAsync(TaiKhoan taiKhoan,byte[] hash,byte[] salt)
    {
        const string sqlTaiKhoan="INSERT dbo.TaiKhoan(TenDangNhap,MatKhau,MatKhauHash,MatKhauSalt,VaiTro,MaNV,TrangThai) VALUES(@TenDangNhap,'',@Hash,@Salt,@VaiTro,@MaNV,N'Hoạt động');SELECT CAST(SCOPE_IDENTITY() AS int);";
        const string sqlVaiTro="INSERT dbo.TaiKhoanVaiTro(MaTK,MaVaiTro) SELECT @MaTK,MaVaiTro FROM dbo.VaiTro WHERE TenVaiTro=@VaiTro;";
        await using var cn=DbConnectionFactory.CreateConnection();await cn.OpenAsync();await using var transaction=await cn.BeginTransactionAsync();
        try
        {
            await using var cmd=new SqlCommand(sqlTaiKhoan,cn,(SqlTransaction)transaction);cmd.Parameters.Add("@TenDangNhap",System.Data.SqlDbType.VarChar,50).Value=taiKhoan.TenDangNhap;cmd.Parameters.Add("@Hash",System.Data.SqlDbType.VarBinary,64).Value=hash;cmd.Parameters.Add("@Salt",System.Data.SqlDbType.VarBinary,32).Value=salt;cmd.Parameters.Add("@VaiTro",System.Data.SqlDbType.NVarChar,50).Value=taiKhoan.VaiTro;cmd.Parameters.Add("@MaNV",System.Data.SqlDbType.Int).Value=taiKhoan.MaNV;var maTK=(int)(await cmd.ExecuteScalarAsync()??throw new InvalidOperationException());
            await using var cmdVaiTro=new SqlCommand(sqlVaiTro,cn,(SqlTransaction)transaction);cmdVaiTro.Parameters.Add("@MaTK",System.Data.SqlDbType.Int).Value=maTK;cmdVaiTro.Parameters.Add("@VaiTro",System.Data.SqlDbType.NVarChar,50).Value=taiKhoan.VaiTro;if(await cmdVaiTro.ExecuteNonQueryAsync()!=1)throw new InvalidOperationException("Vai trò không tồn tại.");
            await transaction.CommitAsync();return maTK;
        }
        catch{await transaction.RollbackAsync();throw;}
    }

    public async Task CapNhatTrangThaiAsync(int maTK,string trangThai)
    {
        await using var cn=DbConnectionFactory.CreateConnection();await using var cmd=new SqlCommand("UPDATE dbo.TaiKhoan SET TrangThai=@TrangThai WHERE MaTK=@MaTK;",cn);cmd.Parameters.Add("@MaTK",System.Data.SqlDbType.Int).Value=maTK;cmd.Parameters.Add("@TrangThai",System.Data.SqlDbType.NVarChar,30).Value=trangThai;await cn.OpenAsync();await cmd.ExecuteNonQueryAsync();
    }
    public async Task<NguoiDung?> LayTheoTenDangNhapAsync(string tenDangNhap)
    {
        const string sql = """
            SELECT tk.MaTK, tk.MaNV, tk.TenDangNhap, nv.HoTen, tk.VaiTro, tk.MatKhauHash, tk.MatKhauSalt
            FROM dbo.TaiKhoan tk
            INNER JOIN dbo.NhanVien nv ON nv.MaNV = tk.MaNV
            WHERE tk.TenDangNhap = @TenDangNhap AND tk.TrangThai = N'Hoạt động' AND nv.TrangThai = N'Đang làm';
            """;
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@TenDangNhap", System.Data.SqlDbType.VarChar, 50).Value = tenDangNhap;
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;
        return new NguoiDung
        {
            MaTK = reader.GetInt32(0), MaNV = reader.GetInt32(1), TenDangNhap = reader.GetString(2),
            HoTen = reader.GetString(3), VaiTro = reader.GetString(4),
            MatKhauHash = reader.IsDBNull(5) ? Array.Empty<byte>() : (byte[])reader.GetValue(5),
            MatKhauSalt = reader.IsDBNull(6) ? Array.Empty<byte>() : (byte[])reader.GetValue(6)
        };
    }

    public async Task CapNhatMatKhauAsync(int maTK, byte[] hash, byte[] salt)
    {
        const string sql = "UPDATE dbo.TaiKhoan SET MatKhauHash=@Hash, MatKhauSalt=@Salt, MatKhau='' WHERE MaTK=@MaTK;";
        await using var connection = DbConnectionFactory.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@MaTK", System.Data.SqlDbType.Int).Value = maTK;
        command.Parameters.Add("@Hash", System.Data.SqlDbType.VarBinary, 64).Value = hash;
        command.Parameters.Add("@Salt", System.Data.SqlDbType.VarBinary, 32).Value = salt;
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }
}
