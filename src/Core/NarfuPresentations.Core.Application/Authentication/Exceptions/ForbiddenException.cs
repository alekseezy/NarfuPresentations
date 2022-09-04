using System.Net;

using NarfuPresentations.Core.Application.Common.Exceptions;

namespace NarfuPresentations.Core.Application.Authentication.Exceptions;

public class ForbiddenException : ApplicationLayerException
{
    public ForbiddenException(string message)
        : base(message, null, HttpStatusCode.Forbidden)
    {
    }
}
