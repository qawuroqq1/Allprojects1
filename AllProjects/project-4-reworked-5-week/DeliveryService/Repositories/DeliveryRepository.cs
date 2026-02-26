/// <summary>
/// Репозиторий доставок на основе Entity Framework Core.
/// </summary>
namespace DeliveryService.Repositories
{
    using DeliveryService.Models;

    /// <summary>
    /// Реализация репозитория доставок.
    /// </summary>
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly DeliveryDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр репозитория доставок.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public DeliveryRepository(DeliveryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет новую запись доставки.
        /// </summary>
        /// <param name="deliveryOrder">Сущность доставки.</param>
        public async Task AddAsync(DeliveryOrder deliveryOrder)
        {
            ArgumentNullException.ThrowIfNull(deliveryOrder);

            await _context.DeliveryOrders.AddAsync(deliveryOrder);
        }
    }
}