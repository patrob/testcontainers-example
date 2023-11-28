using Microsoft.EntityFrameworkCore;

namespace TestcontainersExample.Data;

public class ContextInitializer(ApplicationDbContext context)
{
    public async Task Initialize()
    {
        await context.Database.MigrateAsync();
    }
}