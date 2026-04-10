using DeviceManagement.Api.Data.Interfaces;
using Microsoft.Data.SqlClient;
namespace DeviceManagement.Api.Data;

public class DatabaseConfig : IDbConnectionFactory
{
    private readonly string _connectionString;

    public DatabaseConfig(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}