using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Logging;
public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Waiting to handle [{request}] process to [{response}] response", typeof(TRequest).Name, typeof(TResponse).Name);
        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();
        var timeTaken = timer.Elapsed;
        logger.LogInformation("Request [{request}] is completed in {time} seconds and now [{response}] response is releasing", typeof(TRequest).Name, Math.Round(timeTaken.TotalSeconds, 2), typeof(TResponse).Name);
        
        return response;
    }
}
