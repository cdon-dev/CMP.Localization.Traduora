using Microsoft.Extensions.Localization;
using System;

namespace Core
{
	public class StringLocalizerFactory : IStringLocalizerFactory
	{
		public IStringLocalizer Create(Type resourceSource)
		{
			throw new NotImplementedException();
		}

		public IStringLocalizer Create(string baseName, string location)
		{
			throw new NotImplementedException();
		}
	}
}
