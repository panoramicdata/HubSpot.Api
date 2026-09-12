using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface ILineItems
{
	[Get("/crm/v3/objects/line_items")]
	Task<CrmPage<HubSpotLineItem>> GetPageAsync(
		CrmPageRequest pageRequest,
		CancellationToken cancellationToken);

	[Get("/crm/v3/objects/line_items/properties")]
	Task<List<string>> GetProperties(
		CancellationToken cancellationToken);
}
