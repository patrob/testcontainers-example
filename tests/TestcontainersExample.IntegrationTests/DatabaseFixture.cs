using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Respawn;
using Testcontainers.MsSql;
using TestcontainersExample.Data;

namespace TestcontainersExample.IntegrationTests;

public class DatabaseFixture : IAsyncDisposable
{
    private readonly MsSqlContainer _container;
    private DbConnection? _connection = null!;
    private string _connectionString = null!;
    private Respawner _respawner = null!;

    public DatabaseFixture()
    {
        _container = new MsSqlBuilder()
            .WithAutoRemove(true)
            .Build();
        InitialiseAsync().Wait();
    }

    private async Task InitialiseAsync()
    {
        await _container.StartAsync();

        _connectionString = _container.GetConnectionString();

        _connection = new SqlConnection(_connectionString);

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(_connectionString)
            .Options;
        var context = new ApplicationDbContext(options);

        await context.Database.MigrateAsync();

        _respawner = await Respawner.CreateAsync(_connectionString, new RespawnerOptions
        {
            TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
        });
    }

    public DbConnection? GetConnection()
    {
        return _connection;
    }

    public async Task ResetAsync()
    {
        await _respawner.ResetAsync(_connectionString);
    }

    public async ValueTask DisposeAsync()
    {
        await _connection!.DisposeAsync();
        await _container.DisposeAsync();
    }
}

[CollectionDefinition("Storage")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
}
