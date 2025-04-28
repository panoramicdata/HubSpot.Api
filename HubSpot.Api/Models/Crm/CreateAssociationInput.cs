using HubSpot.Api.Converters;
using System.Text.Json.Serialization;

namespace HubSpot.Api.Models.Crm;

public class CreateAssociationInput
{
	public required ObjectId From { get; set; }

	public required ObjectId To { get; set; }

	[JsonConverter(typeof(AssociationTypeConverter))]
	public required AssociationType Type { get; set; }
}
