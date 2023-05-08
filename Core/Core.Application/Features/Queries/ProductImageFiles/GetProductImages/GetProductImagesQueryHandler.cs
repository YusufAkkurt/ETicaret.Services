using Core.Application.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core.Application.Features.Queries.ProductImageFiles.GetProductImages;

public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IConfiguration _configuration;

    public GetProductImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
    {
        _productReadRepository = productReadRepository;
        _configuration = configuration;
    }

    public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.Table.Include(table => table.ProductImageFiles).AsNoTracking().FirstOrDefaultAsync(row => row.Id.Equals(request.Id));

        var response = product.ProductImageFiles.Select(image => new GetProductImagesQueryResponse(id:image.Id, fileName: image.FileName, path: $"{_configuration["BaseStorageUrl"]}/{image.Path}"));

        return response.ToList();
    }
}
