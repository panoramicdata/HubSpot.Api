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

Key:

| Symbol | API Coverage |
| - | - |
| :black_circle: | None |
| :yellow_circle: | Partial |
| :green_square: | Full |


| Type | Object                    | Documentation | HubSpot API Status | Nuget Coverage |
| - |---------------------------| - | - | - |
| Analytics | Analytics                 | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/events/v3/send) | Developer Preview | :black_circle: |
| Auth | Auth                      | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/oauth/v1) | Latest | :black_circle: |
| Automation | Automation                | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/automation/v4/actions) | Latest | :black_circle: |
| Business Units | Business Units            | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/business-units/v3) | Stable | :black_circle: |
| Communication Preferences | Communication Preferences | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/communication-preferences/v3) | Developer Preview | :black_circle: |
| Conversations | Conversations             | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/conversations/v3/visitor-identification) | Latest | :black_circle: |
| CMS | Domains                   | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/domains) | Developer Preview | :yellow_circle: |
| CMS | Source Code               | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/source-code) | Developer Preview | :black_circle: |
| CMS | Blog Posts                | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/blogs/blog-posts) | Developer Preview | :black_circle: |
| CMS | Authors                   | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/blogs/authors) | Developer Preview | :black_circle: |
| CMS | URL Redirects             | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/url-redirects) | Developer Preview | :black_circle: |
| CMS | Performance               | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/performance) | Developer Preview | :black_circle: |
| CMS | Hubdb                     | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/hubdb) | Developer Preview | :black_circle: |
| CMS | Tags                      | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/blogs/tags) | Developer Preview | :black_circle: |
| CMS | Audit Logs                | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/audit-logs) | Developer Preview | :black_circle: |
| CMS | Site Search               | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/cms/v3/site-search) | Developer Preview | :black_circle: |
| CRM | Accounting                | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/extensions/accounting) | Latest | :black_circle: |
| CRM | Associations              | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/associations) | Latest | :black_circle: |
| CRM | Associations (v4)         | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v4/associations) | Stable | :black_circle: |
| CRM | Calling                   | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/extensions/calling) | Latest | :black_circle: |
| CRM | Companies                 | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/companies) | Latest | :yellow_circle: |
| CRM | Contacts                  | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/contacts) | Latest | :yellow_circle: |
| CRM | Extensions                | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/extensions/sales-objects/v1/object-types) | Latest | :black_circle: |
| CRM | Deals                     | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/deals) | Latest | :yellow_circle: |
| CRM | Feedback Submissions      | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/feedback_submissions) | Developer Preview | :yellow_circle: |
| CRM | Imports                   | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/imports) | Latest | :black_circle: |
| CRM | Line Items                | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/line_items) | Latest | :yellow_circle: |
| CRM | Objects                   | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects) | Latest | :black_circle: |
| CRM | Owners                    | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/owners) | Latest | :black_circle: |
| CRM | Pipelines                 | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/pipelines) | Latest | :black_circle: |
| CRM | Properties                | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/properties) | Latest | :black_circle: |
| CRM | Quotes                    | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/quotes) | Latest | :black_circle: |
| CRM | Schemas                   | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/schemas) | Latest | :black_circle: |
| CRM | Tickets                   | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/tickets) | Latest | :yellow_circle: |
| CRM | Timeline                  | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/timeline) | Latest | :black_circle: |
| CRM | Videoconferencing         | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/extensions/videoconferencing) | Latest | :black_circle: |
| CRM | Tasks                     | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/tasks) | Latest | :yellow_circle: |
| Events | Events                    | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/events/v3/events) | Developer Preview | :black_circle: |
| Marketing | Marketing Events (beta)   | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/marketing/v3/marketing-events-beta) | Latest | :black_circle: |
| Marketing | Transactional             | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/marketing/v3/transactional) | Latest | :black_circle: |
| Webhooks | Webhooks                  | [Link](https://api.hubspot.com/api-catalog-public/v1/apis/webhooks/v3) | Latest | :black_circle: |
 