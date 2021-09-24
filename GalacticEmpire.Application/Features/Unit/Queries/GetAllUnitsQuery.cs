using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Unit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Unit.Queries
{
    public static class GetAllUnitsQuery
    {
        public class Query : IRequest<List<UnitDto>> { }

        public class Handler : IRequestHandler<Query, List<UnitDto>>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IMapper mapper;

            public Handler(GalacticEmpireDbContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<List<UnitDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var units = await dbContext.Units
                    .Include(unit => unit.UnitLevels)
                    .Include(unit => unit.UnitPriceMaterials)
                        .ThenInclude(upm => upm.Material)
                    .ProjectTo<UnitDto>(mapper.ConfigurationProvider)
                    .ToListAsync();

                return units;
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
