using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;

namespace Application.Common.MediatR
{
    public class SecuredBaseQuery<TRequest> : IRequest<TRequest>, ISecuredRequest
    {
        public SecuredBaseQuery(params string[] roles)
        {
            Roles = roles;
        }

        public string[] Roles { get; set; }

        public void SetRoles(params string[] roles) => Roles = roles;
    }

    public class CachableBaseQuery<TRequest> : IRequest<TRequest>, ICachableRequest
    {
        public CachableBaseQuery(bool bypassCache, string cacheKey, TimeSpan? slidingExpiration)
        {
            BypassCache = bypassCache;
            CacheKey = cacheKey;
            SlidingExpiration = slidingExpiration;
        }

        public bool BypassCache { get; set; }
        public string CacheKey { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }

        public void SetCacheValues(bool bypassCache, string cacheKey, TimeSpan? slidingExpiration)
        {
            BypassCache = bypassCache;
            CacheKey = cacheKey;
            SlidingExpiration = slidingExpiration;
        }
    }

    public class CachableAndSecuredBaseQuery<TRequest> : IRequest<TRequest>, ICachableRequest, ISecuredRequest
    {
        public CachableAndSecuredBaseQuery(bool bypassCache, string cacheKey, TimeSpan? slidingExpiration, params string[] roles)
        {
            BypassCache = bypassCache;
            CacheKey = cacheKey;
            SlidingExpiration = slidingExpiration;
            Roles = roles;
        }

        public bool BypassCache { get; set; }
        public string CacheKey { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }
        public string[] Roles { get; set; }

        public void SetCacheValues(bool bypassCache, string cacheKey, TimeSpan? slidingExpiration)
        {
            BypassCache = bypassCache;
            CacheKey = cacheKey;
            SlidingExpiration = slidingExpiration;
        }

        public void SetRoles(params string[] roles) => Roles = roles;
    }
}
