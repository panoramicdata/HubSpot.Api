using Refit;

namespace HubSpot.Api.Models.Crm;

/// <summary>
/// The query-string options that every CRM endpoint returning objects accepts.
/// </summary>
/// <remarks>
/// These were once repeated as four or six optional parameters on each interface method. Carrying
/// them as one object keeps the endpoints to a single shape, and lets a caller build the options
/// once and reuse them across object types.
/// </remarks>
public class CrmGetRequest
{
	/// <summary>
	/// The properties to return. Requesting a property that does not exist is ignored rather than
	/// being an error.
	/// </summary>
	[AliasAs("properties")]
	public ICollection<string>? Properties { get; set; }

	/// <summary>
	/// The properties to return with their history of previous values.
	/// </summary>
	[AliasAs("propertiesWithHistory")]
	public ICollection<string>? PropertiesWithHistory { get; set; }

	/// <summary>
	/// The object types to retrieve associated ids for.
	/// </summary>
	[AliasAs("associations")]
	public ICollection<string>? Associations { get; set; }

	/// <summary>
	/// Whether to return only archived results.
	/// </summary>
	[AliasAs("archived")]
	public bool? Archived { get; set; }
}
