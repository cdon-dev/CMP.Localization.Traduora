using Newtonsoft.Json;

namespace Traduora.Client.Api
{
    internal class ApiCredentials
    {
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("clientSecret")]
        public string ClientSecret { get; set; }

        public string grantType => "client_credentials";
    }
}