using System.Net;

namespace NarfuPresentations.Core.Application.Common.Exceptions;

public class ApplicationLayerException : Exception
{
    public ApplicationLayerException(string message, List<string>? errors = default,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }

    public List<string>? ErrorMessages { get; }

    public HttpStatusCode StatusCode { get; }
}
