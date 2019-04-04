using System;
using System.Threading;
using System.Threading.Tasks;

namespace CachedLocalizer.Cache
{
    public class AsyncPollingCache<T>
    {
        private readonly Func<Task<T>> _asyncDataProvider;
        private readonly Timer _refreshTimer;
        private T _data;

        public AsyncPollingCache(Func<Task<T>> asyncDataProvider, TimeSpan refreshInterval)
        {
            _asyncDataProvider = asyncDataProvider;

            _refreshTimer = new Timer(RefreshData, null, refreshInterval, refreshInterval);
        }

        public T GetData()
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
