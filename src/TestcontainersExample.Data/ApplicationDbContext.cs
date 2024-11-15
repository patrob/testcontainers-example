using Microsoft.EntityFrameworkCore;
using TestcontainersExample.Data.Entities;

namespace TestcontainersExample.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
}