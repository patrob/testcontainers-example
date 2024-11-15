using TestcontainersExample.Data.Entities;

namespace TestcontainersExample.IntegrationTests.Books;

public class GetBooksTests : BaseTest
{
    private readonly Author TestAuthor;

    private readonly Book TestBook;

    public GetBooksTests(CustomWebApplicationFactory factory):base(factory)
    {
        TestAuthor = new Author{Name = "Bilbo Baggins"};
        TestBook = new Book {Title = "The Hobbit"};
    }
    
    [Fact]
    public async Task GetAllBooks_ShouldReturnAllBooks()
    {
        Context.Authors.Add(TestAuthor);
        Context.Books.Add(TestBook);
        await Context.SaveChangesAsync();

        var result = await Client.GetAsync("/api/books");
        result.EnsureSuccessStatusCode();
        var books = await result.Content.ReadFromJsonAsync<IEnumerable<Book>>();
        books.Should().ContainSingle(b => b.Title == TestBook.Title);
    }
}