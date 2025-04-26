using HubSpot.Api.Models.Crm.Base;

namespace HubSpot.Api.Models.Crm;

public class HubSpotObjectWithProperties : HubSpotCrmBaseObject
{
	public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
}