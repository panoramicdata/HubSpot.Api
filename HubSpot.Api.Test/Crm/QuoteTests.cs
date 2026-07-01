namespace HubSpot.Api.Test.Crm;

public class QuoteTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.Quotes.GetPageAsync(cancellationToken: CancellationToken);
		page.Results.Should().NotBeEmpty();
	}
}
