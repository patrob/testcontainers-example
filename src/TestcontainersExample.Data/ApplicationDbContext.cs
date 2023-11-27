using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestcontainersExample.Core.Common.Interfaces;
using TestcontainersExample.Core.Entities;

namespace TestcontainersExample.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<User> Users { get; set; }

}