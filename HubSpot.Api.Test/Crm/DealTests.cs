using FluentAssertions;
using HubSpot.Api.Models;
using Xunit.Abstractions;

namespace HubSpot.Api.Test.Crm;

public class DealTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async void GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.Deals.GetPageAsync();
		page.Results.Should().NotBeEmpty();
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
