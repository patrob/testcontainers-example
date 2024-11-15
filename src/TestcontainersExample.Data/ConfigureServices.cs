using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestcontainersExample.Data;

public static class ConfigureServices
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Application");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
    }
}