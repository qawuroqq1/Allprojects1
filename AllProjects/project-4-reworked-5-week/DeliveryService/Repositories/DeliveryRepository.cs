/// <summary>
/// Репозиторий доставок на основе Entity Framework Core.
/// </summary>
namespace DeliveryService.Repositories
{
    using DeliveryService.Models;

    /// <summary>
    /// Реализация репозитория доставок.
    /// </summary>
    public sealed class DeliveryRepository : IDeliveryRepository
    {
        private readonly DeliveryDbContext context;

        /// <summary>
        /// Инициализирует новый экземпляр репозитория доставок.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public DeliveryRepository(DeliveryDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Добавляет новую запись доставки.
        /// </summary>
        /// <param name="deliveryOrder">Сущность доставки.</param>
        public async Task AddAsync(DeliveryOrder deliveryOrder)
        {
            ArgumentNullException.ThrowIfNull(deliveryOrder);

            await this.context.DeliveryOrders.AddAsync(deliveryOrder).ConfigureAwait(false);
        }
    }
}