using HubSpot.Api.Exceptions;

namespace HubSpot.Api.Test.Crm;

public class FeedbackSubmissionTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task GetPageAsync_Succeeds()
	{
		await ((Func<Task>)(() => Client.Crm.FeedbackSubmissions.GetPageAsync(cancellationToken: CancellationToken)))
			.Should()
			.ThrowAsync<HubSpotApiErrorException>();
	}
}
