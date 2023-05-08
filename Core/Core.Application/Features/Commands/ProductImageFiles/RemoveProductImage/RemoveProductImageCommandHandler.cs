using Core.Application.Abstractions.Storage;
using Core.Application.Repositories.Products;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Commands.ProductImageFiles.RemoveProductImage;

public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IStorageService _storageService;

    public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IStorageService storageService)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _storageService = storageService;
    }

    public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.Table.Include(table => table.ProductImageFiles).FirstOrDefaultAsync(row => row.Id.Equals(request.Id));

        var productImageFile = product?.ProductImageFiles.FirstOrDefault(row => row.Id.Equals(request.ImageId));

        if (productImageFile is not null)
        {
            await _storageService.DeleteAsync(productImageFile.Path.Replace(productImageFile.FileName, ""), productImageFile.FileName);
            product?.ProductImageFiles.Remove(productImageFile);
            await _productWriteRepository.SaveAsync();
        }

        return new();
    }
}
