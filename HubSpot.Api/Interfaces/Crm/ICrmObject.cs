using HubSpot.Api.Models.Crm;
using HubSpot.Api.Models.Crm.Base;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface ICrmObject<TObject> where TObject : HubSpotObject
{
	[Get("/properties")]
	Task<List<string>> GetProperties(
		CancellationToken cancellationToken);

	[Post("/gdpr-delete")]
	Task DeleteAsync(
		[Body] DeleteRequest deleteRequest,
		CancellationToken cancellationToken);

	[Post("/search")]
	Task<CrmPage<TObject>> SearchAsync(
		[Body] SearchRequest searchRequest,
		CancellationToken cancellationToken);
}
