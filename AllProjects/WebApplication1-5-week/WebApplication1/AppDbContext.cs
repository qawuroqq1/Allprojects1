namespace WebApplication1
{
    using Microsoft.EntityFrameworkCore;
    using WebApplication1.Configurations;
    using WebApplication1.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.Entity<Order>().Property(o => o.Status).HasConversion<int>();
        
     }
    }
}