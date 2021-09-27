using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using GalacticEmpire.Application.ExtensionsAndServices.Identity;
using GalacticEmpire.Application.ExtensionsAndServices.PaginationExtensions;
using GalacticEmpire.Application.PaginationExtensions;
using GalacticEmpire.Dal;
using GalacticEmpire.Shared.Dto.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.Features.User.Queries
{
    public static class GetAttackableUsersQuery
    {
        public class Query : IRequest<PagedResult<AttackableUserDto>>
        {
            public PaginationData PaginationData { get; set; }
            public string? Filter { get; set; }
        }

        public class Handler : IRequestHandler<Query, PagedResult<AttackableUserDto>>
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

            public async Task<PagedResult<AttackableUserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = identityService.GetCurrentUserId();

                var empire = await dbContext.Empires
                    .Include(e => e.Alliance)
                    .Where(e => e.OwnerId == userId)
                    .FirstOrDefaultAsync();

                var allianceMembers = dbContext.AllianceMembers
                    .Where(a => a.AllianceId == empire.Alliance.AllianceId)
                    .AsEnumerable();

                var attackableUsers = dbContext.Users
                    .Include(u => u.Empire)
                    .Where(u => u.Id != userId &&
                        !allianceMembers.Any(am => am.EmpireId == u.Empire.Id)
                    )
                    .ProjectTo<AttackableUserDto>(mapper.ConfigurationProvider)
                    .AsQueryable();


                if (!string.IsNullOrEmpty(request.Filter) && !string.IsNullOrWhiteSpace(request.Filter))
                {
                    attackableUsers = attackableUsers
                        .Where(u => u.UserName.Contains(request.Filter) || u.EmpireName.Contains(request.Filter));
                }

                return attackableUsers.ToPagedList(request.PaginationData.PageSize, request.PaginationData.PageNumber); ;
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
