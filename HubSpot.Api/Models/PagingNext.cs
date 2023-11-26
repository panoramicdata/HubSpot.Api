using System.Text.Json.Serialization;

namespace HubSpot.Api.Models;

public class PagingNext
{
	[JsonPropertyName("after")]
	public required string After { get; set; }

	[JsonPropertyName("link")]
	public required string Link { get; set; }
}