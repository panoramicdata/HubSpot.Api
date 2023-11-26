using System.Text.Json.Serialization;

namespace HubSpot.Api.Models;

public class Page
{
	[JsonPropertyName("results")]
	public required ICollection<HubSpotObject> Results { get; set; }

	[JsonPropertyName("paging")]
	public Paging? Paging { get; set; }
}
