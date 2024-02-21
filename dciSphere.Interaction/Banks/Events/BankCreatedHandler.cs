using dciSphere.Abstraction.Events;
using dciSphere.Messages.Banks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Interaction.Banks.Events;
public class BankCreatedHandler(ILogger<BankCreatedHandler> logger) : IEventHandler<dciSphere.Messages.Banks.BankCreated>
{
    public Task HandleAsync(BankCreated @event, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Bank created, now time to handle side effects of this result");
        return Task.CompletedTask;
    }
}