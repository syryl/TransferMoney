using dciSphere.Abstraction.Extensions;
using dciSphere.Abstraction.Mediatr;
using dciSphere.Abstraction.Wrapping;
using dciSphere.Abstractions.Repository;
using dciSphere.Core.Models;
using dciSphere.Interaction.Banks.Dto;
using dciSphere.Interaction.Banks.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Interaction.Banks;
public class BrowseBanks
{
    public record Query() : PagedRequest<PagedList<BankDto>>;

    public class Handler(IViewRepository<Bank> vrepo) : IRequestHandler<Query, PagedList<BankDto>>
    {
        public async Task<PagedList<BankDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var banks = await vrepo.View.Select(x => x.MapToDto()).PaginateAsync(request);
            return banks;
        }
    }
}
