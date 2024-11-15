using Microsoft.AspNetCore.Mvc;
using TestcontainersExample.Data.Entities;
using TestContainersExample.Web.Repositories;

namespace TestcontainersExample.Web.Controllers;

public class AuthorsController(IAuthorRepository authorRepository) : ApiControllerBase
{
    [HttpGet]
    public IEnumerable<Author> GetAll()
    {
        return authorRepository.GetAll();
    }
}