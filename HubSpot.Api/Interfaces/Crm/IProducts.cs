using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IProducts : ICrmObject<HubSpotProduct>
{
	[Post("/crm/v3/objects/products")]
	Task<HubSpotProduct> CreateAsync(
		[Body] CreateRequest createRequest,
		CancellationToken cancellationToken);

	[Get("/crm/v3/objects/products")]
	Task<CrmPage<HubSpotProduct>> GetPageAsync(
		CrmPageRequest pageRequest,
		CancellationToken cancellationToken);

	[Get("/crm/v3/objects/products/{id}")]
	Task<HubSpotProduct> GetAsync(
		string id,
		CrmGetRequest getRequest,
		CancellationToken cancellationToken);

	[Delete("/crm/v3/objects/products/{id}")]
	Task ArchiveAsync(
		string id,
		CancellationToken cancellationToken);
}
