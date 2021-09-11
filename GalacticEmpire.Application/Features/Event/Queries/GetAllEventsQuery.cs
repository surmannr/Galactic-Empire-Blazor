using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Event;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Event.Queries
{
    public static class GetAllEventsQuery
    {
        public class Query : IRequest<List<EventDto>> { }

        public class Handler : IRequestHandler<Query, List<EventDto>>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IMapper mapper;

            public Handler(GalacticEmpireDbContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<List<EventDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var events = await this.dbContext.Events.ProjectTo<EventDto>(mapper.ConfigurationProvider).ToListAsync();
                return events;
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