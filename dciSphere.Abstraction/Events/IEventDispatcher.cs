using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Abstraction.Events;
public interface IEventDispatcher
{
    Task PublishAsync(IEvent task, CancellationToken cancellationToken = default);
    ValueTask<IEvent> Subscribe(CancellationToken cancellationToken);
    int Count();
}
