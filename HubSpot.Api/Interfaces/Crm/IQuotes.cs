﻿using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IQuotes
{
	[Get("/crm/v3/objects/quotes")]
	Task<CrmPage<HubSpotQuote>> GetPageAsync(
		int? limit = null,
		string? after = null,
		ICollection<string>? properties = null,
		ICollection<string>? propertiesWithHistory = null,
		ICollection<string>? associations = null,
		bool? archived = null,
		CancellationToken cancellationToken = default
	);

	[Get("/crm/v3/objects/quotes/properties")]
	Task<List<string>> GetProperties(
		CancellationToken cancellationToken = default);
}
