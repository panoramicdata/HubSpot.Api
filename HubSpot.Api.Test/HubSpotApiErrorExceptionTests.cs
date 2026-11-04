using HubSpot.Api.Exceptions;
using HubSpot.Api.Models.Crm;
using System.Net;

namespace HubSpot.Api.Test;

public class HubSpotApiErrorExceptionTests
{
	private static HubSpotApiErrorException Build(HttpStatusCode status, string message) => new(
		status,
		new HubSpotError
		{
			Status = "error",
			Message = message,
			CorrelationId = "corr-1",
			Category = ErrorCategory.Conflict
		});

	[Fact]
	public void Ctor_StoresStatusMessage()
	{
		var exception = Build(HttpStatusCode.Conflict, "duplicate");

		exception.Message.Should().Be("duplicate");
	}

	[Theory]
	[InlineData(HttpStatusCode.BadRequest)]
	[InlineData(HttpStatusCode.TooManyRequests)]
	[InlineData(HttpStatusCode.InternalServerError)]
	public void Ctor_StoresStatusCode(HttpStatusCode status)
	{
		Build(status, "x").StatusCode.Should().Be(status);
	}

	[Fact]
	public void Ctor_StoresError()
	{
		var exception = Build(HttpStatusCode.BadRequest, "boom");

		exception.Error.CorrelationId.Should().Be("corr-1");
	}
}
