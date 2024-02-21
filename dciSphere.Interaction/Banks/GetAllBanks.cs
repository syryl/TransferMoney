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
public class GetAllBanks
{
    [GetCached("GetAlBanks", 10)]
    public record Query() : IRequest<List<BankDto>>;

    public class Handler(IViewRepository<Bank> vrepo) : IRequestHandler<Query, List<BankDto>>
    {
        public async Task<List<BankDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var banks = await vrepo.View.Select(x => x.MapToDto()).ToListAsync();
            return banks;
        }
    }
}
