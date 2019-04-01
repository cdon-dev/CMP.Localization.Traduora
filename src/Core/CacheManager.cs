using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CacheManager
    {
        private readonly Func<Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>>> _asyncDataProvider;

        private IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> _data;

        private IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> Data
        {
            get
            {
                if (_data != null) return _data;

                _data = _asyncDataProvider().Result;
                return _data;
            }
        }

        public Func<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> DataProvider => () => Data;

        public CacheManager(Func<Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>>> asyncDataProvider)
        {
            _asyncDataProvider = asyncDataProvider;
        }
    }
}
