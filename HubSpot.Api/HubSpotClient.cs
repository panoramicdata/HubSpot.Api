using HubSpot.Api.Converters;
using HubSpot.Api.Exceptions;
using HubSpot.Api.Models.Crm;
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

	internal static readonly SystemTextJsonContentSerializer SystemTextJsonContentSerializer = new(new JsonSerializerOptions
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		Converters =
		{
			// Custom converter for AssociationType
			new AssociationTypeConverter(), 

			// General-purpose enum converter
			new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseUpper),
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
			ContentSerializer = SystemTextJsonContentSerializer,
			ExceptionFactory = CreateExceptionAsync
		};

		Analytics = new();
		Auth = new();
		Automation = new();
		BusinessUnits = new();
		CommunicationPreferences = new();
		Conversations = new();
		Cms = new(_httpClient, refitSettings);
		Crm = new(_httpClient, refitSettings);
		Events = new();
		Marketing = new();
		Webhooks = new();
	}

	/// <summary>
	/// Builds the exception that Refit throws for an unsuccessful response.
	/// </summary>
	/// <remarks>
	/// This runs on the failure path of every request, so it must always yield an exception
	/// describing the response rather than throwing one of its own.
	/// </remarks>
	private static async ValueTask<Exception?> CreateExceptionAsync(HttpResponseMessage responseMessage)
	{
		if (responseMessage.IsSuccessStatusCode)
		{
			return null;
		}

		HubSpotError? hubSpotError = null;
		try
		{
			hubSpotError = await SystemTextJsonContentSerializer
				.FromHttpContentAsync<HubSpotError>(responseMessage.Content, CancellationToken.None)
				.ConfigureAwait(false);
		}
		catch (Exception)
		{
			// Intentionally ignored. An error body that is absent, truncated or not in HubSpot's
			// documented error shape is expected — gateways and proxies return HTML, for example —
			// and is handled below by reporting the raw content instead. Every failure mode here is
			// a failure to read or deserialize the body, so there is nothing to recover: the raw
			// content carried by HubSpotApiDeserializationException is strictly more diagnostic.
		}

		if (hubSpotError is not null)
		{
			return new HubSpotApiErrorException(responseMessage.StatusCode, hubSpotError);
		}

		var content = string.Empty;
		try
		{
			content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
		}
		catch (Exception)
		{
			// Intentionally ignored. If the body cannot be read at all — a connection dropped
			// mid-response, for instance — there is no content to report, and throwing from the
			// exception factory would replace the response's own failure with an unrelated error.
			// The empty content is reported instead, which still identifies the failed request.
		}

		return new HubSpotApiDeserializationException(content);
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
