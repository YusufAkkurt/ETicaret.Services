using Core.Domain.Entities.Common;
using Core.Domain.Entities.Files;

namespace Core.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public float Price { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<ProductImageFile> ProductImageFiles { get; set; }
}
