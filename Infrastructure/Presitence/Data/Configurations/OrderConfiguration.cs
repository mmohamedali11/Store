
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configurations
{
    public class OrderConfiguartion : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.Address)
                .WithMany()
                .HasForeignKey(o => o.AddressId);

            builder.HasMany(o => o.OrderItems)
         .WithOne(oi => oi.Order)
         .HasForeignKey(oi => oi.OrderId);


            builder.Property(o => o.Total)
                .HasColumnType("decimal(18,2)");
        }


    }
}
