namespace Core.Application.Features.Commands.Products.RemoveProduct;

public class RemoveProductCommandRequest : IRequest<RemoveProductCommandResponse>
{
    public Guid Id { get; set; }
}
