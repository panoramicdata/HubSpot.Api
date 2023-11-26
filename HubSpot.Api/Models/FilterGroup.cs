using System.Text.Json.Serialization;

namespace HubSpot.Api.Models;

public class FilterGroup
{
	[JsonPropertyName("filters")]
	public required List<Filter> Filters { get; set; }
}