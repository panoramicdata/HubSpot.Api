using System.Text.Json.Serialization;

namespace HubSpot.Api.Models;

public class CreateRequest
{
	[JsonPropertyName("properties")]
	public required IDictionary<string, object?> Properties { get; set; }

	[JsonPropertyName("associations")]
	public required List<AssociationsFor> Associations { get; set; }
}
