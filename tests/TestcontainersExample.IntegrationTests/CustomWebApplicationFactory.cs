using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TestcontainersExample.Data;

namespace TestcontainersExample.IntegrationTests;

[Collection("Storage")]
// ReSharper disable once ClassNeverInstantiated.Global
public class CustomWebApplicationFactory(DatabaseFixture databaseFixture) : WebApplicationFactory<Program>
{
    private readonly DbConnection? _connection = databaseFixture.GetConnection()!;
    
    public DatabaseFixture DatabaseFixture => databaseFixture;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Local");
        builder.ConfigureServices(services =>
        {
            services
                .RemoveAll<DbContextOptions<ApplicationDbContext>>()
                .AddDbContext<ApplicationDbContext>((sp, options) =>
                {
                    options.UseSqlServer(_connection!);
                });
        });
    }
}