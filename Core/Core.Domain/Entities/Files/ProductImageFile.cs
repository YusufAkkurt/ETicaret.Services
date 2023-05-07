namespace Core.Domain.Entities.Files;

public class ProductImageFile : File
{
    public virtual ICollection<Product> Products { get; set; }
}
