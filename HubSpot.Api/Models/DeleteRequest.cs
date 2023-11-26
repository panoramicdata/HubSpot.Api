using System.Text.Json.Serialization;

namespace HubSpot.Api.Models;

public class DeleteRequest
{
	[JsonPropertyName("objectId")]
	public required string ObjectId { get; set; }

	[JsonPropertyName("idProperty")]
	public string? IdProperty { get; set; }
}
