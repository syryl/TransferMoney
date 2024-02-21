using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Cache;
public interface ICacheRepository
{
    Task SetAsync<T>(CacheRecord cacheRecord, T data);
    Task<T?> GetAsync<T>(CacheRecord cacheRecord);
}
