using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml;
using CoderBash.Net.ECB.Constants;
using CoderBash.Net.ECB.Exceptions;
using CoderBash.Net.ECB.Models;

namespace CoderBash.Net.ECB.Client
{
	// TODO set log event Ids
	public sealed class ECBClient : IDisposable
	{
		private readonly HttpClient _client;
		private readonly ECBCurrencies _currencies;

		public ECBClient()
		{
			_client = SetupClient();
			_currencies = new ECBCurrencies();
		}

		public async Task<List<ExchangeRate>> GetDailyRatesAsync(CancellationToken cancellationToken = default)
		{
			return await FetchRatesAsync(ECBConfiguration.ECB_DAILY_ENDPOINT, cancellationToken);
		}

		public async Task<List<ExchangeRate>> GetRecentHistoryAsync(CancellationToken cancellationToken = default)
		{
			return await FetchRatesAsync(ECBConfiguration.ECB_HIST90_ENDPOINT, cancellationToken);
		}

		public async Task<List<ExchangeRate>> GetFullHistoryAsync(CancellationToken cancellationToken = default)
		{
			return await FetchRatesAsync(ECBConfiguration.ECB_HISTORY_ENDPOINT, cancellationToken);
		}

		#region Fetch methods
		private async Task<List<ExchangeRate>> FetchRatesAsync(string fromEndpoint, CancellationToken cancellationToken = default)
		{
			var exchangeRates = new List<ExchangeRate>();

            var ecbResponseContent = await GetDataAsync(fromEndpoint, cancellationToken);

            var rootNode = GetRootCubeNode(ecbResponseContent);

            if (rootNode == null)
            {
                throw new ECBFormatException($"Unable to find a root CUBE node for the response of ECB request '{fromEndpoint}'");
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
				throw new ECBFormatException("ECB response could not be loaded as XML.");
			}

			XmlNode? rootNode = null;

			foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes)
			{
				if (node.Name.Equals("cube", StringComparison.OrdinalIgnoreCase)) {
					rootNode = node;
					break;
				}
			}

			return rootNode;
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
				throw new ECBRequestException($"An error occurred fetching data from ECB endpoint '{endpoint}': {response.ReasonPhrase}");
			}

			var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

			if (responseContent == null)
			{
				throw new ECBRequestException($"Unable to read response content from ECB endpoint '{endpoint}'");
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
				BaseAddress = new Uri(ECBConfiguration.ECB_BASE_URL)
			};

			return client;
		}
        #endregion
    }
}

