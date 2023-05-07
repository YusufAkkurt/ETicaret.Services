using Core.Application.Repositories.Files._Bases;
using Infrastructure.Persistance.Contexts;

namespace Infrastructure.Persistance.Repositories.Files._Bases;

public class FileReadRepository : ReadRepository<Core.Domain.Entities.Files.File>, IFileReadRepository
{
    public FileReadRepository(ETicaretDbContext context) : base(context) { }
}
