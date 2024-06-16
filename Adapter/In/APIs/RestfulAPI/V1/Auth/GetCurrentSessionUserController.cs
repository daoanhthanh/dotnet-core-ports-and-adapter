using Adapter.In.RestfulAPI.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ports.In.Cache;

namespace Adapter.In.RestfulAPI.V1.Auth;

[Route("api/v{version:apiVersion}")]
public class GetCurrentSessionUserController(
    IConfiguration configuration,
    ICache cache
) : RestfulControllerBase(configuration, cache)
{
    [HttpGet("me")]
    public IActionResult GetMyInfo()
    {
        return WithAuthentication(user =>
        {
            cache.GetSession(user.SessionId);

            return Ok();
        });
    }
}