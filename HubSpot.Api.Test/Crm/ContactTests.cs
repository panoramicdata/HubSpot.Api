using FluentAssertions;
using HubSpot.Api.Exceptions;
using HubSpot.Api.Models;
using System.Net;

namespace HubSpot.Api.Test.Crm;

public class ContactTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async Task GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.Contacts.GetPageAsync(cancellationToken: CancellationToken);
		page.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task SearchAsync_ByEmail_Succeeds()
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
		}, cancellationToken: CancellationToken);

		page.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task CreateReadUpdateAndDelete_Succeeds()
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

		HubSpotObject createdObject;
		try
		{
			createdObject = await Client.Crm.Contacts.CreateAsync(createRequest, cancellationToken: CancellationToken);
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
		var readObject = await Client.Crm.Contacts.GetAsync(createdObject.Id, cancellationToken: CancellationToken);
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
		_ = await Client.Crm.Contacts.PatchAsync(readObject.Id, patchInfo, cancellationToken: CancellationToken);

		// Re-read the item and check the update
		readObject = await Client.Crm.Contacts.GetAsync(createdObject.Id, cancellationToken: CancellationToken);
		readObject.Should().NotBeNull();
		readObject.Id.Should().Be(createdObject.Id);
		readObject.Properties.Should().NotBeEmpty();
		readObject.Properties["firstname"].Should().Be("Robert");

		// Delete the item
		await Client.Crm.Contacts.DeleteAsync(new DeleteRequest
		{
			ObjectId = createdObject.Id
		}, cancellationToken: CancellationToken
		);
	}
}
