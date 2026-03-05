/// <summary>
/// Контекст базы данных DeliveryService.
/// </summary>
namespace DeliveryService.Models
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Контекст базы данных для работы с доставками.
    /// </summary>
    public class DeliveryDbContext : DbContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр контекста.
        /// </summary>
        /// <param name="options">Параметры конфигурации контекста.</param>
        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Набор доставок.
        /// </summary>
        public DbSet<DeliveryOrder> DeliveryOrders { get; set; } = null!;
    }
}