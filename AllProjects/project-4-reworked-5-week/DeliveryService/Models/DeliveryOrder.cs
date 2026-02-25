/// <summary>
/// Сущность доставки, связанная с заказом.
/// </summary>
namespace DeliveryService.Models
{
    /// <summary>
    /// Представляет информацию о доставке заказа.
    /// </summary>
    public sealed class DeliveryOrder
    {
        /// <summary>
        /// Уникальный идентификатор доставки.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор заказа, для которого оформлена доставка.
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Адрес доставки.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Статус доставки.
        /// </summary>
        public string Status { get; set; } = "Pending";
    }
}