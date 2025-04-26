namespace HubSpot.Api.Models.Crm;

public class SearchRequest
{
	public string? Query { get; set; }

	public required int Limit { get; set; }

	public required string After { get; set; }

	public required List<string> Properties { get; set; }

	public required List<string> Sorts { get; set; }

	public required List<FilterGroup> FilterGroups { get; set; }
}
