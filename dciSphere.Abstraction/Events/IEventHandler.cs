using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Events;
public interface IEventHandler<in TEvent> where TEvent : class, IEvent
{
    public Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}