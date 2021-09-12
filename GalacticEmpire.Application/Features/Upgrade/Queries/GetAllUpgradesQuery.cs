using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Upgrade;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Upgrade.Queries
{
    public static class GetAllUpgradesQuery
    {
        public class Query : IRequest<List<UpgradeDto>>
        {

        }

        public class Handler : IRequestHandler<Query, List<UpgradeDto>>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IMapper mapper;

            public Handler(GalacticEmpireDbContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<List<UpgradeDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var upgrades = await dbContext.Upgrades
                    .Include(upgrade => upgrade.UpgradePriceMaterials)
                        .ThenInclude(upm => upm.Material)
                    .ProjectTo<UpgradeDto>(mapper.ConfigurationProvider)
                    .ToListAsync();

                return upgrades;
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {

            }
        }
    }
}
