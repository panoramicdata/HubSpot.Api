using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IOwners
{
	[Get("/crm/v3/owners")]
	Task<CrmPage<HubSpotOwner>> GetPageAsync(
		CrmPageRequest pageRequest,
		CancellationToken cancellationToken);
}
