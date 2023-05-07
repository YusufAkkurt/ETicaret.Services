using Core.Domain.Entities.Common;
using System.Linq.Expressions;

namespace Core.Application.Repositories;

public interface IReadRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> GetAll(bool tracking = true);
    IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression, bool tracking = true);

    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true);
    Task<TEntity> GetByIdAsync(Guid id, bool tracking = true);
}
