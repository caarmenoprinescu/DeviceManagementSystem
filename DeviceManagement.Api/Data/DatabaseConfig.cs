using Microsoft.Data.SqlClient;

namespace DeviceManagement.Api.Data;

public class DatabaseConfig
{
    public static readonly string ConnectionString = 
        @"Server=localhost,1433;Database=DeviceManagementSystem;User Id=sa;Password=Parola@12345;TrustServerCertificate=True;";

    public static SqlConnection GetDatabaseConnection()
    {
        return new SqlConnection(ConnectionString);
    }

}