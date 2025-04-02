using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.MemoryCache
{
    public interface IMemoryCacheService
    {
        void Set(string key, object value, TimeSpan expireTimeSpan);

        T Get<T>(string key) where T : class;

        List<T> GetList<T>(string key) where T : class;

        void Remove(string key);
    }
}
