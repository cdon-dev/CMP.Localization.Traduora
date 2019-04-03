using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Config
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddTypedConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.UseConfigurationValidation();
            services.ConfigureValidatableSetting<TraduoraApiSettings>(config.GetSection("TraduoraApi"));
            services.ConfigureValidatableSetting<TraduoraSecretSettings>(config.GetSection("TraduoraSecrets"));

            return services;
        }
    }
}
