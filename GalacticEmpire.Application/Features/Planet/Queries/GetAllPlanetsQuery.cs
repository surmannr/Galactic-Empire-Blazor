using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Planet;
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
    public static class GetAllPlanetsQuery
    {
        public class Query : IRequest<List<PlanetDto>> { }

        public class Handler : IRequestHandler<Query, List<PlanetDto>>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IMapper mapper;

            public Handler(GalacticEmpireDbContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<List<PlanetDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var planets = await dbContext.Planets
                    .Include(planet => planet.PlanetPriceMaterials)
                        .ThenInclude(ppm => ppm.Material)
                    .ProjectTo<PlanetDto>(mapper.ConfigurationProvider)
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
