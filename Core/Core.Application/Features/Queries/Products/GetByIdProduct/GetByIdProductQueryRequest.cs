namespace Core.Application.Features.Queries.Products.GetByIdProduct;

public class GetByIdProductQueryRequest : IRequest<GetByIdProductQueryResponse>
{
    public Guid Id { get; set; }
}