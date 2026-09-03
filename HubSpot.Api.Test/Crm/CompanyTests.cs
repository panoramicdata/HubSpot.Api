using HubSpot.Api.Models.Crm;

namespace HubSpot.Api.Test.Crm;

public class CompanyTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.Companies.GetPageAsync(cancellationToken: CancellationToken);
		page.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task CreateReadUpdateAndDelete_Succeeds()
	{
		var createRequest = new CreateRequest
		{
			Properties = new Dictionary<string, string>
			{
				{ "name", "Test Inc."},
				{ "website", "https://test.com/"},
			},
			Associations = []
		};

		var createdId = await CrmTestHelpers.CreateOrRecoverIdAsync(
			createRequest,
			(request, cancellationToken) => Client.Crm.Companies.CreateAsync(request, cancellationToken));

		// Re-read the item
		_ = await CrmTestHelpers.ReadAndVerifyAsync(
			createdId,
			(id, cancellationToken) => Client.Crm.Companies.GetAsync(id, cancellationToken: cancellationToken));

		// Delete the item
		await Client
			.Crm
			.Companies
			.ArchiveAsync(createdId, cancellationToken: CancellationToken);
	}

	[Fact]
	public async Task SearchAsync_ByDomain_Succeeds()
	{
		var page = await Client
			.Crm
			.Companies
			.SearchAsync(
				CrmTestHelpers.SearchFor(
					"domain",
					FilterOperator.Eq,
					"panoramicdata.com",
					"domain",
					"company",
					"website"),
				cancellationToken: CancellationToken
			);

		page.Results.Should().NotBeEmpty();
	}
}
