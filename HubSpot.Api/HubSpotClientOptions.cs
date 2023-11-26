using Microsoft.Extensions.Logging;

namespace HubSpot.Api;

public class HubSpotClientOptions
{
	public string ApiKey { get; set; } = string.Empty;
	public bool ReadOnly { get; set; }
	public string? UserAgent { get; set; }
	public ILoggerFactory? LoggerFactory { get; set; }
	public int MaxAttemptCount { get; set; } = 1;
}
