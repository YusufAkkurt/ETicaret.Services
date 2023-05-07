using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistance;

internal static class Configuration
{
    public static string PostgreSQLConnectionString
    {
        get
        {
            var configurationManager = new ConfigurationManager();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/Presentation.WebAPI"));
            configurationManager.AddJsonFile("appsettings.json");

            return configurationManager.GetConnectionString("PostgreSQL");
        }
    }
}
