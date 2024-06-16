using System.ComponentModel.DataAnnotations;

namespace Adapter.In.RestfulAPI.V1.Auth.Forms;

public class LoginForm
{
    [Required] public string UserId { get; set; }

    [Required] public string Password { get; set; }
}