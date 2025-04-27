namespace HubSpot.Api.Models.Crm.Base;

public abstract class HubSpotObject
{
	public required string Id { get; set; }

	public required DateTime CreatedAt { get; set; }

	public required DateTime UpdatedAt { get; set; }

	public required bool Archived { get; set; }
}
