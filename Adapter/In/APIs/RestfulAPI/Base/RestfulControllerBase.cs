using Adapter.In.RestfulAPI.Response;
using Domain.Core.Exceptions;
using Domain.Core.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Ports.In.Cache;

namespace Adapter.In.RestfulAPI.Base;

[ApiController]
public class RestfulControllerBase : ControllerBase
{
    private readonly ICache _cache;

    protected readonly string SessionName;

    protected RestfulControllerBase(
        IConfiguration configuration,
        ICache cache
    )
    {
        _cache = cache;
        SessionName = configuration["Session:Name"] ?? "changeMe";
    }


    protected IActionResult WithAuthentication(Func<SessionUser, IActionResult> action)
    {
        try
        {
            var sessionId = HttpContext.Session.GetString(SessionName);

            var sessionUser = _cache.GetSession(SessionId.Of(sessionId!));

            return action(sessionUser!);
        }
        catch (Exception e)
        {
            return Unauthorized(new
            {
                message = "Unauthorized"
            });
        }
    }

    protected async Task<IActionResult> Handle(Func<Task<IActionResult>> result)
    {
        try
        {
            return await result();
        }
        catch (Exception ex)
        {
            return Handle(ex);
        }
    }

    protected Task<IActionResult> WithAuthentication(Func<SessionUser, Task<IActionResult>> action)
    {
        try
        {
            var sessionId = HttpContext.Session.GetString(SessionName);

            var sessionUser = _cache.GetSession(SessionId.Of(sessionId!));

            return action(sessionUser!);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

            return Task.FromResult<IActionResult>(Unauthorized(new
            {
                message = "Unauthorized"
            }));
        }
    }
    
    /// <summary>
    /// Create a proper response for client, based on the response object
    /// </summary>
    private ObjectResult Handle([ActionResultObjectValue] Exception exception)
    {
        var responseError = new ApiResponse(false);

        switch (exception)
        {
            case BaseException e:
                // var responseError = new ApiResponse(false)
                responseError.ErrorCode = e.ExceptionCode.ToString();
                responseError.ErrorMessage = e.Message;

                return new ObjectResult(responseError)
                {
                    StatusCode = e.HttpStatusCode,
                };
            case { } other:

                Console.WriteLine("\nMessage ---\n{0}", other.Message);
                Console.WriteLine(
                    "\nHelpLink ---\n{0}", other.HelpLink);
                Console.WriteLine("\nSource ---\n{0}", other.Source);
                Console.WriteLine(
                    "\nStackTrace ---\n{0}", other.StackTrace);
                Console.WriteLine(
                    "\nTargetSite ---\n{0}", other.TargetSite);

                responseError.ErrorCode = ExceptionCode.INTERNAL_ERROR.ToString();
                responseError.ErrorMessage = "Máy chủ không khả dụng, thử lại sau 5 phút";
                return new ObjectResult(responseError)
                {
                    StatusCode = 500
                };
            default:
                responseError.ErrorCode = ExceptionCode.INTERNAL_ERROR.ToString();
                responseError.ErrorMessage = "Máy chủ không khả dụng, thử lại sau 5 phút";
                return new ObjectResult(responseError)
                {
                    StatusCode = 500
                };
        }
    }

    public override OkObjectResult Ok([ActionResultObjectValue] object? value)
    {
        if (value != null) return base.Ok(new ApiResponse(value));
        return base.Ok(new ApiResponse(true));
    }


    // private readonly DomainNotificationHandler _notifications;
    // private readonly IMediatorHandler _mediator;
    //
    // protected RestfulControllerBase(
    //     INotificationHandler<DomainNotificationDTO> notifications,
    //     IMediatorHandler mediator)
    // {
    //     _notifications = (DomainNotificationHandler)notifications;
    //     _mediator = mediator;
    // }

    // protected IEnumerable<DomainNotificationDTO> Notifications => _notifications.GetNotifications();
    //
    // protected bool IsValidOperation()
    // {
    //     return !_notifications.HasNotifications();
    // }
    //
    // protected new IActionResult Response(object result = null)
    // {
    //     if (IsValidOperation())
    //     {
    //         return Ok(new
    //         {
    //             success = true,
    //             data = result,
    //         });
    //     }
    //
    //     return BadRequest(new
    //     {
    //         success = false,
    //         errors = _notifications.GetNotifications().Select(n => n.Value),
    //     });
    // }

    // protected void NotifyModelStateErrors()
    // {
    //     var errors = ModelState.Values.SelectMany(v => v.Errors);
    //     foreach (var err in errors)
    //     {
    //         var errMsg = err.Exception == null ? err.ErrorMessage : err.Exception.Message;
    //         NotifyError(string.Empty, errMsg);
    //     }
    // }

    // protected void NotifyError(string code, string message)
    // {
    //     _mediator.RaiseEvent(new DomainNotificationDTO(code, message));
    // }

    // protected void AddIdentityErrors(IdentityResult result)
    // {
    //     foreach (var error in result.Errors)
    //     {
    //         NotifyError(result.ToString(), error.Description);
    //     }
    // }
    //
    // protected ObjectResult Handle([ActionResultObjectValue] object response)
    // {
    //     var responseError = new ApiResponse(false);
    //
    //     switch (response)
    //     {
    //         case BaseException e:
    //             // var responseError = new ApiResponse(false)
    //             responseError.ErrorCode = e.ErrorCode;
    //             responseError.ErrorMessage = e.Message;
    //
    //             return new ObjectResult(responseError)
    //             {
    //                 StatusCode = e.HTTPStatusCode,
    //             };
    //         case Exception ex:
    //
    //             Console.WriteLine("\nMessage ---\n{0}", ex.Message);
    //             Console.WriteLine(
    //                 "\nHelpLink ---\n{0}", ex.HelpLink);
    //             Console.WriteLine("\nSource ---\n{0}", ex.Source);
    //             Console.WriteLine(
    //                 "\nStackTrace ---\n{0}", ex.StackTrace);
    //             Console.WriteLine(
    //                 "\nTargetSite ---\n{0}", ex.TargetSite);
    //
    //             responseError.ErrorCode = "INTERNAL_ERROR";
    //             responseError.ErrorMessage = "Internal Server Error";
    //             return new ObjectResult(responseError)
    //             {
    //                 StatusCode = 500
    //             };
    //         default:
    //             return new OkObjectResult(response);
    //     }
    // }
    //
    // public override OkObjectResult Ok([ActionResultObjectValue] object? value)
    // {
    //     if (value != null) return base.Ok(new ApiResponse(value));
    //     return base.Ok(new ApiResponse(true));
    // }
}