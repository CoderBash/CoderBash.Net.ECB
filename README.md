# CoderBash.Net.ECB

[![.NET](https://github.com/CoderBash/CoderBash.Net.ECB/actions/workflows/dotnet.yml/badge.svg)](https://github.com/CoderBash/CoderBash.Net.ECB/actions/workflows/dotnet.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=coderbash-net-ecb&metric=alert_status&token=3ed4d70545ff16dca9e795e5912ce5e9f402c275)](https://sonarcloud.io/summary/new_code?id=coderbash-net-ecb)

Integrate the [European Central Bank Euro Exchange Rate](https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html) API into your .NET project!

The CoderBash ECB .NET Library targets the .NET 6 and .NET 7 frameworks.

## Installation with Nuget
To install the library via NuGet:
* Search for `CoderBash.Net.ECB` in the NuGet Library, or
* Type `Install-Package CoderBash.Net.ECB` in the NuGet Package Manager, or
* Install using the dotnet CLI with `dotnet add package CoderBash.Net.ECB`

## Usage
### 1. Create a new client object
```C#
using var client = new EcbClient();
```

### 2. Request exchange rates through one of the provided methods
```C#
// Fetch only the latest available rates
var exchangeRates = await client.GetDailyRatesAsync();

// Fetch the recent historical rates (latest 90 days)
var exchangeRates = await client.GetRecentHistoryAsync();

// Fetch all historical rates
var exchangeRates = await client.GetFullHistoryAsync();
```

## Important notes
### Exchange rates updates
Updates are published at around `16:00 CET` on working days. During weekends and public holidays, no updates will be published by the European Central Bank. See [ECB Working Hours](https://www.ecb.europa.eu/services/contacts/working-hours/html/index.en.html) for more information on possible closing days.

### Available currencies
The CoderBash ECB .NET Library currently only supports (and returns) rates for the currencies that have been made publicly availble at the time of writing. See [ECB Euro Foreign Exchange Reference Rates](https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html) home page for available currencies.
