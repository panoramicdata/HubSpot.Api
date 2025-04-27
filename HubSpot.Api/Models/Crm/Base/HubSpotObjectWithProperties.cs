namespace HubSpot.Api.Models.Crm.Base;

public abstract class HubSpotObjectWithProperties : HubSpotObject
{
	public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
}