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
    protected Guid TestUserId { get; private set; }

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
        await SetupTestUser();
    }

    private Task ResetDatabase()
    {
        return Factory.DatabaseFixture.ResetAsync();
    }

    private async Task SetupTestUser()
    {
        var user = new User
        {
            Name = "Patrick Test"
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync();
        TestUserId = user.Id;
    }
}