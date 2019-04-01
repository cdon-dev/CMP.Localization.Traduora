using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public class CacheManager
    {
        private readonly Func<Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>>> _asyncDataProvider;

        private IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> _data;

        private IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> GetData()
        {
            if (_data != null) return _data;

            _data = _asyncDataProvider().Result;
            return _data;
        }

        private readonly Timer _refreshTimer;

        public Func<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> DataProvider => GetData;

        public CacheManager(Func<Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>>> asyncDataProvider, int refreshMilliseconds)
        {
            _asyncDataProvider = asyncDataProvider;

            _refreshTimer = new Timer(RefreshData, null, refreshMilliseconds, refreshMilliseconds);
        }

        private async void RefreshData(object _)
        {
            _data = await _asyncDataProvider();
        }
    }
}
