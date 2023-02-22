using System;
namespace CoderBash.Net.ECB.Constants
{
	/// <summary>
	/// 
	/// </summary>
	public class ECBCurrency
	{
		/// <summary>
		/// 
		/// </summary>
		public string IsoCode { get; set; } = null!;

		/// <summary>
		/// 
		/// </summary>
		public string Description { get; set; } = null!;

		/// <summary>
		/// 
		/// </summary>
		public string Symbol { get; set; } = null!;

		public ECBCurrency(string isoCode, string symbol, string description)
		{
			IsoCode = isoCode;
			Symbol = symbol;
			Description = description;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class ECBCurrencies : List<ECBCurrency>
	{
		public ECBCurrencies()
		{
			Add(new ECBCurrency("EUR", "€", "Euro"));
            Add(new ECBCurrency("USD", "$", "United States Dollar"));
            Add(new ECBCurrency("JPY", "¥", "Japanese Yen"));
            Add(new ECBCurrency("BGN", "Lev", "Bulgarian Lev"));
            Add(new ECBCurrency("CZK", "Kč", "Czech Koruna"));
            Add(new ECBCurrency("DKK", "kr", "Danish Krone"));
            Add(new ECBCurrency("GBP", "£", "Pound Sterling"));
            Add(new ECBCurrency("HUF", "Ft", "Hungarian Forint"));
            Add(new ECBCurrency("PLN", "zł", "Polish złoty"));
            Add(new ECBCurrency("RON", "Leu", "Romanian Leu"));
            Add(new ECBCurrency("SEK", "kr", "Swedish Krona"));
            Add(new ECBCurrency("CHF", "Fr", "Swiss Franc"));
            Add(new ECBCurrency("ISK", "kr", "Icelandic Krona"));
            Add(new ECBCurrency("NOK", "kr", "Norwegian Krona"));
            Add(new ECBCurrency("TRY", "₺", "Turkish Lira"));
            Add(new ECBCurrency("AUD", "$", "Australian Dollar"));
            Add(new ECBCurrency("BRL", "R$", "Brazilian Real"));
            Add(new ECBCurrency("CAD", "$", "Canadian Dollar"));
            Add(new ECBCurrency("CNY", "¥", "Renminbi"));
            Add(new ECBCurrency("HKD", "$", "Hong Kong Dollar"));
            Add(new ECBCurrency("IDR", "Rp", "Indonesian Rupiah"));
            Add(new ECBCurrency("ILS", "₪", "Israeli New Shekel"));
            Add(new ECBCurrency("INR", "₹", "Indian Rupee"));
            Add(new ECBCurrency("KRW", "₩", "South Korean Won"));
            Add(new ECBCurrency("MXN", "$", "Mexican Peso"));
            Add(new ECBCurrency("MYR", "RM", "Malaysian Ringgit"));
            Add(new ECBCurrency("NZD", "$", "New Zealand Dollar"));
            Add(new ECBCurrency("PHP", "₱", "Philippine Peso"));
            Add(new ECBCurrency("SGD", "$", "Singapore Dollar"));
            Add(new ECBCurrency("THB", "฿", "Thai Baht"));
            Add(new ECBCurrency("ZAR", "R", "South African Rand"));
        }
	}
}

