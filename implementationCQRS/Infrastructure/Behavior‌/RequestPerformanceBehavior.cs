using MediatR;
using System.Diagnostics;

namespace implementationCQRS.Infrastructure.Behavior_
{
    public class RequestPerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<RequestPerformanceBehavior<TRequest, TResponse>> _logger;

        public RequestPerformanceBehavior(ILogger<RequestPerformanceBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async  Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            TResponse response = await next();

            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds > TimeSpan.FromSeconds(5).Milliseconds)
            {
                // This method has taken a long time, So we log that to check it later.
                _logger.LogWarning($"{request} has taken {stopwatch.ElapsedMilliseconds} to run completely !");
            }

            return response;
        }
    }
}
