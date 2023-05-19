using Charisma.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.Infrastructure.Persistance.CharismaDb.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .HasColumnType("NVARCHAR(50)")
            .IsRequired();

        builder.OwnsOne(x => x.Price, nav =>
        {
            nav.WithOwner();
            nav.Property(x => x.Value)
            .HasColumnName("Price")
            .HasColumnType("DECIMAL(10,0)")
            .IsRequired();
        }).Navigation(x => x.Price)
        .IsRequired();
    }
}
