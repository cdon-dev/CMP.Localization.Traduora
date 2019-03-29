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

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> Index(string locale = "de_DE")
        {
            //Change the 3 variables below for your Traduora project and API Key=======
            string clientId = _config["Traduora2:ClientId"];
            string clientSecret = _config["Traduora2:Secret"];
            string projectId = "03aac4d9-a898-49f0-8546-1343c2964b4a";
            //=========================================================================

            var traduoraClient = new TraduoraClient(_config["TraduoraApi:BaseUrl"]);

            string key = await traduoraClient.Authenticate(clientId, clientSecret);

            JObject exportedJson = await traduoraClient.GetTranslations(projectId, locale, key);

            return $"This is the home controller speaking. \n\nHere are some translations: \n\n{exportedJson}";
        }
    }
}