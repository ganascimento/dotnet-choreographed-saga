using Dotnet.Saga.Stock.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .ConfigContext(builder.Configuration)
    .ConfigureCors()
    .ConfigDependencyInjection()
    .ConfigBus(builder.Configuration);

var app = builder.Build();

app.Services.ExecuteMigration();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
