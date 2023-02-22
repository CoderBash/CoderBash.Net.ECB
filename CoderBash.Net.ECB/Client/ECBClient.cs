using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml;
using CoderBash.Net.ECB.Constants;
using CoderBash.Net.ECB.Exceptions;
using CoderBash.Net.ECB.Models;

namespace CoderBash.Net.ECB.Client
{
    /// <summary>
    /// Client for the ECB <see href="https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html">Euro foreign exchange reference rates</see> API.
	/// The base currency for all exchange rates is EURO.
    /// </summary>
    public sealed class EcbClient : IDisposable
	{
		private readonly HttpClient _client;
		private readonly EcbCurrencies _currencies;

		public EcbClient()
		{
			_client = SetupClient();
			_currencies = new EcbCurrencies();
		}

		/// <summary>
		/// Fetch daily rates in all available currencies
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns>List of <see cref="ExchangeRate"/> models</returns>
		public async Task<List<ExchangeRate>> GetDailyRatesAsync(CancellationToken cancellationToken = default)
		{
			return await FetchRatesAsync(EcbConfiguration.ECB_DAILY_ENDPOINT, cancellationToken);
		}

        /// <summary>
        /// Fetch recent historical rates (most recent 90 days) in all available currencies
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List of <see cref="ExchangeRate"/> models</returns>
        public async Task<List<ExchangeRate>> GetRecentHistoryAsync(CancellationToken cancellationToken = default)
		{
			return await FetchRatesAsync(EcbConfiguration.ECB_HIST90_ENDPOINT, cancellationToken);
		}

        /// <summary>
        /// Fetch all historical rates in all available currencies
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List of <see cref="ExchangeRate"/> models</returns>
        public async Task<List<ExchangeRate>> GetFullHistoryAsync(CancellationToken cancellationToken = default)
		{
			return await FetchRatesAsync(EcbConfiguration.ECB_HISTORY_ENDPOINT, cancellationToken);
		}

		#region Fetch methods
		private async Task<List<ExchangeRate>> FetchRatesAsync(string fromEndpoint, CancellationToken cancellationToken = default)
		{
			var exchangeRates = new List<ExchangeRate>();

            var ecbResponseContent = await GetDataAsync(fromEndpoint, cancellationToken);

            var rootNode = GetRootCubeNode(ecbResponseContent);

            if (rootNode == null)
            {
                throw new EcbFormatException($"Unable to find a root CUBE node for the response of ECB request '{fromEndpoint}'");
            }

            foreach (XmlNode cubeNode in rootNode.ChildNodes)
            {
                var processedRates = HandleCubeNode(cubeNode);
                exchangeRates.AddRange(processedRates);
            }

            return exchangeRates;
		}

		private List<ExchangeRate> HandleCubeNode(XmlNode cubeNode)
		{
			var exchangeRates = new List<ExchangeRate>();

			if (!TryGetDate(cubeNode, out var exchangeDate))
			{
				return exchangeRates;
			}

			foreach (XmlNode rateNode in cubeNode.ChildNodes)
			{
				var exchangeRate = HandleRateNode(rateNode, exchangeDate);
				if (exchangeRate != null)
					exchangeRates.Add(exchangeRate);
			}

			return exchangeRates;
		}

		private ExchangeRate? HandleRateNode(XmlNode rateNode, DateTime exchangeDate)
		{
			if (rateNode.Attributes == null
				|| rateNode.Attributes["currency"] == null
				|| rateNode.Attributes["rate"] == null)
			{
				return null;
			}

			var rateAttribute = rateNode.Attributes["rate"]!.Value;
			var currencyAttribute = rateNode.Attributes["currency"]!.Value;

			if (!double.TryParse(rateAttribute, NumberStyles.Number, CultureInfo.InvariantCulture, out var rate))
			{
				return null;
			}

			var currency = _currencies.FirstOrDefault(c => c.IsoCode.Equals(currencyAttribute, StringComparison.OrdinalIgnoreCase));
			if (currency == null)
			{
				return null;
			}

			return new ExchangeRate
			{
				Date = exchangeDate,
				FromCurrency = "EUR",
				Rate = rate,
				ToCurrency = currency.IsoCode
			};
		}
		#endregion

		#region XML helper methods
		private static XmlNode? GetRootCubeNode(string data)
		{
			var xmlDocument = new XmlDocument();
			xmlDocument.Load(new StringReader(data));

			if (xmlDocument.DocumentElement == null)
			{
				throw new EcbFormatException("ECB response could not be loaded as XML.");
			}

			return xmlDocument.DocumentElement
				.ChildNodes
				.Cast<XmlNode>()
				.Where(node => node.Name.Equals("Cube", StringComparison.OrdinalIgnoreCase))
				.FirstOrDefault();
		}

		private static bool TryGetDate(XmlNode forNode, out DateTime exchangeDate)
		{
			exchangeDate = DateTime.MinValue;

			if (forNode.Attributes == null || forNode.Attributes["time"] == null)
			{
				return false;
			}

			var timeAttribute = forNode.Attributes["time"]!.Value;

			return DateTime.TryParse(timeAttribute, out exchangeDate);
		}
		#endregion

		#region Http interface
		private async Task<string> GetDataAsync(string endpoint, CancellationToken cancellationToken = default)
		{
			var response = await _client.GetAsync(endpoint, cancellationToken);

			if (!response.IsSuccessStatusCode)
			{
				throw new EcbRequestException($"An error occurred fetching data from ECB endpoint '{endpoint}': {response.ReasonPhrase}");
			}

			var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

			if (responseContent == null)
			{
				throw new EcbRequestException($"Unable to read response content from ECB endpoint '{endpoint}'");
			}

			return responseContent;
		}
		#endregion

		#region IDisposable implementation
		public void Dispose()
		{
			GC.SuppressFinalize(this);

			_client?.Dispose();
		}
        #endregion

        #region Setup
        private static HttpClient SetupClient()
		{
			var client = new HttpClient
			{
				BaseAddress = new Uri(EcbConfiguration.ECB_BASE_URL)
			};

			return client;
		}
        #endregion
    }
}

