using TestcontainersExample.Data.Entities;
using TestContainersExample.Web.Dto;

namespace TestcontainersExample.IntegrationTests.E2E.Books;

[Collection("Storage")]
public class GetBooksTests(CustomWebApplicationFactory factory) : BaseTest(factory)
{
    private readonly Author _testAuthor = new() {Name = "Bilbo Baggins"};

    private readonly Book _testBook = new() {Title = "The Hobbit"};

    [Fact]
    public async Task GetAllBooks_ShouldReturnAllBooks()
    {
        _testBook.Author = _testAuthor;
        Context.Books.Add(_testBook);
        await Context.SaveChangesAsync();

        var result = await Client.GetAsync("/api/books");
        result.EnsureSuccessStatusCode();
        var books = await result.Content.ReadFromJsonAsync<IEnumerable<BookDto>>();
        books.Should().ContainSingle(b => b.Title == _testBook.Title && b.Author == _testAuthor.Name);
    }
}