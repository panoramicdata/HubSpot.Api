using System.Text.Json.Serialization;

namespace HubSpot.Api.Models;

public class AssociationSpec
{
	[JsonPropertyName("associationCategory")]
	public required AssociationCategory Category { get; set; }

	[JsonPropertyName("associationTypeId")]
	public required int AssociationTypeId { get; set; }
}