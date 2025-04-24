using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using SneakerECommerce.Application.Common;
using SneakerECommerce.Application.Interfaces.ICachingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public T? GetPaginationData<T>(string key)
        {
            var data = _cache?.GetString(key);
            string jsonContent = data;
            if (data.StartsWith("data\""))
            {
                jsonContent = data.Substring(5, data.Length - 6);
                jsonContent = System.Text.RegularExpressions.Regex.Unescape(jsonContent);
            }

            // Deserialize trực tiếp với options để xử lý case-insensitive
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(jsonContent, options);
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
