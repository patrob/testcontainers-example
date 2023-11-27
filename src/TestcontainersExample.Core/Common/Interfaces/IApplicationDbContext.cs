using Microsoft.EntityFrameworkCore;
using TestcontainersExample.Core.Entities;

namespace TestcontainersExample.Core.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Post> Posts { get; set; }
    DbSet<User> Users { get; set; }
    
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}