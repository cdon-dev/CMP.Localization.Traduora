using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Refit;
using Traduora.Client.Api;

namespace Traduora.Client
{
    public class TraduoraClient
    {
        private readonly ITraduoraApi _traduoraApi;
        private const string DefaultFormat = "jsonnested";

        public TraduoraClient(HttpClient httpClient)
        {
            _traduoraApi = RestService.For<ITraduoraApi>(httpClient);
        }

        public async Task<string> Authenticate(string clientId, string clientSecret)
        {
            var authBody = new ApiCredentials { ClientId = clientId, ClientSecret = clientSecret };

            JToken responseJson = await _traduoraApi.GetToken(authBody);

            return responseJson["data"]["accessToken"].Value<string>();
        }

        public async Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>>
            GetData(string projectId, string authToken,
            string format = DefaultFormat)
        {
            var output = new Dictionary<string, IReadOnlyDictionary<string, string>>();

            JObject localesJson = await _traduoraApi.GetLocales(projectId, $"Bearer {authToken}");

            foreach (JToken localeObject in localesJson["data"].Value<JArray>())
            {
                string localeCode = localeObject["locale"]["code"].Value<string>();

                string culture = localeCode.Replace('_', '-');

                output[culture] = await GetTranslations(projectId, localeCode, authToken, format);
            }

            return new ReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>(output);
        }

        private async Task<IReadOnlyDictionary<string, string>>
            GetTranslations(string projectId, string locale, string authToken,
            string format = DefaultFormat)
        {
            JObject exportedJson = await _traduoraApi.GetExportedData(projectId, format, locale, $"Bearer {authToken}");

            return ToReadOnlyDictionary(exportedJson);
        }

        private static IReadOnlyDictionary<string, string> ToReadOnlyDictionary(JObject exportedJson)
        {
            var output = new Dictionary<string, string>();
            foreach (JProperty jProperty in exportedJson.Properties())
            {
                output[jProperty.Name] = jProperty.Value.Value<string>();
            }

            return new ReadOnlyDictionary<string, string>(output);
        }
    }
}