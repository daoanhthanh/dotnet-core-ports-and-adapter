using Domain.Core.Session;
using Ports.In.Cache;

namespace Domain.Services.Cache;

public class CacheService: ICache
{
    public bool SetSession(SessionUser value)
    {
        throw new NotImplementedException();
    }

    public SessionUser? GetSession(SessionId key)
    {
        throw new NotImplementedException();
    }

    public void RemoveSession(SessionId key)
    {
        throw new NotImplementedException();
    }
}