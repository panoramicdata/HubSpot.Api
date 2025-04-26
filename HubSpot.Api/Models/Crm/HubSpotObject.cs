namespace HubSpot.Api.Models.Crm;

public class HubSpotObject
{
	public required string Id { get; set; }

	/// <summary>
	/// The Properties. They are now optional, because certain endpoints (such as Owners) do NOT return Properties!!!
	/// </summary>
	public IDictionary<string, string>? Properties { get; set; } = new Dictionary<string, string>();

	public required DateTime CreatedAt { get; set; }

	public required DateTime UpdatedAt { get; set; }

	public required bool Archived { get; set; }
}
