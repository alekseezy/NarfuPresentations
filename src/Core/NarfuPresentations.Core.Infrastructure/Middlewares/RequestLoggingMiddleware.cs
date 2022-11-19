using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using NarfuPresentations.Core.Application.Identity;

namespace NarfuPresentations.Core.Infrastructure.Middlewares;

public class RequestLoggingMiddleware : IMiddleware
{
    private readonly ICurrentUser _currentUserService;
    private readonly ILogger _logger;

    public RequestLoggingMiddleware(ILoggerFactory loggerFactory, ICurrentUser currentUserService)
    {
        _currentUserService = currentUserService;
        _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        finally
        {
            var user = !string.IsNullOrEmpty(_currentUserService.GetUserEmail()) ? _currentUserService.GetUserEmail() : "Anonymous";

            var messageBuilder = new StringBuilder();
            var ip = context.Connection.RemoteIpAddress?.MapToIPv4().ToString();

            messageBuilder.AppendLine("HTTP Request Information:");
            messageBuilder.Append("Request By: ");
            messageBuilder.Append(user);
            messageBuilder.AppendFormat("({0})\n", ip);
            messageBuilder.Append("Schema: ");
            messageBuilder.AppendLine(context.Request.Scheme);
            messageBuilder.Append("Host: ");
            messageBuilder.AppendLine(context.Request.Host.ToString());
            messageBuilder.Append("Query String: ");
            messageBuilder.AppendLine(context.Request.QueryString.ToString());
            messageBuilder.Append("Response Status Code: ");
            messageBuilder.AppendLine(context.Response.StatusCode.ToString());

            _logger.LogInformation("{message}", messageBuilder.ToString());
        }
    }
}
