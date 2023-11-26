# HubSpot.Api

The HubSpot REST API nuget package, authored by Panoramic Data Limited.

[![Nuget](https://img.shields.io/nuget/v/HubSpot.Api)](https://www.nuget.org/packages/HubSpot.Api/)
[![Nuget](https://img.shields.io/nuget/dt/HubSpot.Api)](https://www.nuget.org/packages/HubSpot.Api/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/59bb860a1129452d8211893953ec056f)](https://app.codacy.com/gh/panoramicdata/HubSpot.Api/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=panoramicdata/HubSpot.Api&amp;utm_campaign=Badge_grade)
If you want some HubSpot software developed, come find us at: https://www.panoramicdata.com/ !

A simple example:

```c#
using HubSpot.Api;

[...]

public static async Task GetAllDeals(ILogger logger, CancellationToken cancellationToken)
{
	using var hubSpotClient = new HubSpotClient(
		new HubSpotClientOptions
		{
			AccessToken = "[ACCESSTOKEN]",
			Logger = logger
		}
	);

	var deals = await hubSpotClient
		.Deals
		.GetPageAsync(cancellationToken)
		.ConfigureAwait(false);

	Console.WriteLine($"Deal Count: {deals.Count}");
}
