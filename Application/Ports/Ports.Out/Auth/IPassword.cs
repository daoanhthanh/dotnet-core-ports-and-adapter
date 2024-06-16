namespace Ports.Out.Auth;

public interface IPassword
{
    public string Hash(string password);
    public bool Verify(string password, string hash);

}