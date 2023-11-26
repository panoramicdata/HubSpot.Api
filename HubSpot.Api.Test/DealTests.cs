using FluentAssertions;
using Xunit.Abstractions;

namespace HubSpot.Api.Test;

public class DealTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async void GetPageAsync_Succeeds()
	{
		var page = await Client.Deals.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}
}
