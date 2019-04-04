using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Config
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddTypedConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<TraduoraApiSettings>(config.GetSection("TraduoraApi"));
            services.Configure<TraduoraSecretSettings>(config.GetSection("TraduoraSecrets"));

            return services;
        }
    }
}
