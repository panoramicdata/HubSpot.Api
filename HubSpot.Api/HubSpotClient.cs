using HubSpot.Api.Sections;
using Refit;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HubSpot.Api;

public class HubSpotClient : IDisposable
{
	private const string HubSpotRootUrl = "https://api.hubapi.com";
	private readonly HttpClient _httpClient;
	private bool disposedValue;

	internal static SystemTextJsonContentSerializer SystemTextJsonContentSerializer = new(new JsonSerializerOptions
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		Converters =
		{
			new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseUpper)
		}
	});

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
			ContentSerializer = SystemTextJsonContentSerializer
		};

		Analytics = new(_httpClient, refitSettings);
		Auth = new(_httpClient, refitSettings);
		Automation = new(_httpClient, refitSettings);
		BusinessUnits = new(_httpClient, refitSettings);
		CommunicationPreferences = new(_httpClient, refitSettings);
		Conversations = new(_httpClient, refitSettings);
		Cms = new(_httpClient, refitSettings);
		Crm = new(_httpClient, refitSettings);
		Events = new(_httpClient, refitSettings);
		Marketing = new(_httpClient, refitSettings);
		Webhooks = new(_httpClient, refitSettings);
	}

	public Analytics Analytics { get; }

	public Auth Auth { get; }

	public Automation Automation { get; }

	public BusinessUnits BusinessUnits { get; }

	public CommunicationPreferences CommunicationPreferences { get; }

	public Conversations Conversations { get; }

	public Cms Cms { get; }

	public Crm Crm { get; }

	public Events Events { get; }

	public Marketing Marketing { get; }

	public Webhooks Webhooks { get; }

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
