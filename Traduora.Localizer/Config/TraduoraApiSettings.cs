using System;

namespace Traduora.Localizer.Config
{
    public class TraduoraApiSettings
    {
        public Uri BaseUrl { get; set; }
        public int RefreshSeconds { get; set; } = 30;
        public TimeSpan RefreshIntervalSeconds => TimeSpan.FromSeconds(RefreshSeconds);
    }
}
