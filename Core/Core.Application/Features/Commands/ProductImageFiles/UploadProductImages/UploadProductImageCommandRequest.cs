using Microsoft.AspNetCore.Http;

namespace Core.Application.Features.Commands.ProductImageFiles.UploadProductImage;

public class UploadProductImageCommandRequest : IRequest<UploadProductImageCommandResponse>
{
    public Guid Id { get; set; }
    public IFormFileCollection? Files { get; set; }
}