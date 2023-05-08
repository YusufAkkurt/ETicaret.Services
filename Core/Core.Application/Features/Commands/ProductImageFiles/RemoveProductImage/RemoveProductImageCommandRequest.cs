namespace Core.Application.Features.Commands.ProductImageFiles.RemoveProductImage;

public class RemoveProductImageCommandRequest : IRequest<RemoveProductImageCommandResponse>
{
    public Guid Id { get; set; }
    public Guid ImageId { get; set; }

    public RemoveProductImageCommandRequest(Guid ıd, Guid ımageId)
    {
        Id = ıd;
        ImageId = ımageId;
    }
}