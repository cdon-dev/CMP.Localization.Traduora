using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Refit;

namespace Traduora.Client.Api
{
    internal interface ITraduoraApi
    {
        [Get("/projects/{projectId}/exports?format={format}&locale={locale}")]
        Task<JObject> GetExportedData(string projectId, string format, string locale,
            [Header("Authorization")] string auth);

        [Post("/auth/token")]
        Task<JObject> GetToken([Body] ApiCredentials apiCredentials);
    }
}