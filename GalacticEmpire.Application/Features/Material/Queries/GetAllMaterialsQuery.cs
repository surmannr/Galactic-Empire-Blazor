using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Material;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Material.Queries
{
    public static class GetAllMaterialsQuery
    {
        public class Query : IRequest<List<MaterialDto>> { }

        public class Handler : IRequestHandler<Query, List<MaterialDto>>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IMapper mapper;

            public Handler(GalacticEmpireDbContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<List<MaterialDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var materials = await this.dbContext.Materials
                    .ProjectTo<MaterialDto>(mapper.ConfigurationProvider)
                    .ToListAsync();

                return materials;
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
