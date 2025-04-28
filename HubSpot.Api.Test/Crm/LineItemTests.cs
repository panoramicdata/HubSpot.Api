using FluentAssertions;
using Xunit.Abstractions;

namespace HubSpot.Api.Test.Crm;

public class LineItemTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetProperties_Succeeds()
	{
		var properties = await Client.Crm.LineItems.GetProperties();
		properties.Should().NotBeEmpty();
	}

	[Fact]
	public async void GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.LineItems.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}
}
