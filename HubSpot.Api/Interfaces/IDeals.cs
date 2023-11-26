using HubSpot.Api.Models;
using Refit;

namespace HubSpot.Api.Interfaces;
public interface IDeals
{
	[Get("/deals")]
	Task<Page> GetPageAsync(
		int? limit = null,
		string? after = null,
		ICollection<string>? properties = null,
		ICollection<string>? propertiesWithHistory = null,
		ICollection<string>? associations = null,
		bool? archived = null,
		CancellationToken cancellationToken = default
	);
}
