using Dotnet.Saga.Order.API.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Saga.Order.API.Configuration;

public static class ConfigureData
{
    public static IServiceCollection ConfigContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<DataContext>(
            option =>
            {
                option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        );

        return services;
    }
}