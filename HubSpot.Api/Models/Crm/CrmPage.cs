using HubSpot.Api.Models.Crm.Base;

namespace HubSpot.Api.Models.Crm;

public class CrmPage<TObject> where TObject : HubSpotCrmBaseObject
{
	public int? Total { get; set; }

	public required ICollection<TObject> Results { get; set; }

	public Paging? Paging { get; set; }
}
