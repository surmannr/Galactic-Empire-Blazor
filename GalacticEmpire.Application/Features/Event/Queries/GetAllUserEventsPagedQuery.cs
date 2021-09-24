using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.ExtensionsAndServices.PaginationExtensions;
using GalacticEmpire.Application.PaginationExtensions;
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
    public static class GetAllUserEventsPagedQuery
    {
        public class Query : IRequest<PagedResult<EventDto>>
        {
            public PaginationData? PaginationData { get; set; }
            public string? Filter {  get; set; }
        }

        public class Handler : IRequestHandler<Query, PagedResult<EventDto>>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IMapper mapper;
            private readonly IIdentityService identityService;

            public Handler(GalacticEmpireDbContext dbContext, IMapper mapper, IIdentityService identityService)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
                this.identityService = identityService;
            }

            public async Task<PagedResult<EventDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = identityService.GetCurrentUserId();

                var empire = await dbContext.Empires
                    .Where(e => e.OwnerId == userId)
                    .Include(e => e.EmpireEvents)
                        .ThenInclude(e => e.Event)
                    .SingleOrDefaultAsync();

                var events = empire.EmpireEvents
                    .OrderByDescending(e => e.Date)
                    .AsQueryable()
                    .ProjectTo<EventDto>(mapper.ConfigurationProvider);

                // Filter ellenőrzés
                if (!string.IsNullOrEmpty(request.Filter) && !string.IsNullOrWhiteSpace(request.Filter))
                {
                    events = events.Where(u => u.Name.Contains(request.Filter) || u.Description.Contains(request.Filter));
                }

                var pagedEvents = GetPagedResultWithCheck(events, request);

                return pagedEvents;
            }

            private PagedResult<EventDto> GetPagedResultWithCheck(IQueryable<EventDto> events, Query request)
            {
                if (request.PaginationData is not null)
                {
                    return events.ToPagedList(request.PaginationData.PageSize, request.PaginationData.PageNumber);
                }
                else
                {
                    return events.ToPagedList(events.Count(), 1);
                }
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.PaginationData).SetValidator(new PaginationRuleValidator()).When(x => x.PaginationData is not null);
            }
        }
    }
}
