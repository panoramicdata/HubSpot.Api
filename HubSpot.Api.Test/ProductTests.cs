using FluentAssertions;
using Xunit.Abstractions;

namespace HubSpot.Api.Test;

public class ProductTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async void GetPage_Succeeds()
	{
		var page = await Client.Products.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}
}
