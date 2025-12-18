using System.Text.Json;

namespace HubSpot.Api.Test;

public abstract class TestBase(ITestOutputHelper testOutputHelper) : IDisposable
{
	private HubSpotClient? _client;
	private bool disposedValue;
	private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

	protected static CancellationToken CancellationToken => TestContext.Current.CancellationToken;

	private TestConfiguration Configuration
	{
		get
		{
			// Have we already created this?
			if (field is not null)
			{
				// Yes - return that one
				return field;
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
			field = JsonSerializer.Deserialize<TestConfiguration>(File.ReadAllText(fileInfo.FullName))
				?? throw new FormatException("Invalid appsettings.json file format.");

			return field;
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