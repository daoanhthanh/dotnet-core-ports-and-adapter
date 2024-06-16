
namespace Ports.Out.Cache;

public interface ICache
{
    /// <summary>
    /// Get Data using key
    /// </summary>
    T? GetData<T>(string key);

    /// <summary>
    /// Set Data with Value and Expiration Time of Key in seconds, default is -1 (no expiration)
    /// </summary>
    bool SetData<T>(string key, T value, long ttl = -1);

    /// <summary>
    /// Remove Data
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    object RemoveData(string key);
}