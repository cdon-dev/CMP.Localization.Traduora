using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using Traduora.Provider.Api;

namespace Traduora.Provider
{
    public class TraduoraProvider
    {
        private readonly ITraduoraApi _traduoraApi;
        private const string DefaultFormat = "jsonnested";

        public TraduoraProvider(HttpClient httpClient)
        {
            var refitSettings = new RefitSettings
            {
                ContentSerializer = new JsonContentSerializer(
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    })
            };

            _traduoraApi = RestService.For<ITraduoraApi>(httpClient, refitSettings);
        }

        public async Task<string> Authenticate(string clientId, string clientSecret)
        {
            var authBody = new ApiCredentials { ClientId = clientId, ClientSecret = clientSecret };

            return (await _traduoraApi.GetToken(authBody)).AccessToken;
        }

        public async Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>>
            GetData(string projectId, string authToken,
            string format = DefaultFormat)
        {
            var output = new Dictionary<string, IReadOnlyDictionary<string, string>>();

            LocalesResponse localesJson = await _traduoraApi.GetLocales(projectId, $"Bearer {authToken}");

            foreach (LocaleWrapper locale in localesJson.Data)
            {
                output[locale.Locale.Culture] = await GetTranslations(projectId, locale.Locale.Code, authToken, format);
            }

            return new ReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>(output);
        }

        private async Task<IReadOnlyDictionary<string, string>>
            GetTranslations(string projectId, string locale, string authToken,
            string format = DefaultFormat)
        {
            Dictionary<string, string> exportedResult =
                await _traduoraApi.GetExportedData(projectId, format, locale, $"Bearer {authToken}");

            return new ReadOnlyDictionary<string, string>(exportedResult);
        }
    }
}