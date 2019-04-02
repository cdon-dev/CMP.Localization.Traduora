using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Traduora.Provider;

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
            string projectId = _config["Traduora2:ProjectId"]; ;
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
