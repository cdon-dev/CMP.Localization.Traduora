using Web;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddTraduora(this IServiceCollection services)
        {
            services.AddHttpClient("HttpClient");
            services.AddTransient<ITraduoraService, TraduoraService>();

            return services;
        }
    }
}
