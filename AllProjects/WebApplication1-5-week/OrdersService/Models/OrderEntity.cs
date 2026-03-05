namespace OrdersService.Models
{
    /// <summary>
    /// Представляет сущность заказа в базе данных.
    /// </summary>
    public class OrderEntity
    {
        /// <summary>
        /// Уникальный идентификатор заказа.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование заказа.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Стоимость заказа.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Статус заказа.
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}