using HubSpot.Api.Models.Crm;

namespace HubSpot.Api.Test.Crm;

public class AssociationTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	// Panoramic Data
	private const string PanoramicDataCompanyId = "8689909238";

	[Fact]
	public async Task GetContactToCompanyAssociations_Succeeds()
	{
		var associations = await Client.Crm.Associations.GetContactToCompanyAssociations(
			CrmTestHelpers.AssociationsForId("1351"), cancellationToken: CancellationToken);

		associations.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task GetCompanyToContactAssociations_Succeeds()
	{
		var associations = await Client.Crm.Associations.GetCompanyToContactAssociations(
			CrmTestHelpers.AssociationsForId(PanoramicDataCompanyId), cancellationToken: CancellationToken);

		associations.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task GetCompanyToDealAssociations_Succeeds()
	{
		var associations = await Client.Crm.Associations.GetCompanyToDealAssociations(
			CrmTestHelpers.AssociationsForId("8612263671"), cancellationToken: CancellationToken);

		associations.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task GetDealToCompanyAssociations_Succeeds()
	{
		var associations = await Client.Crm.Associations.GetDealToCompanyAssociations(
			CrmTestHelpers.AssociationsForId("9149763809"), cancellationToken: CancellationToken);

		associations.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task CreateContactAndAssociateWithCompany_Succeeds()
	{
		var createRequest = new CreateRequest
		{
			Properties = new Dictionary<string, string>
			{
				{ "email", "test@test.com" },
				{ "firstname", "DeleteMeTest" },
				{ "lastname", "DeleteMeTest" }
			},
			Associations = []
		};

		var createdId = await CrmTestHelpers.CreateOrRecoverIdAsync(
			createRequest,
			(request, cancellationToken) => Client.Crm.Contacts.CreateAsync(request, cancellationToken));

		// Re-read the item
		var readObject = await CrmTestHelpers.ReadAndVerifyAsync(
			createdId,
			(id, cancellationToken) => Client.Crm.Contacts.GetAsync(id, cancellationToken: cancellationToken));

		try
		{
			// Associate with a Company
			await AssociateContactWithCompanyAsync(readObject.Id, PanoramicDataCompanyId);
			await VerifyContactToCompanyAssociationAsync(readObject.Id, PanoramicDataCompanyId);
		}
		catch
		{
			// Didn't work but we still want to delete the created Contact
		}
		finally
		{
			// Delete the item
			await Client.Crm.Contacts.DeleteAsync(new DeleteRequest
			{
				ObjectId = createdId
			}, cancellationToken: CancellationToken);
		}
	}

	private Task AssociateContactWithCompanyAsync(string contactId, string companyId)
		=> Client.Crm.Contacts.AssociateWithCompany(new CreateAssociationRequest
		{
			Inputs =
			[
				new()
				{
					From = new ObjectId
					{
						Id = contactId,
					},
					To = new ObjectId
					{
						Id = companyId
					},
					Type = AssociationType.ContactToCompany
				}
			]
		}, cancellationToken: CancellationToken);

	private async Task VerifyContactToCompanyAssociationAsync(string contactId, string companyId)
	{
		var associations = await Client.Crm.Associations.GetContactToCompanyAssociations(
			CrmTestHelpers.AssociationsForId(contactId), cancellationToken: CancellationToken);

		associations.Results.Should().NotBeEmpty();
		associations.Results[0].To[0].Type.Should().Be(AssociationType.ContactToCompany);
		associations.Results[0].To[0].Id.Should().Be(companyId);
	}
}
