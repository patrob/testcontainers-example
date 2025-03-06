using Microsoft.AspNetCore.Mvc;
using TestcontainersExample.Data.Entities;
using TestContainersExample.Web.Dto;
using TestContainersExample.Web.Repositories;

namespace TestcontainersExample.Web.Controllers;

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