using Core.Domain.Entities.Common;

namespace Core.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Order> Orders { get; set; }
}