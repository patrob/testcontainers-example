using TestcontainersExample.Data;
using TestcontainersExample.Data.Entities;

namespace TestContainersExample.Web.Repositories;

public interface IAuthorRepository
{
    IEnumerable<Author> GetAll();
}

public class AuthorRepository(ApplicationDbContext dbContext) : IAuthorRepository
{
    public IEnumerable<Author> GetAll()
    {
        return dbContext.Authors;
    }
}