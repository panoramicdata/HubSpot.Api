namespace HubSpot.Api.Models.Crm;

public class CreateAssociationInput
{
	public required AssociateFrom From { get; set; }

	public required AssociateTo To { get; set; }

	public required string Type { get; set; }
}
