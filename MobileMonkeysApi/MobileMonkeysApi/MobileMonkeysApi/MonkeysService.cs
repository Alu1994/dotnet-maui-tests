using Dapper;
using System.Data.SqlClient;

namespace MobileMonkeysApi;

public sealed class MonkeysService : IMonkeysService
{
    public async Task<IEnumerable<Monkey>> GetMonkeys(CancellationToken cancellationToken)
    {
        var connectionString = "Server=MATHEUS-PC\\SQLEXPRESS;Database=MonkeysDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        using var connection = new SqlConnection(connectionString);

        var command = new CommandDefinition("SELECT * FROM [dbo].[Monkeys]", cancellationToken: cancellationToken);
        return await connection.QueryAsync<Monkey>(command);
    }

    public async Task<bool> PostMonkeys(Monkey monkey, CancellationToken cancellationToken)
    {
        try
        {
            var connectionString = "Server=MATHEUS-PC\\SQLEXPRESS;Database=MonkeysDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            using var connection = new SqlConnection(connectionString);

            var command = new CommandDefinition(@"INSERT INTO [dbo].[Monkeys] ([Name]
,[Location]
,[Details]
,[Image]
,[Population]
,[Latitude]
,[Longitude]) VALUES (@Name, @Location, @Details, @Image, @Population, @Latitude, @Longitude)",
                parameters: monkey, cancellationToken: cancellationToken);
            return await connection.ExecuteAsync(command) > 0;
        }
        catch
        {
            return false;
        }
    }
}

public interface IMonkeysService
{
    Task<IEnumerable<Monkey>> GetMonkeys(CancellationToken cancellationToken);
    Task<bool> PostMonkeys(Monkey monkey, CancellationToken cancellationToken);
}