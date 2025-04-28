using FluentAssertions;
using HubSpot.Api.Exceptions;
using HubSpot.Api.Models.Crm;
using System.Net;
using Xunit.Abstractions;

namespace HubSpot.Api.Test.Crm;

public class DealTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetProperties_Succeeds()
	{
		var properties = await Client.Crm.Deals.GetProperties();
		properties.Should().NotBeEmpty();
	}

	[Fact]
	public async void GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.Deals.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async void CreateReadUpdateAndDelete_Succeeds()
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

		HubSpotDeal createdObject;
		try
		{
			createdObject = await Client.Crm.Deals.CreateAsync(createRequest);
			createdObject.Should().NotBeNull();
		}
		catch (HubSpotApiErrorException e) when (e.StatusCode == HttpStatusCode.Conflict)
		{
			e.Error.Category.Should().Be(ErrorCategory.Conflict);
			createdObject = new HubSpotDeal
			{
				Id = e.Message.Split(' ').Last(),
				Properties = createRequest.Properties,
				Archived = false,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};
		}

		// Re-read the item
		var readObject = await Client.Crm.Deals.GetAsync(createdObject.Id);
		readObject.Should().NotBeNull();
		readObject.Id.Should().Be(createdObject.Id);
		readObject.Properties.Should().NotBeEmpty();

		// Delete the item
		await Client
			.Crm
			.Deals
			.ArchiveAsync(createdObject.Id);
	}

	[Fact]
	public async void SearchAsync_ByDealName_Succeeds()
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
				}
			);

		page.Results.Should().NotBeEmpty();
	}
}
