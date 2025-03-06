using TestContainersExample.Web.Books;

namespace TestContainersExample.Web;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        return services.AddTransient<IBookRepository, BookRepository>();
    }
}