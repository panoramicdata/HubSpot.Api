using FluentAssertions;
using Xunit.Abstractions;

namespace HubSpot.Api.Test;

public class CompanyTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async void GetPageAsync_Succeeds()
	{
		var page = await Client.Companies.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}
}