using dciSphere.Abstraction.Cache;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Cache;
internal class InMemoryCacheProvider(IMemoryCache provider) : ICacheRepository
{
    public async Task<T> GetAsync<T>(CacheRecord cacheRecord)
    {
        var value = provider.Get(cacheRecord.GetKey());
        if(value is not null)
            return await Task.FromResult((T)value);
        return default;
    }
    public Task SetAsync<T>(CacheRecord cacheRecord, T data)
    {
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(cacheRecord.Expiration);
        provider.Set<T>(cacheRecord.GetKey(), data, cacheOptions);
        return Task.CompletedTask;
    }
}
