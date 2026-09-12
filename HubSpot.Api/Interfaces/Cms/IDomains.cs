using HubSpot.Api.Models.Cms;
using Refit;

namespace HubSpot.Api.Interfaces.Cms;

public interface IDomains
{
	[Get("/cms/v3/domains")]
	Task<CmsPage> GetPageAsync(
		DomainPageRequest pageRequest,
		CancellationToken cancellationToken);
}
