using Microsoft.AspNetCore.Mvc;
using TestcontainersExample.Data.Entities;
using TestcontainersExample.Web.Controllers;

namespace TestContainersExample.Web.Authors;

public class AuthorsController(IAuthorRepository authorRepository) : ApiControllerBase
{
    [HttpGet]
    public IEnumerable<Author> GetAll()
    {
        return authorRepository.GetAll();
    }
}