using FluentAssertions;
using HubSpot.Api.Models;

namespace HubSpot.Api.Test.Crm;

public class ProductTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.Products.GetPageAsync(cancellationToken: CancellationToken);
		page.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task SearchAsync_ByName_Succeeds()
	{
		var page = await Client
			.Crm
			.Products
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
									PropertyName = "name",
									Operator = FilterOperator.ContainsToken,
									Value = "ReportMagic"
								}
							]
						}
					],
					Limit = 100,
					Properties =
					[
						"name"
					],
					Sorts =
					[
						"name"
					]
				}, cancellationToken: CancellationToken
			);

		page.Results.Should().NotBeEmpty();
	}
}
