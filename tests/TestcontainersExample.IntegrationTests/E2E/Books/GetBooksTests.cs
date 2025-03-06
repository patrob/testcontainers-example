using TestcontainersExample.Data.Entities;
using TestContainersExample.Web.Books;

namespace TestcontainersExample.IntegrationTests.E2E.Books;

[Collection("Storage")]
public class GetBooksTests : BaseTest
{
    private readonly Author _testAuthor = new() { Name = "Bilbo Baggins" };

    private readonly Book _testBook;

    public GetBooksTests(CustomWebApplicationFactory factory) : base(factory)
    {
        _testBook = new Book { Title = "The Hobbit", Author = _testAuthor };
    }

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