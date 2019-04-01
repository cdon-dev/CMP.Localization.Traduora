using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Traduora.Client;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Index(string locale = "de_DE")
        {
            //Change the 3 variables below for your Traduora project and API Key=======
            string clientId = _config["Traduora2:ClientId"];
            string clientSecret = _config["Traduora2:Secret"];
            string projectId = "03aac4d9-a898-49f0-8546-1343c2964b4a";
            //=========================================================================

            HttpClient httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_config["TraduoraApi:BaseUrl"]);

            var traduoraClient = new TraduoraClient(httpClient);

            string key = await traduoraClient.Authenticate(clientId, clientSecret);

            JObject exportedJson = await traduoraClient.GetTranslations(projectId, locale, key);

            return $"This is the home controller speaking. \n\nHere are some translations: \n\n{exportedJson}";
        }
    }
}