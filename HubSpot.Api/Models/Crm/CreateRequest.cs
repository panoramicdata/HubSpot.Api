using HubSpot.Api.Models.Crm;

namespace HubSpot.Api.Models;

public class CreateRequest
{
	public required IDictionary<string, string> Properties { get; set; }

	public required List<AssociationsFor> Associations { get; set; }
}
