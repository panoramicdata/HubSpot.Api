using HubSpot.Api.Exceptions;
using HubSpot.Api.Models.Crm;
using HubSpot.Api.Models.Crm.Base;
using System.Net;

namespace HubSpot.Api.Test.Crm;

/// <summary>
/// Steps shared by the CRM object tests, which all create, re-read, search and tidy up in the
/// same way over different object types.
/// </summary>
internal static class CrmTestHelpers
{
	private static CancellationToken CancellationToken => TestContext.Current.CancellationToken;

	/// <summary>
	/// Creates a test object and returns its id.
	/// </summary>
	/// <remarks>
	/// A test object left behind by an earlier run makes the create return Conflict. HubSpot puts
	/// the id of the object that already exists at the end of the message, so the test can carry
	/// on against that one rather than failing on someone else's leftovers.
	/// </remarks>
	internal static async Task<string> CreateOrRecoverIdAsync<T>(
		CreateRequest createRequest,
		Func<CreateRequest, CancellationToken, Task<T>> createAsync)
		where T : HubSpotObject
	{
		try
		{
			var createdObject = await createAsync(createRequest, CancellationToken);
			createdObject.Should().NotBeNull();
			return createdObject.Id;
		}
		catch (HubSpotApiErrorException e) when (e.StatusCode == HttpStatusCode.Conflict)
		{
			e.Error.Category.Should().Be(ErrorCategory.Conflict);
			return e.Message.Split(' ').Last();
		}
	}

	/// <summary>
	/// Re-reads an object by id and asserts that it came back with the expected id and some properties.
	/// </summary>
	internal static async Task<T> ReadAndVerifyAsync<T>(
		string id,
		Func<string, CancellationToken, Task<T>> getAsync)
		where T : HubSpotObjectWithProperties
	{
		var readObject = await getAsync(id, CancellationToken);
		readObject.Should().NotBeNull();
		readObject.Id.Should().Be(id);
		readObject.Properties.Should().NotBeEmpty();
		return readObject;
	}

	/// <summary>
	/// Builds a search for a single property value, returning the given properties sorted by
	/// <paramref name="propertyName"/>.
	/// </summary>
	internal static SearchRequest SearchFor(
		string propertyName,
		FilterOperator filterOperator,
		string value,
		params string[] properties) => new()
		{
			After = "",
			FilterGroups =
			[
				new()
				{
					Filters =
					[
						new Filter
						{
							PropertyName = propertyName,
							Operator = filterOperator,
							Value = value
						}
					]
				}
			],
			Limit = 100,
			Properties = [.. properties],
			Sorts = [propertyName]
		};

	/// <summary>
	/// Builds the request body that the association endpoints take for a single object id.
	/// </summary>
	internal static GetAssociationsFor AssociationsForId(string id) => new()
	{
		Inputs =
		[
			new()
			{
				Id = id,
			}
		]
	};
}
