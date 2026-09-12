namespace HubSpot.Api.Test.Cms;

public class DealTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task GetPageAsync_Succeeds()
	{
		var page = await Client.Cms.Domains.GetPageAsync(new(), CancellationToken);
		page.Results.Should().NotBeEmpty();
	}
}
