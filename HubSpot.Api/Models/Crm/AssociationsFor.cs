namespace HubSpot.Api.Models.Crm;

public class AssociationsFor
{
	public required List<AssociationSpec> Types { get; set; }

	public required ObjectId To { get; set; }
}