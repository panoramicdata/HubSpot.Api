using HubSpot.Api.Models;
using Refit;

namespace HubSpot.Api.Interfaces;

public interface ITickets
{
	[Post("/tickets")]
	Task<Page> SearchAsync(
		[Body] SearchRequest searchRequest,
		CancellationToken cancellationToken = default
	);
}
