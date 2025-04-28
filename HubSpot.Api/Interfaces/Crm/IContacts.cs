using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IContacts
{
	// This could be made generic so that a Contact could be associated with other items
	[Post("/crm/v3/associations/contacts/company/batch/create")]
	Task<object> AssociateWithCompany(
		[Body] CreateAssociationRequest associationRequest,
		CancellationToken cancellationToken = default
	);

	[Post("/crm/v3/objects/contacts")]
	Task<HubSpotContact> CreateAsync(
		[Body] CreateRequest createRequest,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/contacts/{id}")]
	Task<HubSpotContact> GetAsync(
		string id,
		[Query] IReadOnlyList<string>? properties = null,
		[Query] IReadOnlyList<string>? propertiesWithHistory = null,
		[Query] IReadOnlyList<string>? associations = null,
		[Query] bool? archived = null,
		CancellationToken cancellationToken = default
	);

	[Patch("/crm/v3/objects/contacts/{id}")]
	Task<HubSpotContact> PatchAsync(
		string id,
		[Body] HubSpotPatchObject hubSpotObject,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/contacts")]
	Task<CrmPage<HubSpotContact>> GetPageAsync(
		int? limit = null,
		string? after = null,
		ICollection<string>? properties = null,
		ICollection<string>? propertiesWithHistory = null,
		ICollection<string>? associations = null,
		bool? archived = null,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/contacts/properties")]
	Task<List<string>> GetProperties(
		CancellationToken cancellationToken = default);

	[Post("/crm/v3/objects/contacts/gdpr-delete")]
	Task DeleteAsync(
		[Body] DeleteRequest deleteRequest,
		CancellationToken cancellationToken = default
	);

	[Post("/crm/v3/objects/contacts/search")]
	Task<CrmPage<HubSpotContact>> SearchAsync(
		[Body] SearchRequest searchRequest,
		CancellationToken cancellationToken = default
	);
}
