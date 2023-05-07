namespace Core.Application.ViewModels.Products;

public class VMUpdateProduct
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public float Price { get; set; }
}