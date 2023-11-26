# HubSpot.Api

The HubSpot REST API nuget package, authored by Panoramic Data Limited.

[![Nuget](https://img.shields.io/nuget/v/HubSpot.Api)](https://www.nuget.org/packages/HubSpot.Api/)
[![Nuget](https://img.shields.io/nuget/dt/HubSpot.Api)](https://www.nuget.org/packages/HubSpot.Api/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/59bb860a1129452d8211893953ec056f)](https://app.codacy.com/gh/panoramicdata/HubSpot.Api/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=panoramicdata/HubSpot.Api&amp;utm_campaign=Badge_grade)

If you want some HubSpot software developed, come find us at: https://www.panoramicdata.com/ !

---
## Examples

A simple example:

```c#
using HubSpot.Api;

[...]

public static async Task GetAllDeals(ILogger logger, CancellationToken cancellationToken)
{
	using var hubSpotClient = new HubSpotClient(
		new HubSpotClientOptions
		{
			AccessToken = "[ACCESSTOKEN]",
			Logger = logger
		}
	);

	var deals = await hubSpotClient
		.Deals
		.GetPageAsync(cancellationToken: cancellationToken)
		.ConfigureAwait(false);

	Console.WriteLine($"Deal Count: {deals.Results.Count}");
}
```
---
## API Coverage

This table provides a full list of HubSpot APIs, built from [this JSON file](https://api.hubspot.com/api-catalog-public/v1/apis), including references to their documentation and coverage in this library.

We're aiming for full coverage and all Pull Requests are welcome.

| Type | Object | Documentation | HubSpot API Status | Nuget Coverage |
| - | - | - | - | - |
| Analytics | Analytics | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/events/v3/send) | Developer Preview | None |
| Auth | Auth | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/oauth/v1) | Latest | None |
| Automation | Automation | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/automation/v4/actions) | Latest | None |
| Business Units | Business Units | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/business-units/v3) | Stable | None |
| Communication Preferences | Communication Preferences | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/communication-preferences/v3) | Developer Preview | None |
| Conversations | Conversations | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/conversations/v3/visitor-identification) | Latest | None |
| CMS | Domains | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/domains) | Developer Preview | None |
| CMS | Source Code | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/source-code) | Developer Preview | None |
| CMS | Blog Posts | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/blogs/blog-posts) | Developer Preview | None |
| CMS | Authors | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/blogs/authors) | Developer Preview | None |
| CMS | URL Redirects | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/url-redirects) | Developer Preview | None |
| CMS | Performance | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/performance) | Developer Preview | None |
| CMS | Hubdb | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/hubdb) | Developer Preview | None |
| CMS | Tags | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/blogs/tags) | Developer Preview | None |
| CMS | Audit Logs | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/audit-logs) | Developer Preview | None |
| CMS | Site Search | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/site-search) | Developer Preview | None |
| CRM | Accounting | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/extensions/accounting) | Latest | None |
| CRM | Associations | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/associations) | Latest | None |
| CRM | Associations (v4) | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v4/associations) | Stable | None |
| CRM | Calling | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/extensions/calling) | Latest | None |
| CRM | Companies | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/companies) | Latest | Partial |
| CRM | Contacts | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/contacts) | Latest | Partial |
| CRM | Extensions | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/extensions/sales-objects/v1/object-types) | Latest | None |
| CRM | Deals | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/deals) | Latest | Partial |
| CRM | Feedback Submissions | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/feedback_submissions) | Developer Preview | Partial |
| CRM | Imports | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/imports) | Latest | None |
| CRM | Line Items | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/line_items) | Latest | Partial |
| CRM | Objects | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects) | Latest | None |
| CRM | Owners | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/owners) | Latest | None |
| CRM | Pipelines | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/pipelines) | Latest | None |
| CRM | Properties | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/properties) | Latest | None |
| CRM | Quotes | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/quotes) | Latest | None |
| CRM | Schemas | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/schemas) | Latest | None |
| CRM | Tickets | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/tickets) | Latest | Partial |
| CRM | Timeline | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/timeline) | Latest | None |
| CRM | Videoconferencing | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/extensions/videoconferencing) | Latest | None |
| Events | Events | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/events/v3/events) | Developer Preview | None |
| Marketing | Marketing Events (beta) | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/marketing/v3/marketing-events-beta) | Latest | None |
| Marketing | Transactional | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/marketing/v3/transactional) | Latest | None |
| Webhooks | Webhooks | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/webhooks/v3) | Latest | None |
 