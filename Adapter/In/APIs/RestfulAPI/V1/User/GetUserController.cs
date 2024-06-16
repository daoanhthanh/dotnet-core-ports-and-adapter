using Adapter.In.RestfulAPI.V1.User.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ports.In.Cache;
using Ports.In.User;

namespace Adapter.In.RestfulAPI.V1.User;

public class GetUserController(
    // INotificationHandler<DomainNotificationDTO> notifications,
    // IMediatorHandler mediator,
    IConfiguration conf,
    GetUser getUser,
    ICache cache)
    : UserV1Controller(
        conf,
        cache
        // notifications, mediator
    )
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await getUser.All());
    }


    [HttpGet("{phoneOrEmail}")]
    public async Task<IActionResult> GetByPhoneOrEmail(string phoneOrEmail)
    {
        var user = await getUser.ByPhoneOrEmail(phoneOrEmail);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}