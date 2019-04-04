using System.Net.Http;
using CachedLocalizer;
using CachedLocalizer.Cache;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Traduora.Provider;
using Web.Config;

namespace Web
{
    public static class ServiceExtensions
    {
        private const string HttpClientName = "traduora";
        public static IServiceCollection AddTraduora(this IServiceCollection services)
        {
            services
                .AddHttpClient(HttpClientName)
                .ConfigureHttpClient((sp, client) =>
            {
                var traduoraApiSettings = sp.GetRequiredService<IOptions<TraduoraApiSettings>>().Value;
                client.BaseAddress = traduoraApiSettings.BaseUrl;
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
                var traduoraApiSettings = sp.GetRequiredService<IOptions<TraduoraApiSettings>>().Value;

                var cache = new DictionaryPollingCache(
                    sp.GetRequiredService<ITraduoraService>().GetTranslations,
                    traduoraApiSettings.RefreshIntervalSeconds);

                return new CachedDictionaryStringLocalizer(cache.GetData);
            });

            return services;
        }
    }
}
