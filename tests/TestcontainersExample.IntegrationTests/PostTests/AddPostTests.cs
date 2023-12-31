using TestcontainersExample.Core.Features.Commands;

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
            UserId = TestUser.Id,
        };
        
        var response = await Client.PostAsJsonAsync(_url, command, _cancellationTokenSource.Token);
        response.EnsureSuccessStatusCode();

        Context.Posts.Count().Should().BeGreaterThan(0);
    }
}