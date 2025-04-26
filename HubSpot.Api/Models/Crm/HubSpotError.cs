namespace HubSpot.Api.Models.Crm;

public class HubSpotError
{
	public required string Status { get; set; }

	public required string Message { get; set; }

	public required string CorrelationId { get; set; }

	public required ErrorCategory Category { get; set; }
}

