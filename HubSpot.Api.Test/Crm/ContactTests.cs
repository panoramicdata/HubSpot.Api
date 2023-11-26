using FluentAssertions;
using HubSpot.Api.Exceptions;
using HubSpot.Api.Models;
using System.Net;
using Xunit.Abstractions;

namespace HubSpot.Api.Test.Crm;

public class ContactTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async void GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.Contacts.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async void CreateReadUpdateAndDelete_Succeeds()
	{
		var createRequest = new CreateRequest
		{
			Properties = new Dictionary<string, object?>
			{
				{ "email", "test@test.com"},
				{ "phone", "+44 1234 567 890"},
				{ "company", "Test Inc."},
				{ "website", "https://test.com/"},
				{ "firstname", "Bob"},
				{ "lastname", "Sherunkle"},
			},
			Associations = []
		};

		HubSpotObject createdObject;
		try
		{
			createdObject = await Client.Crm.Contacts.CreateAsync(createRequest);
			createdObject.Should().NotBeNull();
		}
		catch (HubSpotApiErrorException e) when (e.StatusCode == HttpStatusCode.Conflict)
		{
			e.Error.Category.Should().Be(ErrorCategory.Conflict);
			createdObject = new HubSpotObject
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

		// Delete the item
		await Client.Crm.Contacts.DeleteAsync(new DeleteRequest
		{
			ObjectId = createdObject.Id
		}
		);
	}
}
