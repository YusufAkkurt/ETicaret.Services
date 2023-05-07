using Core.Domain.Entities.Common;

namespace Core.Domain.Entities;

public class Order : BaseEntity
{
    public Guid CustomerId { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }

    public virtual Customer Customer { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}
