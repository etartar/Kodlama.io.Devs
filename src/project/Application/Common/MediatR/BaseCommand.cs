using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;

namespace Application.Common.MediatR
{
    public class SecuredBaseCommand<TRequest> : IRequest<TRequest>, ISecuredRequest
    {
        public SecuredBaseCommand(params string[] roles)
        {
            Roles = roles;
        }

        public string[] Roles { get; set; }

        public void SetRoles(params string[] roles) => Roles = roles;
    }

    public class CacheRemoverBaseCommand<TRequest> : IRequest<TRequest>, ICacheRemoverRequest
    {
        public CacheRemoverBaseCommand(bool bypassCache, string cacheKey)
        {
            BypassCache = bypassCache;
            CacheKey = cacheKey;
        }

        public bool BypassCache { get; set; }
        public string CacheKey { get; set; }

        public void SetCacheValues(bool bypassCache, string cacheKey)
        {
            BypassCache = bypassCache;
            CacheKey = cacheKey;
        }
    }

    public class CacheRemoverAndSecuredBaseCommand<TRequest> : IRequest<TRequest>, ICacheRemoverRequest, ISecuredRequest
    {
        public CacheRemoverAndSecuredBaseCommand(bool bypassCache, string cacheKey, params string[] roles)
        {
            BypassCache = bypassCache;
            CacheKey = cacheKey;
            Roles = roles;
        }

        public bool BypassCache { get; set; }
        public string CacheKey { get; set; }
        public string[] Roles { get; set; }

        public void SetCacheValues(bool bypassCache, string cacheKey)
        {
            BypassCache = bypassCache;
            CacheKey = cacheKey;
        }

        public void SetRoles(params string[] roles) => Roles = roles;
    }
}
