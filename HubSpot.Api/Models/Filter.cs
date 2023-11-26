using System.Text.Json.Serialization;

namespace HubSpot.Api.Models;

public class Filter
{
	[JsonPropertyName("highValue")]
	public string? HighValue { get; set; }

	[JsonPropertyName("propertyName")]
	public required string PropertyName { get; set; }

	[JsonPropertyName("values")]
	public ICollection<string>? Values { get; set; }

	[JsonPropertyName("value")]
	public string? Value { get; set; }

	[JsonPropertyName("operator")]
	public required FilterOperator Operator { get; set; }
}