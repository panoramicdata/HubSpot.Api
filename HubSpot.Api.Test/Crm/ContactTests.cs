using FluentAssertions;
using HubSpot.Api.Exceptions;
using HubSpot.Api.Models.Crm;
using System.Net;
using Xunit.Abstractions;

namespace HubSpot.Api.Test.Crm;

public class ContactTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetProperties_Succeeds()
	{
		var properties = await Client.Crm.Contacts.GetProperties();
		properties.Should().NotBeEmpty();
	}

	[Fact]
	public async void GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.Contacts.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async void SearchAsync_ByEmail_Succeeds()
	{
		var page = await Client.Crm.Contacts.SearchAsync(new SearchRequest
		{
			After = "",
			FilterGroups =
			[
				new() {
					Filters =
					[
						new Filter
						{
							PropertyName = "email",
							Operator = FilterOperator.Eq,
							Value = "david.bond@panoramicdata.com"
						}
					]
				}
			],
			Limit = 100,
			Properties =
			[
				"email",
				"firstname",
				"lastname",
				"phone",
				"company",
				"website"
			],
			Sorts =
			[
				"email"
			]
		});

		page.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async void CreateReadUpdateAndDelete_Succeeds()
	{
		var createRequest = new CreateRequest
		{
			Properties = new Dictionary<string, string>
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

		// Update the item
		var patchInfo = new HubSpotPatchObject
		{
			Properties = new Dictionary<string, object?>
			{
				{ "firstname", "Robert"},
			}
		};
		readObject = await Client.Crm.Contacts.PatchAsync(readObject.Id, patchInfo);

		// Re-read the item and check the update
		readObject = await Client.Crm.Contacts.GetAsync(createdObject.Id);
		readObject.Should().NotBeNull();
		readObject.Id.Should().Be(createdObject.Id);
		readObject.Properties.Should().NotBeEmpty();
		readObject.Properties["firstname"].Should().Be("Robert");

		// Delete the item
		await Client.Crm.Contacts.DeleteAsync(new DeleteRequest
		{
			ObjectId = createdObject.Id
		}
		);
	}
}
