using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
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
    public static class GetRankListQuery
    {
        public class Query : IRequest<PagedResult<UserRankDto>>
        {
            public PaginationData? PaginationData { get; set; }
            public string? Filter { get; set; }
        }

        public class Handler : IRequestHandler<Query, PagedResult<UserRankDto>>
        {
            private readonly GalacticEmpireDbContext dbContext;
            private readonly IMapper mapper;

            public Handler(GalacticEmpireDbContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<PagedResult<UserRankDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = dbContext.Users
                    .OrderByDescending(u => u.Points)
                    .Include(user => user.Empire)
                    .AsQueryable()
                    .ProjectTo<UserRankDto>(mapper.ConfigurationProvider);

                // Filter ellenőrzés
                if (!string.IsNullOrEmpty(request.Filter) && !string.IsNullOrWhiteSpace(request.Filter))
                {
                    users = users.Where(u => u.UserName.Contains(request.Filter) || u.EmpireName.Contains(request.Filter));
                }

                var pagedList = await GetPagedResultWithCheck(users,request);

                // Helyezés számítás, ugyanannyi ponttal rendelkező emberek ugyanazon a helyen szerepelnek
                foreach (var user in pagedList.Results)
                {
                    user.Placement = (await dbContext.Users.OrderByDescending(u => u.Points).Where(u => u.Points > user.Points).CountAsync()) + 1;
                }

                return pagedList;
            }

            private async Task<PagedResult<UserRankDto>> GetPagedResultWithCheck(IQueryable<UserRankDto> users, Query request)
            {
                if (request.PaginationData is not null)
                {
                    return await users.ToPagedListAsync(request.PaginationData.PageSize, request.PaginationData.PageNumber);
                }
                else
                {
                    return await users.ToPagedListAsync(users.Count(), 1);
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
