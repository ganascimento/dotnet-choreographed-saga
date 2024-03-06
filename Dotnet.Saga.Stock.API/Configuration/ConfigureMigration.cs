using Dotnet.Saga.Stock.API.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Saga.Stock.API.Configuration;

public static class MigrationExtensions
{
    public static void ExecuteMigration(this IServiceProvider provider)
    {
        using (var scope = provider.CreateScope())
        {
            var services = scope.ServiceProvider;

            using (var context = services.GetRequiredService<DataContext>())
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}