using System.Net;

namespace NarfuPresentations.Core.Application.Common.Exceptions;

public class NotFoundException : ApplicationLayerException
{
    public NotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.NotFound)
        : base(message, null, statusCode)
    {
    }
}
