using System;
using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Web;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public const string HttpClientName = "traduora";
        public static IServiceCollection AddTraduora(this IServiceCollection services)
        {
            services.AddHttpClient(HttpClientName)
                .ConfigureHttpClient((sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                client.BaseAddress = new Uri(config["TraduoraApi:BaseUrl"]);
            });

            services.AddTransient<ITraduoraService, TraduoraService>();

            services.AddSingleton<IStringLocalizer>(sp =>
            {
                var cache = new CacheManager(sp.GetRequiredService<ITraduoraService>().GetTranslations);

                return new StringLocalizer(cache.DataProvider);
            });

            return services;
        }
    }
}
