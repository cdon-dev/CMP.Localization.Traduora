using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Refit;
using Traduora.Client.Api;

namespace Traduora.Client
{
    public class TraduoraClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://traduora.azurewebsites.net/api/v1";
        private const string DefaultFormat = "jsonnested";

        public TraduoraClient(bool logging = false)
        {
            _httpClient = logging ? new HttpClient(new HttpLoggingHandler()) : new HttpClient();

            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<JObject> GetTranslations(string projectId, string locale, string auth,
            string format = DefaultFormat)
        {
            var traduoraApi = RestService.For<ITraduoraApi>(_httpClient);

            return await traduoraApi.GetExportedData(projectId, format, locale, $"Bearer {auth}");
        }

        public async Task<string> Authenticate(string clientId, string clientSecret)
        {
            var traduoraApi = RestService.For<ITraduoraApi>(_httpClient);

            var authBody = new ApiCredentials {ClientId = clientId, ClientSecret = clientSecret};

            JToken responseJson = await traduoraApi.GetToken(authBody);

            return responseJson["data"]["accessToken"].Value<string>();
        }
    }
}