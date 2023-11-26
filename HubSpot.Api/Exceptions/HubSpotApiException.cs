namespace HubSpot.Api.Exceptions;

public abstract class HubSpotApiException(string message) : Exception(message)
{
}