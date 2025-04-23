using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using SneakerECommerce.Application.Interfaces.ICachingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SneakerECommerce.Application.InMemoryCache
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache? _cache;

        public CachingService(IDistributedCache? cache)
        {
            _cache = cache;
        }

        public T? GetData<T>(string key)
        {
            var data = _cache?.GetString(key);
            if (data == null)
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(data);
        }

        public object RemoveData(string key)
        {
            throw new NotImplementedException();
        }

        public void SetData<T>(string key, T data)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            };
            _cache?.SetString(key, JsonSerializer.Serialize(data), options);
        }
    }
}
