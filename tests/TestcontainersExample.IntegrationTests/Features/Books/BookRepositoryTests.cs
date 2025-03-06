using Microsoft.EntityFrameworkCore;
using TestcontainersExample.Data;
using TestcontainersExample.Data.Entities;
using TestContainersExample.Web.Books;

namespace TestcontainersExample.IntegrationTests.Features.Books;

[Collection("Storage")]
public class BookRepositoryTests
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IBookRepository _bookRepository;
    private readonly Author _testAuthor;
    private readonly Book _testBook;

    public BookRepositoryTests(DatabaseFixture databaseFixture)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(databaseFixture.GetConnection()!)
            .Options;
        _dbContext = new ApplicationDbContext(options);
        _bookRepository = new BookRepository(_dbContext);
        
        _testAuthor = new Author{Name = "Bilbo Baggins"};
        _testBook = new Book {Title = "The Hobbit", Author = _testAuthor};
        databaseFixture.ResetAsync().Wait();
    }
    

    [Fact]
    public void GetAll_ShouldReturnAllBooks()
    {
        _testBook.Author = _testAuthor;
        _dbContext.Authors.Add(_testAuthor);
        _dbContext.Books.Add(_testBook);
        _dbContext.SaveChanges();
        var books = _bookRepository.GetAll().ToList();
        books.Should().HaveCount(1);
        books.Should().ContainSingle(b => b.Title == _testBook.Title);
    }
}