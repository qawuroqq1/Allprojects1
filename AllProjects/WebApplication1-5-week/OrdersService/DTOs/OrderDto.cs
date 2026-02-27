namespace OrdersService.DTOs
{
    /// <summary>
    /// Модель данных заказа для внешних запросов и ответов.
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// Уникальный идентификатор заказа.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование заказа.
        /// </summary>
        required public string Name { get; set; }

        /// <summary>
        /// Общая стоимость заказа.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Статус заказа.
        /// </summary>
        required public string Status { get; set; }
    }
}