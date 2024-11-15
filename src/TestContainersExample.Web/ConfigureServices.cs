using TestContainersExample.Web.Repositories;

namespace TestContainersExample.Web;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        return services.AddTransient<IBookRepository, BookRepository>();
    }
}