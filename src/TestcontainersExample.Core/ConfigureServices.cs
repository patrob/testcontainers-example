using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TestcontainersExample.Core;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(options => options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}