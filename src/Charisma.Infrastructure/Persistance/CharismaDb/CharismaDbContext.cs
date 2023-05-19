using Charisma.Application.Common.Interfaces;
using Charisma.Domain;
using Charisma.Infrastructure.Persistance.CharismaDb.Configurations;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Charisma.Infrastructure.Persistance.CharismaDb;

public class CharismaDbContext : IdentityDbContext, ICharismaDbContext
{
    public CharismaDbContext(DbContextOptions<CharismaDbContext> options) : base(options)
    {
    }


    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
        
        builder.Entity<IdentityUser>()
            .HasData(new IdentityUser
            {
                UserName= "develop",
                Id= "cc45d7cd-068b-4468-9c83-b90ed688854f",
                NormalizedUserName = "DEVELOP",
                PasswordHash = "AQAAAAEAACcQAAAAEMiTSK3hftPIZoc8ZLHYsJLs8qyNqN4vRJmWrVaqq2rPGAVW4zIBAibNkrLl2dcoOg==",
                SecurityStamp = "J7EICUMPTWD4XZYNQQXHWIK3EO4ULRNP",
                ConcurrencyStamp = "3fd4f013-3285-4401-9568-2255d281d698"
            });

        base.OnModelCreating(builder);
    }
}