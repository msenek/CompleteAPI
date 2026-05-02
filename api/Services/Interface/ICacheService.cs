using System.Threading.Tasks;

namespace TestAPI.Services.Interface
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(T value, string key, TimeSpan ttl);
        Task RemoveAsync(string key);
    }
}
