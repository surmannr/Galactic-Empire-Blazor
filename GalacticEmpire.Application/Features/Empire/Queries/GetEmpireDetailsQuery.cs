using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Empire;
using GalacticEmpire.Shared.Dto.Event;
using GalacticEmpire.Shared.Dto.Material;
using GalacticEmpire.Shared.Dto.Unit;
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
                    .Include(e => e.EmpireMaterials)
                        .ThenInclude(e => e.Material)
                    .Include(e => e.EmpirePlanets)
                        .ThenInclude(e => e.Planet)
                            .ThenInclude(e => e.PlanetProperty)
                    .Include(e => e.EmpireEvents)
                        .ThenInclude(e => e.Event)
                    .FirstOrDefaultAsync();

                var empireEvent = empire.EmpireEvents.LastOrDefault();

                var empireDetails = new EmpireDetailsDto
                {
                    Id = empire.Id,
                    Name = empire.Name,
                    MaxNumberOfUnits = empire.MaxNumberOfUnits,
                    MaxNumberOfPopulation = empire.MaxNumberOfPopulation,
                    Event = mapper.Map<EventDto>(empireEvent),
                    Planets = mapper.Map<List<EmpirePlanetDto>>(empire.EmpirePlanets),
                    Materials = mapper.Map<List<MaterialDetailsDto>>(empire.EmpireMaterials),
                    AllianceName = empire.Alliance?.Alliance.Name ?? null,
                    AllianceInvitationRight = empire.Alliance?.InvitationRight,
                    IsAllianceLeader = empire.Alliance?.IsLeader,
                    Population = empire.Population,
                    Units = mapper.Map<List<BattleUnitDto>>(empire.EmpireUnits)
                };

                return empireDetails;
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
