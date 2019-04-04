using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Localization;

namespace CachedLocalizer
{
	public class CachedDictionaryStringLocalizer : IStringLocalizer
	{
		private readonly Func<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> _modelProvider;

        private readonly CultureInfo _culture;
        private CultureInfo Culture => _culture ?? CultureInfo.CurrentUICulture;

        public CachedDictionaryStringLocalizer(Func<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> modelProvider)
        {
            _modelProvider = modelProvider;
        }

        public CachedDictionaryStringLocalizer(
            CultureInfo culture,
            Func<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> modelProvider) : this(modelProvider)
        {
            _culture = culture;
        }

        public LocalizedString this[string name]
        {
            get
            {
                try
                {
                    return new LocalizedString(name, _modelProvider()[Culture.Name][name]);
                }
                catch
                {
                    return new LocalizedString(name, name);
                }
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                try
                {
                    string formattedString = string.Format(_modelProvider()[Culture.Name][name], arguments);

                    return new LocalizedString(name, formattedString);
                }
                catch
                {
                    return new LocalizedString(name, name);
                }
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _modelProvider()[Culture.Name].Select(kvp => new LocalizedString(kvp.Key, kvp.Value));
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new CachedDictionaryStringLocalizer(culture, _modelProvider);
        }
    }
}