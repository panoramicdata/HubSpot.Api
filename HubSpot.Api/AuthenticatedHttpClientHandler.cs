﻿using HubSpot.Api.Exceptions;
using HubSpot.Api.Models.Crm;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace HubSpot.Api;

internal sealed class AuthenticatedHttpClientHandler : HttpClientHandler
{
	private readonly HubSpotClientOptions _options;
	private readonly ILogger _logger;
	private readonly LogLevel _levelToLogAt = LogLevel.Trace;

	public AuthenticatedHttpClientHandler(HubSpotClientOptions hubSpotClientOptions)
	{
		_options = hubSpotClientOptions;

		// Use the default logger if no factory is provided
		_logger = _options.Logger ?? _options.LoggerFactory?.CreateLogger<AuthenticatedHttpClientHandler>() as ILogger ?? NullLogger.Instance;
	}

	protected override async Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken)
	{
		if (_options.ReadOnly)
		{
			// Simplistic ReadOnly implementation to ensure only reading from the API
			// Check that this is a GET
			if (request.Method != HttpMethod.Get)
			{
				throw new InvalidOperationException(Resources.OnlyReadOnlyOperationsPermitted);
			}
		}

		// Ensure the Access Token is set
		if (_options.AccessToken?.Length == 0)
		{
			throw new InvalidOperationException(Resources.AccessTokenIsNotSet);
		}
		// The Access Token is set
		request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {_options.AccessToken}");

		var logPrefix = $"Request {Guid.NewGuid()}: ";

		// Add the request headers
		if (_options.UserAgent is not null)
		{
			request.Headers.Add("User-Agent", _options.UserAgent);
		}

		var attemptCount = 0;
		while (true)
		{
			attemptCount++;
			cancellationToken.ThrowIfCancellationRequested();

			// Only do diagnostic logging if we're at the level we want to enable for as this is more efficient
			if (_logger.IsEnabled(_levelToLogAt))
			{
				_logger.Log(_levelToLogAt, "{LogPrefix}Request\r\n{Request}", logPrefix, request);
				if (request.Content != null)
				{
					var requestContent = await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
					_logger.Log(_levelToLogAt, "{LogPrefix}RequestContent\r\n{RequestContent}", logPrefix, requestContent);
				}
			}

			// Complete the action
			var httpResponseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

			// Only do diagnostic logging if we're at the level we want to enable for as this is more efficient
			if (_logger.IsEnabled(_levelToLogAt))
			{
				_logger.Log(_levelToLogAt, "{LogPrefix}Response\r\n{HttpResponseMessage}", logPrefix, httpResponseMessage);
				if (httpResponseMessage.Content != null)
				{
					var responseContent = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
					_logger.Log(_levelToLogAt, "{LogPrefix}ResponseContent\r\n{ResponseContent}", logPrefix, responseContent);
				}
			}

			TimeSpan delay;
			// As long as we were not given a back-off request then we'll return the response and any further StatusCode handling is up to the caller
			var statusCodeInt = (int)httpResponseMessage.StatusCode;

			switch (statusCodeInt)
			{
				case 403: // Forbidden
				case 409: // Conflict
					var responseContentString = await httpResponseMessage
						.Content!
						.ReadAsStringAsync(cancellationToken)
						.ConfigureAwait(false);
					var hubSpotError = await HubSpotClient.SystemTextJsonContentSerializer
						.FromHttpContentAsync<HubSpotError>(httpResponseMessage.Content, cancellationToken)
						?? throw new HubSpotApiDeserializationException(responseContentString);
					throw new HubSpotApiErrorException(httpResponseMessage.StatusCode, hubSpotError);
				case 429:
					// Back off by the requested amount.
					var headers = httpResponseMessage.Headers;
					var foundHeader = headers.TryGetValues("Retry-After", out var retryAfterHeaders);
					var retryAfterSecondsString = foundHeader
						? retryAfterHeaders?.FirstOrDefault() ?? "1"
						: "1";
					if (!int.TryParse(retryAfterSecondsString, out var retryAfterSeconds))
					{
						retryAfterSeconds = 1;
					}

					delay = TimeSpan.FromSeconds(1.1 * retryAfterSeconds);
					_logger.LogDebug(
						"{LogPrefix}Received {StatusCodeInt} on attempt {AttemptCount}/{MaxAttemptCount}.",
						logPrefix, statusCodeInt, attemptCount, _options.MaxAttemptCount
						);
					break;
				case 502:
					_logger.LogInformation(
						"{LogPrefix}Received {StatusCodeInt} on attempt {AttemptCount}/{MaxAttemptCount}.",
						logPrefix, statusCodeInt, attemptCount, _options.MaxAttemptCount
						);
					delay = TimeSpan.FromSeconds(5);
					break;
				default:
					if (attemptCount > 1)
					{
						_logger.LogDebug(
							"{LogPrefix}Received {StatusCodeInt} on attempt {AttemptCount}/{MaxAttemptCount}.",
							logPrefix, statusCodeInt, attemptCount, _options.MaxAttemptCount
							);
					}

					if (statusCodeInt == 500)
					{
						_logger.LogError(
							"{LogPrefix}Received remote error code 500 on attempt {AttemptCount}/{MaxAttemptCount}. ({Method} - {Url})",
							logPrefix,
							attemptCount,
							_options.MaxAttemptCount,
							request.Method.ToString(),
							request.RequestUri
							);
					}

					return httpResponseMessage;
			}
			// Try up to the maximum retry count.
			if (attemptCount >= _options.MaxAttemptCount)
			{
				_logger.LogInformation(
					"{LogPrefix}Giving up retrying. Returning {StatusCodeInt} on attempt {AttemptCount}/{MaxAttemptCount}. ({Method} - {Url})",
					logPrefix,
					statusCodeInt,
					attemptCount,
					_options.MaxAttemptCount,
					request.Method.ToString(),
					request.RequestUri
					);
				return httpResponseMessage;
			}

			_logger.LogInformation(
				"{LogPrefix}Received {StatusCode} on attempt {AttemptCount}/{MaxAttemptCount} - Waiting {TotalSeconds:N2}s. ({Method} - {Url})",
				logPrefix,
				statusCodeInt,
				attemptCount,
				_options.MaxAttemptCount,
				delay.TotalSeconds,
				request.Method.ToString(),
				request.RequestUri
				);
			await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
		}
	}
}