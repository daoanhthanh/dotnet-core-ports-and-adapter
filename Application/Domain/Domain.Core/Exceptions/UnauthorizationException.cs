using Domain.Core.Exceptions;

namespace Domain.Core.Exceptions;

public class UnauthorizationException(string message)
    : BaseException(ExceptionCode.UNAUTHORIZED, message, 401);