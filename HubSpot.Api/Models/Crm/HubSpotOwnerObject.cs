using HubSpot.Api.Models.Crm.Base;

namespace HubSpot.Api.Models.Crm;

public class HubSpotOwnerObject : HubSpotCrmBaseObject
{
	public required string Email { get; set; }

	public required string FirstName { get; set; }

	public required string LastName { get; set; }

	public required string Type { get; set; }

	public required long UserId { get; set;	}

	public required long UserIdIncludingInactive { get; set; }
}
