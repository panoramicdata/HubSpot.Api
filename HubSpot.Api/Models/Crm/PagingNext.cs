namespace HubSpot.Api.Models;

public class PagingNext
{
	public required string After { get; set; }

	public string? Link { get; set; }
}