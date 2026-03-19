namespace OrdersService;

using Microsoft.EntityFrameworkCore;
using OrdersService.Models;

/// <summary>
/// Контекст базы данных для работы с заказами.
/// </summary>
public class OrderDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderDbContext"/> class.
    /// Инициализирует новый экземпляр контекста базы данных.
    /// </summary>
    /// <param name="options">Параметры конфигурации контекста.</param>
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Набор заказов.
    /// </summary>
    public DbSet<OrderEntity> Orders { get; set; } = null!;

    /// <summary>
    /// Конфигурирует модель базы данных.
    /// </summary>
    /// <param name="modelBuilder">Построитель модели.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
    }
}