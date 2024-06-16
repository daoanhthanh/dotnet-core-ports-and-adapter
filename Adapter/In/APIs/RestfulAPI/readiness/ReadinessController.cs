using Microsoft.AspNetCore.Mvc;

namespace Adapter.In.RestfulAPI.readiness;

[ApiController]
[Route("/")]
public class ReadinessController: ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Hello World!";
    }
}