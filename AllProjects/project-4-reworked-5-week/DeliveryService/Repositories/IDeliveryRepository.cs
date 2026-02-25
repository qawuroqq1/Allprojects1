/// <summary>
/// Контракт репозитория для работы с доставками.
/// </summary>
namespace DeliveryService.Repositories
{
    using DeliveryService.Models;

    /// <summary>
    /// Определяет операции доступа к данным доставок.
    /// </summary>
    public interface IDeliveryRepository
    {
        /// <summary>
        /// Добавляет новую запись доставки.
        /// </summary>
        /// <param name="deliveryOrder">Сущность доставки.</param>
        Task AddAsync(DeliveryOrder deliveryOrder);
    }
}