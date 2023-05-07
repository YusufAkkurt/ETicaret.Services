using Core.Application.Abstractions.Storage;
using Core.Application.Repositories.Files.ProductImageFiles;
using Core.Application.Repositories.Products;
using Core.Application.RequestParameters;
using Core.Application.ViewModels.Products;
using Core.Domain.Entities;
using Core.Domain.Entities.Files;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IStorageService _storageService;

    public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IStorageService storageService)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _productImageFileReadRepository = productImageFileReadRepository;
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _storageService = storageService;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] Pagination pagination)
    {
        var totalCount = _productReadRepository.GetAll(false).Count();

        var products = _productReadRepository.GetAll(false).Select(_product => new
        {
            _product.Id,
            _product.Name,
            _product.Stock,
            _product.Price,
            _product.CreatedDate,
            _product.UpdatedDate
        }).Skip(pagination.Page * pagination.Size).Take(pagination.Size);

        return Ok(new { totalCount, products });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _productReadRepository.GetByIdAsync(id, false));
    }

    [HttpPost]
    public async Task<IActionResult> Post(VMCreateProduct createProduct)
    {
        await _productWriteRepository.AddAsync(new() { Name = createProduct.Name, Price = createProduct.Price, Stock = createProduct.Stock });
        await _productWriteRepository.SaveAsync();

        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> Put(VMUpdateProduct updateProduct)
    {
        var product = await _productReadRepository.GetByIdAsync(updateProduct.Id);

        product.Name = updateProduct.Name;
        product.Stock = updateProduct.Stock;
        product.Price = updateProduct.Price;

        await _productWriteRepository.SaveAsync();

        return StatusCode((int)HttpStatusCode.Accepted);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _productWriteRepository.RemoveAsync(id);
        await _productWriteRepository.SaveAsync();

        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Upload([FromQuery] Guid id)
    {
        var uploadImageResults = await _storageService.UploadAsync("product-images", Request.Form.Files);

        var product = await _productReadRepository.GetByIdAsync(id);

        await _productImageFileWriteRepository.AddRangeAsync(uploadImageResults.Select(_result => new ProductImageFile()
        {
            FileName = _result.fileName,
            Path = _result.pathOrContainerName,
            Storage = _storageService.StorageName,
            Products = new List<Product>() { product },
        }).ToList());

        await _productImageFileWriteRepository.SaveAsync();

        return Ok();
    }
}
