using FluentAssertions;
using Xunit.Abstractions;

namespace HubSpot.Api.Test;

public class LineItemTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async void GetPageAsync_Succeeds()
	{
		var page = await Client.LineItems.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}
}
