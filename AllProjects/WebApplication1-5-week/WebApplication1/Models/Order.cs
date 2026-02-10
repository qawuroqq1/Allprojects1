namespace WebApplication1.Models
{
    using System;

    public enum OrderStatus
    {
        New,
        InProgress,
        Completed,
    }

    public class Order
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public OrderStatus Status { get; set; }
    }
}