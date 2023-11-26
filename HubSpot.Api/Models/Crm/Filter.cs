namespace HubSpot.Api.Models;

public class Filter
{
	public string? HighValue { get; set; }

	public required string PropertyName { get; set; }

	public ICollection<string>? Values { get; set; }

	public string? Value { get; set; }

	public required FilterOperator Operator { get; set; }
}