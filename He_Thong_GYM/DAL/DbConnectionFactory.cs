using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace He_Thong_GYM.DAL;

public static class DbConnectionFactory
{
    private static readonly Lazy<string> ConnectionString = new(ReadConnectionString);

    public static SqlConnection CreateConnection() => new(ConnectionString.Value);

    private static string ReadConnectionString()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();
        var connectionString = configuration.GetConnectionString("QL_PhongGym");
        if (string.IsNullOrWhiteSpace(connectionString) || connectionString.Contains("YOUR_SERVER", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Chưa cấu hình SQL Server. Hãy cập nhật ConnectionStrings:QL_PhongGym trong appsettings.json.");
        }
        return connectionString;
    }
}
