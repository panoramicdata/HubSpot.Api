﻿using HubSpot.Api.Models;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface ITickets
{
	[Post("/crm/v3/objects/tickets")]
	Task<CrmPage> SearchAsync(
		[Body] SearchRequest searchRequest,
		CancellationToken cancellationToken = default
	);
}
