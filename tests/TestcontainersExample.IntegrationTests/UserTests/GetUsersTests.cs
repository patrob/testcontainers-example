using TestcontainersExample.Core.Dtos;

namespace TestcontainersExample.IntegrationTests.UserTests;

[Collection("Storage")]
public class GetUsersTests(CustomWebApplicationFactory factory) : BaseTest(factory)
{
    private readonly string _url = "api/Users";
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    
    [Fact]
    [IntegrationTest]
    public async Task ShouldGetUsersInDatabase()
    {
        var response = await Client.GetAsync(_url, _cancellationTokenSource.Token);
        response.EnsureSuccessStatusCode();

        var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
        users.Should().HaveCount(1);
    }
}