/// <summary>
/// </summary>
namespace OrdersService
{
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using OrdersService.Mappings;
    using OrdersService.Repositories;
    using OrdersService.Services;

    internal static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string? defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(defaultConnection))
            {
                throw new InvalidOperationException("ConnectionStrings:DefaultConnection is not configured.");
            }

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(defaultConnection));

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            string? rabbitConnection = builder.Configuration.GetConnectionString("RabbitMq");
            builder.Services.AddMassTransit(x =>
            {
                if (!string.IsNullOrWhiteSpace(rabbitConnection))
                {
                    x.UsingRabbitMq((_, cfg) =>
                    {
                        cfg.Host(rabbitConnection);
                    });
                }
                else
                {
                    x.UsingInMemory();
                }
            });

            builder.Services.AddControllers();

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (IServiceScope scope = app.Services.CreateScope())
            {
                AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
