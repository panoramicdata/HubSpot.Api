namespace HubSpot.Api.Models.Crm;

public class DeleteRequest
{
	public required string ObjectId { get; set; }

	public string? IdProperty { get; set; }
}
