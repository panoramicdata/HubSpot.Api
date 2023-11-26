using System.Text.Json.Serialization;

namespace HubSpot.Api.Models;

public class Page
{
	[JsonPropertyName("results")]
	public required ICollection<Result> Results { get; set; }

	[JsonPropertyName("paging")]
	public required Paging Paging { get; set; }
}
