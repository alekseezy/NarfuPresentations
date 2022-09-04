using System.Net;

using Microsoft.AspNetCore.Http;

using NarfuPresentations.Core.Application.Common.Exceptions;
using NarfuPresentations.Core.Application.Common.Serializers;
using NarfuPresentations.Core.Application.Identity;
using NarfuPresentations.Shared.Contracts.Middlewares.Results;

using Serilog;
using Serilog.Context;

namespace NarfuPresentations.Core.Infrastructure.Middlewares;

internal class ExceptionMiddleware : IMiddleware
{
    private readonly ICurrentUser _currentUser;
    private readonly ISerializerService _jsonSerializer;

    public ExceptionMiddleware(
        ICurrentUser currentUser,
        ISerializerService jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
        _currentUser = currentUser;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var email = _currentUser.GetUserEmail() ?? "Anonymous";
            var userId = _currentUser.GetUserId();
            if (userId != Guid.Empty) LogContext.PushProperty("UserId", userId);
            LogContext.PushProperty("UserEmail", email);
            var errorId = Guid.NewGuid().ToString();
            LogContext.PushProperty("ErrorId", errorId);
            LogContext.PushProperty("StackTrace", exception.StackTrace);
            var errorResult = new ErrorResult
            {
                Source = exception.TargetSite?.DeclaringType?.FullName,
                Exception = exception.Message.Trim(),
                ErrorId = errorId,
                SupportMessage =
                    $"Provide the ErrorId {errorId} to the support team for further analysis."
            };
            errorResult.Messages.Add(exception.Message);
            if (exception is not ApplicationLayerException && exception.InnerException is not null)
                while (exception.InnerException is not null)
                    exception = exception.InnerException;

            switch (exception)
            {
                case ApplicationLayerException e:
                    errorResult.StatusCode = (int)e.StatusCode;
                    if (e.ErrorMessages is not null) errorResult.Messages = e.ErrorMessages;

                    break;

                case KeyNotFoundException:
                    errorResult.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                default:
                    errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            Log.Error(
                "{ErrorResultException} Request failed with Status Code {ResponseStatusCode} and Error Id {ErrorId}",
                errorResult.Exception, context.Response.StatusCode, errorId);
            var response = context.Response;
            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                response.StatusCode = errorResult.StatusCode;
                await response.WriteAsync(_jsonSerializer.Serialize(errorResult));
            }
            else
            {
                Log.Warning("Can't write error response. Response has already started");
            }
        }
    }
}
