using HubSpot.Api.Interfaces.Crm;
using Refit;

namespace HubSpot.Api.Sections;

public class Crm(HttpClient httpClient, RefitSettings refitSettings)
{
	public ICompanies Companies { get; init; }
		= RestService.For<ICompanies>(httpClient, refitSettings);

	public IContacts Contacts { get; init; }
		= RestService.For<IContacts>(httpClient, refitSettings);

	public IDeals Deals { get; init; }
		= RestService.For<IDeals>(httpClient, refitSettings);

	public IFeedbackSubmissions FeedbackSubmissions { get; init; }
		= RestService.For<IFeedbackSubmissions>(httpClient, refitSettings);

	public ILineItems LineItems { get; init; }
		= RestService.For<ILineItems>(httpClient, refitSettings);

	public IOwners Owners { get; init; }
		= RestService.For<IOwners>(httpClient, refitSettings);

	public IProducts Products { get; init; }
		= RestService.For<IProducts>(httpClient, refitSettings);

	public IQuotes Quotes { get; init; }
		= RestService.For<IQuotes>(httpClient, refitSettings);

	public ITickets Tickets { get; init; }
		= RestService.For<ITickets>(httpClient, refitSettings);
    
    public ITasks Tasks { get; init; }
        = RestService.For<ITasks>(httpClient, refitSettings);
}
