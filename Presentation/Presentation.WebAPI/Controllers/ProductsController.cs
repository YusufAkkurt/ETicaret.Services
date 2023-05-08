using Azure.Core;
using Core.Application.Abstractions.Storage;
using Core.Application.Features.Commands.ProductImageFiles.RemoveProductImage;
using Core.Application.Features.Commands.ProductImageFiles.UploadProductImage;
using Core.Application.Features.Commands.Products.CreateProduct;
using Core.Application.Features.Commands.Products.RemoveProduct;
using Core.Application.Features.Commands.Products.UpdateProduct;
using Core.Application.Features.Queries.ProductImageFiles.GetProductImages;
using Core.Application.Features.Queries.Products.GetAllProduct;
using Core.Application.Features.Queries.Products.GetByIdProduct;
using Core.Application.Repositories.Files.ProductImageFiles;
using Core.Application.Repositories.Products;
using Core.Application.ViewModels.Products;
using Core.Domain.Entities;
using Core.Domain.Entities.Files;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    private readonly IConfiguration _configuration;

    private readonly IMediator _mediator;

    public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _productImageFileReadRepository = productImageFileReadRepository;
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _storageService = storageService;
        _configuration = configuration;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest request) => Ok(await _mediator.Send(request));

    [HttpGet("{Id}")]
    public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest request) => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProductCommandRequest request)
    {
        await _mediator.Send(request);

        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest request)
    {
        await _mediator.Send(request);

        return StatusCode((int)HttpStatusCode.Accepted);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest request)
    {
        await _mediator.Send(request);

        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest request)
    {
        request.Files = Request.Form.Files;
        await _mediator.Send(request);

        return Ok();
    }

    [HttpGet("[action]/{Id}")]
    public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest request) => Ok(await _mediator.Send(request));

    [HttpDelete("[action]/{Id}")]
    public async Task<IActionResult> DeleteProductImage([FromQuery] Guid imageId, [FromRoute] Guid id)
    {
        await _mediator.Send(new RemoveProductImageCommandRequest(id, imageId));

        return Ok();
    }
}
