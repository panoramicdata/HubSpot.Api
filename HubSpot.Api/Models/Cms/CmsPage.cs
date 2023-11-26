namespace HubSpot.Api.Models.Cms;
public class CmsPage
{
	public required int Total { get; set; }
	public required List<DomainObject> Results { get; set; }
}
