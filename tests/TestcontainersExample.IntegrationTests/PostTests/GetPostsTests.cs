using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TestcontainersExample.Core.Dtos;
using TestcontainersExample.Core.Entities;
using TestcontainersExample.Data;
using Xunit.Categories;

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

        var users = await response.Content.ReadFromJsonAsync<List<PostDto>>();
        users.Should().HaveCount(3);
    }    
}