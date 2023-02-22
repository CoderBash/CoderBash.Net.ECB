using System;
namespace CoderBash.Net.ECB.Constants
{
	/// <summary>
	/// 
	/// </summary>
	public class EcbCurrency
	{
		/// <summary>
		/// 
		/// </summary>
		public string IsoCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Symbol { get; set; }

		public EcbCurrency(string isoCode, string symbol, string description)
		{
			IsoCode = isoCode;
			Symbol = symbol;
			Description = description;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class EcbCurrencies : List<EcbCurrency>
	{
		public EcbCurrencies()
		{
			Add(new EcbCurrency("EUR", "€", "Euro"));
            Add(new EcbCurrency("USD", "$", "United States Dollar"));
            Add(new EcbCurrency("JPY", "¥", "Japanese Yen"));
            Add(new EcbCurrency("BGN", "Lev", "Bulgarian Lev"));
            Add(new EcbCurrency("CZK", "Kč", "Czech Koruna"));
            Add(new EcbCurrency("DKK", "kr", "Danish Krone"));
            Add(new EcbCurrency("GBP", "£", "Pound Sterling"));
            Add(new EcbCurrency("HUF", "Ft", "Hungarian Forint"));
            Add(new EcbCurrency("PLN", "zł", "Polish złoty"));
            Add(new EcbCurrency("RON", "Leu", "Romanian Leu"));
            Add(new EcbCurrency("SEK", "kr", "Swedish Krona"));
            Add(new EcbCurrency("CHF", "Fr", "Swiss Franc"));
            Add(new EcbCurrency("ISK", "kr", "Icelandic Krona"));
            Add(new EcbCurrency("NOK", "kr", "Norwegian Krona"));
            Add(new EcbCurrency("TRY", "₺", "Turkish Lira"));
            Add(new EcbCurrency("AUD", "$", "Australian Dollar"));
            Add(new EcbCurrency("BRL", "R$", "Brazilian Real"));
            Add(new EcbCurrency("CAD", "$", "Canadian Dollar"));
            Add(new EcbCurrency("CNY", "¥", "Renminbi"));
            Add(new EcbCurrency("HKD", "$", "Hong Kong Dollar"));
            Add(new EcbCurrency("IDR", "Rp", "Indonesian Rupiah"));
            Add(new EcbCurrency("ILS", "₪", "Israeli New Shekel"));
            Add(new EcbCurrency("INR", "₹", "Indian Rupee"));
            Add(new EcbCurrency("KRW", "₩", "South Korean Won"));
            Add(new EcbCurrency("MXN", "$", "Mexican Peso"));
            Add(new EcbCurrency("MYR", "RM", "Malaysian Ringgit"));
            Add(new EcbCurrency("NZD", "$", "New Zealand Dollar"));
            Add(new EcbCurrency("PHP", "₱", "Philippine Peso"));
            Add(new EcbCurrency("SGD", "$", "Singapore Dollar"));
            Add(new EcbCurrency("THB", "฿", "Thai Baht"));
            Add(new EcbCurrency("ZAR", "R", "South African Rand"));
        }
	}
}

