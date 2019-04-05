using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Traduora.Provider.Api
{
    internal class LocalesResponse
    {
        public IEnumerable<LocaleWrapper> Data { get; set; }
    }

    internal class LocaleWrapper
    {
        public Locale Locale { get; set; }
    }

    internal class Locale
    {
        public string Code { get; set; }
        public string Culture => Code.Replace('_', '-');
    }
}
