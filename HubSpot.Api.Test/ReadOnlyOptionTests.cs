namespace HubSpot.Api.Test;

/// <summary>
/// Pure unit tests: a refused write throws before any request is sent, so no credentials or portal are needed.
/// </summary>
public class ReadOnlyOptionTests
{
	private const string FakeToken = "fake-token-for-tests-0123456789abcdef";

	[Fact]
	public void IsReadOnly_DefaultsToFalse()
		=> new HubSpotClientOptions().IsReadOnly.Should().BeFalse();

	[Fact]
	public async Task WriteIsRefused_WhenIsReadOnly()
	{
		using var client = new HubSpotClient(new HubSpotClientOptions { AccessToken = FakeToken, IsReadOnly = true });

		var act = () => client.Crm.Companies.ArchiveAsync("1", TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<InvalidOperationException>();
	}

#pragma warning disable CS0618 // The alias is what is under test
	[Fact]
	public void ObsoleteReadOnly_IsAnAliasOfIsReadOnly()
	{
		var options = new HubSpotClientOptions { ReadOnly = true };

		options.IsReadOnly.Should().BeTrue();

		options.IsReadOnly = false;

		options.ReadOnly.Should().BeFalse();
	}
#pragma warning restore CS0618
}
