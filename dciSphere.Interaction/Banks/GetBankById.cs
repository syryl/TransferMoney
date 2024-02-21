using dciSphere.Abstraction.Cache;
using dciSphere.Abstractions.Repository;
using dciSphere.Core.Models;
using dciSphere.Interaction.Banks.Dto;
using dciSphere.Interaction.Banks.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Interaction.Banks;
public class GetBankById
{
    [GetCached("GetBankById", 5)]
    public record Query : IRequest<BankDto>
    {
        [CacheKey]
        public int Id { get; init; }
    }

    public class Handler(IViewRepository<Bank> vrepo) : IRequestHandler<Query, BankDto>
    {
        public async Task<BankDto> Handle(Query request, CancellationToken cancellationToken) => (await vrepo.View.SingleOrDefaultAsync(x => x.Id == request.Id)).MapToDto();
    }
}
