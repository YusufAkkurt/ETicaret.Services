using Core.Domain.Entities.Common;

namespace Core.Application.Repositories;

public interface IWriteRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    Task<bool> AddAsync(TEntity entity);
    Task<bool> AddRangeAsync(List<TEntity> entities);

    bool Remove(TEntity entity);
    bool RemoveRange(List<TEntity> entities);
    Task<bool> RemoveAsync(Guid id);
    
    bool Update(TEntity entity);
    bool UpdateRange(List<TEntity> entities);
    
    Task<bool> SaveAsync();
}
