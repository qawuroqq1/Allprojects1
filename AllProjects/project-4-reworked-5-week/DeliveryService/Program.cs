using DeliveryService.Consumers;
using DeliveryService.Models;
using DeliveryService.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Точка входа DeliveryService.
/// Конфигурирует базу данных, репозитории и MassTransit consumer.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

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
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("delivery-orders-queue", e =>
        {
            /// <summary>
            /// Повторная попытка обработки сообщения при ошибке.
            /// 3 попытки с интервалом 5 секунд.
            /// </summary>
            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));

            e.ConfigureConsumer<OrderCreatedConsumer>(context);
        });
    });
});

var app = builder.Build();

/// <summary>
/// Автоматическое создание базы данных при запуске.
/// </summary>
using (IServiceScope scope = app.Services.CreateScope())
{
    DeliveryDbContext context = scope.ServiceProvider.GetRequiredService<DeliveryDbContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();