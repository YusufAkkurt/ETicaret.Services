using Core.Application.Repositories.Files.ProductImageFiles;
using Core.Domain.Entities.Files;
using Infrastructure.Persistance.Contexts;

namespace Infrastructure.Persistance.Repositories.Files.ProductImageFiles;

public class ProductImageFileWriteRepository : WriteRepository<ProductImageFile>, IProductImageFileWriteRepository
{
    public ProductImageFileWriteRepository(ETicaretDbContext context) : base(context)
    {
    }
}
