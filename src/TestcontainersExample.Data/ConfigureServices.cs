using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestcontainersExample.Core.Common.Interfaces;

namespace TestcontainersExample.Data;

public static class ConfigureServices
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Application");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<ContextInitializer>();
    }
    
    public static async Task InitializeDatabase(this IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();
        var contextInitializer = scope.ServiceProvider.GetRequiredService<ContextInitializer>();
        await contextInitializer.Initialize();
    }
}