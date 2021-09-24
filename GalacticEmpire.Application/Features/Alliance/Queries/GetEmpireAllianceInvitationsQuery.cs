using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.ExtensionsAndServices.PaginationExtensions;
using GalacticEmpire.Application.PaginationExtensions;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.Alliance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.Alliance.Queries
{
    public static class GetEmpireAllianceInvitationsQuery
    {
        public class Query : IRequest<PagedResult<AllianceInvitationDto>>
        {
            public PaginationData PaginationData { get; set; }
            public string Filter { get; set; }
        }

        public class Handler : IRequestHandler<Query, PagedResult<AllianceInvitationDto>>
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

            public async Task<PagedResult<AllianceInvitationDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = identityService.GetCurrentUserId();

                var empire = await dbContext.Empires
                    .Include(e => e.Alliance)
                    .Include(e => e.AllianceReceivedInvitations)
                        .ThenInclude(e => e.Alliance)
                            .ThenInclude(e => e.Members)
                    .Include(e => e.AllianceReceivedInvitations)
                        .ThenInclude(e => e.InviterEmpire)
                    .Where(e => e.OwnerId == userId)
                    .FirstOrDefaultAsync();

                var empireInvitations = empire.AllianceReceivedInvitations
                    .AsQueryable()
                    .ProjectTo<AllianceInvitationDto>(mapper.ConfigurationProvider);

                // Filter ellenőrzés
                if (!string.IsNullOrEmpty(request.Filter) && !string.IsNullOrWhiteSpace(request.Filter))
                {
                    empireInvitations = empireInvitations.Where(u => u.AllianceName.Contains(request.Filter) || u.InviterEmpireName.Contains(request.Filter));
                }

                return empireInvitations.ToPagedList(request.PaginationData.PageSize, request.PaginationData.PageNumber);
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
