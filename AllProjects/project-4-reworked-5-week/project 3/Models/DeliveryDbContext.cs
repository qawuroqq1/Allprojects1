using Microsoft.EntityFrameworkCore;

namespace project_3.Models
{
    public class DeliveryOrder
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }

    public class DeliveryDbContext : DbContext
    {
        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options) { }
        public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
    }
}