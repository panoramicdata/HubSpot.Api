using HubSpot.Api.Models;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IDeals
{
	[Get("/crm/v3/objects/deals")]
	Task<CrmPage> GetPageAsync(
		int? limit = null,
		string? after = null,
		ICollection<string>? properties = null,
		ICollection<string>? propertiesWithHistory = null,
		ICollection<string>? associations = null,
		bool? archived = null,
		CancellationToken cancellationToken = default
	);

	[Post("/crm/v3/objects/deals/search")]
	Task<CrmPage> SearchAsync(
		[Body] SearchRequest searchRequest,
		CancellationToken cancellationToken = default
	);
}

