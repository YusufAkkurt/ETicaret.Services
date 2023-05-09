using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Entities.Identities;

public class AppUser : IdentityUser<Guid>
{
    public string NameSurname { get; set; }
}
