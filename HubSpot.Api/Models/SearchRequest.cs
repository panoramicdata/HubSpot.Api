using System.Text.Json.Serialization;

namespace HubSpot.Api.Models;

public class SearchRequest
{
	[JsonPropertyName("query")]
	public string? Query { get; set; }

	[JsonPropertyName("limit")]
	public required int Limit { get; set; }

	[JsonPropertyName("after")]
	public required string After { get; set; }

	[JsonPropertyName("properties")]
	public required List<string> Properties { get; set; }

	[JsonPropertyName("filterGroups")]
	public required List<FilterGroup> FilterGroups { get; set; }
}
