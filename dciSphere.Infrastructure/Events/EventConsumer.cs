using dciSphere.Abstraction.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Events;
public class EventConsumer(ILogger<EventConsumer> logger, IServiceProvider serviceProvider) : BackgroundService
{
   
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(EventConsumer)} is running....");
        await ProcessEventTask(cancellationToken);
    }

    private async Task ProcessEventTask(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var scope = serviceProvider.CreateAsyncScope();
                var dispatcher = scope.ServiceProvider.GetService<IEventDispatcher>();
                logger.LogInformation("Waiting for new event to be processed...");
                var eventTask = await dispatcher.Subscribe(cancellationToken);
                var eventType = eventTask.GetType();
                logger.LogInformation("Running task {EventType}", eventType);
                var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
                var tasks = scope.ServiceProvider.GetServices(handlerType).Select(x =>
                {
                    return ((dynamic)x!).HandleAsync((dynamic)eventTask, cancellationToken) as Task;
                });
                await Task.WhenAll(tasks);
                logger.LogInformation("Completed task {EventType}", eventType);
                logger.LogInformation("In queue there are {ItemsCount} events to be consumed", dispatcher.Count());
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("Operation of handling event task canceled");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred executing event task work.");
            }
        }
    }
}
