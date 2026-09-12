namespace HubSpot.Api.Test.Crm;

public class LineItemTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.LineItems.GetPageAsync(new(), CancellationToken);
		page.Results.Should().NotBeEmpty();
	}
}
