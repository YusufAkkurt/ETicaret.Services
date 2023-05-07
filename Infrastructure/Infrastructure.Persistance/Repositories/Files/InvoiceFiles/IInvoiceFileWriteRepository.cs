using Core.Application.Repositories.Files.InvoiceFiles;
using Core.Domain.Entities.Files;
using Infrastructure.Persistance.Contexts;

namespace Infrastructure.Persistance.Repositories.Files.InvoiceFiles;

public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository
{
    public InvoiceFileWriteRepository(ETicaretDbContext context) : base(context) { }
}
