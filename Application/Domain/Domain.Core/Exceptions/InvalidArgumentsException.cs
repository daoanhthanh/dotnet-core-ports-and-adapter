using Domain.Core.Exceptions;

namespace Domain.Core.Exceptions;

public class InvalidArgumentsException(string message)
    : BaseException(ExceptionCode.UNPROCESSABLE_ENTITY, message, 422);