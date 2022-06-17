using Microsoft.AspNetCore.Http;

using NarfuPresentations.Core.Application.Identity;

using Serilog;
using Serilog.Context;

namespace NarfuPresentations.Core.Infrastructure.Middlewares;

public class ResponseLoggingMiddleware : IMiddleware
{
    private readonly ICurrentUser _currentUser;

    public ResponseLoggingMiddleware(ICurrentUser currentUser) =>
        _currentUser = currentUser;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context);

        var originalBody = context.Response.Body;

        using var loggedBody = new MemoryStream();
        context.Response.Body = loggedBody;
        var responseBody = string.Empty;
        if (context.Request.Path.ToString().Contains("tokens"))
        {
            responseBody = "[Redacted] Contains Sensetive Information.";
        }
        else
        {
            loggedBody.Seek(0, SeekOrigin.Begin);
            responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        }

        var email = _currentUser.GetUserEmail() is { } userEmail ? userEmail : "Anonymous";
        var userId = _currentUser.GetUserId();
        if (userId != Guid.Empty)
            LogContext.PushProperty("UserId", userId);

        LogContext.PushProperty("UserEmail", email);
        LogContext.PushProperty("StatusCode", context.Response.StatusCode);
        LogContext.PushProperty("ResponseTimeUTC", DateTime.UtcNow);
        Log.ForContext("ResponseHeaders", context.Response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
            .ForContext("ResponseBody", responseBody)
            .Information("HTTP {RequestMethod} Request to {RequestPath} by {RequesterEmail} has Status Code {StatusCode}",
            context.Request.Method, context.Request.Path, string.IsNullOrEmpty(email) ? "Anonymous" : email,
            context.Response.StatusCode);

        loggedBody.Seek(0, SeekOrigin.Begin);
        await loggedBody.CopyToAsync(originalBody);
    }
}
