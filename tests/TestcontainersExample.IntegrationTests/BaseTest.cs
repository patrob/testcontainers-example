using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TestcontainersExample.Data;

namespace TestcontainersExample.IntegrationTests;

[Collection("Storage")]
public class BaseTest : IClassFixture<CustomWebApplicationFactory>
{
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext Context;
    private CustomWebApplicationFactory Factory { get; }

    protected BaseTest(CustomWebApplicationFactory factory)
    {
        Factory = factory;
        Client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = true,
        });
        
        Context = factory.Services.GetService<ApplicationDbContext>()!;
        SetupBaseTestData().Wait();
    }

    private async Task SetupBaseTestData()
    {
        await ResetDatabase();
    }

    private Task ResetDatabase()
    {
        return Factory.DatabaseFixture.ResetAsync();
    }
}