using Core.Application.Repositories.Orders;
using Core.Domain.Entities;
using Infrastructure.Persistance.Contexts;

namespace Infrastructure.Persistance.Repositories.Orders;

public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
{
    public OrderWriteRepository(ETicaretDbContext context) : base(context) { }
}