/// <summary>
/// </summary>
namespace OrdersService.DTOs
{
    public sealed class OrderDto
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}