using dciSphere.Abstraction.Events;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace dciSphere.Infrastructure.Events;
public class EventDispatcher : IEventDispatcher
{
    private readonly Channel<IEvent> _queue = Channel.CreateUnbounded<IEvent>();
    public async Task PublishAsync(IEvent task, CancellationToken cancellationToken = default)
    {
        await _queue.Writer.WriteAsync(task);
    }

    public ValueTask<IEvent> Subscribe(CancellationToken cancellationToken)
    {
        return _queue.Reader.ReadAsync(cancellationToken);
    }

    public int Count()
    {
        return _queue.Reader.Count;
    }
}
