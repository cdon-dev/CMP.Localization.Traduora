using System;
using System.Net.Http;
using CachedLocalizer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Traduora.Provider;

namespace Web
{
    public static class ServiceExtensions
    {
        private const string HttpClientName = "traduora";
        public static IServiceCollection AddTraduora(this IServiceCollection services)
        {
            services.AddHttpClient(HttpClientName)
                .ConfigureHttpClient((sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                client.BaseAddress = new Uri(config["TraduoraApi:BaseUrl"]);
            });

            services.AddTransient(sp =>
            {
                HttpClient httpClient = sp.GetRequiredService<IHttpClientFactory>()
                    .CreateClient(HttpClientName);

                return new TraduoraProvider(httpClient);
            });

            services.AddTransient<ITraduoraService, TraduoraService>();

            services.AddSingleton<IStringLocalizer>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                int refreshMilliseconds = Convert.ToInt32(config["TraduoraApi:RefreshMilliseconds"]);

                // TODO: use typed configuration
                var cache = new CacheManager(sp.GetRequiredService<ITraduoraService>().GetTranslations, TimeSpan.FromMilliseconds(refreshMilliseconds));

                return new CachedDictionaryStringLocalizer(cache.DataProvider);
            });

            return services;
        }
    }
}
