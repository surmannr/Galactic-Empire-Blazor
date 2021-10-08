using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Material;
using GalacticEmpire.Shared.Dto.Planet;
using GalacticEmpire.Shared.Dto.Time;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Planet.Queries
{
    public static class GetAllCapturablePlanetsQuery
    {
        public class Query : IRequest<List<PlanetDetailsDto>> { }

        public class Handler : IRequestHandler<Query, List<PlanetDetailsDto>>
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

            public async Task<List<PlanetDetailsDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = identityService.GetCurrentUserId();

                var empire = await dbContext.Empires
                    .Where(e => e.OwnerId == userId)
                    .Include(e => e.EmpirePlanets)
                    .SingleAsync();

                var planets = await dbContext.Planets
                    .Include(planet => planet.PlanetProperty)
                    .Include(e => e.EmpirePlanets)
                    .Include(planet => planet.PlanetPriceMaterials)
                        .ThenInclude(ppm => ppm.Material)
                    .Select(p => new PlanetDetailsDto { 
                        Id = p.Id,
                        CapturingTime = mapper.Map<TimeDto>(p.CapturingTime),
                        Description = p.Description,
                        ImageUrl = p.ImageUrl,
                        Name = p.Name,
                        PlanetProperty = new PlanetPropertyDto { 
                            BaseBitcoin = p.PlanetProperty.BaseBitcoin,
                            BaseFood = p.PlanetProperty.BaseFood,
                            BaseQuartz = p.PlanetProperty.BaseQuartz,
                            MaxPopulationCount = p.PlanetProperty.MaxPopulationCount,
                            MaxUnitCount = p.PlanetProperty.MaxUnitCount,
                            PlanetId = p.Id
                        },
                        IsCaptured = p.EmpirePlanets.Any(s => s.PlanetId == p.Id),
                        RequiredMaterials = mapper.Map<List<PriceMaterialDto>>(p.PlanetPriceMaterials)
                    })
                    .ToListAsync();

                return planets;
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
