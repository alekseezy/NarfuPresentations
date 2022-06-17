using System.Net;

namespace NarfuPresentations.Core.Application.Common.Exceptions;

public class ConflictException : ApplicationLayerException
{
    public ConflictException(string message)
        : base(message, null, HttpStatusCode.Conflict)
    {
    }
}
