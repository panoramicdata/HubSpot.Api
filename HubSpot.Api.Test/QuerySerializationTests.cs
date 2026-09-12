using HubSpot.Api.Interfaces.Cms;
using HubSpot.Api.Interfaces.Crm;
using HubSpot.Api.Models.Cms;
using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Test;

/// <summary>
/// Pins the query string that the request objects produce.
/// </summary>
/// <remarks>
/// The filters these endpoints take used to be optional method parameters, whose names Refit put
/// on the wire directly. Now that they are properties of a request object, nothing in the type
/// system keeps the wire format from drifting: a renamed property, or a lost <c>AliasAs</c>,
/// would silently send HubSpot a parameter it ignores. These tests capture the outgoing URI so a
/// drift fails here rather than in a live test that needs portal credentials.
/// </remarks>
public class QuerySerializationTests
{
	/// <summary>
	/// Mirrors the settings <see cref="HubSpotClient"/> builds, which is what decides how a
	/// collection property is written out.
	/// </summary>
	private static RefitSettings RefitSettings => new()
	{
		CollectionFormat = CollectionFormat.Multi,
		ContentSerializer = HubSpotClient.SystemTextJsonContentSerializer
	};

	private sealed class UriCapturingHandler(string responseJson) : HttpMessageHandler
	{
		public Uri? RequestUri { get; private set; }

		protected override Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken)
		{
			RequestUri = request.RequestUri;
			return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
			{
				Content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json")
			});
		}
	}

	private const string EmptyCrmPage = """{"results":[]}""";

	private const string EmptyCmsPage = """{"total":0,"results":[]}""";

	private const string OneCompany = """{"id":"123","createdAt":"2024-01-01T00:00:00Z","updatedAt":"2024-01-01T00:00:00Z","archived":false,"properties":{}}""";

	private static (T Api, UriCapturingHandler Handler) Build<T>(string responseJson)
	{
		var handler = new UriCapturingHandler(responseJson);
		var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://api.hubapi.com") };
		return (RestService.For<T>(httpClient, RefitSettings), handler);
	}

	[Fact]
	public async Task CrmPageRequest_WritesEveryFilterUsingHubSpotsOwnNames()
	{
		var (companies, handler) = Build<ICompanies>(EmptyCrmPage);

		await companies.GetPageAsync(
			new CrmPageRequest
			{
				Limit = 10,
				After = "cursor",
				Properties = ["name", "domain"],
				PropertiesWithHistory = ["name"],
				Associations = ["contacts"],
				Archived = false
			},
			TestContext.Current.CancellationToken);

		var query = handler.RequestUri!.Query;
		query.Should().Contain("limit=10");
		query.Should().Contain("after=cursor");
		query.Should().Contain("properties=name").And.Contain("properties=domain");
		query.Should().Contain("propertiesWithHistory=name");
		query.Should().Contain("associations=contacts");
		// Refit writes a bool with ToString(), so "False" rather than "false". HubSpot parses it
		// case-insensitively, and this is what the optional parameters sent before the request
		// objects existed, so it is pinned rather than corrected here.
		query.Should().Contain("archived=False");
	}

	[Fact]
	public async Task CrmPageRequest_LeavesUnsetFiltersOffTheWire()
	{
		var (companies, handler) = Build<ICompanies>(EmptyCrmPage);

		await companies.GetPageAsync(new CrmPageRequest { Limit = 1 }, TestContext.Current.CancellationToken);

		handler.RequestUri!.Query.Should().Be("?limit=1");
	}

	[Fact]
	public async Task CrmGetRequest_WritesEveryFilterUsingHubSpotsOwnNames()
	{
		var (companies, handler) = Build<ICompanies>(OneCompany);

		await companies.GetAsync(
			"123",
			new CrmGetRequest
			{
				Properties = ["name"],
				PropertiesWithHistory = ["domain"],
				Associations = ["deals"],
				Archived = true
			},
			TestContext.Current.CancellationToken);

		handler.RequestUri!.AbsolutePath.Should().Be("/crm/v3/objects/companies/123");
		var query = handler.RequestUri.Query;
		query.Should().Contain("properties=name");
		query.Should().Contain("propertiesWithHistory=domain");
		query.Should().Contain("associations=deals");
		query.Should().Contain("archived=True");
	}

	[Fact]
	public async Task DomainPageRequest_WritesEveryFilterUsingHubSpotsOwnNames()
	{
		var (domains, handler) = Build<IDomains>(EmptyCmsPage);

		await domains.GetPageAsync(
			new DomainPageRequest
			{
				CreatedAfter = new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc),
				Sort = ["domain"],
				After = "cursor",
				Limit = 5,
				Archived = false
			},
			TestContext.Current.CancellationToken);

		var query = handler.RequestUri!.Query;
		// Only the presence of the parameter is asserted: the value is formatted with the current
		// culture rather than ISO-8601, which predates the request objects and is tracked separately.
		query.Should().Contain("createdAfter=");
		query.Should().Contain("sort=domain");
		query.Should().Contain("after=cursor");
		query.Should().Contain("limit=5");
		// Refit writes a bool with ToString(), so "False" rather than "false". HubSpot parses it
		// case-insensitively, and this is what the optional parameters sent before the request
		// objects existed, so it is pinned rather than corrected here.
		query.Should().Contain("archived=False");
		query.Should().NotContain("createdBefore");
	}
}
