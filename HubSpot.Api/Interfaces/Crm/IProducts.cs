using HubSpot.Api.Models;
using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IProducts
{
	[Post("/crm/v3/objects/products")]
	Task<HubSpotProduct> CreateAsync(
		[Body] CreateRequest createRequest,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/products")]
	Task<CrmPage<HubSpotProduct>> GetPageAsync(
		int? limit = null,
		string? after = null,
		ICollection<string>? properties = null,
		ICollection<string>? propertiesWithHistory = null,
		ICollection<string>? associations = null,
		bool? archived = null,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/products/{id}")]
	Task<HubSpotProduct> GetAsync(
		string id,
		[Query] IReadOnlyList<string>? properties = null,
		[Query] IReadOnlyList<string>? propertiesWithHistory = null,
		[Query] IReadOnlyList<string>? associations = null,
		[Query] bool? archived = null,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/products/properties")]
	Task<List<string>> GetProperties(
		CancellationToken cancellationToken = default);

	[Delete("/crm/v3/objects/products/{id}")]
	Task ArchiveAsync(
		string id,
		CancellationToken cancellationToken = default
	);

	[Post("/crm/v3/objects/products/gdpr-delete")]
	Task DeleteAsync(
		[Body] DeleteRequest deleteRequest,
		CancellationToken cancellationToken = default
	);

	[Post("/crm/v3/objects/products/search")]
	Task<CrmPage<HubSpotProduct>> SearchAsync(
		[Body] SearchRequest searchRequest,
		CancellationToken cancellationToken = default
	);
}
