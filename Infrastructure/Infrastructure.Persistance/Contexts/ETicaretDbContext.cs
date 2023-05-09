using Core.Domain.Entities;
using Core.Domain.Entities.Common;
using Core.Domain.Entities.Files;
using Core.Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Contexts;

public class ETicaretDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Core.Domain.Entities.Files.File> Files { get; set; }
    public DbSet<ProductImageFile> ProductImageFiles { get; set; }
    public DbSet<InvoiceFile> InvoiceFiles { get; set; }

    public ETicaretDbContext(DbContextOptions options) : base(options) { }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added: entry.Entity.CreatedDate = DateTime.UtcNow; break;
                case EntityState.Modified: entry.Entity.UpdatedDate = DateTime.UtcNow; break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
