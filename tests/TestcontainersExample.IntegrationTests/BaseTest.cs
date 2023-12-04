using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TestcontainersExample.Core.Entities;
using TestcontainersExample.Data;

namespace TestcontainersExample.IntegrationTests;

[Collection("Storage")]
public class BaseTest : IClassFixture<CustomWebApplicationFactory>
{
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext Context;
    protected User TestUser { get; private set; }

    private CustomWebApplicationFactory Factory { get; }

    protected BaseTest(CustomWebApplicationFactory factory)
    {
        Factory = factory;
        Client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = true,
        });
        TestUser = new User
        {
            Name = "Patrick Test"
        };
        
        Context = factory.Services.GetService<ApplicationDbContext>()!;
        SetupBaseTestData().Wait();
    }

    private async Task SetupBaseTestData()
    {
        await ResetDatabase();
        await SetupTestUser();
    }

    private Task ResetDatabase()
    {
        return Factory.DatabaseFixture.ResetAsync();
    }

    private async Task SetupTestUser()
    {
        Context.Users.Add(TestUser);
        await Context.SaveChangesAsync();
    }
}