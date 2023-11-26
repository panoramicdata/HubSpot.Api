using FluentAssertions;
using Xunit.Abstractions;

namespace HubSpot.Api.Test.Csm;

public class DealTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async void GetPageAsync_Succeeds()
	{
		var page = await Client.Cms.Domains.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}
}
