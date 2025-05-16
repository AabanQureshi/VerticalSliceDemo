using Domain.Entities;
using Domain.ValueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasConversion
                (
                    id => id.Id,
                    Value => new ProductId(Value)
                );

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500);
            builder
                .OwnsOne(p => p.Price, price =>
            {
                price.Property(p => p.Amount)
                .HasColumnName("Price_Amount")
                .IsRequired();

                price.Property(p => p.Currency)
                .HasColumnName("Price_Currency")
                .IsRequired()
                .HasMaxLength(5);
            });
            builder.Property(p => p.Stock)
                .IsRequired();


        }
    }
}
