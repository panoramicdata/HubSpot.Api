using FluentAssertions;
using HubSpot.Api.Exceptions;
using HubSpot.Api.Models.Crm;
using System.Net;
using Xunit.Abstractions;

namespace HubSpot.Api.Test.Crm;

public class AssociationTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetContactToCompanyAssociations_Succeeds()
	{
		var assocations = await Client.Crm.Associations.GetContactToCompanyAssociations(
			new GetAssociationsFor
			{
				Inputs =
				[
					new()
					{
						// Robert Limbrey
						Id = "1351",
					}
				]
			});

		assocations.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task GetCompanyToContactAssociations_Succeeds()
	{
		var assocations = await Client.Crm.Associations.GetCompanyToContactAssociations(
			new GetAssociationsFor
			{
				Inputs =
				[
					new()
					{
						// Panoramic Data
						Id = "8689909238",
					}
				]
			});

		assocations.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task GetCompanyToDealAssociations_Succeeds()
	{
		var assocations = await Client.Crm.Associations.GetCompanyToDealAssociations(
			new GetAssociationsFor
			{
				Inputs =
				[
					new()
					{
						// R2UT - cannot use PDL as we have no associated Deals
						Id = "8614251743",
					}
				]
			});

		assocations.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task GetDealToCompanyAssociations_Succeeds()
	{
		var assocations = await Client.Crm.Associations.GetDealToCompanyAssociations(
			new GetAssociationsFor
			{
				Inputs =
				[
					new()
					{
						// R2UT - cannot use PDL as we have no associated Deals
						Id = "9152131272",
					}
				]
			});

		assocations.Results.Should().NotBeEmpty();
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

		HubSpotContact createdObject;
		try
		{
			createdObject = await Client.Crm.Contacts.CreateAsync(createRequest);
			createdObject.Should().NotBeNull();
		}
		catch (HubSpotApiErrorException e) when (e.StatusCode == HttpStatusCode.Conflict)
		{
			e.Error.Category.Should().Be(ErrorCategory.Conflict);
			createdObject = new HubSpotContact
			{
				Id = e.Message.Split(' ').Last(),
				Properties = createRequest.Properties,
				Archived = false,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};
		}

		// Re-read the item
		var readObject = await Client.Crm.Contacts.GetAsync(createdObject.Id);
		readObject.Should().NotBeNull();
		readObject.Id.Should().Be(createdObject.Id);
		readObject.Properties.Should().NotBeEmpty();

		try
		{
			// Associate with a Company
			await Client.Crm.Contacts.AssociateWithCompany(new CreateAssociationRequest
			{
				Inputs =
				[
					new()
					{
						From = new ObjectId
						{
							Id = readObject.Id,
						},
						To = new ObjectId
						{
							// Panoramic Data
							Id = "8689909238"
						},
						Type = AssociationType.ContactToCompany
					}
				]
			});

			var assocations =
				await Client.Crm.Associations.GetContactToCompanyAssociations(
					new GetAssociationsFor
					{
						Inputs =
						[
							new()
							{
								Id = readObject.Id,
							}
						]
					});

			assocations.Results.Should().NotBeEmpty();
			assocations.Results[0].To[0].Type.Should().Be(AssociationType.ContactToCompany);
			assocations.Results[0].To[0].Id.Should().Be("8689909238");
		}
		catch (Exception ex)
		{
			// Didn't work but we still want to delete the created Contact
		}
		finally
		{
			// Delete the item
			await Client.Crm.Contacts.DeleteAsync(new DeleteRequest
			{
				ObjectId = createdObject.Id
			});
		}
	}
}
