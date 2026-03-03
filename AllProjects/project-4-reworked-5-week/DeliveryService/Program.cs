using DeliveryService.Consumers;
using DeliveryService.Models;
using DeliveryService.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DeliveryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

/// <summary>
/// Конфигурация MassTransit и подключение к RabbitMQ.
/// Регистрирует OrderCreatedConsumer и настраивает очередь получения сообщений.
/// </summary>
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/");
        cfg.ReceiveEndpoint("delivery-orders-queue", e =>
        {
            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
        });
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (IServiceScope scope = app.Services.CreateScope())
{
    DeliveryDbContext context = scope.ServiceProvider.GetRequiredService<DeliveryDbContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/", () => "DeliveryService is running");

app.MapControllers();

var dataSources = app.Services.GetRequiredService<IEnumerable<EndpointDataSource>>();
foreach (var ds in dataSources)
{
    foreach (var e in ds.Endpoints)
    {
        Console.WriteLine($"ENDPOINT: {e.DisplayName}");
    }
}

app.Run();