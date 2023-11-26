using System.Text.Json.Serialization;

namespace HubSpot.Api.Models;

public class HubSpotError
{
	[JsonPropertyName("status")]
	public required string Status { get; set; }

	[JsonPropertyName("message")]
	public required string Message { get; set; }

	[JsonPropertyName("correlationId")]
	public required string CorrelationId { get; set; }

	[JsonPropertyName("category")]
	public required ErrorCategory Category { get; set; }
}

