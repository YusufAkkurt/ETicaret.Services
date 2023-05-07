using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistance;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ETicaretDbContext>
{
    public ETicaretDbContext CreateDbContext(string[] args)
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<ETicaretDbContext>();
        dbContextOptionsBuilder.UseNpgsql(Configuration.PostgreSQLConnectionString);

        return new(dbContextOptionsBuilder.Options);
    }
}