using FluentAssertions;
using HubSpot.Api.Exceptions;
using HubSpot.Api.Models;
using System.Net;

namespace HubSpot.Api.Test.Crm;

public class DealTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
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

		HubSpotObject createdObject;
		try
		{
			createdObject = await Client.Crm.Deals.CreateAsync(createRequest, cancellationToken: CancellationToken);
			createdObject.Should().NotBeNull();
		}
		catch (HubSpotApiErrorException e) when (e.StatusCode == HttpStatusCode.Conflict)
		{
			e.Error.Category.Should().Be(ErrorCategory.Conflict);
			createdObject = new HubSpotObject
			{
				Id = e.Message.Split(' ').Last(),
				Properties = createRequest.Properties,
				Archived = false,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};
		}

		// Re-read the item
		var readObject = await Client.Crm.Deals.GetAsync(createdObject.Id, cancellationToken: CancellationToken);
		readObject.Should().NotBeNull();
		readObject.Id.Should().Be(createdObject.Id);
		readObject.Properties.Should().NotBeEmpty();

		// Delete the item
		await Client
			.Crm
			.Deals
			.ArchiveAsync(createdObject.Id, cancellationToken: CancellationToken);
	}

	[Fact]
	public async Task SearchAsync_ByDealName_Succeeds()
	{
		var page = await Client
			.Crm
			.Deals
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
									PropertyName = "dealname",
									Operator = FilterOperator.Neq,
									Value = "WOO"
								}
							]
						}
					],
					Limit = 100,
					Properties =
					[
						"dealname",
						"amount",
						"closedate",
						"pipeline",
						"dealstage",
						"hubspot_owner_id"
					],
					Sorts =
					[
						"dealname"
					]
				}, cancellationToken: CancellationToken
			);

		page.Results.Should().NotBeEmpty();
	}
}
