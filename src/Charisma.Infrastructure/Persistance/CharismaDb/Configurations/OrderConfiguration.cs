using Charisma.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charisma.Infrastructure.Persistance.CharismaDb.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne<IdentityUser>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.CreatedAt);

        builder.Property(x => x.PostType);

        builder.OwnsOne(x => x.NetPrice, nav =>
        {
            nav.WithOwner();
            nav.Property(x => x.Value)
            .HasColumnName("NetPrice")
            .HasColumnType("DECIMAL(10,0)")
            .IsRequired();
        }).Navigation(x => x.NetPrice)
        .IsRequired();

        builder.OwnsOne(x => x.DiscountPrice, nav =>
        {
            nav.WithOwner();
            nav.Property(x => x.Value)
            .HasColumnName("DiscountPrice")
            .HasColumnType("DECIMAL(10,0)")
            .IsRequired();
        }).Navigation(x => x.DiscountPrice)
        .IsRequired();

        builder.Ignore(x=> x.FinalPrice);

        builder.HasMany(x => x.OrderDetails)
            .WithOne()
            .HasForeignKey(x => x.OrderId)
            .HasPrincipalKey(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}