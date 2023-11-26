using Newtonsoft.Json;
using Xunit.Abstractions;

namespace HubSpot.Api.Test;

public abstract class TestBase(ITestOutputHelper testOutputHelper) : IDisposable
{
	private HubSpotClient? _client;
	private bool disposedValue;
	private TestConfiguration? _configuration;
	private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

	private TestConfiguration Configuration
	{
		get
		{
			// Have we already created this?
			if (_configuration is not null)
			{
				// Yes - return that one
				return _configuration;
			}
			// No - we need to create one

			// Load config from file
			var fileInfo = new FileInfo("../../../appsettings.json");

			// Does the config file exist?
			if (!fileInfo.Exists)
			{
				// No - hint to the user what to do
				throw new InvalidOperationException("Missing appsettings.json.  Please copy the appsettings.example.json in the project root folder and set the various values appropriately.");
			}
			// Yes

			// Load in the config
			_configuration = JsonConvert.DeserializeObject<TestConfiguration>(File.ReadAllText(fileInfo.FullName))
				?? throw new FormatException("Invalid appsettings.json file format.");

			_configuration.HubSpotClientOptions.LoggerFactory = _testOutputHelper.BuildLoggerFactory();

			return _configuration;
		}
	}

	protected HubSpotClient Client
	{
		get
		{
			if (_client is not null)
			{
				return _client;
			}

			return _client = new HubSpotClient(Configuration.HubSpotClientOptions);
		}
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				_client?.Dispose();
			}

			// TODO: free unmanaged resources (unmanaged objects) and override finalizer
			// TODO: set large fields to null
			disposedValue = true;
		}
	}

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}