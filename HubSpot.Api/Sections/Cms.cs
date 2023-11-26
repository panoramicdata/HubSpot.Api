using HubSpot.Api.Interfaces.Cms;
using Refit;

namespace HubSpot.Api.Sections;

public class Cms(HttpClient httpClient, RefitSettings refitSettings)
{
	public IDomains Domains { get; init; }
		= RestService.For<IDomains>(httpClient, refitSettings);
}
