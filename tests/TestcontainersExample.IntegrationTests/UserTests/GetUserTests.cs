using TestcontainersExample.Core.Dtos;

namespace TestcontainersExample.IntegrationTests.UserTests;

[Collection("Storage")]
public class GetUserTests(CustomWebApplicationFactory factory) : BaseTest(factory)
{
    private readonly string _url = "api/Users";
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    
    [Fact]
    [IntegrationTest]
    public async Task ShouldGetUserInDatabase()
    {
        var response = await Client.GetAsync($"{_url}/{TestUser.Id}", _cancellationTokenSource.Token);
        response.EnsureSuccessStatusCode();

        var user = await response.Content.ReadFromJsonAsync<UserDto>();
        user!.Id.Should().Be(TestUser.Id);
        user.Name.Should().Be(TestUser.Name);
    }
}