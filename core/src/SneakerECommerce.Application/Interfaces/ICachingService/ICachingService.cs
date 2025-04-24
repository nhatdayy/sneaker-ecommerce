using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerECommerce.Application.Interfaces.ICachingService
{
    public interface ICachingService
    {
        public T? GetData<T>(string key);
        public T? GetPaginationData<T>(string key);
        public object RemoveData(string key);
        public void SetData<T>(string key, T value);
    }
}
