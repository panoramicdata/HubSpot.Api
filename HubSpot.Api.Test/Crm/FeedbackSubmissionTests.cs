using FluentAssertions;
using HubSpot.Api.Exceptions;

namespace HubSpot.Api.Test.Crm;

public class FeedbackSubmissionTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetPageAsync_Succeeds()
	{
		await ((Func<Task>)(() => Client.Crm.FeedbackSubmissions.GetPageAsync(cancellationToken: CancellationToken)))
			.Should()
			.ThrowAsync<HubSpotApiErrorException>();
	}
}
