using Core.Application.Repositories.Files._Bases;
using Infrastructure.Persistance.Contexts;

namespace Infrastructure.Persistance.Repositories.Files._Bases;

public class FileWriteRepository : WriteRepository<Core.Domain.Entities.Files.File>, IFileWriteRepository
{
    public FileWriteRepository(ETicaretDbContext context) : base(context) { }
}
