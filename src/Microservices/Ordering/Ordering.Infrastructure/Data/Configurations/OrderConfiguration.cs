using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));
            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId).IsRequired();
            builder.HasMany(x => x.OrderItems).WithOne().HasForeignKey(x => x.OrderId);
            builder.ComplexProperty(
            o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
            });
            builder.ComplexProperty(o => o.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(x => x.LastName).HasMaxLength(50);
                addressBuilder.Property(x => x.EmailAddress).HasMaxLength(200).IsRequired();
                addressBuilder.Property(x => x.AddressLine).HasMaxLength(400).IsRequired();
                addressBuilder.Property(x => x.State).IsRequired();
                addressBuilder.Property(x => x.City).IsRequired();
                addressBuilder.Property(x => x.Country).IsRequired();
                addressBuilder.Property(x => x.ZipCode).IsRequired();
            });
            builder.ComplexProperty(o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(x => x.LastName).HasMaxLength(50);
                addressBuilder.Property(x => x.EmailAddress).HasMaxLength(200).IsRequired();
                addressBuilder.Property(x => x.AddressLine).HasMaxLength(400).IsRequired();
                addressBuilder.Property(x => x.State).IsRequired();
                addressBuilder.Property(x => x.City).IsRequired();
                addressBuilder.Property(x => x.Country).IsRequired();
                addressBuilder.Property(x => x.ZipCode).IsRequired();
            });
            builder.ComplexProperty(o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(x => x.CardName).HasMaxLength(100);
                paymentBuilder.Property(x => x.CardNumber).HasMaxLength(16);
                paymentBuilder.Property(x => x.PaymentMethod).IsRequired();
                paymentBuilder.Property(x => x.CVV).HasMaxLength(3);
                paymentBuilder.Property(x => x.Expiration);
            });

            builder.Property(x => x.Status).HasDefaultValue(OrderStatus.Draft).HasConversion(
                x => x.ToString(),
                dbStatus => Enum.Parse<OrderStatus>(dbStatus)
                );
        }
    }
}
