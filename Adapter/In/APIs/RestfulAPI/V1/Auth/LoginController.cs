using Adapter.In.RestfulAPI.Base;
using Adapter.In.RestfulAPI.V1.Auth.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ports.In.Auth;
using Ports.In.Cache;

namespace Adapter.In.RestfulAPI.V1.Auth;

[Route("api/v{version:apiVersion}")]
public class LoginController(
    IConfiguration configuration,
    IAuthenticator authenticator,
    ICache cache
) : RestfulControllerBase(configuration, cache)
{
    [HttpPost("login")]
    public Task<IActionResult> Login([FromBody] LoginForm form)
    {
        return Handle(async () =>
        {
            var sessionUser = await authenticator.Login(form.UserId, form.Password);
            
            HttpContext.Session.SetString(SessionName, sessionUser.Id.ToString());

            return Ok(new { success = true });
        });
    }


    [HttpGet("logout")]
    public IActionResult Logout()
    {
        return WithAuthentication( user =>
        {
            Response.Cookies.Delete(SessionName);

            cache.RemoveSession(user.SessionId);

            return Ok(new
            {
                message = "Success"
            });
        });
    }
}