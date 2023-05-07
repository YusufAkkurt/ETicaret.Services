using Core.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    DbSet<TEntity> Table { get; }
}
