namespace HubSpot.Api;

public class HubSpotClientOptions
{
	public string ApiKey { get; set; } = string.Empty;
	public bool ReadOnly { get; internal set; }
}
