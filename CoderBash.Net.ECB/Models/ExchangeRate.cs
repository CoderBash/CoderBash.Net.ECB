using System;
namespace CoderBash.Net.ECB.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class ExchangeRate
	{
		/// <summary>
		/// 
		/// </summary>
		public string FromCurrency { get; set; } = null!;

		/// <summary>
		/// 
		/// </summary>
		public string ToCurrency { get; set; } = null!;

		/// <summary>
		/// 
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public double Rate { get; set; }
	}
}

