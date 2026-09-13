using System.Net;
using FluentAssertions;
using HubSpot.Api.Exceptions;
using HubSpot.Api.Models;
using Xunit.Abstractions;

namespace HubSpot.Api.Test.Crm;

public class TaskTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
    [Fact]
    public async void CreateReadUpdateAndDelete_Succeeds()
    {
        var createRequest = new CreateRequest
        {
            Properties = new Dictionary<string, string>
            {
                { "hs_timestamp", "2019-10-30T03:30:17.883Z" },
                { "hs_task_body", """
                                    <h3>Send Proposal</h3>
                                    <a href="https://example.com">Example</a>
                                  """ },
                { "hs_task_subject", "Follow-up for Brian Buyer" },
                { "hs_task_status", "NOT_STARTED" },
                { "hs_task_priority", "HIGH" },
                { "hs_task_type", "TODO" }
            },
            Associations = []
        };

        HubSpotObject createdObject;
        try
        {
            createdObject = await Client.Crm.Tasks.CreateAsync(createRequest);
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
        var readObject = await Client.Crm.Tasks.GetAsync(createdObject.Id);
        readObject.Should().NotBeNull();
        readObject.Id.Should().Be(createdObject.Id);
        readObject.Properties.Should().NotBeEmpty();

        // Delete the item
        await Client
            .Crm
            .Tasks
            .ArchiveAsync(createdObject.Id);
    }
}