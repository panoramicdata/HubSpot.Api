using FluentAssertions;
using Xunit.Abstractions;

namespace HubSpot.Api.Test.Crm;

public class QuoteTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetProperties_Succeeds()
	{
		var properties = await Client.Crm.Quotes.GetProperties();
		properties.Should().NotBeEmpty();
	}

	[Fact]
	public async void GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.Quotes.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}
}
