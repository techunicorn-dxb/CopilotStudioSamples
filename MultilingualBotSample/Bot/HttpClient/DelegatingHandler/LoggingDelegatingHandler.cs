using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class LoggingDelegatingHandler(ILogger<LoggingDelegatingHandler> logger)
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Before HTTP request");

            var result = await base.SendAsync(request, cancellationToken);

            result.EnsureSuccessStatusCode();

            logger.LogInformation("After HTTP request");

            return result;
        }
        catch (Exception e)
        {
            logger.LogError(e, "HTTP request failed");

            throw;
        }
    }
}