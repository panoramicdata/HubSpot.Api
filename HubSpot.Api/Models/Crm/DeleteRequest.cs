namespace HubSpot.Api.Models;

public class DeleteRequest
{
	public required string ObjectId { get; set; }

	public string? IdProperty { get; set; }
}
