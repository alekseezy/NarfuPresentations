using NarfuPresentations.Core.Application.Common.Exceptions;

using System.Net;

namespace NarfuPresentations.Core.Application.Authentication.Exceptions;

public class UnauthorizedException : ApplicationLayerException
{
    public UnauthorizedException(string message)
       : base(message, null, HttpStatusCode.Unauthorized)
    {
    }
}
