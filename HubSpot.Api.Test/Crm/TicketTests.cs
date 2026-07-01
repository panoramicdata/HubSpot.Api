using HubSpot.Api.Models.Crm;

namespace HubSpot.Api.Test.Crm;

public class TicketTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task SearchAsync_Succeeds()
	{
		var page = await Client.Crm.Tickets.SearchAsync(new SearchRequest
		{
			After = "",
			Properties = [
				"id"
			],
			Limit = 100,
			Query = null,
			FilterGroups =
			[
				new()
				{
					Filters = [
					new()
					{
						PropertyName = "hs_pipeline",
						Operator = FilterOperator.Eq,
						Value = "0"
					},
					new()
					{
						PropertyName = "hs_pipeline_stage",
						Operator = FilterOperator.Eq,
						Value = "1"
					},
					new()
					{
						PropertyName = "hs_ticket_priority",
						Operator = FilterOperator.Eq,
						Value = "HIGH"
					}
					]
				}
			],
			Sorts = [
				"createdate"
			]
		}, cancellationToken: CancellationToken);
		page.Should().NotBeNull();
	}
}
