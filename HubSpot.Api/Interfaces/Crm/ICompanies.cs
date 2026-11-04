using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface ICompanies
{
	[Post("/crm/v3/associations/companies/{type}/batch/create")]
	Task<object> AssociateWith([Body] CreateAssociationRequest associationRequest, string type, CancellationToken cancellationToken);

	[Post("/crm/v3/objects/companies")]
	Task<HubSpotCompany> CreateAsync([Body] CreateRequest createRequest, CancellationToken cancellationToken);

	[Get("/crm/v3/objects/companies")]
	Task<CrmPage<HubSpotCompany>> GetPageAsync(
		CrmPageRequest pageRequest,
		CancellationToken cancellationToken);

	[Get("/crm/v3/objects/companies/{id}")]
	Task<HubSpotCompany> GetAsync(
		string id,
		CrmGetRequest getRequest,
		CancellationToken cancellationToken);

	[Get("/crm/v3/objects/companies/properties")]
	Task<List<string>> GetProperties(
		CancellationToken cancellationToken);

	[Delete("/crm/v3/objects/companies/{id}")]
	Task ArchiveAsync(
		string id,
		CancellationToken cancellationToken);

	[Post("/crm/v3/objects/companies/gdpr-delete")]
	Task DeleteAsync([Body] DeleteRequest deleteRequest, CancellationToken cancellationToken);

	[Post("/crm/v3/objects/companies/search")]
	Task<CrmPage<HubSpotCompany>> SearchAsync([Body] SearchRequest searchRequest, CancellationToken cancellationToken);
}
