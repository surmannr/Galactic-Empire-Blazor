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
        public class Query : IRequest<List<AttackableUserDto>>
        {
            public string? Filter { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<AttackableUserDto>>
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

            public async Task<List<AttackableUserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = identityService.GetCurrentUserId();

                var empire = await dbContext.Empires
                    .Include(e => e.Alliance)
                    .Where(e => e.OwnerId == userId)
                    .FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(request.Filter) && !string.IsNullOrWhiteSpace(request.Filter))
                {
                    return await dbContext.Users
                    .Include(u => u.Empire)
                    .Where(u => u.Id != userId && !u.Empire.Alliance.Alliance.Members.Any(am => am.EmpireId == u.Empire.Id)
                        && u.UserName.Contains(request.Filter) || u.Empire.Name.Contains(request.Filter)
                    )
                    .ProjectTo<AttackableUserDto>(mapper.ConfigurationProvider)
                    .ToListAsync();
                }

                var attackableUsers = await dbContext.Users
                    .Include(u => u.Empire)
                    .Where(u => u.Id != userId && !u.Empire.Alliance.Alliance.Members.Any(am => am.EmpireId == u.Empire.Id))
                    .ProjectTo<AttackableUserDto>(mapper.ConfigurationProvider)
                    .ToListAsync();

                return attackableUsers;
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
