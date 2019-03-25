using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Core
{
	public class StringLocalizer : IStringLocalizer
	{
		readonly Func<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> modelProvider;

		public StringLocalizer(Func<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> modelProvider)
		{
			this.modelProvider = modelProvider;
		}

		public LocalizedString this[string name] => throw new NotImplementedException();

		public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

		public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
		{
			throw new NotImplementedException();
		}

		public IStringLocalizer WithCulture(CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
