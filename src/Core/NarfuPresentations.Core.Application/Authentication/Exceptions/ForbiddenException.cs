using NarfuPresentations.Core.Application.Common.Exceptions;

using System.Net;

namespace NarfuPresentations.Core.Application.Authentication.Exceptions;

public class ForbiddenException : ApplicationLayerException
{
    public ForbiddenException(string message)
        : base(message, null, HttpStatusCode.Forbidden)
    {
    }
}
