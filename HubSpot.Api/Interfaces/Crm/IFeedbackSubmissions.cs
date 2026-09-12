using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IFeedbackSubmissions
{
	[Get("/crm/v3/objects/feedback_submissions")]
	Task<CrmPage<HubSpotFeedbackSubmission>> GetPageAsync(
		CrmPageRequest pageRequest,
		CancellationToken cancellationToken);
}
