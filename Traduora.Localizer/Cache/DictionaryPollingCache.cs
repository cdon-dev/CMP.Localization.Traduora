using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Traduora.Localizer.Cache
{
    public class DictionaryPollingCache : AsyncPollingCache<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>>
    {
        public DictionaryPollingCache(
            Func<Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>>> asyncDataProvider, TimeSpan refreshInterval) :
            base(asyncDataProvider, refreshInterval)
        {}
    }
}
