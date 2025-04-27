using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface ITickets
{
	[Post("/crm/v3/objects/tickets")]
	Task<CrmPage<HubSpotTicket>> SearchAsync(
		[Body] SearchRequest searchRequest,
		CancellationToken cancellationToken = default
	);
}
