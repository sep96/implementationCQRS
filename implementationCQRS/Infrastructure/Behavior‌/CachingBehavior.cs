using implementationCQRS.Queries.Caching;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace implementationCQRS.Infrastructure.Behavior_
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICachableQuery
    {
        private readonly IDistributedCache _cache;


        public CachingBehavior(IDistributedCache distributedCache)
        {
            _cache = distributedCache;
        }



        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response;
            var cachedResponse = await _cache.GetAsync(request.Key, cancellationToken);
            if (cachedResponse != null)
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
            else
            {
                response = await next();
                var serialized = Encoding.Default.GetBytes(JsonConvert.SerializeObject(response));
                await _cache.SetAsync(request.Key, serialized, cancellationToken);
            }
            return response;
        }
    }
}
