using System;
namespace CoderBash.Net.ECB.Models
{
    /// <summary>
    /// Model for the exchange rates of the ECB <see href="https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html">Euro foreign exchange reference rates</see> API
    /// </summary>
    public class ExchangeRate
	{
		/// <summary>
		/// The base currency of the exchange rate (defaults to EURO).
		/// </summary>
		public string FromCurrency { get; set; } = null!;

		/// <summary>
		/// The target currency of the exchange rate
		/// </summary>
		public string ToCurrency { get; set; } = null!;

		/// <summary>
		/// The date to which the exchange rate applies
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// The rate of the exchange rate (target currency value = (base currency value * rate))
		/// </summary>
		public double Rate { get; set; }
	}
}

