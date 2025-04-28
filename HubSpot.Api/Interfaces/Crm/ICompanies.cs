using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface ICompanies
{

	[Post("/crm/v3/associations/companies/contacts/batch/create")]
	Task<object> AssociateWithContact(
		[Body] CreateAssociationRequest associationRequest,
		CancellationToken cancellationToken = default);

	[Post("/crm/v3/associations/companies/deals/batch/create")]
	Task<object> AssociateWithDeal(
		[Body] CreateAssociationRequest associationRequest,
		CancellationToken cancellationToken = default);

	[Post("/crm/v3/objects/companies")]
	Task<HubSpotCompany> CreateAsync(
		[Body] CreateRequest createRequest,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/companies")]
	Task<CrmPage<HubSpotCompany>> GetPageAsync(
		int? limit = null,
		string? after = null,
		ICollection<string>? properties = null,
		ICollection<string>? propertiesWithHistory = null,
		ICollection<string>? associations = null,
		bool? archived = null,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/companies/{id}")]
	Task<HubSpotCompany> GetAsync(
		string id,
		[Query] IReadOnlyList<string>? properties = null,
		[Query] IReadOnlyList<string>? propertiesWithHistory = null,
		[Query] IReadOnlyList<string>? associations = null,
		[Query] bool? archived = null,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/companies/properties")]
	Task<List<string>> GetProperties(
		CancellationToken cancellationToken = default);

	[Delete("/crm/v3/objects/companies/{id}")]
	Task ArchiveAsync(
		string id,
		CancellationToken cancellationToken = default
	);

	[Post("/crm/v3/objects/companies/gdpr-delete")]
	Task DeleteAsync(
		[Body] DeleteRequest deleteRequest,
		CancellationToken cancellationToken = default
	);

	[Post("/crm/v3/objects/companies/search")]
	Task<CrmPage<HubSpotCompany>> SearchAsync(
		[Body] SearchRequest searchRequest,
		CancellationToken cancellationToken = default
	);
}
