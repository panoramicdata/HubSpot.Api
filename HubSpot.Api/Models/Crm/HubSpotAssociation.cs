namespace HubSpot.Api.Models.Crm;

public class HubSpotAssociation
{
	public required ObjectId From { get; set; }

	public List<AssociationTo> To { get; set; } = [];
}