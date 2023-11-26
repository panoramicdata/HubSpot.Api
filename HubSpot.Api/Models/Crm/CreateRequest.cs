namespace HubSpot.Api.Models;

public class CreateRequest
{
	public required IDictionary<string, object?> Properties { get; set; }

	public required List<AssociationsFor> Associations { get; set; }
}
