namespace HubSpot.Api.Models;

public class CrmPage
{
	public required ICollection<HubSpotObject> Results { get; set; }

	public Paging? Paging { get; set; }
}
