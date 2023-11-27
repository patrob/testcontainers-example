using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestcontainersExample.Core.Common.Interfaces;

namespace TestcontainersExample.Data;

public static class ConfigureServices
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Application")));
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    }
}