using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestcontainersExample.Core.Features.Commands;
using Xunit.Categories;

namespace TestcontainersExample.IntegrationTests.UserTests;

[Collection("Storage")]
public class AddUserTests(CustomWebApplicationFactory factory) : BaseTest(factory)
{
    private readonly string _url = "api/Users";
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    
    [Fact]
    [IntegrationTest]
    public async Task ShouldAddUserToDatabase()
    {
        var command = new AddUserCommand()
        {
            Name = "Gandalf the White"
        };
        
        var response = await Client.PostAsJsonAsync(_url, command, _cancellationTokenSource.Token);
        response.EnsureSuccessStatusCode();
        
        var user = await Context.Users.FirstOrDefaultAsync(x => x.Name == command.Name);
        user.Should().NotBeNull();
    }
}