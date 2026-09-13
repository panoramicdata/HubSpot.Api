using HubSpot.Api.Models;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface ITasks
{
    // https://developers.hubspot.com/docs/guides/api/crm/engagements/tasks
    
    [Post("/crm/v3/objects/tasks")]
    Task<HubSpotObject> CreateAsync(
        [Body] CreateRequest createRequest,
        CancellationToken cancellationToken = default
    );
    
    [Get("/crm/v3/objects/tasks/{id}")]
    Task<HubSpotObject> GetAsync(
        string id,
        [Query] IReadOnlyList<string>? properties = null,
        [Query] IReadOnlyList<string>? propertiesWithHistory = null,
        [Query] IReadOnlyList<string>? associations = null,
        [Query] bool? archived = null,
        CancellationToken cancellationToken = default
    );
    
    [Patch("/crm/v3/objects/tasks/{id}")]
    Task<HubSpotObject> PatchAsync(
        string id,
        [Body] HubSpotPatchObject hubSpotObject,
        CancellationToken cancellationToken = default
    );
    
    [Get("/crm/v3/objects/tasks")]
    Task<CrmPage> GetPageAsync(
        int? limit = null,
        string? after = null,
        ICollection<string>? properties = null,
        ICollection<string>? propertiesWithHistory = null,
        ICollection<string>? associations = null,
        bool? archived = null,
        CancellationToken cancellationToken = default
    );
    
    [Delete("/crm/v3/objects/tasks/{id}")]
    Task ArchiveAsync(
        string id,
        CancellationToken cancellationToken = default
    );
    
    [Post("/crm/v3/objects/tasks/search")]
    Task<CrmPage> SearchAsync(
        [Body] SearchRequest searchRequest,
        CancellationToken cancellationToken = default
    );
}