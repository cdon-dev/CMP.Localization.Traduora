namespace Traduora.Provider.Api
{
    internal class ApiCredentials
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string GrantType => "client_credentials";
    }
}