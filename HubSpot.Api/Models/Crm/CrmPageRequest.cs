using Refit;

namespace HubSpot.Api.Models.Crm;

/// <summary>
/// The query-string options for a paged CRM listing: <see cref="CrmGetRequest"/> plus paging.
/// </summary>
public class CrmPageRequest : CrmGetRequest
{
	/// <summary>
	/// The maximum number of results to return per page.
	/// </summary>
	[AliasAs("limit")]
	public int? Limit { get; set; }

	/// <summary>
	/// The paging cursor token from the previous page's <see cref="Paging"/>.
	/// </summary>
	[AliasAs("after")]
	public string? After { get; set; }
}
