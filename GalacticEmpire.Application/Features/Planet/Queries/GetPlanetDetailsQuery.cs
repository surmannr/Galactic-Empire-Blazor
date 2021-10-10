using AutoMapper;
using FluentValidation;
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
    public static class GetPlanetDetailsQuery
    {
        public class Query : IRequest<PlanetDetailsDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, PlanetDetailsDto>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IMapper mapper;

            public Handler(GalacticEmpireDbContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<PlanetDetailsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var planet = await dbContext.Planets
                    .Where(p => p.Id == request.Id)
                    .Include(planet => planet.PlanetProperty)
                    .Include(e => e.EmpirePlanets)
                    .Include(planet => planet.PlanetPriceMaterials)
                        .ThenInclude(ppm => ppm.Material)
                    .Select(p => new PlanetDetailsDto
                    {
                        Id = p.Id,
                        CapturingTime = mapper.Map<TimeDto>(p.CapturingTime),
                        Description = p.Description,
                        ImageUrl = p.ImageUrl,
                        Name = p.Name,
                        PlanetProperty = new PlanetPropertyDto
                        {
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
                    .SingleOrDefaultAsync();

                return planet;
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Id).NotNull().WithMessage("Az azonosító nem lehet null");
            }
        }
    }
}
