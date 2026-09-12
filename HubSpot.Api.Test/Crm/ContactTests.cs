using HubSpot.Api.Models.Crm;
using HubSpot.Api.Models.Crm.Base;

namespace HubSpot.Api.Test.Crm;

public class ContactTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.Contacts.GetPageAsync(new(), CancellationToken);
		page.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async Task SearchAsync_ByEmail_Succeeds()
	{
		var page = await Client.Crm.Contacts.SearchAsync(
			CrmTestHelpers.SearchFor(
				"email",
				FilterOperator.Eq,
				"david.bond@panoramicdata.com",
				"email",
				"firstname",
				"lastname",
				"phone",
				"company",
				"website"),
			cancellationToken: CancellationToken);

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

		var createdId = await CrmTestHelpers.CreateOrRecoverIdAsync(
			createRequest,
			(request, cancellationToken) => Client.Crm.Contacts.CreateAsync(request, cancellationToken));

		// Re-read the item
		var readObject = await ReadContactAsync(createdId);

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
		readObject = await ReadContactAsync(createdId);
		readObject.Properties["firstname"].Should().Be("Robert");

		// Delete the item
		await Client.Crm.Contacts.DeleteAsync(new DeleteRequest
		{
			ObjectId = createdId
		}, cancellationToken: CancellationToken);
	}

	private Task<HubSpotContact> ReadContactAsync(string id)
		=> CrmTestHelpers.ReadAndVerifyAsync(
			id,
			(contactId, cancellationToken) => Client.Crm.Contacts.GetAsync(contactId, new(), cancellationToken));
}
