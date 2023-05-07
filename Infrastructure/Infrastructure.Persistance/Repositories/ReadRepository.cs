using Core.Application.Repositories;
using Core.Domain.Entities.Common;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistance.Repositories;

public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ETicaretDbContext _context;

    public ReadRepository(ETicaretDbContext context)
    {
        _context = context;
    }

    public DbSet<TEntity> Table => _context.Set<TEntity>();

    public IQueryable<TEntity> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();

        if (tracking is false) query = query.AsNoTracking();

        return query;
    }

    public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression, bool tracking = true)
    {
        var query = Table.Where(expression);

        if (tracking is false) query = query.AsNoTracking();

        return query;
    }

    public async Task<TEntity> GetByIdAsync(Guid id, bool tracking = true)
    {
       var query = Table.AsQueryable();

        if (tracking is false) query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(row => row.Id.Equals(id));
    }

    public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
    {
        var query = Table.AsQueryable();

        if (tracking is false) query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(expression);
    }
}
