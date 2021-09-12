using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Empire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Empire.Queries
{
    public static class GetEmpireDetailsQuery
    {
        public class Query : IRequest<EmpireDetailsDto> { }

        public class Handler : IRequestHandler<Query, EmpireDetailsDto>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IIdentityService identityService;
            private readonly IMapper mapper;

            public Handler(GalacticEmpireDbContext dbContext, IIdentityService identityService, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.identityService = identityService;
                this.mapper = mapper;
            }

            public async Task<EmpireDetailsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = identityService.GetCurrentUserId();

                var empire = await dbContext.Empires
                    .Where(e => e.OwnerId == userId)
                    .Include(e => e.EmpireUnits)
                        .ThenInclude(e => e.Unit)
                    .Include(e => e.Alliance)
                        .ThenInclude(e => e.Alliance)
                    .Include(e => e.EmpirePlanets)
                        .ThenInclude(e => e.EmpirePlanetUpgrades)
                            .ThenInclude(e => e.Upgrade)
                    .Include(e => e.EmpirePlanets)
                        .ThenInclude(e => e.Planet)
                            .ThenInclude(e => e.PlanetProperty)
                    .ProjectTo<EmpireDetailsDto>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                return empire;
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
