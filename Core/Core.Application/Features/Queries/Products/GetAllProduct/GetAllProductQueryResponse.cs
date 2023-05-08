namespace Core.Application.Features.Queries.Products.GetAllProduct;

public class GetAllProductQueryResponse
{
    public int TotalCount { get; set; }
    public object Products { get; set; }

    public GetAllProductQueryResponse(int totalCount, object products)
    {
        TotalCount = totalCount;
        Products = products;
    }
}