using HubSpot.Api.Models;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IContacts
{
	[Post("/crm/v3/objects/contacts")]
	Task<HubSpotObject> CreateAsync(
		[Body] CreateRequest createRequest,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/contacts/{id}")]
	Task<HubSpotObject> GetAsync(
		string id,
		[Query] IReadOnlyList<string>? properties = null,
		[Query] IReadOnlyList<string>? propertiesWithHistory = null,
		[Query] IReadOnlyList<string>? associations = null,
		[Query] bool? archived = null,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/contacts")]
	Task<CrmPage> GetPageAsync(
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
}
