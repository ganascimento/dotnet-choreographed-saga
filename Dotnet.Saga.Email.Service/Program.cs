using Dotnet.Saga.Email.Service.Consumers;
using Dotnet.Saga.Email.Service.Interfaces;
using Dotnet.Saga.Email.Service.Services;
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
        services.AddScoped<IEmailService, EmailService>();

        services.AddLogging(lb => lb
            .SetMinimumLevel(LogLevel.Information)
            .AddConsole());

        var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

        services.AddHostedService<CancelOrderConsumer>();
        services.AddHostedService<PaymentErrorConsumer>();
        services.AddHostedService<PaymentSuccessConsumer>();
    });


await builder.RunConsoleAsync(config =>
{
    Console.WriteLine("Dotnet.Saga.Email.Service is running...");
});