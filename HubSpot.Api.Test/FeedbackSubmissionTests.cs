using FluentAssertions;
using Xunit.Abstractions;

namespace HubSpot.Api.Test;

public class FeedbackSubmissionTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async void GetPage_Succeeds()
	{
		var page = await Client.FeedbackSubmissions.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}
}
