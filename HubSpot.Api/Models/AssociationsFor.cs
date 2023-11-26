using System.Text.Json.Serialization;

namespace HubSpot.Api.Models;

public class AssociationsFor
{
	[JsonPropertyName("types")]
	public required List<AssociationSpec> Types { get; set; }

	[JsonPropertyName("to")]
	public required ObjectId To { get; set; }
}