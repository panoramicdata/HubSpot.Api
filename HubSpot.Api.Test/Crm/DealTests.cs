using HubSpot.Api.Models.Crm;

namespace HubSpot.Api.Test.Crm;

public class DealTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.Deals.GetPageAsync(cancellationToken: CancellationToken);
		page.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task CreateReadUpdateAndDelete_Succeeds()
	{
		var createRequest = new CreateRequest
		{
			Properties = new Dictionary<string, string>
			{
				{ "dealname", "Test Deal"},
				{ "amount", "1500"},
				{ "closedate", "2023-12-31"},
				{ "pipeline", "default"},
				{ "dealstage", "appointmentscheduled"},
			},
			Associations = []
		};

		var createdId = await CrmTestHelpers.CreateOrRecoverIdAsync(
			createRequest,
			(request, cancellationToken) => Client.Crm.Deals.CreateAsync(request, cancellationToken));

		// Re-read the item
		_ = await CrmTestHelpers.ReadAndVerifyAsync(
			createdId,
			(id, cancellationToken) => Client.Crm.Deals.GetAsync(id, cancellationToken: cancellationToken));

		// Delete the item
		await Client
			.Crm
			.Deals
			.ArchiveAsync(createdId, cancellationToken: CancellationToken);
	}

	[Fact]
	public async Task SearchAsync_ByDealName_Succeeds()
	{
		var page = await Client
			.Crm
			.Deals
			.SearchAsync(
				CrmTestHelpers.SearchFor(
					"dealname",
					FilterOperator.Neq,
					"WOO",
					"dealname",
					"amount",
					"closedate",
					"pipeline",
					"dealstage",
					"hubspot_owner_id"),
				cancellationToken: CancellationToken
			);

		page.Results.Should().NotBeEmpty();
	}
}
