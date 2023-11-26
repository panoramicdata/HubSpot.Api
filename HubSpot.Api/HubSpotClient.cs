using HubSpot.Api.Interfaces;
using Refit;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HubSpot.Api;

public class HubSpotClient : IDisposable
{
	private const string HubSpotRootUrl = "https://api.hubapi.com/crm/v3/objects";
	private readonly HttpClient _httpClient;
	private bool disposedValue;

	public string Version { get; }

	public HubSpotClient(HubSpotClientOptions hubSpotClientOptions)
	{
		var apiClientVersion = new Version(ThisAssembly.AssemblyFileVersion);
		Version = $"{apiClientVersion.Major}.{apiClientVersion.Minor}.{apiClientVersion.Build}";

		var authenticatingHttpClientHandler = new AuthenticatedHttpClientHandler(hubSpotClientOptions);
		_httpClient = new HttpClient(authenticatingHttpClientHandler)
		{
			BaseAddress = new Uri(HubSpotRootUrl)
		};
		var refitSettings = new RefitSettings
		{
			CollectionFormat = CollectionFormat.Multi,
			ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
				Converters =
				{
					new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseUpper)
				}
			})
		};

		Companies = RestService.For<ICompanies>(_httpClient, refitSettings);
		Contacts = RestService.For<IContacts>(_httpClient, refitSettings);
		Deals = RestService.For<IDeals>(_httpClient, refitSettings);
		FeedbackSubmissions = RestService.For<IFeedbackSubmissions>(_httpClient, refitSettings);
		LineItems = RestService.For<ILineItems>(_httpClient, refitSettings);
		Products = RestService.For<IProducts>(_httpClient, refitSettings);
		Quotes = RestService.For<IQuotes>(_httpClient, refitSettings);
		Tickets = RestService.For<ITickets>(_httpClient, refitSettings);
	}

	public ICompanies Companies { get; set; }
	public IContacts Contacts { get; set; }
	public IDeals Deals { get; set; }
	public IFeedbackSubmissions FeedbackSubmissions { get; set; }
	public ILineItems LineItems { get; set; }
	public IProducts Products { get; set; }
	public IQuotes Quotes { get; set; }
	public ITickets Tickets { get; set; }


	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				_httpClient.Dispose();
			}

			disposedValue = true;
		}
	}

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
