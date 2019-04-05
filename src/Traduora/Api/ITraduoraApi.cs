using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace Traduora.Provider.Api
{
    internal interface ITraduoraApi
    {
        [Get("/projects/{projectId}/exports?format={format}&locale={locale}")]
        Task<Dictionary<string, string>> GetExportedData(string projectId, string format, string locale,
            [Header("Authorization")] string auth);

        [Get("/projects/{projectId}/translations")]
        Task<LocalesResponse> GetLocales(string projectId, [Header("Authorization")] string auth);

        [Post("/auth/token")]
        Task<AuthResponse> GetToken([Body] ApiCredentials apiCredentials);
    }
}