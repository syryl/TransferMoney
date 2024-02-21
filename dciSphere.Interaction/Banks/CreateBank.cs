using dciSphere.Abstraction.Events;
using dciSphere.Abstractions.Repository;
using dciSphere.Core.Models;
using dciSphere.Interaction.Banks.Events;
using dciSphere.Messages.Banks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Interaction.Banks;
public class CreateBank
{
    public record Command(string Name) : IRequest<bool>;
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator(IViewRepository<Bank> vrepo)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .MustAsync(async (name, ct) => !await vrepo.View.AnyAsync(x => x.Name == name, ct))
                .WithMessage("Already bank name called {PropertyValue} exists");
        }
    }
    public sealed class Handler(IRepository<Bank> repo, IEventDispatcher dispatcher) : IRequestHandler<Command, bool>
    {
        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var bank = new Bank(request.Name);
            await repo.Entity.AddAsync(bank);
            await repo.CommitAsync();
            await dispatcher.PublishAsync(new BankCreated(request.Name));
            return true;
        }
    }
}
