using FluentAssertions;
using Xunit.Abstractions;

namespace HubSpot.Api.Test;

public class ContactTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async void GetPage_Succeeds()
	{
		var page = await Client.Contacts.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}
}
