using System.Net.Http.Json;
using FluentAssertions;
using TestcontainersExample.Core.Features.Commands;
using Xunit.Categories;

namespace TestcontainersExample.IntegrationTests.PostTests;

[Collection("Storage")]
public class AddPostTests(CustomWebApplicationFactory factory) : BaseTest(factory)
{
    private readonly string _url = "api/Posts";
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    
    [Fact]
    [IntegrationTest]
    public async Task ShouldAddPostToDatabase()
    {
        var command = new AddPostCommand
        {
            Title = "Some Title",
            Body = "Some Body",
            UserId = TestUserId,
        };
        
        var response = await Client.PostAsJsonAsync(_url, command, _cancellationTokenSource.Token);
        response.EnsureSuccessStatusCode();

        Context.Posts.Count().Should().BeGreaterThan(0);
    }
}