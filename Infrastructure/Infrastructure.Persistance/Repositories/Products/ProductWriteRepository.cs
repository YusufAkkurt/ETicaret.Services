using Core.Application.Repositories.Products;
using Core.Domain.Entities;
using Infrastructure.Persistance.Contexts;

namespace Infrastructure.Persistance.Repositories.Products;

public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
{
    public ProductWriteRepository(ETicaretDbContext context) : base(context) { }
}
