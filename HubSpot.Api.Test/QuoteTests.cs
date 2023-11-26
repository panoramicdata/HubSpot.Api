using FluentAssertions;
using Xunit.Abstractions;

namespace HubSpot.Api.Test;

public class QuoteTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async void GetPageAsync_Succeeds()
	{
		var page = await Client.Quotes.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}
}
