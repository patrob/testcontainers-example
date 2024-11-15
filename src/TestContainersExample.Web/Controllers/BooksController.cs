using Microsoft.AspNetCore.Mvc;
using TestcontainersExample.Data.Entities;
using TestContainersExample.Web.Repositories;

namespace TestcontainersExample.Web.Controllers;

public class BooksController(IBookRepository bookRepository) : ApiControllerBase
{
    [HttpGet]
    public IEnumerable<Book> GetAll()
    {
        return bookRepository.GetAll();
    }
}