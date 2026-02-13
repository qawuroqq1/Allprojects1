using OrdersService.Configurations;
using OrdersService.Models;

namespace OrdersService
{
    using Microsoft.EntityFrameworkCore;
    using OrdersService.Configurations;
    using OrdersService.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<OrderEntity> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Подключаем конфигурацию для OrderEntity
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            
            // Настройка конвертации статуса
            modelBuilder.Entity<OrderEntity>().Property(o => o.Status).HasConversion<int>();
        }
    }
}