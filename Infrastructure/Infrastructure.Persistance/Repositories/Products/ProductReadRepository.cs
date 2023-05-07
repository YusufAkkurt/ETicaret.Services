using Core.Application.Repositories.Products;
using Core.Domain.Entities;
using Infrastructure.Persistance.Contexts;

namespace Infrastructure.Persistance.Repositories.Products;

public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
{
    public ProductReadRepository(ETicaretDbContext context) : base(context) { }
}
