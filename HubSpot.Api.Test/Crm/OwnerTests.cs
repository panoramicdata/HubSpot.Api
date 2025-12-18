using FluentAssertions;

namespace HubSpot.Api.Test.Crm;

public class OwnerTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.Owners.GetPageAsync(cancellationToken: CancellationToken);
		page.Results.Should().NotBeEmpty();
		page.Results.Should().AllSatisfy(x => x.Type.Should().Be("PERSON"));
	}
}
