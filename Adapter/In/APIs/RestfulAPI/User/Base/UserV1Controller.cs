using Adapter.In.RestfulAPI.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ports.In.Cache;

namespace Adapter.In.RestfulAPI.User.Base;

// [Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/users")]
[Produces("application/json")]
public abstract class UserV1Controller(
    // INotificationHandler<DomainNotificationDTO> notifications,
    // IMediatorHandler mediator
    IConfiguration conf,
    ICache cache
) : RestfulControllerBase(
    conf,
    cache
    // notifications, mediator
);