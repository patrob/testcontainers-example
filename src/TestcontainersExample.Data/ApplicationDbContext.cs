using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestcontainersExample.Core.Common.Interfaces;

namespace TestcontainersExample.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Application"));
    }
}