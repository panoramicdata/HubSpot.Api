namespace HubSpot.Api.Exceptions;
public class HubSpotApiDeserializationException(string message)
	: HubSpotApiException($"Error Deserializing API response: {message}")
{
}