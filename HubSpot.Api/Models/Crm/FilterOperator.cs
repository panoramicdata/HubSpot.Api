namespace HubSpot.Api.Models;

public enum FilterOperator
{
	Eq,
	Neq,
	Lt,
	Lte,
	Gt,
	Gte,
	Between,
	In,
	NotIn,
	HasProperty,
	NotHasProperty,
	ContainsToken,
	NotContainsToken,
}