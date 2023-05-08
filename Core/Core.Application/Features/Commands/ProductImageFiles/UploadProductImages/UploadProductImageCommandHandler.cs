using Core.Application.Abstractions.Storage;
using Core.Application.Repositories.Files.ProductImageFiles;
using Core.Application.Repositories.Products;
using Core.Domain.Entities;
using Core.Domain.Entities.Files;

namespace Core.Application.Features.Commands.ProductImageFiles.UploadProductImage;

public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
{
    private readonly IStorageService _storageService;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

    public UploadProductImageCommandHandler(IStorageService storageService, IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
    {
        _storageService = storageService;
        _productReadRepository = productReadRepository;
        _productImageFileWriteRepository = productImageFileWriteRepository;
    }

    public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        var uploadImageResults = await _storageService.UploadAsync("product-images", request.Files);

        var product = await _productReadRepository.GetByIdAsync(request.Id);

        await _productImageFileWriteRepository.AddRangeAsync(uploadImageResults.Select(_result => new ProductImageFile()
        {
            FileName = _result.fileName,
            Path = _result.pathOrContainerName,
            Storage = _storageService.StorageName,
            Products = new List<Product>() { product },
        }).ToList());

        await _productImageFileWriteRepository.SaveAsync();

        return new();
    }
}
