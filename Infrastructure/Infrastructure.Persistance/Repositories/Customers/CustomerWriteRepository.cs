using Core.Application.Repositories.Customers;
using Core.Domain.Entities;
using Infrastructure.Persistance.Contexts;

namespace Infrastructure.Persistance.Repositories.Customers;

public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
{
    public CustomerWriteRepository(ETicaretDbContext context) : base(context) { }
}
