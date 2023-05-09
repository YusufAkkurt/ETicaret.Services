using Core.Application.Repositories.Customers;
using Core.Application.Repositories.Files._Bases;
using Core.Application.Repositories.Files.InvoiceFiles;
using Core.Application.Repositories.Files.ProductImageFiles;
using Core.Application.Repositories.Orders;
using Core.Application.Repositories.Products;
using Core.Domain.Entities.Identities;
using Infrastructure.Persistance.Contexts;
using Infrastructure.Persistance.Repositories.Customers;
using Infrastructure.Persistance.Repositories.Files._Bases;
using Infrastructure.Persistance.Repositories.Files.InvoiceFiles;
using Infrastructure.Persistance.Repositories.Files.ProductImageFiles;
using Infrastructure.Persistance.Repositories.Orders;
using Infrastructure.Persistance.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistance;

public static class ServiceRegistration
{
    public static void AddPersistanceServices(this IServiceCollection services)
    {
        services.AddDbContext<ETicaretDbContext>(options => options.UseNpgsql(Configuration.PostgreSQLConnectionString));
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        }).AddEntityFrameworkStores<ETicaretDbContext>();

        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        #region Files
        services.AddScoped<IFileReadRepository, FileReadRepository>();
        services.AddScoped<IFileWriteRepository, FileWriteRepository>();

        services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
        services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

        services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
        services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
        #endregion
    }
}
