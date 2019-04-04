using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Traduora.Provider;
using Web.Config;

namespace Web
{
    public class TraduoraService : ITraduoraService
    {
        private readonly TraduoraSecretSettings _config;
        private readonly TraduoraProvider _traduora;

        public TraduoraService(IOptions<TraduoraSecretSettings> config, TraduoraProvider traduora)
        {
            _config = config.Value;
            _traduora = traduora;
        }

        public async Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> GetTranslations()
        {
            string key = await _traduora.Authenticate(_config.ClientId, _config.ClientSecret);

            return await _traduora.GetData(_config.ProjectId, key);
        }
    }

    public interface ITraduoraService
    {
        Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> GetTranslations();
    }
}
