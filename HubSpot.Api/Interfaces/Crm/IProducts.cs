using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IProducts
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

	[Get("/crm/v3/objects/products/properties")]
	Task<List<string>> GetProperties(
		CancellationToken cancellationToken);

	[Delete("/crm/v3/objects/products/{id}")]
	Task ArchiveAsync(
		string id,
		CancellationToken cancellationToken);

	[Post("/crm/v3/objects/products/gdpr-delete")]
	Task DeleteAsync(
		[Body] DeleteRequest deleteRequest,
		CancellationToken cancellationToken);

	[Post("/crm/v3/objects/products/search")]
	Task<CrmPage<HubSpotProduct>> SearchAsync(
		[Body] SearchRequest searchRequest,
		CancellationToken cancellationToken);
}
