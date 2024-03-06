using Dotnet.Saga.Payment.Service.Bus;
using Dotnet.Saga.Payment.Service.Consumers;
using Dotnet.Saga.Payment.Service.Infra.Context;
using Dotnet.Saga.Payment.Service.Infra.Repositories;
using Dotnet.Saga.Payment.Service.Interfaces;
using Dotnet.Saga.Payment.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = new HostBuilder()
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IConfiguration>(context.Configuration);
        services.AddTransient<IPaymentRepository, PaymentRepository>();
        services.AddTransient<IPaymentService, PaymentService>();
        services.AddTransient<IBusService, BusService>();

        services.AddLogging(lb => lb
            .SetMinimumLevel(LogLevel.Information)
            .AddConsole());

        var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<DataContext>(
            option =>
            {
                option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        );

        services.AddHostedService<CreateOrderConsumer>();
        services.AddHostedService<CancelOrderConsumer>();

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var servicesProvider = scope.ServiceProvider;

            using (var dbContext = servicesProvider.GetRequiredService<DataContext>())
            {
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }
            }
        }

    });

await builder.RunConsoleAsync(config =>
{
    Console.WriteLine("Dotnet.Saga.Payment.Service is running...");
});