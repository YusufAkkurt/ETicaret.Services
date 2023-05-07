using Core.Application.Repositories.Orders;
using Core.Domain.Entities;
using Infrastructure.Persistance.Contexts;

namespace Infrastructure.Persistance.Repositories.Orders;

public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
{
    public OrderReadRepository(ETicaretDbContext context) : base(context) { }
}
