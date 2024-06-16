using Domain.Core.Session;
using Ports.In.Auth;
using Ports.In.Cache;
using Ports.Out.Auth;
using Ports.Out.User;

namespace Domain.Services.Auth;

public class Authenticator(
    IUserRepository repo,
    IPassword pwdService,
    ICache cache
) : IAuthenticator
{
    public async Task<SessionUser> Login(string phoneOrEmail, string password)
    {
        // Thực hiện logic login
        throw new NotImplementedException();    
    }
}