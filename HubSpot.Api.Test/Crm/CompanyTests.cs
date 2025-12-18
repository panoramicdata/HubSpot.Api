using FluentAssertions;
using HubSpot.Api.Exceptions;
using HubSpot.Api.Models.Crm;
using System.Net;

namespace HubSpot.Api.Test.Crm;

public class CompanyTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
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

		HubSpotCompany createdObject;
		try
		{
			createdObject = await Client.Crm.Companies.CreateAsync(createRequest, cancellationToken: CancellationToken);
			createdObject.Should().NotBeNull();
		}
		catch (HubSpotApiErrorException e) when (e.StatusCode == HttpStatusCode.Conflict)
		{
			e.Error.Category.Should().Be(ErrorCategory.Conflict);
			createdObject = new HubSpotCompany
			{
				Id = e.Message.Split(' ').Last(),
				Properties = createRequest.Properties,
				Archived = false,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};
		}

		// Re-read the item
		var readObject = await Client.Crm.Companies.GetAsync(createdObject.Id, cancellationToken: CancellationToken);
		readObject.Should().NotBeNull();
		readObject.Id.Should().Be(createdObject.Id);
		readObject.Properties.Should().NotBeEmpty();

		// Delete the item
		await Client
			.Crm
			.Companies
			.ArchiveAsync(createdObject.Id, cancellationToken: CancellationToken);
	}

	[Fact]
	public async Task SearchAsync_ByDomain_Succeeds()
	{
		var page = await Client
			.Crm
			.Companies
			.SearchAsync(
				new SearchRequest
				{
					After = "",
					FilterGroups =
					[
						new()
						{
							Filters =
							[
								new Filter
								{
									PropertyName = "domain",
									Operator = FilterOperator.Eq,
									Value = "panoramicdata.com"
								}
							]
						}
					],
					Limit = 100,
					Properties =
					[
						"domain",
						"company",
						"website"
					],
					Sorts =
					[
						"domain"
					]
				}, cancellationToken: CancellationToken
			);

		page.Results.Should().NotBeEmpty();
	}
}