using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Src.Extensions;

public class Authentication: IAuthenticationHandlerProvider
{
    public Task<IAuthenticationHandler?> GetHandlerAsync(HttpContext context, string authenticationScheme)
    {
        throw new NotImplementedException();
    }
}