using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface ILineItems
{
	[Get("/crm/v3/objects/line_items")]
	Task<CrmPage<HubSpotLineItem>> GetPageAsync(
		int? limit = null,
		string? after = null,
		ICollection<string>? properties = null,
		ICollection<string>? propertiesWithHistory = null,
		ICollection<string>? associations = null,
		bool? archived = null,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/line_items/properties")]
	Task<List<string>> GetProperties(
		CancellationToken cancellationToken = default);
}