using Core.Application.Repositories;
using Core.Application.Repositories.Files.InvoiceFiles;
using Core.Domain.Entities.Files;
using Infrastructure.Persistance.Contexts;

namespace Infrastructure.Persistance.Repositories.Files.InvoiceFiles;

public class InvoiceFileReadRepository : ReadRepository<InvoiceFile>, IInvoiceFileReadRepository
{
    public InvoiceFileReadRepository(ETicaretDbContext context) : base(context) { }
}
