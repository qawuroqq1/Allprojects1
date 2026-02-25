using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersService.Models;

namespace OrdersService.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности заказа для Entity Framework Core.
/// </summary>
public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    /// <summary>
    /// Применяет конфигурацию сущности заказа.
    /// </summary>
    /// <param name="builder">Построитель конфигурации сущности.</param>
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable(nameof(AppDbContext.Orders));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Price)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();
    }
}