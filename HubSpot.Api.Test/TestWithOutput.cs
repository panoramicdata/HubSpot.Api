using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace HubSpot.Api.Test;

public class TestWithOutput : TestBed<Fixture>
{
	protected ILogger Logger { get; }

	protected static CancellationToken CancellationToken => TestContext.Current.CancellationToken;

	public HubSpotClient Client { get; }

	public TestWithOutput(ITestOutputHelper testOutputHelper, Fixture fixture) : base(testOutputHelper, fixture)
	{
		var loggerFactory = fixture.GetService<ILoggerFactory>(testOutputHelper)
			?? throw new InvalidOperationException("LoggerFactory is null");
		Logger = loggerFactory.CreateLogger(GetType());

		var testPortalConfigOptions = fixture
			.GetService<IOptions<TestPortalConfig>>(testOutputHelper)
			?? throw new InvalidOperationException("TestPortalConfig is null");

		Client = new HubSpotClient(new HubSpotClientOptions
		{
			AccessToken = testPortalConfigOptions.Value.AccessToken,
			Logger = Logger
		});
	}
}
