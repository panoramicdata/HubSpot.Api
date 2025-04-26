using HubSpot.Api.Models.Crm;

namespace HubSpot.Api.Models;

public class AssociationSpec
{
	public required AssociationCategory AssociationCategory { get; set; }

	public required int AssociationTypeId { get; set; }
}