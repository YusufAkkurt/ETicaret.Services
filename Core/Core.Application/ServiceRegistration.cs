using Microsoft.Extensions.DependencyInjection;

namespace Core.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServiceRegistration));
    }
}
