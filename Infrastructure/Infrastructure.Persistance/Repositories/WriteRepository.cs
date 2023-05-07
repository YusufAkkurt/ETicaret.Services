using Core.Application.Repositories;
using Core.Domain.Entities.Common;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories;

public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ETicaretDbContext _context;

    public WriteRepository(ETicaretDbContext context)
    {
        _context = context;
    }

    public DbSet<TEntity> Table => _context.Set<TEntity>();

    public async Task<bool> AddAsync(TEntity entity)
    {
        var entityEntry = await Table.AddAsync(entity);
        return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<TEntity> entities)
    {
        await Table.AddRangeAsync(entities);
        return true;
    }

    public bool Remove(TEntity entity)
    {
        var entityEntry = Table.Remove(entity);
        return entityEntry.State == EntityState.Deleted;
    }

    public bool RemoveRange(List<TEntity> entities)
    {
        Table.RemoveRange(entities);
        return true;
    }

    public async Task<bool> RemoveAsync(Guid id)
    {
        var entity = await Table.FindAsync(id);
        if (entity is null) return false;
        
        return this.Remove(entity);
    }

    public bool Update(TEntity entity)
    {
        var entityEntry = Table.Update(entity);
        return entityEntry.State == EntityState.Added;
    }

    public bool UpdateRange(List<TEntity> entities)
    {
        Table.UpdateRange(entities);
        return true;
    }

    public async Task<bool> SaveAsync() => await _context.SaveChangesAsync() > 0;
}
