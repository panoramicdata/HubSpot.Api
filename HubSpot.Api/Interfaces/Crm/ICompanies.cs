using HubSpot.Api.Models;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface ICompanies
{
	[Post("/crm/v3/objects/companies")]
	Task<HubSpotObject> CreateAsync(
		[Body] CreateRequest createRequest,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/companies")]
	Task<CrmPage> GetPageAsync(
		int? limit = null,
		string? after = null,
		ICollection<string>? properties = null,
		ICollection<string>? propertiesWithHistory = null,
		ICollection<string>? associations = null,
		bool? archived = null,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/companies/{id}")]
	Task<HubSpotObject> GetAsync(
		string id,
		[Query] IReadOnlyList<string>? properties = null,
		[Query] IReadOnlyList<string>? propertiesWithHistory = null,
		[Query] IReadOnlyList<string>? associations = null,
		[Query] bool? archived = null,
		CancellationToken cancellationToken = default
	);

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
	Task<CrmPage> SearchAsync(
		[Body] SearchRequest searchRequest,
		CancellationToken cancellationToken = default
	);
}
