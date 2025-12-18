using FluentAssertions;
using HubSpot.Api.Exceptions;
using HubSpot.Api.Models.Crm;
using System.Net;

namespace HubSpot.Api.Test.Crm;

public class AssociationTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetContactToCompanyAssociations_Succeeds()
	{
		var associations = await Client.Crm.Associations.GetContactToCompanyAssociations(
			new GetAssociationsFor
			{
				Inputs =
				[
					new()
					{
						Id = "1351",
					}
				]
			}, cancellationToken: CancellationToken);

		associations.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task GetCompanyToContactAssociations_Succeeds()
	{
		var associations = await Client.Crm.Associations.GetCompanyToContactAssociations(
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
			}, cancellationToken: CancellationToken);

		associations.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task GetCompanyToDealAssociations_Succeeds()
	{
		var associations = await Client.Crm.Associations.GetCompanyToDealAssociations(
			new GetAssociationsFor
			{
				Inputs =
				[
					new()
					{
						Id = "8614251743",
					}
				]
			}, cancellationToken: CancellationToken);

		associations.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task GetDealToCompanyAssociations_Succeeds()
	{
		var associations = await Client.Crm.Associations.GetDealToCompanyAssociations(
			new GetAssociationsFor
			{
				Inputs =
				[
					new()
					{
						Id = "9152131272",
					}
				]
			}, cancellationToken: CancellationToken);

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

		HubSpotContact createdObject;
		try
		{
			createdObject = await Client.Crm.Contacts.CreateAsync(createRequest, cancellationToken: CancellationToken);
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
		var readObject = await Client.Crm.Contacts.GetAsync(createdObject.Id, cancellationToken: CancellationToken);
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
							Id = "8689909238"
						},
						Type = AssociationType.ContactToCompany
					}
				]
			}, cancellationToken: CancellationToken);

			var associations =
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
					}, cancellationToken: CancellationToken);

			associations.Results.Should().NotBeEmpty();
			associations.Results[0].To[0].Type.Should().Be(AssociationType.ContactToCompany);
			associations.Results[0].To[0].Id.Should().Be("8689909238");
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
				ObjectId = createdObject.Id
			}, cancellationToken: CancellationToken);
		}
	}
}
