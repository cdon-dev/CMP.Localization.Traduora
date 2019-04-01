using Newtonsoft.Json;

namespace Traduora.Client.Api
{
    internal class ApiCredentials
    {
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("clientSecret")]
        public string ClientSecret { get; set; }

        [JsonProperty("grantType")]
        public string GrantType => "client_credentials";
    }
}