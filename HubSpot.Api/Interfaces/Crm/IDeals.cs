using HubSpot.Api.Models;
using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IDeals
{
	[Post("/crm/v3/associations/deals/companies/batch/create")]
	Task<HubSpotDeal> AssociateWithCompany(
		[Body] CreateAssociationRequest associateRequest,
		CancellationToken cancellationToken = default
	);

	[Post("/crm/v3/objects/deals")]
	Task<HubSpotDeal> CreateAsync(
		[Body] CreateRequest createRequest,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/deals/{id}")]
	Task<HubSpotDeal> GetAsync(
		string id,
		[Query] IReadOnlyList<string>? properties = null,
		[Query] IReadOnlyList<string>? propertiesWithHistory = null,
		[Query] IReadOnlyList<string>? associations = null,
		[Query] bool? archived = null,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/deals/properties")]
	Task<List<string>> GetProperties(
		CancellationToken cancellationToken = default);

	[Patch("/crm/v3/objects/deals/{id}")]
	Task<HubSpotDeal> PatchAsync(
		string id,
		[Body] HubSpotPatchObject hubSpotObject,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/deals")]
	Task<CrmPage<HubSpotDeal>> GetPageAsync(
		int? limit = null,
		string? after = null,
		ICollection<string>? properties = null,
		ICollection<string>? propertiesWithHistory = null,
		ICollection<string>? associations = null,
		bool? archived = null,
		CancellationToken cancellationToken = default
	);

	[Delete("/crm/v3/objects/deals/{id}")]
	Task ArchiveAsync(
		string id,
		CancellationToken cancellationToken = default
	);

	[Post("/crm/v3/objects/deals/gdpr-delete")]
	Task DeleteAsync(
		[Body] DeleteRequest deleteRequest,
		CancellationToken cancellationToken = default
	);

	[Post("/crm/v3/objects/deals/search")]
	Task<CrmPage<HubSpotDeal>> SearchAsync(
		[Body] SearchRequest searchRequest,
		CancellationToken cancellationToken = default
	);
}

