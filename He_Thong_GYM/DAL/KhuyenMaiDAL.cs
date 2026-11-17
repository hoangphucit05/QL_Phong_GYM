using Microsoft.Data.SqlClient;using He_Thong_GYM.Models;
namespace He_Thong_GYM.DAL;
public sealed class KhuyenMaiDAL{public async Task<List<KhuyenMai>> LayTatCaAsync(){const string q="SELECT MaKM,TenKM,PhanTramGiam FROM dbo.KhuyenMai ORDER BY MaKM DESC;";var k=new List<KhuyenMai>();await using var c=DbConnectionFactory.CreateConnection();await using var m=new SqlCommand(q,c);await c.OpenAsync();await using var r=await m.ExecuteReaderAsync();while(await r.ReadAsync())k.Add(new KhuyenMai{MaKM=r.GetInt32(0),TenKM=r.GetString(1),PhanTramGiam=r.GetDecimal(2)});return k;}}
