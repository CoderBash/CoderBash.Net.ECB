using System;
using System.Diagnostics;
using CoderBash.Net.ECB.Client;

namespace CoderBash.Net.ECB.Tests
{
	[TestFixture(
		Author = "NicolasDemarbaix",
		Category = "CoderBash.Net.ECB",
		Description = "ECB Client Tests",
		TestOf = typeof(ECBClient))]
	public class ECBClientTests
	{
		private const long MAX_PROCESSING_TIME_DAILY_MS = 500;
		private const long MAX_PROCESSING_TIME_HIST90_MS = 500;
		private const long MAX_PROCESSING_TIME_HIST_MS = 2000;

		private const int MIN_EXCHANGERATE_COUNT_DAILY = 30;
		private const int MIN_EXCHANGERATE_COUNT_HIST90 = 1600;
		private const int MIN_EXCHANGERATE_COUNT_HIST = 150000;

		[Test(
			Author = "NicolasDemarbaix",
			Description = "Test ECB daily endpoint")]
		public void Test_ECB_Daily()
		{
			using var client = new ECBClient();

			Assert.DoesNotThrowAsync(async () =>
			{
				var stopwatch = Stopwatch.StartNew();

				var exchangeRates = await client.GetDailyRatesAsync();

				stopwatch.Stop();

				Assert.Multiple(() =>
				{
					Assert.That(exchangeRates, Has.Count.GreaterThanOrEqualTo(MIN_EXCHANGERATE_COUNT_DAILY), $"Expected at least {MIN_EXCHANGERATE_COUNT_DAILY} exchange rates to be fetched from the daily endpoint");
					Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThanOrEqualTo(MAX_PROCESSING_TIME_DAILY_MS), $"ECB daily endpoint should be processed in less than {MAX_PROCESSING_TIME_DAILY_MS}ms");
				});
			});
		}

		[Test(
			Author = "NicolasDemarbaix",
			Description = "Test ECB history (90d) endpoint")]
		public void Test_ECB_History90()
		{
			using var client = new ECBClient();

			Assert.DoesNotThrowAsync(async () =>
			{
				var stopwatch = Stopwatch.StartNew();

				var exchangeRates = await client.GetRecentHistoryAsync();

				stopwatch.Stop();

				Assert.Multiple(() =>
				{
					Assert.That(exchangeRates, Has.Count.GreaterThanOrEqualTo(MIN_EXCHANGERATE_COUNT_HIST90), $"Expected at least {MIN_EXCHANGERATE_COUNT_HIST90} exchange rates to be fetched from the hist90 endpoint (accounted for weekends)");
					Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThanOrEqualTo(MAX_PROCESSING_TIME_HIST90_MS), $"ECB hist90 endpoint should be processed in less than {MAX_PROCESSING_TIME_HIST90_MS}ms");
				});
			});
		}

		[Test(
			Author = "NicolasDemarbaix",
			Description = "Test ECB history endpoiht")]
		public void Test_ECB_History()
		{
			using var client = new ECBClient();

			Assert.DoesNotThrowAsync(async () =>
			{
				var stopwatch = Stopwatch.StartNew();

				var exchangeRates = await client.GetFullHistoryAsync();

				stopwatch.Stop();

				Assert.Multiple(() =>
				{
					Assert.That(exchangeRates, Has.Count.GreaterThanOrEqualTo(MIN_EXCHANGERATE_COUNT_HIST), $"Expected at least {MIN_EXCHANGERATE_COUNT_HIST} exchange rates to be fetched from the hist endpoint (accounted for weekends)");
					Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThanOrEqualTo(MAX_PROCESSING_TIME_HIST_MS), $"ECB hist endpoint should be processed in less than {MAX_PROCESSING_TIME_HIST_MS}ms");
				});
			});
		}
	}
}

