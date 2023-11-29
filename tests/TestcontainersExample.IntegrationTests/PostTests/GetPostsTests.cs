using TestcontainersExample.Core.Common.Models;
using TestcontainersExample.Core.Dtos;
using TestcontainersExample.Core.Entities;

namespace TestcontainersExample.IntegrationTests.PostTests;


public class GetPostsTests(CustomWebApplicationFactory factory) : BaseTest(factory)
{
    private readonly string _url = "api/Posts";
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    private async Task AddPostToDatabase(string title, string body)
    {
        var post = new Post {Title = title, Body = body, UserId = TestUserId};
        Context.Posts.Add(post);
        await Context.SaveChangesAsync();
    }
    
    [Fact]
    [IntegrationTest]
    public async Task ShouldGetPostsFromDatabase()
    {
        await AddPostToDatabase("Title1", "Body1");
        await AddPostToDatabase("Title2", "Body2");
        await AddPostToDatabase("Title3", "Body3");
        
        var response = await Client.GetAsync(_url, _cancellationTokenSource.Token);
        response.EnsureSuccessStatusCode();

        var users = await response.Content.ReadFromJsonAsync<PagedResult<PostDto>>();
        users!.Items.Should().HaveCount(3);
    }
    
    [Fact]
    [IntegrationTest]
    public async Task ShouldGetPagedPostsFromDatabase()
    {
        const int expectedItems = 5;
        
        for (var i = 1; i <= expectedItems + 3; i++)
        {
            await AddPostToDatabase($"Title{i}", $"Body{i}");    
        }
        
        
        var response = await Client.GetAsync($"{_url}?page=1&pageSize={expectedItems}", _cancellationTokenSource.Token);
        response.EnsureSuccessStatusCode();

        var users = await response.Content.ReadFromJsonAsync<PagedResult<PostDto>>();
        users!.Items.Should().HaveCount(expectedItems);
        users!.TotalCount.Should().Be(expectedItems + 3);
    }
    
    [Fact]
    [IntegrationTest]
    public async Task ShouldGetSortedPostsFromDatabase()
    {
        string[] titles =
        [
            "A Title",
            "C Title",
            "B Title",
        ];

        foreach (var title in titles)
        {
            await AddPostToDatabase(title, "Body");
        }


        var response = await Client.GetAsync($"{_url}?sortBy=Title", _cancellationTokenSource.Token);
        response.EnsureSuccessStatusCode();

        var users = await response.Content.ReadFromJsonAsync<PagedResult<PostDto>>();
        users!.Items.Should().HaveCount(titles.Length);
        users.Items.Should().BeInAscendingOrder(p => p.Title);
    }
}