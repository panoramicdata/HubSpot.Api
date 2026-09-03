using System.Net;

namespace HubSpot.Api.Test;

/// <summary>
/// Tests for header redaction in diagnostic output.
///
/// <para>
/// <c>AuthenticatedHttpClientHandler</c> sets an Authorization header carrying the access token and
/// then passes the whole <see cref="HttpRequestMessage"/> to the logger. Its <c>ToString()</c>
/// renders every header, so without redaction a usable token is written wherever those messages end
/// up. The logging sits inside the retry loop, so it happened once per attempt rather than once per
/// request.
/// </para>
///
/// <para>
/// These are pure unit tests. They construct messages directly and require no credentials, no
/// configuration and no live portal.
/// </para>
/// </summary>
public class HttpExtensionsTests
{
	// Deliberately NOT shaped like a real HubSpot private app token. A literal beginning "pat-na1-"
	// matches HubSpot's published credential format, so GitHub push protection blocks it - correctly,
	// since a test constant should never be mistakable for a live credential.
	private const string FakeToken = "fake-token-for-tests-0123456789abcdef";

	/// <summary>
	/// The headline case: the access token this client sets must not survive into the message.
	/// </summary>
	[Fact]
	public void ToRedactedString_RequestWithBearerToken_DoesNotLeakTheCredential()
	{
		using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.hubapi.com/crm/v3/objects/contacts");
		request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {FakeToken}");

		var rendered = request.ToRedactedString();

		rendered.Should().NotContain(FakeToken);
		rendered.Should().Contain($"Authorization: Bearer <redacted, length {FakeToken.Length}>");
	}

	/// <summary>
	/// Proves the defect being fixed: the framework rendering leaks, the replacement does not.
	/// </summary>
	[Fact]
	public void ToRedactedString_UnlikeToString_DoesNotContainTheToken()
	{
		using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.hubapi.com/crm/v3/objects/contacts");
		request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {FakeToken}");

		request.ToString().Should().Contain(FakeToken, "the framework rendering is what leaked");
		request.ToRedactedString().Should().NotContain(FakeToken);
	}

	/// <summary>
	/// The diagnostically useful parts of the message must survive intact.
	/// </summary>
	[Fact]
	public void ToRedactedString_KeepsMethodUriAndOtherHeaders()
	{
		using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.hubapi.com/crm/v3/objects/contacts");
		request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {FakeToken}");
		request.Headers.TryAddWithoutValidation("User-Agent", "HubSpot.Api");

		var rendered = request.ToRedactedString();

		rendered.Should().Contain("Method: POST");
		rendered.Should().Contain("https://api.hubapi.com/crm/v3/objects/contacts");
		rendered.Should().Contain("User-Agent: HubSpot.Api");
		rendered.Should().NotContain(FakeToken);
	}

	/// <summary>
	/// Content headers are rendered too, so they must be redacted on the same terms.
	/// </summary>
	[Fact]
	public void ToRedactedString_RedactsContentHeaders()
	{
		using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.hubapi.com/crm/v3/objects/contacts")
		{
			Content = new StringContent("{}")
		};
		request.Content!.Headers.TryAddWithoutValidation("X-Api-Key", "s3cr3t-content-header");

		var rendered = request.ToRedactedString();

		rendered.Should().NotContain("s3cr3t-content-header");
		rendered.Should().Contain("<redacted");
		rendered.Should().Contain("Content-Type: text/plain; charset=utf-8");
	}

	/// <summary>
	/// A header added without validation keeps whatever casing the caller used.
	/// </summary>
	/// <param name="headerName">The header name casing under test.</param>
	[Theory]
	[InlineData("authorization")]
	[InlineData("AUTHORIZATION")]
	[InlineData("AuThOrIzAtIoN")]
	public void ToRedactedString_AuthorizationHeader_IsRedactedWhateverTheCasing(string headerName)
	{
		using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.hubapi.com/");
		request.Headers.TryAddWithoutValidation(headerName, $"Bearer {FakeToken}");

		var rendered = request.ToRedactedString();

		rendered.Should().NotContain(FakeToken);
		rendered.Should().Contain("<redacted");
	}

	/// <summary>
	/// The other standard credential-bearing header names are redacted too.
	/// </summary>
	/// <param name="headerName">The credential-bearing header name under test.</param>
	[Theory]
	[InlineData("Proxy-Authorization")]
	[InlineData("Cookie")]
	[InlineData("X-API-Key")]
	[InlineData("Api-Key")]
	[InlineData("X-Api-Token")]
	[InlineData("X-Auth-Token")]
	public void ToRedactedString_OtherCredentialHeaders_AreRedacted(string headerName)
	{
		const string secret = "s3cr3t-value-that-must-not-be-logged";
		using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.hubapi.com/");
		request.Headers.TryAddWithoutValidation(headerName, secret);

		var rendered = request.ToRedactedString();

		rendered.Should().NotContain(secret);
		rendered.Should().Contain("<redacted");
	}

	/// <summary>
	/// A vendor may prefix the standard header name rather than using it directly.
	/// </summary>
	[Fact]
	public void ToRedactedString_VendorPrefixedAuthorizationHeader_IsRedacted()
	{
		using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.hubapi.com/");
		request.Headers.TryAddWithoutValidation("X-Vendor-Authorization", $"Bearer {FakeToken}");

		var rendered = request.ToRedactedString();

		rendered.Should().NotContain(FakeToken);
		rendered.Should().Contain($"X-Vendor-Authorization: Bearer <redacted, length {FakeToken.Length}>");
	}

	/// <summary>
	/// A cookie value also contains a space, so treating the text before the first space as a scheme
	/// would preserve the very value being redacted. Only Authorization style headers keep a scheme.
	/// </summary>
	[Fact]
	public void ToRedactedString_CookieValueContainingASpace_IsRedactedWhole()
	{
		const string cookie = "session=abc123def456; HttpOnly";
		using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.hubapi.com/");
		request.Headers.TryAddWithoutValidation("Cookie", cookie);

		var rendered = request.ToRedactedString();

		rendered.Should().Contain($"Cookie: <redacted, length {cookie.Length}>");
		rendered.Should().NotContain("session=abc");
	}

	/// <summary>
	/// A credential with no scheme prefix has nothing safe to preserve, so all of it goes.
	/// </summary>
	[Fact]
	public void ToRedactedString_CredentialWithoutAScheme_IsRedactedEntirely()
	{
		using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.hubapi.com/");
		request.Headers.TryAddWithoutValidation("X-API-Key", "abcdef123456");

		var rendered = request.ToRedactedString();

		rendered.Should().Contain("X-API-Key: <redacted, length 12>");
	}

	/// <summary>
	/// Response rendering goes through the same redaction, so Set-Cookie is covered.
	/// </summary>
	[Fact]
	public void ToRedactedString_ResponseSetCookie_IsRedacted()
	{
		using var response = new HttpResponseMessage(HttpStatusCode.TooManyRequests);
		response.Headers.TryAddWithoutValidation("Set-Cookie", "session=abc123def456; HttpOnly");

		var rendered = response.ToRedactedString();

		rendered.Should().NotContain("abc123def456");
		rendered.Should().Contain("<redacted");
	}

	/// <summary>
	/// The response status and the Retry-After header drive the back-off, so both must survive.
	/// </summary>
	[Fact]
	public void ToRedactedString_ResponseKeepsStatusAndRetryAfter()
	{
		using var response = new HttpResponseMessage(HttpStatusCode.TooManyRequests);
		response.Headers.TryAddWithoutValidation("Retry-After", "30");

		var rendered = response.ToRedactedString();

		rendered.Should().Contain("StatusCode: 429");
		rendered.Should().Contain("Retry-After: 30");
	}

	/// <summary>
	/// A request carrying no credential is rendered with nothing removed.
	/// </summary>
	[Fact]
	public void ToRedactedString_NoCredentialHeaders_RedactsNothing()
	{
		using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.hubapi.com/");
		request.Headers.TryAddWithoutValidation("User-Agent", "HubSpot.Api");

		var rendered = request.ToRedactedString();

		rendered.Should().Contain("User-Agent: HubSpot.Api");
		rendered.Should().NotContain("<redacted");
	}
}
