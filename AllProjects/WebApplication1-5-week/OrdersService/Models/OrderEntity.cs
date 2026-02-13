using System;

namespace OrdersService.Models
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; }
    }
}