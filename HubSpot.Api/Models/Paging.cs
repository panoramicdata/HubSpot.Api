using System.Text.Json.Serialization;

namespace HubSpot.Api.Models;

public class Paging
{
	[JsonPropertyName("next")]
	public required PagingNext Next { get; set; }
}
