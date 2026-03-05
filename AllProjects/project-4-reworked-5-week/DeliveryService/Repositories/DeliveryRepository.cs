/// <summary>
/// Репозиторий доставок на основе Entity Framework Core.
/// </summary>
namespace DeliveryService.Repositories
{
    using DeliveryService.Models;
    using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// Возвращает список всех доставок.
        /// </summary>
        /// <returns>Коллекция доставок.</returns>
        public async Task<List<DeliveryOrder>> GetAllAsync()
        {
            return await _context.DeliveryOrders.ToListAsync();
        }

        /// <summary>
        /// Возвращает доставку по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор доставки.</param>
        /// <returns>Доставка или null.</returns>
        public async Task<DeliveryOrder?> GetByIdAsync(Guid id)
        {
            return await _context.DeliveryOrders.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}