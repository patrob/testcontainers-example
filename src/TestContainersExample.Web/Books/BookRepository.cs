using Microsoft.EntityFrameworkCore;
using TestcontainersExample.Data;
using TestcontainersExample.Data.Entities;

namespace TestContainersExample.Web.Books;

public interface IBookRepository
{
    IEnumerable<Book> GetAll();
}

public class BookRepository(ApplicationDbContext dbContext) : IBookRepository
{
    public IEnumerable<Book> GetAll()
    {
        return dbContext.Books.Include(b => b.Author);
    }
}