namespace HubSpot.Api.Models.Cms;

public class DomainObject
{
	public required string Id { get; set; }
	public required DateTime CreatedAt { get; set; }
	public required DateTime UpdatedAt { get; set; }
	public required string Domain { get; set; }
	public required bool IsPrimaryLandingPage { get; set; }
	public required bool IsPrimaryEmail { get; set; }
	public required bool IsPrimaryBlogPost { get; set; }
	public required bool IsPrimarySitePage { get; set; }
	public required bool IsPrimaryKnowledge { get; set; }
	public required bool IsResolving { get; set; }
	public required bool IsManuallyMarkedAsResolving { get; set; }
	public required bool IsHttpsEnabled { get; set; }
	public required bool IsHttpsOnly { get; set; }
	public required bool IsUsedForBlogPost { get; set; }
	public required bool IsUsedForSitePage { get; set; }
	public required bool IsUsedForLandingPage { get; set; }
	public required bool IsUsedForEmail { get; set; }
	public required bool IsUsedForKnowledge { get; set; }
	public required string ExpectedCname { get; set; }
	public required string RedirectTo { get; set; }
}
