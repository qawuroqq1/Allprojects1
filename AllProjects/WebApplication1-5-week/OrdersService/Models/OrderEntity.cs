/// <summary>
/// </summary>
namespace OrdersService.Models
{
    public sealed class OrderEntity
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public OrderStatus Status { get; set; }
    }
}