using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Traduora.Client;

namespace Web
{
    public class TraduoraService : ITraduoraService
    {
        private readonly IConfiguration _config;
        private readonly TraduoraClient _traduora;

        public TraduoraService(IConfiguration config, TraduoraClient traduora)
        {
            _config = config;
            _traduora = traduora;
        }

        public async Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> GetTranslations()
        {
            //Change the 3 variables below for your Traduora project and API Key=======
            string clientId = _config["Traduora2:ClientId"];
            string clientSecret = _config["Traduora2:Secret"];
            string projectId = "03aac4d9-a898-49f0-8546-1343c2964b4a";
            //=========================================================================

            string key = await _traduora.Authenticate(clientId, clientSecret);

            return await _traduora.GetData(projectId, key);
        }
    }

    public interface ITraduoraService
    {
        Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> GetTranslations();
    }
}
