using DeliveryService.Consumers;
using DeliveryService.Models;
using DeliveryService.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DeliveryDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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
            var context = scope.ServiceProvider.GetRequiredService<DeliveryDbContext>();
            context.Database.Migrate();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapGet("/", () => "DeliveryService is running");
        app.MapControllers();

        app.Run();
    }
}