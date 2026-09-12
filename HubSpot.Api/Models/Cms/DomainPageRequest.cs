using Refit;

namespace HubSpot.Api.Models.Cms;

/// <summary>
/// The query-string options for a paged domain listing.
/// </summary>
public class DomainPageRequest
{
	/// <summary>
	/// Only return domains created at this exact time.
	/// </summary>
	[AliasAs("createdAt")]
	public DateTime? CreatedAt { get; set; }

	/// <summary>
	/// Only return domains created after this time.
	/// </summary>
	[AliasAs("createdAfter")]
	public DateTime? CreatedAfter { get; set; }

	/// <summary>
	/// Only return domains created before this time.
	/// </summary>
	[AliasAs("createdBefore")]
	public DateTime? CreatedBefore { get; set; }

	/// <summary>
	/// Only return domains updated at this exact time.
	/// </summary>
	[AliasAs("updatedAt")]
	public DateTime? UpdatedAt { get; set; }

	/// <summary>
	/// Only return domains updated after this time.
	/// </summary>
	[AliasAs("updatedAfter")]
	public DateTime? UpdatedAfter { get; set; }

	/// <summary>
	/// Only return domains updated before this time.
	/// </summary>
	[AliasAs("updatedBefore")]
	public DateTime? UpdatedBefore { get; set; }

	/// <summary>
	/// The fields to sort by.
	/// </summary>
	[AliasAs("sort")]
	public ICollection<string>? Sort { get; set; }

	/// <summary>
	/// The paging cursor token from the previous page.
	/// </summary>
	[AliasAs("after")]
	public string? After { get; set; }

	/// <summary>
	/// The maximum number of results to return per page.
	/// </summary>
	[AliasAs("limit")]
	public int? Limit { get; set; }

	/// <summary>
	/// Whether to return only archived results.
	/// </summary>
	[AliasAs("archived")]
	public bool? Archived { get; set; }
}
