namespace HubSpot.Api.Models.Crm;

public class GetAssociationResponse
{
	public string Status { get; set; } = string.Empty;

	public List<HubSpotAssociation> Results { get; set; } = [];

	public DateTime? CompletedAt { get; set; }

	public DateTime? StartedAt { get; set; }
}
