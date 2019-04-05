using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Traduora.Localizer.Config;
using Traduora.Provider;

namespace Traduora.Localizer
{
    public class TraduoraService
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
            try
            {
                string key = await _traduora.Authenticate(_config.ClientId, _config.ClientSecret);

                return await _traduora.GetData(_config.ProjectId, key);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                var emptyDictionary = new ReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>
                    (new Dictionary<string, IReadOnlyDictionary<string, string>>());

                return emptyDictionary;
            }
        }
    }
}
