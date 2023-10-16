using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestcontainersExample.Core.Common.Interfaces;

namespace TestcontainersExample.Data;

public static class ConfigureServices
{
    public static void ConfigureDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer());
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    }
}