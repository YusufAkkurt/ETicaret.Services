using Core.Application.Repositories.Files.ProductImageFiles;
using Core.Domain.Entities.Files;
using Infrastructure.Persistance.Contexts;

namespace Infrastructure.Persistance.Repositories.Files.ProductImageFiles;

public class ProductImageFileReadRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository
{
    public ProductImageFileReadRepository(ETicaretDbContext context) : base(context)
    {
    }
}