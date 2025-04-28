namespace HubSpot.Api.Models.Crm;

public class AssociationSpec
{
	public required AssociationCategory AssociationCategory { get; set; }

	public required int AssociationTypeId { get; set; }
}