using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IDeals
{
	[Post("/crm/v3/associations/deals/companies/batch/create")]
	Task<object> AssociateWithCompany(
		[Body] CreateAssociationRequest associateRequest,
		CancellationToken cancellationToken);

	[Post("/crm/v3/associations/deals/contacts/batch/create")]
	Task<object> AssociateWithContact(
		[Body] CreateAssociationRequest associateRequest,
		CancellationToken cancellationToken);

	[Post("/crm/v3/objects/deals")]
	Task<HubSpotDeal> CreateAsync(
		[Body] CreateRequest createRequest,
		CancellationToken cancellationToken);

	[Get("/crm/v3/objects/deals/{id}")]
	Task<HubSpotDeal> GetAsync(
		string id,
		CrmGetRequest getRequest,
		CancellationToken cancellationToken);

	[Get("/crm/v3/objects/deals/properties")]
	Task<List<string>> GetProperties(
		CancellationToken cancellationToken);

	[Patch("/crm/v3/objects/deals/{id}")]
	Task<HubSpotDeal> PatchAsync(
		string id,
		[Body] HubSpotPatchObject hubSpotObject,
		CancellationToken cancellationToken);

	[Get("/crm/v3/objects/deals")]
	Task<CrmPage<HubSpotDeal>> GetPageAsync(
		CrmPageRequest pageRequest,
		CancellationToken cancellationToken);

	[Delete("/crm/v3/objects/deals/{id}")]
	Task ArchiveAsync(
		string id,
		CancellationToken cancellationToken);

	[Post("/crm/v3/objects/deals/gdpr-delete")]
	Task DeleteAsync(
		[Body] DeleteRequest deleteRequest,
		CancellationToken cancellationToken);

	[Post("/crm/v3/objects/deals/search")]
	Task<CrmPage<HubSpotDeal>> SearchAsync(
		[Body] SearchRequest searchRequest,
		CancellationToken cancellationToken);
}
