using HubSpot.Api.Models.Cms;
using Refit;

namespace HubSpot.Api.Interfaces.Cms;
public interface IDomains
{
	[Get("/cms/v3/domains")]
	Task<CmsPage> GetPageAsync(
		[Query] DateTime? createdAt = null,
		[Query] DateTime? createdAfter = null,
		[Query] DateTime? createdBefore = null,
		[Query] DateTime? updatedAt = null,
		[Query] DateTime? updatedAfter = null,
		[Query] DateTime? updatedBefore = null,
		[Query] List<string>? sort = null,
		[Query] string? after = null,
		[Query] int? limit = null,
		[Query] bool? archived = null,
		CancellationToken cancellationToken = default
		);
}
