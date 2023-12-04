using TestcontainersExample.Core.Entities;
using TestcontainersExample.Core.Features.Commands;

namespace TestcontainersExample.IntegrationTests.PostTests;

[Collection("Storage")]
public class DeletePostTests(CustomWebApplicationFactory factory) : BaseTest(factory)
{
    private readonly string _url = "api/Posts";
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    
    [Fact]
    [IntegrationTest]
    public async Task ShouldDeletePostFromDatabase()
    {
        var testPost = new Post
        {
            Body = "hello world",
            Title = "Some Cool Title",
            UserId = TestUser.Id,
        };
        
        Context.Posts.Add(testPost);
        await Context.SaveChangesAsync(_cancellationTokenSource.Token);
        
        var command = new DeletePostCommand
        {
            Id = testPost.Id,
        };
        
        var response = await Client.DeleteAsync($"{_url}/{command.Id}", _cancellationTokenSource.Token);
        response.EnsureSuccessStatusCode();

        Context.Posts.Count().Should().Be(0);
    }
}