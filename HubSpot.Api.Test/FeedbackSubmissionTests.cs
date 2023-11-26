using FluentAssertions;
using HubSpot.Api.Exceptions;
using Xunit.Abstractions;

namespace HubSpot.Api.Test;

public class FeedbackSubmissionTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async void GetPageAsync_Succeeds()
	{
		await ((Func<Task>)(() => Client.FeedbackSubmissions.GetPageAsync()))
			.Should()
			.ThrowAsync<HubSpotApiErrorException>();
	}
}
