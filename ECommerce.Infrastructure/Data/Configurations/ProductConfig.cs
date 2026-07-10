using ECommerce.Domin.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand)
                   .WithMany()
                   .HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.ProductType)
                   .WithMany()
                   .HasForeignKey(p => p.TypeId);

            builder.Property(X => X.Price).HasColumnType("decimal(18,2)");
            builder.Property(X => X.Name).HasMaxLength(100);
            builder.Property(X => X.Description).HasMaxLength(500);
            builder.Property(X => X.PictureUrl).HasMaxLength(200);

        }
    }
}
