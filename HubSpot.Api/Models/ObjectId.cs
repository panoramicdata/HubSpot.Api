using System.Text.Json.Serialization;

namespace HubSpot.Api.Models;

public class ObjectId
{
	[JsonPropertyName("id")]
	public required string Id { get; set; }
}