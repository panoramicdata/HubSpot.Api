using System.Text.Json;
using HubSpot.Api.Converters;
using HubSpot.Api.Models.Crm;

namespace HubSpot.Api.Test;

public class AssociationTypeConverterTests
{
	private static string Serialise(AssociationType value) => JsonSerializer.Serialize(value);

	private static AssociationType Deserialize(string json) =>
		JsonSerializer.Deserialize<AssociationType>(json);

	[Theory]
	[InlineData("billing_contact_to_company", AssociationType.BillingContactToCompany)]
	[InlineData("contact_to_company", AssociationType.ContactToCompany)]
	[InlineData("contact_to_company_unlabeled", AssociationType.ContactToCompanyUnlabelled)]
	[InlineData("contact_to_deal", AssociationType.ContactToDeal)]
	[InlineData("company_to_billing_contact", AssociationType.CompanyToBillingContact)]
	[InlineData("company_to_contact", AssociationType.CompanyToContact)]
	[InlineData("company_to_contact_unlabeled", AssociationType.CompanyToContactUnlabelled)]
	[InlineData("company_to_deal", AssociationType.CompanyToDeal)]
	[InlineData("company_to_deal_unlabeled", AssociationType.CompanyToDealUnlabelled)]
	[InlineData("deal_to_company", AssociationType.DealToCompany)]
	[InlineData("deal_to_company_unlabeled", AssociationType.DealToCompanyUnlabelled)]
	[InlineData("deal_to_contact", AssociationType.DealToContact)]
	public void Read_WireName_ReturnsMatchingEnum(string wireName, AssociationType expected)
	{
		Deserialize($"\"{wireName}\"").Should().Be(expected);
	}

	[Theory]
	[InlineData(AssociationType.BillingContactToCompany, "billing_contact_to_company")]
	[InlineData(AssociationType.ContactToCompany, "contact_to_company")]
	[InlineData(AssociationType.ContactToCompanyUnlabelled, "contact_to_company_unlabeled")]
	[InlineData(AssociationType.ContactToDeal, "contact_to_deal")]
	[InlineData(AssociationType.CompanyToBillingContact, "company_to_billing_contact")]
	[InlineData(AssociationType.CompanyToContact, "company_to_contact")]
	[InlineData(AssociationType.CompanyToContactUnlabelled, "company_to_contact_unlabeled")]
	[InlineData(AssociationType.CompanyToDeal, "company_to_deal")]
	[InlineData(AssociationType.CompanyToDealUnlabelled, "company_to_deal_unlabeled")]
	[InlineData(AssociationType.DealToCompany, "deal_to_company")]
	[InlineData(AssociationType.DealToCompanyUnlabelled, "deal_to_company_unlabeled")]
	[InlineData(AssociationType.DealToContact, "deal_to_contact")]
	public void Write_EnumValue_ProducesWireName(AssociationType value, string wireName)
	{
		Serialise(value).Should().Be($"\"{wireName}\"");
	}

	[Fact]
	public void Read_UnknownValue_ThrowsJsonException()
	{
		var exception = Record.Exception(() => Deserialize("\"no_such_type\""));

		exception.Should().BeOfType<JsonException>();
	}
}
