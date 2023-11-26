using System.Text.Json.Serialization;

namespace HubSpot.Api.Models;

public class Result
{
	[JsonPropertyName("id")]
	public required string Id { get; set; }

	[JsonPropertyName("properties")]
	public required IDictionary<string, object?> Properties { get; set; }

	[JsonPropertyName("createdAt")]
	public required DateTime CreatedAt { get; set; }

	[JsonPropertyName("updatedAt")]
	public required DateTime UpdatedAt { get; set; }

	[JsonPropertyName("archived")]
	public required bool Archived { get; set; }
}
