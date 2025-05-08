using FluentAssertions;
using HubSpot.Api.Models.Crm;
using Xunit.Abstractions;

namespace HubSpot.Api.Test.Crm;

public class ProductTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetProperties_Succeeds()
	{
		var properties = await Client.Crm.Products.GetProperties();
		properties.Should().NotBeEmpty();
	}

	[Fact]
	public async void GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.Products.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async void SearchAsync_ByName_Succeeds()
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
				}
			);

		page.Results.Should().NotBeEmpty();
	}
}