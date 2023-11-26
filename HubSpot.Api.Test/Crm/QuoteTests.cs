using FluentAssertions;
using Xunit.Abstractions;

namespace HubSpot.Api.Test.Crm;

public class QuoteTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async void GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.Quotes.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}
}
