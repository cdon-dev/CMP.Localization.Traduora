using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CachedLocalizer
{
    public class CacheManager
    {
        private readonly Func<Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>>> _asyncDataProvider;

        private IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> _data;

        private readonly Timer _refreshTimer;

        public CacheManager(Func<Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>>> asyncDataProvider, TimeSpan refreshInterval)
        {
            _asyncDataProvider = asyncDataProvider;

            _refreshTimer = new Timer(RefreshData, null, refreshInterval, refreshInterval);
        }

        public Func<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> DataProvider => GetData;

        private IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> GetData()
        {
            if (_data != null) return _data;

            _data = _asyncDataProvider().Result;
            return _data;
        }

        private async void RefreshData(object _)
        {
            _data = await _asyncDataProvider();
        }
    }
}
