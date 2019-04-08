using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Traduora.Localizer.Cache
{
    public class TranslationPollingService : BackgroundService
    {
        private readonly Func<Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>>> _asyncDataProvider;
        private readonly TimeSpan _refreshInterval;

        public TranslationPollingService(
            Func<Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>>> asyncDataProvider,
            TimeSpan refreshInterval)
        {
            _asyncDataProvider = asyncDataProvider;
            _refreshInterval = refreshInterval;

            Data = _asyncDataProvider().Result;
        }

        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> Data { get; set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Data = await _asyncDataProvider();
                await Task.Delay(_refreshInterval, stoppingToken);
            }
        }
    }
}
