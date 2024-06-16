
using Ports.Out.Auth;

namespace Adapter.Out.ThirdPartyLibs;

public class BcryptPassword: IPassword
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}