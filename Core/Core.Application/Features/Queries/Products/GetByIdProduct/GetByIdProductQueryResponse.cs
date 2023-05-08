namespace Core.Application.Features.Queries.Products.GetByIdProduct;

public class GetByIdProductQueryResponse
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public float Price { get; set; }

    public GetByIdProductQueryResponse(string name, int stock, float price)
    {
        Name = name;
        Stock = stock;
        Price = price;
    }
}
