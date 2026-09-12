using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IQuotes
{
	[Get("/crm/v3/objects/quotes")]
	Task<CrmPage<HubSpotQuote>> GetPageAsync(
		CrmPageRequest pageRequest,
		CancellationToken cancellationToken);

	[Get("/crm/v3/objects/quotes/properties")]
	Task<List<string>> GetProperties(
		CancellationToken cancellationToken);
}
