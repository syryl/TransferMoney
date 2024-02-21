using dciSphere.Core.ValueObject;
using FluentValidation;
using dciSphere.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using dciSphere.Abstractions.Repository;

namespace dciSphere.Interaction.Accounts;
public sealed class CreateAccount
{
    public record Command
        (
            string Name,
            int BankId
        ) : IRequest<bool>;

    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator(IViewRepository<Account> vrepo)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .MustAsync(async (name, ct) => !await vrepo.View.AnyAsync(x => x.Name == name, ct))
                .WithMessage("Already account name called {PropertyValue} exists");
        }
    }

    public sealed class Handler(IRepository<Account> repo) : IRequestHandler<Command, bool>
    {
        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var account = new Account(request.Name, new Money(), request.BankId);
            await repo.Entity.AddAsync(account);
            await repo.CommitAsync();
            return true;
        }
    }
}
