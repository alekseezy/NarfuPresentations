using System.Net;

using NarfuPresentations.Core.Application.Common.Exceptions;

namespace NarfuPresentations.Core.Application.Authentication.Exceptions;

public class UnauthorizedException : ApplicationLayerException
{
    public UnauthorizedException(string message)
        : base(message, null, HttpStatusCode.Unauthorized)
    {
    }
}
