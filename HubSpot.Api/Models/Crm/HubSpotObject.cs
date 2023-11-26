namespace HubSpot.Api.Models;

public class HubSpotObject
{
	public required string Id { get; set; }

	public required IDictionary<string, object?> Properties { get; set; }

	public required DateTime CreatedAt { get; set; }

	public required DateTime UpdatedAt { get; set; }

	public required bool Archived { get; set; }
}
