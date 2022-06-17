using System.Net;

namespace NarfuPresentations.Core.Application.Common.Exceptions;

public class InternalServerException : ApplicationLayerException
{
    public InternalServerException(string message, List<string>? errors = default)
        : base(message, errors, HttpStatusCode.InternalServerError)
    {
    }
}
