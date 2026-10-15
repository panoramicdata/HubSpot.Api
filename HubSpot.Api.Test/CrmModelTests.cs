using HubSpot.Api.Models.Crm;

namespace HubSpot.Api.Test;

public class CrmModelTests
{
	[Fact]
	public void HubSpotAssociation_Defaults_ExposeDefaults()
	{
		var association = new HubSpotAssociation
		{
			From = new ObjectId { Id = "42" }
		};

		association.From.Id.Should().Be("42");
		association.To.Should().BeEmpty();
	}

	[Fact]
	public void GetAssociationResponse_Defaults_ExposeDefaults()
	{
		var response = new GetAssociationResponse();

		response.Status.Should().BeEmpty();
		response.Results.Should().BeEmpty();
		response.CompletedAt.Should().BeNull();
		response.StartedAt.Should().BeNull();
	}

	[Fact]
	public void AssociationTo_StoresItsMembers()
	{
		var to = new AssociationTo
		{
			Id = "7",
			Type = AssociationType.ContactToDeal
		};

		to.Id.Should().Be("7");
		to.Type.Should().Be(AssociationType.ContactToDeal);
	}

	[Fact]
	public void ObjectId_StoresItsMembers()
	{
		new ObjectId { Id = "9" }.Id.Should().Be("9");
	}
}
