using System;
using System.Diagnostics.CodeAnalysis;

namespace CoderBash.Net.ECB.Constants
{
	[ExcludeFromCodeCoverage]
	public class ECBConfiguration
	{
		public const string ECB_BASE_URL = "https://www.ecb.europa.eu/stats/eurofxref/";
		public const string ECB_DAILY_ENDPOINT = "eurofxref-daily.xml";
		public const string ECB_HIST90_ENDPOINT = "eurofxref-hist-90d.xml";
        public const string ECB_HISTORY_ENDPOINT = "eurofxref-hist.xml";
    }
}

