# HubSpot.Api

The HubSpot REST API nuget package, authored by Panoramic Data Limited.

[![Nuget](https://img.shields.io/nuget/v/HubSpot.Api)](https://www.nuget.org/packages/HubSpot.Api/)
[![Nuget](https://img.shields.io/nuget/dt/HubSpot.Api)](https://www.nuget.org/packages/HubSpot.Api/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx)](https://www.codacy.com/gh/panoramicdata/HubSpot.Api/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=panoramicdata/HubSpot.Api&amp;utm_campaign=Badge_Grade)

If you want some LogicMonitor software developed, come find us at: https://www.panoramicdata.com/ !

A simple example:

```c#
using HubSpot.Api;

[...]

public static async Task GetAllDevices(ILogger logger, CancellationToken cancellationToken)
{
	using var hubSpotClient = new HubSpotClient(
		new HubSpotClientOptions
		{
			Key = "accessKey",
			Logger = logger
		}
	);

	var deals = await hubSpotClient
		.Deals
		.GetAllAsync(cancellationToken)
		.ConfigureAwait(false);

	Console.WriteLine($"Deal Count: {deals.Count}");
}
