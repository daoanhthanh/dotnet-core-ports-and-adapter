using Domain.Core.Session;

namespace Ports.In.Cache;

public interface ICache
{
    
    /// <summary>
    /// Set session for login, default is 2 hours
    /// </summary>
    bool SetSession(SessionUser value);

    public SessionUser? GetSession(SessionId key);
    
    public void RemoveSession(SessionId key);
}