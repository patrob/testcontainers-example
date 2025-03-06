using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Respawn;
using Testcontainers.MsSql;
using TestcontainersExample.Data;

namespace TestcontainersExample.IntegrationTests;

// ReSharper disable once ClassNeverInstantiated.Global
public class DatabaseFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _container = new MsSqlBuilder()
        .WithName("test-db")
        .WithCreateParameterModifier(cmd =>
        {
            cmd.User = "root";
        })
        .WithImage("mcr.microsoft.com/mssql/server:2019-CU28-ubuntu-20.04")
        .WithAutoRemove(true)
        .Build();
    
    private DbConnection? _connection;
    private string _connectionString = null!;
    private Respawner _respawner = null!;

    public async Task InitializeAsync()
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

    public async Task DisposeAsync()
    {
        await _connection!.DisposeAsync();
        await _container.DisposeAsync();
    }
}

[CollectionDefinition("Storage")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>;
