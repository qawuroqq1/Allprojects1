// <copyright file="OrderConfiguration..cs" company="AllProjects">
// Copyright (c) AllProjects. All rights reserved.
// </copyright>

namespace OrdersService.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OrdersService.Models;

    public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.ToTable(nameof(AppDbContext.Orders));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();
        }
    }
}