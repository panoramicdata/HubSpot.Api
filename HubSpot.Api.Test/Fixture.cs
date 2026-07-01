using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace HubSpot.Api.Test;

public class Fixture : TestBedFixture
{
	private IConfigurationRoot? _configuration;

	protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
	{
		if (_configuration is null)
		{
			throw new InvalidOperationException("Configuration is null");
		}

		services
			.AddScoped<CancellationTokenSource>()
			.Configure<TestPortalConfig>(_configuration.GetSection("HubSpotClientOptions"));

#if DEBUG
		services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug));
#endif
	}

	protected override ValueTask DisposeAsyncCore()
		=> default;

	protected override IEnumerable<TestAppSettings> GetTestAppSettings()
	{
		_configuration = new ConfigurationBuilder()
			.AddUserSecrets<Fixture>()
			.Build();

		return [
			new TestAppSettings
			{
				IsOptional = true,
				Filename = null,
			}
		];
	}
}
