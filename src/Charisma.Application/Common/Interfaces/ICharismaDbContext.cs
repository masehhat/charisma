using Charisma.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Charisma.Application.Common.Interfaces;

public interface ICharismaDbContext
{
    DbSet<IdentityUser> Users { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Product> Products { get; set; }

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}