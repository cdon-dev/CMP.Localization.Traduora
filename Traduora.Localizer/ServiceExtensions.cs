using System.Net.Http;
using DynamicDictionaryLocalizer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Traduora.Localizer.Cache;
using Traduora.Localizer.Config;
using Traduora.Provider;

namespace Traduora.Localizer
{
    public static class ServiceExtensions
    {
        private const string HttpClientName = "traduora";
        public static IServiceCollection AddTraduora(this IServiceCollection services, IConfiguration config)
        {
            services.AddTypedConfiguration(config);

            services
                .AddHttpClient(HttpClientName)
                .ConfigureHttpClient((sp, client) =>
            {
                TraduoraApiSettings traduoraApiSettings = sp.GetRequiredService<IOptions<TraduoraApiSettings>>().Value;
                client.BaseAddress = traduoraApiSettings.BaseUrl;
            });

            services.AddTransient(sp =>
            {
                HttpClient httpClient = sp.GetRequiredService<IHttpClientFactory>()
                    .CreateClient(HttpClientName);

                return new TraduoraProvider(httpClient);
            });

            services.AddTransient<TraduoraService>();

            services.AddSingleton<IStringLocalizer>(sp =>
            {
                TraduoraApiSettings traduoraApiSettings =
                    sp.GetRequiredService<IOptions<TraduoraApiSettings>>().Value;

                var cache = new DictionaryPollingCache(
                    sp.GetRequiredService<TraduoraService>().GetTranslations,
                    traduoraApiSettings.RefreshIntervalSeconds);

                return new DynamicDictionaryStringLocalizer(cache.GetData);
            });

            return services;
        }
    }
}
