using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemEntity>
    {
        public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ItemsCount).IsRequired();
            builder.Property(x => x.ItemPrice).IsRequired();

            builder
                .HasOne(x => x.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.OrderId);

            builder
                .HasOne(x => x.Item)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(o => o.OrderId);
        }
    }
}
