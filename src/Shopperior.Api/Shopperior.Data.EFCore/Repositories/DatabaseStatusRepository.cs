using System.Data;
using Microsoft.Data.SqlClient;
using Shopperior.Domain.Contracts.Shared.Models;
using Shopperior.Domain.Contracts.Shared.Repositories;

namespace Shopperior.Data.EFCore.Repositories;

public class DatabaseStatusRepository : IDatabaseStatusRepository
{
    private readonly string _connectionString;

    public DatabaseStatusRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDatabaseStatus> GetStatus()
    {
        DatabaseStatus databaseStatus = null;
        try
        {
            var sqlText = @"SELECT 'OK' AS Status, GETDATE() AS Timestamp";
            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new SqlCommand(sqlText, conn);
            cmd.CommandType = CommandType.Text;
            await using var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                var status = reader.GetString("Status");
                var timestamp = reader.GetDateTime("Timestamp");
                databaseStatus = new DatabaseStatus
                {
                    Status = status,
                    Timestamp = timestamp
                };
            }
        }
        catch (Exception ex)
        {
            databaseStatus = new DatabaseStatus
            {
                Status = ex.Message,
                Timestamp = DateTime.Now
            };
        }

        return databaseStatus;
    }
}

public class DatabaseStatus : IDatabaseStatus
{
    public string Status { get; set; }
    public DateTime? Timestamp { get; set; }
}