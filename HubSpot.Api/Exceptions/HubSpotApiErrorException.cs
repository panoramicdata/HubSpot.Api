using HubSpot.Api.Models;
using System.Net;

namespace HubSpot.Api.Exceptions;
public class HubSpotApiErrorException(HttpStatusCode statusCode, HubSpotError error) : HubSpotApiException(error.Message)
{
	public HttpStatusCode StatusCode { get; } = statusCode;

	public HubSpotError Error { get; } = error;
}
