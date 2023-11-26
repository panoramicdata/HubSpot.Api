using HubSpot.Api.Interfaces;

namespace HubSpot.Api;

public class HubSpotClient : IDisposable
{
	private const string HubSpotRootUrl = "https://api.hubapi.com/crm/v3/objects/deals";
	private bool disposedValue;

	public string Version { get; }

	public HubSpotClient(HubSpotClientOptions hubSpotClientOptions)
	{
		var apiClientVersion = new Version(ThisAssembly.AssemblyFileVersion);
		Version = $"{apiClientVersion.Major}.{apiClientVersion.Minor}.{apiClientVersion.Build}";

		var authenticatingHttpClientHandler = new AuthenticatedHttpClientHandler(hubSpotClientOptions);
		var httpClient = new HttpClient(authenticatingHttpClientHandler);

		Deals = Refit.RestService.For<IDeals>(httpClient, HubSpotRootUrlFor("deals"));
	}

	public IDeals Deals { get; set; }

	private static string HubSpotRootUrlFor(string objectType)
		=> $"{HubSpotRootUrl}/{objectType}";

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				// TODO: dispose managed state (managed objects)
			}

			// TODO: free unmanaged resources (unmanaged objects) and override finalizer
			// TODO: set large fields to null
			disposedValue = true;
		}
	}

	// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
	// ~HubSpotClient()
	// {
	//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
	//     Dispose(disposing: false);
	// }

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
