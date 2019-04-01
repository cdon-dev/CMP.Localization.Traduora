using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Localization;

namespace Core
{
	public class StringLocalizer : IStringLocalizer
	{
		private readonly Func<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> _modelProvider;

        private CultureInfo _culture;
        private CultureInfo Culture => _culture ?? CultureInfo.CurrentUICulture;

        public StringLocalizer(Func<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> modelProvider)
        {
            _modelProvider = modelProvider;
        }

        public StringLocalizer(
            CultureInfo culture,
            Func<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> modelProvider) : this(modelProvider)
        {
            _culture = culture;
        }

        public LocalizedString this[string name] => new LocalizedString(name, _modelProvider()[Culture.Name][name]);

		public LocalizedString this[string name, params object[] arguments] => new LocalizedString(name, _modelProvider()[Culture.Name][name]);

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _modelProvider()[Culture.Name].Select(kvp => new LocalizedString(kvp.Key, kvp.Value));
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new StringLocalizer(culture, _modelProvider);
        }
    }
}