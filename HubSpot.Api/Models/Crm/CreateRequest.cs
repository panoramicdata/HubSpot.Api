namespace HubSpot.Api.Models.Crm;

public class CreateRequest
{
	public required IDictionary<string, string> Properties { get; set; }

	public required List<AssociationsFor> Associations { get; set; }
}
