using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Admission.Document.Infrastructure;

public static class InfrastructureConfiguration
{
    public static async Task AddAutoMigrationAsync(this IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DocumentDbContext>();
            await context.Database.MigrateAsync();
        }
    }
}