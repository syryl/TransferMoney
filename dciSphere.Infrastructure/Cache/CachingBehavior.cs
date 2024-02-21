using dciSphere.Abstraction.Cache;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Cache;
public class CachingBehavior<TRequest, TResponse>(ILogger<CachingBehavior<TRequest, TResponse>> logger, ICacheRepository crepo) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var cacheAttribute = request.GetType().GetCustomAttribute(typeof(GetCached), true) as GetCached;
        if (cacheAttribute is null)
            return await next();
        string? findCacheKeyProperty(){
            var properties = request.GetType().GetProperties();
            foreach (var property in properties)
            { 
                var cacheKeyAttribute = property.GetCustomAttribute(typeof(CacheKey), true) as CacheKey;
                if (cacheKeyAttribute is not null)
                    return property.GetValue(request).ToString();
            }
            return null;
        }
        var cacheKey = findCacheKeyProperty();
        if(cacheKey is null)
        {
            logger.LogWarning("Cache attribute is set to {request} yet there is not set cache key", typeof(TRequest).FullName);
        }
        var response = await next();

        var cacheRecord = cacheAttribute.CacheRecord;
        cacheRecord.SetPostfix(cacheKey);
        logger.LogInformation("Cache key {key} is set, starting to retrieve cache data...", cacheRecord.GetKey());
        var cachedValue = await crepo.GetAsync<TResponse>(cacheRecord);
        if (cachedValue is not null)
        {
            logger.LogInformation("Successfully retrieved cache data from {request}", typeof(TRequest).FullName);
            return cachedValue;
        }
            
        await crepo.SetAsync<TResponse>(cacheRecord, response);

        return response;
    }
}