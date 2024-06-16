using System.Text.Json.Serialization;

namespace Domain.Core.Exceptions;

public class BaseException(ExceptionCode exceptionCode, string message, int httpStatusCode)
    : Exception(message)
{
    [JsonIgnore] public int HttpStatusCode { get; } = httpStatusCode;

    public ExceptionCode ExceptionCode { get; } = exceptionCode;
}