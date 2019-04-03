using System;
using NetEscapades.Configuration.Validation;

namespace Web.Config
{
    public class TraduoraApiSettings : IValidatable
    {
        public Uri BaseUrl { get; set; }
        public int RefreshSeconds { get; set; } = 30;
        public TimeSpan RefreshIntervalSeconds => TimeSpan.FromSeconds(RefreshSeconds);

        public void Validate()
        {
            if (BaseUrl == null) throw new Exception("BaseUrl cannot be null");
        }
    }
}
