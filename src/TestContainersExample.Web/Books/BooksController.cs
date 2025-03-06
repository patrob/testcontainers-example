using Microsoft.AspNetCore.Mvc;
using TestcontainersExample.Web.Controllers;

namespace TestContainersExample.Web.Books;

public class BooksController(IBookRepository bookRepository) : ApiControllerBase
{
    [HttpGet]
    public IEnumerable<BookDto> GetAll()
    {
        return bookRepository.GetAll().Select(x => new BookDto
        {
            Title = x.Title,
            Author = x.Author?.Name ?? string.Empty
        });
    }
}