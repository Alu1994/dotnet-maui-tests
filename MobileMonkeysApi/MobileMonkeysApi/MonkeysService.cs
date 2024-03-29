﻿using Dapper;
using System.Data.SqlClient;

namespace MobileMonkeysApi;

public interface IMonkeysService
{
    Task<IEnumerable<Monkey>> GetMonkeys(CancellationToken cancellationToken);
    Task<Monkey?> PostMonkey(Monkey monkey, CancellationToken cancellationToken);
    Task<Monkey> PutMonkey(Monkey monkey, CancellationToken cancellationToken);
}

public sealed class MonkeysService : IMonkeysService
{
    public async Task<IEnumerable<Monkey>> GetMonkeys(CancellationToken cancellationToken)
    {
        var connectionString = "Server=MATHEUS-PC\\SQLEXPRESS;Database=MonkeysDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        using var connection = new SqlConnection(connectionString);

        var command = new CommandDefinition("SELECT * FROM [dbo].[Monkeys]", cancellationToken: cancellationToken);
        return await connection.QueryAsync<Monkey>(command);
    }

    public async Task<Monkey?> PostMonkey(Monkey monkey, CancellationToken cancellationToken)
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
,[Longitude]) VALUES (@Name, @Location, @Details, @Image, @Population, @Latitude, @Longitude); 
SELECT SCOPE_IDENTITY();",
                parameters: monkey, cancellationToken: cancellationToken);
            var id = await connection.ExecuteScalarAsync<int>(command);
            monkey.Id = id;
            return monkey;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Monkey> PutMonkey(Monkey monkey, CancellationToken cancellationToken)
    {
        try
        {
            var connectionString = "Server=MATHEUS-PC\\SQLEXPRESS;Database=MonkeysDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            using var connection = new SqlConnection(connectionString);

            var command = new CommandDefinition(@"UPDATE [dbo].[Monkeys] SET 
 [Name] = @Name
,[Location] = @Location
,[Details] = @Details
,[Image] = @Image
,[Population] = @Population
,[Latitude] = @Latitude
,[Longitude] = @Longitude
WHERE [Id] = @Id;",
                parameters: monkey, cancellationToken: cancellationToken);
            await connection.ExecuteAsync(command);
        }
        catch(Exception ex)
        {
            string x = ex.Message;
        }
        
        return monkey;
    }
}