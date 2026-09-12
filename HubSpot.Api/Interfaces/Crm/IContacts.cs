using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IContacts
{
	// This could be made generic so that a Contact could be associated with other items
	[Post("/crm/v3/associations/contacts/companies/batch/create")]
	Task<object> AssociateWithCompany(
		[Body] CreateAssociationRequest associationRequest,
		CancellationToken cancellationToken);

	[Post("/crm/v3/associations/contacts/deals/batch/create")]
	Task<object> AssociateWithDeal(
		[Body] CreateAssociationRequest associationRequest,
		CancellationToken cancellationToken);

	[Post("/crm/v3/objects/contacts")]
	Task<HubSpotContact> CreateAsync(
		[Body] CreateRequest createRequest,
		CancellationToken cancellationToken);

	[Get("/crm/v3/objects/contacts/{id}")]
	Task<HubSpotContact> GetAsync(
		string id,
		CrmGetRequest getRequest,
		CancellationToken cancellationToken);

	[Patch("/crm/v3/objects/contacts/{id}")]
	Task<HubSpotContact> PatchAsync(
		string id,
		[Body] HubSpotPatchObject hubSpotObject,
		CancellationToken cancellationToken);

	[Get("/crm/v3/objects/contacts")]
	Task<CrmPage<HubSpotContact>> GetPageAsync(
		CrmPageRequest pageRequest,
		CancellationToken cancellationToken);

	[Get("/crm/v3/objects/contacts/properties")]
	Task<List<string>> GetProperties(
		CancellationToken cancellationToken);

	[Post("/crm/v3/objects/contacts/gdpr-delete")]
	Task DeleteAsync(
		[Body] DeleteRequest deleteRequest,
		CancellationToken cancellationToken);

	[Post("/crm/v3/objects/contacts/search")]
	Task<CrmPage<HubSpotContact>> SearchAsync(
		[Body] SearchRequest searchRequest,
		CancellationToken cancellationToken);
}
