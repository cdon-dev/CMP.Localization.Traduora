using Newtonsoft.Json.Linq;

namespace Traduora.Provider.Api
{
    internal class AuthResponse
    {
        public JObject Data { get; set; }
        public string AccessToken => Data["accessToken"].Value<string>();
    }
}
