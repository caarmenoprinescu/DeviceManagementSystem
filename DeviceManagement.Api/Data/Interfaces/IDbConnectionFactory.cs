using Microsoft.Data.SqlClient;

namespace DeviceManagement.Api.Data.Interfaces;

public interface IDbConnectionFactory
{
    SqlConnection CreateConnection();
}