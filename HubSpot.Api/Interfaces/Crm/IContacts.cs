using HubSpot.Api.Models;
using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IContacts
{
	[Post("/crm/v3/associations/contacts/companies/batch/create")]
	Task<HubSpotObjectWithProperties> AssociateWithCompany(
		[Body] CreateAssociationRequest associateRequest,
		CancellationToken cancellationToken = default
	);

	[Post("/crm/v3/objects/contacts")]
	Task<HubSpotObjectWithProperties> CreateAsync(
		[Body] CreateRequest createRequest,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/contacts/{id}")]
	Task<HubSpotObjectWithProperties> GetAsync(
		string id,
		[Query] IReadOnlyList<string>? properties = null,
		[Query] IReadOnlyList<string>? propertiesWithHistory = null,
		[Query] IReadOnlyList<string>? associations = null,
		[Query] bool? archived = null,
		CancellationToken cancellationToken = default
	);

	[Patch("/crm/v3/objects/contacts/{id}")]
	Task<HubSpotObjectWithProperties> PatchAsync(
		string id,
		[Body] HubSpotPatchObject hubSpotObject,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/contacts")]
	Task<CrmPage<HubSpotObjectWithProperties>> GetPageAsync(
		int? limit = null,
		string? after = null,
		ICollection<string>? properties = null,
		ICollection<string>? propertiesWithHistory = null,
		ICollection<string>? associations = null,
		bool? archived = null,
		CancellationToken cancellationToken = default
	);

	[Post("/crm/v3/objects/contacts/gdpr-delete")]
	Task DeleteAsync(
		[Body] DeleteRequest deleteRequest,
		CancellationToken cancellationToken = default
	);

	[Post("/crm/v3/objects/contacts/search")]
	Task<CrmPage<HubSpotObjectWithProperties>> SearchAsync(
		[Body] SearchRequest searchRequest,
		CancellationToken cancellationToken = default
	);
}
