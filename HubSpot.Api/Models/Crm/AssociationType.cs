using HubSpot.Api.Converters;
using System.Text.Json.Serialization;

namespace HubSpot.Api.Models.Crm;

[JsonConverter(typeof(AssociationTypeConverter))]
public enum AssociationType
{
	#region Contact To XXX

	[JsonPropertyName("billing_contact_to_company")]
	BillingContactToCompany,

	[JsonPropertyName("contact_to_company")]
	ContactToCompany,

	[JsonPropertyName("contact_to_company_unlabeled")]
	ContactToCompanyUnlabelled,

	[JsonPropertyName("contact_to_deal")]
	ContactToDeal,

	#endregion

	#region Company To XXX

	[JsonPropertyName("company_to_billing_contact")]
	CompanyToBillingContact,

	[JsonPropertyName("company_to_contact")]
	CompanyToContact,

	[JsonPropertyName("company_to_contact_unlabeled")]
	CompanyToContactUnlabelled,

	[JsonPropertyName("company_to_deal")]
	CompanyToDeal,

	[JsonPropertyName("company_to_deal_unlabeled")]
	CompanyToDealUnlabelled,

	#endregion

	#region Deal To XXX

	[JsonPropertyName("deal_to_company")]
	DealToCompany,

	[JsonPropertyName("deal_to_company_unlabeled")]
	DealToCompanyUnlabelled,

	[JsonPropertyName("deal_to_contact")]
	DealToContact

	#endregion
}