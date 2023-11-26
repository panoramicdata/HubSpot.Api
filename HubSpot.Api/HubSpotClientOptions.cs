using Microsoft.Extensions.Logging;

namespace HubSpot.Api;

public class HubSpotClientOptions
{
	/// <summary>
	/// The HubSpot API key to use for authentication
	/// </summary>
	public string ApiKey { get; set; } = string.Empty;

	/// <summary>
	/// Whether to only allow read-only operations
	/// </summary>
	public bool ReadOnly { get; set; }

	/// <summary>
	/// The User-Agent to use for requests
	/// </summary>
	public string? UserAgent { get; set; }

	/// <summary>
	/// The optional logger factory to use for creating loggers.
	/// Only used if <see cref="Logger"/> is not set.
	/// </summary>
	public ILoggerFactory? LoggerFactory { get; set; }

	/// <summary>
	/// The maximum number of times to try a request before throwing an exception.
	/// </summary>
	public int MaxAttemptCount { get; set; } = 1;

	/// <summary>
	/// The optional logger to use for logging.
	/// </summary>
	public ILogger? Logger { get; set; }
}
